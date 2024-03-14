<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="decision_game_web.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100vh;
            width: 80%; /* Limit the width to 80% */
            margin: 0 auto; /* Center the body */
            font-family: 'Helvetica', sans-serif;
            font-size: 24px;
            
        }
        p {
            font-size: 24px;
            text-align: justify;
            margin-bottom: 2vh;
        }
        #btnStart {
            width: 20%; /* 1/5 of the screen width */
            height: 10vh; /* 1/5 of the viewport height */
            position: absolute;
            bottom: 0;
            left: 50vw; /* Move the left edge of the button to the center of the screen */
            transform: translateX(-50%);
            font-family: 'Helvetica', sans-serif;
            font-size: 28px;
            color: white;
            background-color: royalblue;
            margin-bottom: 2vh;
        }
    </style>
    <script type="text/javascript">
        function checkForm() {
            var age = document.getElementById('<%= age.ClientID %>');
            var sex = document.getElementById('<%= sex.ClientID %>');
            var connectId = document.getElementById('<%= connectId.ClientID %>');
            var btnStart = document.getElementById('<%= btnStart.ClientID %>');

            if (age.value && sex.value && connectId.value) {
                btnStart.disabled = false;
            } else {
                btnStart.disabled = true;
            }
        }

        window.onload = function() {
            checkForm();
            document.getElementById('<%= age.ClientID %>').oninput = checkForm;
            document.getElementById('<%= sex.ClientID %>').onchange = checkForm;
            document.getElementById('<%= connectId.ClientID %>').oninput = checkForm;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>This is a decision-making experiment consisting of 100 rounds. It is expected to take 5-10 minutes. In each round, you will have to choose between three options: A, B, or C (the three options remain the same throughout the game).</p>
            <p>Following each choice, you will be presented with the outcome from the alternative you choose, along with the outcomes you would have gotten had you chosen the other alternatives.</p>
            <p>Your base payment for participating in this experiment is $1. In addition, you can win a bonus of $1. The probability of receiving the bonus increases with the number of points you earn and decreases with the number of points you lose.</p>
            <p>Please note that if you do not complete the whole experiment, you will not receive any payment at all. Should you have any questions, please send an email to: <a href='mailto:nmukhame@campus.haifa.ac.il'>nmukhame@campus.haifa.ac.il</a>. You can leave the experiment now, and it won't hurt your reputation in any way. Clicking on the "Start" button provides your informed consent to proceed.</p>
            <p>If you choose to participate, please fill in the requested personal information below. You can proceed only after all fileds were filled in.</p>
            <label for="age" style="display: inline-block; width: 200px;">Age:</label>
            <asp:TextBox ID="age" runat="server" TextMode="Number" Min="1" Max="120" style="display: inline-block;"></asp:TextBox><br/>
            <label for="sex" style="display: inline-block; width: 200px;">Sex:</label>
            <asp:DropDownList ID="sex" runat="server" style="display: inline-block;">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="male">Male</asp:ListItem>
                <asp:ListItem Value="female">Female</asp:ListItem>
                <asp:ListItem Value="other">Other</asp:ListItem>
            </asp:DropDownList><br/>
            <label for="connectId" style="display: inline-block; width: 200px;">Connect ID:</label>
            <asp:TextBox ID="connectId" runat="server" TextMode="SingleLine" style="display: inline-block;"></asp:TextBox><br/>
            <asp:Button ID="btnStart" runat="server" Text="Start" OnClick="btnStart_Click" />
        </div>
    </form>
</body>
</html>
