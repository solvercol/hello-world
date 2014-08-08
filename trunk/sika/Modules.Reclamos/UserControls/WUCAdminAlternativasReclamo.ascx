<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminAlternativasReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminAlternativasReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTA DE ALTERNATIVAS
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoAlternativa" runat="server" Text="Registrar Alternativa" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <tr>
                    <th style="width:3%;">                        
                    </th>
                    <th style="width:3%;">                        
                    </th>
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
                    <th style="width:20%;text-align:left;">
                        Seguimiento
                    </th>
                </tr>
                <tr>
                    <td style="text-align:center; ">
                        <asp:ImageButton 
                                    ID="imgSelect" 
                                    runat="server"
                                    CausesValidation="false"
                                    BorderStyle="None"
                                    ImageUrl="~/Resources/Images/select.png"  />
                    </td>
                    <td style="text-align:center; ">
                        <asp:ImageButton 
                                    ID="imgDelete" 
                                    runat="server"
                                    CausesValidation="false"
                                    BorderStyle="None"
                                    ImageUrl="~/Resources/Images/RemoveGrid.png"  />
                    </td>
                    <td style="text-align:left">
                        Adición de biocida en la formulación del producto y se implementó mayor control al tratamiento de agua de producción.
                    </td>
                    <td style="text-align:left;">
                        24/09/2013 12:35:13 pm
                    </td>
                    <td style="text-align:left">
                        Albeiro Cadavid
                    </td>
                    <td style="text-align:left; color:#a31717">
                        Enviado
                    </td>   
                    <td style="text-align:left">
                        Esto es el campo de seguimiento
                    </td>                    
                </tr>
            </table>
        </td>
    </tr>    
</table>

<asp:Panel ID="pnlAdminAlternativa"  runat="server" CssClass="popup_Container" Width="500" Height="400" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Administrar Alternativa
        </div>
        <div class="TitlebarRight" id="divClose">
        </div>
    </div>

    <div class="popup_Body">                                                    
        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 30%">
                    Causas :
                </th>

                <td class="Separador"></td>

                <td class="Line" style="width:70%">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Factores :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Alternativas de Solución :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
             <tr>
                <th style="text-align:left">
                    Responsable :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
             <tr>
                <th style="text-align:left">
                    Fecha :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
             <tr>
                <th style="text-align:left">
                    Seguimiento :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    NuevoAnexo :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="btnAdd" runat="server" Text="Agregar" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <table class="tbl" width="100%">
                        <tr>
                            <th style="width:90%">Archivo</th>
                            <th style="width:10%"></th>
                        </tr>
                        <tr>
                            <td>
                                Archivo de Pruebas.pdf
                            </td>
                            <td>
                                 <asp:ImageButton 
                                    ID="imgDeleteAnexo" 
                                    runat="server"
                                    CausesValidation="false"
                                    BorderStyle="None"
                                    ImageUrl="~/Resources/Images/RemoveGrid.png"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
    
<asp:Button ID="btnPopUpAdminAlternativaTargetControl" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender 
ID="mpeAdminAlternativa" 
runat="server" 
TargetControlID="btnNuevoAlternativa" 
PopupControlID="pnlAdminAlternativa" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divClose"> 
</ajaxToolkit:ModalPopupExtender>   