<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCReadingReclamoServicioT5Detalle.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCReadingReclamoServicioT5Detalle" %>

<table width="100%" class="tblSecciones">
    <tr>
        <td colspan="7" class="TituloSeccion">
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
            Planta :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblPlanta" runat="server" />
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
            Reclamo Atendido Por :
        </th>

        <td class="Separador"></td>

        <td class="Line" >
            <asp:Label ID="lblAtendidoPor" runat="server" />
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
            Nombre de quien plantea el reclamo :
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblQuienReclama" runat="server" />
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
            Procedimiento interno afectado :
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblProcedimientoAfectado" runat="server" />
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
            Area que incumple el procedimiento :
        </th>

        <td class="Separador"></td>

        <td class="Line">
            <asp:Label ID="lblAreaIncumple" runat="server" />
        </td>

        <td class="Separador"></td>

        <th style="text-align:left">                
        </th>

        <td class="Separador"></td>

        <td >
        </td>

    </tr>
    <tr>        
        <td colspan="6" class="TituloSeccion">
            Descripción del Problema
        </td>
    </tr>
    <tr>
        <td colspan="7" class="Line" style="padding:8px;">
            <asp:Label ID="txtDescripcionProblema" runat="server" />
        </td>            
    </tr>
</table>