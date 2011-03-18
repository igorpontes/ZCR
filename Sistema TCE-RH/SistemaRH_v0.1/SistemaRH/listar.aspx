<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="listar.aspx.cs"
    Inherits="SistemaRH.listar" Title="Listar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div style="overflow: auto; height: 400px; width: 100%;">
        <table width="100%">
            <tr>
                <td align="right" style="margin-left: 40px">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                        ForeColor="#4E7C3B" GridLines="None" Height="85%" Width="98%" AllowPaging="True"
                        OnRowCommand="GridView1_RowCommand" HorizontalAlign="Center" AllowSorting="True"
                        OnSorting="GridView1_Sorting1" OnPageIndexChanging="GridView1_PageIndexChanging"
                        Style="margin-top: 46px; margin-left: 0px;" DataKeyNames="id">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" Visible="False" />
                            <asp:TemplateField HeaderText="Arquivo" SortExpression="arq_Arquivo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("arq_Arquivo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonArquivo" runat="server" Height="25px" ImageUrl="~/imagens/botao_Abrir.png"
                                        CommandName="Abrir" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nome_Colaborador" HeaderText="Colaborador" SortExpression="nome_Colaborador" />
                            <asp:BoundField DataField="naturalidade" HeaderText="Naturalidade" SortExpression="naturalidade" />
                            <asp:BoundField DataField="data_Nascimento" HeaderText="Nascimento" SortExpression="data_Nascimento"
                                DataFormatString="{0:d}" />
                            <asp:TemplateField HeaderText="Sexo" SortExpression="sexo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("sexo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("sexo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="nome_Pai" HeaderText="Pai" SortExpression="nome_Pai" />
                            <asp:BoundField DataField="nome_Mae" HeaderText="Mãe" SortExpression="nome_Mae" />
                            <asp:BoundField DataField="cargo" HeaderText="Cargo" SortExpression="cargo" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonAlterar" runat="server" CommandName="Alterar" Height="25px"
                                        ImageUrl="~/imagens/botao_Alterar.png" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonExcluir" runat="server" CommandName="Excluir" Height="25px"
                                        ImageUrl="~/imagens/botao_Excluir.png" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#AD0008" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#ad0008" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#ad0008" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1a6988" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <EditRowStyle BackColor="#AD0008" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourcePessoa" runat="server" DeleteMethod="RemoverPessoa"
                        InsertMethod="InserirPessoa" SelectMethod="Todos" TypeName="SistemaRH.Adaptador"
                        UpdateMethod="AtualizarPessoa">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="pessoa" Type="Object" />
                            <asp:Parameter Name="endereco" Type="Object" />
                            <asp:Parameter Name="telefone" Type="Object" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="pessoa" Type="Object" />
                            <asp:Parameter Name="telefone" Type="Object" />
                        </InsertParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
