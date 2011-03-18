<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="buscaavancada.aspx.cs"
    Inherits="GED_TCESE.WebForm3" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelCentral" runat="server" Width="934px" Height="389px" DefaultButton="ImageButtonPesquisar">
        <center>
            <fieldset id="Fieldset1" class="fieldsetBuscaAvancada">
                <legend style="font-family: 'Verdana'; font-weight: 700; font-size: large">Busca Avançada</legend>
                <table border="0px" style="width: 643px">
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButtonVoltar" runat="server" Font-Bold="True" Font-Underline="False"
                                ForeColor="Black" OnClick="LinkButtonVoltar_Click" ToolTip="Voltar para a página inicial">&lt;&lt; 
                                Voltar</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="LabelBuscaQualquerPalavra" runat="server" Font-Bold="True" Text="Busca por qualquer palavra: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxBuscaQualquerPalavra" runat="server" Width="350px" ToolTip="Busca por qualquer palavra informada"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="LabelBuscaComExpressao" runat="server" Font-Bold="True" Text="Busca com a expressão: "
                                ToolTip="Busca da expressão informada">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxBuscaComExpressao" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:ImageButton ID="ImageButtonPesquisar" runat="server" ImageUrl="~/imagens/botao_Pesquisar.png"
                                OnClick="ImageButtonPesquisar_Click" ToolTip="Pesquisar resultados" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </asp:Panel>
</asp:Content>