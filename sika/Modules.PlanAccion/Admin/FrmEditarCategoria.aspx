<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditarCategoria.aspx.cs" Inherits="Modules.PlanAccion.Admin.FrmEditarCategoria" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick"   />
        <asp:Button ID="btnCancelar" runat="server" Text="Eliminar" OnClick="BtnCancelarClick" CausesValidation="false" />
    </div>

    <div class="group">

     <table width="100%" class="tblSecciones">
         <tr>
            <td class="validator" style="width:1%">
                *
            </td>
            <th>
                Secuencia:
            </th>
            <td class="Separador"></td>
            <td >
                    <ig:WebNumericEditor 
                    ID="txtSecuencia" 
                    runat="server" 
                    HorizontalAlign="Left">
                    </ig:WebNumericEditor>
                               
                    <asp:RequiredFieldValidator 
                    ID="rfvTxtCantidad" 
                    runat="server" 
                    ControlToValidate="txtSecuencia" 
                    CssClass="validator"  
                    Text="**"
                    ErrorMessage="Secuencia Requerida!!"
                    SetFocusOnError="true"
                    Display="Dynamic" ></asp:RequiredFieldValidator>
          </td>
       </tr>

        <tr>
            <td class="validator">
                *
            </td>
            <th>
                Descripcion:
            </th>
            <td class="Separador"></td>
            <td >
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Height="60" Width="400"></asp:TextBox>
                               
                    <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    runat="server" 
                    ControlToValidate="txtDescripcion" 
                    CssClass="validator"  
                    Text="**"
                    ErrorMessage="Descripción Requerida!!"
                    SetFocusOnError="true"
                    Display="Dynamic" ></asp:RequiredFieldValidator>
          </td>
       </tr>

        <tr>
            <td class="validator">
                *
            </td>
            <th>
                No.Mínimo Actividades:
            </th>
            <td class="Separador"></td>
            <td >
                    <ig:WebNumericEditor 
                    ID="txtNumeroMinimo" 
                    runat="server" 
                    HorizontalAlign="Left">
                    </ig:WebNumericEditor>
                               
                    <asp:RequiredFieldValidator 
                    ID="rfvDescripcion" 
                    runat="server" 
                    ControlToValidate="txtNumeroMinimo" 
                    CssClass="validator"  
                    Text="**"
                    ErrorMessage="No.Mpinimo Requerida!!"
                    SetFocusOnError="true"
                    Display="Dynamic" ></asp:RequiredFieldValidator>
          </td>
    </tr>

       <tr>
            <td class="validator">
              
            </td>
            <th>
                Activa:
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
