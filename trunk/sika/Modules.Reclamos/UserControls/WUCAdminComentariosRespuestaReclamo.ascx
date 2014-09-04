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
                                <th style="width:61%;text-align:left;">
                                    Descripción
                                </th>
                                <th style="width:14%; text-align:left;">
                                    Fecha
                                </th>
                                <th style="width:20%;text-align:left;">
                                    Autor
                                </th>
                            </tr>
                        </HeaderTemplate>   
                        <ItemTemplate>
                            <tr runat="server" id="rowParent" >
                                <td style="text-align:center; ">
                                    <asp:HiddenField ID="hddIdComentario" runat="server" />
                                    <img alt="Ver Comentarios y Respuestas" id="imgVerExpand" runat="server" src="~/Resources/Images/Expand.gif" />
                                    <asp:ImageButton 
                                                ID="imgSelectComentario" 
                                                runat="server"
                                                CausesValidation="false"
                                                BorderStyle="None"
                                                ImageUrl="~/Resources/Images/select.png"
                                                OnClick="BtnSelectComentario_Click" />
                                </td>  
                                <td style="text-align:left">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>                  
                                <td style="text-align:left;">
                                    <asp:Label ID="lblFechaComentario" runat="server" />
                                </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblAutor" runat="server" />
                                </td>                    
                            </tr>
                            <tr runat="server" class="child" id="rowChild" >
                                <td colspan="4">
                                    <div id="divDetalle"  runat="server" style="float: left; margin-top: 0px; display:none; width:100%; padding-bottom:15px;">
                                        <table width="100%">
                                            <asp:repeater id="rptChildComentarios" runat="server" OnItemDataBound="RptComentariosAsociadosList_ItemDataBound" >
                                                <itemtemplate>
                                                    <tr >
                                                        <td style="width:5%">
                                                        </td>
                                                        <td style="text-align:left;width:61%">
                                                            <asp:Label ID="lblDescripcion" runat="server" />
                                                        </td>                  
                                                        <td style="text-align:left;width:14%">
                                                            <asp:Label ID="lblFechaComentario" runat="server" />
                                                        </td>
                                                        <td style="text-align:left;width:20%">
                                                            <asp:Label ID="lblAutor" runat="server" />
                                                        </td> 
                                                    </tr>
                                                </itemtemplate>
                                            </asp:repeater>
                                        </table>
                                    </div>
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
        <asp:Panel ID="pnlAdminComentarioRespuesta"  runat="server" CssClass="popup_Container" Width="500" Height="300" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Comentario / Respuesta
                </div>
                <div class="TitlebarRight" id="divCloseAdminComentario">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnSaveComentario_Click"  />
            </div>

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
                    <tr>
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