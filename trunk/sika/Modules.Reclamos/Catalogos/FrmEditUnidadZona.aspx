<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FrmEditUnidadZona.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmEditUnidadZona" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"  causesvalidation="False"></asp:button>
    <asp:button id="btnEliminar" OnClientClick="return confirm('¿Esta seguro?');" Visible="false" runat="server" OnClick="BtnDeleteClick" causesvalidation="False" text="Eliminar"></asp:button>
</div>
<asp:UpdatePanel ID="test" runat="server">
<ContentTemplate>

<table width="100%" class="tblSecciones">
        <tr>
          
            <td>
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Unidad:</th>
						    <td align="left" class="bordeTabla">
						        <asp:Literal runat="server" ID="txtUnidad"></asp:Literal>
						    </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Zona:</th>
						    <td align="left" class="bordeTabla">
                             <asp:Literal runat="server" ID="txtZona"></asp:Literal>
						    </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtDescripcion" runat="server" TextMode="MultiLine" width="300px" MaxLength="512">
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
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Gerente:</th>
						    <td align="left" class="bordeTabla">
                                <asp:Literal runat="server" ID="txtGerente"></asp:Literal>
						    </td>
                            <td>&nbsp;</td>
					    </tr>
                                <tr>
                         <th style="text-align:left;vertical-align:top">Tarifa Flete:</th>
						    <td align="left" class="bordeTabla">
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
						    <td align="left" class="bordeTabla">
                            <asp:checkbox id="chkActive" runat="server"></asp:checkbox></td>
                            <td>&nbsp;</td>
					    </tr>	
                 <%--       <tr>
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
						    <th style="text-align:left;vertical-align:top">Modificado por:</th>
						    <td align="left"><asp:Label ID="lblModifiedBy" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha modificación:</th>
						    <td align="left"><asp:Label ID="lblModifiedOn" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>--%>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
                            <td>&nbsp;</td>
					    </tr>
				    </table>
            </td>
        </tr>
    
    </table>
</ContentTemplate>
</asp:UpdatePanel>


<asp:UpdatePanel ID="UPFormUnidad" runat="server">
 <ContentTemplate>
<asp:Panel ID="pnlUnidades"  runat="server" CssClass="popup_Container" Width="500" Height="200" style="display:none;">
 <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Adicionar Unidad
        </div>
        <div class="TitlebarRight" id="divCloseUnidad">
        </div>
    </div>
<div class="popup_Body"> 
 <div style="padding:3px; text-align:right;">
	<asp:button id="btnSaveUnidad" runat="server" OnClick="BtnSaveUnidadClick" text="Guardar" CausesValidation="false"></asp:button>
</div>
<table width="100%" class="tblSecciones">
        <tr>
            <td>
				    <table id="Table1" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
					    <tr>
                         <th style="text-align:left;vertical-align:top">Nombre:</th>
						    <td align="left">
						        <asp:textbox id="txtNombreUnidad" runat="server" width="400px" MaxLength="512">
						        </asp:textbox>
                                <ajaxToolkit:AutoCompleteExtender ID="acNombre" runat="server"
                                enabled="True" minimumprefixlength="2" enablecaching="true"
                                ServicePath="WSScripts.asmx" ServiceMethod="buscarUnidad"
                                TargetControlID="txtNombreUnidad" completionsetcount="10"
                                completioninterval="200" CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"> 
                                </ajaxToolkit:AutoCompleteExtender>
						        <asp:requiredfieldvalidator id="rfvNombreUnidad" 
						        runat="server" 
						        errormessage="El campo [Nombre] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNombreUnidad">
								</asp:requiredfieldvalidator>
                            </td>
					    </tr>	    
                        <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chbActiveUnidad" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>	
                        <tr>
						    <th style="text-align:left;vertical-align:top">Creado por:</th>
						    <td align="left"><asp:Label ID="lblCreateByUnidad" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha creación:</th>
						    <td align="left"><asp:Label ID="lblCreateOnUnidad" runat="server"></asp:Label></td>
					    </tr>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
					    </tr>
				    </table>
                </td>
        </tr>
    
</table>

</div>
</asp:Panel>

<asp:Button ID="btnTargetControlU" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender
ID="mpeUnidades" 
runat="server" 
TargetControlID="btnTargetControlU" 
PopupControlID="pnlUnidades"
BackgroundCssClass="ModalPopupBG" DropShadow="true" 
cancelcontrolid="divCloseUnidad"
> 
</ajaxToolkit:ModalPopupExtender>
</ContentTemplate>
</asp:UpdatePanel>
 <asp:UpdatePanel ID="upFormZonas" runat="server">
    <ContentTemplate> 
<asp:Panel ID="PanelZonas"  runat="server" CssClass="popup_Container" Width="500" Height="200" style="display:none;">
 <div class="popup_Titlebar" id="Div1">
        <div class="TitlebarLeft">
            Adicionar Zona
        </div>
        <div class="TitlebarRight" id="divCloseZona">
        </div>
    </div>
<div class="popup_Body">  
<div style="padding:3px; text-align:right;">
	<asp:button id="btnSaveZona" runat="server" OnClick="BtnSaveZonaClick" text="Guardar" CausesValidation="false"></asp:button>
</div>
<table width="100%" class="tblSecciones">
        <tr>
            <td>
				    <table id="Table2" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
			            <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left">
						        <asp:textbox id="txtNombreZona" runat="server" TextMode="SingleLine" width="400px" MaxLength="512">
						        </asp:textbox>
                                <ajaxToolkit:AutoCompleteExtender ID="acDescripcion" runat="server"
                                enabled="True" minimumprefixlength="2" enablecaching="true"
                                ServicePath="WSScripts.asmx" ServiceMethod="buscarZona"
                                TargetControlID="txtNombreZona" completionsetcount="10"
                                completioninterval="200" CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem" OnClientItemSelected="ItemSeleccionado"> 
                                </ajaxToolkit:AutoCompleteExtender>
						        <asp:requiredfieldvalidator id="rfvNombreZona"
						        runat="server" 
						        errormessage="El campo [Descripción zona] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNombreZona">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        	    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chbActiveZona" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>	
                        <tr>
						    <th style="text-align:left;vertical-align:top">Creado por:</th>
						    <td align="left"><asp:Label ID="lblCreateByZona" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha creación:</th>
						    <td align="left"><asp:Label ID="lblCreateOnZona" runat="server"></asp:Label></td>
					    </tr>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
					    </tr>
				    </table>
                </td>
        </tr>
    
</table>

</div>

</asp:Panel>

<asp:Button ID="btnTargetControlZ" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender
ID="mpeZonas" 
runat="server" 
TargetControlID="btnTargetControlZ" 
PopupControlID="PanelZonas"
BackgroundCssClass="ModalPopupBG" DropShadow="true" 
cancelcontrolid="divCloseZona"
> 
</ajaxToolkit:ModalPopupExtender>

</ContentTemplate>
</asp:UpdatePanel>


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