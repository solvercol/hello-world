<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucPanelEstado.ascx.cs" Inherits="Modules.Reclamos.UserControls.WucPanelEstado" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width:35%">
            Estado:
        </td>
        <td>
            <b> <asp:Literal ID="litestado" runat="server" ></asp:Literal></b>
        </td>
    </tr>
    <tr>
        <td>
            Solicitado Por:
        </td>
        <td>
            <b> <asp:Literal ID="litSolicitadoPor" runat="server" ></asp:Literal></b>
        </td>
    </tr>
    <tr>
        <td>
            Fecha Solicitud:
        </td>
        <td>
            <b> <asp:Literal ID="litFechaSolicitud" runat="server" ></asp:Literal></b>
        </td>
    </tr>
    <tr>
        <td>
            Responsable&nbsp;Actual:
        </td>
        <td>
            <b> <asp:Literal ID="litResponsable" runat="server" ></asp:Literal></b>
        </td>
    </tr>
    <tr>
        <td>
            Asignado en:
        </td>
        <td>
            <b> <asp:Literal ID="litAsignadoEn" runat="server" ></asp:Literal></b>
        </td>
    </tr>
    <tr>
        <td>
            No. Días:
        </td>
        <td>
            <b> <asp:Literal ID="litNumeroDias" runat="server" ></asp:Literal></b>
        </td>
    </tr>
</table>