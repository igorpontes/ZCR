<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs"
    Inherits="GED_TCESE.WebForm2" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 140px;
        }
        .style2
        {
            width: 142px;
        }
        .style3
        {
            width: 62px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="lbModulos" class="divDefault">
        <table id="TabelaTipos" class="tabelaTipos">
            <tr style="text-align: center">
                <td align="center" width="99%">
                    <asp:RadioButtonList ID="RadioButtonListTipos" runat="server" AutoPostBack="True"
                        Font-Bold="True" RepeatDirection="Horizontal" Width="99%" RepeatColumns="3" 
                        RepeatLayout="Flow" ToolTip="Escolha o módulo desejado" Visible="false">
                        <asp:ListItem Selected="True">Jurisprudência</asp:ListItem>
                        <%--<asp:ListItem>Processo de Despesa</asp:ListItem>
                        <asp:ListItem>Protocolo</asp:ListItem>--%>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <%
            if (RadioButtonListTipos.Items[0].Selected)
            {                
        %>
        <asp:Panel ID="PanelJurisprudencia" runat="server" HorizontalAlign="Center" Height="350px" Width="100%" Visible="True" DefaultButton="ImageButtonPesquisar">
            <fieldset id="Fieldset2" class="fieldsetDefault">
                <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Jurisprudência</legend>
                <table id="TabelaJurisprudencia" title="Jurisprudencia" class="tabelaTiposPainel" style="vertical-align: bottom;
	                padding-right: 5px;	padding-left: 5px;	padding-top: 10px; text-align: justify;">
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelNumeroProcesso" runat="server" Font-Bold="True" Text="Número do Processo:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxNumeroProcesso" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Número do processo"></asp:TextBox>
                            <asp:Label ID="LabelInfNumeroProcesso" runat="server" ForeColor="Red" Text="* Somente números" 
                                Font-Size="Smaller"></asp:Label>
                        </td>
                        <td align="center" rowspan="7" class="rightCell">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelAnoProcesso" runat="server" Font-Bold="True" Text="Ano do Processo:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxAnoProcesso" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Ano do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelOrigem" runat="server" Font-Bold="True" Text="Origem:" Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxOrigem" runat="server" BackColor="#F3F3F3" 
                            Width="250px" ToolTip="Origem do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelAssunto" runat="server" Font-Bold="True" Text="Assunto:" Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxAssunto" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Assunto do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelDescricao" runat="server" Font-Bold="True" Text="Descrição:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDescricao" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Descrição do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelPessoaInteressado" runat="server" Font-Bold="True" Text="Interessado:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxInteressado" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Interessado no processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelQualquerCampo" runat="server" Font-Bold="True" Text="Busca Textual:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxQualquerCampo" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Busca Textual"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <br />
                            <asp:ImageButton ID="ImageButtonPesquisar" runat="server" ImageUrl="~/imagens/botao_Pesquisar.png"
                                OnClick="ImageButtonPesquisar_Click" ToolTip="Pesquisar dados" />
                            &nbsp;
                            <asp:ImageButton ID="ImageButtonBuscaAvancada" runat="server" ImageUrl="~/imagens/botaoBuscaAvancada.png"
                                OnClick="ImageButtonBuscaAvancada_Click" ToolTip="Busca avançada" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%
            }
            else if (RadioButtonListTipos.Items[1].Selected)
            {
        %>
        <asp:Panel ID="PanelProcessoDespesa" runat="server" Height="350px" Width="100%" Visible="True" DefaultButton="ImageButtonPesquisarDespesa">
            <fieldset id="Fieldset3" class="fieldsetDefault">
                <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Processo
                    de Despesa</legend>
                <table id="TabelaProcessoDespesa" title="Processo de Despesa" class="tabelaTiposPainel">
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="LabelErroDespesa" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="LabelDespesaDocumento1" runat="server" Font-Bold="True" Text="Número do processo: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDespesaDocumento1" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Número do processo"></asp:TextBox>
                        </td>
                        <td align="center" rowspan="7" class="rightCell">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="LabelDespesaDocumento2" runat="server" Font-Bold="True" Text="Ano do processo: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDespesaDocumento2" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Ano do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="LabelDespesaDocumento3" runat="server" Font-Bold="True" Text="Ano de referência: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDespesaDocumento3" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Origem do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="LabelDespesaDocumento4" runat="server" Font-Bold="True" Text="Mês de referência:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDespesaDocumento4" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Mês de referência do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="LabelDespesaDocumento5" runat="server" Font-Bold="True" Text="Nome da parte: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDespesaDocumento5" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Nome da parte"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="LabelDespesaDocumento6" runat="server" Font-Bold="True" Text="Valor da despesa: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDespesaDocumento6" runat="server" BackColor="#F3F3F3" 
                                Width="250px"  ToolTip="Valor da despesa"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="LabelDespesaQualquerCampo" runat="server" Font-Bold="True" Text="Busca Textual:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxDespesaQualquerCampo" runat="server" BackColor="#F3F3F3"
                                Width="250px" ToolTip="Busca Textual"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <br />
                            <asp:ImageButton ID="ImageButtonPesquisarDespesa" runat="server" ImageUrl="~/imagens/botao_Pesquisar.png"
                                OnClick="ImageButtonPesquisarDespesa_Click" ToolTip="Pesquisar dados" />
                            &nbsp;
                            <asp:ImageButton ID="ImageButtonBuscaAvancadaDespesa" runat="server" ImageUrl="~/imagens/botaoBuscaAvancada.png"
                                OnClick="ImageButtonBuscaAvancadaDespesa_Click" ToolTip="Busca avançada" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%
            }
            else
            {       
        %>
        <asp:Panel ID="PanelProtocolo" runat="server" Height="350px" Width="100%" Visible="True" DefaultButton="ImageButtonPesquisarProtocolo">
            <fieldset id="Fieldset1" class="fieldsetDefault">
                <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Protocolo</legend>
                <table id="TabelaProtocolo" title="Protocolo" class="tabelaTiposPainel">
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="LabelErroProtocolo" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="LabelProtocoloDocumento1" runat="server" Font-Bold="True" Text="Número/Ano do processo: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxProtocoloDocumento1" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Número e ano do processo"></asp:TextBox>
                        </td>
                        <td align="center" rowspan="7" class="rightCell">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="LabelProtocoloDocumento2" runat="server" Font-Bold="True" Text="Origem do processo: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxProtocoloDocumento2" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Origem do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="LabelProtocoloDocumento3" runat="server" Font-Bold="True" Text="Tipo de processo:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxProtocoloDocumento3" runat="server" BackColor="#F3F3F3" 
                                Width="250px"  ToolTip="Tipo de processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="LabelProtocoloDocumento4" runat="server" Font-Bold="True" Text="Ano de referência: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxProtocoloDocumento4" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Ano de referência do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="LabelProtocoloDocumento5" runat="server" Font-Bold="True" Text="Mês de referência: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxProtocoloDocumento5" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Mês de referência do processo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="LabelProtocoloDocumento6" runat="server" Font-Bold="True" Text="Nome da parte: "
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxProtocoloDocumento6" runat="server" BackColor="#F3F3F3" 
                                Width="250px" ToolTip="Nome da parte"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:Label ID="LabelProtocoloQualquerCampo" runat="server" Font-Bold="True" Text="Busca Textual:"
                                Width="180px"></asp:Label>
                        </td>
                        <td class="middleCell">
                            <asp:TextBox ID="TextBoxProtocoloQualquerCampo" runat="server" BackColor="#F3F3F3"
                                Width="250px" ToolTip="Busca Textual"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <br />
                            <asp:ImageButton ID="ImageButtonPesquisarProtocolo" runat="server" ImageUrl="~/imagens/botao_Pesquisar.png"
                                OnClick="ImageButtonPesquisarProtocolo_Click"  ToolTip="Pesquisar dados"/>
                            &nbsp;
                            <asp:ImageButton ID="ImageButtonBuscaAvancadaProtocolo" runat="server" ImageUrl="~/imagens/botaoBuscaAvancada.png"
                                OnClick="ImageButtonBuscaAvancadaProtocolo_Click"  ToolTip="Busca avançada"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <%                
            }        
        %>
    </div>
</asp:Content>