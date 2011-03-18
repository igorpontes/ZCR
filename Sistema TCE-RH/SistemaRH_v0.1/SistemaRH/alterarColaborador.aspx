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
                                        <asp:Label ID="LabelNaturalidade" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Naturalidade: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNaturalidade" runat="server" BackColor="#FFFFFF" TabIndex="5"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelSexo" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Overline="False"
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" Text="Sexo: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadioButtonListSexo" runat="server" Font-Size="Smaller"
                                            RepeatDirection="Horizontal" TabIndex="4">
                                            <asp:ListItem>Masculino</asp:ListItem>
                                            <asp:ListItem>Feminino</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelNome_Pai" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Pai:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNome_Pai" runat="server" BackColor="#FFFFFF" TabIndex="6"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelNome_Mãe" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Mãe:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNome_Mae" runat="server" BackColor="#FFFFFF" TabIndex="7"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelEndereco" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Endereço: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxEndereco" runat="server" BackColor="#FFFFFF" TabIndex="10"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelNumero" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Número:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNumero" runat="server" BackColor="#FFFFFF" TabIndex="11"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelComplemento" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Complemento:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxComplemento" runat="server" BackColor="#FFFFFF" TabIndex="12"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelBairro" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Bairro:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxBairro" runat="server" BackColor="#FFFFFF" TabIndex="13"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Overline="False"
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" Text="Cidade: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCidade" runat="server" BackColor="#FFFFFF" TabIndex="14"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Overline="False"
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" Text="Estado: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxEstado" runat="server" BackColor="#FFFFFF" TabIndex="15"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:Label ID="LabelCEP" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Overline="False"
                                            Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller" Text="CEP: "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCEP" runat="server" BackColor="#FFFFFF" TabIndex="16" Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelCargo" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Cargo:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxCargo" runat="server" BackColor="#FFFFFF" TabIndex="17" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelTelefoneResidencial" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Telefone Residencial:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelefoneResidencial" runat="server" BackColor="#FFFFFF" TabIndex="16"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="LabelTelefone2" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Telefone Celular:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxTelefoneCelular" runat="server" BackColor="#FFFFFF" TabIndex="16"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <asp:Label ID="LabelArquivoAnexado" runat="server" Font-Bold="False" Font-Names="Verdana"
                                            Font-Overline="False" Style="font-family: 'Verdana'; font-weight: 700; font-size: smaller"
                                            Text="Anexar Currículo:"></asp:Label>
                                    </td>
                                    <td colspan="1">
                                        <asp:FileUpload ID="FileUploadArquivo" runat="server" TabIndex="25" Width="250px" />
                                    </td>
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
