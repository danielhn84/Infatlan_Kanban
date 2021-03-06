﻿<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="miTablero.aspx.cs" Inherits="Infatlan_Kanban.pages.miTablero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>
    <script type="text/javascript">  
        function Calculation(e, value) {
            var grid = document.getElementById("<%= GvCheckList.ClientID %>");
            var rowData = e.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;

            if (value != '') {
                for (var rowId = 1; rowId < grid.rows.length; rowId++) {
                    if (rowIndex === rowId) {
                        var txtbx = grid.rows[rowId].cells[0].children[0];
                        txtbx.value = "Si";
                    }
                }
            } else {
                for (var rowId = 1; rowId < grid.rows.length; rowId++) {
                    if (rowIndex === rowId) {
                        var txtbx = grid.rows[rowId].cells[0].children[0];
                        txtbx.value = "No";
                    }
                }
            }         
        }
    </script>

 
     <%--   <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function (event) {
            console.log("DOM fully loaded and parsed");
            getloc();
        });
    </script>--%>

    <script type="text/javascript">
        function openDescargarModal() { $('#DescargaModal').modal('show'); }
        

        function closeModalCargaMasivaOp() { $('#CargaMasiva').modal('show'); }
        function openModalCargaMasivaOp() { $('#CargaMasiva').modal('show'); }


        function closeModal() { $('#VisualizarImagen').modal('show'); }
        function openModalImagen() { $('#VisualizarImagen').modal('show'); }

        function ModalTarjetaOpen() { $('#ModalTarjeta').modal('show'); }
        function ModalTarjetaClose() { $('#ModalTarjeta').modal('hide'); }

        function ModalTarjetaConfirmarOpen() { $('#ModalTarjetaConfirmar').modal('show'); }
        function ModalTarjetaConfirmarClose() { $('#ModalTarjetaConfirmar').modal('hide'); }

        function ModalTarjetaCrearOpen() { $('#ModalTarjetaCrear').modal('show'); }
        function ModalTarjetaCrearClose() { $('#ModalTarjetaCrear').modal('hide'); }

        function ModalTarjetaCerrarOpen() { $('#ModalConfirmarCerrar').modal('show'); }
        function ModalTarjetaCerrarClose() { $('#ModalConfirmarCerrar').modal('hide'); }


        function ModalTarjetaCrearOpeOpen() { $('#ModalTarjetaCrearOperativa').modal('show'); }
        function ModalTarjetaCrearOpeClose() { $('#ModalTarjetaCrearOperativa').modal('hide'); }

        function ModalTarjetaCrearOpeOpenConfirmar() { $('#ModalTarjetaConfirmarOperativa').modal('show'); }
        function ModalTarjetaCrearOpeCloseConfirmar() { $('#ModalTarjetaConfirmarOperativa').modal('hide'); }


    </script>
    <link href="../assets/node_modules/select2/dist/css/select2.css" rel="stylesheet" />
    <link href="dist/css/pages/tab-page.css" rel="stylesheet">
    <link href="../dist/css/pages/tab-page.css" rel="stylesheet" />

    <link href="../dist/css/pages/ribbon-page.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">



    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7; margin: 0;">
                <span style="display: inline-block; height: 100%; vertical-align: middle;"></span>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="/images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="display: inline-block; vertical-align: middle;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <div class="row page-titles">
        <div class="col-md-4 align-self-center">
             <img src="../images/TextoBlanco.png" height="20"/>
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                <li class="breadcrumb-item active">Mi Tablero</li>
            </ol>
        </div>

   <div class="col-md-8 " style="float: right">
            <div class="d-flex justify-content-end align-items-center">

                <asp:UpdatePanel runat="server" ID="UpdatePanel18" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:LinkButton ID="BtnAddOperativa" runat="server" title="Agregar" Text="Agregar" Style="background-color: white; color: #003e7f;" class="btn" OnClick="BtnAddOperativa_Click">
                                       <i class="mdi mdi-plus-box"></i> Tarjeta Operativa                 
                        </asp:LinkButton>
                        <asp:LinkButton ID="BtnAddTarjeta" runat="server" title="Agregar" Text="Agregar" Style="background-color: white; color: #003e7f;" class="btn" OnClick="BtnAddTarjeta_Click">
                                        <i class="mdi mdi-plus-box"></i> Tarjeta Técnica&nbsp&nbsp
                        </asp:LinkButton>

                        <asp:LinkButton ID="BtnBusqueda" runat="server" title="Buscar Tarjeta" Text="Búsqueda" Style="background-color: white; color: #003e7f;" class="btn" OnClick="BtnBusqueda_Click1">
                                        <i class="fa  fa-search-plus"></i>
                        </asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


    </div>

    <div class="row">
        <div class="col">
            <div class="card">
                <div class="card-body">
                    <div class="row col-12">
                        <h4 class="card-title">Tablero Kanban Board</h4>
                    </div>

                    <div class="row col-12">
                        <div class="col-2">
                            <h6 class="card-subtitle">Listado de tarjetas.</h6>
                        </div>

                        <div class=" col-10 text-right" style="zoom: 80%; text-align:left">                          
                            <label class="control-label">Prioridades</label>
                            
                            <span class="label label-danger">Máxima</span>
                            <span class="label label-primary">Alta</span>
                            <span class="label label-warning">Normal</span>
                            <span class="label label-info">Baja</span>
                        </div>
               


                    </div>

                         
                    <div class="card-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="card-header" role="tab" runat="server" id="DivBusquedaReportes" visible="false">
                                    <div class="row col-12 font-12" runat="server" id="rowDetalle">

                                        <div class="col-md-4" runat="server" id="DivBusqueda" visible="true">
                                            <asp:Label ID="Label11" runat="server" Text="Tipo de búsqueda:" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            <asp:DropDownList runat="server" ID="DdlTipoBusqueda" CssClass="select2 form-control custom-select" AutoPostBack="true" OnSelectedIndexChanged="DdlTipoBusqueda_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-4" runat="server" id="DivEquipoColaborador" visible="false">
                                            <asp:Label ID="LbTipoSelecciono" runat="server" Text="" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            <asp:DropDownList runat="server" Visible="false" ID="DdlEquipoTrabajo" CssClass="select2 form-control custom-select" AutoPostBack="true" OnSelectedIndexChanged="DdlEquipoTrabajo_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:DropDownList runat="server" Visible="false" ID="DdlColaborador" CssClass="select2 form-control custom-select" AutoPostBack="true" OnSelectedIndexChanged="DdlColaborador_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-4"  runat="server"  id="DivTipoTarjeta" visible="false">
                                            <asp:Label ID="Label12" runat="server" Text="Tipo Tarjeta:" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                            <asp:DropDownList ID="DdlTarjeta" runat="server" AutoPostBack="true" CssClass="select2 form-control custom-select" OnSelectedIndexChanged="DdlTarjeta_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Técnicas"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Operativas"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel runat="server" ID="UpdatePanel19" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row" style="zoom: 75%;">
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
                                            <div id="collapseEnCola" class="collapse show" role="tabpanel" aria-labelledby="EnCola"  style="background-color: whitesmoke">
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
                                            <div id="collapseEnEjecucion" class="collapse show" role="tabpanel" aria-labelledby="EnEjecucion" style="background-color: whitesmoke">
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
                                            <div id="collapseAtrasados" class="collapse show" role="tabpanel" aria-labelledby="Atrasados" style="background-color: whitesmoke">
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
                                            <div id="collapseDetenidas" class="collapse show" role="tabpanel" aria-labelledby="Detenidas" style="background-color: whitesmoke">
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
                                            <div id="collapseCompletados" class="collapse show" role="tabpanel" aria-labelledby="Completados" style="background-color: whitesmoke">
                                                <div class="card-body">
                                                    <asp:Literal Text="" ID="LitNotificacionesCompletadosHoy" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%--    MODAL INICIO--%>
    <div class="modal fade" id="ModalTarjeta" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 880px; height: 890px; top: 414px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpTitulo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTitulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UPFormulario" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="HiddenId" OnValueChanged="btnTickectEvento" runat="server" />            
                            <div class="vtabs">
                                <ul class="nav nav-tabs tabs-vertical" role="tablist">
                                    <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab" runat="server" id="tabGenerales"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspGenerales</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" runat="server" role="tab" id="tabAdjuntos" visible="true"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspAdjuntos</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentarios" role="tab" runat="server" id="tabComentarios"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspComentarios</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#historial" role="tab" runat="server" id="tabHistorial"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspHistorial</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#verificacion" role="tab" runat="server" id="tabVerificacion"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspCheck List</span></a></li>
                                    <%--   <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#solucion" role="tab" runat="server" id="tabSolucion"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspSolución</span></a></li>--%>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content" style="height: 550px; width: 600px;">
                                    <div class="tab-pane active" id="home" role="tabpanel" style="height: 450px; width: 630px;">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-8">
                                                        <label class="control-label">Título:</label>
                                                        <asp:TextBox ID="TxTitulo" runat="server" class="form-control text-uppercase" OnTextChanged="TxTitulo_TextChanged"></asp:TextBox>
                                                    </div>

                                                    <div class="col-4">
                                                        <label class="control-label">Tiempo (min):</label>
                                                        <asp:TextBox ID="TxMinProductivo_1"  runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <br />
                                                <div class="row">
                                                    <div class="col-4">
                                                        <label class="control-label">Fecha Solicitud:</label>
                                                        <asp:TextBox ID="TxFechaSolicitud_1" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-4">
                                                        <label class="control-label">Fecha Inicio:</label>
                                                        <asp:TextBox ID="TxFechaInicio_1" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-4">
                                                        <label class="control-label">Fecha Entrega:</label>
                                                        <asp:TextBox ID="TxFechaEntrega_1" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-8">
                                                        <label class="control-label">Responsable:</label>
                                                        <asp:DropDownList runat="server" ID="DdlResponsable_1" CssClass="select2 form-control custom-select"   Width="100%"  OnSelectedIndexChanged="DdlResponsable_1_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-4">
                                                        <label class="control-label">Prioridad:</label>
                                                        <asp:DropDownList ID="DdlPrioridad_1" runat="server" CssClass="form-control custom-select" Style="width: 100%">
                                                            <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <br />

                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <div class="col-2">
                                                                <label class="control-label">Descripción:</label>
                                                            </div>
                                                            <div class="col-10">
                                                                <asp:TextBox ID="TxDescripcion_1" TextMode="MultiLine" Rows="2"  runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <div class="col-2">
                                                                <label class="control-label">Gestión:</label>
                                                            </div>
                                                            <div class="col-10">
                                                                <asp:DropDownList runat="server" ID="DdlTipoGestion_1" CssClass="select2 form-control custom-select" Style="width: 100%"  Enabled="false"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" runat="server" id="divAccion">
                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <div class="col-2">
                                                                <label class="control-label">Acción:</label>
                                                            </div>
                                                            <div class="col-10">
                                                                <asp:DropDownList ID="DdlAccion" runat="server" CssClass="form-control custom-select"  Style="width: 100%" OnSelectedIndexChanged="DdlAccion_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="Seleccione opción..."></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Cerrar Tarjeta Kanban"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Solicitud Cambio Estado a Detenido"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="Solicitud Eliminar Tarjeta"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" runat="server" id="divMotivoEliminar" visible="false">
                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <div class="col-2">
                                                                <label class="control-label">Motivo:</label>
                                                            </div>
                                                            <div class="col-10">
                                                                <asp:DropDownList runat="server" ID="DdlMotivoEliminar" CssClass="form-control custom-select" Style="width: 100%"  Enabled="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <div class="col-2">
                                                                <label class="control-label">Detalle:</label>
                                                            </div>
                                                            <div class="col-10">
                                                                <asp:TextBox ID="TxDetalle"  TextMode="MultiLine" Rows="2" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" runat="server" id="divNuevasFechas" visible="false">
                                                    <div class="col-12">
                                                        <div class="form-group row">
                                                            <div class="col-2">
                                                                <label class="control-label">Fecha Inicio:</label>
                                                            </div>
                                                            <div class="col-4">
                                                                <asp:TextBox ID="TxNewFechaInicio"  runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-2">
                                                                <label class="control-label">Fecha Entrega:</label>
                                                            </div>
                                                            <div class="col-4">
                                                                <asp:TextBox ID="TxNewFechaEntrega" runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
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

                                    <div class="tab-pane" id="comentarios" role="tabpanel" style="height: 450px; width: 630px;">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>
                                                <div class="row p-t-20" runat="server" id="divComentarioAdd">
                                                    <div class="col-2">
                                                        <label class="control-label">Comentario:</label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TxComentario_1" TextMode="MultiLine" Rows="1"  runat="server" class="form-control text-uppercase" OnTextChanged="TxComentario_1_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2" style="text-align: center">
                                                        <asp:LinkButton ID="BtnAddComentario_1" runat="server" Text="+" title="Añadir" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnAddComentario_1_Click"></asp:LinkButton>
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
                                                                HeaderStyle-HorizontalAlign="center" OnRowCommand="GvAdjuntoLectura_RowCommand"
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

                                    <div class="tab-pane" id="verificacion" role="tabpanel" style="height: 450px; width: 630px;">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel41" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row p-t-20">
                                                    <div class="col-4">
                                                        <label class="control-label">¿Requiere Escalación? </label>
                                                    </div>
                                                    <div class="col-2">
                                                        <%--<asp:DropDownList ID="ddlEscalacion" runat="server" CssClass="form-control custom-select" Style="width: 100%" OnSelectedIndexChanged="ddlEscalacion_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No"></asp:ListItem>

                                                        </asp:DropDownList>--%>

                                                        <asp:RadioButtonList ID="ckEscalacion" RepeatDirection="Horizontal" Width="90px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ckEscalacion_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Si</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </div>

                                                    <div class="col-6">
                                                        <asp:DropDownList runat="server" ID="DdlEquipos" CssClass="select2 form-control custom-select" Style="width: 100%" AutoPostBack="true" Visible="false"></asp:DropDownList>
                                                    </div>
                                                    </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                        <div class="row p-t-20" runat="server" id="div11" visible="true">
                                            <div class="col-12">
                                                <!--Fin Fila 1-->
                                                <%-- <asp:UpdatePanel runat="server" ID="UpdatePanelLLeno">
                                                    <ContentTemplate>--%>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvCheckList" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        AllowPaging="true" OnRowDataBound="GvCheckList_RowDataBound"
                                                        GridLines="None"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtEvtTo" runat="server" Text="No" Width="2px" Height="2px" ReadOnly="false" Visible="true" BackColor="#00468c" BorderColor="#00468c"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="tipo" Visible="true" HeaderText="Pregunta" ItemStyle-Width="1%" />
                                                            <asp:BoundField DataField="pregunta" Visible="true" HeaderText="Respuesta" ItemStyle-Width="30%" />
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center" ItemStyle-Width="90%">
                                                                <ItemTemplate>
                                                                    <asp:FileUpload ID="FuRespuesta" runat="server" Visible="false" class="form-control" onchange="Calculation(this, this.value)" />
                                                                    <asp:FileUpload ID="FuRespuestaDoc" runat="server" Visible="false" class="form-control" onchange="Calculation(this, this.value)" />
                                                                    <asp:TextBox ID="TxRespuesta" TextMode="MultiLine" Width="100%" Rows="3" Visible="false" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                            </div>
                                        </div>
                                        <div class="row p-t-20" runat="server" id="div13" visible="false">
                                            <div class="col-12">
                                                <!--Fin Fila 1-->
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvCheckListLleno" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        AllowPaging="true" OnRowDataBound="GvCheckListLleno_RowDataBound"
                                                        GridLines="None" OnRowCommand="GvCheckListLleno_RowCommand"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="id" Visible="false" HeaderText="Usuario" ItemStyle-Width="35%" />
                                                            <asp:BoundField DataField="pregunta" HeaderText="Pregunta" Visible="true" ItemStyle-Width="55%" />
                                                            <asp:BoundField DataField="tipo" HeaderText="Respuesta" Visible="true" ItemStyle-Width="55%" />
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center" ItemStyle-Width="60%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TxRespuesta" TextMode="MultiLine" Width="100%" Rows="3" Visible="true" runat="server"></asp:TextBox>
                                                                    <asp:LinkButton ID="BtnImagenVer" runat="server" title="Visualizar" class="btn btn-warning mr-2" CommandArgument='<%#Eval("id")%>' CommandName="Visualizar">
                                                                        <i class="mdi mdi-image" ></i>
                                                                    </asp:LinkButton>

                                                                    <asp:LinkButton ID="BtnDescargar" runat="server" title="Descargar" class="btn btn-info mr-2" CommandArgument='<%#Eval("id")%>' CommandName="Descargar">
                                                                        <i class="mdi mdi-download" ></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField DataField="respuesta" Visible="true" ItemStyle-Width="55%" />


                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                    <ContentTemplate>
                        <div class="col-md-12" runat="server" id="divAlertaGuardar" visible="true" style="text-align: center">
                            <p>
                                <b><code>
                                    <label class="col-form-label" runat="server" id="LbAlertaGuardar"></label>
                                </code></b>
                            </p>                          
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                        <ContentTemplate>
                            <asp:Button ID="BtnCancelarTarea_1" runat="server" Text="Cancelar" OnClick="BtnCancelarTarea_1_Click" class="btn btn-secondary" />
                            <asp:Button ID="BtnConfirmarTarea_1" runat="server" Text="Enviar" OnClick="BtnConfirmarTarea_1_Click" class="btn" Style="background-color: #00468c; color: #ffffff;" Visible="true" />
                          <%--  <asp:Button ID="BtnValidar" runat="server" Text="Validar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnValidar_Click" />--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalTarjetaFinalizada" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none; zoom: 75%">
        <div class="modal-dialog" role="document">
            <%--            <div class="modal-content" style="width: 800px; height: 910px; top: 452px; left: 50%; transform: translate(-50%, -50%);">--%>
            <div class="modal-content" style="width: 880px; height: 860px; top: 414px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTituloTarjetaFinalizad" runat="server" Text="Tarjeta Finalizada" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="row" runat="server" id="Div1">
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #D9272E; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label2" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Cola</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #D9272E; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label3" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Ejecución</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #D9272E; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label4" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Detenidas</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #D9272E; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="Label5" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Atrasados</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h6 style="color: #D9272E"><b>-Cargabilidad Minutos Diarios:</b></h6>
                            <div class="table-responsive m-t-20">
                                <asp:GridView ID="GridView1" runat="server" Visible="true"
                                    CssClass="table table-bordered"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows" HeaderStyle-HorizontalAlign="center"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true" OnPageIndexChanging="GVDistribucion_PageIndexChanging"
                                    GridLines="None"
                                    PageSize="2">
                                    <Columns>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="center" />
                                        <asp:BoundField DataField="min" HeaderText="Mins Diarios" ItemStyle-HorizontalAlign="center" />
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h6 style="color: #D9272E"><b>-Datos Generales:</b></h6>
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
                                    <asp:TextBox ID="TxPrioridadFinalizada" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Tiempo Productivo:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxTimeProductivoFinalizada" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Fecha Inicio:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxFechaInicioFinalizada" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Fecha Entrega:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxFechaFinFinalizada" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" Style="text-align: center"></asp:TextBox>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="Button2" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" />
                        </ContentTemplate>

                        <%--                        <triggers>
                                <asp:PostBackTrigger ControlID="BtnConfirmarTarea" />
                        </triggers>--%>
                    </asp:UpdatePanel>
                </div>
                TxTitulo
            </div>
        </div>
    </div>

    <%--    MODAL CREAR--%>
    <div class="modal fade" id="ModalTarjetaCrear" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 875px; height: 795px; top: 386px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTituloCrear" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <asp:UpdatePanel ID="UpdatePanel38" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:LinkButton ID="BtnCancelarTecnica" runat="server" title="Cerrar" Style="background-color: white; color: #003e7f;" class="btn"  OnClick="BtnCancelarTecnica_Click">
                                        <i class="fa  fa-close"></i>
                            </asp:LinkButton>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <div class="vtabs">
                        <ul class="nav nav-tabs tabs-vertical" role="tablist">
                            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#homeCrear" role="tab" runat="server" id="A1"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspGenerales</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentariosCrear" role="tab" runat="server" id="A3"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspComentarios</span></a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content" style="height: 400px; width: 600px;">
                            <div class="tab-pane active" id="homeCrear" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel15">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-8">
                                                <label class="control-label">Título:</label>
                                                <asp:TextBox ID="TxTitulo_1"  runat="server" class="form-control text-uppercase"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Tiempo (min):</label>
                                                <asp:TextBox ID="TxMinProductivo"  runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-4">
                                                <label class="control-label">Fecha Solicitud:</label>
                                                <asp:TextBox ID="TxFechaSolicitud"  ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Fecha Inicio:</label>
                                                <asp:TextBox ID="TxFechaInicio"  runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Fecha Entrega:</label>
                                                <asp:TextBox ID="TxFechaEntrega"  runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-12">
                                                <label class="control-label">Descripción:</label>
                                                <asp:TextBox ID="TxDescripcion" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-8">
                                                <label class="control-label">Responsable:</label>

                                                <asp:DropDownList runat="server" ID="DdlResponsable" AutoPostBack="true" CssClass="select2 form-control custom-select font-12"  Style="width: 100%; zoom: 75%" OnSelectedIndexChanged="DdlResponsable_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Prioridad:</label>
                                                <asp:DropDownList ID="DdlPrioridad" runat="server"  CssClass="form-control custom-select" Style="width: 100%;">
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
                                                <asp:DropDownList runat="server" ID="DdlTipoGestion" CssClass="select2 form-control custom-select  font-12" Style="width: 100%" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row p-t-20" runat="server" id="div2">
                                    <div class="col-12">
                                        <label class="control-label">Archivos Adjuntos:</label>
                                        <asp:FileUpload ID="FuAdjunto" AllowMultiple="false" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="comentariosCrear" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel16">
                                    <ContentTemplate>
                                        <div class="row" runat="server" id="div3">
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
                                                        PageSize="8">
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
                        <ContentTemplate>
                            <asp:Button ID="BtnCancelarTarjeta" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="BtnCancelarTarjeta_Click" />
                            <asp:Button ID="BtnConfirmarTarea" runat="server" Text="Enviar" OnClick="BtnConfirmarTarea_Click" class="btn" Style="background-color: #00468c; color: #ffffff;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalTarjetaConfirmar" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <%--<div class="modal-content" style="width: 800px; height: 910px; top: 452px; left: 50%; transform: translate(-50%, -50%);">--%>
            <div class="modal-content" style="width: 895px; height: 900px; top: 414px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpTituloConfirmar" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTituloConfirmar" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>


                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row" runat="server" id="DivEstados">
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #00468c; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbEnColaModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Cola</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #00468c; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbEjecucionModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Ejecución</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #00468c; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbDetenidasModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Detenidas</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box text-center" style="background-color: #00468c; color: #ffffff;">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbAtrasadoModal" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Atrasados</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <h6 style="font-size: 15px;"><b>║Cargabilidad Minutos Diarios:</b></h6>
                            <div class="table-responsive">
                                <asp:GridView ID="GVDistribucion" runat="server" Visible="true"
                                    CssClass="table table-hover table-sm"
                                    PagerStyle-CssClass="pgr"
                                    HeaderStyle-CssClass="table"
                                    RowStyle-CssClass="rows" HeaderStyle-HorizontalAlign="center"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true" OnPageIndexChanging="GVDistribucion_PageIndexChanging"
                                    GridLines="None"
                                    PageSize="2">
                                    <Columns>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="center" />
                                        <asp:BoundField DataField="min" HeaderText="Mins Diarios" ItemStyle-HorizontalAlign="center" />
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <br />
                            <h6 style="font-size: 15px;"><b>║Datos Generales:</b></h6>
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
                                    <asp:TextBox ID="TxPrioridadModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Tiempo:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxTimeModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Inicio:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxInicioModal" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Entrega:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxEntregaModal" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row" runat="server" visible="false" id="divSolucion">
                                <div class="col-md-2">
                                    <label class="col-form-label">Solución:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxSolucion" TextMode="MultiLine" Rows="2" AutoPostBack="true" runat="server" class="form-control" ReadOnly="false"></asp:TextBox>
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

                            <div class="col-md-12" runat="server" style="text-align: center; color: #D9272E" visible="false" id="divComentariosAdjuntos">
                                <p>
                                    <code>
                                        <asp:Label ID="LbAdvertenciaModal" runat="server" Text=""></asp:Label></code>
                                </p>
                            </div>

                            <div class="col-md-12" runat="server" style="text-align: center; color: seagreen" visible="false" id="divDiaNoHabil">
                                <p>
                                    <code>
                                        <asp:Label ID="LbDiaNoHabil" runat="server" Text=""></asp:Label></code>
                                </p>
                            </div>

                            <div class="col-md-12" runat="server" style="text-align: center; color: seagreen" visible="false" id="divTareaFinalizada">
                                <p>
                                    <code>
                                        <asp:Label ID="LbTareaFinalizada" runat="server" Text="La tarea se encuentra en estado Finalizada, la fecha de entrega es menor a la fecha actual del sistema"></asp:Label></code>
                                </p>
                            </div>


                            <div class="col-md-12" runat="server" style="text-align: center; color: seagreen" visible="false" id="divCamposVacios">
                                <p>
                                    <code>
                                        <asp:Label ID="LbCamposVacios" runat="server"></asp:Label></code>
                                </p>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnEnviarInfo" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnEnviarInfo_Click" />
                        </ContentTemplate>

                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnEnviarInfo" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <%--    zoom:75%"--%>
    <%--MODAL DE CONFIRMACION CERRAR--%>
    <div class="modal fade" id="ModalConfirmarCerrar" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelConfirmar">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <b>
                                    <asp:Label Text="" runat="server" ID="LbTituloCerrar" CssClass="col-form-label"></asp:Label></b>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmar" runat="server" Text="Aceptar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnConfirmar_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnConfirmar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <%--    MODAL CREAR TARJETA OPERATIVA--%>
    <div class="modal fade" id="ModalTarjetaCrearOperativa" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 875px; height: 815px; top: 386px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTituloCrearOperativa" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
           
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel37" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <%--<asp:LinkButton ID="BtnCargaMasiva" runat="server" title="Carga Masiva" Text="Carga Masiva" Style="background-color: white; color: #003e7f;" class="btn" OnClick="BtnCargaMasiva_Click">
                                        <i class="fa  fa-upload"></i>
                            </asp:LinkButton>--%>

                            <asp:LinkButton ID="BtnCerrarOp" runat="server" title="Cerrar" Style="background-color: white; color: #003e7f;" class="btn" OnClick="BtnCerrarOp_Click">
                                        <i class="fa  fa-close"></i>
                            </asp:LinkButton>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <div class="vtabs">
                        <ul class="nav nav-tabs tabs-vertical" role="tablist">
                            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#homeCrearOperativa" role="tab" runat="server" id="A2"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspGenerales</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentariosCrearOperativa" role="tab" runat="server" id="A4"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspComentarios</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#NotificacionCrearOperativa" role="tab" runat="server" id="A5"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspNotificaciones</span></a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#LVCrearOperativa" role="tab" runat="server" id="A6"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspCheck List</span></a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content" style="height: 400px; width: 600px;">
                            <div class="tab-pane active" id="homeCrearOperativa" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel23">
                                    <ContentTemplate>
                                        <div class="row" runat="server">
                                            <div class="col-4" runat="server">
                                                <label class="control-label"></label>
                                                <div style="text-align: center;">
                                                    <asp:CheckBoxList ID="CkTiempoIlimitado" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green" AutoPostBack="true" OnSelectedIndexChanged="CkTiempoIlimitado_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text="&nbspTarjeta Tiempo Ilimitado"></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>

                                            <div class="col-4" runat="server">
                                                <label class="control-label">Fecha Solicitud:</label>
                                                <asp:TextBox ID="TxFechaSolicitudOperativa"  ReadOnly="true" runat="server" class="form-control text-center"></asp:TextBox>
                                            </div>

                                            <div class="col-4" runat="server">
                                                <label class="control-label">Fecha Fin Tarjeta:</label>
                                                <asp:TextBox ID="TxFechaFinTarjeta"  runat="server" TextMode="DateTimeLocal" class="form-control" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="TxNoVence" Text="Nunca Vence" BackColor="Green" ForeColor="White" AutoPostBack="true" runat="server" ReadOnly="true" class="form-control text-center"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row p-t-20">
                                            <div class="col-4">
                                                <label class="control-label">Frecuencia:</label>
                                                <asp:DropDownList ID="DdlFrecuencia" AutoPostBack="true" runat="server"  CssClass="form-control custom-select" Style="width: 100%;" OnSelectedIndexChanged="DdlFrecuencia_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Diaria"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Semanal"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Quincenal"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Mensual"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Trimestral"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="Anual"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-4">
                                                <label class="control-label">Fecha Inicio:</label>
                                                <asp:TextBox ID="TxFechaInicioOperativa"  runat="server" TextMode="DateTimeLocal" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Plazo finalizar tarea (días):</label>
                                                <asp:TextBox ID="TxDiasEntregaOperativa"  runat="server" TextMode="number" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20" style="zoom: 75%; text-align: center" runat="server" visible="false" id="DivFrecuenciaDiaria">
                                            <div class="col-1">
                                            </div>
                                            <div class="col-1">
                                            </div>

                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckDomingo" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="Dom"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckLunes" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspLun"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckMartes" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspMar"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckMiercoles" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspMie"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckJueves" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspJue"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckViernes" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspVie"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckSabado" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspSab"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>

                                         <div class="row p-t-20" style="zoom: 75%; text-align: center" runat="server" visible="false" id="DivMeses">
                                            

                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckEnero" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspEne"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckFebrero" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspFeb"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckMarzo" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspMar"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckAbril" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspAbr"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>

                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckMayo" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspMay"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="col-1">
                                                <asp:CheckBoxList ID="ckJunio" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                    <asp:ListItem Value="1" Text="&nbspJun"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                             <div class="col-1">
                                                 <asp:CheckBoxList ID="ckJulio" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                     <asp:ListItem Value="1" Text="&nbspJul"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </div>
                                            
                                             <div class="col-1">
                                                 <asp:CheckBoxList ID="ckAgosto" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                     <asp:ListItem Value="1" Text="&nbspAgo"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </div>
                                             <div class="col-1">
                                                 <asp:CheckBoxList ID="ckSeptiembre" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                     <asp:ListItem Value="1" Text="&nbspSept"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </div>
                                             <div class="col-1">
                                                 <asp:CheckBoxList ID="ckOctubre" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                     <asp:ListItem Value="1" Text="&nbspOct"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </div>
                                             <div class="col-1">
                                                 <asp:CheckBoxList ID="ckNoviembre" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                     <asp:ListItem Value="1" Text="&nbspNov"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </div>
                                             <div class="col-1">
                                                 <asp:CheckBoxList ID="ckDiciembre" runat="server" CssClass="check green" data-checkbox="icheckbox_flat-green">
                                                     <asp:ListItem Value="1" Text="&nbspDic"></asp:ListItem>
                                                 </asp:CheckBoxList>
                                             </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-8">
                                                <label class="control-label">Título:</label>
                                                <asp:TextBox ID="TxTituloOperativa" runat="server" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-4">
                                                <label class="control-label">Tiempo (min):</label>
                                                <asp:TextBox ID="TxMinProductivoOperativa"  runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row p-t-20">
                                            <div class="col-12">
                                                <label class="control-label">Descripción:</label>
                                                <asp:TextBox ID="TxDescripcionOperativa"  TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-6">
                                                <label class="control-label">Responsable:</label>
                                                <asp:DropDownList runat="server" ID="DdlResponsableOperativa" CssClass="select2 form-control custom-select " AutoPostBack="true"   Width="100%"  OnSelectedIndexChanged="DdlResponsableOperativa_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                            <div class="col-6">
                                                <label class="control-label">Suplente:</label>
                                                <asp:DropDownList runat="server" ID="DdlSuplente" CssClass="select2 form-control custom-select font-12"  Style="width: 100%;" ></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row p-t-20">
                                            <div class="col-6">
                                                 <label class="control-label">Tipo Gestión:</label>
                                                 <asp:DropDownList runat="server" ID="DdlGestionOperativa" CssClass="select2 form-control custom-select  font-12" Style="width: 100%"  Enabled="false"></asp:DropDownList>
                                            </div>

                                            <div class="col-6">
                                                <label class="control-label">Prioridad:</label>
                                                <asp:DropDownList ID="DdlPrioridadOperativa" runat="server" CssClass="form-control custom-select" Style="width: 100%;">
                                                    <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row p-t-20">
                                    <div class="col-2">
                                        <label class="col-form-label" runat="server" id="Label8">Adjunto:</label>
                                    </div>
                                    <div class="col-10">
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="comentariosCrearOperativa" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel24">
                                    <ContentTemplate>
                                        <div class="row p-t-20" runat="server" id="div9">
                                            <div class="col-2">
                                                <label class="control-label">Comentario:</label>
                                            </div>
                                            <div class="col-8">
                                                <asp:TextBox ID="TxComentarioOperativa" TextMode="MultiLine" Rows="1" AutoPostBack="true" runat="server" class="form-control text-uppercase"></asp:TextBox>
                                            </div>
                                            <div class="col-2" style="text-align: center">
                                                <asp:LinkButton ID="BtnAddComenOperativa" runat="server" Text="+" title="Añadir" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnAddComenOperativa_Click"></asp:LinkButton>
                                            </div>

                                        </div>

                                        <div class="col-md-12" runat="server" id="divComentarioOperativa" visible="true" style="text-align: center">
                                            <p>
                                                <b><code>
                                                    <label class="col-form-label" runat="server" id="divAlertaComentarioOperativa"></label>
                                                </code></b>
                                            </p>
                                        </div>

                                        <div class="col-md-12" runat="server" id="div12" visible="true">
                                            <div class="row col-12 mt-3">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvComentariosOperativas" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false" OnRowCommand="GvComentariosOperativas_RowCommand"
                                                        AllowPaging="true" OnPageIndexChanging="GvComentariosOperativas_PageIndexChanging"
                                                        GridLines="None"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="idComentario" Visible="false" HeaderText="Usuario" ItemStyle-Width="35%" />
                                                            <asp:BoundField DataField="usuario" Visible="true" HeaderText="Usuario" ItemStyle-Width="35%" />
                                                            <asp:BoundField DataField="comentario" HeaderText="Comentario" Visible="true" ItemStyle-Width="95%" />

                                                            <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center" ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="BtnEliminarComentario" runat="server" title="Eliminar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idComentario")%>' CommandName="Eliminar">
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

                            <div class="tab-pane" id="NotificacionCrearOperativa" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel30" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row p-t-20" runat="server" id="div6">
                                            <div class="col-3">
                                                <label class="control-label">Personal a Notificar:</label>
                                            </div>
                                            <div class="col-7">
                                                <asp:DropDownList runat="server" ID="DdlNotificar" CssClass="select2 form-control custom-select font-12" AutoPostBack="true" Style="width: 100%;"></asp:DropDownList>
                                            </div>
                                            <div class="col-2" style="text-align: center">
                                                <asp:LinkButton ID="BtnPesonalAdd" runat="server" Text="+" title="Añadir" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnPesonalAdd_Click"></asp:LinkButton>
                                            </div>
                                        </div>


                                        <div class="col-md-12" runat="server" id="divAlertaNotificacion" visible="false" style="text-align: center">
                                            <p>
                                                <b><code>
                                                    <label class="col-form-label" runat="server" id="LbNotificacionAlerta"></label>
                                                </code></b>
                                            </p>
                                        </div>


                                        <div class="col-md-12" runat="server" id="div10" visible="true">
                                            <div class="row col-12 mt-3">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvNotificacion" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false" OnRowCommand="GvNotificacion_RowCommand"
                                                        AllowPaging="true" OnPageIndexChanging="GvNotificacion_PageIndexChanging"
                                                        GridLines="None"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="idNotificacion" Visible="false" HeaderText="Id" ItemStyle-Width="5%" />
                                                            <asp:BoundField DataField="usuario" Visible="true" HeaderText="Usuario" ItemStyle-Width="15%" />
                                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" Visible="true" ItemStyle-Width="95%" ItemStyle-HorizontalAlign="center" />

                                                            <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center" ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="BtnEliminarNoti" runat="server" title="Eliminar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idNotificacion")%>' CommandName="Eliminar">
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

                            <div class="tab-pane" id="LVCrearOperativa" role="tabpanel" style="height: 450px; width: 630px;">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel31" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <%--                                 <div class="collapse m-t-15" id="pgr2">
                                            <pre class="line-numbers language-javascript"><code>&lt;div class="progress"&gt;<br/>&lt;div class="progress-bar bg-success" role="progressbar" style="width: 75%;height:15px;" role="progressbar""&gt; 75% &lt;/div&gt;<br/>&lt;/div&gt;</code></pre>
                                        </div>
                                        <div class="progress m-t-20">
                                            <div class="progress-bar bg-success" style="width: 0%; height: 15px;" role="progressbar">0%</div>
                                        </div>--%>

                                        <div class="row p-t-20" runat="server" id="div4">
                                            <div class="col-2">
                                                <label class="control-label">Validación:</label>
                                            </div>
                                            <div class="col-6">
                                                <asp:TextBox ID="TxOpcion" TextMode="MultiLine" Rows="1" AutoPostBack="true" runat="server" class="form-control text-uppercase"></asp:TextBox>
                                            </div>
                                            <div class="col-3">
                                                <asp:DropDownList ID="DdlTipoOpcion" runat="server" AutoPostBack="true" CssClass="form-control custom-select" Style="width: 100%;" OnSelectedIndexChanged="DdlFrecuencia_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Texto"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Imagen"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Documento"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-1" style="text-align: center">
                                                <asp:LinkButton ID="BtnOpcion" runat="server" Text="+" title="Añadir" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnOpcion_Click"></asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="col-md-12" runat="server" id="divAlertaOpcion" visible="false" style="text-align: center">
                                            <p>
                                                <b><code>
                                                    <label class="col-form-label" runat="server" id="LbOpcionAlerta"></label>
                                                </code></b>
                                            </p>
                                        </div>

                                        <div class="col-md-12" runat="server" id="div7" visible="true">
                                            <div class="row col-12 mt-3">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GvOpcionesLista" runat="server"
                                                        CssClass="table table-hover table-sm"
                                                        PagerStyle-CssClass="pgr"
                                                        HeaderStyle-CssClass="table"
                                                        HeaderStyle-HorizontalAlign="center"
                                                        RowStyle-CssClass="rows"
                                                        AutoGenerateColumns="false"
                                                        AllowPaging="true" OnRowCommand="GvOpcionesLista_RowCommand"
                                                        GridLines="None" OnPageIndexChanging="GvOpcionesLista_PageIndexChanging"
                                                        PageSize="10">
                                                        <Columns>
                                                            <asp:BoundField DataField="idOpcion" Visible="false" HeaderText="Id" ItemStyle-Width="3%" />
                                                            <asp:BoundField DataField="pregunta" HeaderText="Validación" Visible="true" ItemStyle-Width="95%" />
                                                            <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="true" ItemStyle-Width="95%" />
                                                            <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center" ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="BtnEliminarComen" runat="server" title="Eliminar" class="btn btn-primary mr-2" CommandArgument='<%#Eval("idOpcion")%>' CommandName="Eliminar">
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
                </div>

                <asp:UpdatePanel runat="server" ID="UpdatePanel25">
                    <ContentTemplate>
                        <div class="col-md-12" runat="server" id="divAlertaGeneralOperativa" visible="false" style="text-align: center">
                            <p>
                                <b><code>
                                    <label class="col-form-label" runat="server" id="LbAdvertenciaOperativa"></label>
                                </code></b>
                            </p>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnCancelarOperativa" runat="server" Text="Cancelar" class="btn btn-secondary" OnClick="BtnCancelarOperativa_Click" />
                            <asp:Button ID="BtnEnviarOperativa" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnEnviarOperativa_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <%--MODAL DE CONFIRMACION TARJETA OPERATIVA--%>
    <div class="modal fade" id="ModalTarjetaConfirmarOperativa" data-backdrop="static" data-keyboard="false" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 875px; height: 900px; top: 414px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTituloConfirmarOpe" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>


                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%-- <br />--%>
                            <h6 style="font-size: 15px;"><b>║Datos Generales:</b></h6>
                            <br />
                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Título:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxTituloConfirmarOpe" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row" runat="server" id="div5">
                                <div class="col-md-2">
                                    <label class="col-form-label">Prioridad:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxPrioridadConfirmarOpe" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Tiempo:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxTiempoConfirmarOpe" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Inicio Tarjeta:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxInicioConfirmarOpe" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Plazo finalizar tarea (días):</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxEntregaConfirmarOpe" AutoPostBack="true" runat="server" class="form-control" TextMode="Number" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel29" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="btnClose" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnEnviarOperativaConfirmar" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnEnviarOperativaConfirmar_Click" />
                        </ContentTemplate>

                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnEnviarInfo" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>



    <%--    MODAL  VISUALIZAR LA IMAGEN--%>
    <div class="modal fade" id="VisualizarImagen" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelUsuario">Visualización de la Imagen</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel32" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <img id="ImgPrevia" height="350" width="420" src="../images/vistaPrevia.jpg" style="border-width: 0px;" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateUsuarioBotones" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" class="btn btn-secondary"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


        <%--    MODAL DESCARGAR ARCHIVO--%>
    <div class="modal fade" id="DescargaModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">

                    <asp:UpdatePanel ID="UpdatePanel33" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelDescarga">Descargar Imagen/Documento
                                <asp:Label ID="LbIdDoc" runat="server" Text=""></asp:Label>
                            </h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="form-group row">
                                    <label class="col-sm-12 col-form-label">
                                        <h4>Privacidad de documentos</h4>
                                    </label>
                                    <div class="col-sm-12" style="text-align: justify">
                                        Este documento adjunto es confidencial, especialmente en lo que hace referencia a los datos personales que puedan contener y se dirigen exclusivamente al destinatario referenciado. Si usted no lo es y ha descargado este archivo o tiene conocimiento del mismo por cualquier motivo, le rogamos nos lo comunique por este medio y proceda a borrarlo, y que, en todo caso, se abstenga de utilizar, reproducir, alterar, archivar o comunicar a terceros el documento adjunto.
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel35" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group row">
                                <asp:Label ID="Label9" runat="server" Text="" Class="col-sm-12" Style="color: indianred; text-align: center;"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="BtnDescargarArchivo" runat="server" Text="Descargar" class="btn btn-success" OnClick="BtnDescargarArchivo_Click"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnDescargarArchivo" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--    MODAL CARGA MASIVA OPERATIVA--%>
    <div class="modal fade" id="CargaMasiva" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 600px; top: 320px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <h4 class="modal-title">Carga Masiva Operativa</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="col-md-12" runat="server">
                        <div class="form-group row">
                            <label class="col-4 col-form-label">Descargue la plantilla:</label>
                            <div class="col-md-8">
                               <a href="../plantilla/plantillaCargaMasiva.xlsx">Plantilla</a>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12" runat="server">
                        <div class="form-group row">
                            <div class="col-4">
                                <label class="control-label">Cargar Plantilla:</label>
                            </div>
                            <div class="col-8">
                                <asp:FileUpload ID="FuCargaOP" AllowMultiple="false" ClientIDMode="AutoID" runat="server" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel39" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group row">
                                <asp:Label ID="LbError" runat="server" Text="" Class="col-sm-12" Style="color: indianred; text-align: center;"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="BtnCargar" runat="server" Text="Cargar" class="btn btn-success" OnClick="BtnCargar_Click" />
                        </ContentTemplate>

                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnCargar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">


    <%--    <script type="text/javascript">   
        var UpdatePanel1 = '<%=UpdatePanel1.ClientID%>';
        $(function () {
            $("#btnModal").click(function () {
                document.getElementById("<%=TextBox1.ClientID%>").value = $(this).data('titulo');
                __doPostBack('<%= TextBox1.ClientID %>', '');
            });
        });
    </script>--%>

    <%--        <script type="text/javascript">   
            var UpdatePanel1 = '<%=UpdatePanel1.ClientID%>';
            $(function () {
                $("#btnModalBusqueda").click(function () {
                    document.getElementById("<%=HiddenId.ClientID%>").value = $(this).data('titulo');
                __doPostBack('<%= HiddenId.ClientID %>', '');
            });
        });
    </script>--%>


    <asp:Literal ID="LitEnCola" runat="server"></asp:Literal>
    <asp:Literal ID="LitEnEjecucion" runat="server"></asp:Literal>
    <asp:Literal ID="LitAtrasados" runat="server"></asp:Literal>
    <asp:Literal ID="LitCompletadosHoy" runat="server"></asp:Literal>
    <asp:Literal ID="LitDetenidas" runat="server"></asp:Literal>
<%--        <script type="text/javascript">  
            function Calculation() {
                var grid = document.getElementById("<%= GvCheckList.ClientID %>");
                for (var i = 0; i < grid.rows.length - 1; i++) {
                    var txtAmountReceive = $("input[id*=TxValidacion]")
                    if (txtAmountReceive[i].value != '') {
                        alert(txtAmountReceive[i].value);
                    }
                }
            }
    </script> --%>

    <%--COMBO BUSCADOR--%>

    <script src="../assets/node_modules/select2/dist/js/select2.js"></script>
    <link href="../assets/node_modules/select2/dist/css/select2.css" rel="stylesheet" />


    <style>
        /*.select2-selection__rendered {line-height: 31px !important;}
        .select2-container .select2-selection--single {height: 35px !important;}
        .select2-selection__arrow {height: 34px !important;}*/
    </style>
    <script>
        $(function () {
            $(".select2").select2();
            $(".ajax").select2({
                ajax: {
                    url: "https://api.github.com/search/repositories",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        return {
                            q: params.term, // search term
                            page: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        return {
                            results: data.items,
                            pagination: {
                                more: (params.page * 30) < data.total_count
                            }
                        };
                    },
                    cache: true
                },
                escapeMarkup: function (markup) {
                    return markup;
                },
                minimumInputLength: 1,
            });
        });
    </script>

</asp:Content>
