﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Infatlan_Kanban.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>Kanban Board | Login</title>
    <link rel="stylesheet" href="/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="/vendors/base/vendor.bundle.base.css">
    <link rel="stylesheet" href="/css/style.css">
    <link rel="shortcut icon" href="/assets/images/favicon.fw.png" />
    <link href="/css/pages/login-register-lock.css" rel="stylesheet">
    <link href="/css/style.min.css" rel="stylesheet">
</head>
<body>
    <div id="MyDiv" runat="server"  ></div>
    <section id="wrapper" class="login-register ">
        <div class="login-box card" style="border-radius: 20px; zoom:75%">
            <div class="card-body" >
                <form class="form-horizontal form-material m-t-40 text-center" id="loginform" runat="server">
                    <a class="db"><img src="../assets/images/texto_logo_azul.png" width="210"  height="35" alt="Home" /></a>
                    <h3 class="m-t-20"><b>Bienvenidos | Kanban Board</b></h3>
                    <h6 class="font-weight-light">Ingrese sus credenciales.</h6>
                    <br />
                        <div class="form-group m-t-40">
                            <div class="col-xs-12">
                                <asp:TextBox ID="TxUsername" class="form-control form-control-lg border-left-0" placeholder="Usuario" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <asp:TextBox ID="TxPassword" TextMode="Password" class="form-control form-control-lg border-left-0" placeholder="Contraseña" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />

                        <div class="form-group text-center">
                                <asp:Button ID="BtnLogin" class="btn btn-block btn-lg font-weight-medium auth-form-btn" style="background-color:#00468c;  color: #ffffff;" runat="server" Text="Entrar"  OnClick="BtnLogin_Click" />                              
                        </div>

                    <asp:HiddenField ID="HiddenField1" runat="server" />

                        <div class="my-2 d-flex justify-content-center align-center" style="display: flex; color: #D9272E; justify-content: center">
                            <asp:Label ID="LbMensaje" runat="server" Text=""></asp:Label>
                        </div>
                </form>
            </div>
        </div>
    </section>

    <script src="/assets/node_modules/jquery/jquery-3.2.1.min.js"></script>
    <script src="/assets/node_modules/popper/popper.min.js"></script>
    <script src="/assets/node_modules/bootstrap/dist/js/bootstrap.min.js"></script>

    
</body>
</html>
