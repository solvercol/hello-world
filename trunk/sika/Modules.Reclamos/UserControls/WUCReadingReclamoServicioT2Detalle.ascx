<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCReadingReclamoServicioT2Detalle.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCReadingReclamoServicioT2Detalle" %>

<table width="100%" class="tblSecciones">
    <tr>
        <td colspan="7" class="accordionHeaderSelected">
            Información General
        </td>
    </tr>
    <tr>
        <th style="width: 10%; text-align:left">
            Categoría Reclamo :
        </th>

        <td class="Separador"></td>

        <td class="Line" style="width: 40%">
            <asp:Label ID="lblCategoriaReclamo" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="width: 10%; text-align:left">
            Area :
        </th>

        <td class="Separador"></td>

        <td class="Line" style="width: 30%">
            <asp:Label ID="lblArea" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            Asesor :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblAsesor" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Planta :                                    
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblPlanta" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            # Pedido <br />
            # Factura <br />
            # Remisión :
        </th>

        <td class="Separador"></td>

        <td class="Line" style="vertical-align:middle;" >
            <asp:Label ID="lblPedidoFacturaRemision" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            # Diario de Inventario :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblDiarioInventario" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Reclamo Atendido Por :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblAtendidoPor" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            No. Recordatorios :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblNoRecordatorios" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            Respuesta Inmediata :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblRespuestaInmediata" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Tipo de Contacto :                
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblTipoContacto" runat="server" />
        </td>

    </tr>
    <tr>
        <td colspan="7" class="accordionHeaderSelected">
            Datos Cliente
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Cliente :
        </th>

        <td class="Separador"></td>

        <td colspan="5">
            <asp:Label ID="lblCliente" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Unidad / Zona :
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblUnidadZona" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">                
        </th>

        <td class="Separador"></td>

        <td >
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Nombre Contacto :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblNombreContacto" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Email Contacto :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
           <asp:Label ID="lblEmailContacto" runat="server" />
        </td>

    </tr>  
    <tr>
        <th style="text-align:left">
            Fecha Pedido :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblFechaPedido" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Fecha del Compromiso :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblFechaCompromiso" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            Fecha Real Entrega :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblFechaRealEntrega" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Días de Incumplimiento :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblDiasIncuplimiento" runat="server" />
        </td>

    </tr>
    <tr>
        <td colspan="7" class="accordionHeaderSelected">
            Descripción del Problema
        </td>
    </tr>
    <tr>
        <td colspan="7">   
            <asp:TextBox ID="txtDescripcionProblema" runat="server" TextMode="MultiLine" Width="97%" Rows="4" Enabled="false" />                               
        </td>            
    </tr>
</table>