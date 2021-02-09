<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Infatlan_Kanban._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
        <link href="dist/css/style.min.css" rel="stylesheet">
    <!-- page css -->
    <link href="dist/css/pages/ribbon-page.css" rel="stylesheet">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

  <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProgress1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>

        <link href="dist/css/pages/tab-page.css" rel="stylesheet">
    <link href="../dist/css/pages/tab-page.css" rel="stylesheet" />
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=obtenerCargabilidadApilado()%>);

            var options = {
                //width: 600,
                //height: 400,
                title: 'Tareas en Ejecución vrs WIP',
                vAxis: { title: 'Minutos' },
                hAxis: { title: 'Días' },
                //legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '75%' },
                isStacked: true,
            };
            var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values"));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawVisualization);

        function drawVisualization() {
            // Some raw data (not necessarily accurate)strDatosConcat
            var data = google.visualization.arrayToDataTable(<%=obtenerCargabilidad()%>);

            var options = {
                title: 'Tareas en Ejecución vrs WIP',
                vAxis: { title: 'Minutos' },
                hAxis: { title: 'Días' },
                seriesType: 'bars',
                width: 1450,
                height: 500,
                series: { 15: { type: 'line' } }
            };

            var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=obtenerTareasCerradas()%>);

            var options = {
                title: 'Estado Tareas Cerradas',
                pieHole: 0.4,
                slices: { 0: { color: '#57CB07' }, 1: { color: 'red' } }
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchartCerradas'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);

        //var contenido = "['Task', 'Hours per Day'],['Work', 11],['Eat', 2],['Commute', 2],['Watch TV', 2],['Sleep', 7]";
        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=obtenerEstados()%>);

            var options = {
                title: 'Estado Tareas',
                pieHole: 0.4,
                slices: { 0: { color: '#082BF3' }, 1: { color: '#57CB07' }, 2: { color: '#F0E20B' }, 3: { color: '#F0270B' } }
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=obtenerTipoGestion()%>);

            var options = {
                title: 'Tipo de Gestión',
                pieHole: 0.4
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchart1'));
            chart.draw(data, options);
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

<%--        <div class="row page-titles">
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
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                    <contenttemplate>                
                        <asp:Button ID="BtnBusqueda" class="btn btn-success d-none d-lg-block m-l-15" runat="server" Text="Búsqueda" OnClick="BtnBusqueda_Click"  /> 
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>--%>


                    <div class="row page-titles">
                    <div class="col-md-12">
                        <h4 class=" text-dark">Kanban Board | Gestiones Técnicas</h4>
                    </div>
                    <div class="col-md-6">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0)">Inicio</a></li>
                            <li class="breadcrumb-item active">Dashboard</li>
                        </ol>
                    </div>
                </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" runat="server" id="DivBusqueda" visible="false">
                <div class="col">
                    <div class="card">
                        <div class="card-header" role="tab" id="Busqueda">
                            <a class="link" data-toggle="collapse" data-parent="#accordion2" href="#collapseBusqueda" aria-expanded="true" aria-controls="collapseOne13">
                                <div class="box" style="background-color: #D9272E; color: #ffffff; opacity: 0.9;">
                                    <h6 class="text-white"><i>
                                        <img src="../images/busqueda.png" width="30" /></i> Sección de Búsqueda</h6>
                                </div>
                            </a>
                        </div>
                        <div id="collapseBusqueda" class="collapse show" role="tabpanel" aria-labelledby="Busqueda">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-4">
                                        <label class="control-label">Tipo Búsqueda:</label>
                                        <asp:DropDownList ID="DdlBusqueda" runat="server" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Personal"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Equipo Trabajo"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Colaborador"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-4" runat="server" id="divEquipoTrabajo" visible="true">
                                        <label class="control-label">Equipo de Trabajo:</label>
                                        <asp:DropDownList runat="server" ID="DropDownList1" CssClass="select2 form-control custom-select" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="col-4" runat="server" id="divColaborador" visible="true">
                                        <label class="control-label">Colaborador:</label>
                                        <asp:DropDownList runat="server" ID="DropDownList2" CssClass="select2 form-control custom-select" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

<%--    <div class="row page-titles">
        <div class="col-md-5 align-self-center">
            <h4 class="card-title"><strong>Kanban Board Gestiones Técnicas </strong></h4>
        </div>
        <div class="col-md-7 align-self-center text-right">
            <div class="d-flex justify-content-end align-items-center">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Módulos</a></li>
                    <li class="breadcrumb-item"><a href="javascript:void(0)">Gestiones Técnicas</a></li>
                    <li class="breadcrumb-item active">Dashboard</li>
                </ol>
            </div>
        </div>
    </div>--%>

    <div class="row p-t-20">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <br />


                    <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="nav-dashboard" role="tabpanel" aria-labelledby="nav-datos-tab">
                            <div class="form-body col-md-12">
                              <%--  <br />
                                <div class="row p-t-20">
                                    <div class="col-1">
                                        <label class="col-form-label">Inicio:</label>
                                    </div>
                                    <div class="col-5">
                                        <asp:TextBox ID="TxFechaInicio" AutoPostBack="true" runat="server" TextMode="Date" class="form-control" OnTextChanged="TxFechaInicio_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-1">
                                        <label class="col-form-label">Fin:</label>
                                    </div>

                                    <div class="col-5">

                                        <asp:TextBox ID="TxFechaEntrega" AutoPostBack="true" runat="server" TextMode="Date" class="form-control" OnTextChanged="TxFechaEntrega_TextChanged"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <br />

    <%--                             <div class="vtabs customvtab">
                                    <ul class="nav nav-tabs customtab2" role="tablist">
                                    <li class="nav-item"> <a class="nav-link active" data-toggle="tab" href="#home7" role="tab"><span class="hidden-sm-up"><i class="ti-home"></i></span> <span class="hidden-xs-down">Home</span></a> </li>
                                    <li class="nav-item"> <a class="nav-link" data-toggle="tab" href="#profile7" role="tab"><span class="hidden-sm-up"><i class="ti-user"></i></span> <span class="hidden-xs-down">Profile</span></a> </li>
                                    <li class="nav-item"> <a class="nav-link" data-toggle="tab" href="#messages7" role="tab"><span class="hidden-sm-up"><i class="ti-email"></i></span> <span class="hidden-xs-down">Messages</span></a> </li>
                                </ul>

                   
                                </div>--%>

                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-calendar-minus-o"></i></span><span class="hidden-xs-down">Tareas en Ejecución</span></a> </li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tareasWIP" runat="server" role="tab"><span class="hidden-sm-up"><i class="fa fa-bars"></i></span><span class="hidden-xs-down">Tareas vrs WIP</span></a> </li>
                                </ul>

                                <div class="tab-content tabcontent-border">
                                    <!--PRIMER CONTENIDO-->
                                    <div class="tab-pane active p-20" id="home" role="tabpanel">
                                        <div class="row p-t-20">
                                            <div class="col-lg-12">
                                                <center>
                                             <div id="columnchart_values" style="width: 1450px; height: 500px; align-items:center"></div>
                                            <center/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane  p-20" id="tareasWIP" role="tabpanel">
                                        <div class="row">
                                            <div class="col-12">
                                                <center>
                                                    <div id="chart_div" style="width: 1450px; height: 500px; align-items:center"></div>
                                               <center/>
                                            </div>
                                        </div>
                                    </div>

                                </div>


                                <div class="row">
                                    <div class="col-md-4 m-t-30" id="donutchartCerradas" style="width: 500px; height: 400px;"></div>
                                    <div class="col-md-4 m-t-30" id="donutchart" style="width: 500px; height: 400px;"></div>
                                    <div class="col-md-4 m-t-30" id="donutchart1" style="width: 500px; height: 400px;"></div>
                                </div>

                            </div>
                        </div>



                        <div class="row">
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
