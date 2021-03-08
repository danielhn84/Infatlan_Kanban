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
                //vAxis: { title: 'Minutos' },
                hAxis: { title: 'Días' },
                seriesType: 'bars',
                width: 1000,
                height: 500,
                series: { 1: { type: 'line' } }
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
                //title: 'Estado Tareas Cerradas',
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
                //title: 'Estado Tareas',
                pieHole: 0.4,
                slices: { 0: { color: '#082BF3' }, 1: { color: '#57CB07' }, 2: { color: '#F0E20B' }, 3: { color: '#F0270B' } }
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchartEstados'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = google.visualization.arrayToDataTable(<%=obtenerTipoGestion()%>);

            var options = {
                //title: 'Tipo de Gestión',
                pieHole: 0.4
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchartGestiones'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
     <script type="text/javascript">
         google.charts.load('current', {'packages': ['corechart'] });
         google.charts.setOnLoadCallback(drawVisualization);

         function drawVisualization() {
             // Some raw data (not necessarily accurate)strDatosConcat

             var data = google.visualization.arrayToDataTable(<%=obtenerCargabilidadFinalizado()%>);

             var options = {
                 title: 'Tareas en Ejecución vrs WIP',
                 //vAxis: { title: 'Minutos' },
                 hAxis: { title: 'Días' },
                 seriesType: 'bars',
                 width: 1000,
                 height: 500,
                 series: { 1: { type: 'line' } }
             };      
             var chart = new google.visualization.ComboChart(document.getElementById('chart_Finalizado'));
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

    <div class="row page-titles">
        <div class="col-md-12">
            <%-- <h4 class=" text-dark">Kanban Board | Gestiones Técnicas</h4>--%>
            <img src="../images/bannerTexto.JPG" />
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



    <div class="row p-t-20" style="zoom:80%">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-body">
                    <br />


                    <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="nav-dashboard" role="tabpanel" aria-labelledby="nav-datos-tab">
                            <div class="form-body col-md-12">

                                <br />
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home" role="tab"><span class="hidden-sm-up"><i class="fa fa-calendar-minus-o"></i></span><span class="hidden-xs-down"> Tareas en Ejecución</span></a> </li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tareasWIP" runat="server" role="tab"><span class="hidden-sm-up"><i class="fa fa-bars"></i></span><span class="hidden-xs-down"> Tareas en Ejecución vrs WIP</span></a> </li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tareasWIPFinalizadas" runat="server" role="tab"><span class="hidden-sm-up"><i class="fa fa-archive"></i></span><span class="hidden-xs-down"> Tareas Finalizadas vrs WIP</span></a> </li>
                                </ul>

                                <div class="tab-content tabcontent-border">
                                    <!--PRIMER CONTENIDO-->
                                    <div class="tab-pane active p-20" id="home" role="tabpanel">

                                        <div class="col-md-12 m-t-30" runat="server" id="divGraficoApilado" visible="false">
                                            <center>
                                         <center>
                                         <div id="columnchart_values" style="width: 1000px; height: 500px; align-items:center"></div>
                                             <center/>
                                        </div>
                                        <div class="col-md-12 m-t-30" runat="server" id="divImagenApilado" visible="false">
                                            <center>
                                   
                                            <img src="images/NoData.JPG"/>
                                        <center/>
                                        </div>

                                    </div>

                                    <div class="tab-pane  p-20" id="tareasWIP" role="tabpanel">
                                                  
                                        <div class="col-md-12 m-t-30" runat="server" id="divGraficoCargabilidad" visible="false">                                            <center>
                                         <center>  
                                         <div id="chart_div"  style="width: 1000px; height: 500px; align-items:center"></div>
                                             <center/>
                                        </div>
                                        <div class="col-md-12 m-t-30" runat="server" id="divImagenCragabilidad" visible="false">
                                            <center>
                                   
                                            <img src="images/NoData.JPG"/>
                                        <center/>
                                        </div>

                                    </div>

                                    <div class="tab-pane  p-20" id="tareasWIPFinalizadas" role="tabpanel">

                                        <div class="col-md-12 m-t-30" runat="server" id="divGraficoFinalizada" visible="false">
                                            <center>
                                         <center>  
                                         <div id="chart_Finalizado"  style="width: 1000px; height: 500px; align-items:center"></div>
                                             <center/>
                                        </div>
                                        <div class="col-md-12 m-t-30" runat="server" id="divImagenFinalizado" visible="false">
                                            <center>
                                   
                                            <img src="images/NoData.JPG"/>
                                        <center/>
                                        </div>

                                    </div>
                                </div>


                                <div class="row">

                                    <div class="col-md-4 m-t-30" runat="server" id="divGraficoEstadosCerrados" visible="false">
                                        <center>
                                         <asp:Label ID="Label1" runat="server" Text="Tarjetas Cerradas" BackColor="#00468c" ForeColor="White"  width="400" height="20"></asp:Label>
                                         <div class="col-md-12" id="donutchartCerradas" style="width: 500px; height: 400px;" ></div>
                                             <center/>
                                    </div>
                                    <div class="col-md-4 m-t-30" runat="server" id="divImagenCerradas" visible="false">
                                        <center>
                                        <asp:Label ID="Label3" runat="server" Text="Tarjetas Cerradas" BackColor="#00468c" ForeColor="White"  width="350" height="20"></asp:Label>
                                            <img src="images/NoData.JPG"/>
                                        <center/>
                                    </div>


                                    <div class="col-md-4 m-t-30" runat="server" id="divGraficoEstados" visible="false">
                                        <center>
                                         <asp:Label ID="Label2" runat="server" Text="Estados Tarjetas" BackColor="#00468c" ForeColor="White"  width="400" height="20"></asp:Label>
                                          <div class="col-md-12" style="width: 500px; height: 400px;" id="donutchartEstados"></div>
                                         <center/>
                                    </div>
                                    <div class="col-md-4 m-t-30" runat="server" id="divImagenEstados" visible="false">
                                        <center>
                                        <asp:Label ID="Label4" runat="server" Text="Estados Tarjetas" BackColor="#00468c" ForeColor="White"  width="350" height="20"></asp:Label>
                                            <img src="images/NoData.JPG"/>
                                        <center/>
                                    </div>


                                    <div class="col-md-4 m-t-30" runat="server" id="divGraficoGestiones" visible="false">
                                        <center>
                                         <asp:Label ID="Label5" runat="server" Text="Tipo Gestiones" BackColor="#00468c" ForeColor="White"  width="400" height="20"></asp:Label>
                                          <div class="col-md-12" id="donutchartGestiones" style="width: 500px; height: 400px;"></div>
                                         <center/>
                                    </div>

                                    <div class="col-md-4 m-t-30" runat="server" id="divImagenGestiones" visible="false">
                                        <center>
                                        <asp:Label ID="Label6" runat="server" Text="Estados Tarjetas" BackColor="#00468c" ForeColor="White"  width="350" height="20"></asp:Label>
                                            <img src="images/NoData.JPG"/>
                                        <center/>
                                    </div>





                                   

                                </div>

                            </div>
                        </div>




                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
