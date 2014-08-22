<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCReadingReclamoServicio.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCReadingReclamoServicio" %>

<table width="100%" cellpadding="0" cellspacing="0" class="tblPreView">
    <tr>
        <td align="left" style="color:#360090; font-weight:bold; font-size:12pt;" colspan="2" class="TextUpperCase">
            <asp:Label ID="lblNombreCategoria" runat="server" />
        </td>                           
    </tr>               
                
    <tr>
        <td  class="Noline">
            Target Market :  <asp:Label ID="lblArea" Font-Bold="true" runat="server" />
        </td>                    
    </tr>   
</table>