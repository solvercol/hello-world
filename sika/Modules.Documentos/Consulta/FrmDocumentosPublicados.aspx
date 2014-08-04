<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmDocumentosPublicados.aspx.cs" Inherits="Modules.Documentos.Consulta.FrmDocumentosPublicados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Nombre <asp:textbox id="txtFiltroNombre" runat="server" Width="500px"></asp:textbox>
        <asp:button id="btnFiltrar" runat="server" OnClick="BtnFindClick" text="Buscar" ValidationGroup="Filter"></asp:button>
	</p>
    <table width="700px" style="align-self:flex-start;border:inset;border-width:1px">
        <thead>
            <tr>
                <th style="text-align:left; width:30px">Categoría|</th>
                <th style="text-align:left">Título</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="PnlContainer" Width="700px" Height="360px" ScrollBars="Vertical" runat="server">
                        <asp:TreeView ID="TrvwDocumentos" runat="server" Width="680px" CollapseImageUrl="~/Resources/Images/Collapse.gif" ExpandImageUrl="~/Resources/Images/Expand.gif">
                        </asp:TreeView>
                    </asp:Panel>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
