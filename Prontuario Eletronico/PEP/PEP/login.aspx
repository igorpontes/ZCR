<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true"
    CodeBehind="login.aspx.cs" Inherits="PEP.WebForm1" Title="Sistema de Prontuário Eletrônico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="panelCentral" runat="server" DefaultButton="ImageButtonEnviar">
            <fieldset id="Fieldset2" class="fieldsetLogin">
                <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Acessar</legend>
                 <table width="300px" border="0px" style="" cellpadding="0px" align="center" cellspacing="0px">
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
                            <asp:ImageButton ID="ImageButtonEnviar" runat="server" 
                                ImageUrl="~/imagens/botao_Login.png" onclick="ImageButtonEnviar_Click" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
