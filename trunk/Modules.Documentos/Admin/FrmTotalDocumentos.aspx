<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmTotalDocumentos.aspx.cs" Inherits="Modules.Documentos.Admin.FrmTotalDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:3px; text-align:right;">
        <%--<asp:Button ID="btnNuevo" runat="server" Text="Nuevo Documento" OnClick="BtnNuevoClick" />--%>
    </div>
    <table>
        <tr>
            <td>Nombre</td>
            <td>
                <asp:textbox id="txtFiltroNombre" runat="server" Width="500px"></asp:textbox>                    
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td>Estado</td>
            <td><asp:DropDownList ID="ddlEstado" runat="server" Width="250px"></asp:DropDownList></td>
            <td>
                <asp:button id="btnFiltrar" runat="server" OnClick="BtnFindClick" text="Buscar" ValidationGroup="Filter" Width="70px"></asp:button>
            </td>
        </tr>
        <tr>
            <td>Responsable</td>
            <td><asp:DropDownList ID="ddlResponsableDoc" runat="server" Width="250px"></asp:DropDownList></td>
            <td>
                <asp:button id="btnLimpiar" runat="server" OnClick="BtnClearFilterClick" text="Limpiar" Width="70px"></asp:button>
            </td>
        </tr>
             
	</table>    
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

