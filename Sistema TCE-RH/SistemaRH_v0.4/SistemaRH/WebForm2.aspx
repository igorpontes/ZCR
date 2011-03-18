<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs"
    Inherits="SistemaRH.WebForm2" Title="Sistema RH Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script runat="server">
        void ButtonDeleteAll_Click(object sender, EventArgs e)
        {
            Attachments1.DeleteAllAttachments();

        }

    </script>

    <link href="estilo.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div class="tableArquivos">
        <br />
        <br />
        <br />
        <CuteWebUI:UploadAttachments InsertText="Incluir Arquivos ..." runat="server" ID="Attachments1"
            MultipleFilesUpload="true">
            <InsertButtonStyle />
        </CuteWebUI:UploadAttachments>
        <br />
        <br />
        <asp:Button ID="ButtonDeleteAll" runat="server" Text="Delete All" OnClick="ButtonDeleteAll_Click" />&nbsp;&nbsp;
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            Font-Size="12px" HorizontalAlign="Center" 
            Font-Names="Segoe UI, Arial,Verdana,Helvetica,sans-serif;" CssClass="tableArquivos">
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
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceArquivos" runat="server" SelectMethod="RetornaArquivos"
            TypeName="SistemaRH.Adaptador"></asp:ObjectDataSource>
    </div>
</asp:Content>
