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

      
        function ModalListaOpen() { $('#ModalLista').modal('show'); }
        function ModalListaClose() { $('#ModalLista').modal('hide'); }

        function ModalTarjetaOpen() { $('#ModalTarjeta').modal('show'); }
        function ModalTarjetaClose() { $('#ModalTarjeta').modal('hide'); }

        function openDescargarModal() { $('#DescargaModal').modal('show'); }

        function closeModal() { $('#VisualizarImagen').modal('show'); }
        function openModalImagen() { $('#VisualizarImagen').modal('show'); }

    </script>
    
  <%--  <link href="dist/css/pages/tab-page.css" rel="stylesheet">--%>
    <link href="../dist/css/pages/tab-page.css" rel="stylesheet" />
        <link href="../assets/node_modules/select2/dist/css/select2.css" rel="stylesheet" />
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
                       <img src="../images/TextoBlanco.png" height="20"/>
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
                <div class="card-body"  style="border:none;">
                    <nav>
                        <div class="nav nav-pills " id="nav-tab" role="tablist">
                            <a class="nav-item nav-link active" runat="server" visible="false" id="nav_tarjetasCerradas_tab" data-toggle="tab" href="#nav-solicitudesCerradas" role="tab" aria-controls="nav-solicitudesCerradas" aria-selected="false"><i class="mdi mdi-book-open"></i> Mis Tarjetas Cerradas</a>
                            <a class="nav-item nav-link" runat="server" visible="false" id="nav_tarjetasCerradasColaborador_tab" data-toggle="tab" href="#nav-solicitudesCerradasColaborador" role="tab" aria-controls="nav-solicitudesCerradasColaborador" aria-selected="false"><i class="mdi mdi-book-open"></i> Tarjetas Cerradas Colaborador</a>
<%--                            <a class="nav-item nav-link" runat="server" visible="false" id="nav_Reasignar" data-toggle="tab" href="#nav-Reasignar" role="tab" aria-controls="nav-Reasignar" aria-selected="false"><i class="mdi  mdi-pencil"></i> Reasignar Tarjetas</a>
                            <a class="nav-item nav-link" runat="server" visible="false" id="nav_tarjetaDetenido_tab" data-toggle="tab" href="#nav-DetenidoTarjeta" role="tab" aria-controls="nav-nav_tarjetaDetenido" aria-selected="false"><i class="mdi mdi-clock"></i> Detener Tarjetas</a> 
                            <a class="nav-item nav-link" runat="server" visible="false" id="nav_eliminarTarjeta_tab" data-toggle="tab" href="#nav-EliminarTarjeta" role="tab" aria-controls="nav-nav_tarjetaEliminar" aria-selected="false"><i class="mdi mdi-delete"></i> Eliminar Tarjetas</a>  --%>
                        </div>
                    </nav>
                    <br />
                    <br />
                    <div class="tab-content">
                        <div class="tab-pane fade show active" id="nav-solicitudesCerradas" role="tabpanel">
                            <div class="row col-12">
                                <div class="col-6">
                                    <h4 class="card-title">
                                        <label runat="server" id="LbTituloTarjeta"></label>
                                    </h4>
                                    <h6 class="card-subtitle">Datos generales de las tarjetas kanban.</h6>
                                </div>
                                <div class="col-6 text-right" style="zoom: 75%">
                                    <asp:Label ID="Label6" runat="server" Text="Prioridades:" ForeColor="Black"></asp:Label>
                                    <span class="label label-danger">Máxima</span>
                                    <span class="label label-primary">Alta</span>
                                    <span class="label label-warning">Normal</span>
                                    <span class="label label-info">Baja</span>
                                </div>
                            </div>
                                               
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <div class="card-header" role="tab" runat="server" id="DivBusquedaReporte" visible="true">

                                         <div class="row col-12" runat="server">
                                             <label class="col-2 col-form-label  font-12">Tipo de Búsqueda:</label>

                                             <div class="col-md-5" runat="server" id="DivBusqueda" visible="true">
                                                 <asp:DropDownList ID="DdlTipoBusqueda" runat="server" Font-Size="Smaller" AutoPostBack="true" CssClass="form-control custom-select" OnSelectedIndexChanged="DdlTipoBusqueda_SelectedIndexChanged">
                                                     <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                     <asp:ListItem Value="1" Text="Id Tarjeta"></asp:ListItem>
                                                     <asp:ListItem Value="2" Text="Rango de Fecha"></asp:ListItem>
                                                 </asp:DropDownList>
                                             </div>

                                             <div class="col-md-5" runat="server" id="divTXBusqueda" visible="false">
                                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" class="form-control text-uppercase" OnTextChanged="TxBusqueda_TextChanged"></asp:TextBox>
                                            </div>
                                         </div>
                                         <br /> 
                                      
                                        <div class="row col-12 font-12" runat="server" id="rowDetalle">                                                                                                              
                                            <div class="col-md-2" runat="server" id="divTxInicio" visible="false">
                                                <asp:Label ID="Label4" runat="server" Text="Fecha Inicio" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:TextBox ID="TxInicioBusqueda" Font-Size="Smaller" AutoPostBack="true" runat="server" TextMode="Date" class="form-control"></asp:TextBox> 
                                            </div>

                                            <div class="col-md-2" runat="server" id="divFechaFin" visible="false">
                                                <asp:Label ID="Label5" runat="server" Text="Fecha Fin" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:TextBox ID="TxFinBusqueda" AutoPostBack="true" runat="server"  Font-Size="Smaller" TextMode="Date" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2" runat="server" id="divTipoTarjeta" visible="false">
                                                <asp:Label ID="Label7" runat="server" Text="Tipo Tarjeta" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DdlTarjeta"  Font-Size="Smaller" runat="server"  CssClass="select2 form-control custom-select">
                                                    <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Técnicas"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Operativas"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="divGestion" visible="false">
                                                <asp:Label ID="Label8" runat="server" Text="Gestión" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList runat="server" ID="DdlTipoGestion" Font-Size="80%" CssClass="select2 form-control custom-select" AutoPostBack="true"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="divPrioridad" visible="false">
                                                <asp:Label ID="Label9" runat="server" Text="Prioridad" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DdlPrioridad" runat="server"  Font-Size="80%" CssClass="select2 form-control custom-select" >
                                                    <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="divBotones" visible="false">
                                                <br />
                                             <asp:LinkButton ID="BtnBuscar" runat="server" title="Buscar" class="btn btn-primary" OnClick="BtnBuscar_Click"><i class="mdi mdi-search-web text-white"></i></asp:LinkButton>
                                             <asp:LinkButton ID="BtnLimpiar" runat="server" title="Restablecer" Style="background-color: #0F71F5" class="btn" OnClick="BtnLimpiar_Click"><i class="mdi mdi-refresh text-white"></i></asp:LinkButton>
                                             <asp:LinkButton ID="BtnDescargar" runat="server" title="Descargar" Style="background-color: #059500" class="btn" OnClick="BtnDescargar_Click"><i class="mdi mdi-download text-white"></i></asp:LinkButton>
                                         </div>
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

                                            <div class="col-md-12" runat="server" id="divMisSolicitudes" visible="true" style="zoom:75%">
                                                <div class="row col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GvSolicitudes" runat="server"
                                                            CssClass="table  table-hover table-sm"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false" OnRowCommand="GvSolicitudes_RowCommand"
                                                            AllowPaging="true"  OnPageIndexChanging="GvSolicitudes_PageIndexChanging"
                                                            GridLines="None" OnRowDataBound="GvSolicitudes_RowDataBound"
                                                            PageSize="10">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Detalle" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnVer" Title="Ver Tarjeta" Visible="true" runat="server" class="btn" Style="background-color: #F1961B; color: #ffffff;" CommandArgument='<%# Eval("idSolicitud") %>' CommandName="Ver">
                                                                         <i class="icon-pencil" ></i>
                                                                        </asp:LinkButton>

                                                                        <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnVerificacion"  Title="Lista Verificación" Visible="true" runat="server" class="btn" Style="background-color: #1CC8FF; color: #ffffff;" CommandArgument='<%# Eval("idSolicitud") %>' CommandName="Lista">
                                                                         <i class=" icon-list" ></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>

                                                                    
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                              
                                                                
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Id Tarjeta" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" />
                                                                <asp:BoundField DataField="fechaFinalizoTarjeta" ItemStyle-HorizontalAlign="center" HeaderText="Finalizó" Visible="true" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" />                                                                                                                
                                                                <asp:BoundField DataField="userCreo" HeaderText="Usuario Creo" Visible="true" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-ForeColor="White" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="nombreestado" HeaderText="Estado" Visible="true" ItemStyle-ForeColor="White" ItemStyle-HorizontalAlign="center"  />
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

                        <div class="tab-pane" id="nav-solicitudesCerradasColaborador" role="tabpanel">
                            <div class="row col-12">
                                <div class="col-6">
                                    <h4 class="card-title">
                                        <label runat="server" id="LbTituloSoliCerradas"></label>
                                    </h4>
                                    <h6 class="card-subtitle">Datos generales de las Tarjetas Kanban.</h6>
                                </div>
                                <div class="col-6 text-right" style="zoom: 75%">
                                    <asp:Label ID="Label2" runat="server" Text="Prioridades:" ForeColor="Black"></asp:Label>
                                    <span class="label label-danger">Máxima</span>
                                    <span class="label label-primary">Alta</span>
                                    <span class="label label-warning">Normal</span>
                                    <span class="label label-info">Baja</span>
                                </div>
                            </div>

                  <%--          <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                  
                                    <div class="col-md-12" runat="server" id="div11" visible="false" style="text-align: center">
                                        <p><b><code>No cuenta con tarjetas cerradas en el rango de fechas establecidas.</code></b></p>
                                    </div>
                                </ContentTemplate>
                             
                            </asp:UpdatePanel>--%>



                            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="card-header" role="tab" runat="server" id="Div9" visible="true">

                                        <div class="row col-12" runat="server">
                                            <label class="col-2 col-form-label  font-12">Tipo de Búsqueda:</label>

                                            <div class="col-md-5" runat="server" id="Div10" visible="true">
                                                <asp:DropDownList ID="DdlBusquedaTarjetasColaborador" runat="server" Font-Size="Smaller" AutoPostBack="true" CssClass="form-control custom-select"   OnSelectedIndexChanged="DdlBusquedaTarjetasColaborador_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Seleccione"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Id Tarjeta"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Búsqueda Avanzada"></asp:ListItem>                                                
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-5" runat="server" id="divIdTarjeta" visible="false">
                                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxIdTrajetaCerrada" AutoPostBack="true" class="form-control text-uppercase" OnTextChanged="TxIdTrajetaCerrada_TextChanged"></asp:TextBox>
                                            </div>

                                        </div>
                                        <br />
                                                                                                                    
                                        <div class="row col-12 font-12" runat="server" id="DivDetalleBusqueda" visible="false" >
                                            <div class="col-md-2" runat="server" id="DivTipoBusqueda" visible="true">
                                                <asp:Label ID="Label10" runat="server" Text="Buscar por:" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DdlBusquedaPorTipo" runat="server" Font-Size="80%" CssClass="select2 form-control custom-select" AutoPostBack="true" OnSelectedIndexChanged="DdlBusquedaPorTipo_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="Todo"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Equipo"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Colaborador"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-4" runat="server" id="DivSeleccionBusqueda" visible="false">
                                                <asp:Label ID="LbTituloOpcion" runat="server" Text="Opción" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList runat="server" ID="DdlEquipoBA" Font-Size="80%" CssClass="select2 form-control custom-select" AutoPostBack="true" OnSelectedIndexChanged="DdlEquipoBA_SelectedIndexChanged"></asp:DropDownList>
                                                 <asp:DropDownList runat="server" ID="DdlColaboradorBA" Font-Size="80%" CssClass="select2 form-control custom-select" AutoPostBack="true" OnSelectedIndexChanged="DdlColaboradorBA_SelectedIndexChanged"></asp:DropDownList>
                                            </div>

                                              <div class="col-md-2" runat="server" id="Div4" visible="true">
                                                <asp:Label ID="Label13" runat="server" Text="Asignadas por Mi:" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DropDownList7" runat="server" Font-Size="80%" CssClass="select2 form-control custom-select">
                                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Si"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="divFechaInicioBA" visible="true">
                                                <asp:Label ID="Label15" runat="server" Text="Fecha Inicio" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:TextBox ID="TXFechaInicioBA" Font-Size="Smaller" AutoPostBack="true" runat="server" TextMode="Date" class="form-control"></asp:TextBox> 
                                            </div>

                                            <div class="col-md-2" runat="server" id="DivFechaFinBA" visible="true">
                                                <asp:Label ID="Label16" runat="server" Text="Fecha Fin" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:TextBox ID="TXFechaFinBA" AutoPostBack="true" runat="server"  Font-Size="Smaller" TextMode="Date" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2" runat="server" id="DivTipoTarjetaBA" visible="true">
                                                <asp:Label ID="Label17" runat="server" Text="Tipo Tarjeta" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DDLTipoTarjetaBA"  Font-Size="Smaller" runat="server"  CssClass="select2 form-control custom-select">
                                                    <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Técnicas"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Operativas"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="Div2" visible="true">
                                                <asp:Label ID="Label12" runat="server" Text="Estado:" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DdlEstadoBA" runat="server" Font-Size="80%" CssClass="select2 form-control custom-select">
                                                    <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Realizado a Tiempo"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Realizado Fuera de Tiempo"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-4" runat="server" id="DivGestionBA" visible="true">
                                                <asp:Label ID="Label18" runat="server" Text="Gestión" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList runat="server" ID="DdlGestionBA" Font-Size="80%" CssClass="select2 form-control custom-select" AutoPostBack="true"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="DivPrioridadBA" visible="true">
                                                <asp:Label ID="Label19" runat="server" Text="Prioridad" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DropDownList6" runat="server"  Font-Size="80%" CssClass="select2 form-control custom-select" >
                                                    <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>



                                            <div class="col-md-2" runat="server" id="divBotonesBA" visible="true">
                                                <br />
                                             <asp:LinkButton ID="BtnBuscarBA" runat="server" title="Buscar" class="btn btn-primary" OnClick="BtnBuscarBA_Click"><i class="mdi mdi-search-web text-white"></i></asp:LinkButton>
                                             <asp:LinkButton ID="LinkButton5" runat="server" title="Restablecer" Style="background-color: #0F71F5" class="btn" OnClick="BtnLimpiar_Click"><i class="mdi mdi-refresh text-white"></i></asp:LinkButton>
                                             <asp:LinkButton ID="LinkButton6" runat="server" title="Descargar" Style="background-color: #059500" class="btn" OnClick="BtnDescargar_Click"><i class="mdi mdi-download text-white"></i></asp:LinkButton>
                                         </div>
                                        </div>


                                   <%--     <div class="row col-12 font-12" runat="server" id="Div14">

                                            <div class="col-md-2" runat="server" id="divTxFechaInicio" visible="false">
                                                <asp:Label ID="Label10" runat="server" Text="Fecha Inicio" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:TextBox ID="TxFechaInicio" Font-Size="Smaller" AutoPostBack="true" runat="server" TextMode="Date" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2" runat="server" id="divTxFechaFin" visible="false">
                                                <asp:Label ID="Label11" runat="server" Text="Fecha Fin" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:TextBox ID="TxFechaFin" AutoPostBack="true" runat="server" Font-Size="Smaller" TextMode="Date" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-2" runat="server" id="div17" visible="false">
                                                <asp:Label ID="Label12" runat="server" Text="Tipo Tarjeta" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DropDownList2" Font-Size="Smaller" runat="server" CssClass="select2 form-control custom-select">
                                                    <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Técnicas"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Operativas"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="div19" visible="false">
                                                <asp:Label ID="Label13" runat="server" Text="Gestión" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList runat="server" ID="DropDownList3" Font-Size="80%" CssClass="select2 form-control custom-select" AutoPostBack="true"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="div20" visible="false">
                                                <asp:Label ID="Label14" runat="server" Text="Prioridad" class="control-label" Style="margin-left: auto; margin-right: auto"></asp:Label>
                                                <asp:DropDownList ID="DropDownList4" runat="server" Font-Size="80%" CssClass="select2 form-control custom-select">
                                                    <asp:ListItem Value="0" Text="Todas"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Máxima Prioridad"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2" runat="server" id="div21" visible="false">
                                                <br />
                                                <asp:LinkButton ID="LinkButton1" runat="server" title="Buscar" class="btn btn-primary" OnClick="BtnBuscar_Click"><i class="mdi mdi-search-web text-white"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" title="Restablecer" Style="background-color: #0F71F5" class="btn" OnClick="BtnLimpiar_Click"><i class="mdi mdi-refresh text-white"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" runat="server" title="Descargar" Style="background-color: #059500" class="btn" OnClick="BtnDescargar_Click"><i class="mdi mdi-download text-white"></i></asp:LinkButton>
                                            </div>
                                        </div>--%>


                                    </div>
                                    <div class="col-md-12" runat="server" id="div22" visible="false" style="text-align: center">
                                        <p><b><code>No cuenta con tarjetas cerradas en el rango de fechas establecidas.</code></b></p>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnDescargar" />
                                </Triggers>
                            </asp:UpdatePanel>





                            <asp:UpdatePanel runat="server" ID="UpSolicitudesColaboradores" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">

                                            <div class="col-md-12" runat="server" id="divSolicitudesColaboradores" visible="true" style="zoom: 75%">
                                                <div class="row col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GvSolicitudesColaborador" runat="server"
                                                            CssClass="table table-hover table-sm"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows" 
                                                            AutoGenerateColumns="false" OnRowCommand="GvSolicitudesColaborador_RowCommand"
                                                            AllowPaging="true"  OnPageIndexChanging="GvSolicitudesColaborador_PageIndexChanging"
                                                            GridLines="None"  OnRowDataBound="GvSolicitudesColaborador_RowDataBound"
                                                            PageSize="10">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnVer" Title="Ver Tarjeta" Visible="true" runat="server" class="btn" Style="background-color: #F1961B; color: #ffffff;" CommandArgument='<%# Eval("idSolicitud") %>' CommandName="Ver">
                                                                         <i class="icon-pencil" ></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" />                     
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" />
                                                                <asp:BoundField DataField="fechaFinalizoTarjeta" ItemStyle-HorizontalAlign="center" HeaderText="Finalizó" Visible="true" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" />
                                                                <asp:BoundField DataField="nombreResponsable" HeaderText="Responsable" Visible="true"  ItemStyle-HorizontalAlign="center"/>
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-ForeColor="White" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="nombreestado" HeaderText="Estado" Visible="true" ItemStyle-ForeColor="White" ItemStyle-HorizontalAlign="center" />
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

                        <div class="tab-pane" id="nav-DetenidoTarjeta" role="tabpanel">
                            <div class="row col-12">
                                <div class="col-8">
                                    <h4 class="card-title">
                                        <label runat="server" id="LbTituloDetenerTarjeta"></label>
                                    </h4>
                                     <h6 class="card-subtitle">Datos generales de la tarjeta a detener.</h6>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <div class="card-header" role="tab" runat="server" id="Div5" visible="true" style="zoom:85%">
                                    <div class="row col-10">
                                        <div class="col-2  ">
                                            <label class="control-label col-form-label">Buscar por:</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox runat="server" ID="TxBusquedaDetener" class="form-control text-uppercase" placeholder="Búsqueda por Id Tarjeta o Nombre del colaborador." OnTextChanged="TxBusquedaDetener_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                         </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" runat="server" id="div3" visible="true" style="zoom:85%">
                                                <div class="row col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GvDetener" runat="server"
                                                            CssClass="table  table-hover table-sm"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            HeaderStyle-HorizontalAlign="center"
                                                            RowStyle-CssClass="rows"
                                                            AutoGenerateColumns="false" OnRowDataBound="GvDetener_RowDataBound"
                                                            AllowPaging="true"  OnRowCommand="GvDetener_RowCommand"
                                                            GridLines="None" OnPageIndexChanging="GvDetener_PageIndexChanging"
                                                            PageSize="7">
                                                            <Columns>
                                                                                                                                <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnDetener" runat="server" title="Reasignar" class="btn" Style="background-color: #F1961B; color: #ffffff;" CommandArgument='<%# Eval("idSolicitud")%>' CommandName="Detener">
                                                                <i class="mdi  mdi-pencil text-white"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" ItemStyle-Width="2%" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" ItemStyle-Width="18%" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" ItemStyle-Width="2%" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" ItemStyle-Width="6%" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" ItemStyle-Width="6%" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />                                                              
                                                                <asp:BoundField DataField="detalleFinalizo" HeaderText="Motivo" Visible="true" ItemStyle-Width="27%" />
                                                                <asp:BoundField DataField="nombreResponsable" HeaderText="Responsable" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="nombreTeams" HeaderText="Equipo" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-HorizontalAlign="center" ItemStyle-Width="8%"  ItemStyle-ForeColor="White" />

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

                        <div class="tab-pane" id="nav-Reasignar" role="tabpanel" >
                            <h4 class="card-title">
                                <label runat="server" id="LbTituloModificarTarjeta"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de la tarjeta a reasignar.</h6>
                           <%-- <br />--%>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <div class="card-header" role="tab" runat="server" id="Div7" visible="true" style="zoom:85%">
                                    <div class="row col-10">
                                        <div class="col-2  ">
                                            <label class="control-label col-form-label">Búsqueda:</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox runat="server" ID="TxBuscarReasignar" class="form-control text-uppercase" placeholder="Búsqueda por Id Tarjeta o Nombre del colaborador."  OnTextChanged="TxBuscarReasignar_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                          </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>  
                            

                                                   

                            <asp:UpdatePanel runat="server" ID="UpTablaReasignar" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" runat="server" id="div1" visible="true" style="zoom:75%">
                                                <div class="row col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GvReasignar" runat="server"
                                                            CssClass="table  table-hover table-sm"
                                                            PagerStyle-CssClass="pgr"
                                                            HeaderStyle-CssClass="table"
                                                            HeaderStyle-HorizontalAlign="center" 
                                                            RowStyle-CssClass="rows" OnRowCommand="GvReasignar_RowCommand"
                                                            AutoGenerateColumns="false" OnPageIndexChanging="GvReasignar_PageIndexChanging"
                                                            AllowPaging="true" OnRowDataBound="GvReasignar_RowDataBound"
                                                            GridLines="None" 
                                                            PageSize="10">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnReasignar" runat="server" title="Reasignar" class="btn" Style="background-color: #F1961B; color: #ffffff;" CommandArgument='<%# Eval("idSolicitud")%>' CommandName="Reasignar">
                                                                <i class="mdi  mdi-pencil text-white"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" ItemStyle-Width="2%" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" ItemStyle-Width="20%" />
                                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" ItemStyle-Width="25%" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" ItemStyle-Width="3%" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
                                                                
                                                          
                                                                <asp:BoundField DataField="nombreResponsable" HeaderText="Responsable" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="nombreTeams" HeaderText="Equipo" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-HorizontalAlign="center" ItemStyle-Width="8%" ItemStyle-ForeColor="White" />
                                                                

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

                         <div class="tab-pane" id="nav-EliminarTarjeta" role="tabpanel" >
                            <h4 class="card-title">
                                <label runat="server" id="Label1"></label>
                            </h4>
                            <h6 class="card-subtitle">Datos generales de la tarjeta a Eliminar.</h6>
                           <%-- <br />--%>

                            <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <div class="card-header" role="tab" runat="server" id="Div6" visible="true" style="zoom:85%">
                                    <div class="row col-10">
                                        <div class="col-2  ">
                                            <label class="control-label col-form-label">Búsqueda:</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox runat="server" ID="TextBox1" class="form-control text-uppercase" placeholder="Búsqueda por Id Tarjeta o Nombre del colaborador."  AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                          </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>  
                            

                                                   

                            <asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row p-t-20">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" runat="server" id="div8" visible="true" style="zoom:75%">
                                                <div class="row col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridView1" runat="server"
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

                                                                <asp:TemplateField HeaderText="Seleccione" ItemStyle-HorizontalAlign="center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="BtnReasignar" runat="server" title="Reasignar" class="btn" Style="background-color: #F1961B; color: #ffffff;" CommandArgument='<%# Eval("idSolicitud")%>' CommandName="Reasignar">
                                                                <i class="mdi  mdi-pencil text-white"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:BoundField DataField="idSolicitud" Visible="true" ItemStyle-HorizontalAlign="center" HeaderText="Tarjeta" ItemStyle-Width="2%" />
                                                                <asp:BoundField DataField="titulo" HeaderText="Titulo" Visible="true" ItemStyle-Width="20%" />
                                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" ItemStyle-Width="25%" />
                                                                <asp:BoundField DataField="minSolicitud" ItemStyle-HorizontalAlign="center" HeaderText="Mins" Visible="true" ItemStyle-Width="3%" />
                                                                <asp:BoundField DataField="fechaInicio" ItemStyle-HorizontalAlign="center" HeaderText="Inicio" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="fechaEntrega" ItemStyle-HorizontalAlign="center" HeaderText="Entrega" Visible="true" ItemStyle-Width="7%" />
                                                                <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" Visible="true" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
                                                                
                                                          
                                                                <asp:BoundField DataField="nombreResponsable" HeaderText="Responsable" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="nombreTeams" HeaderText="Equipo" Visible="true" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="center" />
                                                                <asp:BoundField DataField="prioridad" HeaderText="Prioridad" Visible="true" ItemStyle-HorizontalAlign="center" ItemStyle-Width="8%" ItemStyle-ForeColor="White" />
                                                                

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
    <div class="modal fade" id="ModalTarjeta" data-backdrop="static" data-keyboard="false"  role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
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
                            <div class="vtabs" runat="server" >
                                <ul class="nav nav-tabs tabs-vertical" role="tablist" runat="server" id="tab">
                                    <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab" runat="server" id="tabGenerales"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspGenerales</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#adjunto" runat="server" role="tab" id="tabAdjuntos" visible="true"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspAdjuntos</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#comentarios" role="tab" runat="server" id="tabComentarios"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspComentarios</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#historial" role="tab" runat="server" id="tabHistorial"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspHistorial</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#solucion" role="tab" runat="server" id="tabSolucion"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspSolución</span></a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#verificacion" role="tab" runat="server" id="tabVerificacion"><span class="hidden-sm-up"></span><span class="hidden-xs-down">&nbspCheck List</span></a></li>
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
                                                        <asp:DropDownList runat="server" ID="DdlResponsable_1" CssClass="select2 form-control custom-select" AutoPostBack="true" Style="width: 100%" OnSelectedIndexChanged="DdlResponsable_1_SelectedIndexChanged" ></asp:DropDownList>
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

                                                <div class="row p-t-20" runat="server" visible="false" id="divNotasReasignar">
                                                    <div class="col-12">
                                                        <label class="control-label">Nota:</label>
                                                        <asp:TextBox ID="TxNota" AutoPostBack="true" TextMode="MultiLine" Rows="3" runat="server" class="form-control"></asp:TextBox>
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
                                            <div class="row p-t-20">
                                                <div class="col-12">
                                                    <label class="control-label">Solución:</label>
                                                    <asp:TextBox ID="TxSolucion" AutoPostBack="true" ReadOnly="true" TextMode="MultiLine" Rows="5" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="tab-pane" id="verificacion" role="tabpanel" style="height: 450px; width: 630px;">
                              
                                        <div class="row p-t-20">
                                            <div class="col-6">
                                                <label class="control-label">¿Tarjeta Operativa Requiere Escalación? </label>
                                            </div>

                                            <div class="col-4">
                                                <asp:RadioButtonList ID="ckEscalacion" RepeatDirection="Horizontal" Width="90px" runat="server">
                                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                        </div>
                      
                                        <div class="row p-t-20" runat="server" id="div18" visible="true">
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
                                                        AllowPaging="true"  OnRowCommand="GvCheckListLleno_RowCommand"
                                                        GridLines="None"  OnRowDataBound="GvCheckListLleno_RowDataBound"
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
                                    <br />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnCancelarTarea_1" runat="server" Text="Cancelar" class="btn btn-secondary"  OnClick="BtnCancelarTarea_1_Click"/>
                            <asp:Button ID="BtnConfirmarTarea_1" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnConfirmarTarea_1_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>





        <%--   MODAL  LISTA DE VERIFICACION--%>
    <div class="modal fade" id="ModalLista"  data-backdrop="static" data-keyboard="false"  role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="width: 700px; height: 770px; top: 385px; left: 50%; transform: translate(-50%, -50%);">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h4 class="modal-title">
                                <strong>
                                    <asp:Label ID="LbTituloLista" runat="server" Text="" Style="margin-left: auto; margin-right: auto"></asp:Label></strong></h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row p-t-20">
                                <div class="col-6">
                                    <label class="control-label">¿Tarjeta Operativa Requiere Escalación? </label>
                                </div>

                                <div class="col-4">
                                    <asp:RadioButtonList ID="RbEscalacion" RepeatDirection="Horizontal" Width="90px" runat="server" Enabled="false">
                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                            </div>

                            <div class="row p-t-20" runat="server" id="div12" visible="true">
                                <div class="col-12">
                                    <!--Fin Fila 1-->
                                    <div class="table-responsive">
                                        <asp:GridView ID="GvListaUnica" runat="server"
                                            CssClass="table table-hover table-sm"
                                            PagerStyle-CssClass="pgr"
                                            HeaderStyle-CssClass="table"
                                            HeaderStyle-HorizontalAlign="center"
                                            RowStyle-CssClass="rows"
                                            AutoGenerateColumns="false"  OnRowCommand="GvListaUnica_RowCommand"
                                            AllowPaging="true"   OnRowDataBound="GvListaUnica_RowDataBound"
                                            GridLines="None" 
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" Text="Cerrar" class="btn btn-secondary"/>
                        </ContentTemplate>
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
                                <asp:Label ID="Label3" runat="server" Text="" Class="col-sm-12" Style="color: indianred; text-align: center;"></asp:Label>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
                <%--COMBO BUSCADOR--%>

    <script src="../assets/node_modules/select2/dist/js/select2.js"></script>
    <link href="../assets/node_modules/select2/dist/css/select2.css" rel="stylesheet" />
    <style>
        .select2-selection__rendered {line-height: 31px !important;}
        .select2-container .select2-selection--single {height: 35px !important;}
        .select2-selection__arrow {height: 34px !important;}
    </style>
</asp:Content>
