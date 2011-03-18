<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="alterardoc.aspx.cs" Inherits="SistemaRH.alterardoc" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="tab_editar.ascx" tagname="tab_editar" tagprefix="uc1" %>
<%@ Register src="tab_default.ascx" tagname="tab_default" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="estilo.css" rel="stylesheet" type="text/css" />

    <script src="niftycube.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onload=function(){
        Nifty("ul#split h3","top");
        Nifty("ul#split div","bottom");
        }
       
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
 <table align="center" style="height: 80%">
        <tr>
            <td align="center" style="width: 60%;">
                <br />
                <ul id="split">
                    <li id="cadastro" style="top: 30%">
                        <h3 style="width: 750px">
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Editar" Style="font-family: 'Verdana';
                                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                                Font-Size="Medium"></asp:Label></h3>
                        <div>
                            <table id="tableDefault" width="750px" border="0px" cellpadding="0px" align="center"
                                cellspacing="2px">
                                <tr>
                                    <td style="height: 3px" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" rowspan="5">
                                        <asp:ImageButton ID="ImageButtonFoto" runat="server" ImageUrl="~/imagens/TemplateRosto.jpg"
                                            Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelMatricula" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Matricula: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxMatricula" runat="server" BackColor="#FFFFFF" Width="250px"
                                            TabIndex="1" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelNome_Colaborador" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Nome do Colaborador: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextNome_Colaborador" runat="server" BackColor="#FFFFFF" Width="250px"
                                            TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="LabelDocs" runat="server" Text="Documentos:"></asp:Label>
                                        
                                        <br />
                                        
                                        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="3" Width="700px"
                                                    Height="150px" >
                                            <asp:TabPanel runat="server" HeaderText="Pessoais" ID="TabPanelPessoais">
                                                <ContentTemplate>
                                                 <asp:Button ID="btnDisparaUCPessoais" Style="display: none" runat="server" OnClick="btnDisparaUCPessoais_Click"
                                                                UseSubmitBehavior="False" />
                                                    <uc1:tab_editar ID="tab_editar1" runat="server" />
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanelTitulacao" runat="server" HeaderText="Titulações">
                                                <ContentTemplate>
                                                
                                                    <uc1:tab_editar ID="tab_editar2" runat="server" />
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanelPortaria" runat="server" HeaderText="Portarias">
                                                <ContentTemplate>
                                                
                                                    <uc1:tab_editar ID="tab_editar3" runat="server" />
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanelCI" runat="server" HeaderText="CIs">
                                                <HeaderTemplate>
                                                    CIs
                                                </HeaderTemplate>
                                                <ContentTemplate>
                                                
                                                    <uc1:tab_editar ID="tab_editar4" runat="server" />
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanelAviso" runat="server" HeaderText="Aviso de Férias">
                                                <ContentTemplate>
                                                
                                                    <uc1:tab_editar ID="tab_editar5" runat="server" />
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanelRequerimento" runat="server" HeaderText="Requerimentos">
                                                <ContentTemplate>
                                                
                                                    <uc1:tab_editar ID="tab_editar6" runat="server" />
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanelOutros" runat="server" HeaderText="Outros">
                                                <ContentTemplate>
                                                    <uc1:tab_editar ID="tab_editar7" runat="server" />
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        
                                        </asp:TabContainer>
                                        
                                        <br />
                                        
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/imagens/botao_enviar.png" onclick="ImageButton1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </li>
                </ul>
            </td>
        </tr>
    </table>



</asp:Content>
