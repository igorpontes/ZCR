<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cadastroDocs.aspx.cs"
    Inherits="SistemaRH.CadastroDocs" Title="Sistema RH Digital" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/tab_default.ascx" TagName="tab_default" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.4.custom.js" type="text/javascript"></script>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.CornerTop').corner();
        $('.CornerDown').corner();

        var indexTab = 0;
        function getTab(sender, args) {
            indexTab = sender.get_activeTabIndex();

            if (indexTab == 0) {
                document.getElementById('<%= btnDisparaUCPessoais.ClientID %>').click();
            } else if (indexTab == 1) {
                document.getElementById('<%= btnDisparaUCTitulacao.ClientID %>').click();
            } else if (indexTab == 2) {
                document.getElementById('<%= btnDisparaUCPortaria.ClientID %>').click();
            } else if (indexTab == 3) {
                document.getElementById('<%= btnDisparaUCPortariasComProcesso.ClientID %>').click();
            } else if (indexTab == 4) {
                document.getElementById('<%= btnDisparaUCCI.ClientID %>').click();
            } else if (indexTab == 5) {
                document.getElementById('<%= btnDisparaUCAviso.ClientID %>').click();
            } else if (indexTab == 6) {
                document.getElementById('<%= btnDisparaUCRequerimento.ClientID %>').click();
            } else if (indexTab == 7) {
                document.getElementById('<%= btnDisparaUCOutros.ClientID %>').click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <br />
    <center>
        <div id="TopCadDocs" class="CornerTop" data-corner="top 10px">
            <asp:Label ID="Label2" runat="server" Text="&nbsp;&nbsp;Cadastrar" Style="font-family: 'Verdana';
                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                Font-Size="Medium"></asp:Label>
        </div>
        <div id="DownCadDocs" class="CornerDown" data-corner="bottom 10px">
            <table id="tableDefault" width="750px" border="0px" cellpadding="0px" align="center"
                cellspacing="2px">
                <tr>
                    <td colspan="3" align="center">
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
                <tr>
                    <td align="center" rowspan="5">
                        <asp:Image ID="ImageFoto" runat="server" ImageUrl="~/imagens/TemplateRosto.jpg" Width="120px" />
                        <%--<asp:ImageButton ID="ImageButtonFoto" runat="server" ImageUrl="~/imagens/TemplateRosto.jpg"
                                            Width="100px" />--%>
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
                            TabIndex="1" OnTextChanged="TextBoxMatricula_TextChanged"></asp:TextBox>
                        &nbsp;<asp:CustomValidator ID="CustomValidatorMatricula" runat="server" ErrorMessage="*Matrícula já existe!"
                            Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic" ControlToValidate="TextBoxMatricula"
                            OnServerValidate="CustomValidatorMatricula_ServerValidate"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <asp:Label ID="LabelCPF" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Overline="False"
                            Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" Text="CPF: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxCPF" runat="server" BackColor="#FFFFFF" Width="250px" TabIndex="1"></asp:TextBox>
                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="TextBoxCPF" Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"
                            ValidationExpression="(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})|(\d{9}-\d{2})$)" ErrorMessage="*CPF inválido!"></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="CustomValidatorCPF" runat="server" ErrorMessage="*CPF já existe!"
                            ControlToValidate="TextBoxCPF" Font-Names="Verdana" Font-Size="XX-Small" Display="Dynamic"
                            OnServerValidate="CustomValidatorCPF_ServerValidate"></asp:CustomValidator>
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
                    <td valign="middle">
                        <asp:Label ID="LabelCarregarFoto" runat="server" Font-Bold="False" Font-Names="Verdana"
                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                            Text="Carregar Foto: "></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="230px" />
                        <asp:ImageButton ID="ImageButtonCarregarImagem" runat="server" ImageUrl="~/imagens/load.gif"
                            ImageAlign="AbsMiddle" Style="text-align: right" OnClick="ImageButtonCarregarImagem_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="LabelDocs" runat="server" Text="Documentos:"></asp:Label>
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
                        </asp:ToolkitScriptManager>
                        <asp:UpdatePanel ID="UpdatePanelTabContainer" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />--%>
                                <%--<asp:PostBackTrigger ControlID="btnPreview" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="700px"
                                    Height="150px" AutoPostBack="false" OnClientActiveTabChanged="getTab" OnActiveTabChanged="TabContainer1_ActiveTabChanged"
                                    OnInit="TabContainer1_Init" OnLoad="TabContainer1_Load">
                                    <% 
                                         
                                        SistemaRH.Adaptador adpt = new SistemaRH.Adaptador();
                                        string login = (String)Session["usuario"];
                                        SistemaRH.Usuario usuario = adpt.retornaUsuario(login);
                                        // usuario.permissoes
                                        int[] permissoes = new int[8];
                                        int i = 0;
                                        foreach (var item in usuario.permissoes)
                                        {
                                            permissoes[i] = item.tipo_permissao;
                                            i++;
                                        }

                                        i = 0;
                                        if (permissoes[i] == 2)
                                        {
                                                 
                                    %>
                                    <asp:TabPanel runat="server" HeaderText="Pessoais" ID="TabPanelPessoais">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCPessoais" Style="display: none" runat="server" OnClick="btnDisparaUCPessoais_Click"
                                                UseSubmitBehavior="False" />
                                            <uc3:tab_default ID="tab_default1" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <% 
                                        }
                                             i++;
                                             if (permissoes[i] == 2)
                                             {

                                    %>
                                    <asp:TabPanel ID="TabPanelTitulacao" runat="server" HeaderText="Titulações">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCTitulacao" Style="display: none" runat="server" OnClick="btnDisparaUCTitulacao_Click" />
                                            <uc3:tab_default ID="tab_default2" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <% 
                                        }
                                            i++;
                                            if (permissoes[i] == 2)
                                            {

 
                                    %>
                                    <asp:TabPanel ID="TabPanelPortaria" runat="server" HeaderText="Portarias">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCPortaria" Style="display: none" runat="server" OnClick="btnDisparaUCPortaria_Click" /><uc3:tab_default
                                                ID="tab_default3" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <% 
                                        }
                                            i++;
                                            if (permissoes[i] == 2)
                                            {

 
                                    %>
                                    <asp:TabPanel ID="TabPanelPortariaComProcesso" runat="server" HeaderText="Portarias c/ processo">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCPortariasComProcesso" Style="display: none" runat="server"
                                                OnClick="btnDisparaUCPortariasComProcesso_Click" />
                                            <uc3:tab_default ID="tab_default4" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <% 
                                        }
                                            i++;
                                            if (permissoes[i] == 2)
                                            {

                                    %>
                                    <asp:TabPanel ID="TabPanelCI" runat="server" HeaderText="CIs">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCCI" Style="display: none" runat="server" OnClick="btnDisparaUCCI_Click" /><uc3:tab_default
                                                ID="tab_default5" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <% 
    
                                        }
                                            i++;
                                            if (permissoes[i] == 2)
                                            {

                                    %>
                                    <asp:TabPanel ID="TabPanelAviso" runat="server" HeaderText="Aviso de Férias">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCAviso" Style="display: none" runat="server" OnClick="btnDisparaUCAviso_Click" /><uc3:tab_default
                                                ID="tab_default6" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <% 
    
                                        }
                                            i++;
                                            if (permissoes[i] == 2)
                                            {

                                    %>
                                    <asp:TabPanel ID="TabPanelRequerimento" runat="server" HeaderText="Requerimentos">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCRequerimento" Style="display: none" runat="server" OnClick="btnDisparaUCRequerimento_Click" /><uc3:tab_default
                                                ID="tab_default7" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <% 
    
                                        }
                                            i++;
                                            if (permissoes[i] == 2)
                                            {

                                    %>
                                    <asp:TabPanel ID="TabPanelOutros" runat="server" HeaderText="Outros">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisparaUCOutros" Style="display: none" runat="server" OnClick="btnDisparaUCOutros_Click" /><uc3:tab_default
                                                ID="tab_default8" runat="server" />
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <%
    
}
                                        
                                    %>
                                </asp:TabContainer>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/botao_enviar.png"
                            OnClick="ImageButton1_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>
