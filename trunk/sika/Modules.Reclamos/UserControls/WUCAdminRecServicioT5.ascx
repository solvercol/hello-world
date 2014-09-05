<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAdminRecServicioT5.ascx.cs" Inherits="Modules.Reclamos.UserControls.WUCAdminRecServicioT5" %>

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
        <asp:Button ID="Button1" runat="server" Text="Regresar" OnClick="BtnRegresar_Click" />
        <asp:Button ID="Button2" runat="server" Text="Guardar"  ValidationGroup="vgGeneral" OnClientClick="return ShowSplashModal();" OnClick="BtnGuardar_Click" />
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
                            <th style="width: 10%; text-align:left; vertical-align:top">
                                Categoría Reclamo :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" style="width: 40%">
                                <asp:Label ID="lblCategoriaReclamo" runat="server" />
                            </td>

                            <td class="Separador"></td>

                            <th style="width: 10%; text-align:left; vertical-align:top">
                                Area :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" style="width: 30%">
                                <asp:Label ID="lblArea" runat="server" />
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
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

                            <td class="Separador"></td>

                            <th style="text-align:left">                                
                            </th>

                            <td class="Separador"></td>

                            <td >
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
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

                            <th style="text-align:left; vertical-align:top">
                                No. Recordatorios :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line" >
                                <ig:WebNumericEditor    Id="txtNoRecordatorios" runat="server"
                                                        Nullable="false" MinValue="0" Width="50" />
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
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
                            <th style="text-align:left; vertical-align:top">
                                Nombre de quien plantea el reclamo :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <ig:WebDropDown ID="wddQuienReclama" 
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
                            </th>

                            <td class="Separador"></td>

                            <td >
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Procedimiento interno afectado :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <ig:WebDropDown ID="wddProcedimientoInternoAfectado" 
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
                            </th>

                            <td class="Separador"></td>

                            <td >
                            </td>

                        </tr>
                        <tr>
                            <th style="text-align:left; vertical-align:top">
                                Area que incumple el procedimiento :
                            </th>

                            <td class="Separador"></td>

                            <td class="Line">
                                <ig:WebDropDown ID="wddAreaIncumpleProcedimiento" 
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
                            </th>

                            <td class="Separador"></td>

                            <td >
                            </td>

                        </tr>
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
                                    <asp:TextBox ID="txtDescripcionProblema" runat="server" TextMode="MultiLine" Width="97%" Rows="4" MaxLength="1024" />
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