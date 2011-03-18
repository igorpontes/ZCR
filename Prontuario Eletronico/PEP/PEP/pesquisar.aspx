<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="pesquisar.aspx.cs" Inherits="PEP.WebForm11" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="ButtonPesquisaComando">
        <div style="overflow: auto;height: 400px; width: 197%; margin-top: 64px;">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="LabelComandoPesquisa" runat="server" Text="Pesquisa: "></asp:Label>
                        <asp:TextBox ID="TextBoxComandoPesquisa" runat="server" Width="350px"></asp:TextBox>
                        <asp:Button ID="ButtonPesquisaComando" runat="server" Text="Pesquisar" 
                            onclick="ButtonPesquisaComando_Click" />
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButtonVoltar" runat="server" Font-Bold="True" 
                            Font-Underline="False" ForeColor="Black" onclick="LinkButtonVoltar_Click" 
                            ToolTip="Voltar para a página inicial">&lt;&lt; 
                        Voltar</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>                    
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CellPadding="3" ForeColor="#4E7C3B" GridLines="None" Height="100%" Width="98%" AllowPaging="True" 
                            onrowcommand="GridView1_RowCommand" HorizontalAlign="Center" AllowSorting="True" 
                            onsorting="GridView1_Sorting1" onpageindexchanging="GridView1_PageIndexChanging" style="margin-top: 46px; margin-left: 0px;" 
                            DataKeyNames="id"><RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" 
                                    Visible="False" />
                                <asp:TemplateField HeaderText="Arquivo" SortExpression="arq_Arquivo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("arq_Arquivo") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonArquivo" runat="server" Height="25px" 
                                            ImageUrl="~/imagens/botao_Abrir.png" CommandName="Abrir" CommandArgument="<%# Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="numero_Registro" HeaderText="Registro" 
                                    SortExpression="numero_Registro" />
                                <asp:BoundField DataField="nome_Paciente" HeaderText="Paciente" 
                                    SortExpression="nome_Paciente" />
                                <asp:BoundField DataField="naturalidade" HeaderText="Naturalidade" 
                                    SortExpression="naturalidade" />
                                <asp:BoundField DataField="data_Nascimento" HeaderText="Nascimento" 
                                    SortExpression="data_Nascimento" DataFormatString="{0:d}" />
                                <asp:TemplateField HeaderText="Sexo" SortExpression="sexo">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("sexo") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("sexo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nome_Pai" HeaderText="Pai" 
                                    SortExpression="nome_Pai" />
                                <asp:BoundField DataField="nome_Mae" HeaderText="Mãe" 
                                    SortExpression="nome_Mae" />
                                <asp:BoundField DataField="profissao" HeaderText="Profissão" 
                                    SortExpression="profissao" />
                                <asp:BoundField DataField="pessoa_Responsavel" HeaderText="Responsável" 
                                    SortExpression="pessoa_Responsavel" />
                                <asp:BoundField DataField="procedencia" HeaderText="Procedência" 
                                    SortExpression="procedencia" />
                                <asp:BoundField DataField="nome_Clinica_Diagnostico" 
                                    HeaderText="Clínica de Diagnóstico" SortExpression="nome_Clinica_Diagnostico" />
                                <asp:BoundField DataField="diagnostico" HeaderText="Diagnóstico" 
                                    SortExpression="diagnostico" />
                                <asp:BoundField DataField="cid" HeaderText="C.I.D." SortExpression="cid" />
                                <asp:BoundField DataField="nome_Clinica_Internacao" 
                                    HeaderText="Clínica de Internação" SortExpression="nome_Clinica_Internacao" />
                                <asp:BoundField DataField="diagnostico_Provisorio" 
                                    HeaderText="Diagnóstico Provisório" SortExpression="diagnostico_Provisorio" />
                                <asp:BoundField DataField="data_Internacao" DataFormatString="{0:d}" 
                                    HeaderText="Internação" SortExpression="data_Internacao" />
                                <asp:BoundField DataField="medico_Solicitante" HeaderText="Médico Solicitante" 
                                    SortExpression="medico_Solicitante" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonAlterar" runat="server" CommandName="Alterar" 
                                            Height="25px" ImageUrl="~/imagens/botao_Alterar.png" CommandArgument="<%# Container.DataItemIndex %>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButtonExcluir" runat="server" CommandName="Excluir" 
                                            Height="25px" ImageUrl="~/imagens/botao_Excluir.png" CommandArgument="<%# Container.DataItemIndex %>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#AD0008" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#ad0008" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#ad0008" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#AD0008" Font-Bold="True" ForeColor="White" 
                                        HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#AD0008" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>                    
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ObjectDataSource ID="ObjectDataSourceProntuario" runat="server" 
                            DeleteMethod="RemoverProntuario" InsertMethod="InserirProntuario" 
                            SelectMethod="Todos" TypeName="PEP.Adaptador" 
                            UpdateMethod="AtualizarProntuario">
                            <DeleteParameters>
                                <asp:Parameter Name="id" Type="Int32" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="prontuario" Type="Object" />
                                <asp:Parameter Name="endereco" Type="Object" />
                                <asp:Parameter Name="telefone" Type="Object" />
                                <asp:Parameter Name="medico" Type="Object" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="prontuario" Type="Object" />
                                <asp:Parameter Name="telefone" Type="Object" />
                                <asp:Parameter Name="medico" Type="Object" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
