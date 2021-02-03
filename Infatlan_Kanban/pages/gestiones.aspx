<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="gestiones.aspx.cs" Inherits="Infatlan_Kanban.pages.gestiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>

    <link href="../../css/select2.css" rel="stylesheet" />
    <script type="text/javascript">
        function openModal() { $('#ModalGestiones').modal('show'); }
        function cerrarModal() { $('#ModalGestiones').modal('hide'); }

        function openModalTeams() { $('#ModalAddTeams').modal('show'); }
        function cerrarModalTeams() { $('#ModalAddTeams').modal('hide'); }
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
                    <li class="breadcrumb-item active">Tipos de Gestión</li>
                </ol>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">
                        <label runat="server" id="LbTituloTarjeta">Tipo de Gestiones</label></h4>
                    <h6 class="card-subtitle">Listado activo de gestiones técnicas.</h6>
                  <%--  <br />--%>
                    <div class="card-body">
                        <div class="row col-7">
                            <label class="col-2 col-form-label">Búsqueda</label>
                            <div class="col-8">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" class="form-control text-uppercase" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged"></asp:TextBox>
                            </div>
                            <asp:LinkButton ID="BtnNuevo" runat="server" title="Agregar" Text="Agregar" style="background-color:#D9272E;  color: #ffffff;" class="btn" OnClick="BtnNuevo_Click">
                                        <i class="mdi mdi-plus text-white mr-2"></i>Agregar
                            </asp:LinkButton>
                        </div>

                        <div class="table-responsive m-t-20">
                            <asp:GridView ID="GVBusqueda" runat="server"
                                CssClass="table table-bordered"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows" HeaderStyle-HorizontalAlign="center"
                                AutoGenerateColumns="false" OnRowCommand="GVBusqueda_RowCommand"
                                AllowPaging="true" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                                GridLines="None"
                                PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="idTipoGestion" HeaderText="No." ItemStyle-HorizontalAlign="center" />
                                    <asp:BoundField DataField="nombreGestion" HeaderText="Tipo Gestión" />
                                    <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" Visible="true" title="Editar" runat="server" class="btn btn-info" CommandArgument='<%# Eval("idTipoGestion") %>' CommandName="EditarGestion">
                                                <i class="icon-pencil" ></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="ModalGestiones" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" style="display: none;">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacion">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbIdGestion" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelModal" runat="server">
                        <ContentTemplate>
                            <div class="row col-12">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-1">
                                            <label class="col-form-label">Gestión</label>
                                        </div>
                                        <div class="col-10">
                                            <asp:TextBox ID="TxGestion" class="form-control text-uppercase" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-1">
                                            <asp:Button ID="BtnAddTeams" runat="server" Text="+" class="btn btn-info" OnClick="BtnAddTeams_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" runat="server" id="divTeams">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Equipo</label>
                                        </div>
                                        <div class="col-9">
                                            <asp:ListBox runat="server" ID="LBTeams" CssClass="select2 form-control custom-select" name="states[]" multiple="multiple" Style="width: 100%" SelectionMode="Multiple" Rows="10"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row col-12">
                                <div class="col-12 grid-margin stretch-card">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel runat="server" ID="UpTeamsGestiones" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GvGestionesTeams" runat="server"
                                                    CssClass="table table-bordered" 
                                                    PagerStyle-CssClass="pgr" 
                                                    HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                    RowStyle-CssClass="rows"
                                                    AutoGenerateColumns="false" 
                                                    AllowPaging="true" OnRowCommand="GvGestionesTeams_RowCommand"
                                                    GridLines="None"  OnPageIndexChanging="GvGestionesTeams_PageIndexChanging"
                                                    PageSize="5"  >
                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="Id" Visible="false"  />
                                                        <asp:BoundField DataField="idTeams" HeaderText="Id Teams" ItemStyle-HorizontalAlign="center"  ControlStyle-Width="10%" />
                                                        <asp:BoundField DataField="nombre" HeaderText="Teams" ControlStyle-Width="60%" />
                                                        <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LbEliminar" class="btn btn-danger mr-2" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>'>                                           
                                            <i class="mdi mdi-delete" ></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color: #D9272E; justify-content: center">
                                <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertencia"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptar" runat="server" Text="Aceptar" class="btn" style="background-color:#D9272E;  color: #ffffff;" OnClick="BtnAceptar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalAddTeams" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" style="display: none;">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <h4 class="modal-title" id="ModalLabelConfirmar">
                                <b>
                                    <asp:Label runat="server" ID="LbTitulo" CssClass="col-form-label"></asp:Label></b>
                            </h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="DdlTeams" CssClass="select2 form-control custom-select" AutoPostBack="true" Style="width: 100%" OnSelectedIndexChanged="DdlTeams_SelectedIndexChanged"></asp:DropDownList>
                            <br />
                            <br />
                            <div class="col-12" runat="server" id="divTeamsSeleccionado" visible="false" style="display: flex; background-color: #D9272E; justify-content: center">
                                <asp:Label runat="server" CssClass="col-form-label text-white" ID="lbAdvertenciaTeams"></asp:Label>
                            </div>
                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAnadirTeams" runat="server" Text="Añadir" class="btn" style="background-color:#D9272E;  color: #ffffff;" OnClick="BtnAnadirTeams_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
    <script src="../../js/select2.js"></script>
    <link href="../../css/select2.css" rel="stylesheet" />
</asp:Content>
