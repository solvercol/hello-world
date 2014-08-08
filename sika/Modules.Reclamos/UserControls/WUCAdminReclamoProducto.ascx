<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminReclamoProducto.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminReclamoProducto" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

    <asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="False" ValidationGroup="vgGeneral"/>
    
    <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  />
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar"  ValidationGroup="vgGeneral"  />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
    </div>

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
                                    * Asesor :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" style="width: 40%">
                                </td>

                                <td class="Separador"></td>

                                <th style="width: 10%; text-align:left">
                                    Planta :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" style="width: 30%">
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    * Producto :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" colspan="5">
                
                                </td>
                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Cantidad Vendida Und :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                                    Cantidad Reclamada Und :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    * Aplicado? :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
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
                                    Fecha de venta :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                                    No. Recordatorios :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Reclamo Atendido por :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                                    Tipo de Contacto :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Respuesta Inmediata :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">                
                                </th>

                                <td class="Separador"></td>

                                <td >
                                </td>

                            </tr>
                        </table>
                    </Content>
                </ajaxToolkit:AccordionPane> 
                <ajaxToolkit:AccordionPane  runat="server" ID="PaneDatosClienteObra"
                                            HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent">
                    <Header>Datos Cliente Obra</Header>
                    <Content>        
                         <table width="100%" class="tblSecciones">
                            <!-- INICIO Datos CLiente Obra -->
                            <tr>
                                <th style="text-align:left">
                                    * Cliente :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line">
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
                                    * Unidad / Zona :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line">
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
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                                    * Email Contacto :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Nombre de la Obra :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                                    Aplicado por :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Propietario de la Obra :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                                    Email de quien aplica :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Email Propietario :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                
                                </th>

                                <td class="Separador"></td>

                                <td >
                                </td>

                            </tr>
                            <!-- FIN Datos CLiente Obra -->
                        </table> 
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane  runat="server" ID="PaneEstadoProducto"
                                            HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent">
                    <Header>Estado del Producto</Header>
                    <Content>    
                        <table width="100%" class="tblSecciones">
                            <!-- INICIO Estado del Producto -->
                            <tr>
                                <th style="text-align:left">
                                    Aspecto exterior envase :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line">
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left"> 
                                    Aspecto del Producto :              
                                </th>

                                <td class="Separador"></td>

                                <td class="Line">
                                </td>

                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Descripción :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line">
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
                                    * Numero de Lote :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                                <td class="Separador"></td>

                                <th style="text-align:left">
                                    * Muestra disponible :
                                </th>

                                <td class="Separador"></td>

                                <td class="Line" >
                                </td>

                            </tr>
                            <!-- FIN Estado del Producto -->
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
                                    <asp:TextBox ID="txtDescripcionProblema" runat="server" TextMode="MultiLine" Width="97%" Rows="4" />
                                </td>            
                            </tr>
                            <tr>            
                                <td colspan="7">                
                                    <asp:Label ID="Label4" runat="server" ForeColor="Red"  >
                                        a. El problema debe estar formulado claramente; describir los hechos, situaciones, características del fenómeno, lugares, fechas, situaciones difíciles. 
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>            
                                <td colspan="7">                
                                    <asp:Label ID="Label5" runat="server" ForeColor="Red"  >
                                        b. Expresar el problema y su relación con una o más variables (Aplicación, superficie, temperatura, etc.) 
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>            
                                <td colspan="7">                
                                    <asp:Label ID="Label6" runat="server" ForeColor="Red" >
                                        c. Defina claramente porque lo considera un reclamo, entendiendo por reclamo toda desviación del comportamiento estándar de un producto o servicio.
                                    </asp:Label>
                                </td>
                            </tr>
                            <!-- FIN Estado del Producto -->
                        </table>
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane  runat="server" ID="PaneDiagnosticoConclusionesPrevias"
                                            HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent">
                    <Header>Diagnóstico y Conclusiones Previas</Header>
                    <Content>
                        <table width="100%" class="tblSecciones">
                            <!-- INICIO Diagnostico y conclusiones previas -->
                            <tr>
                                <th style="text-align:left">
                                    Diagnóstico :
                                </th>
                                <td class="Separador"></td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtDiagnostico" runat="server" TextMode="MultiLine" Width="97%" Rows="4" />
                                </td>            
                            </tr>
                            <tr>
                                <th style="text-align:left">
                                    Conclusiones Previa :
                                </th>
                                <td class="Separador"></td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtConclusionesPrevias" runat="server" TextMode="MultiLine" Width="97%" Rows="4" />
                                </td>            
                            </tr>
                            <!-- FIN Diagnostico y conclusiones previas -->
                        </table>
                    </Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane  runat="server" ID="PaneSolucion"
                                            HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected"
                                            ContentCssClass="accordionContent">
                    <Header>Solución</Header>
                    <Content>
                        <table width="100%" class="tblSecciones">
                            <!-- INICIO Solucion -->
                            <tr>
                                <th style="text-align:left">
                                    Solucionado? :
                                </th>
                                <td class="Separador"></td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtObservacionesSolucion" runat="server" TextMode="MultiLine" Width="97%" Rows="4" />
                                </td>            
                            </tr>
                            <!-- FIN Solucion -->
                        </table>
                    </Content>
                </ajaxToolkit:AccordionPane>       
            </Panes>
        </ajaxToolkit:Accordion>