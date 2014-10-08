<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmMisDocumentos.aspx.cs" Inherits="Modules.Documentos.Admin.FrmMisDocumentos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                </td>
                <td align="right" style="width:80%">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Documento" OnClick="BtnNuevoClick" />
                </td>
            </tr>
        </table>    
        <asp:Panel id="PanelFiltro" CssClass="FondoSeccionFiltro" runat="server"   Width="98%" >
                      
            <table class="tblSecciones" width="100%">
                <tr>
                    <th class="TituloEtiqueta" style="text-align:center; width: 100px;">
                        #Filtro
                    </th>
                    <td style="width:1%"></td>    
                    <td style="text-align:center; width: 400px;">
                        <asp:textbox id="txtFiltroNombre" runat="server" Width="95%" />
                    </td>
                    <td style="width:1%"></td>    
                    <th class="TituloEtiqueta" style="text-align:center;width: 100px;">
                        Estado
                    </th>
                    <td style="width:1%"></td>    
                    <td >
                        <asp:DropDownList ID="ddlEstado" class="chzn-select" runat="server" Width="200px" />
                    </td>
                    <td style="width:5px"></td>                            
                    <td align="left">
                        <asp:button id="btnFiltrar" runat="server" OnClick="BtnFindClick" text="Buscar" CausesValidation="false"  OnClientClick="return ShowSplashModalLoading();" />
                    </td>
                </tr>
            </table>

        </asp:Panel>

        <%--<ajaxToolkit:CollapsiblePanelExtender 
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
        </ajaxToolkit:CollapsiblePanelExtender>  --%>    
    </div>   
    
    <!-- Controles de Despliegue de datos de Reporte -->
    <asp:Panel ID="pReporte" runat="server" style="padding-bottom: 30px;" width="99%" Height="500px" ScrollBars="Auto">                
        <rsweb:ReportViewer ID="rptView" runat="server" Width="100%" Height="100%"
                            ShowBackButton="false" ShowCredentialPrompts="false" ShowPrintButton="false" ShowRefreshButton="false"
                            ShowZoomControl="false">
        </rsweb:ReportViewer>
    </asp:Panel> 

<script type="text/javascript">

    $(".chzn-select").chosen({ allow_single_deselect: true });

    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
 
</script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
