<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmListaGeneralReclamos.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmListaGeneralReclamos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <asp:UpdatePanel ID="upgeneral" runat="server">
    <ContentTemplate>

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
                      
                  <table class="tblSecciones">
                                <tr>
                                    <th class="TituloEtiqueta">
                                        Asesor
                                    </th>
                                    <td style="width:1%"></td>
                                    <td>
                                        <asp:TextBox ID="txtNombreCliente" runat="server" Width="150"></asp:TextBox>
                                    </td>
                                    <td style="width:1%"></td>
                                    <th class="TituloEtiqueta">
                                        Código Cliente
                                    </th>
                                    <td style="width:1%"></td>
                                    <td>
                                        <asp:TextBox ID="txtCodigoCliente" runat="server" Width="60px"></asp:TextBox>
                                    </td>
                                    <td style="width:50%" align="left" >
                                        <asp:Button ID="btnFiltrar" runat="server" Text="Filter" CausesValidation="false" />
                                        <asp:Button ID="btnRemoveFilter" runat="server" Text="Remove" CausesValidation="false"/>
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
         
        
      

        <div style=" height:430px;">

            <div class="pager" style="width:100%">
                <table width="100%">
                    <tr>
                        <th>
                            Responsable
                        </th>
                        <th>
                            Fecha Reclamo
                        </th>
                        <th>
                            Producto Servicio
                        </th>
                        <th>
                            Estado
                        </th>
                        <th>
                            Total Costo
                        </th>
                    </tr>
                </table>				        
	        </div>	
        
        </div>
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

</asp:Content>
