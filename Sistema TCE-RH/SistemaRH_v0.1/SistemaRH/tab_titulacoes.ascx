<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tab_titulacoes.ascx.cs"
    Inherits="SistemaRH.tab_titulacoes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Label ID="Label1" runat="server" Text="Incluir Documento:"></asp:Label>
        <br />
        <asp:FileUpload
            ID="FileUpload1" runat="server" />
        <br />
        <asp:Button ID="ButtonEnviar" runat="server" Text="Enviar" 
            OnClick="ButtonEnviar_Click" 
            onclientclick="return confirm('Você tem certeza que quer adicionar o arquivo?');" />
        <br />
        <asp:Label ID="LabelErro" runat="server"></asp:Label>
        <br />
        <asp:Label ID="LabelArquivo" runat="server" Text=""></asp:Label>
        &nbsp;<asp:LinkButton ID="LinkButtonVer" runat="server" OnClick="LinkButtonVer_Click">Ver</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="LinkButtonDelete" runat="server" 
            onclick="LinkButtonDelete_Click">Deletar</asp:LinkButton>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="ButtonEnviar" />
    </Triggers>
</asp:UpdatePanel>
