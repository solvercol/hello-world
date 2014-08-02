<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmUsuarios.aspx.cs" Inherits="ASP.NETCLIENTE.Pages.Globals.FrmUsuarios" %>

<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
	    <asp:button id="btnNew" runat="server" OnClick="BtnNewClick" text="Nuevo Usuario" ToolTip="Nuevo registro."></asp:button>
    </div>

    <div class="group">
			    <p>
			     Login<asp:textBox ID="txtFiltroUsuario" runat="server"></asp:textBox>
                 Nombres <asp:textbox id="txtFiltroNombres" runat="server"></asp:textbox>
                 <asp:button id="btnFiltrar" runat="server" OnClick="BtnFindClick" text="Buscar"></asp:button>
			    </p>
		       <table id="users" class="tbl">
				    <asp:repeater id="rptUsers" runat="server" OnItemDataBound="RptUsersItemDataBound">
					    <headertemplate>
						    <tr>
							    <th>CWID Usuario</th>
							    <th>Nombres</th>
							    <th>Apellidos</th>
							    <th>Email</th>
							    <th>Activo</th>
							    <th>Fecha ultimo Acceso</th>
							    <th>Acceso Desde</th>
							    <th></th>
						    </tr>
					    </headertemplate>
					    <itemtemplate>
						    <tr>
							    <td><%# DataBinder.Eval(Container.DataItem, "NombreUsuario")%></td>
							    <td><%# DataBinder.Eval(Container.DataItem, "Nombres")%></td>
							    <td><%# DataBinder.Eval(Container.DataItem, "Apellidos")%></td>
							    <td><%# DataBinder.Eval(Container.DataItem, "Email") %></td>
							    <td align="center"><asp:CheckBox ID="ckhActivo" runat="server" /></td>
							    <td><asp:label id="lblLastLogin" runat="server"></asp:label></td>
							    <td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "LastIp") %></td>
							    <td>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Editar"></asp:LinkButton>
							    </td>
						    </tr>
					    </itemtemplate>
                        <AlternatingItemTemplate>
                            <tr class="AlternateGridStyle">
                                <td><%# DataBinder.Eval(Container.DataItem, "NombreUsuario")%></td>
							    <td><%# DataBinder.Eval(Container.DataItem, "Nombres")%></td>
							    <td><%# DataBinder.Eval(Container.DataItem, "Apellidos")%></td>
							    <td><%# DataBinder.Eval(Container.DataItem, "Email") %></td>
							    <td align="center"><asp:CheckBox ID="ckhActivo" runat="server" /></td>
							    <td><asp:label id="lblLastLogin" runat="server"></asp:label></td>
							    <td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "LastIp") %></td>
							    <td>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Editar"></asp:LinkButton>
							    </td>
						    </tr>
                        </AlternatingItemTemplate>
				    </asp:repeater>
			    </table>
	 </div>
				
	<div class="group pager">
				<csc:pager 
                id="pgrUsers" 
                runat="server" 
                OnPageChanged="PgrUsersPageChanged"  
                controltopage="rptUsers" 
                cachedatasource="True" 
                pagesize="10" 
                CacheDuration="10"></csc:pager>
	 </div>			

</asp:Content>