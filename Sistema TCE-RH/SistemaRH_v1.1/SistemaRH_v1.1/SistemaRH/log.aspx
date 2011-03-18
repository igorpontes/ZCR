<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="log.aspx.cs"
    Inherits="SistemaRH.WebFormLog" Title="Sistema RH Digital" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="estilo.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui-1.8.4.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.4.custom.js" type="text/javascript"></script>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script type="text/javascript">
        //          $(document).ready(function() {
        //            $("#TextBoxDataInicio").datepicker();
        //            $("#TextBoxDataFinal").datepicker();
        //          });

        $('.CornerTop').corner();
        $('.CornerDown').corner();
        $('.CornerGrid').corner();


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="False">
    </asp:ToolkitScriptManager>
    <br />
    <center>
        <div id="TopLog" class="CornerTop" data-corner="top 10px">
            <asp:Label ID="Label1" runat="server" Text="&nbsp;&nbsp;Logs" Style="font-family: 'Verdana';
                font-weight: bold; font-size: medium;" Font-Names="Verdana" Font-Bold="True"
                Font-Size="Medium"></asp:Label>
        </div>
        <div id="DownLog" class="CornerDown" data-corner="bottom 10px">
            <table id="tableDefault" width="800px" border="0px" cellpadding="0px" align="center"
                cellspacing="2px">
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Pesquisar:"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="TextBoxPesquisa" runat="server" Height="14px"></asp:TextBox>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:ImageButton ID="ImageButtonEnviarPesquisa" runat="server" OnClick="ButtonEnviarPesquisa_Click"
                                        Height="16px" ImageUrl="~/imagens/search_256.png" Width="16px" />
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                </td>
                                <td align="left" valign="middle">
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Período:"></asp:Label>
                                </td>
                                <td>
                                    <%--<input id="TextBoxDataInicio" name="TextBoxDataInicio" type="text" style="height: 14px;
                                        width: 100px;" runat="server" />--%>
                                    <asp:TextBox ID="TextBoxDataInicio" runat="server" Height="14px" Width="100px"></asp:TextBox>
                                    <%-- <asp:ImageButton runat="server" ID="btnDataInicio" CausesValidation="false" ImageUrl="../../imagens/Calendar.png"
                                        ImageAlign="absmiddle" />--%>
                                    <asp:CalendarExtender ID="CalendarExtenderInicio" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="TextBoxDataInicio" TargetControlID="TextBoxDataInicio" Animated="False">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    à
                                </td>
                                <td>
                                    <%--<input id="TextBoxDataFinal" type="text" style="height: 14px; width: 100px;" runat="server" />--%>
                                    <asp:TextBox ID="TextBoxDataFinal" runat="server" Height="14px" Width="100px"></asp:TextBox>
                                    <%-- <asp:ImageButton runat="server" ID="btnDataFinal" CausesValidation="false" ImageUrl="../../imagens/Calendar.png"
                                        ImageAlign="absmiddle" />--%>
                                    <asp:CalendarExtender ID="CalendarExtenderFinal" runat="server" Animated="False"
                                        Enabled="True" Format="dd/MM/yyyy" PopupButtonID="TextBoxDataFinal" TargetControlID="TextBoxDataFinal">
                                    </asp:CalendarExtender>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButtonPesquisaIntervaloDatas" runat="server" Height="16px"
                                        ImageUrl="~/imagens/search_256.png" Width="16px" OnClick="ImageButtonPesquisaIntervaloDatas_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td align="center" style="width: 100%">
                        <div class="CornerGrid" data-corner="10px">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                ForeColor="#4E7C3B" GridLines="None" Height="100%" Width="100%" AllowPaging="True"
                                HorizontalAlign="Center" AllowSorting="True" Style="margin-top: 0px; margin-left: 0px;
                                width: 100%;" Font-Names="Verdana" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSorting="GridView1_Sorting">
                                <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" Visible="False" />
                                    <asp:BoundField DataField="usuario_log" HeaderText="Usuário" SortExpression="usuario_log" />
                                    <asp:BoundField DataField="tipo_acao_log" HeaderText="Ação" SortExpression="tipo_acao_log" />
                                    <asp:BoundField DataField="mensagem_acao_log" HeaderText="Mensagem" SortExpression="mensagem_acao_log">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="data_log" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" SortExpression="data_log" />
                                </Columns>
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <table>
                                        <tr valign="middle">
                                            <td>
                                                <asp:Image ID="ImageAttention" runat="server" ImageUrl="~/imagens/attention.gif"
                                                    Width="25px" />
                                            </td>
                                            <td valign="middle">
                                                <asp:Label ID="LabelGridVazio" runat="server" Text="Tabela de Logs Vazia" ForeColor="White"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#4E7C3B" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <EditRowStyle BackColor="#4E7C3B" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="TodosLogs"
                            TypeName="SistemaRH.Adaptador"></asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </center>
</asp:Content>
