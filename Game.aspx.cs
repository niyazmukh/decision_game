using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using decision_game_web;

namespace decision_game_web
{
    public class RoundData
    {
        public int Round { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public string ClickedButton { get; set; }
        public int PointsEarned { get; set; }
    }

    public partial class Game : System.Web.UI.Page
    {
        private Random random = new Random();
        private const int MaxRounds = 100;
        private const int PointsA = 8;
        private const double ProbabilityA = 0.51;
        private const int PointsB = 4;
        private const double ProbabilityB = 0.51;
        private const int PointsC = 0;
        private const double ProbabilityC = 1;
        private const int ButtonFreezeTime = 500;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserScore"] == null)
            {
                Session["UserScore"] = 0;
            }

            if (Session["Counter"] == null)
            {
                Session["Counter"] = 1;
            }

            if (!IsPostBack)
            {
                lblMaxRounds.Text = MaxRounds.ToString();
                lblCurrentRound.Text = Session["Counter"].ToString();
                lblUserScore.Text = Session["UserScore"].ToString();

                // Generate a new GUID at the start of each game
                var gameId = Guid.NewGuid();
                Session["GameId"] = gameId;

                // Store the current time in the session when the game starts
                Session["RoundStartTime"] = DateTime.Now;
            }

            if (IsPostBack)
            {
                string eventTarget = Request["__EVENTTARGET"];
                if (eventTarget == UpdatePanel1.ClientID)
                {
                    ResetButtonProperties();
                }
            }

            if (Session["Rounds"] == null)
            {
                Session["Rounds"] = new List<RoundData>();
            }

            gvRounds.RowDataBound += new GridViewRowEventHandler(gvRounds_RowDataBound);
        }

        protected void gvRounds_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RoundData roundData = (RoundData)e.Row.DataItem;
                if (roundData != null)
                {
                    int cellIndex = roundData.ClickedButton == "A" ? 1 : roundData.ClickedButton == "B" ? 2 : 3;
                    e.Row.Cells[cellIndex].CssClass = "bold-blue";
                }
            }
        }

        private void ResetButtonProperties()
        {
            btnA.Text = "A <br/> 8 with 51% chance <br/> -8 with 49% chance";
            btnB.Text = "B <br/> 4 with 51% chance <br/> -4 with 49% chance";
            btnC.Text = "C <br/> 0 for sure";

            string script = @"
                document.getElementById('" + btnA.ClientID + @"').style.backgroundColor = '';
                document.getElementById('" + btnB.ClientID + @"').style.backgroundColor = '';
                document.getElementById('" + btnC.ClientID + @"').style.backgroundColor = '';

                var btnA = document.getElementById('" + btnA.ClientID + @"');
                var btnB = document.getElementById('" + btnB.ClientID + @"');
                var btnC = document.getElementById('" + btnC.ClientID + @"');

                // Enable the buttons
                btnA.disabled = false;
                btnB.disabled = false;
                btnC.disabled = false;

                // Remove the event listeners
                btnA.removeEventListener('click', preventFormSubmission);
                btnB.removeEventListener('click', preventFormSubmission);
                btnC.removeEventListener('click', preventFormSubmission);
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ResetButtonProperties", script, true);
        }

        private int CalculatePoints(int points, double probability)
        {
            double randomValue = random.NextDouble();

            if (randomValue <= probability)
            {
                return points;
            }
            else
            {
                return -points;
            }
        }

        private void ButtonClick(int clickedPoints, double clickedProbability)
        {
            int userScore = (int)Session["UserScore"];
            int pointsEarnedA = CalculatePoints(PointsA, ProbabilityA);
            int pointsEarnedB = CalculatePoints(PointsB, ProbabilityB);
            int pointsEarnedC = CalculatePoints(PointsC, ProbabilityC);

            Session["PointsEarnedA"] = pointsEarnedA;
            Session["PointsEarnedB"] = pointsEarnedB;
            Session["PointsEarnedC"] = pointsEarnedC;

            int clickedPointsEarned = (clickedPoints == PointsA) ? pointsEarnedA : (clickedPoints == PointsB) ? pointsEarnedB : pointsEarnedC;
            userScore += clickedPointsEarned;
            Session["UserScore"] = userScore;
            lblUserScore.Text = userScore.ToString();

            string clickedButtonId = (clickedPoints == PointsA) ? "A" : (clickedPoints == PointsB) ? "B" : "C";
            Session["ClickedButtonId"] = clickedButtonId;

            int counter = (int)Session["Counter"];
            List<RoundData> rounds = (List<RoundData>)Session["Rounds"];
            rounds.Insert(0, new RoundData { Round = counter, A = pointsEarnedA, B = pointsEarnedB, C = pointsEarnedC, ClickedButton = clickedButtonId, PointsEarned = clickedPointsEarned });
            Session["Rounds"] = rounds;

            gvRounds.DataSource = rounds;
            gvRounds.DataBind();

            counter++;
            Session["Counter"] = counter;

            // Calculate the time spent on the current round
            if (Session["RoundStartTime"] != null)
            {
                DateTime roundStartTime = (DateTime)Session["RoundStartTime"];
                TimeSpan roundTime = DateTime.Now - roundStartTime;

                // Store each round's time in the session
                if (Session["RoundTimes"] == null)
                {
                    Session["RoundTimes"] = new List<double>();
                }
                List<double> roundTimes = (List<double>)Session["RoundTimes"];
                roundTimes.Add(roundTime.TotalSeconds);
                Session["RoundTimes"] = roundTimes;
            }

            // Store the current time in the session when a new round starts
            Session["RoundStartTime"] = DateTime.Now;

            // Store each round's score in the session
            if (Session["Scores"] == null)
            {
                Session["Scores"] = new List<int>();
            }
            List<int> scores = (List<int>)Session["Scores"];
            scores.Add(userScore);
            Session["Scores"] = scores;

            lblCurrentRound.Text = counter.ToString();
            lblMaxRounds.Text = MaxRounds.ToString();

            UpdateButtonProperties(pointsEarnedA, pointsEarnedB, pointsEarnedC);

            if (counter > MaxRounds)
            {
                SessionDataSaver.SaveSessionData(Session, Server);
                Response.Redirect("EndGame.aspx");
            }
        }

        private void UpdateButtonProperties(int pointsEarnedA, int pointsEarnedB, int pointsEarnedC)
        {
            btnA.Text = "<span style='font-size:68px'>" + pointsEarnedA.ToString() + "</span>";
            btnB.Text = "<span style='font-size:68px'>" + pointsEarnedB.ToString() + "</span>";
            btnC.Text = "<span style='font-size:68px'>" + pointsEarnedC.ToString() + "</span>";

            string colorA = pointsEarnedA > 0 ? "green" : pointsEarnedA < 0 ? "red" : "blue";
            string colorB = pointsEarnedB > 0 ? "green" : pointsEarnedB < 0 ? "red" : "blue";
            string colorC = pointsEarnedC > 0 ? "green" : pointsEarnedC < 0 ? "red" : "blue";

            string script = @"
                // Disable the buttons
                var btnA = document.getElementById('" + btnA.ClientID + @"');
                var btnB = document.getElementById('" + btnB.ClientID + @"');
                var btnC = document.getElementById('" + btnC.ClientID + @"');

                btnA.disabled = true;
                btnB.disabled = true;
                btnC.disabled = true;

                // Prevent form submission when button is disabled
                btnA.addEventListener('click', function(event) {
                    if (btnA.disabled) {
                        event.preventDefault();
                    }
                });
                btnB.addEventListener('click', function(event) {
                    if (btnB.disabled) {
                        event.preventDefault();
                    }
                });
                btnC.addEventListener('click', function(event) {
                    if (btnC.disabled) {
                        event.preventDefault();
                    }
                });

                // Update the button color
                document.getElementById('" + btnA.ClientID + @"').style.backgroundColor = '" + colorA + @"';
                document.getElementById('" + btnB.ClientID + @"').style.backgroundColor = '" + colorB + @"';
                document.getElementById('" + btnC.ClientID + @"').style.backgroundColor = '" + colorC + @"';

                setTimeout(function () {
                    __doPostBack('" + UpdatePanel1.ClientID + @"', '');
                }, " + ButtonFreezeTime + @");
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "UpdateButtonProperties", script, true);
        }

        protected void btnA_Click(object sender, EventArgs e)
        {
            ButtonClick(PointsA, ProbabilityA);
        }

        protected void btnB_Click(object sender, EventArgs e)
        {
            ButtonClick(PointsB, ProbabilityB);
        }

        protected void btnC_Click(object sender, EventArgs e)
        {
            ButtonClick(PointsC, ProbabilityC);
        }
    }
}
