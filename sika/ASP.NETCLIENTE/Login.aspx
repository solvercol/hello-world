﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ASP.NETCLIENTE.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.: Sika :.</title>
    <link rel="shortcut icon" href="~/Resources/images/favico.png" type="image/x-icon"/>
    <link rel="stylesheet" type="text/css" href="Resources/Styles/Site.css"/>
</head>
<body style=" background-color:#2F323B;">
    <form id="Form1" method="post" runat="server" class="center">
        <div class="container980">
            <div id="HeadderBg">
            
            </div>
            <div style=" background-color:White;">
                <div id="bgIzquierda"></div>
                <div id="bgDerecha">
                    <div class="TitleLoginMensaje">
                        Bienvenido!
                    </div>
                    <div class="LoginMensaje">
                        Este es el sistema de Gestión de Reclamos y Calidad en el que
                        podrá registrar los reclamos y documentos de calidad y saber al instante su estado.
                    </div>
                </div>
	            <div id="loginarea">
	                <div id="login">
	                    <div id="cuyahoga"></div>
                        <br />
                        <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername" Text="Username"></asp:Label>
		                <asp:TextBox id="txtUsername" runat="server" MaxLength="18"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredtxtUsername" ControlToValidate="txtUsername" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
		                <br />
                        <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" Text="Password"></asp:Label>
		                <asp:TextBox id="txtPassword" runat="server" MaxLength="18" textmode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredtxtPassword" ControlToValidate="txtPassword" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
		                <br />
		                <asp:Label id="lblError" runat="server" enableviewstate="False" visible="False" CssClass="loginerror"></asp:Label>
	                    <div id="btnLoginwrap"><asp:Button id="btnLogin" CssClass="btnLogin" runat="server" 
                                Text="Login" onclick="BtnLoginClick"></asp:Button></div>
	                </div>
	            </div>
            </div>
           <div  class="FooterDiv">
                © 2014 - Diseñado por Solver de Colombia Ltda. para Sika Colombia S.A.
           </div>
        </div>
	</form>
</body>
</html>
