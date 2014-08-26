<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmReclamo.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmReclamo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register src="../UserControls/WucPanelEstado.ascx" tagname="WucPanelEstado" tagprefix="uc1" %>
 

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

<%--
<asp:UpdatePanel ID="upMenuPrincipal" runat="server">
    <ContentTemplate>
         <div style="padding:3px; text-align:right;">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" />
            <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEditReclamoClick" />
            <asp:PlaceHolder ID="plhWf" runat="server"></asp:PlaceHolder>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>--%>
   


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

                    <td valign="top">
                        <%--<asp:PlaceHolder ID="phInfoReclamo"  runat="server"></asp:PlaceHolder>                --%>
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
                                    Area:
                                </td>
                                <td class="SeccionesH4">
                                    <asp:Label ID="lblArea" runat="server" />
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
                                    Asesor:
                                </td>
                                <td class="SeccionesH4">
                                    <asp:Label ID="lblAsesor" runat="server"  />
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
                        </table>

                    </td>   
                    
                    <td align="right" style="width:25%" valign="top">
                        <asp:UpdatePanel ID="upMenuBar" runat="server">
                            <ContentTemplate>
                                 <div style="padding:3px; text-align:right;">
                                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEditReclamoClick" />
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
                            <asp:UpdatePanel ID="upMenu" runat="server">
                            <ContentTemplate>  
                                <asp:Menu ID="mnuSecciones" runat="server" Orientation="Horizontal" BackColor="#F5F5F5" Width="100%" OnMenuItemClick="MnuItemClick" > 
                                <StaticSelectedStyle ForeColor="#FFFFFF" BackColor="#F5BA38" BorderStyle="None" Font-Bold="True" CssClass="center"  />
                                <StaticMenuItemStyle ForeColor="#7D7D7C"  BorderStyle="solid" BorderWidth="0" Font-Size="10px" Font-Names="Arial" CssClass="center" 
                                    ItemSpacing="4px" HorizontalPadding="1px" Width="120" VerticalPadding="0px" BackColor="#F5F5F5"
                                    Height="15px" />
                                </asp:Menu>               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>    
        </ContentTemplate>
    </asp:UpdatePanel>

    <div style=" margin-top:2px;">    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>  
                <div style="width:100%;">
                        <asp:PlaceHolder ID="phlContent"  runat="server"></asp:PlaceHolder>
                </div>  
            </ContentTemplate>
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
                        <td style=" width:30%">
                        </td>
                        <td class="Separador15"></td>
                        <td>
                            <%--<asp:Button ID="btnEnviarFecha" runat="server" Text="Aceptar" OnClick="BtnEnviarFechaCick" ValidationGroup="grpFechaEntrega" />--%>
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
    <!-- Aca va el Log -->
</asp:Content>
