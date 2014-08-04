<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucRutasWorkFlow.ascx.cs" Inherits="Modules.WorkFlow.Admin.WucRutasWorkFlow" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.NavigationControls" TagPrefix="ig" %>    
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

        

    <table width="100%" cellpadding="0" cellspacing="0" style=" margin-top:10px;">
        <tr>
            <td style=" width:30%" valign="top">
               <div style=" border:1px solid #EEEEEE; height:410px;">
                <ig:WebDataMenu runat="server" ID="ContextMenu" IsContextMenu="true" OnItemClick="ContextMenuClick"
                    BorderWidth="1" BorderColor="#CCCCCC" EnableScrolling="false">
                    <ClientEvents ItemClick="MenuItem_Click" />
                    <AutoPostBackFlags ItemClick="On" />
                    <Items>
                        <ig:DataMenuItem Text="Seleccionar Ruta" Key="Select" ImageUrl="~/Resources/Images/select.png" />
                        <ig:DataMenuItem Text="Nueva Ruta" Key="New" ImageUrl="~/Resources/Images/ChildNode3.png" />
                        <ig:DataMenuItem Text="Editar Ruta" Key="Edit" ImageUrl="~/Resources/Images/edit.png" />
                        <ig:DataMenuItem Text="Expandir/Contraer" Key="expand" ImageUrl="~/Resources/Images/expand.png" />                        
                    </Items>
                </ig:WebDataMenu>

                <div>
                    <ig:WebDataTree ID="wdtRutas" runat="server" Width="300px" Height="400"
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
               DataKeyNames="IdRuta" 
               HeaderText="Rutas WorkFlow" 
               ID="dvRutas" 
               OnDataBound="OnDataBoundEvent"
               runat="server" 
               BorderStyle="Solid" 
               BorderWidth="1px" EmptyDataText="..."
               BorderColor="Black"
               Width="600px" GridLines="None">
                    
                    <Fields >                       

                        <asp:TemplateField HeaderText="Estado Inicial" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litEstadoInicial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EstadoInicial")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEstadosEdit" runat="server" ></asp:DropDownList>
                                <input type="hidden" id="hdnEstadoInicial"  runat="server" value='<%# Bind("IdEstado") %>' />
                                <asp:RequiredFieldValidator 
                                ID="rfvEstadoInicial" 
                                runat="server"
                                ControlToValidate="ddlEstadosEdit" 
                                ErrorMessage="Campo [Estado Inicial] es requerido!" 
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
                                ErrorMessage="Campo [Estado Inicial] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado Final" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litEstadoFinal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EstadoFinal")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEstadoFinalEdit" runat="server" ></asp:DropDownList>
                                <input type="hidden" id="hdnEstadoFinal"  runat="server" value='<%# Bind("SiguienteEstado") %>' />
                                <asp:RequiredFieldValidator 
                                ID="rfvEstadoFinal" 
                                runat="server"
                                ControlToValidate="ddlEstadoFinalEdit" 
                                ErrorMessage="Campo [Estado Final] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlEstadoFinalEdit" runat="server" ></asp:DropDownList>
                                <asp:RequiredFieldValidator 
                                ID="rfvEstadoFinal" 
                                runat="server"
                                ControlToValidate="ddlEstadoFinalEdit" 
                                ErrorMessage="Campo [Estado Final] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Secuencia" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litSecuencia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Secuencia")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <ig:WebNumericEditor ID="txtSecuenciaEdit" runat="server" HorizontalAlign="Left" Width="20%" Text='<%# DataBinder.Eval(Container.DataItem, "Secuencia")%>'></ig:WebNumericEditor>
                                <asp:RequiredFieldValidator 
                                ID="rfvtxtSecuenciaEdit" 
                                runat="server"
                                ControlToValidate="txtSecuenciaEdit" 
                                ErrorMessage="Campo [Secuencia] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <ig:WebNumericEditor ID="txtSecuenciaEdit" HorizontalAlign="Left" runat="server" Width="20%" ></ig:WebNumericEditor>
                                <asp:RequiredFieldValidator 
                                ID="rfvtxtSecuenciaEdit" 
                                runat="server"
                                ControlToValidate="txtSecuenciaEdit" 
                                ErrorMessage="Campo [Secuencia] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rol" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litRol" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rol")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRolEdit" runat="server" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "Rol")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvrol" 
                                runat="server"
                                ControlToValidate="txtRolEdit" 
                                ErrorMessage="Campo [Rol] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtRolEdit" runat="server" Width="96%" ></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvrol" 
                                runat="server"
                                ControlToValidate="txtRolEdit" 
                                ErrorMessage="Campo [Rol] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ruta de Validación" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFormulaValidacion" runat="server" ReadOnly="true" TextMode="MultiLine" Height="80" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "FormulaValidacion")%>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFormulaValidacionEdit" runat="server" TextMode="MultiLine" Height="80" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "FormulaValidacion")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvFormula" 
                                runat="server"
                                ControlToValidate="txtFormulaValidacionEdit" 
                                ErrorMessage="Campo [Formula de validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtFormulaValidacionEdit" runat="server" TextMode="MultiLine" Height="80" Width="96%"  ></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvFormula" 
                                runat="server"
                                ControlToValidate="txtFormulaValidacionEdit" 
                                ErrorMessage="Campo [Formula de validación] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valida Requeridos" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkValidaRequeridos" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "ValidaRequeridos")%>'/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:CheckBox ID="chkValidaRequeridosEdit" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "ValidaRequeridos")%>'/>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:CheckBox ID="chkValidaRequeridosEdit" runat="server" Checked="false"/>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Validación de Salida" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:TextBox ID="txtValidacionSalida" runat="server" ReadOnly="true" TextMode="MultiLine" Height="40" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "ValidacionesSalida")%>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValidacionSalidaEdit" runat="server" TextMode="MultiLine" Height="40" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "ValidacionesSalida")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:TextBox ID="txtValidacionSalidaEdit" runat="server" TextMode="MultiLine" Height="40" Width="96%"  ></asp:TextBox>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Texto Botón Acción" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litBotonAcciones" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BotonAccionesRutas")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTextoBotonAccionesEdit" runat="server" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "BotonAccionesRutas")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvBotonAccion" 
                                runat="server"
                                ControlToValidate="txtTextoBotonAccionesEdit" 
                                ErrorMessage="Campo [Botón de Acción] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                             <InsertItemTemplate>
                               <asp:TextBox ID="txtTextoBotonAccionesEdit" runat="server" Width="96%" ></asp:TextBox>
                               <asp:RequiredFieldValidator 
                                ID="rfvBotonAccion" 
                                runat="server"
                                ControlToValidate="txtTextoBotonAccionesEdit" 
                                ErrorMessage="Campo [Botón de Acción] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>                       

                        <asp:TemplateField HeaderText="Responsable Actual" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                <asp:Literal ID="litRolResponsable" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RolResponsableActual")%>'></asp:Literal>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRolResponsableEdit" runat="server" Width="96%" Text='<%# DataBinder.Eval(Container.DataItem, "RolResponsableActual")%>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                ID="rfvRolresponsable" 
                                runat="server"
                                ControlToValidate="txtRolResponsableEdit" 
                                ErrorMessage="Campo [Responsable Actual] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                               <asp:TextBox ID="txtRolResponsableEdit" runat="server" Width="96%" ></asp:TextBox>
                               <asp:RequiredFieldValidator 
                                ID="rfvRolresponsable" 
                                runat="server"
                                ControlToValidate="txtRolResponsableEdit" 
                                ErrorMessage="Campo [Responsable Actual] es requerido!" 
                                CssClass="validator" 
                                Text="*" 
                                Display="Dynamic"></asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones Sistema" HeaderStyle-BackColor="#F5F5F5">
                            <ItemTemplate>
                                 <asp:TextBox ID="txtAccionesSistema" runat="server" TextMode="MultiLine" Width="96%" Height="50" Text='<%# DataBinder.Eval(Container.DataItem, "AccionesSistema")%>'></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:TextBox ID="txtAccionesSistemaEdit" runat="server" TextMode="MultiLine" Width="96%" Height="50" Text='<%# DataBinder.Eval(Container.DataItem, "AccionesSistema")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                               <asp:TextBox ID="txtAccionesSistemaEdit" runat="server" TextMode="MultiLine" Width="96%" Height="50" ></asp:TextBox>
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

   