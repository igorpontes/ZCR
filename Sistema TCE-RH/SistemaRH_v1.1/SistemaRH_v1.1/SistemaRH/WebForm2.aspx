<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="SistemaRH.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                            HorizontalAlign="Center" Font-Names="Segoe UI,Arial,Verdana,Helvetica,sans-serif;"
                            CellPadding="0" BackColor="White" CssClass="tableArquivos" OnRowCommand="GridView1_RowCommand">
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
    </div>
    </form>
</body>
</html>
