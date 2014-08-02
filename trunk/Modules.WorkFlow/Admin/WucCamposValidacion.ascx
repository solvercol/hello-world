<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucCamposValidacion.ascx.cs" Inherits="Modules.WorkFlow.Admin.WucCamposValidacion" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>    
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>


<table width="100%" cellpadding="0" cellspacing="0" style=" margin-top:10px;">
        <tr>
            <td style=" width:30%" valign="top">
               <div style=" border:1px solid #EEEEEE; height:350px;">
                <ig:WebDataMenu runat="server" ID="ContextMenu" IsContextMenu="true" OnItemClick="ContextMenuClick"
                    BorderWidth="1" BorderColor="#CCCCCC" EnableScrolling="false">
                    <ClientEvents ItemClick="MenuItem_Click" />
                    <AutoPostBackFlags ItemClick="On" />
                    <Items>
                        <ig:DataMenuItem Text="Seleccionar Validación" Key="Select" ImageUrl="~/Resources/Images/select.png" />
                        <ig:DataMenuItem Text="Nueva Validación" Key="New" ImageUrl="~/Resources/Images/ChildNode3.png" />
                        <ig:DataMenuItem Text="Editar Validación" Key="Edit" ImageUrl="~/Resources/Images/edit.png" />
                        <ig:DataMenuItem Text="Expandir/Contraer" Key="expand" ImageUrl="~/Resources/Images/expand.png" />                        
                    </Items>
                </ig:WebDataMenu>
                <div class="BordeGris">
                    <table width="100%" class="tblSecciones">
                        <tr>
                            <th>
                                Filtrar:
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFiltroEstados" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlFiltroselectedIndexChanged"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <ig:WebDataTree ID="wdtRutas" runat="server" Width="300px"
                        InitialExpandDepth="1">
                        <ClientEvents NodeClick="Node_Click" />                        
                    </ig:WebDataTree>
                </div>
                <input type="hidden" id="hndNodeSelected" runat="server" />
                </div> 
            </td>
            <td style="width:2%;"></td>
            <td style=" width:68%"  valign="top">    
                      
               <asp:DetailsView 
               AutoGenerateRows="False" 
               DataKeyNames="IdRequerido" 
               HeaderText="Validación de Campos" 
               ID="dvRutas" 
               OnDataBound="OnDataBoundEvent"
               runat="server" 
               BorderStyle="Solid" 
               BorderWidth="1px" EmptyDataText="..."
               BorderColor="Black"
               Width="600px" GridLines="None">
                    
                    <Fields >                       

                        <asp:TemplateField HeaderText="Estado" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litEstadoInicial" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEstadosEdit" runat="server" ></asp:DropDownList>
                                <input type="hidden" id="hdnEstadoInicial"  runat="server" value='<%# Bind("IdEstado") %>' />
                                <asp:RequiredFieldValidator 
                                ID="rfvEstadoInicial" 
                                runat="server"
                                ControlToValidate="ddlEstadosEdit" 
                                ErrorMessage="Campo [Estado] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlEstadosEdit" runat="server" ></asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                ID="rfvEstadoInicial" 
                                runat="server"
                                ControlToValidate="ddlEstadosEdit" 
                                ErrorMessage="Campo [Estado] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Campo a Validar" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litCampoValidar" runat="server" Text='<%# Bind("CampoValidar") %>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCampoValidarEdit" runat="server" Text='<%# Bind("CampoValidar") %>' Width="96%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvCampoValidar" 
                                runat="server"
                                ControlToValidate="txtCampoValidarEdit" 
                                ErrorMessage="Campo [Campo a Validar] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtCampoValidarEdit" runat="server"  Width="96%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvCampoValidar" 
                                runat="server"
                                ControlToValidate="txtCampoValidarEdit" 
                                ErrorMessage="Campo [Campo a Validar] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo Validación" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litTipoValidacion" runat="server" Text='<%# Bind("TipoValidacion") %>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlTipoValidacion" runat="server" SelectedValue='<%# Bind("TipoValidacion") %>'>
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                    <asp:ListItem Value="Formula">Formula</asp:ListItem>
                                    <asp:ListItem Value="SP">Procedimiento Almacenado</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                ID="rfvtxtSecuenciaEdit" 
                                runat="server"
                                ControlToValidate="ddlTipoValidacion" 
                                ErrorMessage="Campo [Tipo Validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlTipoValidacion" runat="server">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                    <asp:ListItem Value="Formula">Formula</asp:ListItem>
                                    <asp:ListItem Value="SP">Procedimiento Almacenado</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                ID="rfvtxtSecuenciaEdit" 
                                runat="server"
                                ControlToValidate="ddlTipoValidacion" 
                                ErrorMessage="Campo [Tipo Validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Regla de Validación" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFormulaValidacion" runat="server" ReadOnly="true" TextMode="MultiLine" Height="30" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "ReglaValidacion")%>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFormulaValidacionEdit" runat="server" TextMode="MultiLine" Height="50" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "ReglaValidacion")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvFormula" 
                                runat="server"
                                ControlToValidate="txtFormulaValidacionEdit" 
                                ErrorMessage="Campo [Regla de validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtFormulaValidacionEdit" runat="server" TextMode="MultiLine" Height="50" Width="96%"  ></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvFormula" 
                                runat="server"
                                ControlToValidate="txtFormulaValidacionEdit" 
                                ErrorMessage="Campo [Regla de validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Regla de Dependencia" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFormulaDependencia" runat="server" ReadOnly="true" TextMode="MultiLine" Height="30" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "ReglaDependencia")%>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFormulaDependenciaEdit" runat="server" TextMode="MultiLine" Height="50" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "ReglaDependencia")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtFormulaDependenciaEdit" runat="server" TextMode="MultiLine" Height="50" Width="96%"></asp:TextBox>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mensaje de Validación" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litMensajeValidacion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MensajeValidacion")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMensajeValidacionEdit" runat="server" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "MensajeValidacion")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvBotonAccion" 
                                runat="server"
                                ControlToValidate="txtMensajeValidacionEdit" 
                                ErrorMessage="Campo [Mensaje de Validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                             <InsertItemTemplate>
                               <asp:TextBox ID="txtMensajeValidacionEdit" runat="server" Width="96%"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvBotonAccion" 
                                runat="server"
                                ControlToValidate="txtMensajeValidacionEdit" 
                                ErrorMessage="Campo [Mensaje de Validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>                       
                                              

                    </Fields>
                    <FooterStyle BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="1px" />
                    <HeaderStyle CssClass="HeaderdetailView" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <RowStyle BorderColor="#EEEEEE" BorderStyle="Solid" BorderWidth="1px" CssClass="RowDetailView"/>
                    <EditRowStyle CssClass="RowDetailView" />
                    <AlternatingRowStyle CssClass="RowAlternateGridStyle" />
                  </asp:DetailsView>
                  
                  <div style="margin-top:3px;">
                      <asp:UpdatePanel ID="upControles" runat="server">
                        <ContentTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style=" width:97%" align="left">
                                         <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick" Visible="false"  />
                                         <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="BtnEliminarClick" Visible="false"  CausesValidation="false"/>
                                    </td>
                                    <td style="width:15px;"></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
                  </div>
                
                
                  <div style=" padding-top:3px;">
                        <asp:ValidationSummary 
                        ID="ValidationSummary1" 
                        runat="server" 
                        ShowModelStateErrors="true" 
                        CssClass="validator"
                        HeaderText="Verifique los siguientes errores:"/>
                  </div>
                      
            </td>
        </tr>
    </table>
