<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucCierreSolicitud.ascx.cs" Inherits="Modules.AccionesPC.UserControls.WucCierreSolicitud" %>

<table width="100%" class="tblBuscador" cellpadding="0" cellspacing="0">            
    <tr>
        <td style="width:50%;" class="Etiquetas">
        <asp:Literal ID="litText" runat="server"></asp:Literal>
        </td>
        <td class="Separador15"></td>
        <td valign="middle" style="width:50%;"  align="center">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" class="Etiquetas">
                         La no conformidad fue eliminada?
                    </td>
                </tr>
                 <tr>
                    <td align="center">
                        <asp:RadioButtonList ID="rblEliminada" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="80">
                            <asp:ListItem Value="true">SI</asp:ListItem>
                            <asp:ListItem Value="false">NO</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
           
        </td>
    </tr>
    <tr>
        <td align="center">
          <table cellpadding="0" cellspacing="0" width="180px">

                <tr>
                    <td style="width:100px" align="center">
                        Adecuada
                    </td>
                    <td style="width:80px" align="center">
                       Eficáz
                    </td>
                </tr>


                <tr>
                    <td style="width:100px" align="center">
                        <asp:CheckBox ID="chkAdecuada" runat="server"/>
                    </td>
                    <td style="width:80px" align="center">
                       <asp:CheckBox ID="chkEficaz" runat="server"/>
                    </td>
                </tr>

            </table>   
        </td>
        <td></td>
        <td class="Etiquetas">
            Observaciones:
           <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Width="95%" Height="50"></asp:TextBox>
        </td>
    </tr>
   
    <tr>
            <td colspan="3" class="validator">
                <asp:Label ID="litError" runat="server" Visible="false"></asp:Label>
            </td>
    </tr>                         
 </table>