<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cadastrar.aspx.cs"
    Inherits="GED_TCESE.WebForm10" Title="Tribunal de Contas do Estados de Sergipe - Gestão Eletrônica de Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilos.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelCadastrar" runat="server" Height="350px" Width="100%" Visible="True">
        <fieldset id="Fieldset2" class="fieldsetCadastrar">
            <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Cadastrar</legend>
            
            <table>
                <tr>
                    <td align="right" colspan="3">
                    <br />
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <asp:LinkButton ID="LinkButtonVoltar" runat="server" Font-Bold="True" Font-Underline="False"
                            ForeColor="Black" OnClick="LinkButtonVoltar_Click" ToolTip="Voltar a página inicial">&lt; &lt; Voltar</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="LabelErro" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelNumeroProcesso" runat="server" Font-Bold="True" 
                            Font-Size="Small" Width="160px">Número do Processo: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxNumeroProcesso" runat="server" Width="250px" 
                            BackColor="#F3F3F3" ToolTip="Número do processo" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelAnoProcesso" runat="server" Font-Bold="True" 
                            Font-Size="Small" Width="160px">Ano do Processo: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxAnoProcesso" runat="server" Width="90px" 
                            BackColor="#F3F3F3" ToolTip="Ano do processo" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabeOrigem" runat="server" Font-Bold="True" Font-Size="Small" 
                            Width="160px">Origem: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxOrigem" runat="server" Width="250px" 
                            BackColor="#F3F3F3" ToolTip="Origem do processo" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelAssunto" runat="server" Font-Bold="True" Font-Size="Small" 
                            Width="160px">Assunto: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxAssunto" runat="server" Width="250px" 
                            BackColor="#F3F3F3" ToolTip="Assunto do processo" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelDescricao" runat="server" Font-Bold="True" 
                            Font-Size="Small" Width="160px">Descrição: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxDescricao" runat="server" Width="250px" 
                            BackColor="#F3F3F3" ToolTip="Descrição do processo" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelArquivo" runat="server" Font-Bold="True" Font-Size="Small" 
                            Width="160px">Arquivo: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="FileUpload" runat="server" Width="259px" BackColor="#F3F3F3"
                            ToolTip="Arquivo anexado" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelQtdPessoas" runat="server" Font-Bold="True" 
                            Font-Size="Small" Width="160px">Qtd. Pessoas: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxQtdPessoas" runat="server" Width="40px" 
                            BackColor="#F3F3F3" ToolTip="Quantidade de interessados" />
                        <asp:Button ID="ButtonQtdPessoasInserir" runat="server" Text="Inserir" ToolTip="Inserir Pessoas"
                            OnClick="ButtonQtdPessoasInserir_Click" />
                        &nbsp;&nbsp;
                        <asp:RangeValidator ID="RangeValidatorQtdPessoas" runat="server" ControlToValidate="TextBoxQtdPessoas"
                            ErrorMessage="* Quantidade Inválida" ForeColor="Red" MaximumValue="4" MinimumValue="0" Font-Names="Verdana" Font-Size="XX-Small"></asp:RangeValidator>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelPessoa1" runat="server" Font-Bold="True" Font-Size="Small" 
                            Width="160px">Pessoa 1: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxPessoa1" runat="server" Width="250px" BackColor="#F3F3F3"
                            Visible="False" ToolTip="Interessado 1"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelPessoa2" runat="server" Font-Bold="True" Font-Size="Small" 
                            Width="160px">Pessoa 2: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxPessoa2" runat="server" Width="250px" 
                            Visible="False" BackColor="#F3F3F3" ToolTip="Interessado 2" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelPessoa3" runat="server" Font-Bold="True" Font-Size="Small" 
                            Width="160px">Pessoa 3: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxPessoa3" runat="server" Width="250px" Visible="False" BackColor="#F3F3F3"
                            Style="margin-left: 0px" ToolTip="Interessado 3" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="LabelPessoa4" runat="server" Font-Bold="True" Font-Size="Small" 
                            Width="160px">Pessoa 4: </asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBoxPessoa4" runat="server" Width="250px" Visible="False" BackColor="#F3F3F3"
                            Style="margin-left: 0px" ToolTip="Interessado 4" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="padding:5px;">
                        <asp:ImageButton ID="ImageButtonEnviar" runat="server" BackColor="#F3F3F3" ImageUrl="~/imagens/botao_Enviar.png"
                            Style="height: 24px" OnClick="ImageButtonEnviar_Click" ToolTip="Inserir dados"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
</asp:Content>
