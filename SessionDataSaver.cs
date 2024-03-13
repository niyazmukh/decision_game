using decision_game_web;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace decision_game_web
{
    public class SessionDataSaver
    {
        public static void SaveSessionData(HttpSessionState Session, HttpServerUtility Server)
        {
            // Get the session data
            var sessionId = Session.SessionID;
            var gameId = Session["GameId"]; // Retrieve the GUID from the session
            var age = Session["Age"];
            var sex = Session["Sex"];
            var connectId = Session["ConnectId"];
            var scores = Session["Scores"] as List<int>;
            var rounds = Session["Rounds"] as List<RoundData>;
            var csv = new StringBuilder();
            var roundTimes = Session["RoundTimes"] as List<double>;

            // Reverse the order of the rounds list
            rounds.Reverse();

            // Add a header row (only if the file doesn't exist yet)
            if (!File.Exists(Server.MapPath("~/Data.csv")))
            {
                csv.AppendLine("SessionId,GameId,Age,Sex,ConnectId,UserScore,Round,RoundTime,A,B,C,ClickedButton,PointsEarned");
            }

            // Add a row for each round
            for (int i = 0; i < rounds.Count; i++)
            {
                var round = rounds[i];
                var score = scores[i];
                var roundTime = roundTimes[i];
                csv.AppendLine($"{sessionId},{gameId},{age},{sex},{connectId},{score},{round.Round},{roundTime},{round.A},{round.B},{round.C},{round.ClickedButton},{round.PointsEarned}");
            }


            // Write the CSV data to a file
            File.AppendAllText(Server.MapPath("~/Data.csv"), csv.ToString());
        }
    }
}