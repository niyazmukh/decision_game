<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndGame.aspx.cs" Inherits="decision_game_web.EndGame" %>

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
        }
        p {
            font-size: 24px;
            text-align: justify;
            margin-bottom: 2vh;
        }
        #btnFinish {
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>The experiment is over.</p>
            <p><asp:Label ID="lblScore" runat="server" Text=""></asp:Label></p>
            <p><asp:Label ID="lblMessage" runat="server"></asp:Label></p>
            <p>Thank you for participating.</p>
            <asp:Button ID="btnFinish" runat="server" Text="End game" OnClick="btnFinish_Click" />
        </div>
    </form>
</body>
</html>
