<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminActividadesSolicitudes.ascx.cs" Inherits="Modules.AccionesPC.UserControls.WUCAdminActividadesSolicitudes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTADO PLAN DE ACCIÓN
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoActividad" runat="server" Text="Registrar Actividad" OnClick="BtnAddActividad_Click" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <asp:repeater id="rptActividadesList" runat="server" OnItemDataBound="RptActividadesList_ItemDataBound" >
                    <HeaderTemplate>
                        <tr>
                            <th style="width:3%;">                        
                            </th>
                            <%--<th style="width:3%;">                        
                            </th>--%>
                            <th style="width:33%;text-align:left;vertical-align:top">
                                Descripción
                            </th>
                            <th style="width:15%; text-align:left;;vertical-align:top">
                                Fecha
                            </th>
                            <th style="width:9%;text-align:left; color:#a31717;vertical-align:top">
                                Estado
                            </th>
                            <th style="width:20%;text-align:left;vertical-align:top">
                                Usuario Seguimiento
                            </th>
                               <th style="width:20%;text-align:left;vertical-align:top">
                                Usuario Ejecución
                            </th>
                            <th style="width:20%;text-align:left;vertical-align:top">
                                Autor
                            </th>
                        </tr>
                    </HeaderTemplate>   
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center; ">
                                <asp:HiddenField ID="hddIdActividad" runat="server" />
                                <asp:ImageButton 
                                            ID="imgSelectActividad" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/select.png"
                                            OnClick="BtnSelectActividad_Click" />
                            </td>                                    
                            <td style="text-align:left">
                                <asp:Label ID="lblDescripcion" runat="server" />
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblFechaActividad" runat="server" />
                            </td>
                            <td style="text-align:left; color:#a31717">
                                <asp:Label ID="lblEstado" runat="server" />
                            </td>   
                            <td style="text-align:left">
                                <asp:Label ID="lblSeguimiento" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblEjecucion" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblAutor" runat="server" />
                            </td>                    
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
            </table>
        </td>
    </tr>    
</table>

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>
        <asp:Panel ID="pnlAdminActividad"  runat="server" CssClass="popup_Container" Width="500" Height="500" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Plan de Acción
                </div>
                <div class="TitlebarRight" id="divCloseAdminActividad">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
                <asp:Button ID="btnGuardar" runat="server" Text="Programar Actividad" OnClick="BtnSaveActividad_Click"  />
            </div>

            <asp:ValidationSummary ID="vsActividades" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vsActividades"/>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%;">
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%;">
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                            <asp:Label ID="lblDescripcion" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Fecha Plan de Acción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFechaActividad" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender 
                                ID="cexTxtFechaActividad" 
                                runat="server"  
                                TargetControlID="txtFechaActividad" 
                                PopupPosition="Right" 
                                PopupButtonID="txtFechaActividad"
                                Format="dd/MM/yyyy"
                                CssClass="cal_Theme1" />
                            <asp:Label ID="lblFechaActividad" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr style="z-index: 200;" >
                        <th style="text-align:left; vertical-align:top">
                            Responsable Seguimiento :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="wddUsuarioSeguimiento" runat="server" Width="350px"  class="chzn-select" />
                            <asp:Label ID="lblUsuarioSeguimiento" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr style="z-index: 300;" >
                        <th style="text-align:left; vertical-align:top">
                            Responsable Ejecucion :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" >
                            <asp:DropDownList ID="wddUsuarioEjecucion" runat="server" Width="350px"  class="chzn-select" />
                            <asp:Label ID="lblUsuarioEjecucion" runat="server" />
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

                            <asp:Button ID="btnAddArchivoAdjunto" runat="server" Text="Agregar" OnClick="BtnAddArchivoAdjunto_Click" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-left:2px">
                            <table class="tbl" width="100%">
                                <tr>
                                    <th style="width:90%">Archivo</th>
                                    <th style="width:10%"></th>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlArchivosAdjuntos" runat="server" Width="100%" Height="65px" ScrollBars="Vertical">
                                            <table width="100%">
                                                <asp:repeater id="rptArchivosAdjuntos" runat="server" OnItemDataBound="RptArchivosAdjuntos_ItemDataBound" >                                                                 
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
                                                                    ImageUrl="~/Resources/Images/RemoveGrid.png"
                                                                    OnClick="BtnRemoveArchivoAdjunto_Click"  />
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
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopUpAdminActividadTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeAdminActividad" 
        runat="server" 
        TargetControlID="btnPopUpAdminActividadTargetControl" 
        PopupControlID="pnlAdminActividad" 
        BackgroundCssClass="ModalPopupBG" DropShadow="true" 
        cancelcontrolid="divCloseAdminActividad"> 
        </ajaxToolkit:ModalPopupExtender>   
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel>