<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditarDocumento.aspx.cs" Inherits="Modules.Documentos.Admin.FrmEditarDocumento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<script type="text/javascript">

    function RebindScripts() {

        $(".chzn-select").chosen({ allow_single_deselect: true });

        $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    }
       
</script>

<asp:UpdatePanel ID="upGeneral" runat="server">
    <ContentTemplate>  
    <script type="text/javascript" language="javascript">
        Sys.Application.add_load(RebindScripts);
    </script>  
<table width="100%" cellpadding="0" cellspacing="0" >
    
    <tr>
        <td class="SeparadorVertical" colspan="2">            
           
        </td>
    </tr>
    <tr>
        <td valign="top">
            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick"   />
                <asp:Button ID="btnPublicar" runat="server" Text="Publicar" OnClick="BtnPublicarClick"   />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelarClick" CausesValidation="false" />
            </div>  

            <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>

            <table width="100%" class="tblSecciones">
                <!--Título del documento-->
                <tr>
                    <td class="validator" style="width:1%">
                        *
                    </td>
                    <th style="width:5%;text-align:left">
                        Título del Doc.:
                    </th>
                    <td class="Separador" style="width:1%" />
                    <td class="Line" >
                        <asp:TextBox ID="txtTitulo" runat="server" Width="60%" CssClass="TextUpperCase" MaxLength="512"></asp:TextBox>         
                    </td>
                </tr>
                <!--Estado del documento-->
                <tr>     
                    <td class="validator" style="width:1%">
                        
                    </td>               
                    <th style="text-align:left">
                        Estado:
                    </th>
                    <td class="Separador" />
                    <td  class="Line" >
                        <asp:Label ID="lblEstado" runat="server" />
                    </td>
                </tr>
                <!--Categoría-->
                <tr>
                    <td class="validator" >
                        *
                    </td>
                    <th style="text-align:left">
                        Categoría:
                    </th>
                    <td class="Separador" />
                    <td class="Line" >
                        <asp:DropDownList ID="ddlCategoria" class="chzn-select" runat="server" Width="60%" />
                        <asp:LinkButton ID="lnkAddCategoria" runat="server" Text="Agregar nueva Categoría" OnClick="LnkAddCategoria_Click" CommandArgument="1" />
                    </td>
                </tr>
                <!--Sub Categoría-->
                <tr>
                    <td class="validator">
                        *
                    </td>
                    <th style="text-align:left">
                        Sub Categoría:
                    </th>
                    <td class="Separador" />
                    <td class="Line" >
                        <asp:DropDownList ID="ddlSubCategoria" class="chzn-select" runat="server" Width="60%" />                             
                        <asp:LinkButton ID="lnkAddSubCategoria" runat="server" Text="Agregar nueva SubCategoría" OnClick="LnkAddCategoria_Click" CommandArgument="2" />
                    </td>
                </tr>
                <!--Tipo de Documento-->
                <tr>
                    <td class="validator" >
                        *
                    </td>
                    <th style="text-align:left">
                        Tipo de Documento:
                    </th>
                    <td class="Separador"></td>
                    <td class="Line" >
                        <asp:DropDownList ID="ddlTipoDocumento" class="chzn-select" runat="server" Width="60%" /> 
                        <asp:LinkButton ID="lnkAddTipoDocumento" runat="server" Text="Agregar nuevo Tipo de Documento" OnClick="LnkAddCategoria_Click" CommandArgument="3" />
                    </td>
                </tr>
                <!--Responsable del documento-->
                <tr>
                    <td class="validator" >
                        *
                    </td>
                    <th style="text-align:left">
                        Responsable del Doc.:
                    </th>
                    <td class="Separador" />
                    <td class="Line" >
                        <asp:DropDownList ID="ddlResponsableDoc" class="chzn-select" runat="server" Width="60%" />
                    </td>
                </tr>                
                <!--Observaciones-->
                <tr>
                    <td class="validator" style="vertical-align:top">
                        *
                    </td>
                    <th style="text-align:left; vertical-align:top">
                        Observaciones:
                    </th>
                    <td class="Separador" />
                    <td class="Line" >
                        <asp:TextBox ID="txtObservaciones" Rows="3" runat="server" Width="60%" CssClass="TextUpperCase" MaxLength="512" TextMode="MultiLine" />
                    </td>
                </tr>
                <!--Archivo-->
                <tr id="filaAdjuntar" runat="server">
                    <td class="validator" style="vertical-align:top " >
                        
                    </td>
                    <th style="text-align:left; vertical-align:top ">
                        Anexos:
                    </th>
                    <td class="Separador"></td>
                    <td class="Line" >
                        <asp:FileUpload ID="FileUploadArchivo" runat="server" CausesValidation="False" />
                        <asp:Button ID="btnAdjuntar" runat="server" Text="Adjuntar" OnClick="BtnAdjuntarClick" CausesValidation="false" />
                    </td>
                </tr>
                <tr>
                    <td class="validator" >
                        
                    </td>
                    <th style="text-align:left; vertical-align:top ">
                        
                    </th>
                    <td class="Separador" />
                    <td >
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
                                                <th  style="width:79%" align="center" >
                                                        Archivos
                                                </th>
                                            </tr>
                                        </table>  
                                        <asp:Panel id="pnlContainer" style="width:100%;float: left; " Height="100px" ScrollBars="Vertical" runat="server" >
                                            <table width="100%" cellpadding="0" cellspacing="0" class="tbl" >
				                                <asp:repeater id="rptAdjuntos" runat="server" OnItemDataBound="RptArchivosAdjuntos_ItemDataBound">
					                                <itemtemplate>
						                                <tr>
							                                <td style="width:80%" align="left">
                                                                <asp:LinkButton ID="lnkBtnArchivo" runat="server" onclick="LnkBtnArchivoClick"></asp:LinkButton>                                                        
                                                            </td>
							                                <td style="width:20%" align="center">
                                                                <asp:ImageButton ID="ImgBtnEliminar" runat="server" ImageUrl="~/Resources/Images/RemoveGrid.png"                                                                    
                                                                    OnClientClick="return confirm('¿Confirma que desea eliminar el archivo?');"  CausesValidation="false"
                                                                    onclick="ImgBtnEliminarClick"/>                                        
                                                            </td>
						                                </tr>
					                                </itemtemplate> 
                                                    <AlternatingItemTemplate>
                                                        <tr class="AlternateGridStyle">                                           
							                                <td style="width:80%" align="left">
                                                                <asp:LinkButton ID="lnkBtnArchivo" runat="server" onclick="LnkBtnArchivoClick"></asp:LinkButton>                                                        
                                                            </td>
							                                <td style="width:20%" align="center">
                                                                <asp:ImageButton ID="ImgBtnEliminar" runat="server" ImageUrl="~/Resources/Images/RemoveGrid.png"                                                                    
                                                                    OnClientClick="return confirm('¿Confirma que desea eliminar el archivo?');" CausesValidation="false"
                                                                    onclick="ImgBtnEliminarClick"/>                                        
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
        </td>
    </tr>
</table>

<div>
     <asp:Panel ID="pnlPopUpCategoria"  runat="server" CssClass="popup_Container" Width="400" Height="80" style="display:none;">

        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                <asp:Label ID="lblAdminCategoriaTitle" runat="server" Text="Titulo De La Catagoria" />
            </div>
            <div class="TitlebarRight" id="divClosePopUpCaregoria">
            </div>            
        </div>

        <div class="popup_Body">  
                
            <table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">            
                <tr>
                    <td style="width:10%;" class="Etiquetas">
                        Nombre
                    </td>
                    <td class="Separador15"></td>
                    <td valign="middle" style="width:70%;" class="Line">
                        <asp:TextBox ID="txtCategoria" runat="server" Width="90%" MaxLength="100" CssClass="TextUpperCase" />
                    </td>
                    <td align="right" style="width:10%;">   
                        <asp:Button ID="btnSaveCategoria" runat="server" CausesValidation="false" Text="Guardar" OnClick="BtnSaveCategoria_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    

    <asp:Button ID="btnTargetControlAdminCategoria" runat="server" style="display:none; "/>    

    <asp:ModalPopupExtender
    ID="mpeAdminCategoria" 
    runat="server" 
    TargetControlID="btnTargetControlAdminCategoria" 
    PopupControlID="pnlPopUpCategoria"
    BackgroundCssClass="ModalPopupBG" DropShadow="true" 
    cancelcontrolid="divClosePopUpCaregoria"
    > 
    </asp:ModalPopupExtender>
</div>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAdjuntar" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
    <table width="100%">
        <tr >
            <td style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0" >
                <asp:Label ID="lblLogInfo" runat="server" ForeColor="#808080" Font-Size="8pt" />
            </td>
        </tr>
    </table>
</asp:Content>
