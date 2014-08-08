<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAddReclamo.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmAddReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="../UserControls/WUCAdminReclamoProducto.ascx" tagname="WucAdminReclamoProducto" tagprefix="ucAdminRecProducto" %>
<%@ Register src="../UserControls/WUCAdminRecServicioT1.ascx" tagname="WucAdminReclamoServicioT1" tagprefix="ucAdminRecServicioT1" %>
<%@ Register src="../UserControls/WUCAdminRecServicioT2.ascx" tagname="WucAdminReclamoServicioT2" tagprefix="ucAdminRecServicioT2" %>
<%@ Register src="../UserControls/WUCAdminRecServicioT3.ascx" tagname="WucAdminReclamoServicioT3" tagprefix="ucAdminRecServicioT3" %>
<%@ Register src="../UserControls/WUCAdminRecServicioT4.ascx" tagname="WucAdminReclamoServicioT4" tagprefix="ucAdminRecServicioT4" %>
<%@ Register src="../UserControls/WUCAdminRecServicioT5.ascx" tagname="WucAdminReclamoServicioT5" tagprefix="ucAdminRecServicioT5" %>
<%@ Register src="../UserControls/WUCAdminCostosReclamo.ascx" tagname="WucCostosReclamo" tagprefix="ucCostosReclamo" %>
<%@ Register src="../UserControls/WUCAdminSolucionesReclamo.ascx" tagname="WucAdminSolucionesReclamo" tagprefix="ucAdminSolucionesReclamo" %>
<%@ Register src="../UserControls/WUCAdminAlternativasReclamo.ascx" tagname="WucAdminAlternativasReclamo" tagprefix="ucAdminAlternativasReclamo" %>
<%@ Register src="../UserControls/WUCAdminActividadesReclamo.ascx" tagname="WucActividadesReclamo" tagprefix="ucActividadesReclamo" %>
<%@ Register src="../UserControls/WUCAdminComentariosRespuestaReclamo.ascx" tagname="WucAdminComentariosRespuestaReclamo" tagprefix="ucAdminComentariosRespuestaReclamo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="upgeneral" runat="server">
        <ContentTemplate>        
            <%--<ucAdminRecProducto:WucAdminReclamoProducto id="wucAddReclamoProducto" runat="server" />--%>
            <%--<ucAdminRecServicioT1:WucAdminReclamoServicioT1 id="wucAddReclamoServicioT1" runat="server" />--%>
            <%--<ucAdminRecServicioT2:WucAdminReclamoServicioT2 id="wucAddReclamoServicioT2" runat="server" />--%>
            <%--<ucAdminRecServicioT3:WucAdminReclamoServicioT3 id="wucAddReclamoServicioT3" runat="server" />--%>
            <%--<ucAdminRecServicioT4:WucAdminReclamoServicioT4 id="wucAddReclamoServicioT4" runat="server" />--%>
            <%--<ucAdminRecServicioT5:WucAdminReclamoServicioT5 id="wucAddReclamoServicioT5" runat="server" />--%>
            <%--<ucCostosReclamo:WucCostosReclamo id="wucAddReclamoServicioT5" runat="server" />--%>
            <%--<ucAdminSolucionesReclamo:WucAdminSolucionesReclamo id="wucAddReclamoServicioT5" runat="server" />--%>
            <%--<ucAdminAlternativasReclamo:WucAdminAlternativasReclamo id="wucAddReclamoServicioT5" runat="server" />--%>
            <ucActividadesReclamo:WucActividadesReclamo id="wucAddReclamoServicioT5" runat="server" />
            <%--<ucAdminComentariosRespuestaReclamo:WucAdminComentariosRespuestaReclamo id="wucAddReclamoServicioT5" runat="server" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>