<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Tarjetas.aspx.cs" Inherits="Infatlan_Kanban.pages.Tarjetas" %>
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
        function ModalTarjetaOpen() { $('#ModalTarjeta').modal('show'); }
        function ModalTarjetaClose() { $('#ModalTarjeta').modal('hide'); }
    </script>
    
  <%--  <link href="dist/css/pages/tab-page.css" rel="stylesheet">--%>
    <link href="../dist/css/pages/tab-page.css" rel="stylesheet" />

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
        <div class="col-md-12">
            <h4 class=" text-dark">Kanban Board | Gestiones Técnicas</h4>
        </div>
        <div class="col-md-6">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                <li class="breadcrumb-item active">Tarjetas</li>
            </ol>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12">
            <div class="card">
                <div class="card-body"  style="border:none">
                    <nav>
                        <div class="nav nav-pills " id="nav-tab" role="tablist">
                            <a class="nav-item nav-link active" runat="server" visible="true" id="nav_tarjetasCerradas_tab" data-toggle="tab" href="#nav-solicitudesCerradas" role="tab" aria-controls="nav-solicitudesCerradas" aria-selected="false"><i class="mdi mdi-book-open"></i>Mis Tarjetas Cerradas</a>
                            <a class="nav-item nav-link" runat="server" visible="true" id="nav_Reasignar" data-toggle="tab" href="#nav-Reasignar" role="tab" aria-controls="nav-Reasignar" aria-selected="false"><i class="mdi  mdi-pencil"></i>Reasignar Tarjetas</a>
                            <a class="nav-item nav-link" runat="server" visible="true" id="nav_tarjetaDetenido_tab" data-toggle="tab" href="#nav-DetenidoTarjeta" role="tab" aria-controls="nav-nav_tarjetaDetenido" aria-selected="false"><i class="mdi mdi-clock"></i>Detener Tarjetas</a>
                            
                        </div>
                    </nav>
                    <br />
                    <br />
                    <div class="tab-content tabcontent-border">
                        <div class="tab-pane fade show active" id="nav-solicitudesCerradas" role="tabpanel">
                            <h4 class="card-title">
                                <label runat="server" id="LbTituloTarjeta"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de las tarjetas kanban.</h6>
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
                                            <asp:LinkButton ID="BtnBuscar" runat="server" title="Buscar" class="btn btn-primary"  OnClick="BtnBuscar_Click"><i class="mdi mdi-search-web text-white"></i></asp:LinkButton>
                                            <asp:LinkButton ID="BtnLimpiar" runat="server" title="Restablecer" Style="background-color: #0F71F5" class="btn" OnClick="BtnLimpiar_Click"><i class="mdi mdi-refresh text-white"></i></asp:LinkButton>
                                            <asp:LinkButton ID="BtnDescargar" runat="server" title="Descargar" Style="background-color: #059500" class="btn"  OnClick="BtnDescargar_Click"><i class="mdi mdi-download text-white"></i></asp:LinkButton>
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
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GvSolicitudes" runat="server"
                                                            CssClass="table  table-hover table-sm"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true" 
                                                            GridLines="None"
                                                            PageSize="10">
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

                        <div class="tab-pane" id="nav-DetenidoTarjeta" role="tabpanel" style="height: 720px">
                            <h4 class="card-title">
                                <label runat="server" id="LbTituloDetenerTarjeta"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de la tarjeta a detener.</h6>
                            <br />

                            <div class="row p-t-20 col-9">
                                <div class="col-1  ">
                                    <label class="control-label col-form-label">Buscar:</label>
                                </div>
                                <div class="col-8">
                                    <asp:TextBox runat="server" ID="TxBusquedaDetener" CssClass="form-control" placeholder="Búsqueda por Id Tarea o Nombre del colaborador.."></asp:TextBox>
                                </div>
                            </div>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" runat="server" id="div3" visible="true">
                                                <div class="row col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GvDetener" runat="server"
                                                            CssClass="table  table-hover table-sm"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false"
                                                            AllowPaging="true"  OnRowCommand="GvDetener_RowCommand"
                                                            GridLines="None" OnPageIndexChanging="GvDetener_PageIndexChanging"
                                                            PageSize="10">
                                                            <Columns>
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" ItemStyle-Width="2%" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" ItemStyle-Width="20%" />
                                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" ItemStyle-Width="25%" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" ItemStyle-Width="3%" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-HorizontalAlign="center" ItemStyle-Width="8%" />
                                                                <asp:BoundField DataField="detalleFinalizo" HeaderText="Motivo" Visible="true" ItemStyle-Width="30%" />
                                                                <asp:BoundField DataField="nombreResponsable" HeaderText="Responsable" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="nombreTeams" HeaderText="Equipo" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnDetener" runat="server" title="Reasignar" class="btn btn-info" CommandArgument='<%# Eval("idSolicitud")%>' CommandName="Detener">
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


                        <div class="tab-pane" id="nav-Reasignar" role="tabpanel" style="height: 720px">
                            <h4 class="card-title">
                                <label runat="server" id="LbTituloModificarTarjeta"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de la tarjeta a reasignar.</h6>
                            <br />

                            <div class="row p-t-20 col-9">
                                <div class="col-1  ">
                                    <label class="control-label col-form-label">Buscar:</label>
                                </div>
                                <div class="col-8">
                                    <asp:TextBox runat="server" ID="TxBuscarReasignar" CssClass="form-control" placeholder="Búsqueda por Id Tarea o Nombre del colaborador." ></asp:TextBox>
                                </div>
                            </div>

                            <asp:UpdatePanel runat="server" ID="UpTablaReasignar" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" runat="server" id="div1" visible="true">
                                                <div class="row col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GvReasignar" runat="server"
                                                            CssClass="table  table-hover table-sm"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows" OnRowCommand="GvReasignar_RowCommand"
                                                            AutoGenerateColumns="false" OnPageIndexChanging="GvReasignar_PageIndexChanging"
                                                            AllowPaging="true"
                                                            GridLines="None" 
                                                            PageSize="10">
                                                            <Columns>
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" ItemStyle-Width="2%" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" ItemStyle-Width="20%" />
                                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" ItemStyle-Width="25%" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" ItemStyle-Width="3%" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-HorizontalAlign="center" ItemStyle-Width="8%" />
                                                               
                                                                <asp:BoundField DataField="nombreResponsable" HeaderText="Responsable" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="nombreTeams" HeaderText="Equipo" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
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

                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--    MODAL INICIO--%>
    <div class="modal fade" id="ModalTarjeta" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 880px; height: 860px; top: 414px; left: 50%; transform: translate(-50%, -50%);">

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

                    <asp:UpdatePanel runat="server" ID="UPFormulario" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="vtabs">
                                <ul class="nav nav-tabs tabs-vertical" role="tablist">
                                    <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab" runat="server" id="tabGenerales"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspGenerales</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" runat="server" role="tab" id="tabAdjuntos" visible="true"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspAdjuntos</span></a></li>
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
                                                        <asp:TextBox ID="TxTitulo" AutoPostBack="true" runat="server" class="form-control text-uppercase" ></asp:TextBox>
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
                                                        <asp:DropDownList runat="server" ID="DdlResponsable_1" CssClass="select2 form-control custom-select" AutoPostBack="true" Style="width: 100%" ></asp:DropDownList>
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
        
                                    </div>

                                    <div class="tab-pane" id="comentarios" role="tabpanel" style="height: 450px; width: 630px;">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>
                        
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
                                                                GridLines="None" 
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
                                                                GridLines="None"
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
                                                                AllowPaging="true" 
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
                                                 

                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnCancelarTarea_1" runat="server" Text="Cancelar" class="btn btn-secondary" />
                            <asp:Button ID="BtnConfirmarTarea_1" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
