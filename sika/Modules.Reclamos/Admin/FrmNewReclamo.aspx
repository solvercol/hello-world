<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmNewReclamo.aspx.cs" Inherits="Modules.Reclamos.Admin.FrmNewReclamo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">
        var divModal = 'DivModal';

        function ShowSplashModalLoading() {
            var adiv = $get(divModal);
            adiv.style.visibility = 'visible';
        }
    </script>

    <script type="text/javascript">

        function RebindScripts() {
            $(".chzn-select").chosen({ allow_single_deselect: true });

            $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }           
 
    </script>        
        
 <asp:UpdatePanel ID="upgeneral" runat="server">
    <ContentTemplate>    
        <script type="text/javascript" language="javascript">
            Sys.Application.add_load(RebindScripts);
        </script>  

        <table width="100%" class="tblSecciones">
            <tr>
                <th style="text-align:left; width: 25%">
                    Tipo de Reclamo :
                </th>

                <td class="Separador"></td>

                <td class="Line" style="width:70%">
                    <asp:RadioButtonList ID="rblReclamoType" RepeatLayout="Table" RepeatColumns="2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RblReclamoType_Changed">
                        <asp:ListItem Text="Producto" Value="Producto" Selected="True" />
                        <asp:ListItem Text="Servicio" Value="Servicio" />
                    </asp:RadioButtonList>
                </td>

                <td class="Separador"></td>
            </tr>
            <tr id="trCategoriaReclamo" runat="server" visible="false">
                <th style="text-align:left; width: 25%">
                    Categoría :
                </th>

                <td class="Separador"></td>

                <td class="Line" style="position: absolute; z-index: 1010;" >
                    <asp:DropDownList ID="ddlCategoriaReclamo" runat="server" Width="350px"  class="chzn-select" />
                </td>

                <td class="Separador"></td>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="padding:3px; text-align:right;">
                        <asp:Button ID="btnConfirmNewReclamo" runat="server" Text="Continuar" OnClick="BtnConfirmNuevoReclamo_Click" />
                    </div>
                </td>
            </tr>
        </table>    
    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>