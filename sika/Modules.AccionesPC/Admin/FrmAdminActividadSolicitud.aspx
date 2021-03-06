﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminActividadSolicitud.aspx.cs" Inherits="Modules.AccionesPC.Admin.FrmAdminActividadSolicitud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">
    var divModal = 'DivModal';

    function ShowSplashModalLoading() {
        var adiv = $get(divModal);
        adiv.style.visibility = 'visible';
    }
</script>

<asp:UpdatePanel ID="upGeneral" runat="server">
    <ContentTemplate>
    
        <div id="DivModal">
            <div id="VentanaMensaje">
                <div id="Msg">
                    <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
                </div>
            </div>
        </div>
        <div style="padding:3px; text-align:right;">
            <asp:Button ID="btnRegresar" runat="server" OnClick="BtnRegresarClick" 
                                Text="Regresar" />
            <asp:Button ID="btnEdit" runat="server" OnClick="BtnEditActividadClick" 
                OnClientClick="return ShowSplashModalLoading();" Text="Editar" />
            <asp:Button ID="btnCancel" runat="server" OnClick="BtnCancelActividadClick" 
                OnClientClick="return ShowSplashModalLoading();" Text="Salir" Visible="false" />
            <asp:Button ID="btnSave" runat="server" OnClick="BtnSaveActividadClick" 
                OnClientClick="return ShowSplashModalLoading();" Text="Guardar" 
                Visible="false" />
            <asp:Button ID="btnSaveRealizada" runat="server" 
                OnClick="BtnSaveRealizadaClick" 
                OnClientClick="return ShowSplashModalLoading();" Text="Marcar Realizada" 
                Visible="false" />
            <asp:Button ID="btnCancelActividad" runat="server" 
                OnClick="BtnCancelarActividadClick" 
                OnClientClick="return ShowSplashModalLoading();" Text="Cancelar" 
                Visible="false" />        
        </div>
        <table class="tblSecciones" width="100%" cellpadding="0" cellspacing="0">
    
            <tr>
                <td class="SeparadorVertical" colspan="2">            
           
                </td>
            </tr>
            <tr>
                <td>
                        <tr id="trInfoReclamo" runat="server" visible="false">
                            <td valign="top" colspan="4">
                            <table width="100%" >
                                <tr >
                                <td class="SeccionesH1" colspan="2">
                                    Información Reclamo
                                </td>
                            </tr>
                            <table width="100%" >
                                <tr >
                                <td class="SeccionesH1" colspan="2">
                                    <asp:Label ID="lblTitleReclamo" runat="server" />
                                   <%-- <asp:ImageButton 
                                        ID="ImgSearch" 
                                        BorderWidth="0" 
                                        BorderStyle="None" 
                                        CausesValidation="false" 
                                        runat="server" 
                                        ToolTip="Ver información de reclamo"
                                        ImageUrl="~/Resources/Images/LupaNegra.png"
                                    />--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="SeccionesH2" colspan="2">
                                    <asp:Label ID="lblTitleReclamoFrom" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="SeccionesH4" colspan="2">
                                    <asp:Label ID="lblUnidadTitle" runat="server" Text="Unidad: " Font-Bold="true" /><asp:Label ID="lblUnidad" runat="server" />
                                    <asp:Label ID="lblAsesorTitle" runat="server" Text=" Asesorado Por: " Font-Bold="true" /><asp:Label ID="lblAsesor" runat="server"  />
                                    <asp:Label ID="lblFechaTitle" runat="server" Text=" Fecha Reclamo: " Font-Bold="true" /><asp:Label ID="lblFechaReclamo" runat="server" />
                                </td>
                            </tr>
                        </table>

                            </td>
                        </tr>
                        <tr>
                        <td class="SeparadorVertical" colspan="4">            
                        </td>
                        </tr>
                        <tr>
                            <td class="SeccionesH3" style="width:120px;">
                                Tipo Acción:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblTipoAccion" runat="server" ForeColor="#800000" Font-Bold="true" />
                            </td>
                              <td align="right" style="width:45%" valign="top">                           
                    </td>
                        </tr>
                        <tr>
                            <td class="SeccionesH3">
                                Area:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblArea" runat="server" />
                                - <asp:Label ID="lblGerenteArea" runat="server" />
                            </td>
                            <td class="Separador">
                            </td>
                        </tr>
                       <%-- <tr>
                            <td class="SeccionesH3">
                                Gerente del Area:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                
                            </td>
                            <td class="Separador">
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="SeccionesH3">
                                Responsable Acción:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblResponsableAccion" runat="server" />
                            </td>
                            <td class="Separador">
                            </td>
                        </tr> 
                        <tr>
                            <td class="SeccionesH3">
                                Fecha Inicio - Fecha Final
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                 <asp:Label ID="lblFechaInicio" runat="server"  />
                                - <asp:Label ID="lblFechaFin" runat="server" />
                            </td>
                            <td class="Separador">
                            </td>
                        </tr>
                    </td> 
            </tr>
                <tr>
                    <td class="SeparadorVertical" colspan="4">
                    </td>
                </tr>
                <tr>
                    <td class="TituloSeccion" colspan="4">
                        Datos Plan de Acción
                    </td>
                </tr>
                <tr>
                    <th style="width:7%; text-align:left; vertical-align:top">
                        Estado :
                    </th>
                    <td class="Separador">
                    </td>
                    <td class="Line" colspan="2">
                        <asp:Label ID="lblEstado" runat="server" />
                        <asp:DropDownList ID="ddlEstado" runat="server" Visible="false">
                            <asp:ListItem Text="Registrada" Value="Registrada" />
                            <asp:ListItem Text="Programada" Value="Programada" />
                            <asp:ListItem Text="Realizada" Value="Realizada" />
                            <asp:ListItem Text="Cancelada" Value="Cancelada" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th style="text-align:left; vertical-align:top">
                        Descripción :
                    </th>
                    <td class="Separador">
                    </td>
                    <td class="Line"  colspan="2">
                        <asp:Label ID="lblDescripcion" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th style="text-align:left; vertical-align:top">
                        Fecha Actividad :
                    </th>
                    <td class="Separador">
                    </td>
                    <td class="Line"  colspan="2">
                        <asp:Label ID="lblFecha" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th style="text-align:left; vertical-align:top">
                        Usuario Seguimiento:
                    </th>
                    <td class="Separador">
                    </td>
                    <td class="Line"  colspan="2">
                        <asp:Label ID="lblUsuarioSeguimiento" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th style="text-align:left; vertical-align:top">
                        Usuario Ejecución:
                    </th>
                    <td class="Separador">
                    </td>
                    <td class="Line"  colspan="2">
                        <asp:Label ID="lblUsuarioEjecucion" runat="server" />
                    </td>
                </tr>
                <tr ID="trObservaciones" runat="server">
                    <th style="text-align:left; vertical-align:top">
                        Observaciones :
                    </th>
                    <td class="Separador">
                    </td>
                    <td class="Line"  colspan="2">
                        <asp:Label ID="lblObservaciones" runat="server" />
                        <asp:TextBox ID="txtObservaciones" runat="server" Rows="3" TextMode="MultiLine" 
                            Width="98%" />
                    </td>
                </tr>
                <tr>
                    <th style="text-align:left; vertical-align:top">
                        Anexos :
                    </th>
                    <td class="Separador">
                    </td>
                    <td class="Line"  colspan="2">
                        <asp:FileUpload ID="fupAnexoArchivo" runat="server" />
                        <asp:Button ID="btnAddArchivoAdjunto" runat="server" 
                            OnClick="BtnAddArchivoAdjunto_Click" 
                            OnClientClick="return ShowSplashModalLoading();" Text="Agregar" />
                    </td>
                </tr>
                <tr ID="trAnexos" runat="server">
                    <td>
                    </td>
                    <td class="Separador">
                    </td>
                    <td class="Line"  colspan="2">
                        <table class="tbl" width="100%">
                            <tr>
                                <th style="width:100%">
                                    Archivo</th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlArchivosAdjuntos" runat="server" Height="65px" 
                                        ScrollBars="Vertical" Width="100%">
                                        <table width="100%">
                                            <asp:Repeater ID="rptArchivosAdjuntos" runat="server" 
                                                OnItemDataBound="RptArchivosAdjuntos_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:HiddenField ID="hddIdArchivo" runat="server" />
                                                            <asp:LinkButton ID="lnkNombreArchivo" runat="server" 
                                                                OnClick="BtnDownloadArchivoAdjunto_Click" />
                                                        </td>
                                                        <td style="width:27px;">
                                                            <asp:ImageButton ID="imgDeleteAnexo" runat="server" BorderStyle="None" 
                                                                CausesValidation="false" ImageUrl="~/Resources/Images/RemoveGrid.png" 
                                                                OnClick="BtnRemoveArchivoAdjunto_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
        </table>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel>


<asp:UpdatePanel ID="upPanelCierreActividad" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="pCiereActividad"  runat="server" CssClass="popup_Container" Width="500" Height="200" style="display:none;">  

            <div class="popup_Titlebar" id="Div1">
                <div class="TitlebarLeft">
                    Administrar Actividad
                </div>
                <div class="TitlebarRight" id="div2">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelCierre" runat="server" Text="Regresar"  OnClick="BtnCancelCierreClick" CausesValidation="false" />
                <asp:Button ID="btnSaveRealizarActividad" runat="server" Text="Guardar" Visible="false" OnClick="BtnSaveRealizarActividad_Click"  CausesValidation="false" />
                <asp:Button ID="btnSaveCancelarActividad" runat="server" Text="Guardar" Visible="false" OnClick="BtnSaveCancelarActividad_Click" ValidationGroup="vgCancelarActividad"  />
            </div>

            <asp:ValidationSummary ID="vsCierreActividad" runat="server" DisplayMode="BulletList" Font-Size="8pt" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgCancelarActividad"/>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">                    
                    <tr id="trObservacionesCierre" runat="server">
                        <th style="text-align:left; vertical-align:top">
                            Observaciones Cierre :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtObservacionesCierre" runat="server" TextMode="MultiLine" Rows="7" Width="95%" MaxLength="512" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trObservacionesCancelacion" runat="server">
                        <th style="text-align:left; vertical-align:top">
                            Observaciones Cancelacion :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtObservacionesCancelacion" runat="server" TextMode="MultiLine" Rows="7" Width="95%" MaxLength="512" />
                        </td>

                        <td class="Separador">
                            <asp:RequiredFieldValidator ID="reqObservacionesCcancelcacion" runat="server"
                                                        ErrorMessage="Es necesario ingresar las observaciones de la cancelación de la actividad"
                                                        ControlToValidate="txtObservacionesCancelacion" ForeColor="Red"
                                                        ValidationGroup="vgCancelarActividad" >*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    
    <asp:Button ID="btnPopUpCierreActividadTargetControl" runat="server" style="display:none; "/>    

    <ajaxToolkit:ModalPopupExtender 
        ID="mpeCierreActividad" 
        runat="server" 
        TargetControlID="btnPopUpCierreActividadTargetControl" 
        PopupControlID="pCiereActividad" 
        BackgroundCssClass="ModalPopupBG" DropShadow="true" 
        > 
        </ajaxToolkit:ModalPopupExtender>   
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel> 

</asp:Content>
<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
     <table width="100%">
        <tr >
            <td style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" >
                <asp:Label ID="lblLogInfo" runat="server" ForeColor="#808080" Font-Size="8pt" />
            </td>
        </tr>
    </table>
</asp:Content>