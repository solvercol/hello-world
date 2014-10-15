<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminInformacionSolicitud.ascx.cs" Inherits="Modules.AccionesPC.UserControls.WUCAdminInformacionSolicitud" %>

<table width="100%" class="tblSecciones">
    <tr>
        <td colspan="4" class="TituloSeccion">
            Información General
        </td>
    </tr>
    <tr>
        <th style="width: 10%; text-align:left; vertical-align: top;">
            Proceso Asociado :
        </th>

        <td style="width: 2%;"></td>

        <td class="Line" style="width: 60%">
            <asp:Label ID="lblProcesoAsociado" runat="server" />
        </td>

        <td style="width: 2%;"></td>

    </tr>
    <tr>
        <th style="width: 10%; text-align:left; vertical-align: top;">
            Posible Problema ó No Conformidad :
        </th>

        <td ></td>

        <td class="Line" style="width: 60%">
            <asp:Label ID="lblDescripcionAccion" runat="server" />
        </td>

        <td ></td>

    </tr>
    <tr id="trCierreTitle" runat="server">
        <td colspan="4" class="TituloSeccion">
            Cierre
        </td>
    </tr>
    <tr id="trCierreInfo" runat="server">
        <td colspan="4" class="Line" style="padding:8px;">
            <table width="100%">
               <tr>
                    <th style="width:50%; text-align:left;" rowspan="4">
                        Según los resultados obtenidos en la ejecución e implementación de la acción correctiva/preventiva descrita en el presente documento, determine si la acción fue: *
                        <br />
                        <asp:RadioButtonList ID="rblResultadoCierreSolicitud" runat="server" RepeatLayout="Table" RepeatColumns="2" >
                            <asp:ListItem Text="Adecuada" Value="Adecuada" Selected="True" />
                            <asp:ListItem Text="Eficáz" Value="Eficáz" />
                        </asp:RadioButtonList>
                    </th>
                    <th style="width:5%" rowspan="4">                        
                    </th>
                    <th style="width:45%; text-align:left;" >
                        La no conformidad fue eliminada?
                    </th>
                </tr>
                <tr>
                    <th >
                        <asp:RadioButtonList ID="rblConformidadEliminada" runat="server" RepeatLayout="Table" RepeatColumns="2" >
                            <asp:ListItem Text="SI" Value="SI" Selected="True" />
                            <asp:ListItem Text="NO" Value="NO" />
                        </asp:RadioButtonList>
                    </th>
                </tr>
                <tr>
                    <th style="text-align:left;">
                        Observaciones:
                    </th>
                </tr>
                <tr>
                    <td class="Line" style="text-align:justify;">
                        <asp:Label ID="lblObservacionesCierre" runat="server" />                        
                    </td>
                </tr>
            </table>
        </td>            
    </tr>
</table>