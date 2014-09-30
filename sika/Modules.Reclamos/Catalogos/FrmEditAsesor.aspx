<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditAsesor.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmEditAsesor" %>
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
						    <th style="text-align:left;vertical-align:top">Asesor:</th>
						    <td align="left"><asp:Literal runat="server" ID="txtAsesor"/></td>
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
                                            Width="50%" AutoPostBack="True">
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
                                            Width="50%" AutoPostBack="True">
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
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Jefe(s) Inmediato(s):</th>
						    <td align="left">
                                <asp:Panel id="PanelUsuariosCopia" runat="server" >
                                    <table class="tblSecciones" width="100%">
                                        <tr>
                                            <td>
                                                <ig:WebDropDown ID="wddJefes" CurrentValue=""
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
						    </td>
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


