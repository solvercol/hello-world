<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminAnexosSolicitud.ascx.cs" Inherits="Modules.AccionesPC.UserControls.WUCAdminAnexosSolicitud" %>

<asp:UpdatePanel ID="upAdminAnexosSololicitud" runat="server">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0" class="tblSecciones">
            <tr id="trAddAnexos" runat="server">
                <th style="text-align:left; vertical-align:top">
                    Anexos :
                </th>

                <td class="Separador"></td>

                <td class="Line" id>
                    <asp:FileUpload ID="fupAnexoArchivoSolicitud" runat="server" />

                    <asp:Button ID="btnAddArchivoAdjuntoSolicitud" runat="server" Text="Agregar" OnClick="BtnAddArchivoAdjunto_Click" OnClientClick="return ShowSplashModalLoading();" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr id="trAnexos" runat="server">               
                <td class="Line" colspan="3">
                    <table class="tbl" width="100%">
                        <tr>
                            <th style="width:100%">Archivo</th>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlArchivosAdjuntosSolicitud" runat="server" Width="100%" Height="150px" ScrollBars="Vertical">
                                    <table width="100%">
                                        <asp:repeater id="rptArchivosAdjuntosSolicitud" runat="server" OnItemDataBound="RptArchivosAdjuntos_ItemDataBound" >                                                                 
                                            <ItemTemplate>
                                                <tr>
                                                    <td >
                                                        <asp:HiddenField ID="hddIdArchivoSolicitud" runat="server" />                                                                
                                                        <asp:LinkButton ID="lnkNombreArchivoSolicitud" runat="server" OnClick="BtnDownloadArchivoAdjunto_Click" />
                                                    </td>
                                                    <td style="width:27px;" >
                                                            <asp:ImageButton 
                                                            ID="imgDeleteAnexoSolicitud" 
                                                            runat="server"
                                                            CausesValidation="false"
                                                            BorderStyle="None"
                                                            ImageUrl="~/Resources/Images/RemoveGrid.png"
                                                            OnClick="BtnRemoveArchivoAdjunto_Click"
                                                            />
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
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddArchivoAdjuntoSolicitud" />
    </Triggers>
</asp:UpdatePanel>