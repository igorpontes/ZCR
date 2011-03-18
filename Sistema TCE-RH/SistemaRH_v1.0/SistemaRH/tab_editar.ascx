<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tab_editar.ascx.cs" Inherits="SistemaRH.tab_editar" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<link href="estilo.css" rel="stylesheet" type="text/css" />


<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="False">
</asp:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width: 100%; font-family: Segoe UI, Arial,Verdana,Helvetica,sans-serif;
            font-size: 85%;" border="0px">
            <tr>
                <td width="50%">
                    <asp:Label ID="Label1" runat="server" Text="Incluir Documento:"></asp:Label>
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <br />
                    <br />
                    
                </td>
                <td rowspan="2" width="50%">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                        HorizontalAlign="Center" Font-Names="Segoe UI,Arial,Verdana,Helvetica,sans-serif;"
                        CellPadding="0" BackColor="White" 
                        CssClass="tableArquivos" onrowcommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="imagens/anexo.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nome_Arquivo" HeaderText="Arquivos" SortExpression="nome_Arquivo"
                                ItemStyle-HorizontalAlign="Left">
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
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="RetornaArquivos" TypeName="SistemaRH.Adaptador">
                        <SelectParameters>
                            <asp:Parameter Name="lista" Type="Object" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <asp:ImageButton ID="ImageButtonSalvar" runat="server" 
                        OnClick="ImageButtonSalvar_Click" ImageUrl="~/imagens/botao_salvar.png" 
                        ImageAlign="AbsMiddle" style="text-align: right" />
                    <br />
                    <asp:Label ID="LabelErro" runat="server"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ImageButtonSalvar" />
    </Triggers>
</asp:UpdatePanel>
