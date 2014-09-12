<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmReclamosPorFecha.aspx.cs" Inherits="Modules.Reclamos.Views.FrmReclamosPorFecha" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        var cal1;
        var cal2;

        function pageLoad() {
            cal1 = $find("calendar1");
            cal2 = $find("calendar2");

            modifyCalDelegates(cal1);
            modifyCalDelegates(cal2);
        }

        function modifyCalDelegates(cal) {
            //we need to modify the original delegate of the month cell.
            cal._cell$delegates = {
                mouseover: Function.createDelegate(cal, cal._cell_onmouseover),
                mouseout: Function.createDelegate(cal, cal._cell_onmouseout),

                click: Function.createDelegate(cal, function (e) {

                    e.stopPropagation();
                    e.preventDefault();

                    if (!cal._enabled) return;

                    var target = e.target;
                    var visibleDate = cal._getEffectiveVisibleDate();
                    Sys.UI.DomElement.removeCssClass(target.parentNode, "ajax__calendar_hover");
                    switch (target.mode) {
                        case "prev":
                        case "next":
                            cal._switchMonth(target.date);
                            break;
                        case "title":
                            switch (cal._mode) {
                                case "days": cal._switchMode("months"); break;
                                case "months": cal._switchMode("years"); break;
                            }
                            break;
                        case "month":
                            //if the mode is month, then stop switching to day mode.
                            if (target.month == visibleDate.getMonth()) {
                                //this._switchMode("days");
                            } else {
                                cal._visibleDate = target.date;
                                //this._switchMode("days");
                            }
                            cal.set_selectedDate(target.date);
                            cal._switchMonth(target.date);
                            cal._blur.post(true);
                            cal.raiseDateSelectionChanged();
                            break;
                        case "year":
                            if (target.date.getFullYear() == visibleDate.getFullYear()) {
                                cal._switchMode("months");
                            } else {
                                cal._visibleDate = target.date;
                                cal._switchMode("months");
                            }
                            break;
                        case "today":
                            cal.set_selectedDate(target.date);
                            cal._switchMonth(target.date);
                            cal._blur.post(true);
                            cal.raiseDateSelectionChanged();
                            break;
                    }
                })
            }
        }

        function onCalendarShown(sender, args) {
            //set the default mode to month
            sender._switchMode("months", true);
            changeCellHandlers(cal1);
        }

        function changeCellHandlers(cal) {
            if (cal._monthsBody) {
                //remove the old handler of each month body.
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        $common.removeHandlers(row.cells[j].firstChild, cal._cell$delegates);
                    }
                }
                //add the new handler of each month body.
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        $addHandlers(row.cells[j].firstChild, cal._cell$delegates);
                    }
                }
            }
        }


        function onCalendarHidden(sender, args) {
            if (sender.get_selectedDate()) {
                //get the final date
                var finalDate = new Date(sender.get_selectedDate());
                var selectedMonth = finalDate.getMonth();
                finalDate.setDate(1);
                finalDate.setMonth(selectedMonth + 1);
                //set the date to the TextBox
                sender.get_element().value = finalDate.format(sender._format);
            }
        }


    </script>

    <script language="javascript" type="text/javascript">
        var divModal = 'DivModal';

        function ShowSplashModalLoading() {
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


<div style="padding-bottom:5px; background-color:#FAFAFA;">

    <table width="100%" cellpadding="0" cellspacing="0" class="CabeceraSeccionFiltro">
        <tr>
                       
            <td>
                <asp:Panel style="width:99%;" ID="pnlHeader" runat="server" CssClass="CabeceraSeccionFiltro">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                                    
                        <td style="width:20%;" valign="middle">
                        <div id="divPanelShow">
                            <div style="float: left; vertical-align: middle;" id="PnlFiltroHeader">
                                <asp:ImageButton 
                                ID="ShowHide" 
                                BorderStyle="None"
                                BorderWidth="0"
                                CausesValidation="false"
                                runat="server" 
                                AlternateText="Ver Filtro..." />
                            </div>
                                <div style="float: left; width: 109px; vertical-align:middle; padding-left:10px;">
                                <asp:Label 
                                ID="lbFiltro" 
                                runat="server" 
                                ForeColor="#999999" 
                                Font-Names="Trebuchet MS"
                                Font-Size="0.8em">Ver Filtro...</asp:Label>
                            </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            </td>
            <td align="right" style="width:80%">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Reclamo" OnClick="BtnNuevoReclamo_Click" />
            </td>
        </tr>
    </table>                

    <asp:Panel id="PanelFiltro" CssClass="FondoSeccionFiltro" runat="server"  style="display:none;" Width="98%" >
                      
        <table class="tblSecciones" width="100%">
            <tr>
                <th class="TituloEtiqueta" style="text-align:center">
                    #Reclamo
                </th>
                <td style="width:1%"></td>    
                <th class="TituloEtiqueta" style="text-align:center">
                    Cliente
                </th>
                <td style="width:1%"></td>    
                <th class="TituloEtiqueta" style="text-align:center">
                    Producto
                </th>
                <td style="width:1%"></td>    
                <th class="TituloEtiqueta" style="text-align:center">
                    Servicio
                </th>
                <td style="width:1%"></td>    
                <th class="TituloEtiqueta" style="text-align:center">
                    Fecha
                </th>
                <td style="width:1%"></td>                            
                <td >                                
                                
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtNoReclamo" runat="server" />
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtCliente" runat="server" />
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtProducto" runat="server" />
                </td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtServicio" runat="server" />
                </td>
                <td></td>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width:50%">
                                <asp:TextBox ID="wdpFiltroDateFrom" ClientIDMode="Static" runat="server" Width="75px" Font-Size="8pt" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" BehaviorID="calendar1" runat="server"
                                    Enabled="True" Format="yyyy-MM" TargetControlID="wdpFiltroDateFrom" OnClientShown="onCalendarShown"
                                    OnClientHidden="onCalendarHidden">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td style="width:50%">
                                <asp:TextBox ID="wdpFiltroDateTo" ClientIDMode="Static" runat="server" Width="75px" Font-Size="8pt" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" BehaviorID="calendar2" runat="server"
                                    Enabled="True" Format="yyyy-MM" TargetControlID="wdpFiltroDateTo" OnClientShown="onCalendarShown"
                                    OnClientHidden="onCalendarHidden">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td align="left">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filter" CausesValidation="false" OnClick="BtnFilterReclamos_Click" OnClientClick="return ShowSplashModalLoading();" />
                </td>
            </tr>
        </table>

    </asp:Panel>

    <ajaxToolkit:CollapsiblePanelExtender 
        ID="CollapsiblePanelExtender1" 
        runat="server" 
        TargetControlID="PanelFiltro"
        ExpandControlID="divPanelShow" 
        CollapseControlID="divPanelShow" 
        TextLabelID="lbFiltro"
        ImageControlID="ShowHide" 
        ExpandedText="Ocultar Filtro..." 
        CollapsedText="Ver Filtro..."
        ExpandedImage="~/Resources/images/Collapse.gif" 
        CollapsedImage="~/Resources/images/Expand.gif"
        SuppressPostBack="true" Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender> 
</div>    
        
 <asp:UpdatePanel ID="upgeneral" runat="server">
    <ContentTemplate>     

        <asp:Panel ID="pnlNewReclamo"  runat="server" CssClass="popup_Container" Width="500" Height="150" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Nuevo Reclamo
                </div>
                <div class="TitlebarRight" id="divClose">
                </div>
            </div>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%">
                            Tipo de Reclamo :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:RadioButtonList ID="rblReclamoType" RepeatLayout="Table" RepeatColumns="2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RblReclamoType_Changed">
                                <asp:ListItem Text="Producto" Value="Producto" Selected="True" />
                                <asp:ListItem Text="Servicio" Value="Servicio" />
                            </asp:RadioButtonList>
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr id="trCategoriaReclamo" runat="server" visible="false">
                        <th style="text-align:left; width: 25%">
                            Categoría :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:DropDownList ID="ddlCategoriaReclamo" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="padding:3px; text-align:right;">
                                <asp:Button ID="btnConfirmNewReclamo" runat="server" Text="Continuar" OnClick="BtnConfirmNuevoReclamo_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopNewReclamoTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeNewReclamo" 
        runat="server" 
        TargetControlID="btnPopNewReclamoTargetControl" 
        PopupControlID="pnlNewReclamo" 
        BackgroundCssClass="ModalPopupBG"  DropShadow="true" 
        cancelcontrolid="divClose"> 
        </ajaxToolkit:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>

<!-- Controles de Despliegue de datos de Reporte -->
<asp:Panel ID="pReporte" runat="server" style="padding-bottom: 30px;" width="99%" Height="500px" ScrollBars="Auto">                
    <rsweb:ReportViewer ID="rptReclamos" runat="server" Width="100%" Height="100%" 
                        ShowBackButton="false" ShowCredentialPrompts="false" ShowPrintButton="false" ShowRefreshButton="false"
                        ShowZoomControl="false">
    </rsweb:ReportViewer>
</asp:Panel>

</asp:Content>