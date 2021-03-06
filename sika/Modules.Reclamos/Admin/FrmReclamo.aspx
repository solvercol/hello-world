﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmReclamo.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmReclamo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="../UserControls/WucPanelEstado.ascx" tagname="WucPanelEstado" tagprefix="uc1" %>
<%@ Register src="../UserControls/WUCLogReclamoView.ascx" tagname="WucLogReclamo" tagprefix="ucLogReclamo" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">
    var divModal = 'DivModal';

    function ShowSplashModalLoading() {
        var adiv = $get(divModal);
        adiv.style.visibility = 'visible';
    }
</script>

<script type="text/javascript">

    function RebindScripts() {
        $(".chzn-select").chosen({ allow_single_deselect: true });

        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    }       
 
</script>
    
<div id="DivModal">
    <div id="VentanaMensaje">
        <div id="Msg">
            <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
        </div>
    </div>
</div>

<table width="100%" cellpadding="0" cellspacing="0" >
    
    <tr>
        <td class="SeparadorVertical">            
           
        </td>
    </tr>
     <tr>
        <td>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
                        <asp:PlaceHolder ID="phInfoReclamo"  runat="server"></asp:PlaceHolder>   
                    </td>             

                    <td valign="top">
                        <asp:UpdatePanel ID="upInfoReclamo" runat="server">
                            <ContentTemplate>
                                <table width="100%" >
                                    <tr>
                                        <td class="SeccionesH1" colspan="2">
                                            <asp:Label ID="lblTitleReclamo" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH2" colspan="2">
                                            <asp:Label ID="lblTitleReclamoFrom" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3" style="width:120px;">
                                            Unidad:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblUnidad" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3">
                                            Categoría:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblCategoria" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3">
                                            Responsable Reclamo:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblResponsable" runat="server"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3">
                                            Fecha Reclamo:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblFechaReclamo" runat="server" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="SeccionesH3">
                                            Costo Reclamo:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblTotalCostoReclamo" runat="server" ForeColor="Red" />
                                        </td>
                                    </tr>
                                     <tr id="trAcciones" runat="server" visible="false">
                                        <td class="SeccionesH3">
                                            Acciones Correctivas:
                                        </td>
                                        <td class="SeccionesH4">
                                           <asp:LinkButton ID="lnkAcciones" runat="server" OnClick="LnkAccionesClick"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>   
                    
                    <td align="right" style="width:45%" valign="top">
                        <asp:UpdatePanel ID="upMenuBar" runat="server">
                            <ContentTemplate>
                                 <div style="padding:3px; text-align:right;">
                                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" />
                                    <asp:Button ID="btnCopiarReclamo" runat="server" Text="Copiar Reclamo" OnClick="BtnCopiarReclamoClick" />
                                    <asp:Button ID="btnActualizarIndicadores" runat="server" Text="Crear Plan Acción" OnClick="BtnCreacionAccionesClick" Visible="false" />
                                    <asp:Button ID="btnAsociarPlanAccion" runat="server" Text="Relacionar Plan Acción" OnClick="BtnRelacionarplanAccionClick" Visible="true" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEditReclamoClick" Visible="false" />
                                    <asp:Button ID="btnDeclinar" runat="server" Text="Devolver" OnClick="BtnDevolverReclamoClick" Visible="false" />
                                    <asp:Button ID="btnRechazar" runat="server" Text="Rechazar Reclamo" OnClick="BtnCancelarReclamoClick" Visible="false" />
                                    <asp:Button ID="btnCambiarIngeniero" runat="server" Text="Cambiar Ingeniero R." OnClick="BtnCambiarIngenieroClick" Visible="false" />
                                    <asp:PlaceHolder ID="plhWf" runat="server"></asp:PlaceHolder>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upResumen" runat="server">
                            <ContentTemplate>
                                <div class="ContentPanelResumen">                           
                                                                    
                                    <uc1:WucPanelEstado ID="WucPanelEstado1" runat="server" />
                                                                    
                                </div>    
                            </ContentTemplate>
                        </asp:UpdatePanel>                  
                    </td>
                </tr>                
            </table>
        </td>
    </tr>
    <tr>
        <td class="SeparadorVertical">            
        </td>
    </tr>
    
</table>

    <asp:UpdatePanel ID="upSecciones" runat="server">
        <ContentTemplate>
                <table width="100%" class="FondoGrisSeccionesMenu" cellpadding="0" cellspacing="0"> 
                    <tr>
                        <td>        
                           
                                <asp:Menu ID="mnuSecciones" runat="server" Orientation="Horizontal" BackColor="#F5F5F5" Width="100%" OnMenuItemClick="MnuItemClick" > 
                                <StaticSelectedStyle ForeColor="#FFFFFF" BackColor="#808080" BorderStyle="None" Font-Bold="True" CssClass="center"  />
                                <StaticMenuItemStyle ForeColor="#7D7D7C"  BorderStyle="solid" BorderWidth="0" Font-Size="10px" Font-Bold="true" CssClass="center" 
                                    ItemSpacing="4px" HorizontalPadding="1px" Width="100" VerticalPadding="0px" BackColor="#F5F5F5"
                                    Height="15px" />
                                </asp:Menu>               
                           
                        </td>
                    </tr>
                </table>    
        </ContentTemplate>
    </asp:UpdatePanel>    

    <div style=" margin-top:2px;">    
        <asp:UpdatePanel ID="upContenidoReclamos" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>  
                <script type="text/javascript" language="javascript">
                    Sys.Application.add_load(RebindScripts);
                </script>  
                <div style="width:100%;">
                        <asp:PlaceHolder ID="phlContent"  runat="server"></asp:PlaceHolder>
                </div>  
            </ContentTemplate>
              <%--  <Triggers>
                    <asp:PostBackTrigger ControlID="mnuSecciones" />
                </Triggers>--%>
        </asp:UpdatePanel>
    </div>  
    




<asp:UpdatePanel ID="upInputWindows" runat="server">
        <ContentTemplate>
            

        <asp:Panel ID="pnlVentanaEmergente"  runat="server" CssClass="popup_Container" Width="400" Height="160" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                  <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
                </div>
                <div class="TitlebarRight" id="divCloseMensajes">
                </div>
            </div>

            <div class="popup_Body">      
                <asp:PlaceHolder ID="phlVentanaMensajes" runat="server"></asp:PlaceHolder>                
                <table width="100%" cellpadding="0" cellpadding="0">
                    <tr>
                        <td style=" width:45%">
                        </td>
                        <td class="Separador15"></td>
                        <td>  
                        
                            <asp:Button ID="btnAceptarInput" runat="server" Text="Aceptar" OnClick="BtnEnviarInputCick" ValidationGroup="grpInput" />
                                                     
                        </td>
                    </tr>
                </table>
                          
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeVentanaEmergente" 
        runat="server" 
        TargetControlID="btnTargetControl" 
        PopupControlID="pnlVentanaEmergente" 
        BackgroundCssClass="ModalPopupBG" 
        cancelcontrolid="divCloseMensajes"> 
        </ajaxToolkit:ModalPopupExtender>   


        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="upPopUpCopiarReclamo" runat="server">
        <ContentTemplate>
            

        <asp:Panel ID="pnlVentanaEmergenteCopiarReclamo"  runat="server" CssClass="popup_Container" Width="400" Height="160" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeaderCopiarReclamo">
                <div class="TitlebarLeft">
                  Copiar Reclamo
                </div>
                <div class="TitlebarRight" id="divCloseMensajesCopiarReclamo">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelarCopiar" runat="server" Text="Regresar"  />
                <asp:Button ID="btnSaveCopiar" runat="server" Text="Copiar Reclamo" OnClick="BtnSaveCopiarClick" OnClientClick="return ShowSplashModalLoading;"  />
            </div>

            <div class="popup_Body">      
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 30%; vertical-align:top">
                            Numero Asociación :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:TextBox ID="txtCampoRelacionado" runat="server" Width="90%" MaxLength="50" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                </table>
                          
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnTargetControlCoapiarReclamo" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeCopiarReclamo" 
        runat="server" 
        TargetControlID="btnTargetControlCoapiarReclamo" 
        PopupControlID="pnlVentanaEmergenteCopiarReclamo" 
        BackgroundCssClass="ModalPopupBG" 
        cancelcontrolid="divCloseMensajesCopiarReclamo"> 
        </ajaxToolkit:ModalPopupExtender>   


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
    <ucLogReclamo:WucLogReclamo ID="wucLogReclamo" runat="server" />
    <table width="100%">
        <tr >
            <td style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" 
                >
                <asp:Label ID="lblLogInfo" runat="server" ForeColor="#808080" Font-Size="8pt" />               
            </td>
        </tr>
    </table>
</asp:Content>
