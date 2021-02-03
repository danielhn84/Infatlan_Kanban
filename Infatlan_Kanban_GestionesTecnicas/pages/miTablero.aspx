<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="miTablero.aspx.cs" Inherits="Infatlan_Kanban_GestionesTecnicas.pages.miTablero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
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
                    <li class="breadcrumb-item active">Mi Tablero</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3">
            <div class="card">
                <div class="card-header" role="tab" id="EnCola">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseEnCola" aria-expanded="true" aria-controls="collapseOne13">
                        <div class="box text-center"  style="background-color:#D9272E;  color: #ffffff;">
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

        <div class="col-md-3">
            <div class="card">
                <div class="card-header" role="tab" id="EnEjecucion">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseEnEjecucion" aria-expanded="true" aria-controls="collapseOne13">
                        <div class="box text-center" style="background-color:#D9272E;  color: #ffffff;">
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

        <div class="col-md-3">
            <div class="card">
                <div class="card-header" role="tab" id="Atrasados">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseAtrasados" aria-expanded="true" aria-controls="collapseOne13">
                        <div class="box text-center" style="background-color:#D9272E;  color: #ffffff;">
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

        <div class="col-md-3">
            <div class="card">
                <div class="card-header" role="tab" id="Completados">
                    <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseCompletados" aria-expanded="true" aria-controls="collapseOne13">
                        <div class="box text-center" style="background-color:#D9272E;  color: #ffffff;">
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
