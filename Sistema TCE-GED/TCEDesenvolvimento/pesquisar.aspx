<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="pesquisar.aspx.cs" Inherits="GED_TCESE.WebForm4" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" EnableEventValidation="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" DefaultButton="ButtonPesquisaComando">
    <div style="overflow: auto;height: 400px; width: 100%;">
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="LabelComandoPesquisa" runat="server" Text="Pesquisa: "></asp:Label>
                    <asp:TextBox ID="TextBoxComandoPesquisa" runat="server" Width="350px"></asp:TextBox>
                    <asp:Button ID="ButtonPesquisaComando" runat="server" Text="Pesquisar" 
                        onclick="ButtonPesquisaComando_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:LinkButton ID="LinkButtonVoltar" runat="server" Font-Bold="True" 
                        Font-Underline="False" ForeColor="Black" onclick="LinkButtonVoltar_Click">&lt; &lt; Voltar</asp:LinkButton>
                    </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        Height="85%" Width="100%" AllowPaging="True" CellPadding="4" ForeColor="#4E7C3B" 
                        GridLines="None" ShowFooter="True" onrowcommand="GridView1_RowCommand1" 
                        DataKeyNames="id" onpageindexchanging="GridView1_PageIndexChanging" 
                        onsorting="GridView1_Sorting" AllowSorting="True" Font-Size="Small">
                        <RowStyle BackColor="#E3EAEB" />
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
                                        Height="25px" ImageUrl="~/imagens/botao_Abrir.png" 
                                        PostBackUrl="~/pesquisar.aspx" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Decisão" SortExpression="Abrir Decisão">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                        CommandArgument="<%# Container.DataItemIndex %>" CommandName="AbrirDecisao" 
                                        Height="25px" ImageUrl="~/imagens/botao_Abrir.png" sssss/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="numero_Processo" HeaderText="Número processo" 
                                SortExpression="numero_Processo" />
                            <asp:BoundField DataField="ano_Processo" HeaderText="Ano processo" 
                                SortExpression="ano_Processo" />
                            <asp:BoundField DataField="origem" HeaderText="Origem processo" 
                                SortExpression="origem" />
                            <asp:BoundField DataField="assunto" HeaderText="Assunto processo" 
                                SortExpression="assunto" />
                            <asp:BoundField DataField="descricao" HeaderText="Descrição processo" 
                                SortExpression="descricao" />
                            <asp:BoundField DataField="qtdPessoas" HeaderText="qtdPessoas" 
                                SortExpression="qtdPessoas" Visible="False" />
                            <asp:BoundField DataField="pessoa1" HeaderText="Interessado 1" 
                                SortExpression="pessoa1" />
                            <asp:BoundField DataField="pessoa2" HeaderText="Interessado 2" 
                                SortExpression="pessoa2" />
                            <asp:BoundField DataField="pessoa3" HeaderText="Interessado 3" 
                                SortExpression="pessoa3" />
                            <asp:BoundField DataField="pessoa4" HeaderText="Interessado 4" 
                                SortExpression="pessoa4" />
                        </Columns>
                        <FooterStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSourceLBW" runat="server" 
                        SelectMethod="PesquisarCampos" TypeName="GED_TCESE.Adaptador">
                        <SelectParameters>
                            <asp:SessionParameter Name="comando" SessionField="lista" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>
</asp:Content>
