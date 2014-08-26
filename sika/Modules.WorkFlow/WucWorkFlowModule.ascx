<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucWorkFlowModule.ascx.cs" Inherits="Modules.WorkFlow.WucWorkFlowModule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:PlaceHolder ID="plHolder" runat="server" EnableViewState="false"></asp:PlaceHolder>

 <asp:Panel ID="pnlConfirmacion"  runat="server" CssClass="popup_Container" Width="300" Height="120" style="display:none;">  

        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
              Confirmación.
            </div>
            <div class="TitlebarRight" id="divClose">
            </div>
        </div>

        <div class="popup_Body">  

            <div id="divContinuar" style="display:block;" runat="server">
                <table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">            
                        <tr>
                            <td class="Etiquetas">
                               ¿Confirma que desea continuar?
                            </td>
                        </tr>
                        <tr>                              
                            <td align="center">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelarClick" />
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="BtnAceptarClick" />

                            </td>
                        </tr>
               </table>
            </div>
             <div style="display:none;text-align:center;" id="DivMomento" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                   Un momento Por Favor..
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img id="Img2"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
                                </td>
                            </tr>
                        </table>
                    </div>
                   
        </div>
    </asp:Panel>
    

<asp:Button ID="btnTargetControl" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender 
ID="mpeSearch" 
runat="server" 
TargetControlID="btnTargetControl" 
PopupControlID="pnlConfirmacion" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divClose"> 
</ajaxToolkit:ModalPopupExtender>  