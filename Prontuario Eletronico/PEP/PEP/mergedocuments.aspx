<%@ Page Language="C#" MasterPageFile="~/Site_Master.Master" AutoEventWireup="true" CodeBehind="mergedocuments.aspx.cs" Inherits="PEP.MergeDocuments" Title="Merge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="panelCentral" runat="server" DefaultButton="ButtonMesclar">
            <fieldset id="Fieldset2" class="fieldsetMergeDocuments">
                <legend style="font-family: 'Verdana'; font-weight: 900; font-size: large">Mesclar Documentos</legend>
                 <table border="0px" style="width: 370px" cellpadding="0px" align="center" cellspacing="0px">
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="LabelArquivo1" runat="server" Text="Arquivo 1: "></asp:Label>
                        </td>
                        <td class="cellTxtBox">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="style1">
                             <asp:Label ID="Label2" runat="server" Text="Arquivo 2: "></asp:Label>
                        </td>
                        <td class="cellTxtBox">
                            <asp:FileUpload ID="FileUpload2" runat="server" />                         
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp;
                            <asp:Button ID="ButtonMesclar" runat="server" onclick="Button1_Click" Text="Mesclar" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>