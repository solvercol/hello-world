﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmVerHistDocumento.aspx.cs" Inherits="Modules.Documentos.Consulta.FrmVerHistDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
    </div>
    <div class="group">
        <asp:HiddenField ID="HdfIdDocumento" runat="server" />
        <table width="100%" class="tblSecciones">
             <tr>
                 <th colspan="5">
                     <div style="padding:3px; text-align:left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="LblTitulo" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512" Font-Bold="True" Font-Names="Arial"></asp:Label>
                     </div>
                 </th>
             </tr>
             <!--Título del documento-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th style="width:15%;text-align:left">
                    Título del Documento:
                </th>
                <td class="Separador"></td>
                <td style="width:35%">
                    <asp:Label ID="txtTitulo" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
                <td rowspan="7" style="width:49%;vertical-align:top">
                    <%--Tablas de Adjuntos--%>
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2" id="tdCollapse" runat="server" class="ToolBar">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel id="pnlDetalle" style="width:100%;float: left; " runat="server" >                            
                                    <table class="tbl" cellpadding="0" cellspacing="0" width="100%" style="height:15px">
                                       <tr>
                                            <th  style="width:100%" align="center" >
                                                    Archivos
                                            </th>
                                        </tr>
                                    </table>  
                                    <asp:Panel id="pnlContainer" style="width:100%;float: left; " Height="230px" ScrollBars="Auto" runat="server" >
                                        <table width="100%" cellpadding="0" cellspacing="0" class="tbl" >
				                            <asp:repeater id="rptAdjuntos" runat="server">
					                            <itemtemplate>
						                            <tr>
							                            <td style="width:100%" align="left">
                                                            <asp:LinkButton ID="lnkBtnArchivo" runat="server" Width="350px" 
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdDocumentoAdjuntoHistorial")%>' 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "NombreArchivo")%>' onclick="lnkBtnArchivo_Click"></asp:LinkButton>                                                        
                                                        </td>
						                            </tr>
					                            </itemtemplate> 
                                                <AlternatingItemTemplate>
                                                 <tr class="AlternateGridStyle">                                           
							                            <td style="width:100%" align="left">
                                                            <asp:LinkButton ID="lnkBtnArchivo" runat="server" Width="350px" 
                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdDocumentoAdjuntoHistorial")%>' 
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "NombreArchivo")%>' onclick="lnkBtnArchivo_Click"></asp:LinkButton>                                                        
                                                        </td>
						                         </tr>
                                               </AlternatingItemTemplate>
				                            </asp:repeater>
			                              </table>                      
                                    </asp:Panel>                         
                                </asp:Panel>
                            </td>
                        </tr>
                   </table>
                </td>
             </tr>
             <!--Categoría-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th style="width:15%;text-align:left">
                    Categoría:
                </th>
                <td class="Separador"></td>
                <td style="width:35%">
                    <asp:Label ID="txtCategoria" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
                <td style="width:49%"></td>
             </tr>
             <!--Sub Categoría-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th style="width:15%;text-align:left">
                    Sub Categoría:
                </th>
                <td class="Separador"></td>
                <td style="width:35%">
                    <asp:Label ID="txtSubCategoria" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
                <td style="width:49%"></td>
             </tr>
             <!--Tipo de Documento-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th style="width:15%;text-align:left">
                    Tipo de Documento:
                </th>
                <td class="Separador"></td>
                <td style="width:35%">
                    <asp:Label ID="txtTipoDocumento" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
                <td style="width:49%"></td>
             </tr>
             <!--Responsable del documento-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th style="width:15%;text-align:left">
                    Responsable del Doc.:
                </th>
                <td class="Separador"></td>
                <td style="width:35%">
                    <asp:Label ID="txtResponsableDoc" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
                <td style="width:49%"></td>
             </tr>
            <!--Version-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th style="width:15%;text-align:left">
                    Versión:
                </th>
                <td class="Separador"></td>
                <td style="width:35%" >
                    <asp:Label ID="lblVersion" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
                <td style="width:49%"></td>
             </tr>
             <!--Observaciones-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th style="width:15%;text-align:left">
                    Observaciones:
                </th>
                <td class="Separador"></td>
                <td style="width:35%" >
                    <asp:Label ID="txtObservaciones" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
                <td style="width:49%"></td>
           </tr>             
        </table>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>