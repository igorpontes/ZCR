<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs"
    Inherits="GED_TCESE.WebForm1" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelCentral" runat="server" Width="934px" Height="389px">
        <center>
            <fieldset id="Fieldset1" class="fieldsetLogin">
                <legend style="font-family: 'Verdana'; font-weight: 700; font-size: large">Login</legend>
                <table width="240px" border="0px">
                    <tr>
                        <td style="height:3px" colspan="2">
                          
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" class="cellError">
                            <asp:Label ID="LabelErro" runat="server" ForeColor="Red" Font-Bold="True" Style="font-family: 'Verdana';
                                font-weight: 700; font-size: smaller"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="cellLabel">
                            <asp:Label ID="LabelUsuario" runat="server" Font-Bold="False" Text="Usuário: " Style="font-family: 'Verdana';
                                font-weight: 700; font-size: smaller" Font-Names="Verdana" Font-Overline="False"></asp:Label>
                        </td>
                        <td class="cellTxtBox">
                            <asp:TextBox ID="TextBoxUsuario" runat="server" BackColor="#F3F3F3" 
                                Width="130px" ToolTip="Nome do usuário"></asp:TextBox>
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
                        <td class="cellLabel">
                            <asp:Label ID="LabelSenha" runat="server" Font-Bold="False" Text="Senha: " Style="font-family: 'Verdana';
                                font-weight: 700; font-size: smaller" Font-Names="Verdana"></asp:Label>
                        </td>
                        <td class="cellTxtBox">
                            <asp:TextBox ID="TextBoxSenha" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                Width="130px" ToolTip="Senha do usuário"></asp:TextBox>
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
                            <asp:ImageButton ID="ImageButtonEnviar" runat="server" ImageUrl="~/imagens/botao_Enviar.png"
                                OnClick="ImageButtonEnviar_Click" ToolTip="Efetuar login do usuário"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </asp:Panel>
</asp:Content>
