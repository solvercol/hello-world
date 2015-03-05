<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WuCAdminDocumentosAnexoReclamo.ascx.cs" Inherits="Modules.Reclamos.UserControls.WuCAdminDocumentosAnexoReclamo" %>

<%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>

<table width="100%">
    <tr>
        <td style="width:50%">
            <asp:DropDownList ID="ddlCategoria" runat="server" Width="250px"  class="chzn-select" OnSelectedIndexChanged="DdlCategoria_IndexChanged" AutoPostBack="true" />
        </td>
        <td style="width:50%">
            <div style="text-align:right;">
                <asp:Button ID="btnAddDocumento" runat="server" Text="Nuevo Documento" OnClick="BtnAddAnexo_Click" />
            </div>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top" colspan="2" >
            <table class="tbl" width="100%">
                <tr>
                    <th style="width:3%;">                        
                    </th>
                    <th style="width:12%;text-align:left;vertical-align:top">
                        Titulo
                    </th>
                    <th style="width:20%;text-align:left;vertical-align:top">
                        Descripción
                    </th>
                    <th style="width:10%;text-align:left;vertical-align:top">
                        Categoría
                    </th>
                    <th style="width:20%;text-align:left; vertical-align:top">
                        Documento
                    </th>
                    <th style="width:13%; text-align:left;;vertical-align:top">
                        Creado Por
                    </th>
                    <th style="width:10%; text-align:left;;vertical-align:top">
                        Fecha
                    </th>   
                    <th style="width:2%; text-align:left;;vertical-align:top">                        
                    </th>                            
                </tr>
            </table>
            <asp:Panel id="pnlContainerAnexosList" style="width:100%;float: left; " Height="150px" ScrollBars="Vertical" runat="server" >
                <table class="tbl" width="100%">
                    <asp:repeater id="rptAnexosList" runat="server" OnItemDataBound="RptAnexosList_ItemDataBound" >   
                        <ItemTemplate>
                            <tr class="Normal">
                                <td style="text-align:center;width:3%;vertical-align:top ">
                                    <asp:ImageButton ID="btnImgeAnexoReclamo" runat="server" Width="15px" Height="15px" OnClick="BtnImgeAnexoReclamo_Click" />
                                </td>                                    
                                <td style="text-align:left;width:12%;vertical-align:top">
                                    <asp:Label ID="lblTitulo" runat="server" />
                                </td>
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblCategoria" runat="server" />
                                </td>
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:LinkButton ID="btnDownLoadFile" runat="server" OnClick="BtnDownLoadAnexo_Click" />
                                </td>  
                                <td style="text-align:left;width:13%;vertical-align:top">
                                    <asp:Label ID="lblCreadoPor" runat="server" />
                                </td> 
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFecha" runat="server" />
                                </td>         
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="Alternative">
                                <td style="text-align:center;width:3%;vertical-align:top ">
                                    <asp:ImageButton ID="btnImgeAnexoReclamo" runat="server" Width="15px" Height="15px" OnClick="BtnImgeAnexoReclamo_Click" />
                                </td>                                    
                                <td style="text-align:left;width:12%;vertical-align:top">
                                    <asp:Label ID="lblTitulo" runat="server" />
                                </td>
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:Label ID="lblDescripcion" runat="server" />
                                </td>
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblCategoria" runat="server" />
                                </td>
                                <td style="text-align:left;width:20%;vertical-align:top">
                                    <asp:LinkButton ID="btnDownLoadFile" runat="server" OnClick="BtnDownLoadAnexo_Click" />
                                </td>  
                                <td style="text-align:left;width:13%;vertical-align:top">
                                    <asp:Label ID="lblCreadoPor" runat="server" />
                                </td> 
                                <td style="text-align:left;width:10%;vertical-align:top">
                                    <asp:Label ID="lblFecha" runat="server" />
                                </td>         
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                </table>
            </asp:Panel>            
        </td>
    </tr>    
</table>

<asp:UpdatePanel ID="upModal" runat="server">
    <ContentTemplate> 
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>

         <ig:WebDialogWindow 
            ID="wdwAdminDocumentoAnexoReclamo" 
            runat="server" 
            CssClass="WebDialogWindowStyle" 
            Height="470px"
            Width="530px" 
            InitialLocation="Centered" 
            MaintainLocationOnScroll="True" 
            Modal="True" 
            Moveable="true"
            Left="0px"
            Top="0px" 
            ModalBackgroundCssClass="ModalWebDialogWindowStyle" 
            WindowState="Hidden">
            <Header>
                <CloseBox Visible="true" />
            </Header>
            <ContentPane>
                <Template> 

        <asp:Panel ID="pnlAdminAnexos"  runat="server" CssClass="popup_Container"  Width="97%">  

            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    Administrar Documento Contrato
                </div>
                <div class="TitlebarRight" id="divCloseAdminAnexos">
                </div>
            </div>

            <div style="padding:3px; text-align:right;">
                <asp:Button ID="btnCancelarAnexo" runat="server" Text="Regresar" OnClick="BtnCancelarAnexo_Click" />
                <asp:Button ID="btnSaveAnexo" runat="server" Text="Guardar Documento" OnClick="BtnSaveDocumento_Click"  />
            </div>

            <asp:ValidationSummary ID="vsAnexos" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vsAnexos"/>

            <div class="popup_Body">                                                    
                <table width="100%" class="tblSecciones">
                    <tr>
                        <th style="text-align:left; width: 25%; vertical-align:top">
                            Titulo :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:TextBox ID="txtTitulo" runat="server" Width="90%" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left; width: 25%; vertical-align:top">
                            Descripción :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line" style="width:70%">
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="90%" TextMode="MultiLine" Rows="4" />
                        </td>

                        <td class="Separador"></td>
                    </tr>
                    <tr >
                        <th style="text-align:left; vertical-align:top">
                            Categoría :
                        </th>

                        <td class="Separador"></td>

                        <td class="Line">
                            <asp:DropDownList ID="ddlCategoriaDocumento" runat="server" Width="350px"  class="chzn-select" />
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
                        </td>

                        <td class="Separador"></td>
                    </tr>                    
                </table>
            </div>
        </asp:Panel>

        </Template>
            </ContentPane>
        </ig:WebDialogWindow>    
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>