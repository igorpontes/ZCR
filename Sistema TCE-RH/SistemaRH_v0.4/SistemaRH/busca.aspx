<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="busca.aspx.cs"
    Inherits="SistemaRH.Busca" Title="Sistema RH Digital" %>

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
                    <li id="pesquisar" style="top: 30%">
                        <h3 style="width: 400px">
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Pesquisar" Style="font-family: 'Verdana';
                                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                                Font-Size="Medium"></asp:Label></h3>
                        <div>
                            <table id="tableDefault" width="400px" border="0px" style="" cellpadding="0px" align="center"
                                cellspacing="0px">
                                <tr>
                                    <td align="center" colspan="2">
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
                                        <br />
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
                                    <td width="30%">
                                        <asp:Label ID="LabelBuscaPorPalavra" runat="server" Font-Bold="true">Busca por Palavra:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxBuscaPorPalavra" runat="server" Width="250px" />
                                    </td>
                                </tr>
                                <tr style="padding: 0px; margin: 0px; height: 0px;">
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%">
                                        <asp:Label ID="LabelBuscaPorExpressao" runat="server" Font-Bold="true">Busca por Expressão:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxBuscaPorExpressao" runat="server" Width="250px" />
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
                                        <asp:ImageButton ID="ImageButtonEnviar" runat="server" ImageUrl="~/imagens/botao_Enviar.png"
                                            OnClick="ImageButtonEnviar_Click" />
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
                            </table>
                        </div>
                    </li>
                </ul>
            </td>
        </tr>
    </table>
</asp:Content>
