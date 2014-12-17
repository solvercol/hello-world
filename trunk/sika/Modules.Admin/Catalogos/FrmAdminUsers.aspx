<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAdminUsers.aspx.cs" Inherits="Modules.Admin.Catalogos.FrmAdminUsers" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">            
    <tr>
        <td style="width:10%;" class="Etiquetas">
            Buscar:
        </td>
        <td style="width:5px;"></td>
        <td valign="middle" style="width:60%;" class="Line">
            <asp:TextBox ID="txtFilter" runat="server" Width="90%" MaxLength="100"></asp:TextBox>       
            <asp:Button ID="btnFiltrar" runat="server" CausesValidation="false" Text="Filtrar" OnClick="BtnFiltrarClick" />           
        </td>
        <td align="right" style="width:30%;">   
            
            <asp:button id="btnNew" runat="server" Visible="true" OnClick="BtnNewClick" text="Nuevo Usuario"></asp:button>
        </td>
    </tr>
</table>

<div style="padding:3px; text-align:right;">  
</div>

<table style="width:100%" cellpadding="0" cellspacing="0">
    <tr>
                
        <td >
            

                <asp:UpdatePanel ID="upPrincipal" runat="server"> 
                <ContentTemplate>
                    <table class="tbl" width="100%">
                    <asp:repeater id="rptListado" runat="server" 
                    OnItemCommand ="RptListadoItemCommand"
                    OnItemDataBound="RptListadoItemDataBound">
	                    <headertemplate>
		                    <tr>
		                        <th>Código Usuario</th>
			                    <th>Nombre</th>
			                    <th>Fecha Ingreso</th>
                                <th>Último acceso</th>
                                <th>Nombre Usuario</th>
                                <th>email</th>
			                    <th>Creado </th>	
                                <th>Modificado </th>			                    
			                    <th style="width:50px;">Activo</th>
			                    <th style="width:30px;"></th>
		                    </tr>
	                    </headertemplate>
	                    <itemtemplate>
		                    <tr>
		                        <td align="left"><asp:Literal ID="litUseCode" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litName" runat="server"></asp:Literal></td>
			                    <td align="left"><%# DataBinder.Eval(Container.DataItem, "FechaIngreso", "{0:d}")%></td>
                                 <td align="left"><%# DataBinder.Eval(Container.DataItem, "lastlogin", "{0:G}")%></td>
                                <td align="left"><asp:Literal ID="litUsername" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litEmail" runat="server"></asp:Literal></td>
			                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "CreateOn", "{0:d}")%></td>
                                 <td align="center"><%# DataBinder.Eval(Container.DataItem, "ModifiedOn", "{0:d}")%></td>
			                    <td align="center"><asp:CheckBox id="chkActivo" runat="server" Enabled="false"></asp:CheckBox></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Edit" runat="server" />
			                    </td>
		                    </tr>
	                    </itemtemplate>
                        <AlternatingItemTemplate>
                            <tr class="AlternateGridStyle">
				                <td align="left"><asp:Literal ID="litUseCode" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litName" runat="server"></asp:Literal></td>
			                    <td align="left"><%# DataBinder.Eval(Container.DataItem, "FechaIngreso", "{0:d}")%></td>
                                 <td align="left"><%# DataBinder.Eval(Container.DataItem, "lastlogin", "{0:G}")%></td>
                                <td align="left"><asp:Literal ID="litUsername" runat="server"></asp:Literal></td>
                                <td align="left"><asp:Literal ID="litEmail" runat="server"></asp:Literal></td>
			                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "CreateOn", "{0:d}")%></td>
                                 <td align="center"><%# DataBinder.Eval(Container.DataItem, "ModifiedOn", "{0:d}")%></td>
			                    <td align="center"><asp:CheckBox id="chkActivo" runat="server" Enabled="false"></asp:CheckBox></td>
			                    <td align="center">
				                    <asp:LinkButton ID="CmdEditar" CausesValidation="false" Text="Edit" runat="server" />
			                    </td>
		                    </tr>
                        </AlternatingItemTemplate>
                    </asp:repeater>
                    </table>

                    <div class="pager">
				            <csc:PagerLinq ID="pgrListado" runat="server" PageSize="15" OnPageChanged="PgrChanged"/>
	                </div>	
                </ContentTemplate>
                </asp:UpdatePanel>
              
                        
        </td>
        
       
    
    </tr>

</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
