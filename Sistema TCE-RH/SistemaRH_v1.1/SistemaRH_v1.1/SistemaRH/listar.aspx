<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="listar.aspx.cs"
    Inherits="SistemaRH.listar" Title="Sistema RH Digital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.CornerTop').corner();
        $('.CornerDown').corner();
        $('.CornerGrid').corner();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <br />
    <center>
        <div id="TopListar" class="CornerTop" data-corner="top 10px">
            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Listar Colaboradores" Style="font-family: 'Verdana';
                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                Font-Size="Medium"></asp:Label>
        </div>
        <div id="DownListar" class="CornerDown" data-corner="bottom 10px">
            <table id="tableDefault" width="750px" border="0px" cellpadding="0px" align="center"
                cellspacing="2px">
                <tr>
                    <td align="center">
                        <table>
                            <tr>
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
                        <div class="CornerGrid" data-corner="10px">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                ForeColor="#4E7C3B" GridLines="None" Height="100%" Width="100%" AllowPaging="True"
                                OnRowCommand="GridView1_RowCommand" HorizontalAlign="Center" AllowSorting="True"
                                OnSorting="GridView1_Sorting1" OnPageIndexChanging="GridView1_PageIndexChanging"
                                Style="margin-left: 0px; width: 100%;" Font-Names="Verdana"
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
                                    <asp:TemplateField HeaderText="Arquivos" Visible="False">
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
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table>
                                        <tr valign="middle">
                                            <td>
                                                <asp:Image ID="ImageAttention" runat="server" ImageUrl="~/imagens/attention.gif"
                                                    Width="25px" />
                                            </td>
                                            <td valign="middle">
                                                <asp:Label ID="LabelGridVazio" runat="server" Text="Tabela de Colaboradores Vazia"
                                                    ForeColor="White"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>
