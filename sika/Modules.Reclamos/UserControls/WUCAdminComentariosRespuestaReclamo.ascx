<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminComentariosRespuestaReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminComentariosRespuestaReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>

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
                                <th style="width:29%;text-align:left;">
                                    Asunto
                                </th>
                                <th style="width:29%;text-align:left;">
                                    Mensaje
                                </th>
                                <th style="width:18%; text-align:left;">
                                    Fecha
                                </th>
                                <th style="width:20%;text-align:left;">
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
                                                    <td style="text-align:left;width:29%;vertical-align:top;border:none;">
                                                        <asp:Label ID="lblAsunto" runat="server" />
                                                    </td>
                                                    <td style="text-align:left;width:29%;vertical-align:top;border:none;">
                                                        <asp:Label ID="lblMensjae" runat="server" />
                                                    </td>                  
                                                    <td style="text-align:left;width:18%;vertical-align:top;border:none;">
                                                        <asp:Label ID="lblFechaComentario" runat="server" />
                                                    </td>
                                                    <td style="text-align:left;width:20%;vertical-align:top;border:none;">
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
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>

        <ig:WebDialogWindow 
            ID="wdwAdminComentarios" 
            runat="server" 
            CssClass="WebDialogWindowStyle" 
            Height="470px"
            Width="530px" 
            InitialLocation="Centered" 
            MaintainLocationOnScroll="True" 
            Modal="True" 
            Moveable="true"
            Left="0px"
            Top="0px" 
            ModalBackgroundCssClass="ModalWebDialogWindowStyle" 
            WindowState="Hidden">
            <Header>
                <CloseBox Visible="true" />
            </Header>
            <ContentPane>
                <Template> 

        <asp:Panel ID="pnlAdminComentarioRespuesta"  runat="server" CssClass="popup_Container" Width="97%" >

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Comentario / Respuesta
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresar_Click" />
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

                        <td class="Line" style="position: absolute; z-index: 300;" >
                            <asp:DropDownList ID="wddDestinatarios" runat="server" Width="350px"  class="chzn-select" />
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
                                            <td style="width:90%">
                                            </td>
                                            <td style="width:10%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="position:absolute; z-index: 100;">
                                                <asp:DropDownList ID="wddUsuarioCopia" runat="server" Width="290px"  class="chzn-select" />
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
                        
        </Template>
            </ContentPane>
        </ig:WebDialogWindow>

    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>