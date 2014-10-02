<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAddUnidadZona.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmAddUnidadZona" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register src="../UserControls/WUCAddUnidad.ascx" tagname="WUCAddUnidad" tagprefix="uc1" %>
<%@ Register src="../UserControls/WUCAddZona.ascx" tagname="WUCAddZona" tagprefix="uc2" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <script type="text/javascript">
     function ItemSeleccionado(sender, eventArgs) {
         var fuente = sender.get_element().name.toLowerCase();
         var tipo = "";
         if (fuente.indexOf("descripcion") != -1) {
             tipo = "descripcion";
         }
         $.ajax({
             type: "POST",
             url: "FrmAddUnidadZona.aspx/AsignarZona",
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: "{ Tipo: '" + tipo + "', Id: " + eventArgs.get_value() + "}",
             success: function (response) {
             },
             error: function (xmlRequest) {
                 alert(xmlRequest.status + ' \n\r ' +
                          xmlRequest.statusText + '\n\r' +
                          xmlRequest.responseText);
             }
         });
     }

     function DespuesDeDigitarZona(categoria) {
         var contenido = "";
         if (categoria == "descripcion") {
             contenido = document.getElementById('<%=txtDescripcion.ClientID%>').value;
         }
         $.ajax({
             type: "POST",
             url: "FrmAddUnidadZona.aspx/DespuesDeDigitarCategoria",
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: "{ Categoria: '" + categoria + "', Contenido: '" + contenido + "'}",
             success: function (response) {

             },
             error: function (xmlRequest) {
                 alert(xmlRequest.status + ' \n\r ' +
                          xmlRequest.statusText + '\n\r' +
                          xmlRequest.responseText);
             }
         });
     }
    </script>
<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar" causesvalidation="False"></asp:button>
</div>
<table width="100%" class="tblSecciones">
        <tr>
          
            <td>
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left" style="width:310px;">
						        <asp:textbox id="txtDescripcion" runat="server" TextMode="MultiLine" width="300px" MaxLength="512">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvDescripcion" 
						        runat="server" 
						        errormessage="El campo [Descripción] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripcion">
								</asp:requiredfieldvalidator>
						    </td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Unidad:</th>
						    <td align="left">
						        <ig:WebDropDown ID="wddUnidad" CurrentValue=""
                                            runat="server" 
                                            EnableMultipleSelection="false"
                                            MultipleSelectionType="Checkbox" 
                                            DisplayMode="DropDown"
                                            EnableClosingDropDownOnSelect="false"
                                            StyleSetName="Claymation"
                                            DropDownContainerWidth="300px"
                                            DropDownContainerHeight="220px"
                                            Width="90%" AutoPostBack="True">
                                </ig:WebDropDown>  
						        <asp:requiredfieldvalidator id="rfvUnidad" 
						        runat="server" 
						        errormessage="El campo [Unidad] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="wddUnidad">
								</asp:requiredfieldvalidator>
                                </td>
						        <td align="left">
                                    <uc1:WUCAddUnidad ID="WUCAddUnidad1" runat="server" />
                                </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Zona:</th>
						    <td align="left">
						        <ig:WebDropDown ID="wddZona" CurrentValue=""
                                            runat="server" 
                                            EnableMultipleSelection="false"
                                            MultipleSelectionType="Checkbox" 
                                            DisplayMode="DropDown"
                                            EnableClosingDropDownOnSelect="false"
                                            StyleSetName="Claymation"
                                            DropDownContainerWidth="300px"
                                            DropDownContainerHeight="220px"
                                            Width="90%" AutoPostBack="True">
                                </ig:WebDropDown>
						        <asp:requiredfieldvalidator id="rfvZona" 
						        runat="server" 
						        errormessage="El campo [Zona] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="wddZona">
								</asp:requiredfieldvalidator>
                                </td>
						        <td align="left">
                                 <uc2:WUCAddZona ID="WUCAddZona1" runat="server" />
                                </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Gerente:</th>
						    <td align="left">
						        <ig:WebDropDown ID="wddGerente" CurrentValue=""
                                            runat="server" 
                                            EnableMultipleSelection="false"
                                            MultipleSelectionType="Checkbox" 
                                            DisplayMode="DropDown"
                                            EnableClosingDropDownOnSelect="false"
                                            StyleSetName="Claymation"
                                            DropDownContainerWidth="300px"
                                            DropDownContainerHeight="220px"
                                            Width="90%" AutoPostBack="True">
                                </ig:WebDropDown>
						        <asp:requiredfieldvalidator id="rfvgerente" 
						        runat="server" 
						        errormessage="El campo [Gerente] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="wddGerente">
								</asp:requiredfieldvalidator>
						    </td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
                         <th style="text-align:left;vertical-align:top">Tarifa Flete:</th>
						    <td align="left">
						        <asp:textbox id="txtTarifa" runat="server"  width="300px" MaxLength="24">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvTarifa" 
						        runat="server" 
						        errormessage="El campo [Tarifa] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtTarifa">
								</asp:requiredfieldvalidator>
                                <asp:RegularExpressionValidator ID="revTarifa" runat="server" ErrorMessage ="[Tarifa]: Valor invalido"
                                cssclas="validator"
                                display="Dynamic"
                                enableclientscript="true"
                                controltovalidate="txtTarifa" ValidationExpression="^\d+(\,\d{1,4})?$" ></asp:RegularExpressionValidator>
						    </td>
                            <td>&nbsp;</td>
                        </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
                            <td>&nbsp;</td>
					    </tr>	
                        <tr>
						    <th style="text-align:left;vertical-align:top">Creado por:</th>
						    <td align="left"><asp:Label ID="lblCreateBy" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha creación:</th>
						    <td align="left"><asp:Label ID="lblCreateOn" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>
					    <tr>
                            <td align="left">&nbsp;</td>
                            <td align="left">&nbsp;</td>
					    </tr>
                    </table>
            </td>
        </tr>
    
    </table>
</asp:Content>

