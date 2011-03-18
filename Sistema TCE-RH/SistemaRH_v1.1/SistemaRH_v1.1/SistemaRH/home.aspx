<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs"
    Inherits="SistemaRH.Home" Title="Sistema RH Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.CornerTop').corner();
        $('.CornerDown').corner();
    </script>
    <style type="text/css">
        .style1
        {
            text-align: justify;
            vertical-align: top;
        }
        .style2
        {
            height: 54px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <table class="tableHome" width="80%" align="center" style="height: 467px">
        <tr>
            <td style="padding-left: 30px" colspan="2" class="style2">
                <h3 style="font-family: 'Minion Pro Med'; font-size: x-large; font-weight: bold;">
                    Bem-vindo ao Sistema RH Digital</h3>
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 60%;" class="style1">
                &nbsp;&nbsp; Gestão Documental de RH completa para você administrar sua área de
                Recursos Humanos.
                <br />
                O Sistema de RH Digital é uma solução de arquivo, organização e consulta de documentos
                em formato eletrônico onde você pode cadastrar toda a informação de natureza documental
                dos colaboradores e trocá-la entre os utilizadores do sistema.
                <br />
                Sistema RH Digital é um sistema completo que otimiza os processos de gestão documental
                de RH com recursos versáteis e flexíveis.
                <br />
                <br />
            </td>
            <td align="right" style="width: 40%;" valign="top">
                <div id="TopHome" class="CornerDown" data-corner="top 10px">
                </div>
                <div id="DownHome" class="CornerDown" data-corner="bottom 10px">
                    <table width="240px" border="0px" style="padding-right: 6px; padding-left: 6px" cellpadding="0px"
                        align="center" cellspacing="0px">
                        
                        <tr>
                            <td class="cellLabel" width="30%" style="padding-right: 5px; padding-left: 5px">
                                <asp:Label ID="LabelBuscaPorPalavra" runat="server" Font-Bold="True" Font-Size="Smaller">Busca Rápida:</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellLabel">
                                <asp:TextBox ID="TextBoxBuscaPorPalavra" runat="server" Width="202px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cellErrorFieldRequired" valign="middle" align="right" style="padding-right: 5px;
                                padding-left: 5px; text-align: left;">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsuario" runat="server" ControlToValidate="TextBoxBuscaPorPalavra"
                                    ErrorMessage="*Campo obrigatório" Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-top: 7px; padding-bottom: 0px">
                                <asp:ImageButton ID="ImageButtonPesquisar" runat="server" ImageUrl="~/imagens/botao_pequeno_pesquisar.png"
                                    OnClick="ImageButtonPesquisar_Click" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                </li> </ul>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
