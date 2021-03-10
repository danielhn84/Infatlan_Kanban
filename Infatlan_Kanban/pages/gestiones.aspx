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

    <script type="text/javascript">
        function openModal() { $('#ModalGestiones').modal('show'); }
        function cerrarModal() { $('#ModalGestiones').modal('hide'); }

        function openModalTeams() { $('#ModalAddTeams').modal('show'); }
        function cerrarModalTeams() { $('#ModalAddTeams').modal('hide'); }
    </script>

   <link href="../assets/node_modules/select2/dist/css/select2.css" rel="stylesheet" />




 <style>
.switch {
  position: relative;
  display: inline-block;
  width: 90px;
  height: 36px;
}

.switch input {display:none;}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ca2222;
  -webkit-transition: .4s;
  transition: .4s;
  border-radius: 6px;
}

.slider:before {
  position: absolute;
  content: "";
  height: 30px;
  width: 32px;
  top: 2px;
  left: 1px;
  right: 1px;
  bottom: 1px;
  background-color: white;
  transition: 0.4s;
  border-radius: 6px;
}

input:checked + .slider {
  background-color: #2ab934;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(55px);
}

.slider:after {
  content:'No';
  color: white;
  display: block;
  position: absolute;
  transform: translate(-50%,-50%);
  top: 50%;
  left: 50%;
  font-size: 10px;
  font-family: Verdana, sans-serif;
}
input:checked + .slider:after {
  content:'Si';
}
</style>
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
            <img src="../images/bannerTexto.JPG" />
            <%--<h4 class=" text-dark">Kanban Board | Gestiones Técnicas</h4>--%>
        </div>
        <div class="col-md-6">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                <li class="breadcrumb-item"><a href="javascript:void(0)">Configuraciones</a></li>
                <li class="breadcrumb-item active">Gestiones</li>
            </ol>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">

                    <h4 class="card-title">Tipo de Gestiones</h4>
                    <h6 class="card-subtitle">Listado activo de gestiones técnicas.</h6>

                    <div class="card-body" style="zoom:75%">
                        <div class="row col-12">
                            <label class="col-1 col-form-label">Búsqueda</label>
                            <div class="col-7">
                                <asp:TextBox runat="server" PlaceHolder="Ingrese texto y presione Enter" ID="TxBusqueda" class="form-control text-uppercase" AutoPostBack="true" OnTextChanged="TxBusqueda_TextChanged"></asp:TextBox>
                            </div>
                            <asp:LinkButton ID="BtnNuevo" runat="server" title="Agregar" Text="Agregar"  Style="background-color: #00468c; color: #ffffff;"  class="btn" OnClick="BtnNuevo_Click">
                                         <i class="fa fa-plus-circle text-white mr-2"></i>Nueva Gestión
                            </asp:LinkButton>

                        </div>

                        <div class="table-responsive m-t-20">
                            <asp:GridView ID="GVBusqueda" runat="server"
                                CssClass="table  table-hover table-sm"
                                PagerStyle-CssClass="pgr"
                                HeaderStyle-CssClass="table"
                                RowStyle-CssClass="rows" HeaderStyle-HorizontalAlign="center"
                                AutoGenerateColumns="false" OnRowCommand="GVBusqueda_RowCommand"
                                AllowPaging="true" OnPageIndexChanging="GVBusqueda_PageIndexChanging"
                                GridLines="None"
                                PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="idTipoGestion" HeaderText="No." ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombreGestion" HeaderText="Gestión" />
                                    <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="13%" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnEditar" Visible="true" title="Editar" runat="server" class="btn" Style="background-color: #F1961B; color: #ffffff;" CommandArgument='<%# Eval("idTipoGestion") %>' CommandName="EditarGestion">
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

    <div class="modal fade" id="ModalGestiones" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" style="display: none;">
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
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanelModal" runat="server">
                        <ContentTemplate>
                            <div class="row col-12">
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-3">
                                            <label class="col-form-label">Gestión</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="TxGestion" class="form-control text-uppercase" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-1">
                                            <asp:Button ID="BtnAddTeams" runat="server" Text="+" Style="background-color: #00468c; color: #ffffff;" class="btn" OnClick="BtnAddTeams_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" runat="server" id="divTeams">
                                    <div class="form-group row">
                                        <div class="col-3">
                                            <label class="col-form-label">Equipo</label>
                                        </div>
                                        <div class="col-8">
                                            <asp:ListBox runat="server" ID="LBTeams" CssClass="select2 form-control custom-select" name="states[]" multiple="multiple" Style="width: 100%" SelectionMode="Multiple" Rows="10"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12">
                                    <div class="form-group row ">
                                        <div class="col-3">
                                            <label class="col-form-label">Libre de validación </label>
                                        </div>
                                         <div class="col-8">
                                       
                                        <label class="switch">
                                            <input type="checkbox" runat="server" id="togBtn">
                                            <div class="slider"></div>
                                        </label>
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
                                                    CssClass="table table-hover table-sm"
                                                    PagerStyle-CssClass="pgr" 
                                                    HeaderStyle-CssClass="table" HeaderStyle-HorizontalAlign="center"
                                                    RowStyle-CssClass="rows"
                                                    AutoGenerateColumns="false" 
                                                    AllowPaging="true" OnRowCommand="GvGestionesTeams_RowCommand"
                                                    GridLines="None"  OnPageIndexChanging="GvGestionesTeams_PageIndexChanging"
                                                    PageSize="5"  >
                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="Id" Visible="false"  />
                                                        <asp:BoundField DataField="idTeams" HeaderText="Id Equipo" ItemStyle-HorizontalAlign="center"  ControlStyle-Width="10%" />
                                                        <asp:BoundField DataField="nombre" HeaderText="Equipo" ControlStyle-Width="60%" />
                                                        <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LbEliminar" class="btn btn-primary mr-2" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>'>                                           
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

                            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="btn btn-secondary"  OnClick="BtnCancelar_Click" />
                            <asp:Button ID="BtnAceptar" runat="server" Text="Enviar" class="btn" Style="background-color: #00468c; color: #ffffff;" OnClick="BtnAceptar_Click" />

                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <%--MODAL DE CONFIRMACION--%>
    <div class="modal fade" id="ModalAddTeams"  role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" data-backdrop="static" style="display: none;">
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
                            <asp:DropDownList runat="server" ID="DdlTeams" CssClass="select2 form-control custom-select" style="width: 100%"  AutoPostBack="true"  OnSelectedIndexChanged="DdlTeams_SelectedIndexChanged"></asp:DropDownList>
                            <br />
                            <br />
                            <div class="col-12 text-center" runat="server" id="divTeamsSeleccionado" visible="false" style="display: flex; background-color: #00468c; justify-content: center">
                                <asp:Label runat="server" CssClass="col-form-label text-white" ID="lbAdvertenciaTeams"></asp:Label>
                            </div>
                        
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <asp:Button ID="BtnAnadirTeams" runat="server" Text="Añadir" class="btn" style="background-color:#00468c;  color: #ffffff;" OnClick="BtnAnadirTeams_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
        <%--COMBO BUSCADOR--%>

    <script src="../assets/node_modules/select2/dist/js/select2.js"></script>

    <style>
        .select2-selection__rendered {line-height: 31px !important;}
        .select2-container .select2-selection--single {height: 35px !important;}
        .select2-selection__arrow {height: 34px !important;}
    </style>



   
</asp:Content>
