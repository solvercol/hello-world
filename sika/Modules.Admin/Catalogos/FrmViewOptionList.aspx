﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmViewOptionList.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmViewOptionList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
						    <th style="text-align:left;vertical-align:top">Module Id:</th>
						    <td align="left" class="bordeTabla">
                                <asp:Literal runat="server" ID="txtModule"></asp:Literal>
                            </td>
					    </tr>
                        <tr>
						    <th style="text-align:left;vertical-align:top">Key:</th>
						    <td align="left" class="bordeTabla">
                            <asp:Literal runat="server" ID="txtKey"></asp:Literal>
						    </td>
					    </tr>
                        <tr>
						<th style="text-align:left;vertical-align:top">Value:</th>
						<td align="left" class="bordeTabla">
                                <asp:Literal runat="server" ID="txtValue"></asp:Literal>
						    </td>
					    </tr>
                        <tr>
						<th style="text-align:left;vertical-align:top">Descripción:</th>
						<td align="left" class="bordeTabla">
                                <asp:Literal runat="server" ID="txtDescripcion"></asp:Literal>
						    </td>
					    </tr>
					    <tr>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left" class="bordeTabla">
                            <asp:checkbox id="chkActive" runat="server" Enabled="false"></asp:checkbox></td>
					    </tr>	

				    </table>
            </td>
        </tr>
    
    </table>

    <table width="100%">
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr >
            <td  style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0; font-size:8pt; color:#808080;">
                Creado por:&nbsp;<asp:Label ID="lblCreateBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblCreateOn" runat="server"/>&nbsp;&#44;&nbsp;Modificado por:&nbsp;<asp:Label ID="lblModifiedBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblModifiedOn" runat="server"/>
            </td>
        </tr>
    </table>
</asp:Content>
