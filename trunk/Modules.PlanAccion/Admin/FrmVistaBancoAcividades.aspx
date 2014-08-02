<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmVistaBancoAcividades.aspx.cs" Inherits="Modules.PlanAccion.Admin.FrmVistaBancoAcividades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  <div style="padding:3px; text-align:right;">
        <asp:Button ID="btnNuevo" runat="server" Text="Nueva Actividad" OnClick="BtnNuevaClick" />
    </div>
    <table class="tbl" width="100%">
                    
            <asp:repeater id="rptPartes" runat="server" >
			<headertemplate>
                    <tr>
                    <th style="width:8%">Código</th>
					<th>Descripción</th>
                    <th style="width:7%">Creado</th>
                    <th style="width:15%">...</th>
                </tr>
			</headertemplate>
			<itemtemplate>
				<tr>
					<td class="center"><%# DataBinder.Eval(Container.DataItem, "Codigo")%></td>
					<td class="izquierda"><%# DataBinder.Eval(Container.DataItem, "Descripcion")%></td>
                    <td class="center"><%# DataBinder.Eval(Container.DataItem, "CreateOn", "{0:dd/mm/yyyy}")%></td>
                    <td class="center">
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdActividad") %>'></asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" OnClientClick="return confirm('Confirma que desea eliminar el registro actual?');" CommandName="Eliminar" CommandArgument='<%# Eval("IdActividad") %>'></asp:LinkButton>
                        <asp:LinkButton ID="lnkConfig" runat="server" Text="Configuración" CommandName="Config" CommandArgument='<%# Eval("IdActividad") %>'></asp:LinkButton>
                    </td>
				</tr>
				</itemtemplate>
			</asp:repeater>
                    
    </table>   
    <div class="pager" style="width:100%">
			<csc:pagerlinq
            id="pgrListado" 
            runat="server"
            OnPageChanged="PgrListadoPageChanged"  
            pagesize="20"></csc:pagerlinq>
	 </div>	
        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
