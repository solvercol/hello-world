<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmConfiguracionActividades.aspx.cs" Inherits="Modules.PlanAccion.Admin.FrmConfiguracionActividades" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



<div style="padding:3px; text-align:right;">
    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresarClick" CausesValidation="false" />
        
</div>

<asp:UpdatePanel ID="upgeneral" runat="server">
    <ContentTemplate>
    

<table width="100%">
    <tr>
        <td style="width:30%" valign="top">
            <fieldset style=" height:350px;">
            <div id="MyTreeDiv">
               <asp:TreeView 
               ID="tvcategorias" 
               NodeStyle-CssClass="treeNode"
               RootNodeStyle-CssClass="rootNode"
               LeafNodeStyle-CssClass="leafNode"
               runat="server" 
               Width="100%" 
               ExpandDepth="0"
               ShowLines="true" 
               OnSelectedNodeChanged="SelectedNodeChanged"
               OnTreeNodePopulate="TvCategoriasPopulate" >
               </asp:TreeView>
             </div>
            </fieldset>
        </td>
        <td style="width:1%">
        
        </td>
        <td style="width:70%" valign="top">           
           <fieldset style=" height:350px;" >
                <div style=" border-bottom:1px solid #EAEAEA;">
                    <table width="100%" cellpadding="0" cellspacing="0" class="tblPreView">
                        <tr>
                             <td align="left" style="color:#360090; font-weight:bold; font-size:12pt; width:70%;" colspan="2" class="TextUpperCase">                            
                                    <asp:Literal ID="litCategoriaSeleccionada" runat="server"></asp:Literal>                                
                            </td>
                            <td align="right">
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="BtnNuevoClick" Visible="false" CausesValidation="false" />
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick"   Visible="false"  />
                                <asp:Button ID="btnCancelar" runat="server" Text="Eliminar" OnClick="BtnCancelarClick" CausesValidation="false"  Visible="false" />
                            </td>
                        </tr>
                    </table>                   
                </div>

                <asp:Panel ID="pnlConfig" runat="server" Enabled="false" style="padding-top:10px;">
                <table width="100%" class="tblSecciones">
                    
                    <tr>
                        <td style="width:1%" class="validator">
                            *
                        </td>
                        <th style="width:25%">
                            Actividad:
                        </th>
                        <td style="width:1%"></td>
                        <td style="width:75%">
                            <asp:DropDownList ID="ddlActividades" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvActividad" runat="server" 
                            ErrorMessage="El campo actividad es requerido!!" 
                            ControlToValidate="ddlActividades" 
                            Text="**"
                            CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                
                    <tr>
                        <td>
                           
                        </td>
                        <th>
                             Es Obligatoria?:
                        </th>
                        <td>
                        </td>
                        <td>
                              <asp:CheckBox ID="chkObligatoria" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>                           
                        </td>
                        <th>
                             Es Final?:
                        </th>
                        <td>
                        </td>
                        <td>
                              <asp:CheckBox ID="chkEsFinal" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>                           
                        </td>
                        <th>
                             Es Exclusiva?:
                        </th>
                        <td>
                        </td>
                        <td>
                              <asp:CheckBox ID="chkEsExclusiva" runat="server" AutoPostBack="true" OnCheckedChanged="ExclusivaCheckedChanged" />
                        </td>
                    </tr>

                     <tr id="trRolExclusivo" runat="server" visible="false">
                        <td>                           
                        </td>
                        <th>
                             Rol Exclusivo?:
                        </th>
                        <td>
                        </td>
                        <td>
                              <asp:DropDownList ID="ddlRolExclusivo" runat="server"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>                           
                        </td>
                        <th>
                            Preprogramar Actividad?:
                        </th>
                        <td>
                        </td>
                        <td>
                             <asp:CheckBox ID="chkPreprogramar" runat="server" AutoPostBack="true" OnCheckedChanged="PreProgramarCheckedChanged"/>
                        </td>
                    </tr> 
                    <tr id="trDiasHabiles" runat="server" visible="false">
                        <td>                           
                        </td>
                        <th>
                            # Dias Habiles:
                        </th>
                        <td>
                        </td>
                        <td>
                              <ig:WebNumericEditor 
                                ID="txtDiasHabiles" 
                                runat="server" 
                                HorizontalAlign="Left">
                                </ig:WebNumericEditor>
                        </td>
                    </tr>

                     <tr>
                        <td>                           
                        </td>
                        <th>
                             Secuencia:
                        </th>
                        <td>
                        </td>
                        <td>
                              <ig:WebNumericEditor 
                                ID="txtSecuencia" 
                                runat="server" 
                                HorizontalAlign="Left">
                                </ig:WebNumericEditor>
                                <asp:RequiredFieldValidator ID="rfvSecuencia" runat="server" 
                                ErrorMessage="El campo secuencia es requerido!!" 
                                ControlToValidate="txtSecuencia" 
                                Text="**"
                                CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                </table>
                </asp:Panel>
                <div style=" padding-top:15px;">
                 <asp:ValidationSummary 
                   ID="ValidationSummary1" 
                   runat="server" 
                   ShowModelStateErrors="true" 
                   CssClass="validator"
                   HeaderText="Verifique los siguientes errores:"/>
                </div>
          </fieldset>
           
          
        
        </td>
    </tr>
</table>

    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
