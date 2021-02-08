<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="miTablero.aspx.cs" Inherits="Infatlan_Kanban.pages.miTablero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>

    <%--        <script type="text/javascript">   
            var UpdatePanel1 = '<%=UpdatePanel1.ClientID%>';
            $(function () {
                $("#btnModal").click(function () {
                    document.getElementById("<%=TxTitulo.ClientID%>").value = $(this).data('titulo');
                __doPostBack(UpdatePanel1, '');
            });
        });
    </script>--%>


    <script type="text/javascript">
        function ModalTarjetaOpen() { $('#ModalTarjeta').modal('show'); }
        function ModalTarjetaClose() { $('#ModalTarjeta').modal('hide'); }

        function ModalTarjetaConfirmarOpen() { $('#ModalTarjetaConfirmar').modal('show'); }
        function ModalTarjetaConfirmarClose() { $('#ModalTarjetaConfirmar').modal('hide'); }

        function ModalTarjetaCrearOpen() { $('#ModalTarjetaCrear').modal('show'); }
        function ModalTarjetaCrearClose() { $('#ModalTarjetaCrear').modal('hide'); }

        
    </script>





    <link href="dist/css/pages/tab-page.css" rel="stylesheet">
    <link href="../dist/css/pages/tab-page.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <progresstemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </progresstemplate>
    </asp:UpdateProgress>


<%--    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="card-title"><strong>Kanban Board Gestiones Técnicas </strong></h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Gestiones Técnicas</a></li>
                    <li class="breadcrumb-item active">Mi Tablero</li>
                </ol>
                <asp:UpdatePanel runat="server" ID="UpTarjetaAdd" UpdateMode="Conditional">
                    <contenttemplate>
                        <asp:Button ID="BtnAddTarjeta" class="btn btn-info d-none d-lg-block m-l-15" runat="server" Text="Crear Tarjeta" OnClick="BtnAddTarjeta_Click" />                       
                    </contenttemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                    <contenttemplate>                
                        <asp:Button ID="BtnBusqueda" class="btn btn-success d-none d-lg-block m-l-15" runat="server" Text="Búsqueda"  OnClick="BtnBusqueda_Click"/> 
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>--%>


    <div class="row page-titles">
        <div class="col-md-12">
            <h4 class=" text-dark">Kanban Board | Gestiones Técnicas</h4>
        </div>
        <div class="col-md-6">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                <li class="breadcrumb-item active">Mi Tablero</li>
            </ol>
        </div>
        <div class="col-md-6 text-right">
<%--                            <asp:UpdatePanel runat="server" ID="UpTarjetaAdd" UpdateMode="Conditional">
                    <contenttemplate>
                        <asp:Button ID="BtnAddTarjeta" class="btn btn-info d-none d-lg-block m-l-15" runat="server" Text="Crear Tarjeta" OnClick="BtnAddTarjeta_Click" />                       
                    </contenttemplate>
                </asp:UpdatePanel>--%>
        </div>
    </div>


<%--<div class="card text-white mb-3" style="max-width: 18rem;">
  <div class="card-header" style="background-color: #03a9f3">Header</div>
  <div class="card-body" style="background-color:#EBF5FB >
    <h5 class="card-title">Primary card title</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
  </div>
</div>--%>

    <div class="row">
        <div class="col">
            <div class="card">
                <div class="card-body">

                    <h4 class="card-title">Tablero Kanban Board</h4>
                    <h6 class="card-subtitle">Listado de tarjetas.</h6>
                    <div class="card-body">
                        <div class="form-group row">
                            <div class="row col-12">
                                <div class="row col-11">
                                    <label class="col-1 col-form-label">Búsqueda</label>
                                    <div class="col-3">
                                        <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" class="form-control text-uppercase"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox runat="server" Visible="true" PlaceHolder="Ingrese texto y presione Enter" ID="TextBox1" AutoPostBack="true" class="form-control text-uppercase"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox runat="server" Visible="true" PlaceHolder="Ingrese texto y presione Enter" ID="TextBox2" AutoPostBack="true" class="form-control text-uppercase"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row col-1">
                                    <asp:UpdatePanel runat="server" ID="UpTarjetaAdd" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="BtnAddTarjeta" runat="server" title="Agregar" Text="Agregar" Style="background-color: #00468c; color: #ffffff;" class="btn" OnClick="BtnAddTarjeta_Click">
                                        <i class="fa fa-plus-circle text-white mr-2"></i>Crear Tarjeta
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col">
                                <div class="card">
                                    <div class="card-header" role="tab" id="EnCola">
                                        <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseEnCola" aria-expanded="true" aria-controls="collapseOne13">
                                            <div class="box text-center" style="background-color: #00468c; color: #ffffff; opacity: 0.9;">
                                                <h1 class="font-light text-white">
                                                    <asp:Label ID="LbEnCola" runat="server"></asp:Label></h1>
                                                <h6 class="text-white">En Cola</h6>
                                            </div>
                                        </a>
                                    </div>
                                    <div id="collapseEnCola" class="collapse show" role="tabpanel" aria-labelledby="EnCola">
                                        <div class="card-body">
                                            <asp:Literal Text="" ID="LitNotificacionesEnCola" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col">
                                <div class="card">
                                    <div class="card-header" role="tab" id="EnEjecucion">
                                        <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseEnEjecucion" aria-expanded="true" aria-controls="collapseOne13">
                                            <div class="box text-center" style="background-color: #00468c; color: #ffffff; opacity: 0.9;">
                                                <h1 class="font-light text-white">
                                                    <asp:Label ID="LbEjecucion" runat="server"></asp:Label></h1>
                                                <h6 class="text-white">En Ejecución</h6>
                                            </div>
                                        </a>
                                    </div>
                                    <div id="collapseEnEjecucion" class="collapse show" role="tabpanel" aria-labelledby="EnEjecucion">
                                        <div class="card-body">
                                            <asp:Literal Text="" ID="LitNotificacionesEjecucion" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col">
                                <div class="card">
                                    <div class="card-header" role="tab" id="Atrasados">
                                        <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseAtrasados" aria-expanded="true" aria-controls="collapseOne13">
                                            <div class="box text-center" style="background-color: #00468c; color: #ffffff; opacity: 0.9;">
                                                <h1 class="font-light text-white">
                                                    <asp:Label ID="LbAtrasados" runat="server"></asp:Label></h1>
                                                <h6 class="text-white">Atrasados</h6>
                                            </div>
                                        </a>
                                    </div>
                                    <div id="collapseAtrasados" class="collapse show" role="tabpanel" aria-labelledby="Atrasados">
                                        <div class="card-body">
                                            <asp:Literal Text="" ID="LitNotificacionesAtrasadas" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col">
                                <div class="card">
                                    <div class="card-header" role="tab" id="Detenidas">
                                        <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseDetenidas" aria-expanded="true" aria-controls="collapseOne13">
                                            <div class="box text-center" style="background-color: #00468c; color: #ffffff; opacity: 0.9;">
                                                <h1 class="font-light text-white">
                                                    <asp:Label ID="LbDetenidas" runat="server"></asp:Label></h1>
                                                <h6 class="text-white">Detenidas</h6>
                                            </div>
                                        </a>
                                    </div>
                                    <div id="collapseDetenidas" class="collapse show" role="tabpanel" aria-labelledby="Detenidas">
                                        <div class="card-body">
                                            <asp:Literal Text="" ID="LitNotificacionesDetenidas" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col">
                                <div class="card">
                                    <div class="card-header" role="tab" id="Completados">
                                        <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseCompletados" aria-expanded="true" aria-controls="collapseOne13">
                                            <div class="box text-center" style="background-color: #00468c; color: #ffffff; opacity: 0.9;">
                                                <h1 class="font-light text-white">
                                                    <asp:Label ID="LbCompletados" runat="server"></asp:Label></h1>
                                                <h6 class="text-white">Completados Hoy</h6>
                                            </div>
                                        </a>
                                    </div>
                                    <div id="collapseCompletados" class="collapse show" role="tabpanel" aria-labelledby="Completados">
                                        <div class="card-body">
                                            <asp:Literal Text="" ID="LitNotificacionesCompletadosHoy" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
        <contenttemplate>   
            <div class="row" runat="server" id="DivBusqueda" visible="false">
                <div class="col">
                    <div class="card">
                        <div class="card-header" role="tab" id="Busqueda">
                            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseBusqueda" aria-expanded="true" aria-controls="collapseOne13">
                                <div class="box" style="background-color: #D9272E; color: #ffffff; opacity: 0.9;">
                                     <h6 class="text-white"><i><img src="../images/busqueda.png" width="30"  /></i> Sección de Búsqueda</h6>
                                </div>
                            </a>
                        </div>
                        <div id="collapseBusqueda" class="collapse show" role="tabpanel" aria-labelledby="Busqueda">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-4">
                                        <label class="control-label">Tipo Búsqueda:</label>
                                        <asp:DropDownList ID="DdlBusqueda" runat="server" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Personal"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Equipo Trabajo"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Colaborador"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-4" runat="server" id="divEquipoTrabajo" visible="true">
                                        <label class="control-label">Equipo de Trabajo:</label>
                                        <asp:DropDownList runat="server" ID="DropDownList1" CssClass="select2 form-control custom-select" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="col-4" runat="server" id="divColaborador" visible="true">
                                        <label class="control-label">Colaborador:</label>
                                        <asp:DropDownList runat="server" ID="DropDownList2" CssClass="select2 form-control custom-select" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>




    <%--    MODAL INICIO--%>
    <div class="modal fade" id="ModalTarjeta" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 880px; height: 860px; top: 414px; left: 50%; transform: translate(-50%, -50%);">

                <div class="modal-header">
                    <asp:UpdatePanel ID="UpTitulo" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTitulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="HiddenId" OnValueChanged="btnTickectEvento" runat="server" />
                    <asp:UpdatePanel runat="server" ID="UPFormulario" UpdateMode="Conditional">
                        <contenttemplate>
                    <div class="vtabs">
                        <ul class="nav nav-tabs tabs-vertical" role="tablist">
                            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab" runat="server" id="tabGenerales"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspGenerales</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" runat="server" role="tab" id="tabAdjuntos"  visible="true"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspAdjuntos</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentarios" role="tab" runat="server" id="tabComentarios"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspComentarios</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#historial" role="tab" runat="server" id="tabHistorial"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspHistorial</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#solucion" role="tab" runat="server" id="tabSolucion"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspSolución</span></a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content" style="height: 550px; width: 600px;">
                            <div class="tab-pane active" id="home" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <div class="row p-t-20">
                                            <div class="col-8">
                                                <label class="control-label">Título:</label>
                                                <asp:TextBox ID="TxTitulo" AutoPostBack="true" runat="server" class="form-control text-uppercase" OnTextChanged="TxTitulo_TextChanged"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Tiempo (min):</label>
                                                <asp:TextBox ID="TxMinProductivo_1" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-4">
                                                <label class="control-label">Fecha Solicitud:</label>
                                                <asp:TextBox ID="TxFechaSolicitud_1" AutoPostBack="true" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Fecha Inicio:</label>
                                                <asp:TextBox ID="TxFechaInicio_1" AutoPostBack="true" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Fecha Entrega:</label>
                                                <asp:TextBox ID="TxFechaEntrega_1" AutoPostBack="true" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-12">
                                                <label class="control-label">Descripción:</label>
                                                <asp:TextBox ID="TxDescripcion_1" AutoPostBack="true" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-8">
                                                <label class="control-label">Responsable:</label>
                                                <asp:DropDownList runat="server" ID="DdlResponsable_1" CssClass="select2 form-control custom-select" AutoPostBack="true" Style="width: 100%" OnSelectedIndexChanged="DdlResponsable_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Prioridad:</label>
                                                <asp:DropDownList ID="DdlPrioridad_1" runat="server" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-12">
                                                <label class="control-label">Tipo Gestión:</label>
                                                <asp:DropDownList runat="server" ID="DdlTipoGestion_1" CssClass="select2 form-control custom-select" Style="width: 100%" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row p-t-20" runat="server" id="divAdjunto">
                                    <div class="col-12">
                                        <label class="control-label">Archivos Adjuntos:</label>
                                        <asp:FileUpload ID="FuAdjunto_1" AllowMultiple="true" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>
                  
                            <div class="tab-pane" id="comentarios" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <div class="row p-t-20" runat="server" id="divComentarioAdd">
                                            <div class="col-2">
                                                <label class="control-label">Comentario:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:TextBox ID="TxComentario_1" TextMode="MultiLine" Rows="1" AutoPostBack="true" runat="server" class="form-control text-uppercase" OnTextChanged="TxComentario_1_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-2" style="text-align: center">
                                                <asp:LinkButton ID="BtnAddComentario_1" runat="server" Text="+" title="Añadir" class="btn" Style="background-color: #00468c; color: #ffffff;"  OnClick="BtnAddComentario_1_Click"></asp:LinkButton>
                                            </div>

                                        </div>

                                        <div class="col-md-12" runat="server" id="divAlertaComentario_1" visible="false" style="text-align: center">
                                            <p>
                                                <b><code>
                                                    <label class="col-form-label" runat="server" id="LbAlertaComentario_1"></label>
                                                </code></b>
                                            </p>
                                        </div>

                        

                                        <div class="col-md-12" runat="server" id="divComentarioLectura" visible="false">
                                            <div class="row col-12 mt-3">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvComentarioLectura" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        AllowPaging="true"
                                                        GridLines="None" OnPageIndexChanging="GvComentarioLectura_PageIndexChanging"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="usuarioComentario" Visible="true" HeaderText="Usuario" ItemStyle-Width="35%" />
                                                            <asp:BoundField DataField="comentario" HeaderText="Comentario" Visible="true" ItemStyle-Width="95%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane" id="adjunto" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel12">
                                    <ContentTemplate>

                                        <div class="col-md-12" runat="server" id="divAlertaNoAdjunto" visible="false" style="text-align: center">
                                            <p><b><code>Tarea no cuenta con archivos adjuntos</code></b></p>
                                        </div>

                                        <div class="col-md-12" runat="server" id="divAdjuntoLectura" visible="false">
                                            <div class="row col-12 mt-3">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvAdjuntoLectura" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        AllowPaging="true"
                                                        GridLines="None" OnPageIndexChanging="GvAdjuntoLectura_PageIndexChanging"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="idRows" Visible="false" ItemStyle-Width="27%" />
                                                            <asp:BoundField DataField="nombre" HeaderText="Archivo" Visible="true" ItemStyle-Width="95%" />
                                                            <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="BtnDescargar" runat="server" title="Descargar" class="btn btn-cyan" CommandArgument='<%# Eval("idRows") %>' CommandName="Descargar">
                                                                  <i class="fa fa-download"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane" id="historial" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel13">
                                    <ContentTemplate>
                                        <div class="row p-t-20" runat="server" id="divHistorial" visible="true">
                                            <div class="col-12">
                                                <!--Fin Fila 1-->
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvHistorial" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        AllowPaging="true" OnPageIndexChanging="GvHistorial_PageIndexChanging"
                                                        GridLines="None"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="usuarioCambio" Visible="true" HeaderText="Usuario" ItemStyle-Width="30%" />
                                                            <asp:BoundField DataField="cambio" Visible="true" HeaderText="Cambio" ItemStyle-Width="95%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                            <div class="tab-pane" id="solucion" role="tabpanel" style="height: 450px; width: 630px;">
                                <div class="col-lg-12">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel14" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <!--Inicio Fila 1-->
                                            <div class="row p-t-20" runat="server" id="divAccion">
                                                <div class="col-12">
                                                    <div class="form-group row">
                                                        <div class="col-2">
                                                            <label class="control-label">Acción:</label>
                                                        </div>
                                                        <div class="col-10">
                                                            <asp:DropDownList ID="DdlAccion" runat="server" CssClass="form-control" AutoPostBack="true">
                                                                <asp:ListItem Value="0" Text="Seleccione opción..."></asp:ListItem>

                                                                <asp:ListItem Value="1" Text="Cerrar Tarjeta Kanban"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Solicitud Cambio Estado a Detenido"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row p-t-20">
                                                <div class="col-12">
                                                    <div class="form-group row">
                                                        <div class="col-2">
                                                            <label class="control-label">Detalle:</label>
                                                        </div>
                                                        <div class="col-10">
                                                            <asp:TextBox ID="TxDetalle" AutoPostBack="true" TextMode="MultiLine" Rows="6" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                    <div class="form-group row" runat="server" id="divSolucionAdjunto">

                                        <!--Inicio Fila 1-->
                                        <div class="row col-12">
                                            <div class="col-2">
                                                <label class="col-form-label" runat="server" id="Label1">Archivo:</label>
                                            </div>
                                            <div class="col-10">
                                                <asp:FileUpload ID="FuSolucion" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

<%--                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>
                        <div class="col-md-12" runat="server" id="divAlertaGeneral" visible="false" style="text-align: center">
                            <p>
                                <b><code>
                                    <label class="col-form-label" runat="server" id="LbAdvertencia"></label>
                                </code></b>
                            </p>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <contenttemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmarTarea_1" runat="server" Text="Enviar"   OnClick="BtnConfirmarTarea_1_Click"  class="btn" Style="background-color: #00468c; color: #ffffff;"   />
                         </contenttemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>






    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalTarjetaFinalizada" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
<%--            <div class="modal-content" style="width: 800px; height: 910px; top: 452px; left: 50%; transform: translate(-50%, -50%);">--%>
                <div class="modal-content" style="width: 880px; height: 860px; top: 414px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <h4 class="modal-title" >
                                <strong>
                                    <asp:Label ID="LbTituloTarjetaFinalizad" runat="server" Text="Tarjeta Finalizada" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>

                     <%--style="padding-left: 40px; padding-right: 40px; padding-top: 20px; padding-bottom: 20px;"--%>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <contenttemplate>

                          <div class="row" runat="server" id="Div1">
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#D9272E;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label2" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Cola</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#D9272E;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label3" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Ejecución</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#D9272E;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label4" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Detenidas</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#D9272E;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label5" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Atrasados</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>

                          <h6 style="color:#D9272E"><b>-Cargabilidad Minutos Diarios:</b></h6>
                          <div class="table-responsive m-t-20">
                                <asp:GridView ID="GridView1" runat="server" Visible="true"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows" HeaderStyle-HorizontalAlign="center"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"  OnPageIndexChanging="GVDistribucion_PageIndexChanging"
                                    GridLines="None"
                                    PageSize="2">
                                    <Columns>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="center" />
                                        <asp:BoundField DataField="min" HeaderText="Mins Diarios" ItemStyle-HorizontalAlign="center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
            
                           <h6  style="color:#D9272E"><b>-Datos Generales:</b></h6>
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Título:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxTituloFinalizada" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Prioridad:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxPrioridadFinalizada" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Tiempo Productivo:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxTimeProductivoFinalizada" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Fecha Inicio:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxFechaInicioFinalizada" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Fecha Entrega:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxFechaFinFinalizada" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Solución:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxSolucionFinalizada" TextMode="MultiLine" Rows="3" AutoPostBack="true" runat="server" class="form-control" ReadOnly="false"></asp:TextBox>
                                </div>
                            </div>

                        </contenttemplate>
                    </asp:UpdatePanel>


                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="Button2" runat="server" Text="Enviar" class="btn" style="background-color:#00468c;  color: #ffffff;"   />
                        </contenttemplate>

                        <%--                        <triggers>
                                <asp:PostBackTrigger ControlID="BtnConfirmarTarea" />
                        </triggers>--%>
                    </asp:UpdatePanel>
                </div>
                TxTitulo
            </div>
        </div>
    </div>





    <%--    MODAL INICIO--%>
    <div class="modal fade" id="ModalTarjetaCrear" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 880px; height: 860px; top: 414px; left: 50%; transform: translate(-50%, -50%);">

                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTituloCrear" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="HiddenField1" OnValueChanged="btnTickectEvento" runat="server" />
                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                        <contenttemplate>
                    <div class="vtabs">
                        <ul class="nav nav-tabs tabs-vertical" role="tablist">
                            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#homeCrear" role="tab" runat="server" id="A1"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspGenerales</span></a></li>                        
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentariosCrear" role="tab" runat="server" id="A3"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspComentarios</span></a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content" style="                                height: 550px;
                                width: 600px;">
                            <div class="tab-pane active" id="homeCrear" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel15">
                                    <ContentTemplate>
                                        <div class="row p-t-20">
                                            <div class="col-8">
                                                <label class="control-label">Título:</label>
                                                <asp:TextBox ID="TxTitulo_1" AutoPostBack="true" runat="server" class="form-control text-uppercase" ></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Tiempo (min):</label>
                                                <asp:TextBox ID="TxMinProductivo" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-4">
                                                <label class="control-label">Fecha Solicitud:</label>
                                                <asp:TextBox ID="TxFechaSolicitud" AutoPostBack="true" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Fecha Inicio:</label>
                                                <asp:TextBox ID="TxFechaInicio" AutoPostBack="true" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Fecha Entrega:</label>
                                                <asp:TextBox ID="TxFechaEntrega" AutoPostBack="true" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-12">
                                                <label class="control-label">Descripción:</label>
                                                <asp:TextBox ID="TxDescripcion" AutoPostBack="true" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-8">
                                                <label class="control-label">Responsable:</label>
                                                <asp:DropDownList runat="server" ID="DdlResponsable" CssClass="select2 form-control custom-select" AutoPostBack="true" Style="width: 100%" OnSelectedIndexChanged="DdlResponsable_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Prioridad:</label>
                                                <asp:DropDownList ID="DdlPrioridad" runat="server" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-12">
                                                <label class="control-label">Tipo Gestión:</label>
                                                <asp:DropDownList runat="server" ID="DdlTipoGestion" CssClass="select2 form-control custom-select" Style="width: 100%" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row p-t-20" runat="server" id="div2">
                                    <div class="col-12">
                                        <label class="control-label">Archivos Adjuntos:</label>
                                        <asp:FileUpload ID="FuAdjunto" AllowMultiple="true" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="comentariosCrear" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel16">
                                    <ContentTemplate>
                                        <div class="row p-t-20" runat="server" id="div3">
                                            <div class="col-2">
                                                <label class="control-label">Comentario:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:TextBox ID="TxComentario" TextMode="MultiLine" Rows="1" AutoPostBack="true" runat="server" class="form-control text-uppercase" OnTextChanged="TxComentario_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-2" style="text-align: center">
                                                <asp:LinkButton ID="BtnAddComentario" runat="server" Text="+" title="Añadir" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnAddComentario_Click"></asp:LinkButton>
                                            </div>

                                        </div>

                                        <div class="col-md-12" runat="server" id="divAlertaComentario" visible="false" style="text-align: center">
                                            <p>
                                                <b><code>
                                                    <label class="col-form-label" runat="server" id="LbAlertaComentario"></label>
                                                </code></b>
                                            </p>
                                        </div>

                                        <div class="row p-t-20" runat="server" id="divComentario" visible="false">
                                            <div class="col-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvComentario" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table "
                                                        RowStyle-CssClass="rows"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        AutoGenerateColumns="false"
                                                        AllowPaging="true" OnPageIndexChanging="GvComentario_PageIndexChanging"
                                                        GridLines="None" OnRowCommand="GvComentario_RowCommand"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="idComentario" Visible="false" ItemStyle-Width="1%" />
                                                            <asp:BoundField DataField="usuario" Visible="true" HeaderText="Usuario" ItemStyle-Width="30%" />
                                                            <asp:BoundField DataField="comentario" HeaderText="Comentario" Visible="true" />
                                                            <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center" ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="BtnEliminarComen" runat="server" title="Eliminar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idComentario")%>' CommandName="Eliminar">
                                                                        <i class="mdi mdi-delete" ></i>
                                                                    </asp:LinkButton>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <br />
                        </div>
                    </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <asp:UpdatePanel runat="server" ID="UpdatePanel21">
                    <ContentTemplate>
                        <div class="col-md-12" runat="server" id="divAlertaGeneral" visible="false" style="text-align: center">
                            <p>
                                <b><code>
                                    <label class="col-form-label" runat="server" id="LbAdvertencia"></label>
                                </code></b>
                            </p>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <contenttemplate>
        
                            <asp:Button ID="BtnCancelarTarjeta" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="BtnCancelarTarjeta_Click"  />
                            <asp:Button ID="BtnConfirmarTarea" runat="server" Text="Enviar"  OnClick="BtnConfirmarTarea_Click"   class="btn" Style="background-color: #00468c; color: #ffffff;"   />
                         </contenttemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>




        <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalTarjetaConfirmar" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <%--<div class="modal-content" style="width: 800px; height: 910px; top: 452px; left: 50%; transform: translate(-50%, -50%);">--%>
                 <div class="modal-content" style="width: 896px; height: 860px; top: 414px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpTituloConfirmar" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <h4 class="modal-title" >
                                <strong>
                                    <asp:Label ID="LbTituloConfirmar" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>

               <%--  style="padding-left: 40px; padding-right: 40px; padding-top: 20px; padding-bottom: 20px;"--%>
                <div class="modal-body" >
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <contenttemplate>

                            <div class="row" runat="server" id="DivEstados">
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#00468c;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbEnColaModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Cola</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#00468c;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbEjecucionModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Ejecución</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#00468c;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbDetenidasModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Detenidas</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color:#00468c;  color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbAtrasadoModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Atrasados</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>

                          <h6 style="color:#00468c"><b>-Cargabilidad Minutos Diarios:</b></h6>
                            <div class="table-responsive m-t-20">
                                <asp:GridView ID="GVDistribucion" runat="server" Visible="true"
                                    CssClass="table table-hover table-sm"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows" HeaderStyle-HorizontalAlign="center"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"  OnPageIndexChanging="GVDistribucion_PageIndexChanging"
                                    GridLines="None"
                                    PageSize="2">
                                    <Columns>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="center" />
                                        <asp:BoundField DataField="min" HeaderText="Mins Diarios" ItemStyle-HorizontalAlign="center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
            
                           <h6  style="color:#00468c"><b>-Datos Generales:</b></h6>
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Título:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxTituloModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row" runat="server" id="divPrioridad">
                                <div class="col-md-2">
                                    <label class="col-form-label">Prioridad:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxPrioridadModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Tiempo:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxTimeModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Inicio:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxInicioModal" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Entrega:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxEntregaModal" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row" runat="server" visible="false" id="divSolucion">
                                <div class="col-md-2">
                                    <label class="col-form-label">Solución:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxSolucion" TextMode="MultiLine" Rows="2"  AutoPostBack="true" runat="server" class="form-control" ReadOnly="false"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group row" runat="server" visible="false" id="divAdjuntoSolucion">

                                <div class="col-md-2">
                                    <label class="col-form-label">Adjunto:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:FileUpload ID="FuSolucion_Cerrar" AllowMultiple="false" runat="server" class="form-control" />
                                </div> 
                             </div>
                       
                            <div class="col-md-12" runat="server" style="text-align: center; color:#D9272E" visible="false" id="divComentariosAdjuntos">
                                <p><b><asp:Label ID="LbAdvertenciaModal" runat="server" Text="" ></asp:Label></b></p>
                            </div>

                            <div class="col-md-12" runat="server" style="text-align: center; color:seagreen" visible="false" id="divDiaNoHabil">
                                <p><b><asp:Label ID="LbDiaNoHabil" runat="server" Text="" ></asp:Label></b></p>
                            </div>

                            <div class="col-md-12" runat="server" style="text-align: center; color:seagreen" visible="false" id="divTareaFinalizada">
                                <p><b><asp:Label ID="LbTareaFinalizada" runat="server" Text="La tarea se encuentra en estado Finalizada, la fecha de entrega es menor a la fecha actual del sistema" ></asp:Label></b></p>
                            </div>
                        </contenttemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnEnviarInfo" runat="server" Text="Enviar" class="btn" style="background-color:#00468c;  color: #ffffff;"   OnClick="BtnEnviarInfo_Click"  />
                        </contenttemplate>

                        <%--                        <triggers>
                                <asp:PostBackTrigger ControlID="BtnConfirmarTarea" />
                        </triggers>--%>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>





</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">

    <script type="text/javascript">   
        var UpdatePanel1 = '<%=UpdatePanel1.ClientID%>';
        $(function () {
            $("#btnModal").click(function () {
                document.getElementById("<%=HiddenId.ClientID%>").value = $(this).data('titulo');
                __doPostBack('<%= HiddenId.ClientID %>', '');
            });
        });
    </script>

    <asp:Literal ID="LitEnCola" runat="server"></asp:Literal>
    <asp:Literal ID="LitEnEjecucion" runat="server"></asp:Literal>
    <asp:Literal ID="LitAtrasados" runat="server"></asp:Literal>
    <asp:Literal ID="LitCompletadosHoy" runat="server"></asp:Literal>
    <asp:Literal ID="LitDetenidas" runat="server"></asp:Literal>

</asp:Content>
