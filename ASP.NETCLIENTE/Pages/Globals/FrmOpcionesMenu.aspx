<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmOpcionesMenu.aspx.cs" Inherits="ASP.NETCLIENTE.Pages.Globals.FrmOpcionesMenu" %>

<%@ Register Assembly="Infragistics4.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
	

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">

//    function MantenSesion() {
//        var CONTROLADOR = "../httpHandler/MantenerSession.ashx";
//        var head = document.getElementsByTagName('head').item(0);
//        script = document.createElement('script');
//        script.src = CONTROLADOR;
//        script.setAttribute('type', 'text/javascript');
//        script.defer = true;
//        head.appendChild(script);
//    }
//    setInterval('MantenSesion()', 120000);
   
</script>
	<asp:UpdatePanel ID="upToolBar" runat="server">
<ContentTemplate>
<div style="text-align:left; vertical-align:middle;"  class="ToolBar">
   <%--<asp:Button ID="btnNew" OnClick="BtnNewClick" CausesValidation="false" ToolTip="Nuevo registro." runat="server" Text="Nuevo Nodo"/>--%>
   <asp:button id="btnSave" runat="server" Enabled="false" OnClick="BtnSaveClick" ToolTip="Guardar registro." text="Guardar"></asp:button>
   <asp:button id="btnDelete" runat="server" Enabled="false" OnClick="BtnDeleteClick" ToolTip="Eliminar registro." causesvalidation="False" text="Eliminar"></asp:button>
</div>
</ContentTemplate>
</asp:UpdatePanel>

<table width="100%" cellpadding="0" cellspacing="0" style=" padding-top:5px;">
	
	<tr>
	   
		<td style="width:30%" valign="top">
		<fieldset>
			<asp:UpdatePanel ID="upOpciones" runat="server">
			 <ContentTemplate>
				<table width="100%">
				<tr>
					<td valign="top">
					
						<%--<ignav:UltraWebTree 
						ID="uwtOpcionesMenu" 
						Width="95%"
						Height="400px"
						AllowDrag="true"
						
						AllowDrop="true"
						DataKeyOnClient="true"
						runat="server" DefaultImage="" 
						HoverClass="" Indentation="20" LoadOnDemand="Manual" 
						onnodeclicked="UwtOpcionesMenuNodeClicked">
						<NodePaddings Left="2px" Top="5px" />
						<SelectedNodeStyle BackColor="#02A3E9" ForeColor="White" Font-Bold="true" />
						<HoverNodeStyle Font-Bold="true" />
						</ignav:UltraWebTree>
						--%>
					</td>
				</tr>
			   
			</table>  
			</ContentTemplate>
		  </asp:UpdatePanel>  
		</fieldset>  
		
		</td>
		<td style="width:1%">
		</td>
		<td style="width:50%" valign="top">
		
		<asp:UpdatePanel ID="upDetalle" runat="server">
			<ContentTemplate>
			 <div style="padding-top:5px;">
			 <table id="userdetails" width="100%" class="tbl">					   
						<tr>
							<td align="left" style="width:20%">Descripción</td>
							<td align="left" style="width:80%">
								<asp:textbox id="txtDescripcion" ReadOnly="false" runat="server" width="200px">
								</asp:textbox>
								<asp:requiredfieldvalidator id="rfvDescripcion" 
								runat="server" 
								errormessage="Descripción es requerida" 
								cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripcion">
								</asp:requiredfieldvalidator>
						</td>
						</tr>
						<tr>
							<td align="left">Url</td>
							<td align="left">
								<asp:textbox 
								id="txtUrl" 
								runat="server" 
								ReadOnly="false"
								width="95%"></asp:textbox>
							</td>
						</tr>
						<tr>
							<td align="left">Posición</td>
							<td align="left">
								<asp:textbox 
								id="txtPosicion" 
								runat="server" 
								ReadOnly="false"
								width="50px"></asp:textbox>
								<asp:requiredfieldvalidator 
								id="rfvposicion" 
								runat="server" 
								controltovalidate="txtPosicion" 
								enableclientscript="true" 
								display="Dynamic" 
								cssclass="validator" 
								errormessage="Posición es requerida">
								</asp:requiredfieldvalidator>    
							</td>
						</tr>
						<tr>
							<td align="left">Icono</td>
							<td align="left">
							<asp:textbox 
							id="txtIcono" 
							runat="server" 
							ReadOnly="false"
							width="95%"></asp:textbox>
						</td>
						</tr>
						 <tr>
							<td align="left">Ver en Menu</td>
							<td align="left">
							<asp:checkbox 
							id="chkShowInNavigarion" 
							Enabled="true"
							runat="server"></asp:checkbox></td>
						</tr>
						<tr>
							<td align="left">Activo</td>
							<td align="left">
							<asp:checkbox 
							id="chkActive" 
							Enabled="true"
							runat="server"></asp:checkbox></td>
						</tr>
					</table>   
			</div>		
			
			<br /><br />
			<div>
			
					<table id="roles" class="tbl">
						<asp:repeater id="rptRoles" runat="server">
							<headertemplate>
								<tr>
									<th>Rol</th>
									<th>
									   Fecha Creación
									</th>
									<th>
									</th>
								</tr>
							</headertemplate>
							<itemtemplate>
								<tr>
									<td align="left"><%# DataBinder.Eval(Container.DataItem, "NombreRol")%></td>
									<td align="center"><%# DataBinder.Eval(Container.DataItem, "CreateOn")%></td>
									<td style="text-align:center">
										<asp:checkbox id="chkRole" runat="server"></asp:checkbox>
									</td>
								</tr>
							</itemtemplate>
						</asp:repeater>
					</table>
			</div>	
			 
				
			 </ContentTemplate>
			 
		</asp:UpdatePanel>
		</td>
	</tr>
   
</table>
</asp:Content>