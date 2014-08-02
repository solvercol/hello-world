<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditarBancoActividades.aspx.cs" Inherits="Modules.PlanAccion.Admin.FrmEditarBancoActividades" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick"  CausesValidation="true" />
        <asp:Button ID="btnCancelar" runat="server" Text="Eliminar" OnClick="BtnCancelarClick" CausesValidation="false" />
</div>

    <div class="group">

     <table width="100%" class="tblSecciones" >
         <tr>
            <td class="validator" style="width:1%">
                *
            </td>
            <th style="width:10%">
                Código:
            </th>
            <td class="Separador"></td>
            <td style="width:90%" >
                   <asp:TextBox ID="txtCodigo" runat="server" Width="100px" CssClass="TextUpperCase" ></asp:TextBox>                               
                    <asp:RequiredFieldValidator 
                    ID="rfvTxtCantidad" 
                    runat="server" 
                    ControlToValidate="txtCodigo" 
                    CssClass="validator"  
                    Text="Requerido!!"
                    ErrorMessage="Código Requerido!!"
                    SetFocusOnError="true"
                    Display="Dynamic" ></asp:RequiredFieldValidator>
          </td>
       </tr>

        <tr>
            <td class="validator">
                *
            </td>
            <th valign="top">
                Descripcion:
            </th>
            <td class="Separador"></td>
            <td >
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Height="60" Width="400" CssClass="TextUpperCase"></asp:TextBox>
                               
                    <asp:RequiredFieldValidator 
                    ID="RequiredFieldValidator1" 
                    runat="server" 
                    ControlToValidate="txtDescripcion" 
                    CssClass="validator"  
                    Text="Requerido!!"
                    ErrorMessage="Descripción Requerida!!"
                    SetFocusOnError="true"
                    Display="Dynamic" ></asp:RequiredFieldValidator>
          </td>
       </tr>

        <tr>
            <td class="validator">
                
            </td>
            <th>
                Tiene Pregunta?: 
            </th>
            <td class="Separador"></td>
            <td>
                  <asp:CheckBox ID="chkTienePregunta" runat="server" AutoPostBack="true" OnCheckedChanged="ChkTienePreguntaCheckedChanged" />
          </td>
      </tr>

       <tr>
            <td class="validator">
              
            </td>
            <td colspan="3">
                <div id="divPregunta" runat="server" visible="false" class="BordeGris" >
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <th style="width:10%" valign="top">
                                Pregunta:
                            </th>
                            <td class="Separador"></td>
                            <td style="width:90%">
                                <asp:TextBox ID="txtPregunta" runat="server" MaxLength="250" Width="400" Height="40" TextMode="MultiLine" CssClass="TextUpperCase" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Tipo de Respuesta:
                            </th>
                            <td class="Separador"></td>
                            <td>
                                 <asp:DropDownList ID="ddlTipoRespuesta" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlTiposrespuestaSelectedIndexChanged">
                                 </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trValorRespuesta" runat="server" visible="false">
                            <th valign="top">
                                Valor Respuesta:
                            </th>
                            <td class="Separador"></td>
                            <td>
                                <table cellpadding="0" cellspacing="0" >
                                    <tr>
                                        <td >
                                            <asp:TextBox ID="txtValueInput" runat="server" Width="250" CssClass="TextUpperCase"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnAdd" runat="server" Text="+" OnClick="BtnAddClick" CausesValidation="false"  Width="21px" />&nbsp;
                                            <asp:Button ID="btnRemove" runat="server" Text="-" OnClick="BtnRemoveClick" CausesValidation="false"   Width="21px"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:ListBox ID="lbValues" runat="server" Width="300"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                         <tr id="trRespuestaObligatoria" runat="server" visible="false">
          
                                <th>
                                    Respuesta Obligatoria:
                                </th>
                                <td class="Separador"></td>
                                <td >
                                     <asp:DropDownList id="ddlrespuestaObligatoria" runat="server">
                                        <asp:ListItem Value="S"></asp:ListItem>
                                        <asp:ListItem Value="N"></asp:ListItem>
                                     </asp:DropDownList>
                              </td>
                        </tr>
                    </table>
                
                </div>
            </td>
    </tr>


    <tr>
            <td class="validator">
              
            </td>
            <th>
                Requiere Anexo:
            </th>
            <td class="Separador"></td>
            <td >
                 <asp:CheckBox ID="chkAnexo" runat="server" Checked="false"/>
          </td>
    </tr>

    <tr>
            <td class="validator">
              
            </td>
            <th>
                Requiere Comentarios:
            </th>
            <td class="Separador"></td>
            <td >
                 <asp:CheckBox ID="chkComentarios" runat="server" Checked="false"/>
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
