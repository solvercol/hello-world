<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCReadingReclamoProducto.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCReadingReclamoProducto" %>

<table width="100%" cellpadding="0" cellspacing="0" class="tblPreView">
    <tr>
        <td align="left" style="color:#360090; font-weight:bold; font-size:12pt;" colspan="2" class="TextUpperCase">
            <asp:Label ID="lblNombreProducto" runat="server" />
        </td>                           
    </tr>               
                
    <tr>
        <td  class="Noline">
            Target Market :  <asp:Label ID="lblTargetMarket" Font-Bold="true" runat="server" />
        </td>                    
    </tr>
    <tr>
        <td class="Noline">
            Campo de Aplicación :  <asp:Label ID="lblCampoAplicacion" Font-Bold="true" runat="server" />                     
        </td>
    </tr>
    <tr>
        <td class="Noline">
            Sub-Campo de Aplicación :  <asp:Label ID="lblSubCampoAplicacion" Font-Bold="true" runat="server" />                     
        </td>
    </tr>
    <tr>
        <td class="Noline">
            Presentación :  <asp:Label ID="lblPresentacion" Font-Bold="true" runat="server" />                     
        </td>
    </tr>
</table>