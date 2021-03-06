﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAddAreas.aspx.cs" Inherits="Modules.Reclamos.Catalogos.FrmAddAreas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
	<asp:button id="btnSave" runat="server" OnClick="BtnSaveClick" text="Guardar"></asp:button>
</div>

<table width="100%" class="tblSecciones">
        <tr>
          
            <td>
				    <table id="userdetails" width="100%">
					    
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
                            <td>&nbsp;</td>
					    </tr>
					    <tr>
                            <td>*</td>
						    <th style="text-align:left;vertical-align:top">Nombre:</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtNombre" runat="server" width="400px" MaxLength="512">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvNombre" 
						            runat="server" 
						            errormessage="El campo [Nombre] es requerido!!." 
						            cssclass="validator"
								    display="Dynamic" 
								    enableclientscript="true" 
								    controltovalidate="txtNombre">
								</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
                            <td>*</td>
						    <th style="text-align:left;vertical-align:top">Proceso:</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtProceso" runat="server" TextMode="MultiLine" width="100%">
						        </asp:textbox>
                                <asp:requiredfieldvalidator id="rfvProceso" 
						            runat="server" 
						            errormessage="El campo [Proceso] es requerido!!." 
						            cssclass="validator"
								    display="Dynamic" 
								    enableclientscript="true" 
								    controltovalidate="txtProceso">
								</asp:requiredfieldvalidator>
                                <br />
                                <div class="mensajeMultivalor">
                                    <asp:Literal ID="litMensajeMultivalor" runat="server"></asp:Literal>
                                </div>
						    </td>
					    </tr>
                        <tr>
                            <td>*</td>
						    <th style="text-align:left;vertical-align:top">Gerente:</th>
						    <td align="left" class="bordeTabla">
                                <asp:DropDownList ID="ddlGerentes" class="chzn-select" runat="server" Width="50%" />
						        <asp:requiredfieldvalidator id="rfvgerentes" 
						            runat="server" 
						            errormessage="El campo [Gerente] es requerido!!." 
						            cssclass="validator"
								    display="Dynamic" 
								    enableclientscript="true" 
								    controltovalidate="ddlGerentes">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
					    <tr>
                            <td>&nbsp;</td>
						    <th style="text-align:left;vertical-align:top">Activo:</th>
						    <td align="left" class="bordeTabla">
                            <asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>
					    <tr>
						    <td align="left"></td>
						    <td align="left"></td>
					    </tr>
				    </table>
            </td>
        </tr>
     <script type="text/javascript">
         $(".chzn-select").chosen({ allow_single_deselect: true });

         $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">

    <table width="100%">
        <tr >
            <td  style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0; font-size:8pt; color:#808080;">
                Creado por:&nbsp;<asp:Label ID="lblCreateBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblCreateOn" runat="server"/>
            </td>
        </tr>
    </table>

</asp:Content>
