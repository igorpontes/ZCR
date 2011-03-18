<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tab_default.ascx.cs"
    Inherits="SistemaRH.tab_default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="estilo.css" rel="stylesheet" type="text/css" />
<div style="position: inherit; height: 120px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; font-family: Segoe UI, Arial,Verdana,Helvetica,sans-serif;
                font-size: 85%; height: 120px;" border="0px">
                <tr>
                    <td valign="middle" colspan="2" style="text-align: center; vertical-align: middle; font-family: Segoe UI, Arial,Verdana,Helvetica,sans-serif;
                font-size: 90%;">
                        <table style="text-align: center;">
                            <tr style="text-align: center; vertical-align: middle;">
                                <td>
                                    <asp:Image ID="ImageAttention" runat="server" ImageUrl="~/imagens/attention.gif"
                                        Width="20px" Visible="False" />
                                </td>
                                <td>
                                    <asp:Label ID="LabelErro" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="50%">
                        <asp:Label ID="Label1" runat="server" Text="Incluir Documento:"></asp:Label>
                        <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                    <td rowspan="2" width="50%" align="center">
                        <div id="arquivos">
                            <asp:Table ID="TableArquivo" runat="server" HorizontalAlign="Center" Visible="False">
                                <asp:TableRow runat="server">
                                    <asp:TableCell runat="server" BackColor="#E1E1E1" ColumnSpan="4" ForeColor="White"
                                        HorizontalAlign="Center">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Arquivo" Font-Bold="True"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px">
                                    <asp:TableCell runat="server">
                                        <asp:Image ID="ImageAnexo" runat="server" ImageUrl="imagens/anexo.png" />
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:Label ID="LabelArquivo" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:ImageButton ID="ImageButtonVer" runat="server" AlternateText="Visualizar" ImageUrl="imagens/Preview (1).png"
                                            OnClick="ImageButtonVer_Click" Style="text-align: center" />
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:ImageButton ID="ImageButtonDelete" runat="server" AlternateText="Deletar" ImageUrl="imagens/Delete (1).png"
                                            OnClick="ImageButtonDelete_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" BackColor="White" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px">
                                    <asp:TableCell runat="server">
                                        <asp:Image ID="ImageTrash" runat="server" ImageUrl="~/imagens/trash2.png" Height="15px" />
                                    </asp:TableCell>
                                    <asp:TableCell runat="server" ColumnSpan="2">
                                    <asp:Label ID="Label3" runat="server" Text="Deletar Página: "></asp:Label>
                                    <asp:TextBox ID="TextBoxDeletePagina" runat="server" Width="22px" MaxLength="3" Font-Size="X-Small"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server">
                                        <asp:ImageButton ID="ImageButtonDeletePagina" runat="server" ImageUrl="~/imagens/tick_16.png"
                                            OnClick="ImageButtonDeletePagina_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                        <div id="deletePagina">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td width="50%">
                        <asp:ImageButton ID="ImageButtonSalvar" runat="server" OnClientClick="return confirm('Você tem certeza que quer adicionar o arquivo?');"
                            OnClick="ImageButtonSalvar_Click" ImageUrl="~/imagens/botao_salvar.png" ImageAlign="AbsMiddle"
                            Style="text-align: right" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageButtonSalvar" />
        </Triggers>
    </asp:UpdatePanel>
</div>
