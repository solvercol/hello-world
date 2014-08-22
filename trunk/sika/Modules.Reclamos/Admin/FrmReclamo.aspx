﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmReclamo.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 

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

<asp:UpdatePanel ID="upMenuBar" runat="server">
    <ContentTemplate>
         <div style="padding:3px; text-align:right;">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresar_Click" />
            <asp:Button ID="btnEdit" runat="server" Text="Editar" OnClick="BtnEditReclamo_Click" />
            <asp:PlaceHolder ID="plhWf" runat="server"></asp:PlaceHolder>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
   


<table width="100%" cellpadding="0" cellspacing="0" >
    
    <tr>
        <td class="SeparadorVertical">            
           
        </td>
    </tr>
     <tr>
        <td>
            <table width="100%" cellpadding="0" cellspacing="0" class="tblPreView">
                <tr>
                    <td>
                        <asp:PlaceHolder ID="phInfoReclamo"  runat="server"></asp:PlaceHolder>                
                    </td>   
                    
                    <td align="right" style="width:25%" rowspan="4" valign="top">
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

</asp:Content>



<asp:Content ID="footerContent" runat="server" ContentPlaceHolderID="Footer">
    <!-- Aca va el Log -->
</asp:Content>
