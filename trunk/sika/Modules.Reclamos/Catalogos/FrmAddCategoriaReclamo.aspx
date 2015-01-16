<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAddCategoriaReclamo.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmAddCategoriaReclamo" %>
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
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Tipo Reclamo:</th>
						    <td align="left" class="bordeTabla">
                            <asp:RadioButtonList ID="wddReclamo" runat="server" RepeatDirection="Horizontal" CssClass="radioButtonList" onselectedindexchanged="wddReclamo_SelectedIndexChanged" AutoPostBack="true" ></asp:RadioButtonList>
						        <asp:requiredfieldvalidator id="RFVReclamo" 
						        runat="server" 
						        errormessage="El campo [Tipo Reclamo] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="wddReclamo">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    <tr>
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
						    <th style="text-align:left;vertical-align:top">SubCategoría:</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtSubcategoria" runat="server" width="400px" MaxLength="512">
						        </asp:textbox>
				<%--		        <asp:requiredfieldvalidator id="rfvSubcategoria" 
						        runat="server" 
						        errormessage="El campo [SubCategoria] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtSubcategoria">
								</asp:requiredfieldvalidator>--%>
                                <br />
                                <div class="mensajeMultivalor">
                                    <asp:Literal ID="litMensajeMultivalor" runat="server"></asp:Literal>
                                </div>
						    </td>
                            
					    </tr>
                         <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtDescripcion" runat="server" TextMode="MultiLine" width="400px" MaxLength="512">
						        </asp:textbox>
<%--						        <asp:requiredfieldvalidator id="rfvDescripcion" 
						        runat="server" 
						        errormessage="El campo [Descripción] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripcion">
								</asp:requiredfieldvalidator>--%>
						    </td>
					    </tr>
                        <tr id="trArea" runat="server" visible="false">
						    <th style="text-align:left;vertical-align:top">Área:</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtArea" runat="server" width="400px" MaxLength="512">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="RFVArea" 
						        runat="server" 
						        errormessage="El campo [Área] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtArea">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr id="trResponsable" runat="server" visible="false">
						    <th style="text-align:left;vertical-align:top">Ingeniero Responsable:</th>
						    <td align="left" class="bordeTabla">
                                <asp:DropDownList ID="wddResponsables" class="chzn-select" runat="server" Width="50%" />
						        <asp:requiredfieldvalidator id="rfvesponsables" 
						        runat="server" 
						        errormessage="El campo [Responsable] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="wddResponsables">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr id="trGrupoInformacion" runat="server" visible="false">
						    <th style="text-align:left;vertical-align:top">Grupo Información:</th>
						    <td align="left" class="bordeTabla">
                              <asp:DropDownList ID="WddGrupoInformacion" class="chzn-select" runat="server" Width="50%" >
                                  <asp:ListItem Selected="False" Text="1" Value="1"/>
                                  <asp:ListItem Selected="False" Text="2" Value="2"/>
                                  <asp:ListItem Selected="False" Text="3" Value="3"/>
                                  <asp:ListItem Selected="False" Text="4" Value="4"/>
                                  <asp:ListItem Selected="False" Text="5" Value="5"/>
                              </asp:DropDownList>
						        <asp:requiredfieldvalidator id="rfvGrupoInformacion" 
						        runat="server" 
						        errormessage="El campo [Grupo Información] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="WddGrupoInformacion">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left" class="bordeTabla">
                            <asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>
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
                Creado por:&nbsp;<asp:Label ID="lblCreateBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblCreateOn" runat="server"/>
            </td>
        </tr>
    </table>
</asp:Content>