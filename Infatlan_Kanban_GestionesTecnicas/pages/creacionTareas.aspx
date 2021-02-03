<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="creacionTareas.aspx.cs" Inherits="Infatlan_Kanban_GestionesTecnicas.pages.creacionTareas" %>
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
        function abrirModalConfirmacionMP() { $('#ModalConfirmarMaximaPrioridad').modal('show'); }
        function cerrarModalConfirmacionMP() { $('#ModalConfirmarMaximaPrioridad').modal('hide'); }

        function openEnviarSolicitudModal() { $('#EnviarSolicitudModal').modal('show'); }
        function closeEnviarSolicitudModal() { $('#EnviarSolicitudModal').modal('hide'); }

        function ModalConfirmarOpen() { $('#ModalConfirmarAdjunto').modal('show'); }
        function ModalConfirmarClose() { $('#ModalConfirmarAdjunto').modal('hide'); }
    </script>
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
        <div class="col-md-5 align-self-center">
            <h4 class="card-title"><strong>Kanban Board Gestiones Técnicas </strong></h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Gestiones Técnicas</a></li>
                    <li class="breadcrumb-item active">Creación Tarjeta</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body">
                    <nav>
                        <div class="nav nav-pills " id="nav-tab" role="tablist">
                            <a class="nav-item nav-link active" id="nav-crearTarjeta-tab" data-toggle="tab" href="#nav-crear" role="tab" aria-controls="nav-crear" aria-selected="true"><i class="mdi mdi-plus"></i> Tarjeta</a>
                            <a class="nav-item nav-link" runat="server" visible="true" id="nav_tarjetaModificar_tab" data-toggle="tab" href="#nav-modificarTarjeta" role="tab" aria-controls="nav-modificarTarjeta" aria-selected="false"><i class="mdi  mdi-pencil"></i> Reasignar Tarjeta</a>
                            <a class="nav-item nav-link" runat="server" visible="true" id="nav_tarjetasCerradas_tab" data-toggle="tab" href="#nav-solicitudesCerradas" role="tab" aria-controls="nav-solicitudesCerradas" aria-selected="false"><i class="mdi mdi-book-open"></i> Mis Tarjetas Cerradas</a>
                        </div>
                    </nav>
                    <br />
                    <br />
                    <div class="tab-content tabcontent-border">
                        <div class="tab-pane fade show active" id="nav-crear" role="tabpanel">
                            <h4 class="card-title">
                                <label runat="server" id="LbTituloTarjeta"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de la tarea a realizar, <code>(visualizar tarjetas asignadas en la sección: Mi Tablero)</code>.</h6>
                            <br />

                            <div class="row">
                                <div class="col-md-12">                               
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab" runat="server" id="tabGenerales"><span class="hidden-sm-up"><i class="fa fa-list"></i></span><span class="hidden-xs-down">&nbspDatos Generales</span></a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" runat="server" role="tab" id="tabAdjuntos"><span class="hidden-sm-up"><i class="fa fa-paperclip"></i></span><span class="hidden-xs-down">&nbspAdjuntos</span></a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentarios" role="tab" runat="server" id="tabComentarios"><span class="hidden-sm-up"><i class="fa fa-comment"></i></span><span class="hidden-xs-down">&nbspComentarios</span></a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#historial" role="tab" runat="server" id="tabHistorial" visible="false"><span class="hidden-sm-up"><i class="fa fa-address-book"></i></span><span class="hidden-xs-down">&nbspHistorial</span></a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#solucion" role="tab" runat="server" id="tabSolucion" visible="false"><span class="hidden-sm-up"><i class="fa fa-edit"></i></span><span class="hidden-xs-down">&nbspSolución</span></a></li>
                                        <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#reasignacion" role="tab" runat="server" id="tabReasignacion" visible="false"><span class="hidden-sm-up"><i class="fa fa-pencil"></i></span><span class="hidden-xs-down">&nbspReasignación</span></a></li>
                                    </ul>

                                    <div class="tab-content tabcontent-border" style="height: 530px">
                                        <!--PRIMER CONTENIDO-->
                                        <div class="tab-pane active p-20" id="home" role="tabpanel">
                                            <div class="col-lg-12">
                                                <asp:UpdatePanel runat="server" ID="UPFormulario">
                                                    <ContentTemplate>
                                                        <!--Inicio Fila 1-->
                                                        <div class="row p-t-20">
                                                            <div class="col-6">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Título:</label>
                                                                <asp:TextBox ID="TxTitulo" AutoPostBack="true" runat="server" class="form-control text-uppercase"></asp:TextBox>
                                                            </div>

                                                            <div class="col-6">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Fecha Solicitud:</label>
                                                                <asp:TextBox ID="TxFechaSolicitud" AutoPostBack="true" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <!--Fin Fila 1-->

                                                        <div class="row p-t-20">
                                                            <div class="col-12">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Descripción:</label>
                                                                <asp:TextBox ID="TxDescripcion" AutoPostBack="true" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="row p-t-20">
                                                            <div class="col-6">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Responsable:</label>
                                                                <asp:DropDownList runat="server" ID="DdlResponsable" CssClass="select2 form-control custom-select" OnSelectedIndexChanged="DdlResponsable_SelectedIndexChanged" AutoPostBack="true" Style="width: 100%"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-6">

                                                                <label class="control-label   text-danger">*</label><label class="control-label">Tiempo Productivo(min):</label>
                                                                <asp:TextBox ID="TxMinProductivo" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="row p-t-20">
                                                            <div class="col-6">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Fecha Inicio:</label>
                                                                <asp:TextBox ID="TxFechaInicio" AutoPostBack="true" runat="server" TextMode="DateTimeLocal" class="form-control" OnTextChanged="TxFechaEntrega_TextChanged"></asp:TextBox>
                                                            </div>

                                                            <div class="col-6">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Fecha Entrega:</label>
                                                                <asp:TextBox ID="TxFechaEntrega" AutoPostBack="true" runat="server" TextMode="DateTimeLocal" class="form-control" OnTextChanged="TxFechaEntrega_TextChanged"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="row p-t-20">
                                                            <div class="col-6">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Prioridad:</label>
                                                                <asp:DropDownList ID="DdlPrioridad" runat="server" AutoPostBack="true" CssClass="form-control">
                                                                    <asp:ListItem Value="0" Text="Seleccione una opción"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                                    <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-6">
                                                                <label class="control-label   text-danger">*</label><label class="control-label">Tipo Gestión:</label>
                                                                <asp:DropDownList runat="server" ID="DdlTipoGestion" CssClass="select2 form-control custom-select" Style="width: 100%" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <!--SEGUNDO CONTENIDO-->
                                        <div class="tab-pane p-20" id="adjunto" role="tabpanel">
                                            <div class="row p-t-20">
                                                <div class="col-lg-12">

                                                    <!--Inicio Fila 1-->
                                                    <div class="row col-12">
                                                        <div class="col-1">
                                                            <label class="col-form-label" runat="server" id="lbNombre">Archivo:</label>
                                                        </div>
                                                        <div class="col-7">
                                                            <asp:FileUpload ID="FuAdjunto" runat="server" class="form-control"  />
                                                        </div>


                                                        <div class="col-4">
                                                            <asp:Button ID="BtnAddAdjunto" runat="server" Text="+" class="btn btn-info" OnClick="BtnAddAdjunto_Click" />
                                                        </div>
                                                    </div>

                                                    <%--    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="conditional">
                                                        <ContentTemplate>--%>
                                                        <%--</ContentTemplate>--%>
                                                        <%--<Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnAddAdjunto" />
                                                        </Triggers>--%>
                                                  <%--  </asp:UpdatePanel>--%>
                                                    <!--Fin Fila 1-->


                                                                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                        <ContentTemplate>

                                                    <div class="col-md-12" runat="server" id="divAdjunto" visible="false">
                                                        <div class="row col-12 mt-3">
                                                            <div class="table table-bordered">
                                                                <asp:GridView ID="GvAdjunto" runat="server"
                                                                    CssClass="mydatagrid"
                                                                    PagerStyle-CssClass="pgr"
                                                                    HeaderStyle-CssClass="header"
                                                                    HeaderStyle-HorizontalAlign="center"
                                                                    RowStyle-CssClass="rows"
                                                                    AutoGenerateColumns="false"
                                                                    AllowPaging="true" OnRowCommand="GvAdjunto_RowCommand"
                                                                    GridLines="None"
                                                                    PageSize="3">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="idAdjunto" Visible="false" ItemStyle-Width="27%" />
                                                                        <asp:BoundField DataField="nombre" HeaderText="Archivo" Visible="true" ItemStyle-Width="95%" />
                                                                        <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="BtnEliminar" runat="server" title="Eliminar" Style="background-color: #d9534f" class="btn" CommandArgument='<%# Eval("idAdjunto")%>' CommandName="Eliminar">
                                                                <i class="mdi mdi-delete text-white"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-12" runat="server" id="divAlertaNoAdjunto" visible="false" style="text-align: center">
                                                        <p><b><code>Tarea no cuenta con archivos adjuntos</code></b></p>
                                                    </div>


                                                    <div class="col-md-12" runat="server" id="divAdjuntoLectura" visible="false">
                                                        <div class="row col-12 mt-3">
                                                            <div class="table table-bordered">
                                                                <asp:GridView ID="GvAdjuntoLectura" runat="server"
                                                                    CssClass="mydatagrid"
                                                                    PagerStyle-CssClass="pgr"
                                                                    HeaderStyle-CssClass="header"
                                                                    RowStyle-CssClass="rows"
                                                                    AutoGenerateColumns="false"
                                                                    HeaderStyle-HorizontalAlign="center"
                                                                    AllowPaging="true"
                                                                    GridLines="None"
                                                                    PageSize="3">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="idRows" Visible="false" ItemStyle-Width="27%" />
                                                                        <asp:BoundField DataField="nombre" HeaderText="Archivo" Visible="true" ItemStyle-Width="95%" />
                                                                        <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="BtnDescargar" runat="server" title="Descargar" Style="background-color: #04A8EB" class="btn" CommandArgument='<%# Eval("idRows") %>' CommandName="Descargar">
                                                                <i class="mdi mdi-download text-white"></i>
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
                                            </div>
                                        </div>

                                        <!--TERCER CONTENIDO-->
                                        <div class="tab-pane p-20" id="comentarios" role="tabpanel">
                                            <%--<div class="tab-pane  p-20" id="comentarios" role="tabpanel">--%>
                                            <div class="row p-t-20">
                                                <div class="col-lg-12">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                        <ContentTemplate>

                                                            <div class="col-md-12" runat="server" id="divAlertaComentario" visible="false" style="text-align: center">
                                                                <p><b><code>Tarea no cuenta con comentarios, pero puede adicionar comentarios si desea.</code></b></p>
                                                            </div>


                                                            <!--Inicio Fila 1-->
                                                            <div class="row col-12" runat="server" id="divComentariosTexbox">
                                                                <div class="col-1">
                                                                    <label class="col-form-label" runat="server" id="lbComentario">Comentario:</label>
                                                                </div>
                                                                <div class="col-7">
                                                                    <asp:TextBox ID="TxComentario" TextMode="MultiLine" Rows="1" AutoPostBack="true" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-4">
                                                                    <asp:Button ID="BtnAddComentario" runat="server" Text="+" class="btn btn-info" OnClick="BtnAddComentario_Click" />
                                                                </div>
                                                            </div>

                                                            <!--Fin Fila 1-->
                                                            <div class="col-md-12" runat="server" id="divComentario" visible="false">
                                                                <div class="row col-12 mt-3">
                                                                    <div class="table table-bordered">
                                                                        <asp:GridView ID="GvComentario" runat="server"
                                                                            CssClass="mydatagrid"
                                                                            PagerStyle-CssClass="pgr"
                                                                            HeaderStyle-CssClass="header"
                                                                            RowStyle-CssClass="rows"
                                                                            HeaderStyle-HorizontalAlign="center"
                                                                            AutoGenerateColumns="false"
                                                                            AllowPaging="true"
                                                                            GridLines="None"
                                                                            PageSize="3">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="idComentario" Visible="false" ItemStyle-Width="27%" />
                                                                                <asp:BoundField DataField="usuario" Visible="true" HeaderText="Usuario" ItemStyle-Width="27%" />
                                                                                <asp:BoundField DataField="comentario" HeaderText="Comentario" Visible="true" ItemStyle-Width="95%" />
                                                                                <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="BtnEliminarComen" runat="server" title="Eliminar" Style="background-color: #d9534f" class="btn" CommandArgument='<%#Eval("idComentario")%>' CommandName="Eliminar">
                                                                <i class="mdi mdi-delete text-white"></i>
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>





                                                            <div class="col-md-12" runat="server" id="divComentarioLectura" visible="false">
                                                                <div class="row col-12 mt-3">
                                                                    <div class="table table-bordered">
                                                                        <asp:GridView ID="GvComentarioLectura" runat="server"
                                                                            CssClass="mydatagrid"
                                                                            PagerStyle-CssClass="pgr"
                                                                            HeaderStyle-CssClass="header"
                                                                            HeaderStyle-HorizontalAlign="center"
                                                                            RowStyle-CssClass="rows"
                                                                            AutoGenerateColumns="false"
                                                                            AllowPaging="true"
                                                                            GridLines="None"
                                                                            PageSize="3">
                                                                            <Columns>

                                                                                <asp:BoundField DataField="usuarioComentario" Visible="true" HeaderText="Usuario" />
                                                                                <asp:BoundField DataField="comentario" HeaderText="Comentario" Visible="true" ItemStyle-Width="95%" />

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>

                                        <!--CUARTO CONTENIDO-->
                                        <div class="tab-pane p-20" id="historial" role="tabpanel">
                                            <%--  <div class="tab-pane  p-20" id="historial" role="tabpanel">--%>
                                            <div class="row p-t-20">
                                                <div class="col-lg-12">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                        <ContentTemplate>

                                                            <!--Fin Fila 1-->
                                                            <div class="col-md-12" runat="server" id="divHistorial" visible="true">
                                                                <div class="row col-12 mt-3">
                                                                    <div class="table table-bordered">
                                                                        <asp:GridView ID="GvHistorial" runat="server"
                                                                            CssClass="mydatagrid"
                                                                            PagerStyle-CssClass="pgr"
                                                                            HeaderStyle-CssClass="header"
                                                                            RowStyle-CssClass="rows"
                                                                            AutoGenerateColumns="false"
                                                                            HeaderStyle-HorizontalAlign="center"
                                                                            AllowPaging="true"
                                                                            GridLines="None"
                                                                            PageSize="3">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="usuarioCambio" Visible="true" HeaderText="Usuario" ItemStyle-Width="30%" />
                                                                                <asp:BoundField DataField="cambio" Visible="true" HeaderText="Cambio" ItemStyle-Width="95%" />
                                                                                <asp:BoundField DataField="fechaCambio" HeaderText="Fecha" Visible="true" ItemStyle-Width="20%" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>

                                        <!--QUINTO CONTENIDO-->
                                        <div class="tab-pane p-20" id="solucion" role="tabpanel">
                                            <%--  <div role="tabpanel" id="solucion" class="tab-pane  p-20" >--%>

                                            <div class="col-lg-12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <!--Inicio Fila 1-->
                                                        <div class="row p-t-20" runat="server" id="divAccion">
                                                            <div class="col-12">
                                                                <div class="form-group row">
                                                                    <div class="col-1">
                                                                        <label class="control-label text-danger">*</label><label class="control-label">Acción:</label>
                                                                    </div>
                                                                    <div class="col-11">
                                                                        <asp:DropDownList ID="DdlAccion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlAccion_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Seleccione opción..."></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Solicitud de Re-asignación de Tarjeta"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Cerrar Tarjeta Kanban"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row p-t-20">
                                                            <div class="col-12">
                                                                <div class="form-group row">
                                                                    <div class="col-1">
                                                                        <label class="control-label   text-danger">*</label><label class="control-label">Detalle:</label>
                                                                    </div>
                                                                    <div class="col-11">
                                                                        <asp:TextBox ID="TxDetalle" AutoPostBack="true" TextMode="MultiLine" Rows="6" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="conditional">
                                                            <ContentTemplate>
                                                                <div class="col-12" runat="server" id="divAdjuntoSolucion" visible="false">
                                                                    <div class="form-group row">

                                                                        <!--Inicio Fila 1-->
                                                                        <div class="row col-12">
                                                                            <div class="col-1">
                                                                                <label class="col-form-label" runat="server" id="Label2">Archivo:</label>
                                                                            </div>
                                                                            <div class="col-7">
                                                                                <asp:FileUpload ID="FuSolucion" runat="server" class="form-control" />
                                                                            </div>
                                                                            <div class="col-4">
                                                                                <asp:Button ID="Button1" runat="server" Text="+" class="btn btn-info" OnClick="Button1_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>

                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="Button1" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>




                                                        <%--                                                           <div class="col-12" runat="server" id="divAdjuntoSolucion" visible="false">
                                                            <div class="form-group row">
                                                                <div class="col-1">
                                                                    <label class="col-form-label" runat="server" id="Label1">Archivo:</label>
                                                                </div>
                                                                <div class="col-11">
                                                                    <asp:FileUpload ID="FuSolucion" runat="server" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>


                                            </div>
                                        </div>

                                        <!--SEXTO CONTENIDO-->
                                        <div class="tab-pane p-20" id="reasignacion" role="tabpanel">
                                            <div class="col-lg-12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <!--Inicio Fila 1-->
                                                        <div class="row p-t-20" runat="server" id="div2">
                                                            <div class="col-12">
                                                                <div class="form-group row">
                                                                    <div class="col-1">
                                                                        <label class="control-label text-danger">*</label><label class="control-label">Acción:</label>
                                                                    </div>
                                                                    <div class="col-11">
                                                                        <asp:DropDownList ID="DdlAccionReasignacion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlAccion_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Seleccione opción..."></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Re-asignación de Tarjeta"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Cancelar Tarjeta Kanban"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row p-t-20">
                                                            <div class="col-12">
                                                                <div class="form-group row">
                                                                    <div class="col-1">
                                                                        <label class="control-label   text-danger">*</label><label class="control-label">Comentario:</label>
                                                                    </div>
                                                                    <div class="col-11">
                                                                        <asp:TextBox ID="TxComentarioReasignacion" AutoPostBack="true" TextMode="MultiLine" Rows="6" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>







                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="modal-footer" runat="server" id="divBotonesPrincipales">
                                        <asp:UpdatePanel ID="UpdateBotones" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Button Text="Cancelar" CssClass="btn btn-light" runat="server" />
                                                <asp:Button Text="Enviar" CssClass="btn" style="background-color:#D9272E;  color: #ffffff;" runat="server" ID="BtnEnviar" OnClick="BtnEnviar_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="modal-footer" runat="server" id="divBotonesLecturaTarjeta" visible="false">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Button Text="Volver" CssClass="btn" style="background-color:#D9272E;  color: #ffffff;" runat="server" ID="BtnVolver" OnClick="BtnVolver_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane" id="nav-solicitudesCerradas" role="tabpanel">
                            <h4 class="card-title">
                                <label runat="server" id="LbTituloTarjetaCerrada"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de la tarea a realizar, <code>(visualizar tarjetas asignadas en la sección: Mi Tablero)</code>.</h6>
                            <br />
                            <br />

                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row col-12">
                                        <div class="col-4" style="text-align: center">
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Fecha Inicio</label>
                                                <div class="col-9">
                                                    <asp:TextBox ID="TxInicioBusqueda" AutoPostBack="true" runat="server" TextMode="Date" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-4" style="text-align: center">
                                            <div class="form-group row">
                                                <label class="col-3 col-form-label">Fecha Fin</label>
                                                <div class="col-9">
                                                    <asp:TextBox ID="TxFinBusqueda" AutoPostBack="true" runat="server" TextMode="Date" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--    <div class="col-2">
                                                </div>--%>

                                        <div class="col-4" style="text-align: center">
                                            <asp:LinkButton ID="BtnBuscar" runat="server" title="Buscar" class="btn btn-primary" OnClick="BtnBuscar_Click"><i class="mdi mdi-search-web text-white"></i></asp:LinkButton>
                                            <asp:LinkButton ID="BtnLimpiar" runat="server" title="Restablecer" Style="background-color: #0F71F5" class="btn" OnClick="BtnLimpiar_Click"><i class="mdi mdi-refresh text-white"></i></asp:LinkButton>
                                            <asp:LinkButton ID="BtnDescargar" runat="server" title="Descargar" Style="background-color: #059500" class="btn" OnClick="BtnDescargar_Click"><i class="mdi mdi-download text-white"></i></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-md-12" runat="server" id="divTarjetasCerradas" visible="false" style="text-align: center">
                                        <p><b><code>No cuenta con tarjetas cerradas en el rango de fechas establecidas.</code></b></p>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnDescargar" />
                                </Triggers>
                            </asp:UpdatePanel>



                            <asp:UpdatePanel runat="server" ID="UpMisSolicitudes" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">

                                            <div class="col-md-12" runat="server" id="divMisSolicitudes" visible="true">
                                                <div class="row col-12 mt-3">
                                                    <div class="table table-bordered">
                                                        <asp:GridView ID="GvSolicitudes" runat="server"
                                                            CssClass="mydatagrid"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="header"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true"
                                                            GridLines="None"
                                                            PageSize="5">
                                                            <Columns>
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" />
                                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" />
                                                                <asp:BoundField DataField="userCreo" HeaderText="Usuario Creo" Visible="true" />
                                                                <asp:BoundField DataField="nombreestado" HeaderText="Estado" Visible="true" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <!--SEGUNDO CONTENIDO-->
                        <div class="tab-pane" id="nav-modificarTarjeta" role="tabpanel" style="height: 720px">
                            <h4 class="card-title">
                                <label runat="server" id="LbTituloModificarTarjeta"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de la tarjeta a reasignar.<code>( Lista de tareas areasignar filtrado por equipo de trabajo)</code>.</h6>
                            <br />

                            <asp:UpdatePanel runat="server" ID="UpBusquedaModificar">
                                <ContentTemplate>
                                    <div class="row p-t-20 col-9">
                                        <div class="col-1  ">
                                            <label class="control-label col-form-label">Buscar:</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox runat="server" ID="TxBuscar" CssClass="form-control" placeholder="Búsqueda por Id Tarea o Nombre del colaborador.."></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ID="UpTablaReasignar" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" runat="server" id="div1" visible="true">
                                                <div class="row col-12 mt-3">
                                                    <div class="table table-bordered">
                                                        <asp:GridView ID="GvReasignar" runat="server"
                                                            CssClass="mydatagrid"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="header"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true" OnRowCommand="GvReasignar_RowCommand"
                                                            GridLines="None" OnPageIndexChanging="GvReasignar_PageIndexChanging"
                                                            PageSize="5">
                                                            <Columns>
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" ItemStyle-Width="2%" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" ItemStyle-Width="20%" />
                                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" ItemStyle-Width="25%" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" ItemStyle-Width="3%" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" ItemStyle-Width="10%" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-HorizontalAlign="center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="detalleFinalizo" HeaderText="Motivo" Visible="true" ItemStyle-Width="30%" />
                                                                <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnReasignar" runat="server" title="Reasignar" class="btn btn-info" CommandArgument='<%# Eval("idSolicitud")%>' CommandName="Reasignar">
                                                                <i class="mdi  mdi-pencil text-white"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>



                        </div>
                        <!--/SEGUNDO CONTENIDO-->

                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmarMaximaPrioridad" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 800px; top: 400px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpTitulo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTitulo" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row p-t-20" runat="server" id="DivEstados">
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box bg-info text-center">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbEnCola" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Cola</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box bg-info text-center">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbEjecucion" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">En Ejecución</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box bg-info text-center">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbDetenidas" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Detenidas</h6>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="card">
                                        <div class="box bg-info text-center">
                                            <h1 class="font-light text-white">
                                                <asp:Label ID="LbAtrasado" runat="server"></asp:Label></h1>
                                            <h6 class="text-white">Atrasados</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server" style="text-align: center" id="divMensajeCargabilidad">
                                <p><b><code>Cargabilidad de la tarjeta en minutos diarios.</code></b></p>
                            </div>

                            <div class="table-responsive m-t-20">
                                <asp:GridView ID="GVDistribucion" runat="server" Visible="true"
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
                                        <asp:BoundField DataField="min" HeaderText="Mins" ItemStyle-HorizontalAlign="center" />
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Título:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxTituloModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Prioridad:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxPrioridadModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Tiempo Productivo:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxTimeModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Fecha Inicio:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxInicioModal" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="col-form-label">Fecha Entrega:</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxEntregaModal" AutoPostBack="true" runat="server" class="form-control" TextMode="DateTimeLocal" ReadOnly="true" style="text-align: center"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-md-2">
                                    <label class="col-form-label">Tipo Gestión:</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="TxGestionModal" AutoPostBack="true" runat="server" class="form-control" ReadOnly="true" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row">
                                <div class="col-2">
                                    <label class="control-label" visible="false" runat="server" id="lbDetalleModal">Detalle:</label>
                                </div>
                                <div class="col-10">
                                    <asp:TextBox ID="TxDetalleModal" AutoPostBack="true" Visible="false" runat="server" TextMode="MultiLine" Rows="2" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12" runat="server" style="text-align: center" visible="false" id="divComentariosAdjuntos">
                                <p><b><code><asp:Label ID="LbAdvertenciaModal" runat="server" Text="" ></asp:Label></code></b></p>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmarTarea" runat="server" Text="Enviar" class="btn btn-info" OnClick="BtnConfirmarTarea_Click" />
                        </ContentTemplate>

                            <Triggers>
                                <asp:PostBackTrigger ControlID="BtnConfirmarTarea" />
                            </Triggers>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>










  <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalConfirmarAdjunto" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelConfirmar">
                                <b><asp:Label runat="server" ID="Label1" CssClass="col-form-label"></asp:Label></b>
                            </h4>
                            <asp:Label runat="server" ID="LbMensaje" CssClass="col-form-label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnConfirmarAdjunto" runat="server" Text="Aceptar" class="btn"  style="background-color:#D9272E;  color: #ffffff;" OnClick="BtnConfirmarAdjunto_Click"/>
                        </ContentTemplate>
             <%--           <Triggers>
                            <asp:PostBackTrigger ControlID="BtnConfirmarAdjunto" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
