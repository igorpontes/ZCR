<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="alterardoc.aspx.cs"
    Inherits="SistemaRH.WebForm4" Title="Untitled Page" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

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
                                        
                                        
                                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                                            AllowCustomErrorsRedirect="False" CombineScripts="False">
                                        </asp:ToolkitScriptManager>
                                        <br />
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                                                    Width="380px">
                                                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                        <HeaderTemplate>
                                                            TabPanel1
                                                        </HeaderTemplate>
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                                                    </asp:TabPanel>
                                                    <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                                                    </asp:TabPanel>
                                                </asp:TabContainer>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagens/botao_enviar.png" />
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
