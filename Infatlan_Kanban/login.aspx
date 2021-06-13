<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Infatlan_Kanban.login" %>

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
    <div id="MyDiv" runat="server"   ></div>
    <section id="wrapper" class="login-register " style="  background-image:url('../images/fondo9.png');">
        <div class="align-content-end">
      
   <%--    <div class="login-box card"   style="border-radius: 20px; box-shadow:5px 5px 0px #00468c" >--%>
            <div class="login-box card"   style="border-radius: 20px;" >
                <div class="card-body" ">

                      <form class="form-horizontal"  id="Form1" runat="server"   >
                            <div align="center"><img src="../assets/images/INFATLAN.png" width="210"  height="40" alt="Home"  /></div>
                        
                        <h3 class="m-t-20  text-center"><b>Bienvenidos | Kanban Board</b></h3>
                        <h6 class="font-weight-light  text-center">Ingrese sus credenciales.</h6>
                        <br />
                                <div class="form-group">
                                    <label for="exampleInputEmail" style="text-align:center">Usuario</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="mdi mdi-account-outline text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="TxUsername" class="form-control form-control-lg border-left-0" placeholder="Username"  runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword">Password</label>
                                    <div class="input-group">
                                        <div class="input-group-prepend bg-transparent">
                                            <span class="input-group-text bg-transparent border-right-0">
                                                <i class="mdi mdi-lock-outline text-primary"></i>
                                            </span>
                                        </div>
                                        <asp:TextBox ID="TxPassword" TextMode="Password" class="form-control form-control-lg border-left-0" placeholder="Password"  runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="my-2 d-flex justify-content-between align-items-center">
                                </div>
                                <div class="my-3">
                                    <asp:Button ID="BtnLogin" class="btn btn-block btn-lg font-weight-medium auth-form-btn" style="background-color:#00468c;  color: #ffffff;" runat="server" Text="Ingresar"   OnClick="BtnLogin_Click" />                              
   
                                
                                </div>

                                <div class="my-2 d-flex justify-content-center align-center" style="color:indianred;">
                                    <asp:Label ID="LbMensaje" runat="server" Text=""></asp:Label>
                                </div>

                            </form>

                    
             <%--       <form class="form-horizontal form-material m-t-40 text-center"  id="loginform" runat="server"   >
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
                                    <asp:Button ID="BtnLogin" class="btn btn-block btn-lg font-weight-medium auth-form-btn" style="background-color:#00468c;  color: #ffffff;" runat="server" Text="Ingresar"  OnClick="BtnLogin_Click" />                              
                            </div>

                        <asp:HiddenField ID="HiddenField1" runat="server" />

                            <div class="my-2 d-flex justify-content-center align-center" style="display: flex; color: #D9272E; justify-content: center">
                                <asp:Label ID="LbMensaje" runat="server" Text=""></asp:Label>
                            </div>
                    </form>--%>

    
                </div>
            </div>

                </div>
            </section>


                        



    <script src="/assets/node_modules/jquery/jquery-3.2.1.min.js"></script>
    <script src="/assets/node_modules/popper/popper.min.js"></script>
    <script src="/assets/node_modules/bootstrap/dist/js/bootstrap.min.js"></script>



     


<%--    <script src="/vendors/base/vendor.bundle.base.js"></script>
    <script src="/js/off-canvas.js"></script>
    <script src="/js/hoverable-collapse.js"></script>
    <script src="/js/template.js"></script>--%>

    
</body>
</html>
