<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="teams.aspx.cs" Inherits="Infatlan_Kanban_GestionesTecnicas.pages.teams" %>
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
        function openModal() { $('#ModalTeams').modal('show'); }
        function cerrarModal() { $('#ModalTeams').modal('hide'); }
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
                    <li class="breadcrumb-item active">Equipos de Trabajo</li>
                </ol>
            </div>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Equipos de Trabajos</h4>
                    <h6 class="card-subtitle">Listado equipos de trabajos activos.</h6>
                    <div class="card-body">
                        <div class="row col-7">
                            <label class="col-2 col-form-label">Búsqueda</label>
                            <div class="col-8">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" AutoPostBack="true" class="form-control text-uppercase" OnTextChanged="TxBusqueda_TextChanged"></asp:TextBox>
                            </div>
                            <asp:LinkButton ID="BtnNuevo" runat="server" title="Agregar" Text="Agregar" Style="background-color: #D9272E; color: #ffffff;" class="btn" OnClick="BtnNuevo_Click">
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
                                    <asp:BoundField DataField="idTeams" HeaderText="No." ItemStyle-HorizontalAlign="center" />
                                    <asp:BoundField DataField="nombre" HeaderText="Equipo Trabajo" />
                                    <asp:BoundField DataField="wip" HeaderText="WIP" ItemStyle-HorizontalAlign="center" />
                                    <asp:BoundField DataField="nombreJefe" HeaderText="Jefe" ItemStyle-HorizontalAlign="center" />
                                    <asp:BoundField DataField="hrInicio" HeaderText="Hr Inicio" ItemStyle-HorizontalAlign="center" />
                                    <asp:BoundField DataField="hrFin" HeaderText="Hr Fin" ItemStyle-HorizontalAlign="center" />
                                    <asp:TemplateField HeaderText="Seleccione" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" Title="Editar" Visible="true" runat="server" class="btn btn-info" CommandArgument='<%# Eval("idTeams") %>' CommandName="EditarTeams">
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
    
    
    <%--    INICIO MODAL--%>
    <div class="modal fade" id="ModalTeams" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" style="display: none;">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabelModificacion">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbIdTeams" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelModal" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Nombre Equipo</label>
                                        </div>
                                        <div class="col-10">
                                            <asp:TextBox ID="TxTeams" class="form-control text-uppercase" runat="server" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">WIP</label>
                                        </div>
                                        <div class="col-4">
                                            <asp:TextBox ID="TxWIP" class="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                        <div class="col-1">
                                            <label class="col-form-label">Estado</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:TextBox ID="TxEstado" class="form-control" runat="server" ReadOnly="true" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Hr Inicio</label>
                                        </div>
                                        <div class="col-4">
                                            <asp:TextBox ID="TxHrInicio" class="form-control" runat="server" TextMode="Time" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-1">
                                            <label class="col-form-label">Hr Fin</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:TextBox ID="TxHrFin" class="form-control" runat="server" TextMode="Time" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12" runat="server" id="Div1">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Jefe</label>
                                        </div>
                                        <div class="col-10">
                                            <asp:DropDownList ID="DDLJefe" runat="server" class="fstdropdown-select form-control" OnSelectedIndexChanged="DDLJefe_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12" runat="server"  id="Div2">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Suplente</label>
                                        </div>
                                        <div class="col-10">
                                            <asp:DropDownList ID="DDLSuplente" runat="server" class="fstdropdown-select form-control"  OnSelectedIndexChanged="DDLSuplente_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-12" runat="server" visible="false" id="DivEstado">
                                    <div class="form-group row">
                                        <div class="col-2">
                                            <label class="col-form-label">Estado</label>
                                        </div>
                                        <div class="col-10">
                                            <asp:DropDownList runat="server" ID="DDLEstado" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLEstado_SelectedIndexChanged" >
                                                <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
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
                            <asp:Button ID="BtnAceptar" runat="server" Text="Enviar" class="btn" Style="background-color: #D9272E; color: #ffffff;" OnClick="BtnAceptar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
