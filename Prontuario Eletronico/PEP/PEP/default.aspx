<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PEP.WebForm7" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 370px; margin-top: 68px">
        <asp:Panel ID="PanelProntuario" runat="server" HorizontalAlign="Center" Height="350px" Width="100%" Visible="True" DefaultButton="ImageButtonEnviar">
            <fieldset id="Fieldset2" class="fieldsetDefault">
                <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Prontuário</legend>
                <table id="TabelaProntuario" title="Prontuário" class="tabelaTiposPainel" style="vertical-align: bottom;
	                padding-right: 5px;	padding-left: 5px;	padding-top: 10px; text-align: justify;">
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
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
                            <asp:TextBox ID="TextBoxBuscaPorPalavra" runat="server" Width="250px"/>
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
                            <asp:ImageButton ID="ImageButtonEnviar" runat="server" 
                                ImageUrl="~/imagens/botao_Enviar.png" onclick="ImageButtonEnviar_Click" />
                        </td>
                        <td>
                            &nbsp;
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
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
