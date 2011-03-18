<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cadastroDefault.aspx.cs" Inherits="SistemaRH.CadastroDefault" Title="Sistema RH Digital" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">

    <br />
<br />
<br />
<center>
    <table style="width: 50%; font-family:Verdana; vertical-align:middle;" >
        <tr>
            <td>
                <asp:ImageButton ID="ImageButtonCadastroPessoa" runat="server" 
                    ImageUrl="imagens/User.png" PostBackUrl="cadastroDocs.aspx" Enabled="False" />
            </td>
            <td>
                <asp:ImageButton ID="ImageButtonCadastroDocumento" runat="server" ImageUrl="imagens/Documents.png" PostBackUrl="cadastroDocs.aspx" />
            </td>
          
        </tr>
        <tr>
            <td>
                Cadastro de Pessoas
            </td>
            <td>
                Cadastro de Documentos
            </td>
         
        </tr>
    
    </table>
    </center>
</asp:Content>
