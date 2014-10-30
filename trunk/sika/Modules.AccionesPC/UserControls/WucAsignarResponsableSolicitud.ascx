<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucAsignarResponsableSolicitud.ascx.cs" Inherits="Modules.AccionesPC.UserControls.WucAsignarResponsableSolicitud" %>

<table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">            
    <tr>
        <td style="width:50%;" class="Etiquetas">
            Responsable:
        </td>
        <td class="Separador15"></td>
        <td valign="middle" style="width:70%;" class="Line">
            <asp:DropDownList ID="ddlResponsable" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvResponsable" 
            runat="server" 
            ControlToValidate="ddlResponsable" 
            Text="*" 
            CssClass="validator" 
            ValidationGroup="grpInput"></asp:RequiredFieldValidator>
        </td>
    </tr>
   
    <tr>
            <td colspan="3" class="validator">
                <asp:Label ID="litError" runat="server" Visible="false"></asp:Label>
            </td>
    </tr>  
    <tr>
          <td colspan="3" style=" height:10px;"></td>
    </tr>                         
 </table>