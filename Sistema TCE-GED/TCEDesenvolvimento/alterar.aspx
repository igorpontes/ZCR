<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="alterar.aspx.cs" Inherits="GED_TCESE.WebForm11" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelAlterar" runat="server" Height="350px" Width="100%" Visible="True">
        <fieldset id="Fieldset2" class="fieldsetAlterar">
            <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Alterar</legend>
            <table>
                <tr>
                    <td align="right" colspan="3">
                        <asp:LinkButton ID="LinkButtonVoltar" runat="server" Font-Bold="True" 
                            Font-Underline="False" ForeColor="Black" onclick="LinkButtonVoltar_Click" ToolTip="Voltar para a página inicial">&lt; &lt; Voltar</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelNumeroProcesso" runat="server" Font-Bold="True" 
                            Text="Número do Processo: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNumeroProcesso" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Número do processo"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelAnoProcesso" runat="server" Font-Bold="True" 
                            Text="Ano do Processo: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxAnoProcesso" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Ano do processo"></asp:TextBox>
                    </td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelOrigem" runat="server" Font-Bold="True" Text="Origem: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxOrigem" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Origem do processo"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelAssunto" runat="server" Font-Bold="True" Text="Assunto: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxAssunto" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Assunto do processo"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelDescricao" runat="server" Font-Bold="True" 
                            Text="Descrição: " ToolTip="Descrição do processo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxDescricao" runat="server" BackColor="#F3F3F3" 
                            Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelArquivo" runat="server" Font-Bold="True" Text="Arquivo: "></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server" BackColor="#F3F3F3" 
                            Width="309px" ToolTip="Arquivo anexado" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelPessoa1" runat="server" Font-Bold="True" Text="Pessoa 1: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPessoa1" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Interessado 1"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelPessoa2" runat="server" Font-Bold="True" Text="Pessoa 2: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPessoa2" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Interessado 2"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelPessoa3" runat="server" Font-Bold="True" Text="Pessoa 3: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPessoa3" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Interessado 3"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelPessoa4" runat="server" Font-Bold="True" Text="Pessoa 4: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPessoa4" runat="server" BackColor="#F3F3F3" 
                            Width="300px" ToolTip="Interessado 4"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:ImageButton ID="ImageButtonEnviar" runat="server" 
                            ImageUrl="~/imagens/botao_Enviar.png" onclick="ImageButtonEnviar_Click" ToolTip="Alterar dados" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>