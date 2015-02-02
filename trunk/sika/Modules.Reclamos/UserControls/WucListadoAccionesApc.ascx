<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucListadoAccionesApc.ascx.cs" Inherits="Modules.Reclamos.UserControls.WucListadoAccionesApc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="csc" Namespace="ServerControls" Assembly="ServerControls" %>

<asp:UpdatePanel ID="upListado" runat="server">
   <ContentTemplate>
                
              
                   
                            
                            <asp:Panel id="pnlContainer" style="width:100%;float: left; " ScrollBars="Auto" runat="server" Height="250px">
                                <table width="100%" cellpadding="0" cellspacing="0" class="tbl" >
				                    <asp:repeater 
                                    id="rptListado" 
                                    OnItemDataBound="RptListadoItemDatBound"
                                    runat="server">		
                                    
                                        <HeaderTemplate>
                                            <th>No</th>
                                            <th>F.Solicitd</th>
                                            <th>Acción</th>
                                            <th>Proceso</th>
                                            <th>Tipo</th>
                                            <th>...</th>
                                        </HeaderTemplate>
                                    	                                               
					                    <itemtemplate>
						                <tr>
							                <td style="width:10%" align="left">
                                               <%# Eval("Codigo")%>
                                            </td>
							                <td style="width:10%" align="left">
                                                <%# Eval("FechaSolicitud","{0:d}")%>
                                            </td>
                                            <td style="width:30%" align="left">
                                                <%# Eval("DescripcionAccion")%>
                                            </td>
                                            <td style="width:30%" align="left">
                                                <%# Eval("Proceso")%>
                                            </td>
                                            <td style="width:18%" align="left">
                                                 <%# Eval("TipoAccion")%>
                                            </td>
							                <td style="width:2%" align="left">
                                                <asp:ImageButton 
                                                ID="ImgSelect" CausesValidation="false"
                                                BorderStyle="None"
                                                ToolTip="Select"
                                                runat="server" 
                                                OnClick="BtnSelectClick"
                                                ImageUrl="~/Resources/Images/select.png" />
                                            </td>
						                </tr>
					                    </itemtemplate> 
				                    </asp:repeater>
			                      </table>      
                                          
                            </asp:Panel>                          

                        
              
     </ContentTemplate>
 </asp:UpdatePanel>
