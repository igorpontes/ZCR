<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="listarprotocolos.aspx.cs" Inherits="GED_TCESE.WebForm9" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" EnableEventValidation="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="overflow: auto;height: 400px; width: 100%;">
    <table width="100%">
        <tr>
            <td align="right">
                <asp:LinkButton ID="LinkButtonVoltar" runat="server" Font-Bold="True" 
                    Font-Underline="False" ForeColor="Black" onclick="LinkButtonVoltar_Click">&lt;&lt;Voltar</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" Height="85%" Width="100%" 
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    onrowcommand="GridView1_RowCommand" CellPadding="4" ForeColor="#4E7C3B" 
                    GridLines="None" ShowFooter="True" DataKeyNames="id" Font-Size="Small"
                    onpageindexchanging="GridView1_PageIndexChanging" onsorting="GridView1_Sorting">
                    <RowStyle HorizontalAlign="Center" BackColor="#E3EAEB" />
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" 
                            Visible="False" />
                        <asp:TemplateField HeaderText="Arquivo" SortExpression="arq_Arquivo">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("arq_Arquivo") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButtonAbrir" runat="server" 
                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="Abrir" 
                                    Height="25px" ImageUrl="~/imagens/botao_Abrir.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="documento1" HeaderText="Num/Ano processo" 
                            SortExpression="documento1" />
                        <asp:BoundField DataField="documento2" HeaderText="Origem" 
                            SortExpression="documento2" />
                        <asp:BoundField DataField="documento3" HeaderText="Tipo" 
                            SortExpression="documento3" />
                        <asp:BoundField DataField="documento4" HeaderText="Ano referência" 
                            SortExpression="documento4" />
                        <asp:BoundField DataField="documento5" HeaderText="Mês referência" 
                            SortExpression="documento5" />
                        <asp:BoundField DataField="documento6" HeaderText="Nome da parte" 
                            SortExpression="documento6" />
                    </Columns>
                    <FooterStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="White"
                        HorizontalAlign="Center" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSourceLBW" runat="server" 
        SelectMethod="Todos" TypeName="GED_TCESE.AdaptadorProtocolo">
    </asp:ObjectDataSource>
</asp:Content>
