using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using decision_game_web;

namespace decision_game_web
{
    public partial class EndGame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserScore"] != null)
            {
                int finalScore = (int)Session["UserScore"];
                lblScore.Text = "Your final score is " + finalScore.ToString();

                // Generate a random integer between 0 and 100 for the luck level
                Random random = new Random();
                int luckLevel = random.Next(0, 101);

                // Compare the luck level with the final score and display the appropriate message
                if (finalScore >= luckLevel)
                {
                    lblMessage.Text = "Congratulations, you have earned a bonus of $1!<br/>Your completion code is: XXXX"; // Replace with your actual message
                }
                else
                {
                    lblMessage.Text = "Unfortunately, you have not received a bonus this time.<br/>Your completion code is: YYYY"; // Replace with your actual message
                }

            }

        }

        //private void SaveSessionData()
        //{
        //    // Get the session data
        //    var sessionId = Session.SessionID;
        //    var age = Session["Age"];
        //    var sex = Session["Sex"];
        //    var connectId = Session["ConnectId"];
        //    var userScore = Session["UserScore"];
        //    var rounds = Session["Rounds"] as List<RoundData>;

        //    // Create a StringBuilder to hold the CSV data
        //    var csv = new StringBuilder();

        //    // Add a header row (only if the file doesn't exist yet)
        //    if (!File.Exists(Server.MapPath("~/Data.csv")))
        //    {
        //        csv.AppendLine("SessionId,Age,Sex,ConnectId,UserScore,Round,A,B,C,ClickedButton,PointsEarned");
        //    }

        //    // Add a row for each round
        //    foreach (var round in rounds)
        //    {
        //        csv.AppendLine($"{sessionId},{age},{sex},{connectId},{userScore},{round.Round},{round.A},{round.B},{round.C},{round.ClickedButton},{round.PointsEarned}");
        //    }

        //    // Write the CSV data to a file
        //    File.AppendAllText(Server.MapPath("~/Data.csv"), csv.ToString());
        //}


        protected void btnFinish_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}
