<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminActividadesReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminActividadesReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTA DE ACTIVIDADES
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoActividad" runat="server" Text="Registrar Solución" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
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
                <tr>
                    <td style="text-align:center; ">
                        <asp:ImageButton 
                                    ID="imgSelect" 
                                    runat="server"
                                    CausesValidation="false"
                                    BorderStyle="None"
                                    ImageUrl="~/Resources/Images/select.png"  />
                    </td>
                    <td style="text-align:center; ">
                        <asp:ImageButton 
                                    ID="imgDelete" 
                                    runat="server"
                                    CausesValidation="false"
                                    BorderStyle="None"
                                    ImageUrl="~/Resources/Images/RemoveGrid.png"  />
                    </td>                                        
                    <td style="text-align:left">
                        Realizar nota credito al cliente
                    </td>
                    <td style="text-align:left;">
                        24/09/2013 12:35:13 pm
                    </td>
                    <td style="text-align:left">
                        Roberto Guzman
                    </td>
                    <td style="text-align:left">
                        Jose Huertas
                    </td>                    
                </tr>
            </table>
        </td>
    </tr>    
</table>

<asp:Panel ID="pnlAdminActividad"  runat="server" CssClass="popup_Container" Width="500" Height="450" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Administrar Actividad
        </div>
        <div class="TitlebarRight" id="divClose">
        </div>
    </div>

    <div class="popup_Body">                                                    
        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 25%">
                    Actividad :
                </th>

                <td class="Separador"></td>

                <td class="Line" style="width:70%">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Descripción :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Fecha Actividad :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Asignar a :
                </th>

                <td class="Separador"></td>

                <td class="Line">
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
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Realizada en :
                </th>

                <td class="Separador"></td>

                <td class="Line">
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
TargetControlID="btnNuevoActividad" 
PopupControlID="pnlAdminActividad" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divClose"> 
</ajaxToolkit:ModalPopupExtender>   