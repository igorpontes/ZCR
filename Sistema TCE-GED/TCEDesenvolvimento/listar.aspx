<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="listar.aspx.cs" 
Inherits="GED_TCESE.WebForm7" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" EnableEventValidation="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow: auto;height: 400px; width: 100%;">
        <table width="100%">
            <tr>
                <td align="right" class="style1">
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
                        CellPadding="4" ForeColor="#4E7C3B" 
                        GridLines="None" Height="85%" Width="100%" AllowPaging="True" 
                        OnRowCommand="GridView1_RowCommand" DataKeyNames="id" 
                        HorizontalAlign="Center" AllowSorting="True" OnSorting="GridView1_Sorting1" 
                        onpageindexchanging="GridView1_PageIndexChanging" Font-Size="Small">
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" 
                                Visible="False" />
                            <asp:TemplateField HeaderText="Processo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("arq_Arquivo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonAbrir" runat="server" CommandName="Abrir" 
                                        Height="25px" ImageUrl="~/imagens/botao_Abrir.png" ToolTip="Processo" 
                                        CommandArgument="<%# Container.DataItemIndex %>" 
                                        PostBackUrl="~/listar.aspx" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Decisão" >
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxDecisao" runat="server" Text='<%# Bind("arq_Arquivo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonAbrirDecisao" runat="server" CommandName="AbrirDecisao" 
                                        Height="25px" ImageUrl="~/imagens/botao_Abrir.png" ToolTip="Decisão" 
                                        CommandArgument="<%# Container.DataItemIndex %>" 
                                        PostBackUrl="~/listar.aspx"/>
                                    <%--<%
                                        GED_TCESE.Adaptador apt = new GED_TCESE.Adaptador();
                                        System.Collections.Generic.List<GED_TCESE.Processo> lista = new System.Collections.Generic.List<GED_TCESE.Processo>();
                                        lista = apt.Todos();
                                        ImageButton ibAbrirDecisao = (ImageButton)Page.FindControl("ImageButtonAbrirDecisao");
                                        for (int i = 0; i < lista.Count; i++)
                                        {
                                            if (lista[i].decisao == "")
                                            {
                                                ibAbrirDecisao.ImageUrl = "~\\imagens\\botao_Abrir.png";
                                            }
                                            else
                                            {
                                                ibAbrirDecisao.ImageUrl = "~\\imagens\\botao_Excluir.png";
                                            }
                                        }
                                        GridView1.DataBind();
                                    %>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="numero_Processo" HeaderText="Número do processo" 
                                SortExpression="numero_Processo" />
                            <asp:BoundField DataField="ano_Processo" HeaderText="Ano do processo" 
                                SortExpression="ano_Processo" />
                            <asp:BoundField DataField="origem" HeaderText="Origem do processo" 
                                SortExpression="origem" />
                            <asp:BoundField DataField="assunto" HeaderText="Assunto do processo" 
                                SortExpression="assunto" />
                            <asp:BoundField DataField="descricao" HeaderText="Descrição do processo" 
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
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonAlterar" runat="server" CommandName="Alterar" 
                                        Height="25px" ImageUrl="~/imagens/botao_Alterar.png" 
                                        ToolTip="Alterar Arquivo" 
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonExcluir" runat="server" CommandName="Excluir" 
                                        Height="25px" ImageUrl="~/imagens/botao_Excluir.png" 
                                        ToolTip="Excluir Arquivo" 
                                        CommandArgument="<%# Container.DataItemIndex %>" Visible="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
        SelectMethod="Todos" TypeName="GED_TCESE.Adaptador"></asp:ObjectDataSource>
</asp:Content>
