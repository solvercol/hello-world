<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmTestWUC.aspx.cs" Inherits="Modules.AccionesPC.Admin.FrmTestWUC" %>
<%@ Register src="../UserControls/WUCAdminComentariosRespuestaSolicitud.ascx" tagname="WUCAdminComentariosRespuestaSolicitud" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WUCAdminComentariosRespuestaSolicitud ID="WUCAdminComentariosRespuestaSolicitud1" 
        runat="server" />
</asp:Content>

