<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFilterClient.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCFilterClient" %>

<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%" >
    <tr>
        <td style="width:95%; font-size:12pt; color:#000090;" align="left" class="Line">
            <asp:Literal ID="litNombreClienteSeleccionado" runat="server"></asp:Literal>
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
            OnClick="BtnSearchCliente_Click"
            />
        </td>
    </tr>              
</table>

 <asp:Panel ID="pnlImgClient"  runat="server" CssClass="popup_Container" Width="900" Height="400" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Buscar Cliente
        </div>
        <div class="TitlebarRight" id="divCloseClient">
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
                    <asp:TextBox ID="txtFilterCliente" runat="server" Width="90%" MaxLength="100"></asp:TextBox>                  
                </td>
                <td align="right" style="width:10%;">   
                    <asp:Button ID="btnFiltrar" runat="server" CausesValidation="false" Text="Filtrar" OnClick="BtnFiltrarClick" />                                  
                </td>
            </tr>
        </table>

        <asp:Panel ID="pnlContainerClientList" runat="server" Width="100%" Height="250px" ScrollBars="Vertical" >
            <table id="tblListado" class="tbl" width="100%">
		        <asp:repeater   id="rptListado" 
                                runat="server" 
                                OnItemDataBound="RptListadoItemDataBound">
			        <headertemplate>
					    <tr>
                            <th style="width:3%;">...</th>
						    <th style="width:10%;">Cod.Cliente</th>
                            <th style="width:30%;">Cliente</th> 
                            <th style="width:13%;">Contacto</th>
                            <th style="width:13%;">Email</th>
                            <th style="width:15%;">Unidad</th>
                            <th style="width:15%;">Zona</th>
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
                                <asp:Literal ID="litCodCliente" runat="server"></asp:Literal>
                            </td>
                            <td align="left">
                                <asp:Literal ID="litCliente" runat="server"></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="litContacto" runat="server"></asp:Literal>
                            </td>
                            <td align="right">
                                <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                            </td>
                            <td align="right">
                                <asp:Literal ID="litUnidad" runat="server"></asp:Literal>
                            </td>
                            <td align="left">
                                <asp:Literal ID="litZona" runat="server"></asp:Literal>
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
    

<asp:Button ID="btnTargetControlClient" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender 
ID="mpeSearchClient" 
runat="server" 
TargetControlID="btnTargetControlClient" 
PopupControlID="pnlImgClient" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divCloseClient"> 
</ajaxToolkit:ModalPopupExtender>