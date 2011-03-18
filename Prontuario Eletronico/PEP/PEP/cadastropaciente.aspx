<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true"
    CodeBehind="cadastropaciente.aspx.cs" Inherits="PEP.WebForm4" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 65px; height: 378px;">
        <asp:Panel ID="panelCentral" runat="server" CssClass="cadastroPaciente" Height="516px" Width="1221px" DefaultButton="ImageButtonCadastrar">
            <fieldset id="Fieldset" class="fieldsetCadastrar">
            <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Cadastrar</legend>
                <table align="center" border="0px" cellpadding="0px" cellspacing="0px" 
	                style="height: 421px; width: 910px; margin-left: 0px;">
	                <tr>
	                    <td class="tdDefault" colspan="4" align="center">
	                    
	                        <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
	                    
	                    </td>
	                </tr>
	                <tr>
                        <td class="tdDefault">
                        </td>
                        <td class="tdDefault">
                        </td>
                        <td class="tdDefault" rowspan="6">
                            <asp:Label ID="LabelFoto" runat="server" Font-Bold="False" Font-Names="Verdana" 
                                Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Foto: "></asp:Label>
                        </td>
                        <td align="center" class="tdDefault" rowspan="6">
                            <asp:ImageButton ID="ImageButtonFoto" runat="server" 
                                ImageUrl="~/imagens/TemplateRosto.jpg" Width="88px" Height="96px" />
                        </td>
                    </tr>
	                <tr>
	                    <td class="tdDefault">
	                    
	                    </td>
	                    <td class="tdDefault">
	                    
	                    </td>
	                </tr>
	                <tr>
	                    <td class="tdDefault">
	                    
	                    </td>
	                    <td class="tdDefault">
	                    
	                    </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelNumeroRegistro" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Registro: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNumero_Registro" runat="server" BackColor="#FFFFFF" 
				                Width="250px" TabIndex="1"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelNomePaciente" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Nome do Paciente: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNome_Paciente" runat="server" BackColor="#FFFFFF" 
				                Width="250px" TabIndex="2"></asp:TextBox>
		                </td>
		                <td class="tdDefault">
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelData_Nascimento" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Data de Nascimento: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxData_Nascimento" runat="server" BackColor="#FFFFFF" 
				                TabIndex="3" Width="250px">(dd/mm/yyyy)</asp:TextBox>
		                </td>
		                <td class="tdDefault">
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelSexo" runat="server" Font-Bold="False" Font-Names="Verdana" 
				                Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Sexo: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:RadioButtonList ID="RadioButtonListSexo" runat="server" 
				                Font-Size="Smaller" RepeatDirection="Horizontal" TabIndex="4">
				                <asp:ListItem>Masculino</asp:ListItem>
				                <asp:ListItem>Feminino</asp:ListItem>
			                </asp:RadioButtonList>
		                </td>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelNaturalidade" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Naturalidade: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNaturalidade" runat="server" BackColor="#FFFFFF" 
				                TabIndex="5" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelNome_Pai" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Pai:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNome_Pai" runat="server" BackColor="#FFFFFF" 
				                TabIndex="6" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelNome_Mãe" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Mãe:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNome_Mae" runat="server" BackColor="#FFFFFF" 
				                TabIndex="7" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelPessoa_Responsavel" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Responsável:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxPessoa_Responsavel" runat="server" BackColor="#FFFFFF" 
				                style="margin-bottom: 0px" TabIndex="8" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault">
			                <asp:Label ID="LabelProcedencia" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Procedência:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxProcedencia" runat="server" BackColor="#FFFFFF" 
				                TabIndex="9" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelEndereco" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Endereço: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxEndereco" runat="server" BackColor="#FFFFFF" 
				                TabIndex="10" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault">
			                <asp:Label ID="LabelNumero" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Número:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNumero" runat="server" BackColor="#FFFFFF" 
				                TabIndex="11" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelComplemento" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Complemento:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxComplemento" runat="server" BackColor="#FFFFFF" 
				                TabIndex="12" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelBairro" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Bairro:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxBairro" runat="server" BackColor="#FFFFFF" 
				                TabIndex="13" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Verdana" 
				                Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Cidade: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxCidade" runat="server" BackColor="#FFFFFF" 
				                TabIndex="14" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault">
			                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Verdana" 
				                Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Estado: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxEstado" runat="server" BackColor="#FFFFFF" 
				                TabIndex="15" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelCEP" runat="server" Font-Bold="False" Font-Names="Verdana" 
				                Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="CEP: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxCEP" runat="server" BackColor="#FFFFFF" TabIndex="16" 
				                Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault">
			                <asp:Label ID="LabelProfissao" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Profissão:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxProfissao" runat="server" BackColor="#FFFFFF" 
				                TabIndex="17" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td>
			                <asp:Label ID="LabelTelefoneResidencial" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Telefone Residencial:"></asp:Label>
		                </td>
		                <td>
			                <asp:TextBox ID="TextBoxTelefoneResidencial" runat="server" BackColor="#FFFFFF" 
                                TabIndex="18" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelTelefone2" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Telefone Celular:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxTelefoneCelular" runat="server" BackColor="#FFFFFF" 
                                TabIndex="19" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td>
			                <asp:Label ID="LabelTelefoneComercial" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Telefone Comercial:"></asp:Label>
		                </td>
		                <td>
			                <asp:TextBox ID="TextBoxTelefoneComercial" runat="server" BackColor="#FFFFFF" 
                                TabIndex="20" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault" valign="middle">
			                &nbsp;</td>
		                <td class="tdDefault">
			                &nbsp;</td>
	                </tr>
	                <tr>
                        <td>
                            <asp:Label ID="LabelNome_Clinica_Diagnostico" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Nome da Clínica de Diganóstico:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxNome_Clinica_Diagnostico" runat="server" 
                                BackColor="#FFFFFF" TabIndex="21" Width="250px"></asp:TextBox>
                        </td>
                        <td class="tdDefault" valign="middle">
                            <asp:Label ID="LabelDiagnostico" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Diagnóstico:"></asp:Label>
                        </td>
                        <td class="tdDefault">
                            <asp:TextBox ID="TextBoxDiagnostico" runat="server" BackColor="#FFFFFF" 
                                TabIndex="22" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelCID" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" 
				                Text="CID:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxCID" runat="server" 
				                BackColor="#FFFFFF" TabIndex="23" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault">
			                <asp:Label ID="LabelNome_Medico" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Nome do Médico:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNome_Medico" runat="server" 
				                BackColor="#FFFFFF" TabIndex="24" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault" valign="middle">
			                <asp:Label ID="LabelNome_Clinica_Internacao" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Nome da Clínica de Internação:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxNome_Clinica_Internacao" runat="server" BackColor="#FFFFFF" 
				                TabIndex="25" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault">
			                <asp:Label ID="LabelDianostico_Provisorio" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Diganóstico Provisório:"></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxDiagnostico_Provisorio" runat="server" BackColor="#FFFFFF" 
				                TabIndex="26" Width="250px"></asp:TextBox>
		                </td>
	                </tr>
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelData_Internacao" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Data de Internação: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxData_Internacao" runat="server" BackColor="#FFFFFF" 
                                TabIndex="27" Width="250px">(dd/mm/yyyy)</asp:TextBox>
		                </td>
		                <td class="tdDefault" valign="middle">
                		
		                    <asp:Label ID="LabelMedico_Solicitante1" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Médico 1:"></asp:Label>
                		
		                </td>
	                    <td class="tdDefault">
                            <asp:TextBox ID="TextBoxMedico_Solicitante1" runat="server" BackColor="#FFFFFF" 
                                TabIndex="28" Width="250px"></asp:TextBox>
                        </td>
	                </tr>
	                
	                <tr>
		                <td class="tdDefault">
			                <asp:Label ID="LabelMedicoSolicitante2" runat="server" Font-Bold="False" 
				                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Médico 2: "></asp:Label>
		                </td>
		                <td class="tdDefault">
			                <asp:TextBox ID="TextBoxMedico_Solicitante2" runat="server" BackColor="#FFFFFF" 
                                TabIndex="29" Width="250px"></asp:TextBox>
		                </td>
		                <td class="tdDefault" valign="middle">
                		
		                    <asp:Label ID="LabelMedico_Solicitante3" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Médico 3:"></asp:Label>
                		
		                </td>
	                    <td class="tdDefault">
                            <asp:TextBox ID="TextBoxMedico_Solicitante3" runat="server" BackColor="#FFFFFF" 
                                TabIndex="30" Width="250px"></asp:TextBox>
                        </td>
	                </tr>
	                
	                <tr>
		                <td class="tdDefault" valign="middle">
		                    <asp:Label ID="LabelMedico_Solicitante4" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Médico 4:"></asp:Label>
		                </td>
	                    <td class="tdDefault">
                            <asp:TextBox ID="TextBoxMedico_Solicitante4" runat="server" BackColor="#FFFFFF" 
                                TabIndex="31" Width="250px"></asp:TextBox>
                        </td>
		                <td>
                	        <asp:Label ID="LabelArquivoAnexado" runat="server" Font-Bold="False" 
                                Font-Names="Verdana" Font-Overline="False" Style="font-family: 'Verdana';
					                font-weight: 700; font-size: smaller" Text="Arquivo anexado:"></asp:Label>
                        </td>
		                <td class="tdDefault" colspan="2">
		                    <asp:FileUpload ID="FileUploadArquivo" runat="server" TabIndex="32" Width="250px" />
                        </td>
	                </tr>
                    <tr>
                        <td class="tdDefault" valign="middle">
                            &nbsp;</td>
                        <td align="center" class="tdDefault" colspan="2">
                            &nbsp;</td>
                        <td class="tdDefault">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdDefault" valign="middle">
                        </td>
                        <td align="center" class="tdDefault" colspan="2">
                            <asp:ImageButton ID="ImageButtonCadastrar" runat="server" 
                                ImageUrl="~/imagens/botao_Enviar.png" onclick="ImageButtonCadastrar_Click" 
                                TabIndex="33" />
                        </td>
                        <td class="tdDefault">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
