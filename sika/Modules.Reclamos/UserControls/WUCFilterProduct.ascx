<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFilterProduct.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCFilterProduct" %>

<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%" >
    <tr>
        <td style="width:95%; font-size:12pt; color:#000090;" align="left" class="Line">
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
    <tr id="trInfoProducto" runat="server" visible="false">
        <td colspan="2">
            <table width="100%" class="tbl">
                <tr>
                    <th style="width:20%; text-align:center">
                        Presentación
                    </th>
                    <th style="width:20%; text-align:center">
                        Target Market
                    </th>
                    <th style="width:30%; text-align:center">
                        Campo de Aplicación
                    </th>
                    <th style="width:30%; text-align:center">
                        SubCampo de Aplicación
                    </th>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Label ID="lblPresentacionProducto" runat="server" />
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="lblTargetMarketProducto" runat="server" />
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="lblCampoAplicacionProducto" runat="server" />
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="lblSubCampoAplicacionProducto" runat="server" />
                    </td>
                </tr>
            </table>            
        </td>
    </tr>               
</table>

 <asp:Panel ID="pnlImg"  runat="server" CssClass="popup_Container" Width="900" Height="400" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Buscar Producto
        </div>
        <div class="TitlebarRight" id="divClose">
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
                    <asp:TextBox ID="txtFilterProduct" runat="server" Width="90%" MaxLength="100"></asp:TextBox>                  
                </td>
                <td align="right" style="width:10%;">   
                    <asp:Button ID="btnFiltrar" runat="server" CausesValidation="false" Text="Filtrar" OnClick="BtnFiltrarClick" />                                  
                </td>
            </tr>
        </table>

        <asp:Panel ID="pnlContainerProductList" runat="server" Width="100%" Height="250px" ScrollBars="Vertical" >
            <table id="tblListado" class="tbl" width="100%">
		        <asp:repeater   id="rptListado" 
                                runat="server" 
                                OnItemDataBound="RptListadoItemDataBound">
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
    

<asp:Button ID="btnTargetControl" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender 
ID="mpeSearch" 
runat="server" 
TargetControlID="btnTargetControl" 
PopupControlID="pnlImg" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divClose"> 
</ajaxToolkit:ModalPopupExtender>