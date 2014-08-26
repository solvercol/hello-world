﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminRecServicioT2.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminRecServicioT2" %>

    <%@ Register    Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                    Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
    <%@ Register    Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
                    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>   
    <%@ Register src="WUCFilterClient.ascx" tagname="WucFilterClient" tagprefix="ucFilterClient" %> 

    <script language="javascript" type="text/javascript">
        var divModal = 'DivModal';

        function ShowSplashModal() {
            var adiv = $get(divModal);
            adiv.style.visibility = 'visible';
        }
    </script>
    
    <div id="DivModal">
        <div id="VentanaMensaje">
            <div id="Msg">
                <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
            </div>
        </div>
    </div>

    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="BtnRegresar_Click" />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar"  ValidationGroup="vgGeneral" OnClientClick="return ShowSplashModal();" OnClick="BtnGuardar_Click" />
    </div>

    <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="false" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>

    <ajaxToolkit:Accordion    ID="Secciones"
                                runat="Server"
                                SelectedIndex="0"
                                HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent"
                                AutoSize="None"
                                width="100%"
                                FadeTransitions="true"
                                TransitionDuration="250"
                                FramesPerSecond="40"
                                RequireOpenedPane="false"
                                SuppressHeaderPostbacks="true">
        <Panes>                
            <ajaxToolkit:AccordionPane  runat="server" ID="PaneInfoGeneral"
                                        HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent">
                <Header>Información General</Header>
                <Content>
                    <table width="100%" class="tblSecciones">
                        <tr>
                            <th style="width: 10%; text-align:left">
                                Categoría Reclamo :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" style="width: 40%">
                                <asp:Label ID="lblCategoriaReclamo" runat="server" />
                            </td>

                            <td class="Separador"></td>

                            <th style="width: 10%; text-align:left">
                                Area :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" style="width: 30%">
                                <asp:Label ID="lblArea" runat="server" />
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left">
                                * Asesor :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <ig:WebDropDown ID="wddAsesor" 
                                                runat="server" 
                                                EnableMultipleSelection="false"
                                                MultipleSelectionType="Checkbox" 
                                                DisplayMode="DropDown"
                                                EnableClosingDropDownOnSelect="false"
                                                StyleSetName="Claymation"
                                                DropDownContainerWidth="300px"
                                                DropDownContainerHeight="220px"
                                                OnValueChanged="WddAsesor_ValueChanged"
                                                AutoPostBack="true"
                                                Width="98%">
                                </ig:WebDropDown>
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">
                                Planta :                                    
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <ig:WebDropDown ID="wddPlanta" 
                                                runat="server" 
                                                EnableMultipleSelection="false"
                                                MultipleSelectionType="Checkbox" 
                                                DisplayMode="DropDown"
                                                EnableClosingDropDownOnSelect="false"
                                                StyleSetName="Claymation"
                                                DropDownContainerWidth="250px"
                                                DropDownContainerHeight="150px"
                                                Width="98%">
                                </ig:WebDropDown>
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align:left">
                                # Pedido <br />
                                # Factura <br />
                                # Remisión :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" style="vertical-align:middle;" >
                                <asp:TextBox ID="txtPedidoFacturaRemision" runat="server" Width="90%" />
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">
                                # Diario de Inventario :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <ig:WebNumericEditor    Id="txtDiarioInventario" runat="server" 
                                                        Nullable="false" MinValue="0" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <th style="text-align:left">
                                Reclamo Atendido Por :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <ig:WebDropDown ID="wddReclamoAtentidoPor" 
                                                runat="server" 
                                                EnableMultipleSelection="false"
                                                MultipleSelectionType="Checkbox" 
                                                DisplayMode="DropDown"
                                                EnableClosingDropDownOnSelect="false"
                                                StyleSetName="Claymation"
                                                DropDownContainerWidth="300px"
                                                DropDownContainerHeight="220px"
                                                Width="98%">
                                </ig:WebDropDown>
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">
                                No. Recordatorios :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <ig:WebNumericEditor    Id="txtNoRecordatorios" runat="server"
                                                        Nullable="false" MinValue="0" Width="50" />
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left">
                                Respuesta Inmediata :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:RadioButtonList ID="rblRespuestaInmediata" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <asp:ListItem Value="true" Text="Si" />
                                    <asp:ListItem Value="false" Text="No" Selected="True" />
                                </asp:RadioButtonList>
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">
                                Tipo de Contacto :                
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:RadioButtonList ID="rblTipoContacto" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <asp:ListItem Selected="True" Value="Escrito" Text="Escrito" />
                                    <asp:ListItem Value="Telefonico" Text="Teléfonoico" />
                                </asp:RadioButtonList>
                            </td>

                        </tr>
                    </table>
                </Content>
            </ajaxToolkit:AccordionPane> 
            <ajaxToolkit:AccordionPane  runat="server" ID="PaneDatosClienteObra"
                                        HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent">
                <Header>Datos Cliente</Header>
                <Content>        
                    <table width="100%" class="tblSecciones">
                        <!-- INICIO Datos CLiente Obra -->
                        <tr>
                            <th style="text-align:left">
                                * Cliente :
                            </th>

                            <td class="Separador"></td>

                            <td colspan="5">
                                <ucFilterClient:WucFilterClient ID="ucFilterClient" runat="server" /> 
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left">
                                * Unidad / Zona :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <asp:Label ID="lblUnidadZona" runat="server" />
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">                
                            </th>

                            <td class="Separador"></td>

                            <td >
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left">
                                * Nombre Contacto :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <table width="100%">
                                    <tr>
                                        <td style="width:95%">
                                            <asp:TextBox ID="txtNombreContacto" Width="100%" runat="server" />
                                        </td>
                                        <td style="width:5%">
                                            <asp:RequiredFieldValidator ID="reqNombreContacto"
                                                                        runat="server"
                                                                        ForeColor="Red"
                                                                        ControlToValidate="txtNombreContacto"
                                                                        ValidationGroup="vgGeneral"                                                                 
                                                                        ErrorMessage="El nombre del contacto es obligatorio-Datos Cliente Obra" >*</asp:RequiredFieldValidator> 
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">
                                * Email Contacto :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <table width="100%">
                                    <tr>
                                        <td style="width:95%">
                                            <asp:TextBox ID="txtEmailContacto" Width="100%" runat="server" />
                                        </td>
                                        <td style="width:5%">
                                            <asp:RequiredFieldValidator ID="reqEmailContacto"
                                                                        runat="server"
                                                                        ForeColor="Red"
                                                                        ControlToValidate="txtEmailContacto"
                                                                        ValidationGroup="vgGeneral"                                                                 
                                                                        ErrorMessage="El email de contacto es obligatorio-Datos Cliente Obra" >*</asp:RequiredFieldValidator>   
                                            <asp:RegularExpressionValidator ID="reqExpTxtEmailContacto"
                                                                            runat="server"
                                                                            ForeColor="Red"
                                                                            ControlToValidate="txtEmailContacto"
                                                                            ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
                                                                            ValidationGroup="vgGeneral"                                                                            
                                                                            ErrorMessage="El mail ingresado no se encuentra con una estructura correcta">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>  
                        <tr>
                            <th style="text-align:left">
                                Fecha Pedido :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:TextBox ID="txtFechaPedido" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender 
                                    ID="cexTxtFechaPedido" 
                                    runat="server"  
                                    TargetControlID="txtFechaPedido" 
                                    PopupPosition="Right" 
                                    PopupButtonID="txtFechaPedido"
                                    Format="dd/MM/yyyy"
                                    CssClass="cal_Theme1" />
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">
                                Fecha del Compromiso :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:TextBox ID="txtFechaCompromiso" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender 
                                    ID="cexTxtFechaCompromiso" 
                                    runat="server"  
                                    TargetControlID="txtFechaCompromiso" 
                                    PopupPosition="Right" 
                                    PopupButtonID="txtFechaCompromiso"
                                    Format="dd/MM/yyyy"
                                    CssClass="cal_Theme1" />
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left">
                                Fecha Real Entrega :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <asp:TextBox ID="txtFechaRealEntrega" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender 
                                    ID="cexTxtFechaRealEntrega" 
                                    runat="server"  
                                    TargetControlID="txtFechaRealEntrega" 
                                    PopupPosition="Right" 
                                    PopupButtonID="txtFechaRealEntrega"
                                    Format="dd/MM/yyyy"
                                    CssClass="cal_Theme1" />
                            </td>

                            <td class="Separador"></td>

                            <th style="text-align:left">
                                Días de Incumplimiento :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <ig:WebNumericEditor    Id="txtDiasIncumplimiento" runat="server"
                                                        Nullable="false" MinValue="0" Width="100%" />
                            </td>

                        </tr>  
                        <!-- FIN Datos CLiente Obra -->
                    </table> 
                </Content>
            </ajaxToolkit:AccordionPane>  
            <ajaxToolkit:AccordionPane  runat="server" ID="PaneDescripcionProblema"
                                            HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent">
                <Header>Descripción del Problema</Header>
                <Content>
                    <table width="100%" class="tblSecciones">
                        <!-- INICIO Descripcion del problema -->
                        <tr>
                            <td colspan="7">   
                                <table width="100%">
                                    <tr>
                                        <td style="width:95%">
                                            <asp:TextBox ID="txtDescripcionProblema" runat="server" TextMode="MultiLine" Width="97%" Rows="4" />
                                        </td>
                                        <td style="width:5%">
                                            <asp:RequiredFieldValidator ID="reqTxtDescripcionProblema"
                                                                        runat="server"
                                                                        ForeColor="Red"
                                                                        ControlToValidate="txtDescripcionProblema"
                                                                        ValidationGroup="vgGeneral"                                                                 
                                                                        ErrorMessage="La descripción del problema es obligatoria" >*</asp:RequiredFieldValidator>   
                                        </td>
                                    </tr>
                                </table>                                
                            </td>            
                        </tr>
                        <tr>            
                            <td colspan="7">                
                                <asp:Label ID="lblMensajeDescripcionProblema" runat="server" ForeColor="Red"  >                                    
                                </asp:Label>
                            </td>
                        </tr>
                        <!-- FIN Descripcion -->
                    </table>
                </Content>
            </ajaxToolkit:AccordionPane>       
        </Panes>
    </ajaxToolkit:Accordion>