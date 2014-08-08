<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminCostosReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminCostosReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            COSTOS DE PRODUCTOS INVOLUCRADOS EN EL RECLAMO
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoGasto" runat="server" Text="Adicionar Gasto" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <tr>                    
                    <th style="width:4%;">                        
                    </th>
                    <th style="width:2%;">
                        No
                    </th>
                    <th style="width:28%; text-align:left;">
                        Producto
                    </th>
                    <th style="width:8%;">
                        Peso Neto
                    </th>
                    <th style="width:10%;">
                        Precio Lista
                    </th>
                    <th style="width:8%;">
                        Unidades
                    </th>
                    <th style="width:10%;">
                        Costo Producto
                    </th>
                    <th style="width:10%;">
                        Kilos
                    </th>
                    <th style="width:10%;">
                        Und a Disp.
                    </th>
                    <th style="width:10%;">
                        Costo Disp.
                    </th>
                </tr>
                <tr>
                    <td style="text-align:center; ">
                        <asp:ImageButton 
                                    ID="imgDelete" 
                                    runat="server"
                                    CausesValidation="false"
                                    BorderStyle="None"
                                    ImageUrl="~/Resources/Images/RemoveGrid.png"  />
                    </td>
                    <td style="text-align:center">
                        1
                    </td>
                    <td>
                        SHAFLEX CONSTRUCTION GRIS
                    </td>
                    <td style="text-align:right">
                        0,41
                    </td>
                    <td style="text-align:right">
                        15,000.00
                    </td>
                    <td style="text-align:right">
                        1,00
                    </td>
                    <td style="text-align:right; color:#2e40b3;">
                        10.575,00
                    </td>
                    <td style="text-align:right; color:#d66f00">
                        0,41
                    </td>
                    <td style="text-align:right">
                        1,00
                    </td>
                    <td style="text-align:right; color:#25960f;">
                        405,00
                    </td>                    
                </tr>
            </table>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td>
            <table class="tbl" width="50%" style=" text-align:center;margin:auto; padding:2px;">
                <tr>
                    <th style="width:80%; text-align:left;">
                        Item
                    </th>
                    <th style="text-align:right; width:20%">
                        Valor
                    </th>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Producto
                    </td>
                    <td style="text-align:right; color:#2e40b3;">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Transporte
                    </td>
                    <td style="text-align:right; color:#d66f00">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left" >
                        Costo Discopsición
                    </td>
                    <td style="text-align:right; color:#25960f;">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Pruebas de Campo
                    </td>
                    <td style="text-align:right">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Mano de Obra
                    </td>
                    <td style="text-align:right">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Otros Costos
                    </td>
                    <td style="text-align:right">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="height:7px">
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Asistencia Técnica
                    </td>
                    <td style="text-align:right">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Asistencia Técnica Regional
                    </td>
                    <td style="text-align:right">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Viaje Personas (Tickets, Viáticos)
                    </td>
                    <td style="text-align:right">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Equipos y Herramientas (Arrendamiento)
                    </td>
                    <td style="text-align:right">
                        21.150,00
                    </td>
                </tr>
                <tr>
                    <td style="height:7px">
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td style="font-weight:bolder; text-align:left">
                        Total Costos
                    </td>
                    <td style="text-align:right; font-weight:bolder;">
                        21.150,00
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<asp:Panel ID="pnlAdminCosto"  runat="server" CssClass="popup_Container" Width="500" Height="300" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Administrar Costo
        </div>
        <div class="TitlebarRight" id="divClose">
        </div>
    </div>

    <div class="popup_Body">                                                    
        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 25%">
                    Producto :
                </th>

                <td class="Separador"></td>

                <td class="Line" style="width:70%">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Peso Neto :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Precio Lista :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Unidades :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Costo Producto :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Kilos :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Und a Disponer :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Costo Disponible :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
        </table>
    </div>
</asp:Panel>
    
<asp:Button ID="btnPopUpAdminCostoTargetControl" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender 
ID="mpeAdminCosto" 
runat="server" 
TargetControlID="btnNuevoGasto" 
PopupControlID="pnlAdminCosto" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divClose"> 
</ajaxToolkit:ModalPopupExtender>   