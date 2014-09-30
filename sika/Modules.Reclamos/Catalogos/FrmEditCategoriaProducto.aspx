<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditCategoriaProducto.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmEditCategoriaProducto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"></asp:button>
</div>
<table width="100%" class="tblSecciones">
        <tr>
          
            <td>
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Nombre:</th>
						    <td align="left">
						        <asp:textbox id="txtNombre" runat="server" width="400px" MaxLength="512">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvNombre" 
						        runat="server" 
						        errormessage="El campo [Nombre] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNombre">
								</asp:requiredfieldvalidator>
                            </td>
					    </tr>
                         <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left">
						        <asp:textbox id="txtDescripcion" runat="server" TextMode="MultiLine" width="400px" MaxLength="512">
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
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Responsable:</th>
						    <td align="left">
                                <asp:Panel id="PanelUsuariosCopia" runat="server" >
                                    <table class="tblSecciones" width="100%">
                                        <tr>
                                            <td>
                                                <ig:WebDropDown ID="wddResponsables" CurrentValue=""
                                                            runat="server" 
                                                            EnableMultipleSelection="false"
                                                            MultipleSelectionType="Checkbox" 
                                                            DisplayMode="DropDown"
                                                            EnableClosingDropDownOnSelect="false"
                                                            StyleSetName="Claymation"
                                                            DropDownContainerWidth="300px"
                                                            DropDownContainerHeight="220px"
                                                            Width="50%" AutoPostBack="True">
                                                </ig:WebDropDown>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAddCopia" runat="server" Text="Agregar" OnClick="BtnAddUsuarioCopia_Click" CausesValidation="false"  />
                                                <asp:Button ID="btnRemoveCopia" runat="server" Text="Eliminar" OnClick="BtnRemoveUsuarioCopia_Click" CausesValidation="false"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstUsuariosCopia" runat="server" SelectionMode="Single" Width="300px" Height="100%" />
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>

                                </asp:Panel>
			<%--			        <asp:requiredfieldvalidator id="rfvesponsables" 
						        runat="server" 
						        errormessage="El campo [Ingeniero Responsable] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="lstUsuariosCopia">
								</asp:requiredfieldvalidator>--%>
						    </td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>	
                        <tr>
						    <th style="text-align:left;vertical-align:top">Creado por:</th>
						    <td align="left"><asp:Label ID="lblCreateBy" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha creación:</th>
						    <td align="left"><asp:Label ID="lblCreateOn" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Modificado por:</th>
						    <td align="left"><asp:Label ID="lblModifiedBy" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha modificación:</th>
						    <td align="left"><asp:Label ID="lblModifiedOn" runat="server"></asp:Label></td>
					    </tr>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
					    </tr>
				    </table>
            </td>
        </tr>
    
    </table>
</asp:Content>

