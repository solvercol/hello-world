<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAddZona.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAddZona" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
  
 <asp:UpdatePanel ID="UPFormZona" runat="server">
 <ContentTemplate>

 <table width="100%" >
    <tr>
        <td valign="middle" style="width:3%;" align="left">
            <asp:ImageButton 
            ID="ImgSearch"
            BorderWidth="0" 
            BorderStyle="None" 
            CausesValidation="false" 
            runat="server" 
            ImageUrl="~/Resources/Images/round_plus.png" 
            OnClick="BtnSearchProduct_Click" ToolTip="Adicionar Nueva Zona"
            />
        </td>
    </tr> 
</table>
 
 
<asp:Panel ID="pnlImg"  runat="server" CssClass="popup_Container" Width="500" Height="200" style="display:none;">
 
 <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Adicionar Zona
        </div>
        <div class="TitlebarRight" id="divCloseZona">
        </div>
    </div>
<div class="popup_Body">  
<div style="padding:3px; text-align:right;">
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar" CausesValidation="false"></asp:button>
</div>
<table width="100%" class="tblSecciones">
        <tr>
            <td>
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
			            <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left">
						        <asp:textbox id="txtDescripcion" runat="server" TextMode="SingleLine" width="400px" MaxLength="512">
						        </asp:textbox>
                                <ajaxToolkit:AutoCompleteExtender ID="acDescripcion" runat="server"
                                enabled="True" minimumprefixlength="2" enablecaching="true"
                                ServicePath="WSScripts.asmx" ServiceMethod="buscarZona"
                                TargetControlID="txtDescripcion" completionsetcount="10"
                                completioninterval="200" CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem" OnClientItemSelected="ItemSeleccionado"> 
                                </ajaxToolkit:AutoCompleteExtender>
						        <asp:requiredfieldvalidator id="rfvDescripcion"
						        runat="server" 
						        errormessage="El campo [Descripción] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripcion">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        	    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>	
					    <tr>
                            <table width="100%">
                                <tr >
                                    <td  style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0; font-size:8pt; color:#808080;">
                                        Creado por:&nbsp;<asp:Label ID="lblCreateBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblCreateOn" runat="server"/>
                                    </td>
                                </tr>
                            </table>
					    </tr>
				    </table>
                </td>
        </tr>
    
</table>

</div>

</asp:Panel>

<asp:Button ID="btnTargetControl" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender
ID="mpeSearch" 
runat="server" 
TargetControlID="btnTargetControl" 
PopupControlID="pnlImg"
BackgroundCssClass="ModalPopupBG" DropShadow="true" 
cancelcontrolid="divCloseZona"
> 
</ajaxToolkit:ModalPopupExtender>

</ContentTemplate>
</asp:UpdatePanel>