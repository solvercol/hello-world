<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FrmEditUnidadZona.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmEditUnidadZona" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register src="../UserControls/WUCAddUnidad.ascx" tagname="WUCAddUnidad" tagprefix="uc1" %>
<%@ Register src="../UserControls/WUCAddZona.ascx" tagname="WUCAddZona" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"  causesvalidation="False"></asp:button>
    <asp:button id="btnEliminar" OnClientClick="return confirm('¿Esta seguro?');" Visible="false" runat="server" OnClick="BtnDeleteClick" causesvalidation="False" text="Eliminar"></asp:button>
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
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left" style="width:310px;">
						        <asp:textbox id="txtDescripcion" runat="server" TextMode="MultiLine" width="300px" MaxLength="512">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvDescripcion" 
						        runat="server" 
						        errormessage="El campo [Descripción] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtDescripcion">
								</asp:requiredfieldvalidator>
						    </td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Unidad:</th>
						    <td align="left">
						        <asp:Literal runat="server" ID="txtUnidad"></asp:Literal>
						    </td>
                            <td align="left">
                                    <uc1:WUCAddUnidad ID="WUCAddUnidad1" runat="server" />
                            </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Zona:</th>
						    <td align="left">
                             <asp:Literal runat="server" ID="txtZona"></asp:Literal>
						    </td>
                             <td align="left">
                                 <uc2:WUCAddZona ID="WUCAddZona1" runat="server" />
                            </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Gerente:</th>
						    <td align="left">
                                <asp:Literal runat="server" ID="txtGerente"></asp:Literal>
						    </td>
                            <td>&nbsp;</td>
					    </tr>
                                <tr>
                         <th style="text-align:left;vertical-align:top">Tarifa Flete:</th>
						    <td align="left">
						        <asp:textbox id="txtTarifa" runat="server"  width="300px" MaxLength="24">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvTarifa" 
						        runat="server" 
						        errormessage="El campo [Tarifa] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtTarifa">
								</asp:requiredfieldvalidator>
                                <asp:RegularExpressionValidator ID="revTarifa" runat="server" ErrorMessage ="[Tarifa]: Valor invalido"
                                cssclas="validator"
                                display="Dynamic"
                                enableclientscript="true"
                                controltovalidate="txtTarifa" ValidationExpression="^\d+(\,\d{1,4})?$" ></asp:RegularExpressionValidator>
						    </td>
                            <td>&nbsp;</td>
                        </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chkActive" runat="server"></asp:checkbox></td>
                            <td>&nbsp;</td>
					    </tr>	
                 <%--       <tr>
						    <th style="text-align:left;vertical-align:top">Creado por:</th>
						    <td align="left"><asp:Label ID="lblCreateBy" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha creación:</th>
						    <td align="left"><asp:Label ID="lblCreateOn" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Modificado por:</th>
						    <td align="left"><asp:Label ID="lblModifiedBy" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha modificación:</th>
						    <td align="left"><asp:Label ID="lblModifiedOn" runat="server"></asp:Label></td>
                            <td>&nbsp;</td>
					    </tr>--%>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
                            <td>&nbsp;</td>
					    </tr>
				    </table>
            </td>
        </tr>
    
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
    <table width="100%">
        <tr >
            <td  style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0; font-size:8pt; color:#808080;">
                Creado por:&nbsp;<asp:Label ID="lblCreateBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblCreateOn" runat="server"/>&nbsp;&#44;&nbsp;Modificado por:&nbsp;<asp:Label ID="lblModifiedBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblModifiedOn" runat="server"/>
            </td>
        </tr>
    </table>
</asp:Content>