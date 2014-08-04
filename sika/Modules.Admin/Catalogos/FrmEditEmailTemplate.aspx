<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmEditEmailTemplate.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmEditEmailTemplate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="False" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Save"></asp:button>
	<asp:button id="btnEliminar" OnClientClick="return confirm('¿Are you sure?');" runat="server" OnClick="BtnDeleteClick" causesvalidation="False" text="Delete"></asp:button>
	
</div>

<table width="100%" class="tblSecciones">
        <tr>
          
            <td>
                 
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
					    </tr>
					    <tr>
						    <th align="left">Pais</th>
						    <td align="left">
						    <asp:DropDownList 
                            CssClass="CombosGenericos"
						    ID="ddlPais" 
						    Width="200"
						    runat="server"></asp:DropDownList>
                            
                             <asp:requiredfieldvalidator id="rfvPais" 
						        runat="server" 
						        errormessage="El campo [Pais] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="ddlPais">
								</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
						    <th align="left">Nombre Plantilla</th>
						    <td align="left">
						        <asp:textbox id="txtNombre" runat="server" width="400px" MaxLength="50">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvUsername" 
						        runat="server" 
						        errormessage="El campo [Nombre PLantilla] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNombre">
								</asp:requiredfieldvalidator>
						</td>
					    </tr>
					    
                        <tr>
						    <th align="left">Código Plantilla</th>
						    <td align="left">
						        <asp:textbox id="txtCodigo" runat="server" width="200px" MaxLength="20">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvtxtCodigo" 
						        runat="server" 
						        errormessage="El campo [código Plantilla] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtCodigo">
								</asp:requiredfieldvalidator>
						</td>
					    </tr>
					    
					     <tr>
						    <th align="left">Encabezado Documento</th>
						    <td align="left">
                                <asp:TextBox ID="__txtEncabezado"
                                runat="server"
                                TextMode="MultiLine"
                                Width="500"
                                Height="70"
                                ></asp:TextBox>

                                <ajaxToolkit:htmlEditorExtender 
                                ID="htmlEditorExtender2" ValidateRequestMode="Disabled"
                                TargetControlID="__txtEncabezado" 
                                runat="server" >     
                                <Toolbar>
                                    
                                    <ajaxToolkit:Bold />
                                    <ajaxToolkit:Italic />
                                    <ajaxToolkit:JustifyLeft />
                                    <ajaxToolkit:JustifyCenter />
                                    <ajaxToolkit:JustifyRight />
                                    <ajaxToolkit:JustifyFull />
                                    <ajaxToolkit:FontNameSelector />
                                    <ajaxToolkit:FontSizeSelector />
                                    <ajaxToolkit:ForeColorSelector />
                                    <ajaxToolkit:BackgroundColorSelector />
                                    <ajaxToolkit:RemoveFormat />
                                </Toolbar>       
                            </ajaxToolkit:htmlEditorExtender>

                                <asp:requiredfieldvalidator id="Requiredfieldvalidator1" 
						        runat="server" 
						        errormessage="El campo [Encabezado Documento] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="__txtEncabezado">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    
					     <tr>
						    <td align="left"></td>
						    <td align="left"></td>
					    </tr>
					    
					    <tr>
						    <th align="left">Cuerpo del Documento</th>
						    <td align="left">
                            
                            <asp:TextBox runat="server"
                            ID="__txtContenido" 
                            TextMode="MultiLine" 
                            Width="500"
                            Height="200"
                            Text="" />
    
                            <ajaxToolkit:HtmlEditorExtender 
                                ID="htmlEditorExtender1" 
                                TargetControlID="__txtContenido" ValidateRequestMode="Disabled"
                                runat="server" >     
                                <Toolbar>
                                    <ajaxToolkit:Bold />
                                    <ajaxToolkit:Italic />
                                    <ajaxToolkit:JustifyLeft />
                                    <ajaxToolkit:JustifyCenter />
                                    <ajaxToolkit:JustifyRight />
                                    <ajaxToolkit:JustifyFull />
                                    <ajaxToolkit:SelectAll />
                                    <ajaxToolkit:UnSelect />
                                    <ajaxToolkit:FontNameSelector />
                                    <ajaxToolkit:FontSizeSelector />
                                    <ajaxToolkit:ForeColorSelector />
                                    <ajaxToolkit:BackgroundColorSelector />
                                    <ajaxToolkit:RemoveFormat />
                                </Toolbar>       
                            </ajaxToolkit:HtmlEditorExtender>
                                
                                
                                <asp:requiredfieldvalidator id="Requiredfieldvalidator2" 
						        runat="server" 
						        errormessage="El campo [Cuerpo Documento] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="__txtContenido">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    <tr>
						    <th align="left">Activa</th>
						    <td align="left"><asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>					
				    </table>
    			
		
            </td>
       
        
        </tr>
    
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
