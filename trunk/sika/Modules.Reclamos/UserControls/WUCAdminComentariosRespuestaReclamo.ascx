﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminComentariosRespuestaReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminComentariosRespuestaReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<table width="100%">
    <tr class="SectionMainTitle">
        <td >
            LISTA DE COMENTARIOS Y RESPUESTAS
        </td>
    </tr>
    <tr>
        <td >
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnNuevoComentarioRespuesta" runat="server" Text="Registrar Solución" />
            </div>
        </td>
    </tr>
    <tr style="padding:10px;">
        <td >
            <table class="tbl" width="100%">
                <tr>
                    <th style="width:3%;">                        
                    </th>
                    <th style="width:63%;text-align:left;">
                        Descripción
                    </th>
                    <th style="width:14%; text-align:left;">
                        Fecha
                    </th>
                    <th style="width:20%;text-align:left;">
                        Autor
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
                    <td style="text-align:left">
                        Garrafas infladas de SIKA
                    </td>                  
                    <td style="text-align:left;">
                        24/09/2013 12:35:13 pm
                    </td>
                    <td style="text-align:left">
                        Ivan Sanchez
                    </td>                    
                </tr>
            </table>
        </td>
    </tr>    
</table>

<asp:Panel ID="pnlAdminComentarioRespuesta"  runat="server" CssClass="popup_Container" Width="500" Height="300" style="display:none;">  

    <div class="popup_Titlebar" id="PopupHeader">
        <div class="TitlebarLeft">
            Administrar Comentario / Respuesta
        </div>
        <div class="TitlebarRight" id="divClose">
        </div>
    </div>

    <div class="popup_Body">                                                    
        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 25%">
                    Asunto :
                </th>

                <td class="Separador"></td>

                <td class="Line" style="width:70%">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Observaciones :
                </th>

                <td class="Separador"></td>

                <td class="Line">
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <th style="text-align:left">
                    Destinatario :
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
    
<asp:Button ID="btnPopUpAdminComentarioRespuestaTargetControl" runat="server" style="display:none; "/>    

<ajaxToolkit:ModalPopupExtender 
ID="mpeAdminSolucion" 
runat="server" 
TargetControlID="btnNuevoComentarioRespuesta" 
PopupControlID="pnlAdminComentarioRespuesta" 
BackgroundCssClass="ModalPopupBG" 
cancelcontrolid="divClose"> 
</ajaxToolkit:ModalPopupExtender>