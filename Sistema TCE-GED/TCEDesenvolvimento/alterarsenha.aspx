<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="alterarsenha.aspx.cs" Inherits="GED_TCESE.WebForm12" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div <%--style="height: 356px; width: 936px"--%> class="tabelaTipos">
        <asp:Panel ID="panelCentral" runat="server" Width="934px" Height="400px">
            <center style="height: 253px">
                <fieldset id="Fieldset1" class="fieldsetAlterarSenha">
                    <legend style="font-family: 'Verdana'; font-weight: 700; font-size: large">Alterar Senha</legend>
                        <table class="tabelaAlterarSenha">
                            <tr>

                                <td align="right" colspan="2">
                                    <asp:LinkButton ID="LinkButtonVoltar" runat="server" Font-Bold="True" Font-Underline="False"
                                        ForeColor="Black" OnClick="LinkButtonVoltar_Click" ToolTip="Voltar para a página inicial">&lt; &lt; Voltar</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="padding: 8px">
                                    <asp:Label ID="LabelUsuario" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="LabelSenhaAtual" runat="server" Font-Bold="True" Text="Senha Atual: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxSenhaAtual" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                        Width="150px" AutoPostBack="True" ToolTip="Senha atual"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="LabelErroSenhaAtual" runat="server" Font-Size="8pt" 
                                        ForeColor="Red" Text="* informar senha atual" Visible="False"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="LabelNovaSenha" runat="server" Font-Bold="True" Text="Nova Senha: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxNovaSenha" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                        Width="150px" AutoPostBack="True" ToolTip="Nova senha"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="LabelErroNovaSenha" runat="server" Font-Size="8pt" 
                                        ForeColor="Red" Text="* informar nova senha" Visible="False"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="LabelConfirmacaoSenha" runat="server" Font-Bold="True" 
                                        Text="Confirmação de Senha: " Width="181px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxConfirmacaoSenha" runat="server" BackColor="#F3F3F3" TextMode="Password"
                                        Width="150px" AutoPostBack="True" ToolTip="Confirmação de senha"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="LabelErroConfirmacaoSenha" runat="server" Font-Size="8pt" 
                                        ForeColor="Red" Text="* informar confirmação de senha" Visible="False"></asp:Label>
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
                                        OnClick="ImageButtonConfirmar_Click" ToolTip="Alterar senha do usuário" />
                                </td>
                              
                            </tr>
                        </table>
                </fieldset>
            </center>
        </asp:Panel>
    </div>
</asp:Content>
