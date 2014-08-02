<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditUsuarios.aspx.cs" Inherits="ASP.NETCLIENTE.Pages.Globals.FrmEditUsuarios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">

    function MantenSesion() {
        var CONTROLADOR = "../httpHandler/MantenerSession.ashx";
        var head = document.getElementsByTagName('head').item(0);
        script = document.createElement('script');
        script.src = CONTROLADOR;
        script.setAttribute('type', 'text/javascript');
        script.defer = true;
        head.appendChild(script);
    }
    setInterval('MantenSesion()', 120000);
   
</script>
<div style="text-align:left; vertical-align:middle;"  class="ToolBar">
<asp:UpdatePanel ID="upMenuBar" runat="server">
    <ContentTemplate>
        <asp:button id="btnBack" CausesValidation="false" ToolTip="Regresar formulario anterior." runat="server" text="Regresar" onclick="BtnBackClick"></asp:button>
	    <asp:button id="btnSave" runat="server" text="Guardar" onclick="BtnSaveClick" ToolTip="Guardar registro."></asp:button>
        <asp:button id="btnDelete" runat="server" text="Eliminar" CausesValidation="false" ToolTip="Eliminar registro."  onclick="BtnDeleteClick"></asp:button>
    </ContentTemplate>
</asp:UpdatePanel>
</div>

<div class="group">
<asp:UpdatePanel ID="upGeneral" runat="server">
    <ContentTemplate>
    <table width="100%">
   
    <tr>
        <td style="width:2%" class="validator">
            *
        </td>
        <td style="width:20%">
            Usuario
        </td>
        <td style="width:30%">
            <asp:TextBox ID="txtNombreUsuario" runat="server" Width="250"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator 
            ID="rfvUserName" 
            runat="server" 
            Text="**Campo Requerido" 
            ControlToValidate="txtNombreUsuario"
            Display="Dynamic"
            CssClass="validator">
            </asp:RequiredFieldValidator>
        </td>    
    </tr>

    <tr>
        <td style="width:2%" class="validator">
            *
        </td>
        <td>
            Nombres
        </td>
        <td>
            <asp:TextBox ID="txtNombres" runat="server" Width="250"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator 
            ID="rfvtxtNombres" 
            runat="server" 
            Text="**Campo Requerido" 
            ControlToValidate="txtNombres"
            Display="Dynamic"
            CssClass="validator">
            </asp:RequiredFieldValidator>
        </td>    
    </tr>

    <tr>
        <td style="width:2%" class="validator">
            *
        </td>
        <td>
            Apellidos
        </td>
        <td>
            <asp:TextBox ID="txtApellidos" runat="server" Width="250"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator 
            ID="rfvtxtApellidos" 
            runat="server" 
            Text="**Campo Requerido" 
            ControlToValidate="txtApellidos"
            Display="Dynamic"
            CssClass="validator">
            </asp:RequiredFieldValidator>
        </td>    
    </tr>

    <tr>
        <td style="width:2%" class="validator">*</td>
        <td>
            Email
        </td>
        <td>
            <asp:TextBox ID="txtMail" runat="server" Width="250"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator 
            ID="rfvtxtMail" 
            runat="server" 
            Text="**Campo Requerido" 
            ControlToValidate="txtMail"
            Display="Dynamic"
            CssClass="validator">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator 
            ID="revMail" 
            runat="server" 
            CssClass="validator"
            ControlToValidate="txtMail"
            Text="**Formato incorrecto."
            Display="Dynamic" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
            </asp:RegularExpressionValidator>
        </td>    
    </tr>

    <tr>
        <td style="width:2%"></td>
        <td>
            Activo
        </td>
        <td>
            <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
        </td>
        <td></td>    
    </tr>
    
    <tr>
        <td colspan="4">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width:4%; text-align:center; vertical-align:middle;" >
                    <asp:Image ID="imgAdvertencia" runat="server" ImageUrl="~/Resources/Images/Info1.gif" />
                </td>
                <td>
                     <p class="validator"> Los Campos marcados con (*) son obligatorios.</p>
                </td>
            </tr>
          </table>
        </td>
    </tr>
</table>
    <br />
    <table id="roles" class="tbl">
 <asp:repeater id="rptRoles" runat="server">
	<headertemplate>
		<tr>
			<th>Roles disponibles</th>
			<th>
                
			</th>
		</tr>
	</headertemplate>
	<itemtemplate>
		<tr>
			<td><%# DataBinder.Eval(Container.DataItem, "NombreRol")%></td>
			<td style="text-align:center">
				<asp:checkbox id="chkRole" runat="server"></asp:checkbox>
			</td>
		</tr>
	</itemtemplate>
</asp:repeater>
</table>
    			
    </ContentTemplate>
</asp:UpdatePanel>

</div>
</asp:Content>