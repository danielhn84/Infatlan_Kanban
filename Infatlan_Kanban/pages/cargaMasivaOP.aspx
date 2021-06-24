<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="cargaMasivaOP.aspx.cs" Inherits="Infatlan_Kanban.pages.cargaMasivaOP" %>
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
        <div class="col-md-12">
            <img src="../images/TextoBlanco.png" height="20" />
        </div>
        <div class="col-md-6">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                <li class="breadcrumb-item active">Carga Masiva Operativas</li>
            </ol>
        </div>
    </div>


      <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">Tarjetas Operativas</h4>
                    <h6 class="card-subtitle">Creación Masiva de Tarjetas Operativas</h6>
                    <div class="card-body" style="zoom:75%">
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
                            <asp:Button ID="BtnCargar" runat="server" Text="Cargar" class="btn btn-success"  />
                        </ContentTemplate>

                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnCargar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                </div>
            </div>

            
        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
