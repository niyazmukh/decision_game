using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace decision_game_web
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnStart_Click(object sender, EventArgs e)
        {
            // Store the user's age, sex, and connect ID in the Session state
            Session["Age"] = age.Text;
            Session["Sex"] = sex.SelectedValue;
            Session["ConnectId"] = connectId.Text;

            Response.Redirect("Game.aspx");
        }

    }
}