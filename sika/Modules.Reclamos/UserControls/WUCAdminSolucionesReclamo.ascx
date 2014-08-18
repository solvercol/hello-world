<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminSolucionesReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminSolucionesReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                    Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTA DE SOLUCIONES Y RESPUESTAS
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoSolucion" runat="server" Text="Registrar Solución" OnClick="BtnAddSolucion_Click" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <asp:repeater id="rptSolucionList" runat="server" OnItemDataBound="RptSolucionList_ItemDataBound" >              
                    <HeaderTemplate>
                        <tr>
                            <th style="width:3%;">                        
                            </th>
                            <th style="width:3%;">                        
                            </th>
                            <th style="width:14%;text-align:left;">
                                Fecha
                            </th>
                            <th style="width:30%; text-align:left;">
                                Referencia
                            </th>
                            <th style="width:30%;text-align:left;">
                                Departamento
                            </th>
                            <th style="width:20%;text-align:left;">
                                Autor
                            </th>
                        </tr>
                    </HeaderTemplate>   
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center; ">
                                <asp:HiddenField ID="hddIdSolucion" runat="server" />
                                <asp:ImageButton 
                                            ID="imgSelectSolucion" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/select.png"
                                            OnClick="BtnSelectSolucion_Click"  />
                            </td>
                            <td style="text-align:center; ">
                                <asp:ImageButton 
                                            ID="imgDeleteSolucion" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/RemoveGrid.png" 
                                            OnClick="BtnRemoveSolucion_Click"  />
                            </td>                    
                            <td style="text-align:left;">
                                <asp:Label ID="lblFechaSolucion" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblReferenciaSolucion" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblDepartamentoSolucion" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblCreateBy" runat="server" />
                            </td>                    
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
            </table>
        </td>
    </tr>    
</table>

<asp:Panel ID="pnlAdminSolucion"  runat="server" CssClass="popup_Container" Width="500" Height="300" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Administrar Solución
        </div>
        <div class="TitlebarRight" id="divCloseAdminSolucion">
        </div>
    </div>

    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnSaveSolucion_Click"  />
    </div>

    <div class="popup_Body">                                                    
        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 25%">
                    Respuesta de :
                </th>

                <td class="Separador"></td>

                <td class="Line" style="width:70%">
                    <ig:WebDropDown ID="wddDepartamento" 
                                    runat="server" 
                                    EnableMultipleSelection="false"
                                    MultipleSelectionType="Checkbox" 
                                    DisplayMode="DropDown"
                                    EnableClosingDropDownOnSelect="false"
                                    StyleSetName="Claymation"
                                    DropDownContainerWidth="250px"
                                    DropDownContainerHeight="150px"
                                    Width="98%">
                    </ig:WebDropDown>
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Referencia :
                </th>

                <td class="Separador"></td>
                
                <td class="Line">
                    <asp:TextBox ID="txtReferencia" runat="server" Width="90%" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Observaciones :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:TextBox ID="txtObservaciones" runat="server" Width="90%" TextMode="MultiLine" Rows="3" />
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
    
<asp:Button ID="btnPopUpAdminSolucionTargetControl" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender 
ID="mpeAdminSolucion" 
runat="server" 
TargetControlID="btnPopUpAdminSolucionTargetControl" 
PopupControlID="pnlAdminSolucion" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divCloseAdminSolucion"> 
</ajaxToolkit:ModalPopupExtender>   