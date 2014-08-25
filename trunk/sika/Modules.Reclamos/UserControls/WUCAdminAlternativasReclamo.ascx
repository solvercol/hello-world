<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminAlternativasReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminAlternativasReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTA DE ALTERNATIVAS
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoAlternativa" runat="server" Text="Registrar Alternativa" OnClick="BtnAddAlternativa_Click" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <asp:repeater id="rptAlternativasList" runat="server" OnItemDataBound="RptAlternativasList_ItemDataBound" >
                    <HeaderTemplate>
                        <tr>
                            <th style="width:3%;">                        
                            </th>
                            <%--<th style="width:3%;">                        
                            </th>--%>
                            <th style="width:30%;text-align:left;">
                                Alternativa
                            </th>
                            <th style="width:14%; text-align:left;">
                                Fecha
                            </th>
                            <th style="width:20%;text-align:left;">
                                Responsable
                            </th>
                            <th style="width:10%;text-align:left; color:#a31717">
                                Estado
                            </th>
                            <th style="width:23%;text-align:left;">
                                Seguimiento
                            </th>
                        </tr>
                    </HeaderTemplate>   
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center; ">
                                <asp:HiddenField ID="hddIdAlternativa" runat="server" />
                                <asp:ImageButton 
                                            ID="imgSelectAlternativa" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/select.png" 
                                            OnClick="BtnSelectAlternativa_Click" />
                            </td>
                            <%--<td style="text-align:center; ">
                                <asp:ImageButton 
                                            ID="imgDeleteAlternativa" 
                                            runat="server"
                                            CausesValidation="false"
                                            BorderStyle="None"
                                            ImageUrl="~/Resources/Images/RemoveGrid.png"
                                            OnClick="BtnRemoveAlternativa_Click" />
                            </td>--%>
                            <td style="text-align:left">
                                <asp:Label ID="lblAlternativa" runat="server" />
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblFechaAlternativa" runat="server" />
                            </td>
                            <td style="text-align:left">
                                <asp:Label ID="lblResponable" runat="server" />
                            </td>
                            <td style="text-align:left; color:#a31717">
                                <asp:Label ID="lblEstado" runat="server" />
                            </td>   
                            <td style="text-align:left">
                                <asp:Label ID="lblSeguimiento" runat="server" />
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
        <asp:Panel ID="pnlAdminAlternativa"  runat="server" CssClass="popup_Container" Width="500" Height="430" style="display:none;">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Alternativa
                </div>
                <div class="TitlebarRight" id="divCloseAdminAlternativa">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnSaveAlternativa_Click"  />
            </div>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 30%; vertical-align:top">
                            Causas :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:TextBox ID="txtCausas" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                            <asp:Label ID="lblCausas" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Factores :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFactores" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                            <asp:Label ID="lblFactores" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Alternativas de Solución :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtAlternativa" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                            <asp:Label ID="lblAlternativa" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                     <tr>
                        <th style="text-align:left; vertical-align:top">
                            Responsable :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <ig:WebDropDown ID="wddResponsable" 
                                            runat="server" 
                                            EnableMultipleSelection="false"
                                            MultipleSelectionType="Checkbox" 
                                            DisplayMode="DropDown"
                                            EnableClosingDropDownOnSelect="false"
                                            StyleSetName="Claymation"
                                            DropDownContainerWidth="300px"
                                            DropDownContainerHeight="220px"
                                            Width="98%">
                            </ig:WebDropDown>
                            <asp:Label ID="lblResponsable" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                     <tr>
                        <th style="text-align:left; vertical-align:top">
                            Fecha :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtFechaAlternativa" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender 
                                ID="cexTxtFechaAlternativa" 
                                runat="server"  
                                TargetControlID="txtFechaAlternativa" 
                                PopupPosition="Right" 
                                PopupButtonID="txtFechaAlternativa"
                                Format="dd/MM/yyyy"
                                CssClass="cal_Theme1" />
                            <asp:Label ID="lblFechaAlternativa" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                     <tr>
                        <th style="text-align:left; vertical-align:top">
                            Seguimiento :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:TextBox ID="txtSeguimiento" runat="server" TextMode="MultiLine" Rows="3" Width="90%" />
                            <asp:Label ID="lblSeguimiento" runat="server" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; vertical-align:top">
                            Anexos :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:FileUpload ID="fupAnexoArchivo" runat="server" />

                            <asp:Button ID="btnAddArchivoAdjunto" runat="server" Text="Agregar" OnClick="BtnAddArchivoAdjunto_Click" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding-left:2px">
                            <table class="tbl" width="100%">
                                <tr>
                                    <th style="width:90%">Archivo</th>
                                    <th style="width:10%"></th>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlArchivosAdjuntos" runat="server" Width="100%" Height="65px" ScrollBars="Vertical">
                                            <table width="100%">
                                                <asp:repeater id="rptArchivosAdjuntos" runat="server" OnItemDataBound="RptArchivosAdjuntos_ItemDataBound" >                                                                 
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td >
                                                                <asp:HiddenField ID="hddIdArchivo" runat="server" />                                                                
                                                                <asp:LinkButton ID="lnkNombreArchivo" runat="server" OnClick="BtnDownloadArchivoAdjunto_Click" />
                                                            </td>
                                                            <td style="width:27px;" >
                                                                 <asp:ImageButton 
                                                                    ID="imgDeleteAnexo" 
                                                                    runat="server"
                                                                    CausesValidation="false"
                                                                    BorderStyle="None"
                                                                    ImageUrl="~/Resources/Images/RemoveGrid.png"
                                                                    OnClick="BtnRemoveArchivoAdjunto_Click"  />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:repeater>
                                            </table>                                            
                                        </asp:Panel>
                                    </td>
                                </tr>                                
                            </table>
                        </td>
                        <td class="Separador"></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    
        <asp:Button ID="btnPopUpAdminAlternativaTargetControl" runat="server" style="display:none; "/>    

        <ajaxToolkit:ModalPopupExtender 
        ID="mpeAdminAlternativa" 
        runat="server" 
        TargetControlID="btnPopUpAdminAlternativaTargetControl" 
        PopupControlID="pnlAdminAlternativa" 
        BackgroundCssClass="ModalPopupBG" DropShadow="true" 
        cancelcontrolid="divCloseAdminAlternativa"> 
        </ajaxToolkit:ModalPopupExtender>   
</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjunto" />
    </Triggers>
</asp:UpdatePanel>