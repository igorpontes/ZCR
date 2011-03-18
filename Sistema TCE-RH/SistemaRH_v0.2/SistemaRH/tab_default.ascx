<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tab_default.ascx.cs" Inherits="SistemaRH.tab_default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Label ID="Label1" runat="server" Text="Incluir Documento:"></asp:Label>
        <br />
        <asp:FileUpload
            ID="FileUpload1" runat="server" />
        <br />
       <br />
        <asp:ImageButton ID="ImageButtonSalvar" runat="server" 
            onclientclick="return confirm('Você tem certeza que quer adicionar o arquivo?');" 
            onclick="ImageButtonSalvar_Click" ImageUrl="~/imagens/botao_salvar.png" />
        <br />
        <asp:Label ID="LabelErro" runat="server"></asp:Label>
        <br />
        <asp:Label ID="LabelArquivo" runat="server" Text=""></asp:Label>
        &nbsp;<asp:LinkButton ID="LinkButtonVer" runat="server" OnClick="LinkButtonVer_Click">Ver</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" 
            onclick="LinkButtonDelete_Click">Deletar</asp:LinkButton>
            
        <asp:ImageButton ID="ImageButtonVer" runat="server" 
            ImageUrl="imagens/Preview (2).png" onclick="ImageButtonVer_Click" />
        <asp:ImageButton ID="ImageButtonDelete" runat="server" 
            ImageUrl="imagens/Delete (1).png" onclick="ImageButtonDelete_Click" 
            style="width: 16px" />
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="ImageButtonSalvar" />
    </Triggers>
</asp:UpdatePanel>
