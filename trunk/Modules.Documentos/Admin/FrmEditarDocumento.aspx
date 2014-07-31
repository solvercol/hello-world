<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditarDocumento.aspx.cs" Inherits="Modules.Documentos.Admin.FrmEditarDocumento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ItemSeleccionado(sender, eventArgs) {            
            var fuente = sender.get_element().name.toLowerCase();
            var tipo = "";
            if (fuente.indexOf("subcategoria") != -1) {
                tipo = "subcategoria";
            } else if (fuente.indexOf("categoria") != -1) {
                tipo = "categoria";
            } else if (fuente.indexOf("tipodocumento") != -1) {
                tipo = "tipodocumento";
            }
            $.ajax({
                type: "POST",
                url: "FrmEditarDocumento.aspx/AsignarCategoria",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ Tipo: '" + tipo + "', Id: " + eventArgs.get_value() + "}",
                success: function (response) {
                },
                error: function (xmlRequest) {
                    alert(xmlRequest.status + ' \n\r ' +
                          xmlRequest.statusText + '\n\r' +
                          xmlRequest.responseText);
                }
            });
        }

        function DespuesDeDigitarCategoria(categoria) {
            var contenido = "";
            if (categoria == "SubCategoria") {
                contenido = document.getElementById('<%=txtSubCategoria.ClientID%>').value;
            } else if (categoria == "Categoria") {
                contenido = document.getElementById('<%=txtCategoria.ClientID%>').value;
            } else if (categoria == "TipoDocumento") {
                contenido = document.getElementById('<%=txtTipoDocumento.ClientID%>').value;
            }
            $.ajax({
                type: "POST",
                url: "FrmEditarDocumento.aspx/DespuesDeDigitarCategoria",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{ Categoria: '" + categoria + "', Contenido: '" + contenido + "'}",
                success: function (response) {

                },
                error: function (xmlRequest) {
                    alert(xmlRequest.status + ' \n\r ' +
                          xmlRequest.statusText + '\n\r' +
                          xmlRequest.responseText);
                }
            });
        }
    </script>
    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick"   />
        <asp:Button ID="btnPublicar" runat="server" Text="Publicar" OnClick="BtnPublicarClick"   />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelarClick" CausesValidation="false" />
    </div>
    <div class="group">
        <table width="100%" class="tblSecciones">
             <!--Título del documento-->
             <tr>
                <td class="validator" style="width:1%">
                    *
                </td>
                <th>
                    Título del Documento:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                   <asp:TextBox ID="txtTitulo" runat="server" Width="250px" CssClass="TextUpperCase" MaxLength="512"></asp:TextBox>
                   <asp:RequiredFieldValidator 
                        ID="rfvTxtTitulo" 
                        runat="server" 
                        ControlToValidate="txtTitulo" 
                        CssClass="validator"  
                        Text="Requerido!!"
                        ErrorMessage="Campo Requerido!!"
                        SetFocusOnError="true"
                        Display="Dynamic">
                   </asp:RequiredFieldValidator>
                </td>
             </tr>
             <!--Categoría-->
             <tr>
                <td class="validator" style="width:1%">
                    *
                </td>
                <th>
                    Categoría:
                </th>
                <td class="Separador"></td>
                <td style="width:90%;">
                   <asp:TextBox ID="txtCategoria" runat="server" Width="250px" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:TextBox>
                   <asp:AutoCompleteExtender ID="txtCategoria_AutoCompleteExtender"                         
                        ShowOnlyCurrentWordInCompletionListItem="true" 
                        MinimumPrefixLength="2"
                        runat="server" TargetControlID="txtCategoria"
                        UseContextKey="True" 
                        ServicePath="WSScripts.asmx" 
                        ServiceMethod="GetCategorias"                        
                        OnClientItemSelected="ItemSeleccionado">
                    </asp:AutoCompleteExtender>
                   <asp:RequiredFieldValidator 
                        ID="rfvTxtCategoria"
                        runat="server" 
                        ControlToValidate="txtCategoria" 
                        CssClass="validator"  
                        Text="Requerido!!"
                        ErrorMessage="Campo Requerido!!"
                        SetFocusOnError="true"
                        Display="Dynamic">
                   </asp:RequiredFieldValidator>
                </td>
             </tr>
             <!--Sub Categoría-->
             <tr>
                <td class="validator" style="width:1%">
                    *
                </td>
                <th>
                    Sub Categoría:
                </th>
                <td class="Separador"></td>
                <td style="width:90%">
                   <asp:TextBox ID="txtSubCategoria" runat="server" Width="250px" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:TextBox>                    
                   <asp:AutoCompleteExtender ID="txtSubCategoria_AutoCompleteExtender" 
                        DelimiterCharacters=";, :" 
                        ShowOnlyCurrentWordInCompletionListItem="true" 
                        MinimumPrefixLength="2"
                        runat="server" TargetControlID="txtSubCategoria"
                        UseContextKey="True" 
                        ServicePath="WSScripts.asmx" 
                        ServiceMethod="GetSubCategorias"
                        OnClientItemSelected="ItemSeleccionado">
                    </asp:AutoCompleteExtender>
                   <asp:RequiredFieldValidator 
                        ID="rfvTxtSubCategoria" 
                        runat="server" 
                        ControlToValidate="txtSubCategoria" 
                        CssClass="validator"  
                        Text="Requerido!!"
                        ErrorMessage="Campo Requerido!!"
                        SetFocusOnError="true"
                        Display="Dynamic">
                   </asp:RequiredFieldValidator>
                </td>
             </tr>
             <!--Tipo de Documento-->
             <tr>
                <td class="validator" style="width:1%">
                    *
                </td>
                <th>
                    Tipo de Documento:
                </th>
                <td class="Separador"></td>
                <td style="width:90%">
                   <asp:TextBox ID="txtTipoDocumento" runat="server" Width="250px" 
                       CssClass="TextUpperCase" MaxLength="512"></asp:TextBox>                    
                   <asp:AutoCompleteExtender ID="txtTipoDocumento_AutoCompleteExtender" 
                        DelimiterCharacters=";, :" 
                        ShowOnlyCurrentWordInCompletionListItem="true" 
                        MinimumPrefixLength="2"
                        runat="server" TargetControlID="txtTipoDocumento"
                        UseContextKey="True" 
                        ServicePath="WSScripts.asmx" 
                        ServiceMethod="GetTipoDocumentos"
                        OnClientItemSelected="ItemSeleccionado">
                    </asp:AutoCompleteExtender>
                   <asp:RequiredFieldValidator 
                        ID="rfvTxtTipoDocumento" 
                        runat="server" 
                        ControlToValidate="txtTipoDocumento" 
                        CssClass="validator"  
                        Text="Requerido!!"
                        ErrorMessage="Campo Requerido!!"
                        SetFocusOnError="true"
                        Display="Dynamic">
                   </asp:RequiredFieldValidator>
                </td>
             </tr>
             <!--Responsable del documento-->
             <tr>
                <td class="validator" style="width:1%">
                    *
                </td>
                <th>
                    Responsable del Doc.:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                   <asp:DropDownList ID="ddlResponsableDoc" runat="server" Width="250px"></asp:DropDownList>
                   <asp:RequiredFieldValidator 
                        ID="rfvDdlResponsableDoc" 
                        runat="server" 
                        ControlToValidate="ddlResponsableDoc" 
                        CssClass="validator"  
                        Text="Requerido!!"
                        ErrorMessage="Campo Requerido!!"
                        SetFocusOnError="true"
                        Display="Dynamic">
                   </asp:RequiredFieldValidator>
                </td>
           </tr>
             <!--Archivo-->
             <tr>
                <td class="validator" style="width:1%">
                    *
                </td>
                <th>
                    Contenido:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                    <asp:LinkButton ID="LnkBtnDescargar" runat="server" Font-Names="Arial" Font-Size="Smaller" CausesValidation="false" OnClick="LnkBtnDescargar_Click" Visible="False">Descargar Archivo</asp:LinkButton>
                   <asp:FileUpload ID="FileUploadArchivo" runat="server" CausesValidation="False" />
                </td>
           </tr>
             <!--Observaciones-->
             <tr>
                <td class="validator" style="width:1%">
                    *
                </td>
                <th>
                    Observaciones:
                </th>
                <td class="Separador"></td>
                <td style="width:90%" >
                   <asp:TextBox ID="txtObservaciones" Rows="3" runat="server" Width="250px" CssClass="TextUpperCase" MaxLength="512"></asp:TextBox>
                   <asp:RequiredFieldValidator 
                        ID="rfvTxtObservaciones" 
                        runat="server" 
                        ControlToValidate="txtObservaciones" 
                        CssClass="validator"  
                        Text="Requerido!!"
                        ErrorMessage="Campo Requerido!!"
                        SetFocusOnError="true"
                        Display="Dynamic">
                   </asp:RequiredFieldValidator>
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
                     <asp:CheckBox ID="chkActiva" runat="server" Checked="true"/>
                </td>
            </tr>

        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
