<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminComentarioRespuestaReclamo.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmAdminComentarioRespuestaReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

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
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEditComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Salir" Visible="false" OnClick="BtnCancelComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
                                    <asp:Button ID="btnSave" runat="server" Text="Guardar" Visible="false" OnClick="BtnSaveComentarioClick" OnClientClick="return ShowSplashModalLoading();" />
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
                                Datos Comentarios y Respuestas
                            </td>
                        </tr>
                        <tr>
                            <th style="width:7%; text-align:left; vertical-align:top">
                                Asunto :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" style="width: 90%">
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
                        <tr id="tr1" runat="server">
                            <th style="text-align:left; vertical-align:top">
                                Comentarios y Respuestas :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <table width="100%" >
                                    <asp:repeater id="rptComentariosAsociados" runat="server" OnItemDataBound="RptComentariosAsociados_ItemDataBound"  >                                                                 
                                        <ItemTemplate>
                                            <tr class="Line">
                                                <td style="width:13%; vertical-align:top">
                                                    <asp:Label ID="lblFechaComentario" runat="server" />
                                                </td>
                                                <td style="width:10%; vertical-align:top">
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
                                <ig:WebDropDown ID="wddDestinatarios" 
                                            runat="server" 
                                            EnableMultipleSelection="false"
                                            MultipleSelectionType="Checkbox" 
                                            DisplayMode="DropDown"
                                            EnableClosingDropDownOnSelect="false"
                                            StyleSetName="Claymation"
                                            DropDownContainerWidth="300px"
                                            DropDownContainerHeight="220px"
                                            Width="50%">
                            </ig:WebDropDown>
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
                        <tr>
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
                </td>
            </tr>    
        </table>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>