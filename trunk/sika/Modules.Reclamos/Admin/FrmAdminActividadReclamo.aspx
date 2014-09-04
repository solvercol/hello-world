<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminActividadReclamo.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmAdminActividadReclamo" %>

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

        <table width="100%" cellpadding="0" cellspacing="0" >
    
            <tr>
                <td class="SeparadorVertical">            
           
                </td>
            </tr>
             <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>

                            <td valign="top">
                                <asp:PlaceHolder ID="phInfoReclamo"  runat="server"></asp:PlaceHolder>                

                            <td valign="top">
                                <table width="100%" >
                                    <tr>
                                        <td class="SeccionesH1" colspan="2">
                                            <asp:Label ID="lblTitleReclamo" runat="server" />
                                            <asp:ImageButton 
                                                ID="ImgSearch" 
                                                BorderWidth="0" 
                                                BorderStyle="None" 
                                                CausesValidation="false" 
                                                runat="server" 
                                                ToolTip="Ver información de reclamo"
                                                ImageUrl="~/Resources/Images/LupaNegra.png" 
                                                OnClick="BtnViewReclamo_Click"
                                            />
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
                    
                            <td align="right" style="width:35%" valign="top">                        
                                <div style="padding:3px; text-align:right;">
                                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick"  />
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEditActividadClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Salir" Visible="false" OnClick="BtnCancelActividadClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnSave" runat="server" Text="Guardar" Visible="false" OnClick="BtnSaveActividadClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnSaveRealizada" runat="server" Text="Marcar Realizada" Visible="false" OnClick="BtnSaveRealizadaClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnCancelActividad" runat="server" Text="Cancelar" Visible="false" OnClick="BtnCancelarActividadClick" OnClientClick="return ShowSplashModalLoading();" />
                                </div>
                            </td>
                        </tr>                
                    </table>
                </td>
            </tr>
            <tr>
                <td class="SeparadorVertical">            
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0" class="tblSecciones">
                        <tr>
                            <td colspan="4" class="TituloSeccion">
                                Datos Actividad
                            </td>
                        </tr>
                        <tr>
                            <th style="width:7%; text-align:left; vertical-align:top">
                                Estado :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" style="width: 90%">
                                <asp:Label ID="lblEstado" runat="server" />
                                <asp:DropDownList ID="ddlEstado" runat="server" Visible="false">
                                    <asp:ListItem Text="Registrada" Value="Registrada" />
                                    <asp:ListItem Text="Programada" Value="Programada" />
                                    <asp:ListItem Text="Realizada" Value="Realizada" />
                                    <asp:ListItem Text="Cancelada" Value="Cancelada" />
                                </asp:DropDownList>
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Actividad :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:Label ID="lblActividad" runat="server" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Descripción :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:Label ID="lblDescripcion" runat="server" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Fecha Actividad :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:Label ID="lblFecha" runat="server" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Asignado a :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:Label ID="lblAsignado" runat="server" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr id="trUsuariosCopia" runat="server">
                            <th style=" text-align:left; vertical-align:top">
                                Usuarios Copia :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:ListBox ID="lstUsuariosCopia" runat="server" SelectionMode="Single" Width="98%" Height="80px" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr id="trObservaciones" runat="server">
                            <th style="text-align:left; vertical-align:top">
                                Observaciones :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:Label ID="lblObservaciones" runat="server" />
                                <asp:TextBox ID="txtObservaciones" runat="server" Width="98%" TextMode="MultiLine" Rows="3" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Anexos :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:FileUpload ID="fupAnexoArchivo" runat="server" />

                                <asp:Button ID="btnAddArchivoAdjunto" runat="server" Text="Agregar" OnClick="BtnAddArchivoAdjunto_Click" OnClientClick="return ShowSplashModalLoading();" />
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr id="trAnexos" runat="server">
                            <td></td>
                            <td class="Separador"></td>

                            <td class="Line">
                                <table class="tbl" width="100%">
                                    <tr>
                                        <th style="width:100%">Archivo</th>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlArchivosAdjuntos" runat="server" Width="100%" Height="65px" ScrollBars="Vertical">
                                                <table width="100%">
                                                    <asp:repeater id="rptArchivosAdjuntos" runat="server" OnItemDataBound="RptArchivosAdjuntos_ItemDataBound"  >                                                                 
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td >
                                                                    <asp:HiddenField ID="hddIdArchivo" runat="server" />                                                                
                                                                    <asp:LinkButton ID="lnkNombreArchivo" runat="server" OnClick="BtnDownloadArchivoAdjunto_Click" />
                                                                </td>
                                                                <td style="width:27px;" >
                                                                     <asp:ImageButton 
                                                                        ID="imgDeleteAnexo" 
                                                                        runat="server"
                                                                        CausesValidation="false"
                                                                        BorderStyle="None"
                                                                        ImageUrl="~/Resources/Images/RemoveGrid.png" OnClick="BtnRemoveArchivoAdjunto_Click" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:repeater>
                                                </table>                                            
                                            </asp:Panel>
                                        </td>
                                    </tr>                                
                                </table>
                            </td>

                            <td class="Separador"></td>
                        </tr>
                        <tr id="trLogCierre" runat="server">
                            <th style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" colspan="4">
                                <asp:Label ID="lblLogCierre" runat="server" Font-Bold="true" />
                            </th>
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