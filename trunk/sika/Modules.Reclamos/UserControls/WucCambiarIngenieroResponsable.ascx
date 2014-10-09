<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucCambiarIngenieroResponsable.ascx.cs" Inherits="Modules.Reclamos.UserControls.WucCambiarIngenieroResponsable" %>

<table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">    
    <tr>
        <td colspan="3" class="vaidator" style=" padding-left:10px; padding-right:10px;">
            ¿Confirma que desea continuar con el  proceso?
        </td>
    </tr>          
    <tr>
        <td style="width:50%;" class="Etiquetas">
            Ingeniero Responsable:
        </td>
        <td class="Separador15"></td>
        <td valign="middle" style="width:70%;" class="Line">
            <asp:DropDownList ID="ddlIngeniero" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvIngeniero" 
            runat="server" 
            ControlToValidate="ddlIngeniero" 
            Text="*" 
            CssClass="validator" 
            ValidationGroup="grpInput"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
         <td style="width:50%;" class="Etiquetas">
            Comentarios:
        </td>
        <td class="Separador15"></td>
        <td valign="middle" style="width:70%;" class="Line">
            <asp:TextBox ID="txtComentariosCambioIngeniero" runat="server" TextMode="MultiLine" Height="35px" Width="90%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvComentariosCambioIng" 
            runat="server" 
            ControlToValidate="txtComentariosCambioIngeniero" 
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
