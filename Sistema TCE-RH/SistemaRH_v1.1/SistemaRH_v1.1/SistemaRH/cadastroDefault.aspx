<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cadastroDefault.aspx.cs"
    Inherits="SistemaRH.CadastroDefault" Title="Sistema RH Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.CornerTop').corner();
        $('.CornerDown').corner();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <br />
    <br />
    <br />
    <center>
        <table border="0" width="350px">
            <tr style="margin: 0px; padding: 0px">
                <td style="width: 50%; margin-right: 3px;">
                    <div id="TopCadDefault" class="CornerDown" data-corner="top 10px">
                    </div>
                    <div id="DownCadDefault" class="CornerDown" data-corner="bottom 10px">
                        <table style="width: 100%; font-family: Verdana; vertical-align: middle;">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/UserIcon.png"
                                        PostBackUrl="~/cadastroUser.aspx" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/cadastroUser.aspx" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">Cadastro de Usuário</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 50%; margin-left: 3px;">
                    <div id="TopCadDefault2" class="CornerDown" data-corner="top 10px">
                    </div>
                    <div id="DownCadDefault2" class="CornerDown" data-corner="bottom 10px">
                        <table style="width: 100%; font-family: Verdana; vertical-align: middle;">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/imagens/Users.png"
                                        PostBackUrl="cadastroDocs.aspx" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/cadastroDocs.aspx" EnableTheming="True" Font-Bold="False" Font-Overline="False" Font-Underline="False" ForeColor="Black">Cadastro de Colaboradores</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
