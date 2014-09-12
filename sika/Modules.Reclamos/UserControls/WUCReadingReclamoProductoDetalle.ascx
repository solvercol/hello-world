﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCReadingReclamoProductoDetalle.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCReadingReclamoProductoDetalle" %>

<table width="100%" class="tblSecciones">
    <tr>
        <td colspan="7" class="TituloSeccion">
            Información General
        </td>
    </tr>
    <tr>
        <th style="width: 10%; text-align:left">
            Asesor :
        </th>

        <td class="Separador"></td>

        <td class="Line" style="width: 40%">
            <asp:Label ID="lblAsesor" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="width: 10%; text-align:left">
            Planta :
        </th>

        <td class="Separador"></td>

        <td class="Line" style="width: 30%">
            <asp:Label ID="lblPlanta" runat="server" />
        </td>

    </tr>    
    <tr>
        <th style="text-align:left">
            Cantidad Vendida Und :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblCantidadUnidadVendida" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Cantidad Reclamada Und :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
           <asp:Label ID="lblCantidadReclamadaUnidad" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            * Aplicado? :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblAplicado" runat="server" />
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
            Fecha de venta :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblFechaVenta" runat="server" />
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
            Reclamo Atendido por :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblAtendidoPor" runat="server" />
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
        <td colspan="7" class="TituloSeccion">
            Datos Cliente Obra
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Cliente :
        </th>

        <td class="Separador"></td>

        <td colspan="5" class="Line">
            <asp:Label ID="lblNombreCliente" runat="server" />
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
            Nombre de la Obra :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblNombreObra" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Aplicado por :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblAplicadoPor" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            Propietario de la Obra :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblPropietarioObra" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Email de quien aplica :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblEmailQuienAplica" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            Email Propietario :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
           <asp:Label ID="lblEmailPropietario" runat="server" />                           
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
                
        </th>

        <td class="Separador"></td>

        <td >
        </td>

    </tr>
    <tr>
        <td colspan="7" class="TituloSeccion">
            Estado Del Producto
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Aspecto exterior envase :
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblAspectoExteriorEnvase" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left"> 
            Aspecto del Producto :              
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblAspectoProducto" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            Descripción Aspecto Envase:
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblDescripcionAspectoEnvase" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">   
            Descripción Aspecto Producto:             
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblDescripcionAspectoProducto" runat="server" />
        </td>

    </tr>
    <tr>
        <th style="text-align:left">
            Número de Lote :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
           <asp:Label ID="lblNumeroLote" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            Muestra disponible :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblMuestraDisponible" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            
        </th>

        <td class="Separador"></td>

        <td class="Line" >
           <asp:Label ID="lblNumeroLote2" runat="server" />
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
            
        </th>

        <td class="Separador"></td>

        <td class="Line" >
           <asp:Label ID="lblNumeroLote3" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">
            
        </th>

        <td class="Separador"></td>

        <td >
            
        </td>
    </tr>
    <tr>
        <td colspan="7" class="TituloSeccion">
            Descripción del Problema
        </td>
    </tr>
    <tr>
        <td colspan="7" class="Line" style="padding:8px;">
            <asp:Label ID="txtDescripcionProblema" runat="server"  />
        </td>            
    </tr>
    <tr>
        <td colspan="7" class="TituloSeccion">
            Diagnóstico y Conclusiones Previas
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Diagnóstico :
        </th>
        <td class="Separador"></td>
        <td colspan="4" class="Line">
            <asp:Label ID="txtDiagnostico" runat="server" />
        </td>        
        <td class="Separador"></td>
    </tr>
    <tr>
        <th style="text-align:left">
            Conclusiones Previa :
        </th>
        <td class="Separador"></td>
        <td colspan="4" class="Line">
            <asp:Label ID="txtConclusionesPrevias" runat="server"/>
        </td>            
        <td class="Separador"></td>
    </tr>
    <tr>
        <td colspan="7" class="TituloSeccion">
            Solución
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            Observaciones :
        </th>
        <td class="Separador"></td>
        <td colspan="4" class="Line">
            <asp:Label ID="txtObservacionesSolucion" runat="server"  />
        </td>      
        <td class="Separador"></td>      
    </tr>
</table>