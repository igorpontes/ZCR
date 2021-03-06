﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tab_default.ascx.cs"
    Inherits="SistemaRH.tab_default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="estilo.css" rel="stylesheet" type="text/css" />
<div style="">
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <table style="width: 100%; font-family: Segoe UI, Arial,Verdana,Helvetica,sans-serif;
            font-size: 85%;" border="1px">
            <tr>
                <td width="50%">
                    <asp:Label ID="Label1" runat="server" Text="Incluir Documento:"></asp:Label>
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <br />
                    <br />
                    
                </td>
                <td rowspan="2" width="50%">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Font-Size="12px"
                        HorizontalAlign="Center" Font-Names="Segoe UI,Arial,Verdana,Helvetica,sans-serif;"
                        CellPadding="0" BackColor="White" 
                        CssClass="tableArquivos">
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
                    <asp:ImageButton ID="ImageButtonSalvar" runat="server" OnClientClick="return confirm('Você tem certeza que quer adicionar o arquivo?');"
                        OnClick="ImageButtonSalvar_Click" ImageUrl="~/imagens/botao_salvar.png" 
                        ImageAlign="AbsMiddle" style="text-align: right" />
                    <br />
                    <asp:Label ID="LabelErro" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="LabelArquivo" runat="server" Text=""></asp:Label>
                    &nbsp;<asp:ImageButton ID="ImageButtonVer" runat="server" ImageUrl="imagens/Preview (1).png"
                        OnClick="ImageButtonVer_Click" AlternateText="Visualizar" />
                    &nbsp;<asp:ImageButton ID="ImageButtonDelete" runat="server" ImageUrl="imagens/Delete (1).png"
                        OnClick="ImageButtonDelete_Click" AlternateText="Deletar" />
                    <br />
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ImageButtonSalvar" />
    </Triggers>
</asp:UpdatePanel>
</div>
