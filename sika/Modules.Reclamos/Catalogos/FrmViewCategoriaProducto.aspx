<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmViewCategoriaProducto.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmViewCategoriaProducto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.2238, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
             Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnEdit" runat="server" OnClick="BtnEditClick" text="Editar" causesvalidation="False"></asp:button>
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
						    <th style="text-align:left;vertical-align:top">Nombre:</th>
						    <td align="left">
                                <asp:Literal runat="server" ID="txtNombre"></asp:Literal>
                            </td>
					    </tr>
                         <tr>
						    <th style="text-align:left;vertical-align:top">Descripción:</th>
						    <td align="left">
                            <asp:Literal runat="server" ID="txtDescripcion"></asp:Literal>
						    </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Ingenieros Responsables:</th>
						    <td align="left">
                            <asp:Literal runat="server" ID="txtResponsables"></asp:Literal>
						    </td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left"><asp:checkbox id="chkActive" runat="server" Enabled="false"></asp:checkbox></td>
					    </tr>	
                     <%--   <tr>
						    <th style="text-align:left;vertical-align:top">Creado por:</th>
						    <td align="left"><asp:Label ID="lblCreateBy" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha creación:</th>
						    <td align="left"><asp:Label ID="lblCreateOn" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Modificado por:</th>
						    <td align="left"><asp:Label ID="lblModifiedBy" runat="server"></asp:Label></td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Fecha modificación:</th>
						    <td align="left"><asp:Label ID="lblModifiedOn" runat="server"></asp:Label></td>
					    </tr>--%>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
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