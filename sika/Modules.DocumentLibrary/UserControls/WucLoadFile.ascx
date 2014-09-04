<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucLoadFile.ascx.cs" Inherits="Modules.DocumentLibrary.UserControls.WucLoadFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:UpdatePanel ID="upInputWindows" runat="server">
        <ContentTemplate>
            

        <asp:Panel ID="pnlFechaEntrega"  runat="server" CssClass="popup_Container" Width="400" Height="200" style="display:none;">  
            
              

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                  <asp:Literal ID="litTitulo" runat="server"></asp:Literal>
                </div>
                <div class="TitlebarRight" id="divCloseMensajes">
                </div>
            </div>

            <div class="popup_Body">      
                <asp:PlaceHolder ID="phloadControlLoadFile" runat="server"></asp:PlaceHolder>                  
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeLoad" 
        runat="server" 
        TargetControlID="btnTargetControl" 
        PopupControlID="pnlFechaEntrega" 
        BackgroundCssClass="ModalPopupBG" 
        cancelcontrolid="divCloseMensajes"> 
        </ajaxToolkit:ModalPopupExtender>   

        </ContentTemplate>
    </asp:UpdatePanel>
   

