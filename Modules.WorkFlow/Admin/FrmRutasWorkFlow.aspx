<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmRutasWorkFlow.aspx.cs" Inherits="Modules.WorkFlow.Admin.FrmRutasWorkFlow" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
      <table width="100%" cellpadding="0" cellspacing="0" class="tblSecciones">
        <tr>
            <td class="SeparadorVertical" colspan="4">            
            </td>        
        </tr>
        <tr>
            <td style=" width:1%"></td>
            <th style=" width:20%">
                   
            </th>
            <td style=" width:1%"></td>
            <td>
           <%-- <asp:UpdatePanel ID="upControles" runat="server">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style=" width:50%" align="right">
                                 <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="BtnNuevoClick" CausesValidation="false"  />
                                 <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardarClick" Visible="false"  />
                                 <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="BtnEliminarClick" Visible="false"  CausesValidation="false"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
                
                
            </td>
        </tr> 
        <tr>
            <td class="SeparadorVertical" colspan="4">            
            </td>        
        </tr>
     </table>

     <table width="100%" class="FondoGrisSeccionesMenu" cellpadding="0" cellspacing="0"> 
        <tr>
            <td>        
                <asp:UpdatePanel ID="upMenu" runat="server">
                    <ContentTemplate>  
                        <asp:Menu ID="mnuSecciones" runat="server" Orientation="Horizontal" OnMenuItemClick="MnuItemClick" BackColor="#F5F5F5" Width="100%"> 
                        <StaticSelectedStyle ForeColor="#FFFFFF" BackColor="#F5BA38" BorderStyle="None" Font-Bold="True" CssClass="center"  />
                        <StaticMenuItemStyle ForeColor="#7D7D7C"  BorderStyle="solid" BorderWidth="0" Font-Size="10px" Font-Names="Arial" CssClass="center" 
                            ItemSpacing="4px" HorizontalPadding="1px" Width="120" VerticalPadding="0px" BackColor="#F5F5F5"
                            Height="15px" />
                        </asp:Menu>               
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    <div style=" margin-top:2px;">    
            <asp:UpdatePanel ID="upContainer" runat="server" ChildrenAsTriggers="true">
                   <ContentTemplate>  
                        <div style="width:100%;">
                              <asp:PlaceHolder ID="phlContent"  runat="server"></asp:PlaceHolder>
                        </div>  
                   </ContentTemplate>
            </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
