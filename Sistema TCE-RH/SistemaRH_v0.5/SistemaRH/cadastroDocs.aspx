<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cadastroDocs.aspx.cs"
    Inherits="SistemaRH.CadastroDocs" Title="Sistema RH - Cadastro Documentos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="tab_default.ascx" TagName="tab_default" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />

    <script src="niftycube.js" type="text/javascript"></script>

    <script type="text/javascript">
        window.onload=function(){
        Nifty("ul#split h3","top");
        Nifty("ul#split div","bottom");
        }
      
        var indexTab = 0;
        function getTab(sender, args) {
            indexTab = sender.get_activeTabIndex();
            
            if (indexTab == 0) {
                document.getElementById('<%= btnDisparaUCPessoais.ClientID %>').click();
            }else if (indexTab == 1) {
                document.getElementById('<%= btnDisparaUCTitulacao.ClientID %>').click();
            } else if (indexTab == 2) {
                document.getElementById('<%= btnDisparaUCPortaria.ClientID %>').click();
            } else if (indexTab == 3) {
                document.getElementById('<%= btnDisparaUCCI.ClientID %>').click();
            } else if (indexTab == 4) {
                document.getElementById('<%= btnDisparaUCAviso.ClientID %>').click();
            } else if (indexTab == 5) {
                document.getElementById('<%= btnDisparaUCRequerimento.ClientID %>').click();
            } else if (indexTab == 6) {
                document.getElementById('<%= btnDisparaUCOutros.ClientID %>').click();
            }
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
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Cadastrar" Style="font-family: 'Verdana';
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
                                            TabIndex="1"></asp:TextBox>
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
                                        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" >
                                        </asp:ToolkitScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanelTabContainer" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />--%>
                                                <%--<asp:PostBackTrigger ControlID="btnPreview" />--%>
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="700px"
                                                    Height="150px" AutoPostBack="false" OnClientActiveTabChanged="getTab" 
                                                    OnActiveTabChanged="TabContainer1_ActiveTabChanged" oninit="TabContainer1_Init" 
                                                    onload="TabContainer1_Load">
                                                    <asp:TabPanel runat="server" HeaderText="Pessoais" ID="TabPanelPessoais" >
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnDisparaUCPessoais" Style="display: none" runat="server" OnClick="btnDisparaUCPessoais_Click"
                                                                UseSubmitBehavior="False" />
                                                            <uc3:tab_default ID="tab_default1" runat="server" />
                                                            
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanelTitulacao" runat="server" HeaderText="Titulações">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnDisparaUCTitulacao" Style="display: none" runat="server" OnClick="btnDisparaUCTitulacao_Click" />
                                                            <uc3:tab_default ID="tab_default2" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanelPortaria" runat="server" HeaderText="Portarias">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnDisparaUCPortaria" Style="display: none" runat="server" OnClick="btnDisparaUCPortaria_Click" /><uc3:tab_default
                                                                ID="tab_default3" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanelCI" runat="server" HeaderText="CIs">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnDisparaUCCI" Style="display: none" runat="server" OnClick="btnDisparaUCCI_Click" /><uc3:tab_default
                                                                ID="tab_default4" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanelAviso" runat="server" HeaderText="Aviso de Férias">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnDisparaUCAviso" Style="display: none" runat="server" OnClick="btnDisparaUCAviso_Click" /><uc3:tab_default
                                                                ID="tab_default5" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanelRequerimento" runat="server" HeaderText="Requerimentos">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnDisparaUCRequerimento" Style="display: none" runat="server" OnClick="btnDisparaUCRequerimento_Click" /><uc3:tab_default
                                                                ID="tab_default6" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanelOutros" runat="server" HeaderText="Outros">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnDisparaUCOutros" Style="display: none" runat="server" OnClick="btnDisparaUCOutros_Click" /><uc3:tab_default
                                                                ID="tab_default7" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:TabPanel>
                                                </asp:TabContainer>
                                            </ContentTemplate>
                                            
                                        </asp:UpdatePanel>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                            ImageUrl="~/imagens/botao_enviar.png" onclick="ImageButton1_Click"/>
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
