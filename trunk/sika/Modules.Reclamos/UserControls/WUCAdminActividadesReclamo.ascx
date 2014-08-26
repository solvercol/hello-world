﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminActividadesReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminActividadesReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTA DE ACTIVIDADES
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
                            <th style="width:33%;text-align:left;">
                                Descripción
                            </th>
                            <th style="width:14%; text-align:left;">
                                Fecha
                            </th>
                            <th style="width:25%;text-align:left;">
                                Asignado
                            </th>
                            <th style="width:25%;text-align:left;">
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
                           <%-- <td style="text-align:center; ">
                                <asp:ImageButton 
                                            ID="imgDeleteActividad" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/RemoveGrid.png"
                                            OnClick="BtnRemoveActividad_Click" />
                            </td> --%>                                       
                            <td style="text-align:left">
                                <asp:Label ID="lblDescripcion" runat="server" />
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblFechaActividad" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblAsignado" runat="server" />
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
        <asp:Panel ID="pnlAdminActividad"  runat="server" CssClass="popup_Container" Width="500" Height="490" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Actividad
                </div>
                <div class="TitlebarRight" id="divCloseAdminActividad">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnSaveActividad_Click"  />
            </div>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%; vertical-align:top">
                            Actividad :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <ig:WebDropDown ID="wddActividadesReclamo" 
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
                            <asp:Label ID="lblActividadesReclamo" runat="server" />
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
                            Fecha Actividad :
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
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Asignar a :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <ig:WebDropDown ID="wddUsuarioAsignacion" 
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
                            <asp:Label ID="lblUsuarioAsignacion" runat="server" />
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
                                                <asp:Button ID="btnAddCopia" runat="server" Text="Agregar" OnClick="BtnAddUsuarioCopia_Click" />
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
                            Observaciones :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                            <asp:Label ID="lblObservaciones" runat="server" />
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