﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmError.aspx.cs" Inherits="ASP.NETCLIENTE.FrmError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>.: SIKA :.</title>
     <link href="~/Resources/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
       javascript: window.history.forward(1); 
    </script> 
</head>
<body style=" background-color:#2F323B;">
    <form id="form1" runat="server" class="center">        
        <div id="errorarea" >
	        <div id="AreaError">
	            <div id="ZonaErrorbayer" style="width:100%"></div>
		        <div class="hidden"></div>
		        <div style="height:10px;">
                    
                </div>
                    <h2 style=" color:Red;">
                            <asp:Label ID="lblTituloError" runat="server"></asp:Label>
                    </h2>
		      
		            <h1>
                        <asp:Label ID="lblErrorCode" runat="server"></asp:Label>
                    </h1>
	        </div>
	    </div>        
    </form>
</body>
</html>
