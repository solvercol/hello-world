<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminCostosReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminCostosReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %> 
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<script language="javascript" type="text/javascript">
    function clickButtonProduct(e, buttonid) {
        var evt = e ? e : window.event;
        var bt = document.getElementById(buttonid);
        if (bt) {
            if (evt.keyCode == 13) {
                bt.click();
                return false;
            }
        }
    }
</script>

<asp:UpdatePanel ID="upInfoCostosReclamo" runat="server">
<ContentTemplate>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            COSTOS DE PRODUCTOS INVOLUCRADOS EN EL RECLAMO
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoGasto" runat="server" Text="Adicionar Costo Producto" OnClick="BtnAddCosto_Click" Visible="false" />
                <asp:Button ID="btnSaveCostos" runat="server" Text="Guardar Costos Reclamo" OnClick="BtnSaveCostos_Click" Visible="false" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar Costos Reclamo" OnClick="BtnEditCostos_Click" />
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
                        <asp:Label ID="lblCostoPruebasCampo" runat="server" />
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
                        <asp:Label ID="lblCostoManoObra" runat="server" />
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
                        <asp:Label ID="lblOtrosCostos" runat="server" />
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
                        <asp:Label ID="lblCostosAsistenciaTecnica" runat="server" />
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
                        <asp:Label ID="lblCostosAsistenciaRegional" runat="server" />
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
                        <asp:Label ID="lblCostoViajePersonas" runat="server" />
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
                        <asp:Label ID="lblCostoEquiposHerramientas" runat="server" />
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
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnCancelCosto_Click"  />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnSaveCosto_Click"  />
    </div>

    <asp:ValidationSummary ID="vsCostos" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vsCostos"/>

    <div class="popup_Body">                                                    
        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 25%">
                    Producto :
                </th>

                <td class="Separador"></td>

                <td  style="width:80%" class="Line">
                    <table width="100%">
                        <tr>
                            <td style="width:95%; font-size:12pt; color:#000090;" align="left">
                                <asp:Literal ID="litNombreProductoSeleccionado" runat="server"></asp:Literal>
                            </td>
                            <td style="width:2%;"></td>
                                <td valign="middle" style="width:3%;" align="center">
                                <asp:ImageButton 
                                ID="ImgSearch" 
                                BorderWidth="0" 
                                BorderStyle="None" 
                                CausesValidation="false" 
                                runat="server" 
                                ImageUrl="~/Resources/Images/LupaNegra.png" 
                                OnClick="BtnSearchProduct_Click"
                                />
                            </td>
                        </tr> 
                    </table>
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




<!-- ************************************************************************************** -->
 <asp:Panel ID="pnlImgProducto"  runat="server" CssClass="popup_Container" Width="920" Height="350" style="display:none;">  

    <div class="popup_Titlebar" id="Div1">
        <div class="TitlebarLeft">
            Buscar Producto
        </div>
    </div>

    <div class="popup_Body">  
                
        <table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">            
            <tr>
                <td style="width:10%;" class="Etiquetas">
                    Valor
                </td>
                <td class="Separador15"></td>
                <td valign="middle" style="width:70%;" class="Line">
                    <asp:TextBox ID="txtFilterProduct" runat="server" Width="90%" MaxLength="100" ></asp:TextBox>                  
                </td>
                <td align="right" style="width:10%;">   
                    <asp:Button ID="btnFiltrar" runat="server" CausesValidation="false" Text="Filtrar" OnClick="BtnFiltrarClick" />                                  
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancelar" OnClick="BtnCancelFiltrarClick" />   
                </td>
            </tr>
        </table>

        <asp:Panel ID="pnlContainerProductList" runat="server" Width="100%" Height="250px" ScrollBars="Vertical" >
            <table id="tblListado" class="tbl" width="100%">
		        <asp:repeater   id="rptListadoProducto" 
                                runat="server" 
                                OnItemDataBound="RptListadoProductoItemDataBound">
			        <headertemplate>
					    <tr>
                            <th style="width:3%;">...</th>
						    <th style="width:10%;">Cod.Producto</th>
                            <th style="width:15%;">Producto</th> 
                            <th style="width:8%;">Unidad</th>
                            <th style="width:8%;">Peso.Neto</th>
                            <th style="width:8%;">Precio.Lista</th>
                            <th style="width:15%;">Target Market</th>
                            <th style="width:20%;">Campo Aplicación</th>
                            <th style="width:22%;">SubCampo Aplicación</th>
                            <th style="width:1%;"></th>                        
					    </tr>
				    </headertemplate>
				    <itemtemplate>
					    <tr>
                            <td align="center">
                                <asp:ImageButton 
                                ID="ImgSelect" CausesValidation="false"
                                BorderStyle="None"
                                ToolTip="Select"
                                runat="server" 
                                OnClick="BtnSelect_Click"
                                ImageUrl="~/Resources/Images/select.png" />
					        </td>
						    <td align="center">
                                <asp:Literal ID="litCodProducto" runat="server"></asp:Literal>
                            </td>
                            <td align="left">
                                <asp:Literal ID="litProducto" runat="server"></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="litUnidad" runat="server"></asp:Literal>
                            </td>
                            <td align="right">
                                <asp:Literal ID="litPesoNeto" runat="server"></asp:Literal>
                            </td>
                            <td align="right">
                                <asp:Literal ID="litPrecioLista" runat="server"></asp:Literal>
                            </td>
                            <td align="left">
                                <asp:Literal ID="litTargetMarket" runat="server"></asp:Literal>
                            </td>

                            <td align="left">
                                <asp:Literal ID="litCampoAplicacion" runat="server"></asp:Literal>
                            </td>

                            <td align="left">
                                <asp:Literal ID="litSubCampoAplicacion" runat="server"></asp:Literal>
                            </td>

                            <td>                            
                            </td>
                        				                   
					    </tr>
				    </itemtemplate>
			    </asp:repeater>
		    </table>
        </asp:Panel>                           

        <asp:Label ID="lblNoRecords" runat="server" Text="No Records" CssClass="validator" Visible="false"></asp:Label>
        <div class="pager">
			    <csc:PagerLinq
                id="pgrListado" 
                runat="server"
                OnPageChanged="PgrListadoPageChanged"  
                pagesize="100"></csc:PagerLinq>
	    </div>

    </div>
</asp:Panel>
    

<asp:Button ID="btnTargetControlProducto" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender
ID="mpeSearchProducto" 
runat="server" 
TargetControlID="btnTargetControlProducto" 
PopupControlID="pnlImgProducto"
BackgroundCssClass="ModalPopupBG" DropShadow="true"
> 
</ajaxToolkit:ModalPopupExtender>

</ContentTemplate>
</asp:UpdatePanel>