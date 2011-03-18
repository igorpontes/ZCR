<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cadastroUser.aspx.cs"
    Inherits="SistemaRH.CadastroUser" Title="Sistema RH Digital" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="tab_default.ascx" TagName="tab_default" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.CornerTop').corner();
        $('.CornerDown').corner();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <br />
    <center>
        <div id="TopCadUser" class="CornerTop" data-corner="top 10px">
            <asp:Label ID="Label2" runat="server" Text="&nbsp;&nbsp;Cadastro de Usuários" Style="font-family: 'Verdana';
                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                Font-Size="Medium"></asp:Label>
        </div>
        <div id="DownCadUser" class="CornerDown" data-corner="bottom 10px">
            <table id="tableDefault" width="400px" border="0px" cellpadding="0px" align="center"
                cellspacing="2px">
                <tr>
                    <td colspan="2" align="center">
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
                    <td valign="middle">
                        <asp:Label ID="LabeMatricula" runat="server" Font-Bold="False" Font-Names="Verdana"
                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                            Text="Matrícula:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxMatricula" runat="server" BackColor="#FFFFFF" Width="230px"
                            TabIndex="1"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidatorMatricula" runat="server" ErrorMessage="*Matrícula não existe!"
                            Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic" ControlToValidate="TextBoxMatricula"
                            OnServerValidate="CustomValidatorMatricula_ServerValidate"></asp:CustomValidator>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <asp:Label ID="LabelLogin" runat="server" Font-Bold="False" Font-Names="Verdana"
                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                            Text="Login: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxLogin" runat="server" BackColor="#FFFFFF" Width="230px" TabIndex="1"></asp:TextBox>
                        &nbsp;<br />
                        <asp:CustomValidator ID="CustomValidatorLogin" runat="server" ErrorMessage="*Login já existe!"
                            ControlToValidate="TextBoxLogin" Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"
                            OnServerValidate="CustomValidatorLogin_ServerValidate"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <asp:Label ID="LabelSenha" runat="server" Font-Bold="False" Font-Names="Verdana"
                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                            Text="Senha: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextSenha" runat="server" BackColor="#FFFFFF" Width="230px" TabIndex="2"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <asp:Label ID="LabelConfirmaSenha" runat="server" Font-Bold="False" Font-Names="Verdana"
                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                            Text="Confirma Senha: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextConfirmaSenha" runat="server" BackColor="#FFFFFF" Width="230px"
                            TabIndex="2" TextMode="Password"></asp:TextBox>
                        <br />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextSenha"
                            ControlToValidate="TextConfirmaSenha" Display="Dynamic" ErrorMessage="*Confirmação de Senha diferente!"
                            Font-Names="Verdana" Font-Size="XX-Small"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top: 5px">
                        <table align="center" bgcolor="White">
                            <tr>
                                <td bgcolor="#E1E1E1">
                                    <asp:Label ID="LabelDocs" runat="server" Text="Documentos"></asp:Label>
                                </td>
                                <td bgcolor="#E1E1E1">
                                    <asp:Label ID="LabelLeitura" runat="server" Text="Leitura"></asp:Label>
                                </td>
                                <td bgcolor="#E1E1E1">
                                    <asp:Label ID="LabelAlteracao" runat="server" Text="Alteração"></asp:Label>
                                </td>
                                <td bgcolor="#E1E1E1">
                                    <asp:Label ID="LabelNenhum" runat="server" Text="Nenhum"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    Pessoais
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraPessoais" runat="server" Enabled="true" GroupName="pessoais" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoPessoais" runat="server" GroupName="pessoais" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumPessoais" runat="server" GroupName="pessoais" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    Titulações
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraTitulacao" runat="server" GroupName="titulacao" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoTitulacao" runat="server" GroupName="titulacao" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumTitulacao" runat="server" GroupName="titulacao" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    Portarias
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraPortaria1" runat="server" GroupName="portaria" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoPortaria" runat="server" GroupName="portaria" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumPortaria" runat="server" GroupName="portaria" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    Portarias c/ Processo
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraPortariaProcesso" runat="server" GroupName="portaria_processo" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoPortariaProcesso" runat="server" GroupName="portaria_processo" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumPortariaProcesso" runat="server" GroupName="portaria_processo"
                                        Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    CIs
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraCI" runat="server" GroupName="ci" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoCI" runat="server" GroupName="ci" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumCI" runat="server" GroupName="ci" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    Aviso de Férias
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraAviso" runat="server" GroupName="aviso" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoAviso" runat="server" GroupName="aviso" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumAviso" runat="server" GroupName="aviso" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    Requerimentos
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraRequerimento" runat="server" GroupName="requerimento" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoRequerimento" runat="server" GroupName="requerimento" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumRequerimento" runat="server" GroupName="requerimento"
                                        Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller">
                                    Outros
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="LeituraOutros" runat="server" GroupName="outros" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="AlteracaoOutros" runat="server" GroupName="outros" />
                                </td>
                                <td style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" class="style1">
                                    <asp:RadioButton ID="NenhumOutros" runat="server" GroupName="outros" Checked="True" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:ImageButton ID="ImageButtonEnviar" runat="server" ImageUrl="~/imagens/botao_enviar.png"
                            OnClick="ImageButtonEnviar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>
