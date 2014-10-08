<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmVerDocumento.aspx.cs" Inherits="Modules.Documentos.Consulta.FrmVerDocumento" %>

<%@ Register Src="~/Pages/Modules/Documentos/UserContrlos/WUCLogCambiosDoc.ascx" TagPrefix="uc1" TagName="WUCLogCambiosDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<table width="100%" cellpadding="0" cellspacing="0" >
    
    <tr>
        <td class="SeparadorVertical" colspan="2">            
           
        </td>
    </tr>
    <tr>
        <td valign="top">
            <div style="padding:3px; text-align:right; position:relative; top:3px; z-index:1">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="BtnEditar_Click" CausesValidation="false" />
            </div>
            <table width="100%" style="position:relative; top:-25px;" >
                <tr>
                    <td colspan="4" class="SeccionesH1">                    
                        <asp:Label ID="LblTitulo" runat="server" />
                        <asp:Label ID="lblVersionTitle" Font-Size="8pt" runat="server" Text="Versión:" />
                        <asp:Label ID="lblVersion" Font-Size="8pt" runat="server" />
                    </td>
                </tr>
                <!--Título del documento-->
                <tr>                    
                    <td class="SeccionesH3" style="width:5%;text-align:left">
                        Título del Documento:
                    </td>
                    <td class="Separador" style="width:1%" />
                    <td style="width:35%" class="SeccionesH4" >
                        <asp:Label ID="txtTitulo" runat="server" />
                    </td>
                </tr>
                <!--Estado del documento-->
                <tr>                    
                    <td class="SeccionesH3" style="text-align:left">
                        Estado:
                    </td>
                    <td class="Separador" />
                    <td  class="SeccionesH4" >
                        <asp:Label ID="lblEstado" runat="server" />
                    </td>
                </tr>
                <!--Categoría-->
                <tr>
                    <td style="text-align:left" class="SeccionesH3">
                        Categoría:
                    </td>
                    <td class="Separador" style="width:1%" />
                    <td class="SeccionesH4" >
                        <asp:Label ID="txtCategoria" runat="server" />
                    </td>
                </tr>
                <!--Sub Categoría-->
                <tr>
                    <td class="SeccionesH3" style="text-align:left">
                        Sub Categoría:
                    </td>
                    <td class="Separador" style="width:1%" />
                    <td class="SeccionesH4" >
                        <asp:Label ID="txtSubCategoria" runat="server" />
                    </td>
                </tr>
                <!--Tipo de Documento-->
                <tr>
                    <td class="SeccionesH3" style="text-align:left">
                        Tipo de Documento:
                    </td>
                    <td class="Separador" style="width:1%" />
                    <td class="SeccionesH4" >
                        <asp:Label ID="txtTipoDocumento" runat="server" />
                    </td>
                </tr>
                <!--Responsable del documento-->
                <tr>
                    <td class="SeccionesH3" style="text-align:left">
                        Responsable del Doc.:
                    </td>
                    <td class="Separador" style="width:1%" />
                    <td class="SeccionesH4" >
                        <asp:Label ID="txtResponsableDoc" runat="server" />
                    </td>
                </tr>
                <!--Observaciones-->
                <tr>
                    <td class="SeccionesH3" style="text-align:left">
                        Observaciones:
                    </td>
                    <td class="Separador" style="width:1%" />
                    <td class="SeccionesH4" >
                        <asp:Label ID="txtObservaciones" runat="server" />
                    </td>
                </tr>    
                <tr id="trAnexos" runat="server">
                    <td class="SeccionesH3" style="text-align:left; vertical-align:top">
                        Anexos:
                    </td>
                    <td class="Separador" />
                    <td >
                        <asp:Panel id="pnlDetalle" style="width:100%;float: left; " runat="server" >                            
                            <table class="tbl" cellpadding="0" cellspacing="0" width="100%" style="height:15px">
                                <tr>
                                    <th  style="width:100%" align="center" >
                                            Archivos
                                    </th>
                                </tr>
                            </table>  
                            <asp:Panel id="pnlContainer" style="width:100%;float: left; " Height="80px" ScrollBars="Vertical" runat="server" >
                                <table width="100%" cellpadding="0" cellspacing="0" class="tbl" >
				                    <asp:repeater id="rptAdjuntos" runat="server">
					                    <itemtemplate>
						                    <tr>
							                    <td style="width:100%" align="left">
                                                    <asp:LinkButton ID="lnkBtnArchivo" runat="server" Width="350px" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdDocumentoAdjunto")%>' 
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "NombreArchivo")%>' onclick="lnkBtnArchivo_Click"></asp:LinkButton>                                                        
                                                </td>
						                    </tr>
					                    </itemtemplate> 
                                        <AlternatingItemTemplate>
                                            <tr class="AlternateGridStyle">                                           
							                    <td style="width:100%" align="left">
                                                    <asp:LinkButton ID="lnkBtnArchivo" runat="server" Width="350px" 
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdDocumentoAdjunto")%>' 
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
</table>            
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">    
    <uc1:WUCLogCambiosDoc runat="server" ID="WUCLogCambiosDoc" />
    <table width="100%">
        <tr >
            <td style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" >
                <asp:Label ID="lblLogInfo" runat="server" ForeColor="#808080" Font-Size="8pt" />
            </td>
        </tr>
    </table>
</asp:Content>