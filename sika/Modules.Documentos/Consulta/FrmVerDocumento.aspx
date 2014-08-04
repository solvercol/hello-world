<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmVerDocumento.aspx.cs" Inherits="Modules.Documentos.Consulta.FrmVerDocumento" %>

<%@ Register Src="~/Pages/Modules/Documentos/UserContrlos/WUCLogCambiosDoc.ascx" TagPrefix="uc1" TagName="WUCLogCambiosDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
    </div>
    <div class="group">
        <table width="100%" class="tblSecciones">
             <tr>
                 <th colspan="4">
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
                <th>
                    Título del Documento:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                    <asp:Label ID="txtTitulo" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
             </tr>
             <!--Categoría-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th>
                    Categoría:
                </th>
                <td class="Separador"></td>
                <td style="width:90%">
                    <asp:Label ID="txtCategoria" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
             </tr>
             <!--Sub Categoría-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th>
                    Sub Categoría:
                </th>
                <td class="Separador"></td>
                <td style="width:90%">
                    <asp:Label ID="txtSubCategoria" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
             </tr>
             <!--Tipo de Documento-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th>
                    Tipo de Documento:
                </th>
                <td class="Separador"></td>
                <td style="width:90%">
                    <asp:Label ID="txtTipoDocumento" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
             </tr>
             <!--Responsable del documento-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th>
                    Responsable del Doc.:
                </th>
                <td class="Separador"></td>
                <td style="width:90%">
                    <asp:Label ID="txtResponsableDoc" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
             </tr>
             <!--Archivo-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th>
                    Contenido:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                    <asp:LinkButton ID="LnkBtnDescargar" runat="server" Font-Names="Arial" Font-Size="Smaller" CausesValidation="false" OnClick="LnkBtnDescargar_Click" Visible="False">Descargar Archivo</asp:LinkButton>
                </td>
             </tr>
            <!--Version-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th>
                    Versión:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                    <asp:Label ID="lblVersion" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
             </tr>
             <!--Observaciones-->
             <tr>
                <td class="validator" style="width:1%">
                </td>
                <th>
                    Observaciones:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                    <asp:Label ID="txtObservaciones" runat="server" Width="250px" ReadOnly="true" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:Label>
                </td>
           </tr>             
            <!--Activo-->
            <tr>
                <td class="validator">              
                </td>
                <th>
                    Activo:
                </th>
                <td class="Separador"></td>
                <td >
                     <asp:CheckBox ID="chkActiva" Enabled="false" runat="server" Checked="true"/>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <uc1:WUCLogCambiosDoc runat="server" ID="WUCLogCambiosDoc" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
