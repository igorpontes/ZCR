<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SistemaRH.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistema RH Digital</title>
    <link href="estilo.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.CornerTop').corner();
        $('.CornerDown').corner();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="header">
            <table id="tabelaSuperior" cellspacing="0" cellpadding="0">
                <tr style="padding-bottom: 0px; width: 100%">
                    <td style="padding-left: 10px; padding-top: 3px; padding-bottom: 0px;">
                        <asp:Image ID="logo" runat="server" ImageUrl="~/imagens/logo_rhdigital3.png" Height="46px"
                            Width="220px" />
                    </td>
                    <td align="center" valign="bottom">
                        <div id="tabs">
                        </div>
                    </td>
                    <td align="center" valign="middle">
                        <asp:Image ID="ImageTCE" runat="server" ImageUrl="~/imagens/botao_TCE.png" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="content">
            <br />
            <br />
            <br />
            <table class="tablePrincipal" width="80%" align="center" style="height: 80%">
                <tr>
                    <td style="padding-left: 30px">
                        <h3 style="font-family: 'Minion Pro Med'; font-size: x-large; font-weight: bold;">
                            Bem-vindo ao Sistema RH Digital</h3>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 60%;">
                        <br />
                        <div id="TopLogin" class="CornerTop" data-corner="top 10px">
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Acessar" Style="font-family: 'Verdana';
                                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                                Font-Size="Medium"></asp:Label>
                        </div>
                        <div id="DownLogin" class="CornerDown" data-corner="bottom 10px">
                            <table width="240px" border="0px" style="" cellpadding="0px" align="center" cellspacing="0px">
                                <tr>
                                    <td style="height: 5px" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" class="cellError">
                                        <table>
                                            <tr valign="middle">
                                                <td>
                                                    <asp:Image ID="ImageAttention" runat="server" ImageUrl="~/imagens/attention.gif"
                                                        Width="25px" Visible="False" />
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="LabelErro" runat="server" ForeColor="Red" Font-Bold="True" Style="font-family: 'Verdana';
                                                        font-weight: 700; font-size: smaller"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cellLabel" valign="middle">
                                        <asp:Label ID="LabelUsuario" runat="server" Font-Bold="False" Text="Usuário: " Style="font-family: 'Verdana';
                                            font-weight: 700; font-size: smaller" Font-Names="Verdana" Font-Overline="False"></asp:Label>
                                    </td>
                                    <td class="cellTxtBox">
                                        <asp:TextBox ID="TextBoxUsuario" runat="server" BackColor="#F3F3F3" Width="130px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cellErrorFieldRequired">
                                    </td>
                                    <td class="cellErrorFieldRequired">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsuario" runat="server" ControlToValidate="TextBoxUsuario"
                                            ErrorMessage="*Campo obrigatório" Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cellLabel" valign="middle">
                                        <asp:Label ID="LabelSenha" runat="server" Font-Bold="False" Text="Senha: " Style="font-family: 'Verdana';
                                            font-weight: 700; font-size: smaller" Font-Names="Verdana"></asp:Label>
                                    </td>
                                    <td class="cellTxtBox">
                                        <asp:TextBox ID="TextBoxSenha" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                            Width="130px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cellErrorFieldRequired">
                                    </td>
                                    <td class="cellErrorFieldRequired">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxSenha"
                                            ErrorMessage="*Campo obrigatório" Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right" style="padding-top: 7px; padding-bottom: 0px">
                                        <asp:ImageButton ID="ImageButtonEnviar" runat="server" ImageUrl="~/imagens/botao_enviar.png"
                                            OnClick="ImageButtonEnviar_Click" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td align="right" style="width: 40%;">
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <div id="footer">
            <table id="tabelaInferior" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:Image ID="ImageZDoc" runat="server" ImageUrl="imagens/zcr.png" Height="36px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
