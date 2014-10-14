<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminSolicitudAPC.aspx.cs" Inherits="Modules.AccionesPC.Admin.FrmAdminSolicitudAPC" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script language="javascript" type="text/javascript">
        var divModal = 'DivModal';

        function ShowSplashModal() {
            var adiv = $get(divModal);
            adiv.style.visibility = 'visible';
        }
    </script>

    <script type="text/javascript">

        function RebindScripts() {
            $(".chzn-select").chosen({ allow_single_deselect: true });

            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
 
    </script>
    
    <div id="DivModal">
        <div id="VentanaMensaje">
            <div id="Msg">
                <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upgeneral" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                Sys.Application.add_load(RebindScripts);
            </script>  
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" />
                <asp:Button ID="btnSave" runat="server" Text="Guardar"  ValidationGroup="vgGeneral" OnClick="BtnSaveSolicitudAPCClick" OnClientClick="return ShowSplashModal();" />
            </div>

            <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
            
            <table width="100%" cellpadding="0" cellspacing="0" >
                <tr id="trInfoReclamo" runat="server">
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
                                    Información de la Acción
                                </td>
                            </tr>
                            <tr>
                                <th style="width:7%; text-align:left; vertical-align:top">
                                    * Area :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" style="width: 90%">
                                    <asp:DropDownList ID="wddArea" runat="server" Width="350px"  class="chzn-select" OnSelectedIndexChanged="WddArea_ValueChanged" AutoPostBack="true" />
                                </td>

                                <td class="Separador"></td>
                            </tr>
                            <tr>
                                <th style="text-align:left; vertical-align:top">
                                    * Proceso Asociado :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                    <asp:DropDownList ID="wddProceso" runat="server" Width="350px"  class="chzn-select" />
                                </td>

                                <td class="Separador"></td>
                            </tr>
                            <tr>
                                <th style="text-align:left; vertical-align:top">
                                    * Gerente Area :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                    <asp:Label ID="lblGerenteArea" runat="server" />
                                </td>

                                <td class="Separador"></td>
                            </tr>
                            <tr>
                                <th style="text-align:left; vertical-align:top">
                                    * Tipo Acción :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                    <asp:RadioButtonList ID="rblTipoAccion" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                        <asp:ListItem Selected="True" Value="Preventiva" Text="Preventiva" />
                                        <asp:ListItem Value="Correctiva" Text="Correctiva" />
                                    </asp:RadioButtonList>
                                </td>

                                <td class="Separador"></td>
                            </tr>
                            <tr>
                                <th style="text-align:left; vertical-align:top">
                                    * Posible Problema ó No Conformidad :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                    <asp:TextBox ID="txtDescripcionAccion" runat="server" TextMode="MultiLine" Width="97%" Rows="4" MaxLength="512" />
                                </td>

                                <td class="Separador"></td>
                            </tr>
                            <tr>
                                <th style="text-align:left; vertical-align:top">
                                    * Fechas :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                    <table width="100%">
                                        <tr>
                                            <th style="text-align:left; vertical-align:top">
                                                Fecha Inicio :
                                            </th>
                                            <td >
                                                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender 
                                                    ID="cexTxtFechaInicio" 
                                                    runat="server"  
                                                    TargetControlID="txtFechaInicio" 
                                                    PopupPosition="Right" 
                                                    PopupButtonID="txtFechaInicio"
                                                    Format="dd/MM/yyyy"
                                                    CssClass="cal_Theme1" />
                                            </td>
                                            <th style="text-align:left; vertical-align:top">
                                                Fecha Fin :
                                            </th>
                                            <td >
                                                <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender 
                                                    ID="cexTxtFechaFin" 
                                                    runat="server"  
                                                    TargetControlID="txtFechaFin" 
                                                    PopupPosition="Right" 
                                                    PopupButtonID="txtFechaFin"
                                                    Format="dd/MM/yyyy"
                                                    CssClass="cal_Theme1" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                                <td class="Separador"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="SeparadorVertical">            
                    </td>
                </tr> 
                <tr id="trContainerAnexos" runat="server">
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0" class="tblSecciones">
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
                                                                            OnClick="BtnRemoveArchivoAdjunto_Click"
                                                                             />
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
                    </td>
                </tr>
            </table>          
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
    <table width="100%">
        <tr >
            <td style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" >
                <asp:Label ID="lblLogInfo" runat="server" ForeColor="#808080" Font-Size="8pt" />
            </td>
        </tr>
    </table>
</asp:Content>