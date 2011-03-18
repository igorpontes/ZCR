<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="loginMaster.aspx.cs"
    Inherits="SistemaRH.WebForm1" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />

    <script src="niftycube.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onload=function(){
        Nifty("ul#split h3","top");
        Nifty("ul#split div","bottom same-height");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
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
                <ul id="split">
                    <li id="one" style="top: 30%">
                        <h3>
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Acessar" Style="font-family: 'Verdana';
                                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                                Font-Size="Medium"></asp:Label></h3>
                        <div>
                            <table width="240px" border="0px" style="" cellpadding="0px" align="center" cellspacing="0px">
                                <tr>
                                    <td style="height: 3px" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" class="cellError">
                                        <asp:Label ID="LabelErro" runat="server" ForeColor="Red" Font-Bold="True" Style="font-family: 'Verdana';
                                            font-weight: 700; font-size: smaller"></asp:Label>
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
                                            ErrorMessage="*Campo obrigatório" Font-Names="Verdana" Font-Size="XX-Small"></asp:RequiredFieldValidator>
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
                                            ErrorMessage="*Campo obrigatório" Font-Names="Verdana" Font-Size="XX-Small"></asp:RequiredFieldValidator>
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
                    </li>
                </ul>
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
</asp:Content>
