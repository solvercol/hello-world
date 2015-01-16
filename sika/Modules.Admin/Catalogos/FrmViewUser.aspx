<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmViewUser.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmViewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
						    <th align="left">Código Usuario</th>
						    <td align="left" class="bordeTabla">
						        <asp:Label ID="lblUserCode" runat="server"></asp:Label>
                            </td>
					    </tr>
					    <tr>
						    <th align="left">Nombres</th>
						    <td align="left" class="bordeTabla">
						       <asp:Label ID="lblNames" runat="server"></asp:Label>
						    </td>
					    </tr>
                        <tr>
						    <th align="left">Fecha Ingreso</th>
						    <td align="left" class="bordeTabla">
						        <asp:Label ID="lblIncomeDate" runat="server"></asp:Label>
						    </td>
					    </tr>
                        <tr>
						    <th align="left">Nombre Usuario</th>
						    <td align="left" class="bordeTabla">
						        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </td>
					    </tr>
                        <tr>
						    <th align="left">Email</th>
						    <td align="left" class="bordeTabla">
						       <asp:Label ID="lblEmail" runat="server"></asp:Label>
						    </td>
					    </tr>
					    <tr>
						    <th align="left">Activo</th>
						    <td align="left" class="bordeTabla">
                            <asp:checkbox id="chkActive" Enabled="false" runat="server" Checked="true"></asp:checkbox></td>
					    </tr>	
                        <tr>
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
				                                    <asp:checkbox id="chkRole" Enabled="false" runat="server"></asp:checkbox>
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
    
    <table width="100%">
        <tr><td>&nbsp;</td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr >
            <td  style="text-align:left; vertical-align:top; padding-left: 10px; background-color:#e0e0e0; font-size:8pt; color:#808080;">
                Creado por:&nbsp;<asp:Label ID="lblCreateBy" runat="server" />&nbsp;en&nbsp;<asp:Label ID="lblCreateOn" runat="server"/>&nbsp;&#44;&nbsp;Modificado por:&nbsp;<asp:Label ID="lblModifiedBy" runat="server"/>&nbsp;en&nbsp;<asp:Label ID="lblModifiedOn" runat="server"/>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
