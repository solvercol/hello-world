<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucCategorizarReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WucCategorizarReclamo" %>

<table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">            
    <tr>
        <td style="width:30%;" class="Etiquetas">
            Categoría&nbsp;Reclamo:
        </td>
        <td class="Separador15"></td>
        <td valign="middle" style="width:70%;" class="Line">
            <asp:DropDownList ID="ddlCategoria" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvIngeniero" 
            runat="server" 
            ControlToValidate="ddlCategoria" 
            Text="*" 
            CssClass="validator" 
            ValidationGroup="grpInput"></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td style="width:30%;" class="Etiquetas">
            Area:
        </td>
        <td class="Separador15"></td>
        <td valign="middle" style="width:70%;" class="Line">
            <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvArea" 
            runat="server" 
            ControlToValidate="ddlArea" 
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