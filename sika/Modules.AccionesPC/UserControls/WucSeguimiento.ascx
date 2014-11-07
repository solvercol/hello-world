<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucSeguimiento.ascx.cs" Inherits="Modules.AccionesPC.UserControls.WucSeguimiento" %>


<table class="tbl" width="100%">
                    
        <asp:repeater id="rptListado" runat="server" OnItemDataBound="RptListadoItemDatBound">
		<headertemplate>
             <tr>
                    <th  style="width:15%" align="left" >
                            Fecha
                    </th>
                    <th  style="width:20%" align="left">
                            Autor
                    </th>
                    <th  style="width:15%" align="left">
                           Estado Anterior
                    </th>
                    <th style="width:20%" align="left">
                            Acción
                    </th>
                    <th style="width:15%" align="left">
                            Nuevo Estado    
                    </th>
                    <th style="width:20%" align="left">
                            Nuevo Responsable                        
                    </th>
              </tr>
		</headertemplate>
		<itemtemplate>
			<tr>
					<td style="width:15%" align="left">
                        <asp:Literal ID="litDate" runat="server"></asp:Literal>
                    </td>
					<td style="width:20%" align="left"><%# DataBinder.Eval(Container.DataItem, "Autor")%></td>
					<td style="width:15%" align="left"><%# DataBinder.Eval(Container.DataItem, "EstadoAnterior")%></td>	
                    <td align="left" valign="middle" style="width:20%">
                        <%# DataBinder.Eval(Container.DataItem, "Accion")%>
					</td>
                    <td align="left" valign="middle" style="width:15%">
                        <%# DataBinder.Eval(Container.DataItem, "Nuevoestado")%>
					</td>
                    <td style="width:20%" align="left">
                            <%# DataBinder.Eval(Container.DataItem, "NuevoResponsable")%>
                    </td>
			</tr>
			</itemtemplate>
		</asp:repeater>
                    
</table>  