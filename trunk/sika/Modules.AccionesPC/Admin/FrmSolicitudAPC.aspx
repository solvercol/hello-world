<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmSolicitudAPC.aspx.cs" Inherits="Modules.AccionesPC.Admin.FrmSolicitudAPC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 

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
                                    <tr id="trInfoReclamo" runat="server">
                                        <td valign="top" colspan="2">
                                            <table width="100%" >
                                                <tr>
                                                    <td class="SeccionesH1" colspan="2">
                                                        <asp:Label ID="lblTitleReclamo" runat="server" />
                                                        <asp:ImageButton 
                                                            ID="ImgSearch" 
                                                            BorderWidth="0" 
                                                            BorderStyle="None" 
                                                            CausesValidation="false" 
                                                            runat="server" 
                                                            ToolTip="Ver información de reclamo"
                                                            ImageUrl="~/Resources/Images/LupaNegra.png"
                                                            OnClick="BtnViewReclamoClick"
                                                        />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SeccionesH2" colspan="2">
                                                        <asp:Label ID="lblTitleReclamoFrom" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SeccionesH4" colspan="2">
                                                        <asp:Label ID="lblUnidadTitle" runat="server" Text="Unidad: " Font-Bold="true" /><asp:Label ID="lblUnidad" runat="server" />
                                                        <asp:Label ID="lblAsesorTitle" runat="server" Text=" Asesorado Por: " Font-Bold="true" /><asp:Label ID="lblAsesor" runat="server"  />
                                                        <asp:Label ID="lblFechaTitle" runat="server" Text=" Fecha Reclamo: " Font-Bold="true" /><asp:Label ID="lblFechaReclamo" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </td> 
                                    </tr>                
                                    <tr>
                                        <td class="SeparadorVertical" colspan="2">            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3" style="width:120px;">
                                            Tipo Acción:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblTipoAccion" runat="server" ForeColor="#800000" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3">
                                            Area:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblArea" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3">
                                            Gerente del Area:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblGerenteArea" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3">
                                            Responsable Acción:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblResponsableAccion" runat="server" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="SeccionesH3">
                                            Fecha Inicio:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblFechaInicio" runat="server" ForeColor="Red" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeccionesH3">
                                            Fecha Final:
                                        </td>
                                        <td class="SeccionesH4">
                                            <asp:Label ID="lblFechaFin" runat="server" ForeColor="Red" />
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
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEditSolicitudClick" />
                                    <asp:PlaceHolder ID="plhWf" runat="server"></asp:PlaceHolder>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upResumen" runat="server">
                            <ContentTemplate>
                                <div class="ContentPanelResumen">                           
                                                                    
                                                                    
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
                        <td align="center">                     
                         <asp:Button ID="btnCancelar" runat="server" Text="Cacelar"  OnClick="BtnCancelarInputClick" />
                         <asp:Button ID="btnAceptarInput" runat="server" Text="Aceptar"  ValidationGroup="grpInput" OnClick="BtnAceptarInputClick" />                                                  
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

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
     <table width="100%">
        <tr >
            <td style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" >
                <asp:Label ID="lblLogInfo" runat="server" ForeColor="#808080" Font-Size="8pt" />
            </td>
        </tr>
    </table>
</asp:Content>