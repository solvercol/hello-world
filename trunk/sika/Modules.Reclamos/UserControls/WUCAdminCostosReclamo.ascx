<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminCostosReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminCostosReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %> 

<%@ Register src="WUCFilterProduct.ascx" tagname="WucFilterProduct" tagprefix="ucFilterProduct" %> 

 <script language="javascript" type="text/javascript">
     var divModal = 'DivModal';

     function ShowSplashModal() {
         var adiv = $get(divModal);
         adiv.style.visibility = 'visible';
     }
</script>

<div id="DivModal">
    <div id="VentanaMensaje">
        <div id="Msg">
            <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
        </div>
    </div>
</div>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            COSTOS DE PRODUCTOS INVOLUCRADOS EN EL RECLAMO
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoGasto" runat="server" Text="Adicionar Costo Producto" OnClick="BtnAddCosto_Click" />
                <asp:Button ID="btnSaveCostos" runat="server" Text="Guardar Gastos Reclamo" OnClick="BtnSaveCostos_Click" OnClientClick="return ShowSplashModal();" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <asp:repeater id="rptCostosList" runat="server" OnItemDataBound="RptCostosList_ItemDataBound" >              
                <HeaderTemplate>
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
                </HeaderTemplate>   
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center; ">
                            <asp:HiddenField ID="hddIdCosto" runat="server" />
                            <asp:ImageButton 
                                        ID="imgDeleteCosto" 
                                        runat="server"
                                        CausesValidation="false"
                                        BorderStyle="None"
                                        ImageUrl="~/Resources/Images/RemoveGrid.png"
                                        OnClick="BtnRemoveCosto_Click"  />
                        </td>
                        <td style="text-align:center">
                            <asp:Label ID="lblNoCosto" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblProducto" runat="server" />
                        </td>
                        <td style="text-align:right">
                            <asp:Label ID="lblPesoNeto" runat="server" />
                        </td>
                        <td style="text-align:right">
                            <asp:Label ID="lblPrecioLista" runat="server" />
                        </td>
                        <td style="text-align:right">
                            <asp:Label ID="lblUnidades" runat="server" />
                        </td>
                        <td style="text-align:right; color:#2e40b3;">
                            <asp:Label ID="lblCostoProducto" runat="server" />
                        </td>
                        <td style="text-align:right; color:#d66f00">
                            <asp:Label ID="lblKilos" runat="server" />
                        </td>
                        <td style="text-align:right">
                            <asp:Label ID="lblUnidadesDisponibles" runat="server" />
                        </td>
                        <td style="text-align:right; color:#25960f;">
                            <asp:Label ID="lblCostoDisponible" runat="server" />
                        </td>                    
                    </tr>
                </ItemTemplate>
                </asp:repeater>
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
                        <asp:Label ID="lblCostoProductoReclamo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Transporte
                    </td>
                    <td style="text-align:right; color:#d66f00">
                        <asp:Label ID="lblCostoTransporte" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left" >
                        Costo Discopsición
                    </td>
                    <td style="text-align:right; color:#25960f;">
                        <asp:Label ID="lblCostoDisposicion" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Pruebas de Campo
                    </td>
                    <td style="text-align:right">
                        <ig:WebNumericEditor    Id="txtCostoPruebasCampo" runat="server" 
                                                Nullable="false" MinValue="0" Width="90%"
                                                OnTextChanged="TxtTotalCostosTextChanged" AutoPostBackFlags-ValueChanged="On"  />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Mano de Obra
                    </td>
                    <td style="text-align:right">
                        <ig:WebNumericEditor    Id="txtCostoManoObra" runat="server" 
                                                Nullable="false" MinValue="0" Width="90%"
                                                OnTextChanged="TxtTotalCostosTextChanged" AutoPostBackFlags-ValueChanged="On"  />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Otros Costos
                    </td>
                    <td style="text-align:right">
                        <ig:WebNumericEditor    Id="txtOtrosCostos" runat="server" 
                                                Nullable="false" MinValue="0" Width="90%"
                                                OnTextChanged="TxtTotalCostosTextChanged" AutoPostBackFlags-ValueChanged="On"  />
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
                        <ig:WebNumericEditor    Id="txtCostosAsistenciaTecnica" runat="server" 
                                                Nullable="false" MinValue="0" Width="90%"
                                                OnTextChanged="TxtTotalCostosTextChanged" AutoPostBackFlags-ValueChanged="On"  />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Asistencia Técnica Regional
                    </td>
                    <td style="text-align:right">
                        <ig:WebNumericEditor    Id="txtCostosAsistenciaRegional" runat="server" 
                                                Nullable="false" MinValue="0" Width="90%"
                                                OnTextChanged="TxtTotalCostosTextChanged" AutoPostBackFlags-ValueChanged="On"  />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Viaje Personas (Tickets, Viáticos)
                    </td>
                    <td style="text-align:right">
                        <ig:WebNumericEditor    Id="txtCostoViajePersonas" runat="server" 
                                                Nullable="false" MinValue="0" Width="90%"
                                                OnTextChanged="TxtTotalCostosTextChanged" AutoPostBackFlags-ValueChanged="On"  />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Costo Equipos y Herramientas (Arrendamiento)
                    </td>
                    <td style="text-align:right">
                        <ig:WebNumericEditor    Id="txtCostoEquiposHerramientas" runat="server" 
                                                Nullable="false" MinValue="0" Width="90%"
                                                OnTextChanged="TxtTotalCostosTextChanged" AutoPostBackFlags-ValueChanged="On"  />
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
                        <asp:Label ID="lblTotalCostosReclamo" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<asp:Panel ID="pnlAdminCosto"  runat="server" CssClass="popup_Container" Width="600" Height="400" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Administrar Costo
        </div>
        <div class="TitlebarRight" id="divCloseAdminCosto">
        </div>
    </div>

    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnSaveCosto_Click"  />
    </div>

    <div class="popup_Body">                                                    
        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 25%">
                    Producto :
                </th>

                <td class="Separador"></td>

                <td  style="width:80%">
                    <ucFilterProduct:WucFilterProduct ID="ucFilterProduct" runat="server" ShowProductTable="false" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Peso Neto :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblPesoNetoProducto" runat="server" Text="Label" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Precio Lista :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblPrecioListaProducto" runat="server" Text="Label" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Unidades :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <ig:WebNumericEditor    Id="txtUnidadesProducto" runat="server" 
                                            Nullable="false" MinValue="0" Width="90%"
                                            OnTextChanged="TxtUnidadesProductoTextChanged" AutoPostBackFlags-ValueChanged="On" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Costo Producto :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblCostoProducto" runat="server" Text="Label" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Kilos :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblKilosProducto" runat="server" Text="Label" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Undidades a Disponer :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <ig:WebNumericEditor    Id="txtUnidadesDisponerProducto" runat="server" 
                                            Nullable="false" MinValue="0" Width="90%"
                                            OnTextChanged="TxtUnidadesDisponerProductoTextChanged" AutoPostBackFlags-ValueChanged="On"  />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Costo Disponible :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:Label ID="lblCostoDisponibleProducto" runat="server" Text="Label" />
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
TargetControlID="btnPopUpAdminCostoTargetControl" 
PopupControlID="pnlAdminCosto" 
BackgroundCssClass="ModalPopupBG"  DropShadow="true" 
cancelcontrolid="divCloseAdminCosto"> 
</ajaxToolkit:ModalPopupExtender>   