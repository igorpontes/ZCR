<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="alterarColaborador.aspx.cs" Inherits="SistemaRH.AlterarColaborador" Title="Untitled Page" %>
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
<table align="center" style="height: 80%">
        <tr>
            <td align="center" style="width: 60%;">
                <br />
                <ul id="split">
                    <li id="cadastro" style="top: 30%">
                        <h3 style="width:750px">
                            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Editar" Style="font-family: 'Verdana';
                                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                                Font-Size="Medium"></asp:Label></h3>
                        <div>
                            <table id="tableDefault" width="750px" border="0px" style="" cellpadding="0px" align="center" cellspacing="2px">
                                <tr>
                                    <td style="height: 3px" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="6">
                                        <asp:Label ID="LabelFoto" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Overline="False"
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" Text="Foto: "></asp:Label>
                                    </td>
                                    <td align="center" rowspan="6">
                                        <asp:ImageButton ID="ImageButtonFoto" runat="server" ImageUrl="~/imagens/TemplateRosto.jpg"
                                            Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelNomeColaborador" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Nome do Colaborador: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNome_Colaborador" runat="server" BackColor="#FFFFFF" Width="250px"
                                            TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelData_Nascimento" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Data de Nascimento: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxData_Nascimento" runat="server" BackColor="#FFFFFF" TabIndex="3"
                                            Width="250px">(dd/mm/yyyy)</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td valign="middle">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                
                                <tr>
                                    <td valign="middle">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td valign="middle">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td valign="middle">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="1">
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        &nbsp;
                                    </td>
                                    <td align="center" colspan="2">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td align="center" colspan="4">
                                        <asp:ImageButton ID="ImageButtonCadastrar" runat="server" ImageUrl="~/imagens/botao_Enviar.png"
                                            TabIndex="25" />
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
