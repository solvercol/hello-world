<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminComentariosRespuestaReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminComentariosRespuestaReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTA DE COMENTARIOS Y RESPUESTAS
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoComentarioRespuesta" runat="server" Text="Registrar Comentario" OnClick="BtnAddComentario_Click" />
                <asp:Button ID="btnNuevoRespuestaCliente" runat="server" Text="Enviar Respuesta Cliente" OnClick="BtnAddRespuestaCliente_Click" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <asp:UpdatePanel ID="upContainer" runat="server">
                <ContentTemplate>                
                <table class="tbl" width="100%">
                    <asp:repeater id="rptComentariosList" runat="server" OnItemDataBound="RptComentariosList_ItemDataBound" >
                        <HeaderTemplate>
                            <tr>
                                <th style="width:5%;">                        
                                </th>
                                <th style="width:30%;text-align:left;">
                                    Asunto
                                </th>
                                <th style="width:36%;text-align:left;">
                                    Mensaje
                                </th>
                                <th style="width:15%; text-align:left;">
                                    Fecha
                                </th>
                                <th style="width:15%;text-align:left;">
                                    Autor
                                </th>
                            </tr>
                        </HeaderTemplate>   
                        <ItemTemplate>
                            <tr runat="server" id="rowParent" >
                                <td style="text-align:center;border-right:none;border-top:none;">
                                    <asp:HiddenField ID="hddIdComentario" runat="server" />                                    
                                    <asp:ImageButton 
                                                ID="imgSelectComentario" 
                                                runat="server"
                                                CausesValidation="false"
                                                BorderStyle="None"
                                                ImageUrl="~/Resources/Images/comentarios.png"
                                                OnClick="BtnSelectComentario_Click" ToolTip="Click para ver mas información del comentario." />
                                </td>  
                                <td style="text-align:left;vertical-align:top;border-right:none;border-top:none;">
                                    <asp:Label ID="lblAsunto" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top;border-right:none;border-top:none;">
                                    <asp:Label ID="lblMensaje" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top;border-right:none;border-top:none;">
                                    <asp:Label ID="lblFechaComentario" runat="server" />
                                </td>
                                <td style="text-align:left;vertical-align:top;border-top:none;">
                                    <asp:Label ID="lblAutor" runat="server" />
                                </td>                    
                            </tr>
                            <tr runat="server" class="child" id="rowChild" >
                                <td colspan="5" style="border-top:none;">
                                    <table width="100%" style="border:none">
                                        <asp:repeater id="rptChildComentarios" runat="server" OnItemDataBound="RptComentariosAsociadosList_ItemDataBound" >
                                            <itemtemplate>
                                                <tr >
                                                    <td style="width:5%;text-align:right;border:none;">
                                                        <asp:ImageButton 
                                                                ID="imgSelectComentarioRespuesta" 
                                                                runat="server"
                                                                CausesValidation="false"
                                                                BorderStyle="None"
                                                                ImageUrl="~/Resources/Images/respuesta-comentario.png"/>
                                                    </td>
                                                    <td style="text-align:left;width:30%;vertical-align:top;border:none;">
                                                        <asp:Label ID="lblAsunto" runat="server" />
                                                    </td>
                                                    <td style="text-align:left;width:36%;vertical-align:top;border:none;">
                                                        <asp:Label ID="lblMensjae" runat="server" />
                                                    </td>                  
                                                    <td style="text-align:left;width:15%;vertical-align:top;border:none;">
                                                        <asp:Label ID="lblFechaComentario" runat="server" />
                                                    </td>
                                                    <td style="text-align:left;width:15%;vertical-align:top;border:none;">
                                                        <asp:Label ID="lblAutor" runat="server" />
                                                    </td> 
                                                </tr>
                                            </itemtemplate>
                                        </asp:repeater>
                                    </table>
                                </td>                
                            </tr>
                            </ItemTemplate>
                    </asp:repeater>
                </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>    
</table>
    

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
        <asp:Panel ID="pnlAdminComentarioRespuesta"  runat="server" CssClass="popup_Container" Width="500" Height="470" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Comentario / Respuesta
                </div>
                <div class="TitlebarRight" id="divCloseAdminComentario">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
                <asp:Button ID="btnGuardar" runat="server" Text="Enviar" OnClick="BtnSaveComentario_Click"  />
                <asp:Button ID="btnGuardarCliente" runat="server" Text="Enviar" OnClick="BtnSaveComentarioCliente_Click"  />
            </div>

            <asp:ValidationSummary ID="vsComentarios" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vsComentarios"/>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%; vertical-align:top">
                            Asunto :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:TextBox ID="txtAsunto" runat="server" Width="90%" />
                            <asp:Label ID="lblAsunto" runat="server"/>
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Observaciones :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                            <asp:Label ID="lblObservaciones" runat="server"/>
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trInfoDestinatario" runat="server" >
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
                                            Width="98%">
                            </ig:WebDropDown>
                            <asp:Label ID="lblDestinatarios" runat="server"/>
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trInfoContacto" runat="server" >
                        <th style="text-align:left; vertical-align:top">
                            Mail Contacto :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtMailContacto" runat="server" MaxLength="512" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top;">                    
                             <div id="divPanelShowUsuariosCopia">                        
                                <div style="float: left; vertical-align:middle;">
                                    <asp:Label 
                                    ID="lblUsuariosCopiaTitle" 
                                    runat="server" 
                                    ForeColor="#526C8C" 
                                    Font-Names="Tahoma, Verdana, Arial"
                                    Font-Size="0.82em">Copiar a :</asp:Label>
                                </div>
                                <div style="float: left; vertical-align: middle;" id="PnlFiltroHeader">
                                    <asp:ImageButton 
                                    ID="ShowHideUsuariosCopia" 
                                    BorderStyle="None"
                                    BorderWidth="0"
                                    CausesValidation="false"
                                    runat="server" 
                                    AlternateText="Ver Copiar" />
                                </div>
                            </div>
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">

                            <div>
                                <asp:Panel id="PanelUsuariosCopia" runat="server" >
                      
                                    <table class="tblSecciones" width="100%">
                                        <tr>
                                            <td>
                                                <ig:WebDropDown ID="wddUsuarioCopia" 
                                                                runat="server" 
                                                                EnableMultipleSelection="false"
                                                                MultipleSelectionType="Checkbox" 
                                                                DisplayMode="DropDown"
                                                                EnableClosingDropDownOnSelect="false"
                                                                StyleSetName="Claymation"
                                                                DropDownContainerWidth="300px"
                                                                DropDownContainerHeight="220px"
                                                                Width="98%">
                                                </ig:WebDropDown>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddCopia" runat="server" Text="Agregar" OnClick="BtnAddUsuarioCopia_Click"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lstUsuariosCopia" runat="server" SelectionMode="Single" Width="98%" Height="80px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnRemoveCopia" runat="server" Text="Eliminar" OnClick="BtnRemoveUsuarioCopia_Click"  />
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>

                                <ajaxToolkit:CollapsiblePanelExtender 
                                                ID="cpeCopiarUsuarios" 
                                                runat="server" 
                                                TargetControlID="PanelUsuariosCopia"
                                                ExpandControlID="divPanelShowUsuariosCopia" 
                                                CollapseControlID="divPanelShowUsuariosCopia" 
                                                TextLabelID="lblUsuariosCopiaTitle"
                                                ImageControlID="ShowHideUsuariosCopia" 
                                                ExpandedText="Ocultar Copiar" 
                                                CollapsedText="Ver Copiar"
                                                ExpandedImage="~/Resources/images/Collapse.gif" 
                                                CollapsedImage="~/Resources/images/Expand.gif"
                                                SuppressPostBack="true" Collapsed="true">
                                                </ajaxToolkit:CollapsiblePanelExtender> 
                            </div>

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
    
        <asp:Button ID="btnPopUpAdminComentarioRespuestaTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
                    ID="mpeAdminSolucion" 
                    runat="server" 
                    TargetControlID="btnPopUpAdminComentarioRespuestaTargetControl" 
                    PopupControlID="pnlAdminComentarioRespuesta" 
                    BackgroundCssClass="ModalPopupBG" DropShadow="true"
                    cancelcontrolid="divCloseAdminComentario"> 
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel>