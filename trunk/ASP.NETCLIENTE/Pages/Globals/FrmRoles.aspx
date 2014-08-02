<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmRoles.aspx.cs" Inherits="ASP.NETCLIENTE.Pages.Globals.FrmRoles" %>

<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script language="javascript" type="text/javascript">

    function MantenSesion() {
        var CONTROLADOR = "../httpHandler/MantenerSession.ashx";
        var head = document.getElementsByTagName('head').item(0);
        script = document.createElement('script');
        script.src = CONTROLADOR;
        script.setAttribute('type', 'text/javascript');
        script.defer = true;
        head.appendChild(script);
    }
    setInterval('MantenSesion()', 120000);
   
</script>

    <div style="text-align:left; vertical-align:middle;"  class="ToolBar">
	    <asp:button id="btnNew" runat="server" Visible="false" OnClick="BtnNewClick" text="Nuevo Rol"></asp:button>
    </div>
    <div class="group">
			    <p>
                 Rol <asp:textbox id="txtFiltroNombres" runat="server"></asp:textbox>
                 <asp:button id="btnFiltrar" runat="server" OnClick="BtnFindClick" text="Buscar"></asp:button>
			    </p>
		
			    <table id="users" class="tbl">
				    <asp:repeater id="rptListado" runat="server" OnItemDataBound="RptListadoItemDataBound">
					    <headertemplate>
						    <tr>
							    <th>Rol</th>
							    <th>Activo</th>
							    <th>Fecha Actualización</th>
							    <th></th>
						    </tr>
					    </headertemplate>
					    <itemtemplate>
						    <tr>
							    <td><%# DataBinder.Eval(Container.DataItem, "NombreRol")%></td>
                                <td align="center"><asp:CheckBox ID="ckhGlobal" runat="server" /></td>					    
							    <td><asp:label id="lblDate" runat="server"></asp:label></td>
							    <td align="center">
								    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"></asp:LinkButton>
							    </td>
						    </tr>
					    </itemtemplate>
                        <AlternatingItemTemplate>
                            <tr class="AlternateGridStyle">
                                <td><%# DataBinder.Eval(Container.DataItem, "NombreRol")%></td>
                                <td align="center"><asp:CheckBox ID="ckhGlobal" runat="server" /></td>					    
							    <td><asp:label id="lblDate" runat="server"></asp:label></td>
							    <td align="center">
								    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"></asp:LinkButton>
							    </td>
						    </tr>
                        </AlternatingItemTemplate>
				    </asp:repeater>
			    </table>
			</div>
				
			<div class="group pager">
				<csc:pager 
                id="pgrListado" 
                runat="server" 
                OnPageChanged="PgrListadoPageChanged"  
                controltopage="rptListado" 
                cachedatasource="True" 
                pagesize="10" 
                CacheDuration="10"></csc:pager>
			</div>
			
</asp:Content>