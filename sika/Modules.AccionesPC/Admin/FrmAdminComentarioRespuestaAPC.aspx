<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminComentarioRespuestaAPC.aspx.cs" Inherits="Modules.AccionesPC.Admin.FrmAdminComentarioRespuestaAPC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">
    var divModal = 'DivModal';

    function ShowSplashModalLoading() {
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

<asp:UpdatePanel ID="upGeneral" runat="server">
    <ContentTemplate>
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>
        <div id="DivModal">
            <div id="VentanaMensaje">
                <div id="Msg">
                    <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
                </div>
            </div>
        </div>

         <table class="tblSecciones" width="100%" cellpadding="0" cellspacing="0">
    
            <tr>
               <td class="SeparadorVertical" colspan="2">               
           
                </td>
            </tr>
             <tr>
                <td>
                        <tr>             

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
                                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick"  />
                                    <asp:Button ID="btnEdit" runat="server" Text="Comentar" OnClick="BtnEditComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Salir" Visible="false" OnClick="BtnCancelComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnSave" runat="server" Text="Enviar" Visible="false" OnClick="BtnSaveComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                            </td>  
                        </tr>
                        <tr>
                            <td class="SeccionesH3">
                                Area:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblArea" runat="server" />
                            </td>
                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <td class="SeccionesH3">
                                Gerente del Area:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblGerenteArea" runat="server" />
                            </td>
                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <td class="SeccionesH3">
                                Responsable Acción:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblResponsableAccion" runat="server" />
                            </td>
                            <td class="Separador"></td>
                        </tr>                                    
                        <tr>
                            <td class="SeccionesH3">
                                Fecha Inicio:
                            </td>
                            <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblFechaInicio" runat="server" ForeColor="Red" />
                            </td>
                            <td class="Separador"></td>
                        </tr>
                        <tr>
                            <td class="SeccionesH3">
                                Fecha Final:
                            </td>
                              <td class="Separador"></td>
                            <td class="SeccionesH4">
                                <asp:Label ID="lblFechaFin" runat="server" ForeColor="Red" />
                            </td>
                              <td class="Separador"></td>
                        </tr>    
                </td>
            </tr>
            <tr>
               <td class="SeparadorVertical" colspan="4">          
                </td>
            </tr>
            <tr>
                <td colspan="4" class="TituloSeccion">
                    Datos Comentarios y Respuestas
                </td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Asunto :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblAsunto" runat="server" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Mensaje :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblMensaje" runat="server" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Destinatario :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblDestinatario" runat="server" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left; vertical-align:top">
                    Fecha :
                </th>

                <td class="Separador"></td>

                <td class="Line" >
                    <asp:Label ID="lblFecha" runat="server" />
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
            <tr id="trComentariosRespuesta" runat="server">
                <th style="text-align:left; vertical-align:top">
                    Comentarios y Respuestas :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <table width="100%" >
                        <asp:repeater id="rptComentariosAsociados" runat="server" OnItemDataBound="RptComentariosAsociados_ItemDataBound"  >                                                                 
                            <ItemTemplate>
                                <tr class="Line">
                                    <td style="width:17%; vertical-align:top">
                                        <asp:Label ID="lblFechaComentario" runat="server" />
                                    </td>
                                    <td style="width:12%; vertical-align:top">
                                        <asp:Label ID="lblCreadoPor" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblComentario" runat="server" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:repeater>
                    </table> 
                </td>

                <td class="Separador"></td>
            </tr>  
            <tr id="trComentarios" runat="server">
                <th style="text-align:left; vertical-align:top">
                    Nuevo Comentario :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:TextBox ID="txtComentario" runat="server" Width="98%" TextMode="MultiLine" Rows="3" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr id="trDestinatarios" runat="server">
                <th style="text-align:left; vertical-align:top">
                    Destinatario :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:DropDownList ID="wddDestinatarios" runat="server" Width="350px"  class="chzn-select" />
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
         
        </table>

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