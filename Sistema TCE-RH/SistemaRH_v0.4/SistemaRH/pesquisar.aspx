<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="pesquisar.aspx.cs"
    Inherits="SistemaRH.Pesquisar" Title="Sistema RH Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />

    <script src="niftycube.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onload=function(){
        Nifty("ul#split h3","top");
        Nifty("ul#split div","bottom same-height");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <div style="overflow: auto; height: 450px; width: 100%;">
        <table align="center" style="height: 80%">
            <tr>
                <td align="center" style="width: 60%;">
                    <br />
                    <ul id="split">
                        <li id="listar" style="top: 30%">
                            <h3 style="width: 750px">
                                <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Pesquisa" Style="font-family: 'Verdana';
                                    font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                                    Font-Size="Medium"></asp:Label></h3>
                            <div>
                                <table id="tableDefault" width="750px" border="0px" cellpadding="0px" align="center"
                                    cellspacing="2px">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table>
                                                <tr valign="middle">
                                                    <td>
                                                        <asp:Image ID="ImageAttention" runat="server" ImageUrl="~/imagens/attention.gif"
                                                            Width="25px" Visible="False" />
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:Label ID="LabelErro" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                                ForeColor="#4E7C3B" GridLines="None" Height="85%" Width="100%" AllowPaging="True"
                                                OnRowCommand="GridView1_RowCommand" HorizontalAlign="Center" AllowSorting="True"
                                                OnSorting="GridView1_Sorting1" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                Style="margin-top: 46px; margin-left: 0px; width: 100%;" Font-Names="Verdana"
                                                DataKeyNames="id">
                                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" Visible="False" />
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Image ID="ImageUser" runat="server" ImageUrl="~/imagens/UserIcon.png" Height="25px">
                                                            </asp:Image>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="matricula_Colaborador" HeaderText="Matrícula" SortExpression="matricula_Colaborador" />
                                                    <asp:BoundField DataField="cpf_Colaborador" HeaderText="CPF" SortExpression="cpf_Colaborador" />
                                                    <asp:BoundField DataField="nome_Colaborador" HeaderText="Nome" SortExpression="nome_Colaborador" />
                                                    <asp:BoundField DataField="foto" HeaderText="foto" SortExpression="foto" Visible="False" />
                                                    <asp:TemplateField HeaderText="Arquivos">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonArquivo" runat="server" Height="25px" ImageUrl="~/imagens/Folder black mydocuments.png"
                                                                CommandName="Abrir" CommandArgument="<%# Container.DataItemIndex %>" Enabled="False" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonAlterar" runat="server" CommandName="Alterar" Height="25px"
                                                                ImageUrl="~/imagens/Documents white edit (1).png" CommandArgument="<%# Container.DataItemIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonExcluir" runat="server" CommandName="Excluir" Height="25px"
                                                                ImageUrl="~/imagens/Full Trash.png" CommandArgument="<%# Container.DataItemIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#4E7C3B" ForeColor="White" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <table>
                                                        <tr valign="middle">
                                                            <td>
                                                                <asp:Image ID="ImageAttention" runat="server" ImageUrl="~/imagens/attention.gif"
                                                                    Width="25px" />
                                                            </td>
                                                            <td valign="middle">
                                                                <asp:Label ID="LabelGridVazio" runat="server" Text="Tabela de Colaboradores Vazia"
                                                                    ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#4E7C3B" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <asp:ObjectDataSource ID="ObjectDataSourceDocumento" runat="server" DeleteMethod="RemoverDocumento"
                                                InsertMethod="InserirDocumento" SelectMethod="Todos" TypeName="SistemaRH.Adaptador"
                                                UpdateMethod="AtualizarDocumento" DataObjectTypeName="SistemaRH.Documento">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="id" Type="Int32" />
                                                </DeleteParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="documento" Type="Object" />
                                                    <asp:Parameter Name="id" Type="String" />
                                                </UpdateParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </li>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
