<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="Infatlan_Kanban.pages.usuarios" %>
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
        function openModal() { $('#ModalUsuario').modal('show'); }
        function cerrarModal() { $('#ModalUsuario').modal('hide'); }

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

<%--    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="card-title"><strong>Kanban Board Gestiones Técnicas </strong></h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Gestiones Técnicas</a></li>
                    <li class="breadcrumb-item active">Usuarios</li>
                </ol>
            </div>
        </div>
    </div>--%>
   
    <div class="row page-titles"  >

        <div class="col-md-12">
            <h4 class=" text-dark">Kanban Board | Gestiones Técnicas</h4>
        </div>
        <div class="col-md-6">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                <li class="breadcrumb-item"><a href="javascript:void(0)">Configuraciones</a></li>
                <li class="breadcrumb-item active">Usuarios</li>
            </ol>
        </div>
        <br />
    </div>



    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Usuarios</h4>
                    <h6 class="card-subtitle">Listado activo de usuarios pertenecientes al mismo equipo de trabajo.</h6>
                   <%-- <br />--%>
                    <div class="card-body">
                        <div class="row col-12">
                            <label class="col-1 col-form-label">Búsqueda</label>
                            <div class="col-7">
                                <asp:TextBox runat="server" PlaceHolder="Búsqueda por equipo o código empleado y presione Enter" ID="TxBusqueda" class="form-control text-uppercase" AutoPostBack="true"  OnTextChanged="TxBusqueda_TextChanged"></asp:TextBox>
                            </div>
                            <asp:LinkButton ID="BtnNuevo" runat="server" title="Agregar" Text="Agregar" Style="background-color: #00468c; color: #ffffff;" class="btn" OnClick="BtnNuevo_Click" >
                                        <i class="fa fa-plus-circle text-white mr-2"></i>Agregar Usuario
                            </asp:LinkButton>
                        </div>

                        <div class="table-responsive m-t-20">
                            <asp:GridView ID="GVBusqueda" runat="server"
                                CssClass="table  table-hover table-sm"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows" HeaderStyle-HorizontalAlign="center"
                                AutoGenerateColumns="false"
                                AllowPaging="true"  OnRowCommand="GVBusqueda_RowCommand"
                                GridLines="None" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                                PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="idEmpleado" HeaderText="No." ItemStyle-HorizontalAlign="center" Visible="false" />
                                    <asp:BoundField DataField="teams" HeaderText="Equipo"  ItemStyle-HorizontalAlign="center"/>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="email" HeaderText="Correo"  />
                                    <asp:BoundField DataField="adUser" HeaderText="Usuario"  ItemStyle-HorizontalAlign="center" />
                                    <asp:BoundField DataField="codEmpleado" HeaderText="Codigo" ItemStyle-HorizontalAlign="center"  />
                                    <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" Visible="true" title="Editar" runat="server" class="btn btn-cyan" CommandArgument='<%# Eval("idEmpleado") %>' CommandName="EditarEmpleado">
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
    <div class="modal fade" id="ModalUsuario" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" style="display: none;">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ModalLabel">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="LbTituloModal" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelModal" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12" runat="server" id="DivColaborador">
                                    <div class="form-group row">

                                        <div class="col-1">
                                            <label class="col-form-label">Equipo</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:DropDownList ID="DdlEquipo" runat="server" class="form-control" AutoPostBack="true"  OnSelectedIndexChanged="DdlEquipo_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-2">
                                            <label class="col-form-label">Colaborador</label>
                                        </div>
                                        <div class="col-4">
                                            <asp:DropDownList ID="DdlColaborador" runat="server" class="form-control"  AutoPostBack="true"  OnSelectedIndexChanged="DdlColaborador_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-1">
                                            <label class="col-form-label">Nombre</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:TextBox ID="TxNombre" class="form-control text-uppercase" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>

                                        <div class="col-1">
                                            <label class="col-form-label">Correo</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:TextBox ID="TxCorreo" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-1">
                                            <label class="col-form-label">Codigo</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:TextBox ID="TxCodigo" class="form-control text-uppercase" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>

                                        <div class="col-1">
                                            <label class="col-form-label">User</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:TextBox ID="TxUsuario" class="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-1">
                                            <label class="col-form-label">Estado</label>
                                        </div>
                                        <div class="col-5">
                                            <asp:DropDownList runat="server" ID="DdlEstadoUsuario" CssClass="form-control" AutoPostBack="true" >
                                                <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-2">
                                            <label class="col-form-label">Color Tarjeta</label>
                                        </div>
                                        <div class="col-4">
                                             <asp:TextBox runat="server" ID="TxColor" AutoPostBack="true" class="form-control" TextMode="Color" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-1">
                                            <label class="col-form-label">Rol</label>
                                        </div>
                                        <div class="col-11">
                                           <asp:DropDownList ID="DdlRol" runat="server" class="form-control"  AutoPostBack="true"  ></asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                            </div>


                            <div class="col-12" runat="server" id="DivMensaje" visible="false" style="display: flex; background-color: #00468c; justify-content: center">
                                <asp:Label runat="server" CssClass="col-form-label text-white" ID="LbAdvertencia"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdateModificacionBotones" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAceptar" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnAceptar_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
