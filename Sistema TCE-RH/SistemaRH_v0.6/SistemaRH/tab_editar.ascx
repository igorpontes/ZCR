<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tab_editar.ascx.cs"
    Inherits="SistemaRH.tab_editar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="estilo.css" rel="stylesheet" type="text/css" />
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="width: 100%; font-family: Segoe UI, Arial,Verdana,Helvetica,sans-serif;
	        font-size: 85%;" border="0px" >
	        <tr>
		        <td width="50%">
			        <asp:Label ID="Label1" runat="server" Text="Incluir Documento:"></asp:Label>
			        <br />
			        <asp:FileUpload ID="FileUpload1" runat="server" />
			        <br />
			        <br />
		        </td>
		        <td width="50%">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                        HorizontalAlign="Center" Font-Names="Segoe UI,Arial,Verdana,Helvetica,sans-serif;"
                        CellPadding="0" BackColor="White" 
                        CssClass="tableArquivos" DataSourceID="ObjectDataSource1" 
                        onrowcommand="GridView1_RowCommand1">
                    
                        <Columns>
                            <asp:BoundField DataField="nome_Arquivo" HeaderText="Arquivo" 
                                SortExpression="nome_Arquivo" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="conteudo_Arquivo" HeaderText="conteudo_Arquivo" 
                                SortExpression="conteudo_Arquivo" Visible="false" />
                            <asp:BoundField DataField="tipo_Arquivo" HeaderText="tipo_Arquivo" 
                                SortExpression="tipo_Arquivo" Visible="false" />
                            <asp:TemplateField HeaderText="Visualizar">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonVer" runat="server" 
                                        ImageUrl="~/imagens/Preview1.png" CommandName="Abrir" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excluir">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonExcluir" runat="server" Height="16px" 
                                        ImageUrl="~/imagens/Delete.png" CommandName="Excluir" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
</asp:UpdatePanel>
</div>