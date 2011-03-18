<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tab_default.ascx.cs"
    Inherits="SistemaRH.tab_default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .style1
    {
        height: 61px;
    }
</style>
<div style="position: inherit; height: 120px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%; font-family: Segoe UI, Arial,Verdana,Helvetica,sans-serif;
                font-size: 85%; height: 120px;" border="0px">
                <tr>
                    <td valign="middle" colspan="2" style="text-align: center; vertical-align: middle;
                        font-family: Segoe UI, Arial,Verdana,Helvetica,sans-serif; font-size: 90%;">
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
                    <td width="50%" align="center">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                            HorizontalAlign="Center" Font-Names="Segoe UI,Arial,Verdana,Helvetica,sans-serif;"
                            CellPadding="0" BackColor="White" CssClass="tableArquivos" 
                            OnRowCommand="GridView1_RowCommand" Width="250px" 
                            DataKeyNames="nome_Arquivo">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl="imagens/anexo.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nome_Arquivo" HeaderText="Arquivos" SortExpression="nome_Arquivo"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="conteudo_Arquivo" DataField="conteudo_Arquivo" SortExpression="conteudo_Arquivo"
                                    Visible="False" />
                                <asp:BoundField DataField="tipo_Arquivo" HeaderText="tipo_Arquivo" SortExpression="tipo_Arquivo"
                                    Visible="False" />
                                <asp:TemplateField HeaderText="Ver" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonVer" runat="server" ImageUrl="imagens/Preview (1).png"
                                            CommandName="Abrir" CommandArgument="<%# Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deletar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonDelete" runat="server" ImageUrl="imagens/Delete (1).png"
                                            CommandName="Excluir" CommandArgument="<%# Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="RetornaArquivos"
                            TypeName="SistemaRH.Adaptador">
                            <SelectParameters>
                                <asp:Parameter Name="lista" Type="Object" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        
                        <br />
                    </td>
                </tr>
                <tr>
                    <td width="50%" class="style1">
                        <div id="divTipoProcesso">
                            <asp:Label ID="LabelTipoProcesso" runat="server" Text="Tipo de processo: " Visible="false"></asp:Label>
                            <asp:DropDownList ID="DropDownListTipoProcesso" runat="server" Visible="false" 
                                Width="190px">
                                <asp:ListItem Text="Licença prêmio" Value="lp" />
                                <asp:ListItem Text="Licença médica" Value="lm" />
                                <asp:ListItem Text="Licença por interesse particular" Value="lip" />
                                <asp:ListItem Text="Avanço" Value="av" />
                                <asp:ListItem Text="Complementação salarial" Value="cs" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td width="50%" >
                    <asp:Table ID="TableArquivo" runat="server" BorderColor="Black" 
                            BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" Visible="False" 
                            Width="250px">
                            <asp:TableRow ID="TableRow2" runat="server" BackColor="White" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="2px">
                                <asp:TableCell ID="TableCell2" runat="server">
                                        <asp:Image ID="ImageAnexo" runat="server" 
                                    ImageUrl="imagens/anexo.png" />
                                    </asp:TableCell>
                                <asp:TableCell ID="TableCell3" runat="server">
                                        <asp:Label ID="LabelArquivo" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                <asp:TableCell ID="TableCell5" runat="server">
                                        <asp:ImageButton ID="ImageButtonDelete" runat="server" 
                                        AlternateText="Deletar" ImageUrl="imagens/Delete (1).png"
                                            OnClick="ImageButtonDelete_Click" />
                                    </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow3" runat="server" BackColor="White" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                <asp:TableCell ID="TableCell6" runat="server">
                                        <asp:Image ID="ImageTrash" runat="server" ImageUrl="~/imagens/trash2.png" 
                                        Height="15px" />
                                    </asp:TableCell>
                                <asp:TableCell ID="TableCell7" runat="server" ColumnSpan="2">
                                        <asp:Label ID="Label3" runat="server" Text="Deletar Página: "></asp:Label>
                                        <asp:TextBox ID="TextBoxDeletePagina" runat="server" Width="22px" 
                                        MaxLength="3" Font-Size="X-Small"></asp:TextBox>
                                    </asp:TableCell>
                                <asp:TableCell ID="TableCell8" runat="server">
                                        <asp:ImageButton ID="ImageButtonDeletePagina" runat="server" ImageUrl="~/imagens/tick_16.png"
                                            OnClick="ImageButtonDeletePagina_Click" 
                                    AlternateText="Deletar Página" />
                                    </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow4" runat="server" BackColor="White" 
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                <asp:TableCell ID="TableCell9" runat="server">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagens/trash2.png" 
                                        Height="15px" />
                                    </asp:TableCell>
                                <asp:TableCell ID="TableCell10" runat="server" ColumnSpan="2">
                                        <asp:Label ID="Label4" runat="server" Text="Deletar Páginas: "></asp:Label>
                                        <asp:TextBox ID="TextBoxDeleteInicio" runat="server" Width="22px" 
                                        MaxLength="3" Font-Size="X-Small"></asp:TextBox>
                                        à
                                        <asp:TextBox ID="TextBoxDeleteFinal" runat="server" Width="22px" 
                                        MaxLength="3" Font-Size="X-Small"></asp:TextBox>
                                    </asp:TableCell>
                                <asp:TableCell ID="TableCell11" runat="server">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/tick_16.png"
                                            OnClick="ImageButtonDeleteIntervalo_Click" 
                                        AlternateText="Deletar Intervalo de Páginas" />
                                    </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
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
