<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="alterarSenha.aspx.cs"
    Inherits="SistemaRH.AlteraSenha" Title="Sistema RH Digital" %>

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
    <table align="center" style="height: 80%">
        <tr>
            <td align="center" style="width: 60%;">
                <br />
                <br />
                <br />
                <ul id="split">
                    <li id="alteraSenha" style="top: 30%">
                        <h3 style="width: 308px">
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Alterar Senha" Style="font-family: 'Verdana';
                                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                                Font-Size="Medium"></asp:Label></h3>
                        <div>
                            <table id="tableDefault" width="308px" border="0px" style="" cellpadding="0px" align="center"
                                cellspacing="0px">
                                <tr>
                                    <td align="center" colspan="2" style="height: 1px">
                                        <table>
                                            <tr valign="middle">
                                                <td>
                                                    <asp:Image ID="ImageAttention" runat="server" ImageUrl="~/imagens/attention.gif"
                                                        Width="25px" Visible="False" />
                                                </td>
                                                <td valign="middle">
                                                    <asp:Label ID="LabelErro" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="padding: 8px">
                                        <asp:Label ID="LabelUsuario" runat="server" Font-Bold="True" Style="font-family: 'Verdana';
                                            font-weight: 700; font-size: small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="LabelSenhaAtual" runat="server" Font-Bold="True" Text="Senha Atual: "
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: small"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="TextBoxSenhaAtual" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                            Width="150px" ToolTip="Senha atual"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="right">
                                    <td class="cellErrorFieldRequired">
                                    </td>
                                    <td class="cellErrorFieldRequired">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSenhaAtual" runat="server"
                                            ControlToValidate="TextBoxSenhaAtual" ErrorMessage="*Campo obrigatório" Font-Names="Verdana"
                                            Font-Size="XX-Small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="LabelNovaSenha" runat="server" Font-Bold="True" Text="Nova Senha: "
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: small"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="TextBoxNovaSenha" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                            Width="150px" ToolTip="Nova senha"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="right">
                                    <td class="cellErrorFieldRequired">
                                    </td>
                                    <td class="cellErrorFieldRequired">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNovaSenha" runat="server" ControlToValidate="TextBoxNovaSenha"
                                            ErrorMessage="*Campo obrigatório" Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="LabelConfirmacaoSenha" runat="server" Font-Bold="True" Text="Confirmação de Senha: "
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: small" Width="120px"></asp:Label>
                                    </td>
                                    <td align="right" valign="bottom">
                                        <asp:TextBox ID="TextBoxConfirmacaoSenha" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                            Width="150px" ToolTip="Confirmação de senha"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="right">
                                    <td class="cellErrorFieldRequired">
                                    </td>
                                    <td class="cellErrorFieldRequired">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmacaoSenha" runat="server"
                                            ControlToValidate="TextBoxConfirmacaoSenha" ErrorMessage="*Campo obrigatório"
                                            Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:ImageButton ID="ImageButtonConfirmar" runat="server" ImageUrl="~/imagens/botao_Enviar.png"
                                            ToolTip="Alterar senha do usuário" OnClick="ImageButtonConfirmar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </li>
                </ul>
            </td>
        </tr>
    </table>
</asp:Content>
