﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAddUser.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmAddUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<asp:ValidationSummary ID="vsGeneral" runat="server" DisplayMode="BulletList" ShowMessageBox="true" CssClass="validator" ShowSummary="true" ValidationGroup="vgGeneral"/>
 <div style="padding:3px; text-align:right;">
    <asp:button id="btnReturn" runat="server" OnClientClick="return confirm('¿Esta Seguro?');"  OnClick="BtnBackClick" text="Regresar" causesvalidation="False"></asp:button>
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
						    <th align="left">Código Usuario</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtUserCode" runat="server" width="400px" MaxLength="15">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvUserCode" 
						        runat="server" 
						        errormessage="El campo [User Code] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtUserCode">
								</asp:requiredfieldvalidator>
                            </td>
					    </tr>
					    <tr>
                            <td>*</td>
						    <th align="left">Nombres</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtNames" runat="server" width="400px" MaxLength="60">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvName" 
						        runat="server" 
						        errormessage="El campo [Names] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtNames">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr>
                            <td>*</td>
						    <th align="left">Fecha Ingreso</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtIncomeDate" runat="server" width="100px" MaxLength="12">
						        </asp:textbox>
                                <asp:ImageButton ID="imgbtnCalendar" causesvalidation="False"  runat="server" ImageUrl="~/Resources/images/calendar.png" />
                                <ajaxToolkit:CalendarExtender ID="cldrIncomeDate" runat="server"
                                PopupButtonID="imgbtnCalendar" TargetControlID="txtIncomeDate">
                                </ajaxToolkit:CalendarExtender>
						        <asp:requiredfieldvalidator id="rfvIncomeDate" 
						        runat="server" 
						        errormessage="El campo [Income Date] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtIncomeDate">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr>
                            <td>*</td>
						    <th align="left">Nombre Usuario</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtUserName" runat="server" width="400px" MaxLength="50">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvUserName" 
						        runat="server" 
						        errormessage="El campo [UserName] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtUserName">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr>
                            <td>*</td>
						    <th align="left">Password</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtPassword" runat="server" width="400px" MaxLength="50" TextMode="Password">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvPassword" 
						        runat="server" 
						        errormessage="El campo [Password] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtPassword">
								</asp:requiredfieldvalidator>
						    </td>
					    </tr>
                        <tr>
                            <td>*</td>
						    <th align="left">Email</th>
						    <td align="left" class="bordeTabla">
						        <asp:textbox id="txtEmail" runat="server" width="400px" MaxLength="50">
						        </asp:textbox>
						        <asp:requiredfieldvalidator id="rfvEmail" 
						        runat="server" 
						        errormessage="El campo [Email] es requerido!!." 
						        cssclass="validator"
								display="Dynamic" 
								enableclientscript="true" 
								controltovalidate="txtEmail">
								</asp:requiredfieldvalidator>
                                <asp:RegularExpressionValidator ID="revEmail" 
                                runat="server" 
                                ErrorMessage="Formato Inválido: [Email]" 
                                CssClass="validator" 
                                Display="Dynamic" 
                                EnableClientScript="true" 
                                ControlToValidate="txtEmail" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                </asp:RegularExpressionValidator>
						    </td>
					    </tr>
					    <tr>
                            <td>&nbsp;</td>
						    <th align="left">Activo</th>
						    <td align="left" class="bordeTabla">
                            <asp:checkbox id="chkActive" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <th align="left">Roles disponibles</th>
                            <td align="left" class="bordeTabla">
                                <table id="roles" width="30%">
                                    <asp:repeater id="rptRoles" OnItemDataBound="RptRolesItemDataBound" runat="server">
                                        <headertemplate>
	                                        <tr>
		                                        <th></th>
		                                        <th></th>
	                                        </tr>
                                        </headertemplate>
	                                    <itemtemplate>
		                                    <tr>
			                                    <td><%# DataBinder.Eval(Container.DataItem, "NombreRol") %></td>
			                                    <td style="text-align:left">
				                                    <asp:checkbox id="chkRole" runat="server"></asp:checkbox>
			                                    </td>
		                                    </tr>
	                                    </itemtemplate>
                                    </asp:repeater>
                                </table>
                            </td>	
                        </tr>	
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
</asp:Content>
