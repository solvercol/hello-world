<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WucNewFolder.ascx.cs" Inherits="Modules.DocumentLibrary.UserControls.WucNewFolder" %>

 <script type="text/javascript">
     var divError = 'DivModal';
     function MostrarDivError() {
         var adiv = $get(divError);
         adiv.style.visibility = 'visible';
     }
    </script>

 <div id="DivModal">
                     <div id="VentanaMensaje">
                               <div id="Msg">
                                    <img id="Img1"  src="~/Resources/images/Barloading.gif" runat="server" alt="" />
                               </div>
                            </div>
</div>

<table width="100%" class="tblPreView">
    
     <tr>
        <td align="center" colspan="2">
            <div style=" text-align:left;">
                <asp:Image id="Image1" runat="server" ImageUrl="~/Resources/images/CrearCarpetaTitulo.gif"></asp:Image>
            </div>  
        </td>
    </tr>

    <tr>
        <th style="width:10%">
            Categoría:
        </th>
        <td style="width:90%">
            <asp:DropDownList ID="ddlcategoria" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <th>
            Nombre&nbsp;Carpeta:
        </th>
        <td>
            <asp:TextBox ID="txtNombreCarpeta" runat="server" Width="90%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ValidationGroup="MyValidationGroup" ControlToValidate="txtNombreCarpeta" ErrorMessage="*" Text="*" CssClass="validator" ></asp:RequiredFieldValidator>
        </td>
    </tr>

    <tr>
        <td colspan="2">
            <table class="MessageEdit" width="100%" id="tblMessageEdit" runat="server" visible="false">
                <tr>
                    <td style="width:10px;" align="left">
                                            
                    </td>
                    <td align="left">
                        <asp:Literal ID="litMessageError" runat="server"></asp:Literal>
                    </td>
                </tr>
           </table>
        </td>
    </tr>

    <tr>
        <td></td>
        <td align="right" style="padding-right:20px;">
             <asp:Button 
            ID="btnSave" 
            CausesValidation="true" 
            ValidationGroup="MyValidationGroup"
            runat="server" 
            CssClass="BotonConfirm" OnClientClick="MostrarDivError();"
            Text="Aceptar" 
            onclick="OkButtonClick"/>
        </td>
    </tr>

</table>