<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminActividadesReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminActividadesReclamo" %>

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
                            <th style="width:3%;">                        
                            </th>
                            <th style="width:30%;text-align:left;">
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
                            <td style="text-align:center; ">
                                <asp:ImageButton 
                                            ID="imgDeleteActividad" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/RemoveGrid.png"
                                            OnClick="BtnRemoveActividad_Click" />
                            </td>                                        
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

<asp:Panel ID="pnlAdminActividad"  runat="server" CssClass="popup_Container" Width="500" Height="450" style="display:none;">  

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
                <th style="text-align:left; width: 25%">
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
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Descripción :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
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
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
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
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Copiar a :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Observaciones :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    NuevoAnexo :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="btnAdd" runat="server" Text="Agregar" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <table class="tbl" width="100%">
                        <tr>
                            <th style="width:90%">Archivo</th>
                            <th style="width:10%"></th>
                        </tr>
                        <tr>
                            <td>
                                Archivo de Pruebas.pdf
                            </td>
                            <td>
                                 <asp:ImageButton 
                                    ID="imgDeleteAnexo" 
                                    runat="server"
                                    CausesValidation="false"
                                    BorderStyle="None"
                                    ImageUrl="~/Resources/Images/RemoveGrid.png"  />
                            </td>
                        </tr>
                    </table>
                </td>
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
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divCloseAdminActividad"> 
</ajaxToolkit:ModalPopupExtender>   