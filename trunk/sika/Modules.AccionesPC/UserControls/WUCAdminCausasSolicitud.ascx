<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminCausasSolicitud.ascx.cs" Inherits="Modules.AccionesPC.UserControls.WUCAdminCausasSolicitud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            CAUSAS
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnAddCausa" runat="server" Text="Registrar Causa" OnClick="BtnAddCausa_Click" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <asp:repeater id="rptCausasList" runat="server" OnItemDataBound="RptCausasList_ItemDataBound" >
                    <HeaderTemplate>
                        <tr>
                            <th style="width:3%;">                        
                            </th>
                            <th style="width:42%;text-align:left;vertical-align:top">
                                Descripción
                            </th>
                            <th style="width:35%; text-align:left;;vertical-align:top">
                                Comentarios
                            </th>
                            <th style="width:20%;text-align:left;vertical-align:top">
                                Autor
                            </th>
                        </tr>
                    </HeaderTemplate>   
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center; ">
                                <asp:HiddenField ID="hddIdCausa" runat="server" />
                                <asp:ImageButton 
                                            ID="imgSelectCausa" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/select.png"
                                            OnClick="BtnSelectCausa_Click" />
                            </td>                                    
                            <td style="text-align:left">
                                <asp:Label ID="lblDescripcion" runat="server" />
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblComentarios" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblAutor" runat="server" />
                            </td>                    
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
            </table>
        </td>
    </tr>    
</table>

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>
        <asp:Panel ID="pnlAdminCausa"  runat="server" CssClass="popup_Container" Width="500" Height="250" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Causa
                </div>
                <div class="TitlebarRight" id="divCloseAdminCausa">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnSaveActividad_Click" />
            </div>

            <asp:ValidationSummary ID="vsCausas" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vsCausas"/>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%;">
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%;">
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Comentarios :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtComentarios" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>                    
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopUpAdminCausaTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeAdminCausa" 
        runat="server" 
        TargetControlID="btnPopUpAdminCausaTargetControl" 
        PopupControlID="pnlAdminCausa" 
        BackgroundCssClass="ModalPopupBG" DropShadow="true" 
        cancelcontrolid="divCloseAdminCausa"> 
        </ajaxToolkit:ModalPopupExtender>   
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>