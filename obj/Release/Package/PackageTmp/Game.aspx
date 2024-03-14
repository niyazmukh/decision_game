<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="decision_game_web.Game" %>

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
            width: 80vw; /* Limit the width to 80% */
            margin: 0 auto; /* Center the body */
            font-family: 'Helvetica', sans-serif;
        }
        p {
            font-size: 24px;
            text-align: center;
            margin-bottom: 2vh;
        }
        .button-container {
            display: flex;
            justify-content: space-between;
            /*margin-top: 2vh;*/
            margin-bottom: 4vh;
        }
        .game-button {
            width: 20vw;
            height: 20vh;
            font-family: 'Helvetica', sans-serif;
            font-size: 28px;
            color: white;
            background-color: royalblue;
            margin: 0 2vw;
            display: flex; /* Add this line to center the text */
            justify-content: center; /* Add this line to center the text horizontally */
            align-items: center; /* Add this line to center the text vertically */
            text-decoration: none; /* Add this line to remove the underline */
            /*flex-direction: column;*/ /* Add this line to maintain line breaks */
            text-align: center; /* Add this line to center the text */
        }

        .bold-blue {
            font-weight: bold;
            color: royalblue;
        }

        #btnEndGame {
            width: 20vw;
            height: 10vh;
            position: absolute;
            bottom: 0;
            left: 50vw; /* Move the left edge of the button to the center of the screen */
            transform: translateX(-50%); /* Shift the button leftwards by half of its width */
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: center;">
                    <p>Your total score: <b><font color='royalblue'><asp:Label ID="lblUserScore" runat="server"></asp:Label></font></b></p>
                    <p>Current round: <asp:Label ID="lblCurrentRound" runat="server"></asp:Label> out of <asp:Label ID="lblMaxRounds" runat="server"></asp:Label></p>
                </div>
                <div style="margin-top: 4vh; margin-bottom: 4vh; overflow: auto;">
                    <p>Your choice will either add or substract points from your total score.</p>
                    <p>Please, choose one of the three options.</p>
                </div>
                <div class="button-container">
                     <asp:LinkButton ID="btnA" runat="server" OnClick="btnA_Click" CssClass="game-button">
                        A <br/> 8 with 51% chance <br/> -8 with 49% chance
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnB" runat="server" OnClick="btnB_Click" CssClass="game-button">
                        B <br/> 4 with 51% chance <br/> -4 with 49% chance
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnC" runat="server" OnClick="btnC_Click" CssClass="game-button">
                        C <br/> 0 for sure
                    </asp:LinkButton>                
                </div>
            <div style="width: 80vw; height: 30vh; margin: auto; font-family: Helvetica; font-size: 24px; margin-bottom: 4vh; overflow: auto;">
                <div style="text-align: center;">
                    <asp:Label ID="lblHistory" runat="server" Text="Previous rounds' history"></asp:Label>
                </div>
                <asp:GridView ID="gvRounds" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Round" HeaderText="Round">
                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="A" HeaderText="A">
                            <ItemStyle Width="25%" HorizontalAlign="Center" /> 
                        </asp:BoundField>
                        <asp:BoundField DataField="B" HeaderText="B">
                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="C" HeaderText="C">
                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
