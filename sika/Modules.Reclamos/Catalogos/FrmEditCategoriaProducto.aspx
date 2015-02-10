<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FrmEditCategoriaProducto.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmEditCategoriaProducto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
                            <td>&nbsp;</td>
					    </tr>
					    <tr>
                            <td>*</td>
						    <th style="text-align:left;vertical-align:top">Categoría:</th>
						    <td align="left" class="bordeTabla">
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
                             <td>&nbsp;</td>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtDescripcion" runat="server" TextMode="MultiLine" width="400px" MaxLength="512">
						        </asp:textbox>
						    </td>
					    </tr>
                        <tr>
                            <td style="text-align:left;vertical-align:top">*</td>
						    <th style="text-align:left;vertical-align:top">Ingenieros Responsables:</th>
						    <td align="left" class="bordeTabla">
                                <asp:Panel id="PanelUsuariosCopia" runat="server" >
                                    <table class="tblSecciones" width="100%">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="wddResponsables" class="chzn-select" runat="server" Width="50%" />
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
                                                 <asp:requiredfieldvalidator 
                                                    controltovalidate="lstUsuariosCopia" 
                                                    cssclass="validator" 
                                                    display="Dynamic" 
                                                    enableclientscript="true" 
                                                    errormessage="El campo [Ingenieros Responsables] es requerido!!." 
                                                    id="rfvResponsables" 
                                                    runat="server"
                                                    ValidateEmptyText="true">
								                </asp:requiredfieldvalidator>
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
                             <td>&nbsp;</td>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left" class="bordeTabla">
                            <asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>	
                      <%--  <tr>
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
					    </tr>--%>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
					    </tr>
				    </table>
            </td>
        </tr>
       <script type="text/javascript">

           $(".chzn-select").chosen({ allow_single_deselect: true });

           $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
    <table width="100%">
        <tr >
            <td  style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0; font-size:8pt; color:#808080;">
                Creado por:&nbsp;<asp:Label ID="lblCreateBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblCreateOn" runat="server"/>&nbsp;&#44;&nbsp;Modificado por:&nbsp;<asp:Label ID="lblModifiedBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblModifiedOn" runat="server"/>
            </td>
        </tr>
    </table>
</asp:Content>
