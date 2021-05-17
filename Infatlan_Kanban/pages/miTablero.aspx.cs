using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban.classes;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;


namespace Infatlan_Kanban.pages
{
    public partial class miTablero : System.Web.UI.Page
    {
        db vConexionGestiones = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }


        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //}

            protected void Page_Load(object sender, EventArgs e)
        {
            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            select2();
            String request = Request.QueryString["et"];
            String requestColaborador = Request.QueryString["vColaborador"];
            if (!Page.IsPostBack)
            {

                String vEx = Request.QueryString["ex"];
                if (Convert.ToBoolean(Session["AUTH"]))
                {

                    string vIdRol = Session["ID_ROL_USUARIO"].ToString();

                    if (vIdRol == "2")
                    {
                        DdlTipoBusqueda.Visible = false;
                        BtnBusqueda.Visible = false;
                    }
                    else
                    {
                        DdlTipoBusqueda.Visible = true;
                        BtnBusqueda.Visible = true;
                    }
                    

                    Session["GESTIONES_ID_TARJETA_CERRAR"] = null;
                    if (!String.IsNullOrEmpty(request))
                    {
                        cargarDataEquipoTrabajo(request);
                        //cargarDataTareasAsignadas(request);
                        tipoBusqueda();
                        DivBusquedaReportes.Visible = true;
                        DdlTipoBusqueda.SelectedValue = "2";
  
                        if (DdlTipoBusqueda.SelectedValue == "2")//Reporte Equipo Trabajo
                        {
                            DdlEquipoTrabajo.Visible = true;
                            DdlColaborador.Visible = false;

                            if (vIdRol == "1")
                            {
                                DdlEquipoTrabajo.Items.Clear();
                                String vQuery = "GESTIONES_Solicitud 27,'" + Session["USUARIO"].ToString() + "'";
                                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                                DdlEquipoTrabajo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                                if (vDatos.Rows.Count > 0)
                                {
                                    foreach (DataRow item in vDatos.Rows)
                                    {
                                        DdlEquipoTrabajo.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["nombre"].ToString() });
                                    }
                                }
                            }
                            else if (vIdRol == "3" || vIdRol == "4" || vIdRol == "5")
                            {
                                DdlEquipoTrabajo.Items.Clear();
                                String vQuery = "GESTIONES_Solicitud 25";
                                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                                DdlEquipoTrabajo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                                if (vDatos.Rows.Count > 0)
                                {
                                    foreach (DataRow item in vDatos.Rows)
                                    {
                                        DdlEquipoTrabajo.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["nombre"].ToString() });
                                    }
                                }
                            }
                        }
                        select2();
                        DdlEquipoTrabajo.SelectedValue = request;
                        UpdatePanel17.Update();
                    }
                    else
                    {
                        cargarInicialTarjeta();
                        cargarData();
                        tipoBusqueda();
                    }



                    if (!String.IsNullOrEmpty(requestColaborador))
                    {
                        cargarDataColaborador(requestColaborador);
                        tipoBusqueda();
                        DivBusquedaReportes.Visible = true;
                        DdlTipoBusqueda.SelectedValue = "3";

                        if (DdlTipoBusqueda.SelectedValue == "3")//Reporte Colaborador
                        {
                            DdlEquipoTrabajo.Visible = false;
                            DdlColaborador.Visible = true;

                            if (vIdRol == "1")
                            {
                                DdlColaborador.Items.Clear();
                                String vQuery = "GESTIONES_Solicitud 28,'" + Session["USUARIO"].ToString() + "'";
                                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                                DdlColaborador.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                                if (vDatos.Rows.Count > 0)
                                {
                                    foreach (DataRow item in vDatos.Rows)
                                    {
                                        DdlColaborador.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                                    }
                                }
                            }
                            else if (vIdRol == "3" || vIdRol == "4" || vIdRol == "5")
                            {
                                DdlColaborador.Items.Clear();
                                String vQuery = "GESTIONES_Solicitud 26";
                                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                                DdlColaborador.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                                if (vDatos.Rows.Count > 0)
                                {
                                    foreach (DataRow item in vDatos.Rows)
                                    {
                                        DdlColaborador.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                                    }
                                }
                            }
                        }
                        DdlColaborador.SelectedValue = requestColaborador;
                        UpdatePanel17.Update();

                    }
                    }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
          
            //if (!Page.IsPostBack)
            //{


                //    //if (vEx == null)
                //    //{
                //    //    cargarData();
                //    //    string vMensaje = "Tarjeta creado con éxito";
                //    //    Mensaje(vMensaje, WarningType.Success);
                //    //}
                //    //else if (vEx.Equals("1"))
                //    //{
                //    //    String vRe = "Tarjeta finalizada con éxito.";
                //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                //    //    cargarData();
                //    //}
                //    //else if (vEx.Equals("2"))
                //    //{
                //    //    string  vMensaje = "Tarjeta creado con éxito";
                //    //    Mensaje(vMensaje, WarningType.Success);
                //    //    cargarData();
                //    //}

                //}


        }
        private void select2()
        {
            String vScript = @"
                    $(function test() {
                        $('.select2').select2();
                        $('.ajax').select2({
                            ajax: {
                                url: 'https://api.github.com/search/repositories',
                                dataType: 'json',
                                delay: 250,
                                data: function (params) {
                                    return {
                                        q: params.term, // search term
                                        page: params.page
                                    };
                                },
                                processResults: function (data, params) {
                                    params.page = params.page || 1;
                                    return {
                                        results: data.items,
                                        pagination: {
                                            more: (params.page * 30) < data.total_count
                                        }
                                    };
                                },
                                cache: true
                            },
                            escapeMarkup: function (markup) {
                                return markup;
                            },
                            minimumInputLength: 1,
                        });
                    });
                    ";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "select2", vScript, true);
        }
        void cargarData()
        {

            string vQuery = "GESTIONES_Generales 2,'" + Session["USUARIO"].ToString() + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnCola = vDatos.Rows[0]["cantCola"].ToString();

            vQuery = "GESTIONES_Generales 3,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnEjecucion = vDatos.Rows[0]["cantEjecucion"].ToString();

            vQuery = "GESTIONES_Generales 4,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vCompletadasHoy = vDatos.Rows[0]["cantCompletadasHoy"].ToString();

            vQuery = "GESTIONES_Generales 5,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vAtrasado = vDatos.Rows[0]["cantAtrasado"].ToString();

            vQuery = "GESTIONES_Generales 40,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vDetenidas = vDatos.Rows[0]["cantDetenidas"].ToString();

            LbEnCola.Text = vEnCola;
            LbEjecucion.Text = vEnEjecucion;
            LbCompletados.Text = vCompletadasHoy;
            LbAtrasados.Text = vAtrasado;
            LbDetenidas.Text = vDetenidas;
            string vString = "";
            string vTest = "";
            vQuery = "GESTIONES_Generales 21,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = "", vTipoTarjeta = "", vRibon = "", vMin = "" ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vMin = vDatos.Rows[i]["minSolicitud"].ToString();

                if (vDatos.Rows[i]["usuarioCreo"].ToString() == "00000")
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA OPERATIVA";
                    vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP</div>";
                    //vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP<i class='fa fa-check-circle'></i></div>";
                }
                else
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA TECNICA";
                    vRibon = "";
                }


                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                //style = 'zoom: 60%;'
                vString += "<div class='card' >" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                vRibon +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-clock-o'></i>  MINUTOS: " + vMin + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +

                "<div class='col-12 text-center'><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:13px'>" + vTipoTarjeta + "</h6>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEnCola.Text = vString;
            LitEnCola.Text = vTest;



            //SOLICITUDES EN EJECUCIÓN
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 22,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = "", vTipoTarjeta="", vRibon="", vMin = "";
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vMin = vDatos.Rows[i]["minSolicitud"].ToString();

                if (vDatos.Rows[i]["usuarioCreo"].ToString() == "00000")
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA OPERATIVA";
                    vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP</div>";
                    //vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP<i class='fa fa-check-circle'></i></div>";
                }
                else
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA TECNICA";
                    vRibon = "";
                }
                //vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }

                //style = 'zoom: 60%;'
                vString += "<div class='card' >" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                vRibon+
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-clock-o'></i>  MINUTOS: " + vMin + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:13px'>" + vTipoTarjeta + "</h6>" +
                //"<h6 class='card-subtitle mb-2 text-muted' style='font-size:13px'><i class='fa fa-drivers-license-o'></i> <span class='label label-" + vColorHeader + "'>" + vTipoTarjeta + "</span></h6>" +
                //"<h6 class='card-subtitle mb-2 text-muted' style='font-size:13px'> <i class='fa fa-drivers-license-o'></i> <span class='label label-" + vColorHeader + "'>" + vTipoTarjeta + "</span></h6>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<div class='ribbon ribbon-success ribbon - right'>Ribbon</div>"+

                 //"<div class='ribbon ribbon-bookmark ribbon-vertical-l ribbon-info'><i class='fa fa-heart'></i></div>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEjecucion.Text = vString;
            LitEnEjecucion.Text = vTest;


            //SOLICITUDES ATRASADAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 23,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = "", vTipoTarjeta = "", vRibon = "", vMin = "";
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vMin = vDatos.Rows[i]["minSolicitud"].ToString();

                //vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["usuarioCreo"].ToString() == "00000")
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA OPERATIVA";
                    vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP</div>";
                    //vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP<i class='fa fa-check-circle'></i></div>";
                }
                else
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA TECNICA";
                    vRibon = "";
                }


                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                //style = 'zoom: 60%;'
                vString += "<div class='card' >" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                vRibon +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-clock-o'></i>  MINUTOS: " + vMin + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:13px'>" + vTipoTarjeta + "</h6>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesAtrasadas.Text = vString;
            LitAtrasados.Text = vTest;

            //SOLICITUDES COMPLETADOS HOY
            vString = "";
            vTest = "";
            DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            vQuery = "GESTIONES_Generales 24,'" + Session["USUARIO"].ToString() + "','" + vfechaActualCorta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vEstadoNombre = "", vColorEstado = "", vFechaInicio = "", vColorHeader = "", vTipoTarjeta = "", vRibon = "", vMin = "";
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vEstadoNombre = vDatos.Rows[i]["estado"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vMin = vDatos.Rows[i]["minSolicitud"].ToString();

                //vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                if (vDatos.Rows[i]["usuarioCreo"].ToString() == "00000")
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA OPERATIVA";
                    vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP</div>";
                    //vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP<i class='fa fa-check-circle'></i></div>";
                }
                else
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA TECNICA";
                    vRibon = "";
                }

                if (vEstadoNombre == "Realizado a Tiempo")
                { vColorEstado = "success";
                    //BtnConfirmarTarea_1.Visible = false;
                }
                else
                {

                    vColorEstado = "danger";
                    //BtnConfirmarTarea_1.Visible = false;
                }


                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }
                //style = 'zoom: 60%;'
                vString += "<div class='card' >" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                vRibon +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-clock-o'></i>  MINUTOS: " + vMin + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'><br>" +
                "<h5><span class='label label-" + vColorEstado + "'>" + vEstadoNombre + "</span></h5><br>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
               "<h6 class='card-subtitle mb-2 text-muted' style='font-size:13px'>" + vTipoTarjeta + "</h6>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";


                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;

            }
            LitNotificacionesCompletadosHoy.Text = vString;
            LitCompletadosHoy.Text = vTest;



            //SOLICITUDES DETENIDAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 41,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = "", vTipoTarjeta = "", vRibon = "", vMin = "";
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vMin = vDatos.Rows[i]["minSolicitud"].ToString();
                //vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                if (vDatos.Rows[i]["usuarioCreo"].ToString() == "00000")
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA OPERATIVA";
                    vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP</div>";
                    //vRibon = " <div class='ribbon ribbon-info ribbon-vertical-r'>OP<i class='fa fa-check-circle'></i></div>";
                }
                else
                {
                    vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                    vTipoTarjeta = " TARJETA TECNICA";
                    vRibon = "";
                }


                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }

                //style = 'zoom: 75%;'
                vString += "<div class='card' >" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                vRibon +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-clock-o'></i>  MINUTOS: " + vMin + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:13px'>" + vTipoTarjeta + "</h6>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesDetenidas.Text = vString;
            LitDetenidas.Text = vTest;
        }
        void tipoBusqueda()
        {
            DdlTipoBusqueda.Items.Clear();
            String vQuery = "GESTIONES_Solicitud 24,'" + Session["ID_ROL_USUARIO"].ToString() + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            DdlTipoBusqueda.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    DdlTipoBusqueda.Items.Add(new ListItem { Value = item["nombre"].ToString(), Text = item["reporte"].ToString() });
                }
            }



            DdlNotificar.Items.Clear();
            vQuery = "GESTIONES_Solicitud 26";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            DdlNotificar.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    DdlNotificar.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                }
            }

        }
        void cargarInicialTarjeta()
        {
            try
            {
                string vIdRol = Session["ID_ROL_USUARIO"].ToString();
                TxFechaSolicitud.Text = Convert.ToString(DateTime.Now);


                String vQuery = "";
                if (vIdRol == "1")
                {
                    DdlResponsable.Items.Clear();
                    DdlResponsable_1.Items.Clear();
                    vQuery = "GESTIONES_Solicitud 28,'" + Session["USUARIO"].ToString() + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    DdlResponsable_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlResponsable.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                            DdlResponsable_1.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }

                }
                else if (vIdRol == "3" || vIdRol == "4" || vIdRol == "5")
                {
                    DdlResponsable.Items.Clear();
                    DdlResponsable_1.Items.Clear();
                    vQuery = "GESTIONES_Solicitud 26";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    DdlResponsable_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlResponsable.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                            DdlResponsable_1.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                }
                else if (vIdRol == "2")
                {
                    DdlResponsable.Items.Clear();
                    DdlResponsable_1.Items.Clear();
                    vQuery = "GESTIONES_Generales 38,'" + Session["USUARIO"].ToString() + "'";
                    DataTable vDatosResponsables = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    DdlResponsable_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatosResponsables.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatosResponsables.Rows)
                        {
                            DdlResponsable.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                            DdlResponsable_1.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                }


                DdlMotivoEliminar.Items.Clear();
                vQuery = "GESTIONES_Solicitud 29";
                DataTable vDatosEliminarTarjeta = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlMotivoEliminar.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

                if (vDatosEliminarTarjeta.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosEliminarTarjeta.Rows)
                    {
                        DdlMotivoEliminar.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["motivo"].ToString() });
                    }
                }




                //    String vQuery = "GESTIONES_Generales 37,'" + Session["USUARIO"].ToString() + "','" + Session["USUARIO"].ToString() + "'";
                //DataTable vDatosResponsables = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                //DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                //DdlResponsable_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                //if (vDatosResponsables.Rows.Count > 0)
                //{
                //    foreach (DataRow item in vDatosResponsables.Rows)
                //    {
                //        DdlResponsable.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                //        DdlResponsable_1.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                //    }
                //}
                //else
                //{
                //    vQuery = "GESTIONES_Generales 38,'" + Session["USUARIO"].ToString() + "'";
                //    vDatosResponsables = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                //    DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                //    DdlResponsable_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                //    if (vDatosResponsables.Rows.Count > 0)
                //    {
                //        foreach (DataRow item in vDatosResponsables.Rows)
                //        {
                //            DdlResponsable.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                //            DdlResponsable_1.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void BtnAddTarjeta_Click(object sender, EventArgs e)
        {
            LbTituloCrear.Text = "Crear Tarjeta Kanban Técnica";
            UpdatePanel3.Update();
            UPFormulario.Update();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCrearOpen();", true);
        }
        protected void btnTickectEvento(object sender, EventArgs e)
        {
            string vDato = "";
        }
        protected void BtnAddComentario_Click(object sender, EventArgs e)
        {
            try
            {
                string vUsuarioAD = Session["USUARIO_AD"].ToString();

                divAlertaComentario.Visible = false;
                string vEx = "";

                if (Session["GESTIONES_ID_TARJETA_CERRAR"] == null)
                {
                    vEx = "";
                }
                else
                {
                    vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();
                }

                if (vEx != null && vEx != "")
                {
                    if (TxComentario.Text == "" || TxComentario.Text == string.Empty)
                        throw new Exception("El campo de comentario está vacío.");

                    string usuario = vUsuarioAD + ' ' + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    String vQuery = "GESTIONES_Solicitud 2,'" + vEx + "','" + TxComentario.Text + "','" + usuario + "'";
                    Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    string vCambio = "Usuario agrego nuevo comentario a la tarjeta. Comentario: " + TxComentario.Text;
                    string vCambioSuscripcion = "Buen día, se notifica que el usuario: " + vUsuarioAD + ", agrego nuevo comentario a la tarjeta: " + vEx + "-" + TxTitulo.Text + ", Comentario: " + TxComentario.Text;

                    vQuery = "GESTIONES_Solicitud 4,'" + vEx + "','" + vCambio + "','" + vUsuarioAD + "'";
                    Int32 vInfo4 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    //DATOS COMENTARIOS
                    vQuery = "GESTIONES_Solicitud 14,'" + vEx + "'";
                    DataTable vDatosComentarios = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                    if (vDatosComentarios.Rows.Count > 0)
                    {
                        divComentarioLectura.Visible = true;
                        GvComentarioLectura.DataSource = vDatosComentarios;
                        GvComentarioLectura.DataBind();
                        Session["GESTIONES_TAREAS_COMENTARIOS"] = vDatosComentarios;
                    }
                    else
                    {
                        divComentarioLectura.Visible = false;
                        divAlertaComentario.Visible = true;
                    }

                    //ACTUALIZAR DATOS HISTORIAL
                    vQuery = "GESTIONES_Solicitud 15,'" + vEx + "'";
                    DataTable vDatosHistorial = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                    if (vDatosHistorial.Rows.Count > 0)
                    {
                        GvHistorial.DataSource = vDatosHistorial;
                        GvHistorial.DataBind();
                    }


                    vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vUsuarioCreo = vDatos.Rows[0]["usuarioCreo"].ToString();
                    string vEmailResponsable = vDatos.Rows[0]["emailResponsable"].ToString();
                    string vEmailUsuarioCreo = vDatos.Rows[0]["emailCreo"].ToString();
                    string vResponsable = vDatos.Rows[0]["responsable"].ToString();

                    if (vUsuarioCreo == vResponsable)
                    {
                        string vAsunto = "Nuevo Comentario Tarjeta Kanban, Gestión Técnica: " + vEx;
                        string vTituloSuscripcion = "Gestión Técnica, Nuevo Comentario Tarjeta Kanban";
                        string vQuery5 = "GESTIONES_Solicitud 5,'" + vTituloSuscripcion + "','"
                                        + vEmailUsuarioCreo
                                        + "','" + vEmailResponsable
                                        + "','" + vAsunto + "','" + vCambioSuscripcion + "', '0','" + vEx + "'";
                        Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);
                    }
                    TxComentario.Text = "";
                    vEx = "";
                    Session["GESTIONES_ID_TARJETA_CERRAR"] = null;

                }
                else
                {

                    if (TxComentario.Text == "" || TxComentario.Text == string.Empty)
                        throw new Exception("El campo de comentario está vacío.");

                    string hora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    int numRows = 0;
                    if (Session["GESTIONES_TAREAS_COMENTARIOS"] == null)
                    {
                        numRows = 0;
                    }
                    else
                    {
                        numRows = GvComentario.Rows.Count;
                    }
                    

                    DataTable vData = new DataTable();
                    DataTable vDatos = new DataTable();
                    vData.Columns.Add("idComentario");
                    vData.Columns.Add("usuario");
                    vData.Columns.Add("comentario");

                    string vConteo = "";
                    if (numRows>0)
                    {
                        
                        vDatos = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                        //vData.Columns.Add("idComentario");
                        //vData.Columns.Add("usuario");
                        //vData.Columns.Add("comentario");

                        
                        Session["GESTIONES_CONTEO_COMENTARIO"] = Convert.ToInt32(Session["GESTIONES_CONTEO_COMENTARIO"]) + 1;
                        vConteo = Session["GESTIONES_CONTEO_COMENTARIO"].ToString();
                        //if (vDatos != null)
                        //{
                        //    if (vDatos.Rows.Count < 1)
                        //    {
                                vDatos.Rows.Add(vConteo, vUsuarioAD + ' ' + hora, TxComentario.Text);
                            //}
                            //else
                            //{
                            //    vDatos.Rows.Add(vdd, vUsuarioAD + ' ' + hora, TxComentario.Text);
                            //}
                       


                    }else
                    {

                        //if (vDatos == null)
                        vDatos = vData.Clone();

                        Session["GESTIONES_CONTEO_COMENTARIO"] = "1";
                        vConteo = "1";
                        vDatos.Rows.Add(vConteo, vUsuarioAD + ' ' + hora, TxComentario.Text);

                    }



                    
                    //DataTable vData = new DataTable();
                    //DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                    //vData.Columns.Add("idComentario");
                    //vData.Columns.Add("usuario");
                    //vData.Columns.Add("comentario");




                    GvComentario.DataSource = vDatos;
                    GvComentario.DataBind();
                    Session["GESTIONES_TAREAS_COMENTARIOS"] = vDatos;
                    divComentario.Visible = true;
                    TxComentario.Text = "";
                }
            }
            catch (Exception ex)
            {
                LbAlertaComentario.InnerText = ex.Message;
                divAlertaComentario.Visible = true;
            }
        }
        protected void GvComentario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvComentario.PageIndex = e.NewPageIndex;
                GvComentario.DataSource = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                GvComentario.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GvComentario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    string vId = e.CommandArgument.ToString();
                    if (Session["GESTIONES_TAREAS_COMENTARIOS"] != null)
                    {
                        DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                        DataRow[] result = vDatos.Select("idComentario = '" + vId + "'");
                        foreach (DataRow row in result)
                        {
                            if (row["idComentario"].ToString().Contains(vId))
                            {
                                //LLENAR TABLA DE DATOS A ELIMINAR
                                vDatos.Rows.Remove(row);
                            }
                        }

                        GvComentario.DataSource = vDatos;
                        GvComentario.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void TxComentario_TextChanged(object sender, EventArgs e)
        {
            divAlertaComentario.Visible = false;
        }
        protected void DdlResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable.SelectedValue + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
                Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

                DdlTipoGestion.Items.Clear();
                DdlTipoGestion.Enabled = true;
                vQuery = "GESTIONES_Generales 1,'" + Session["GESTIONES_TEAMS"].ToString() + "'";
                DataTable vDatosTipo = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlTipoGestion.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatosTipo.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosTipo.Rows)
                    {
                        DdlTipoGestion.Items.Add(new ListItem { Value = item["idTipoGestion"].ToString(), Text = item["nombreGestion"].ToString() });
                    }
                }

                vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        private void validacionesCrearSolicitud()
        {
            if (TxTitulo_1.Text.Equals(""))
                throw new Exception("Falta que ingrese el título de la tarea.");

            if (TxDescripcion.Text.Equals(""))
                throw new Exception("Falta que ingrese la descripción de la tarea.");

            if (DdlResponsable.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione responsable.");

            if (TxMinProductivo.Text.Equals(""))
                throw new Exception("Falta que ingrese tiempo productivo en (min).");

            if (DdlTipoGestion.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione tipo de gestión.");

            if (TxFechaEntrega.Text.Equals(""))
                throw new Exception("Falta que ingrese la fecha de entrega.");

            if (DdlPrioridad.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione tipo de prioridad.");



            string vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable.SelectedValue + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vTeams = vDatos.Rows[0]["idTeams"].ToString();
            Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
            Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

            vQuery = "GESTIONES_Generales 58,'" + vTeams + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vCantTeamsAtrasadas = vDatos.Rows[0]["tareasAtrasadas"].ToString();



            vQuery = "GESTIONES_Generales 59,'" + DdlTipoGestion.SelectedValue + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vlibreValidacion = vDatos.Rows[0]["validacion"].ToString();
            string vgestionValidacion = "";
            if (vlibreValidacion == "Si")
            {
                vgestionValidacion = "Si";
            }
            else
            {
                vgestionValidacion = "";
            }



            vQuery = "GESTIONES_Generales 5,'" + DdlResponsable.SelectedValue + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vCantAtrasados = vDatos.Rows[0]["cantAtrasado"].ToString();
            if (Convert.ToInt32(vCantAtrasados) >= Convert.ToInt32(vCantTeamsAtrasadas) && vgestionValidacion == "")
                throw new Exception("Límite establecido de tareas atrasadas: 5, y usted actualmete tiene: " + vCantAtrasados + ", favor finalizar las tarjetas para que pyuedan asignarles nuevas.");


            vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
            Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();

            Session["GESTIONES_HR_INICIO"] = vDatos.Rows[0]["hrInicio"].ToString();
            Session["GESTIONES_HR_FIN"] = vDatos.Rows[0]["hrFin"].ToString();
            Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();


            DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
            DateTime fecha_fin = DateTime.Parse(TxFechaEntrega.Text.ToString());
            string vWip = Session["GESTIONES_WIP"].ToString();

            string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
            string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");

            string vHrInicioSoli = fecha_inicio.ToString("HH:mm");
            string vHrFinSolic = fecha_fin.ToString("HH:mm");

            string vHrIniTeams = Session["GESTIONES_HR_INICIO"].ToString();
            string[] valuesIniTeams = vHrIniTeams.Split(':');

            string vHrFinTeams = Session["GESTIONES_HR_FIN"].ToString();
            string[] valuesFinTeams = vHrFinTeams.Split(':');

            TimeSpan vThrInicioTeams = new TimeSpan(Convert.ToInt32(valuesIniTeams[0]), Convert.ToInt32(valuesIniTeams[1]), 0);
            TimeSpan vThrFinTeams = new TimeSpan(Convert.ToInt32(valuesFinTeams[0]), Convert.ToInt32(valuesFinTeams[1]), 0);


            string vHrIniSoli = vHrInicioSoli;
            string[] valuesIniSoli = vHrIniSoli.Split(':');

            string vHrFinSoli = vHrFinSolic;
            string[] valuesFinSoli = vHrFinSoli.Split(':');

            TimeSpan vThrInicioSoli = new TimeSpan(Convert.ToInt32(valuesIniSoli[0]), Convert.ToInt32(valuesIniSoli[1]), 0);
            TimeSpan vThrFinSoli = new TimeSpan(Convert.ToInt32(valuesFinSoli[0]), Convert.ToInt32(valuesFinSoli[1]), 0);

            //if (vThrInicioSoli < vThrInicioTeams && DdlTipoGestion.SelectedValue != "4" && fecha_inicio.DayOfWeek != DayOfWeek.Saturday && fecha_inicio.DayOfWeek != DayOfWeek.Sunday)
            //    throw new Exception("Favor cambiar la hora de inicio de la tarjeta, está superando la hora de inicio establecido " + vThrInicioTeams);

            //if (vThrFinSoli > vThrFinTeams && DdlTipoGestion.SelectedValue != "4" && fecha_fin.DayOfWeek != DayOfWeek.Saturday && fecha_fin.DayOfWeek != DayOfWeek.Sunday)
            //    throw new Exception("Favor cambiar la hora de entrega de la tarjeta, está superando la hora de fin establecido " + vThrFinTeams);


            DateTime fecha_actual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            TimeSpan fecha_actual_resta10min = new TimeSpan(Convert.ToDateTime(fecha_actual).Ticks);
            fecha_actual_resta10min = fecha_actual_resta10min.Add(new TimeSpan(0, -10, 0));
            String fecha_actual_resta = Convert.ToString(fecha_actual_resta10min); //Original Text
            String[] result = fecha_actual_resta.Split('.');
            string vHr = result[1].ToString();

            string fecha_actual_corta = fecha_actual.ToString("dd/MM/yyyy");
            string fecha_actual_corta_masresta = fecha_actual.ToString("dd/MM/yyyy") + " " + vHr;
            DateTime fecha_actual_corta_masresta_evaluar = Convert.ToDateTime(fecha_actual_corta_masresta);

            DateTime fecha_actualIngreso = Convert.ToDateTime(fecha_inicio.ToString("dd/MM/yyyy HH:mm"));
            DateTime fecha_actualEntrega = Convert.ToDateTime(fecha_fin.ToString("dd/MM/yyyy HH:mm"));

            var timeSpan = fecha_fin - fecha_inicio;
            int vdiasSolicitud = Convert.ToInt16(timeSpan.TotalDays);
            Session["GESTIONES_DIAS_SOLICITUD"] = vdiasSolicitud;


            //if (DdlTipoGestion.SelectedValue != "4" && DdlTipoGestion.SelectedValue != "15" && fecha_actualIngreso < fecha_actual_corta_masresta_evaluar)  //VALIDACION DIFERENTE A INCIDENTES QUE NO PODRAN INGRESARLA A CUALQUIER HORA           
            if (vgestionValidacion == "" && fecha_actualIngreso < fecha_actual_corta_masresta_evaluar)  //VALIDACION DIFERENTE A INCIDENTES QUE NO PODRAN INGRESARLA A CUALQUIER HORA      
                throw new Exception("Nota: El tipo de gestión que seleccionó no permite ingresar tarjetas cuando la fecha actual del sistema es mayor que la fecha de inicio de la tarjeta. Tarjeta tenia que ser debidamente programada");

            if (fecha_actualIngreso > fecha_actualEntrega)
                throw new Exception("Favor verificar la fecha de inicio, no puede ser mayor que la fecha de entrega");

            if (fecha_actualEntrega < fecha_actualIngreso)
                throw new Exception("Favor verificar la fecha de entrega, no puede ser menor que la fecha de inicio");
        }
        private void cargarModal()
        {
            string vQuery = "GESTIONES_Generales 2,'" + DdlResponsable.SelectedValue + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnCola = vDatos.Rows[0]["cantCola"].ToString();

            vQuery = "GESTIONES_Generales 3,'" + DdlResponsable.SelectedValue + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnEjecucion = vDatos.Rows[0]["cantEjecucion"].ToString();

            vQuery = "GESTIONES_Generales 25,'" + DdlResponsable.SelectedValue + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vDetenidas = vDatos.Rows[0]["cantDetenidas"].ToString();

            vQuery = "GESTIONES_Generales 5,'" + DdlResponsable.SelectedValue + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vAtrasado = vDatos.Rows[0]["cantAtrasado"].ToString();

            LbEnColaModal.Text = vEnCola;
            LbEjecucionModal.Text = vEnEjecucion;
            LbDetenidasModal.Text = vDetenidas;
            LbAtrasadoModal.Text = vAtrasado;

            LbTituloConfirmar.Text = "Información General: " + DdlResponsable.SelectedItem.ToString();
            UpTituloConfirmar.Update();
            TxTituloModal.Text = TxTitulo_1.Text;
            TxPrioridadModal.Text = DdlPrioridad.SelectedItem.ToString();
            TxTimeModal.Text = TxMinProductivo.Text + " Mins";
            TxEntregaModal.Text = TxFechaEntrega.Text;
            TxInicioModal.Text = TxFechaInicio.Text;
            UpdatePanel6.Update();
        }
        private void cargarModalDetener()
        {

            string vResponsableTarjeta = Session["GESTIONES_RESPONSABLE_TARJETA_CERRAR"].ToString();
            string vNombreResponsableTarjeta = Session["GESTIONES_NOMBRE_RESPONSABLE_TARJETA_CERRAR"].ToString();

            string vQuery = "GESTIONES_Generales 2,'" + vResponsableTarjeta + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnCola = vDatos.Rows[0]["cantCola"].ToString();

            vQuery = "GESTIONES_Generales 3,'" + vResponsableTarjeta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnEjecucion = vDatos.Rows[0]["cantEjecucion"].ToString();

            vQuery = "GESTIONES_Generales 25,'" + vResponsableTarjeta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vDetenidas = vDatos.Rows[0]["cantDetenidas"].ToString();

            vQuery = "GESTIONES_Generales 5,'" + vResponsableTarjeta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vAtrasado = vDatos.Rows[0]["cantAtrasado"].ToString();

            LbEnColaModal.Text = vEnCola;
            LbEjecucionModal.Text = vEnEjecucion;
            LbDetenidasModal.Text = vDetenidas;
            LbAtrasadoModal.Text = vAtrasado;

            LbTituloConfirmar.Text = "Información General: " + vNombreResponsableTarjeta;
            UpTituloConfirmar.Update();
            TxTituloModal.Text = TxTitulo.Text;
            TxPrioridadModal.Text = DdlPrioridad_1.SelectedItem.ToString();
            TxTimeModal.Text = TxMinProductivo_1.Text + " Mins";
            TxEntregaModal.Text = TxFechaEntrega_1.Text;
            TxInicioModal.Text = TxFechaInicio_1.Text;
            UpdatePanel6.Update();
        }
        void calculoDias()
        {


            DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
            DateTime fecha_fin = DateTime.Parse(TxFechaEntrega.Text.ToString());

            String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
            String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);

            DateTime vFechaInicio = DateTime.Parse(vFecha1);

            string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
            string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");
            DateTime vFechaInicioConver = DateTime.Parse(vFechaInicioSoli);
            DateTime vFechaFinConver = DateTime.Parse(vFechaFinSoli);

            TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
            int businessDays = span.Days;
            int fullWeekCount = businessDays / 7;

            if (businessDays == 7)
            {
                businessDays = businessDays - 2;
            }
            else if (businessDays == 6)
            {
                businessDays = businessDays - 1;
            }
            else if (businessDays > fullWeekCount * 7)
            {
                int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
                int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }
            //subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            if (businessDays < 1)
            {
                businessDays = businessDays * -1;
            }
            else
            {
                businessDays = businessDays;
            }

            int vDias = businessDays + 1;
            Session["GESTIONES_DIAS"] = vDias;
        }
        protected void BtnConfirmarTarea_Click(object sender, EventArgs e)
        {
            try
            {

                validacionesCrearSolicitud();

                GVDistribucion.DataSource = null;
                GVDistribucion.DataBind();
                Session["GESTIONES_TAREAS_MIN_DIARIOS"] = null;
                DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
                DateTime fecha_fin = DateTime.Parse(TxFechaEntrega.Text.ToString());

                String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
                String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
                String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

                DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                DateTime vFechaInicio = DateTime.Parse(vFecha1);

                double vMinDiarios = 0;
                double vWip = Convert.ToInt32(Session["GESTIONES_WIP"].ToString());

                DataTable vData = new DataTable();
                DataTable vDatosMin = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
                vData.Columns.Add("id");
                vData.Columns.Add("fecha");
                vData.Columns.Add("min");
                string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
                string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");
                DateTime vFechaInicioConver = DateTime.Parse(vFechaInicioSoli);
                DateTime vFechaFinConver = DateTime.Parse(vFechaFinSoli);

                string vHrInicialSoli = fecha_inicio.ToString("HH:mm");
                string vHrFinalSoli = fecha_fin.ToString("HH:mm");

                TimeSpan vHrInicialSoliConver = TimeSpan.Parse(vHrInicialSoli);
                TimeSpan vHrFinSoliConver = TimeSpan.Parse(vHrFinalSoli);


                if (vFechaInicio.DayOfWeek != DayOfWeek.Saturday && vFechaInicio.DayOfWeek != DayOfWeek.Sunday)
                {
                    TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
                    int businessDays = span.Days;
                    int fullWeekCount = businessDays / 7;

                    if (businessDays == 7)
                    {
                        businessDays = businessDays - 2;
                    }
                    else if (businessDays == 6)
                    {
                        businessDays = businessDays - 1;
                    }
                    else if (businessDays > fullWeekCount * 7)
                    {
                        int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
                        int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
                        if (lastDayOfWeek < firstDayOfWeek)
                            lastDayOfWeek += 7;
                        if (firstDayOfWeek <= 6)
                        {
                            if (lastDayOfWeek >= 7)
                                businessDays -= 2;
                            else if (lastDayOfWeek >= 6)
                                businessDays -= 1;
                        }
                        else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                            businessDays -= 1;

                        //subtract the weekends during the full weeks in the interval
                        businessDays -= fullWeekCount + fullWeekCount;
                    }
                    int vDias = businessDays + 1;
                    Session["GESTIONES_DIAS"] = vDias;
                    vMinDiarios = Convert.ToInt32(TxMinProductivo.Text) / vDias;

                    int vCount = 0;
                    int vResta = 0;

                    double vMinsFaltante = 0;

                    if (vDias == 1)
                    {
                        if (vDatosMin == null)
                            vDatosMin = vData.Clone();
                        if (vDatosMin != null)
                        {
                            vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                            vDatosMin.Rows.Add("1", vFechaInicioSoli, vMinDiarios);
                        }
                    }
                    else
                    {

                        //VALIDACION INICIO TAREA SI TIENE MIN DISPNIBLES
                        string vCantMinSolicitudes = "";
                        string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaInicioSoli + "'";
                        DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                        vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();

                        if (vCantMinSolicitudes != "")
                        {
                            if (Convert.ToDouble(vCantMinSolicitudes) >= Convert.ToDouble(vWip))
                                throw new Exception("Nota: La fecha seleccionada inicio de la tarjeta ya no cuenta con mins disponibles, su WIP está al limite, favor cambiar la fecha de inicio para poder realizar una mejor distribución de su cargabilidad. Minutos registrados de cargabilidad: " + vCantMinSolicitudes + ", WIP límite establecido: " + vWip);
                        }


                        for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                        {
                            if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                            {
                                vCount = vCount + 1;
                                vResta = (vDias - vCount) + 1;

                                if (vMinsFaltante != 0)
                                {
                                    vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                    vMinsFaltante = 0;
                                }

                                string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaEvaluar + "'";
                                vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                double vSobranteWIPCreacion = 0;

                                if (vCantMinSolicitudes.Equals(""))
                                {
                                    vSobranteWIPCreacion = vWip;
                                }
                                else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                {
                                    vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                }
                                else
                                {
                                    vSobranteWIPCreacion = 0;
                                }


                                if (vMinDiarios <= vSobranteWIPCreacion)
                                {
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        if (vDatosMin.Rows.Count < 1)
                                        {
                                            vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                        }
                                        else
                                        {
                                            vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                        }
                                    }
                                }
                                else
                                {
                                    vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                    vMinDiarios = vSobranteWIPCreacion;
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        if (vDatosMin.Rows.Count < 1)
                                        {
                                            vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                        }
                                        else
                                        {
                                            vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (vMinsFaltante != 0)
                        throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

                }
                else if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday || vFechaInicio.DayOfWeek == DayOfWeek.Sunday)
                {
                    calculoDias();
                    int vDias = Convert.ToInt32(Session["GESTIONES_DIAS"].ToString());
                    LbDiaNoHabil.Text = "Se debe iniciar a trabajar en la tarjeta un día de trabajo no hábil";
                    divDiaNoHabil.Visible = true;
                    if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday)
                    {
                        if (vDias == 1)
                        {
                            if (vDatosMin == null)
                                vDatosMin = vData.Clone();
                            if (vDatosMin != null)
                            {
                                vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                                vDatosMin.Rows.Add("1", vFechaInicioSoli, TxMinProductivo.Text);
                            }
                        }
                        else
                        {
                            vDias = vDias + 2;
                            vMinDiarios = Convert.ToInt32(TxMinProductivo.Text) / vDias;
                            DateTime vFechaFinConverDomingo = vFechaInicioConver.AddDays(1);
                            DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
                            int vCount = 0;
                            int vResta = 0;
                            double vMinsFaltante = 0;

                            for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
                            {
                                string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                if (vDatosMin == null)
                                    vDatosMin = vData.Clone();
                                if (vDatosMin != null)
                                {
                                    if (vDatosMin.Rows.Count < 1)
                                    {
                                        vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                    }
                                    else
                                    {
                                        vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                    }
                                }

                            }

                            for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                            {
                                if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                                {
                                    vCount = vCount + 1;
                                    vResta = (vDias - vCount) + 1;

                                    if (vMinsFaltante != 0)
                                    {
                                        vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                        vMinsFaltante = 0;
                                    }

                                    string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                    string vCantMinSolicitudes = "";
                                    string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaEvaluar + "'";
                                    DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                    vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                    double vSobranteWIPCreacion = 0;

                                    if (vCantMinSolicitudes.Equals(""))
                                    {
                                        vSobranteWIPCreacion = vWip;
                                    }
                                    else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                    {
                                        vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                    }
                                    else
                                    {
                                        vSobranteWIPCreacion = 0;
                                    }


                                    if (vMinDiarios <= vSobranteWIPCreacion)
                                    {
                                        if (vDatosMin == null)
                                            vDatosMin = vData.Clone();
                                        if (vDatosMin != null)
                                        {
                                            if (vDatosMin.Rows.Count < 1)
                                            {
                                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                            }
                                            else
                                            {
                                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                        vMinDiarios = vSobranteWIPCreacion;
                                        if (vDatosMin == null)
                                            vDatosMin = vData.Clone();
                                        if (vDatosMin != null)
                                        {
                                            if (vDatosMin.Rows.Count < 1)
                                            {
                                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                            }
                                            else
                                            {
                                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                            }
                                        }
                                    }
                                }
                            }

                            if (vMinsFaltante != 0)
                                throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);
                        }
                    }
                    else
                    {
                        vDias = vDias;
                        vMinDiarios = Convert.ToInt32(TxMinProductivo.Text) / vDias;
                        DateTime vFechaFinConverDomingo = vFechaInicioConver;
                        DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
                        int vCount = 0;
                        int vResta = 0;
                        double vMinsFaltante = 0;

                        for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
                        {
                            string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                            if (vDatosMin == null)
                                vDatosMin = vData.Clone();
                            if (vDatosMin != null)
                            {
                                if (vDatosMin.Rows.Count < 1)
                                {
                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                }
                                else
                                {
                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                }
                            }

                        }

                        for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                        {
                            if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                            {
                                vCount = vCount + 1;
                                vResta = (vDias - vCount) + 1;

                                if (vMinsFaltante != 0)
                                {
                                    vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                    vMinsFaltante = 0;
                                }

                                string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                string vCantMinSolicitudes = "";
                                string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaEvaluar + "'";
                                DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                double vSobranteWIPCreacion = 0;

                                if (vCantMinSolicitudes.Equals(""))
                                {
                                    vSobranteWIPCreacion = vWip;
                                }
                                else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                {
                                    vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                }
                                else
                                {
                                    vSobranteWIPCreacion = 0;
                                }


                                if (vMinDiarios <= vSobranteWIPCreacion)
                                {
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        if (vDatosMin.Rows.Count < 1)
                                        {
                                            vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                        }
                                        else
                                        {
                                            vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                        }
                                    }
                                }
                                else
                                {
                                    vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                    vMinDiarios = vSobranteWIPCreacion;
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        if (vDatosMin.Rows.Count < 1)
                                        {
                                            vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                        }
                                        else
                                        {
                                            vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                        }
                                    }
                                }
                            }
                        }

                        if (vMinsFaltante != 0)
                            throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

                    }




                    //if (vDiasTarjeta == 1)
                    //{
                    //    if (vDatosMin == null)
                    //        vDatosMin = vData.Clone();
                    //    if (vDatosMin != null)
                    //    {
                    //        vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                    //        vDatosMin.Rows.Add("1", vFechaInicioSoli, TxMinProductivo.Text);
                    //    }
                    //}
                }


                Session["GESTIONES_TAREAS_MIN_DIARIOS"] = vDatosMin;
                GVDistribucion.DataSource = vDatosMin;
                GVDistribucion.DataBind();

                DataTable vDatosComentarios = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                DataTable vDatosAdjuntos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];

                if (vDatosComentarios == null && vDatosAdjuntos == null)
                {
                    LbAdvertenciaModal.Text = "Nota: Los adjuntos y comentarios no son campos obligatorios. Para mayor seguridad se notifica que la tarjeta no cuenta con adjuntos ni comentarios. Si esta seguro dar clic en “Enviar”.";
                }
                else if (vDatosComentarios == null && vDatosAdjuntos != null)
                {
                    LbAdvertenciaModal.Text = "Nota: Los adjuntos y comentarios no son campos obligatorios. Para mayor seguridad se notifica que la tarjeta no cuenta con comentarios. Si esta seguro dar clic en “Enviar”.";
                }
                else if (vDatosComentarios != null && vDatosAdjuntos == null)
                {
                    LbAdvertenciaModal.Text = "Nota: Los adjuntos y comentarios no son campos obligatorios. Para mayor seguridad se notifica que la tarjeta no cuenta con adjuntos. Si esta seguro dar clic en “Enviar”.";
                }
                else
                {
                    LbAdvertenciaModal.Text = "";
                }

                if (fecha_fin <= vfechaActual)
                {
                    divSolucion.Visible = true;
                    divAdjuntoSolucion.Visible = true;
                    divTareaFinalizada.Visible = true;
                    divDiaNoHabil.Visible = false;
                    divPrioridad.Visible = false;
                }
                else
                {
                    divSolucion.Visible = false;
                    divAdjuntoSolucion.Visible = false;
                    divTareaFinalizada.Visible = false;
                    divPrioridad.Visible = true;
                }

                divComentariosAdjuntos.Visible = true;
                cargarModal();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaConfirmarOpen();", true);

            }
            catch (Exception ex)
            {
                LbAdvertencia.InnerText = ex.Message;
                divAlertaGeneral.Visible = true;
            }
        }
        protected void GVDistribucion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDistribucion.PageIndex = e.NewPageIndex;
            GVDistribucion.DataSource = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
            GVDistribucion.DataBind();
        }

        //protected void BtnBusqueda_Click(object sender, EventArgs e)
        //{
        //    DivBusqueda.Visible = true;
        //    UpdatePanel8.Update();
        //}

        private void validacionesCerrarTarea()
        {
            if (DdlAccion.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione acción a realizar.");

            if (TxDetalle.Text.Equals(""))
                throw new Exception("Falta que ingrese ingrese detalle de la acción.");





            String vEx = null;
            vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();
        }

        protected void BtnEnviarInfo_Click(object sender, EventArgs e)
        {
            try
            {
                //ccc
                //TAREA A  DETENER
                if (DdlAccion.SelectedValue == "2")
                {
                    

                    string vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();
                    string vResponsable= Session["GESTIONES_RESPONSABLE_TARJETA_CERRAR"].ToString();
                    string vidEstadoDetener = "3";
                    string vidEstadoTextoDetener = "Solicitud Detenida";
                    string  vEstadoCargabilidadDetener = "0";
                    string vCambioDetener = vidEstadoTextoDetener + ", Detalle: Nueva Fecha Inicio- " + TxNewFechaInicio.Text + ", Nueva Fecha Entrega-" + TxNewFechaEntrega.Text+ ", Motivo: "+ TxDetalle.Text;

                    DateTime vdateActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                    string vdateActualCambio = vdateActual.ToString("dd/MM/yyyy");
                    
                   
                    //ACTUALIZAR LA SOLICITUD A DETENIDO
                    DateTime fecha_entregaNew = DateTime.Parse(TxNewFechaEntrega.Text.ToString());
                    DateTime fecha_inicioNew = DateTime.Parse(TxNewFechaInicio.Text.ToString());

                    DateTime vFechaEntrega = Convert.ToDateTime(fecha_entregaNew.ToString("dd/MM/yyyy HH:mm"));
                    DateTime vFechaInicio = Convert.ToDateTime(fecha_inicioNew.ToString("dd/MM/yyyy HH:mm"));
                    string vQuery = "GESTIONES_Solicitud 34,'" + vEx + "','" + vidEstadoDetener + "','" + TxDetalle.Text + "','" + Session["USUARIO"].ToString() + "','"+ vFechaInicio+"','"+ vFechaEntrega+"'";
                    Int32 vInfo1 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    //GUARDAR HISTORIAL
                    vQuery = "GESTIONES_Solicitud 4,'" + vEx + "','" + vCambioDetener + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    vQuery = "GESTIONES_Solicitud 7,'" + vResponsable + "'";
                    DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vTeams = vDato.Rows[0]["idTeams"].ToString();
                    Session["GESTIONES_CORREO_RESPONSABLE"] = vDato.Rows[0]["email"].ToString();

                    vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                    vDato = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    Session["GESTIONES_CORREO_JEFE"] = vDato.Rows[0]["correoJefe"].ToString();
                    Session["GESTIONES_CORREO_SUPLENTE"] = vDato.Rows[0]["correoSuplente"].ToString();
                    Session["GESTIONES_NOMBRE_JEFE"] = vDato.Rows[0]["nombreJefe"].ToString();
                    Session["GESTIONES_NOMBRE_SUPLENTE"] = vDato.Rows[0]["nombreSuplente"].ToString();

                    //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                    string vAsuntoDetenida = "Tarjeta Detenida, Gestiones Técnicas: " + vEx;
                    string vCorreo = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                    string vQueryDetenida = "GESTIONES_Solicitud 5,'Tarjeta Kanban Detenida','"
                     + vCorreo
                    + "','" + Session["GESTIONES_CORREO_RESPONSABLE"].ToString() + "','" + vAsuntoDetenida + "','" + "Datos Generales Tarjeta', '0','" + vEx + "'";
                    Int32 vInfoDetenida = vConexionGestiones.ejecutarSqlGestiones(vQueryDetenida);

                    //CAMBIAR EL ESTADO DE LA CARGABILIDAD
                    //vQuery = "GESTIONES_Generales 60,'" + vEx + "','" + vdateActualCambio + "'";
                    //Int32 vInfoMenor = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    //vQuery = "GESTIONES_Generales 68,'" + vEx + "','" + vdateActualCambio + "'";
                    //Int32 vInfoMayor = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    vQuery = "GESTIONES_Generales 71,'" + vEx + "'";
                    Int32 vInfoMayor = vConexionGestiones.ejecutarSqlGestiones(vQuery);


                    DataTable vDatosCargabilidadDetenida = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
                    if (vDatosCargabilidadDetenida != null)
                    {
                        for (int num = 0; num < vDatosCargabilidadDetenida.Rows.Count; num++)
                        {
                            string correlativo = vDatosCargabilidadDetenida.Rows[num]["id"].ToString();
                            string fecha = vDatosCargabilidadDetenida.Rows[num]["fecha"].ToString();
                            string min = vDatosCargabilidadDetenida.Rows[num]["min"].ToString();

                            String vQuery6 = "GESTIONES_Solicitud 11,'" + vEx +
                                "','" + correlativo +
                                "','" + fecha +
                                "','" + min +
                                 "','" + vEstadoCargabilidadDetener +
                                 "','" + vResponsable + "'";
                            Int32 vInfo6 = vConexionGestiones.ejecutarSqlGestiones(vQuery6);
                        }
                    }


                    if (vInfo1 == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaConfirmarClose();", true);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaClose();", true);
                        Response.Redirect("/pages/miTablero.aspx");
                    }


                }
                else
                {
                //string vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();
                String vFormato = "dd/MM/yyyy HH:mm"; //"dd/MM/yyyy HH:mm:ss"
                String vFechaInicioTarea = Convert.ToDateTime(TxFechaInicio.Text).ToString(vFormato);
                DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));

                DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
                DateTime vFechaInicio = Convert.ToDateTime(fecha_inicio.ToString("dd/MM/yyyy HH:mm"));


                DateTime fecha_entrega = DateTime.Parse(TxFechaEntrega.Text.ToString());
                DateTime vFechaEntrega = Convert.ToDateTime(fecha_entrega.ToString("dd/MM/yyyy HH:mm"));

                string vidEstado = "";
                string vidEstadoFinalizadaCreacion = "";
                string vidEstadoTexto = "";
                string vtarjetaEstado = "";
                string vCambio = "";

                if (vFechaInicio <= vfechaActual && vFechaEntrega > vfechaActual)
                {
                    vidEstado = "2";
                    vidEstadoTexto = "En Ejecución";
                    vtarjetaEstado = "1";
                }
                else if (vFechaInicio > vfechaActual)
                {
                    vidEstado = "1";
                    vidEstadoTexto = "En Cola";
                    vtarjetaEstado = "0";
                }
                else if (vFechaInicio < vfechaActual && vFechaEntrega <= vfechaActual)
                {
                    //vidEstado = "5";
                    //vidEstadoTexto = "Realizada a Tiempo";
                    //vtarjetaEstado = "4";
                    vidEstadoFinalizadaCreacion = "5";
                    vidEstado = "2";
                    vidEstadoTexto = "En Ejecución";
                    vtarjetaEstado = "1";
                }
                vCambio = "Creación de tarjeta, Estado: " + vidEstadoTexto + ", Prioridad: " + DdlPrioridad.SelectedItem;

                String vQuery1 = "GESTIONES_Solicitud 1,'" + TxTitulo_1.Text
                       + "','" + TxDescripcion.Text
                       + "','" + DdlTipoGestion.SelectedValue
                       + "','" + DdlResponsable.SelectedValue
                       + "','" + DdlPrioridad.SelectedValue
                       + "','" + Session["USUARIO"].ToString()
                       + "','" + TxMinProductivo.Text
                       + "','" + vidEstado
                       + "','" + Session["GESTIONES_TEAMS"].ToString()
                       + "','" + Session["GESTIONES_DIAS"].ToString()
                       + "','" + vFechaEntrega
                       + "','" + vFechaInicio
                       + "','" + Convert.ToString(vfechaActualCorta)
                       + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery1);
                string idSolicitud = vDatos.Rows[0]["idSolicitud"].ToString();

                DataTable vDatosCargabilidad = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
                if (vDatosCargabilidad != null)
                {
                    for (int num = 0; num < vDatosCargabilidad.Rows.Count; num++)
                    {
                        string correlativo = vDatosCargabilidad.Rows[num]["id"].ToString();
                        string fecha = vDatosCargabilidad.Rows[num]["fecha"].ToString();
                        string min = vDatosCargabilidad.Rows[num]["min"].ToString();


                        String vQuery6 = "GESTIONES_Solicitud 11,'" + idSolicitud +
                            "','" + correlativo +
                            "','" + fecha +
                            "','" + min +
                             "','" + vtarjetaEstado +
                             "','" + DdlResponsable.SelectedValue + "'";
                        Int32 vInfo6 = vConexionGestiones.ejecutarSqlGestiones(vQuery6);
                    }
                }

                String vArchivoAdjunto = String.Empty;
                String vNombreArchivo = String.Empty;
                HttpPostedFile bufferDeposito = FuAdjunto.PostedFile;
                byte[] vFileDepositoAdjunto = null;
                String vExtensionAdjunto = String.Empty;

                if (FuAdjunto.HasFile)
                {
                    if (bufferDeposito != null)
                    {
                        vNombreArchivo = FuAdjunto.FileName;
                        Stream vStream = bufferDeposito.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoAdjunto = vReader.ReadBytes((int)vStream.Length);
                        vExtensionAdjunto = System.IO.Path.GetExtension(FuAdjunto.FileName);
                    }

                    if (vFileDepositoAdjunto != null)
                    {
                        vArchivoAdjunto = Convert.ToBase64String(vFileDepositoAdjunto);
                    }
                    else
                    {
                        vArchivoAdjunto = "";
                    }
                }

                if (vArchivoAdjunto != "")
                {
                    String vQuery3 = "GESTIONES_Solicitud 3,'" + idSolicitud +
    "','" + vArchivoAdjunto +
    "','" + vNombreArchivo + "'";
                    Int32 vInfo3 = vConexionGestiones.ejecutarSqlGestiones(vQuery3);
                }

                string vQuery4 = "GESTIONES_Solicitud 4,'" + idSolicitud + "','" + vCambio + "','" + Session["USUARIO_AD"].ToString() + "'";
                Int32 vInfo4 = vConexionGestiones.ejecutarSqlGestiones(vQuery4);

                string vCorreosCopia = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                string vAsunto = "Creación Tarjeta Kanban, Gestiones Técnicas: " + idSolicitud;
                string vQuery5 = "GESTIONES_Solicitud 5,'Creacion Tarjeta Kanban','"
                 + Session["GESTIONES_CORREO_RESPONSABLE"].ToString()
                + "','" + vCorreosCopia + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + idSolicitud + "'";
                Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);


                if (vidEstadoFinalizadaCreacion == "5")
                {
                    string vsolucion = "";
                    if (TxSolucion.Text.Equals(""))
                    {
                        vsolucion = "Tarjeta finalizada con éxito";
                    }
                    else
                    {
                        vsolucion = TxSolucion.Text;
                    }

                    vidEstado = "5";
                    vidEstadoTexto = "Realizada a Tiempo";
                    vtarjetaEstado = "4";

                    string vEstadoCargabilidad = "4";
                    vCambio = "Finalizar Tarjeta, Estado: " + vidEstadoTexto + ", Detalle: " + vsolucion;

                    String vNombreDepot1 = String.Empty;
                    HttpPostedFile bufferDeposito1T = FuSolucion_Cerrar.PostedFile;
                    byte[] vFileDeposito1 = null;
                    String vExtension = String.Empty;

                    if (bufferDeposito1T != null)
                    {
                        vNombreDepot1 = FuSolucion_Cerrar.FileName;
                        Stream vStream = bufferDeposito1T.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDeposito1 = vReader.ReadBytes((int)vStream.Length);
                        vExtension = System.IO.Path.GetExtension(FuSolucion.FileName);
                    }
                    String vArchivo = String.Empty;
                    if (vFileDeposito1 != null)
                        vArchivo = Convert.ToBase64String(vFileDeposito1);
                    //ACTUALIZAR LA SOLICITUD
                    string vQuery = "GESTIONES_Solicitud 16,'" + idSolicitud + "','" + vidEstado + "','" + vsolucion + "','" + Session["USUARIO"].ToString() + "','" + vArchivo + "'";
                    Int32 vInfo1 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    //GUARDAR HISTORIAL
                    vQuery = "GESTIONES_Solicitud 4,'" + idSolicitud + "','" + vCambio + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable.SelectedValue + "'";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                    Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();

                    vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                    Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();
                    Session["GESTIONES_NOMBRE_JEFE"] = vDatos.Rows[0]["nombreJefe"].ToString();
                    Session["GESTIONES_NOMBRE_SUPLENTE"] = vDatos.Rows[0]["nombreSuplente"].ToString();

                    //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                    vAsunto = "Tarjeta Kanban Finalizada, Gestiones Técnicas: " + idSolicitud;
                    vCorreosCopia = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                    vQuery5 = "GESTIONES_Solicitud 5,'Tarjeta Kanban Finalizada','"
                        + Session["GESTIONES_CORREO_RESPONSABLE"].ToString()
                    + "','" + vCorreosCopia + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + idSolicitud + "'";
                    Int32 vInfo6 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);


                    //CAMBIAR EL ESTADO DE LA CARGABILIDAD
                    vQuery = "GESTIONES_Solicitud 22,'" + idSolicitud + "','" + Session["USUARIO"].ToString() + "','" + vEstadoCargabilidad + "'";
                    Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    TxSolucion.Text = "";
                    divComentariosAdjuntos.Visible = false;
                    UpdatePanel6.Update();

                }

                DataTable vDatosComentarios = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                if (vDatosComentarios != null)
                {
                    for (int num = 0; num < vDatosComentarios.Rows.Count; num++)
                    {
                        string usuario = vDatosComentarios.Rows[num]["usuario"].ToString();
                        string comentario = vDatosComentarios.Rows[num]["comentario"].ToString();

                        String vQuery2 = "GESTIONES_Solicitud 2,'" + idSolicitud +
                            "','" + comentario +
                            "','" + usuario + "'";
                        Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery2);
                    }
                }


                if (vInfo5 == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaConfirmarClose();", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaClose();", true);
                    Response.Redirect("/pages/miTablero.aspx");
                }
            }

            }
            catch (Exception ex)
            {
                LbCamposVacios.Text = ex.Message;
                divCamposVacios.Visible = true;
                Response.Redirect("/pages/miTablero.aspx");
                //UpdatePanel6.Update();
            }

        }

        void camposDeshabilitados()
        {
            TxFechaSolicitud_1.ReadOnly = true;
            TxTitulo.ReadOnly = true;
            TxDescripcion_1.ReadOnly = true;
            DdlResponsable_1.Enabled = false;
            TxMinProductivo_1.ReadOnly = true;
            TxFechaInicio_1.ReadOnly = true;
            TxFechaEntrega_1.ReadOnly = true;
            DdlPrioridad_1.Enabled = false;
            DdlTipoGestion_1.Enabled = false;


        }

        void cargarDatosTarjeta()
        {
            camposDeshabilitados();
            tabAdjuntos.Visible = true;
            String vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();

            //DATOS GENERALES
            string vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            if (vDatos.Rows[0]["idEstado"].ToString() == "4")
            {
                DdlAccion.SelectedValue = "1";
                DdlAccion.Enabled = false;
            }

            string vFormato = "yyyy-MM-ddTHH:mm";
            string vFechaInicio = vDatos.Rows[0]["fechaInicio"].ToString();
            string vFechaFin = vDatos.Rows[0]["fechaEntrega"].ToString();
            TxFechaSolicitud_1.Text = vDatos.Rows[0]["fechaEnvio"].ToString();
            TxTitulo.Text = vDatos.Rows[0]["titulo"].ToString();
            TxDescripcion_1.Text = vDatos.Rows[0]["descripcion"].ToString();
            DdlResponsable_1.SelectedValue = vDatos.Rows[0]["responsable"].ToString();
            TxMinProductivo_1.Text = vDatos.Rows[0]["minSolicitud"].ToString();
            TxFechaInicio_1.Text = Convert.ToDateTime(vFechaInicio).ToString(vFormato);
            TxFechaEntrega_1.Text = Convert.ToDateTime(vFechaFin).ToString(vFormato);
            DdlPrioridad_1.SelectedValue = vDatos.Rows[0]["prioridad"].ToString();
            string vEstadoTarjeta = vDatos.Rows[0]["idEstado"].ToString();
            string idOperativa= vDatos.Rows[0]["idOperativa"].ToString();
            

            Session["GESTIONES_USUARIO_CREO"] = vDatos.Rows[0]["usuarioCreo"].ToString();

            vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable_1.SelectedValue + "'";
            DataTable vDatosTeams = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vTeams = vDatosTeams.Rows[0]["idTeams"].ToString();
            
            DdlTipoGestion_1.Items.Clear();
            DdlTipoGestion_1.Enabled = true;
            vQuery = "GESTIONES_Generales 63,'" + vTeams + "'";
            DataTable vDatosTipo = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            DdlTipoGestion_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
            if (vDatosTipo.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosTipo.Rows)
                {
                    DdlTipoGestion_1.Items.Add(new ListItem { Value = item["idTipoGestion"].ToString(), Text = item["nombreGestion"].ToString() });
                }
            }


            DdlTipoGestion_1.SelectedValue = vDatos.Rows[0]["idTipoGestion"].ToString();
            Session["GESTIONES_EMAIL_RESPONSABLE"] = vDatos.Rows[0]["emailResponsable"].ToString();
            Session["GESTIONES_EMAIL_CREO"] = vDatos.Rows[0]["emailCreo"].ToString();

            //DATOS ADJUNTOS
            vQuery = "GESTIONES_Solicitud 13,'" + vEx + "'";
            DataTable vDatosAdjuntos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            if (vDatosAdjuntos.Rows.Count > 0)
            {
                divAdjuntoLectura.Visible = true;
                GvAdjuntoLectura.DataSource = vDatosAdjuntos;
                GvAdjuntoLectura.DataBind();
                Session["GESTIONES_ADJUNTOS_LECTURA"] = vDatosAdjuntos;
            }
            else
            {
                divAdjuntoLectura.Visible = false;
                divAlertaNoAdjunto.Visible = true;
            }

            //DATOS COMENTARIOS
            vQuery = "GESTIONES_Solicitud 14,'" + vEx + "'";
            DataTable vDatosComentarios = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            if (vDatosComentarios.Rows.Count > 0)
            {
                divComentarioLectura.Visible = true;

                GvComentarioLectura.DataSource = vDatosComentarios;
                GvComentarioLectura.DataBind();
                Session["GESTIONES_TAREAS_COMENTARIOS"] = vDatosComentarios;

            }
            else
            {
                LbAlertaComentario.InnerText = "Tarjeta no cuenta con comentarios, si desea puede adicionar";
                divComentarioLectura.Visible = false;
                divAlertaComentario.Visible = true;
            }

            //DATOS HISTORIAL
            vQuery = "GESTIONES_Solicitud 15,'" + vEx + "'";
            DataTable vDatosHistorial = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            if (vDatosHistorial.Rows.Count > 0)
            {
                GvHistorial.DataSource = vDatosHistorial;
                GvHistorial.DataBind();
                Session["GESTIONES_COMENTARIOS_HISTORIAL"] = vDatosHistorial;
            }



            if (vEstadoTarjeta == "5" || vEstadoTarjeta == "6")
            {
                DdlAccion.SelectedValue = "1";
                TxDetalle.Text = vDatos.Rows[0]["detalleFinalizo"].ToString();
                TxDetalle.ReadOnly = true;
                DdlAccion.Enabled = false;
                divSolucionAdjunto.Visible = false;
                divComentarioAdd.Visible = false;
                BtnConfirmarTarea_1.Visible = false;
                //UpdatePanel14.Update();


                LbTitulo.Text = "Tarjeta Kanban Cerrada: " + Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();
                UpTitulo.Update();
            }



            DdlMotivoEliminar.Items.Clear();
            vQuery = "GESTIONES_Solicitud 30";
            DataTable vDatosEliminarTarjeta = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            DdlMotivoEliminar.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

            if (vDatosEliminarTarjeta.Rows.Count > 0)
            {
                foreach (DataRow item in vDatosEliminarTarjeta.Rows)
                {
                    DdlMotivoEliminar.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["motivo"].ToString() });
                }
            }


            //DATOS CHECKLIST
            vQuery = "GESTIONES_SolicitudOperaciones 5,'" + idOperativa + "'";
            DataTable vDatosVerificacion = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            if (vDatosVerificacion.Rows.Count > 0)
            {
                GvCheckList.DataSource = vDatosVerificacion;
                GvCheckList.DataBind();
                Session["GESTIONES_VERIFICACION"] = vDatosVerificacion;
            }


        }
        protected void TxTitulo_TextChanged(object sender, EventArgs e)
        {
            string vidTarjeta = TxTitulo.Text;
            LbTitulo.Text = "Cerrar Tarjeta Kanban: " + vidTarjeta;
            UpTitulo.Update();

            DdlResponsable_1.Items.Clear();
            string vQuery = "GESTIONES_Solicitud 33";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            DdlResponsable_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    DdlResponsable_1.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                }
            }



            vQuery = "GESTIONES_Solicitud 12,'" + vidTarjeta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vidOperativa = vDatos.Rows[0]["idOperativa"].ToString();


            Session["GESTIONES_ID_TARJETA_CERRAR"] = vidTarjeta;
            cargarDatosTarjeta();
            tabAdjuntos.Visible = true;
            DdlTipoGestion_1.Enabled = false;

            
            if (vidOperativa=="")
            {
                tabVerificacion.Visible = false;
            }

            UPFormulario.Update();
        }

        protected void TxTituloVer_TextChanged(object sender, EventArgs e)
        {
            string vidTarjeta = TxTitulo.Text;

        }
        protected void GvComentarioLectura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvComentarioLectura.PageIndex = e.NewPageIndex;
                GvComentarioLectura.DataSource = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                GvComentarioLectura.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GvAdjuntoLectura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvAdjuntoLectura.PageIndex = e.NewPageIndex;
                GvAdjuntoLectura.DataSource = (DataTable)Session["GESTIONES_ADJUNTOS_LECTURA"];
                GvAdjuntoLectura.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GvHistorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvHistorial.PageIndex = e.NewPageIndex;
                GvHistorial.DataSource = (DataTable)Session["GESTIONES_COMENTARIOS_HISTORIAL"];
                GvHistorial.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnCancelarTarjeta_Click(object sender, EventArgs e)
        {
            limpiarCreacionTarea();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCrearClose();", true);
            cargarInicialTarjeta();
            cargarData();
            Response.Redirect("/pages/miTablero.aspx");
        }


        private void limpiarCreacionTarea()
        {
            TxTitulo_1.Text = string.Empty;
            TxFechaSolicitud.Text = string.Empty;
            TxDescripcion.Text = string.Empty;
            DdlResponsable.SelectedIndex = -1;
            TxMinProductivo.Text = string.Empty;
            TxFechaInicio.Text = string.Empty;
            TxFechaEntrega.Text = string.Empty;
            DdlPrioridad.SelectedIndex = -1;
            DdlTipoGestion.SelectedIndex = -1;
            TxComentario.Text = string.Empty;

            Session["GESTIONES_TAREAS_MIN_DIARIOS"] = null;
            Session["GESTIONES_TAREAS_ADJUNTO"] = null;
            Session["GESTIONES_TAREAS_COMENTARIOS"] = null;

            GvComentario.DataSource = null;
            GvComentario.DataBind();

            //divAdjunto.Visible = false;
            divComentario.Visible = false;
        }

        protected void BtnAddComentario_1_Click(object sender, EventArgs e)
        {
            try
            {
                divAlertaComentario_1.Visible = false;
                string vEx = "";
                if (Session["GESTIONES_ID_TARJETA_CERRAR"] == null)
                {
                    vEx = "";
                }
                else
                {
                    vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();
                }

                if (vEx != null && vEx != "")
                {
                    if (TxComentario_1.Text == "" || TxComentario_1.Text == string.Empty)
                        throw new Exception("El campo de comentario está vacío.");

                    string usuario = Session["USUARIO_AD"].ToString() + ' ' + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    String vQuery = "GESTIONES_Solicitud 2,'" + vEx + "','" + TxComentario_1.Text + "','" + usuario + "'";
                    Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    string vCambio = "Usuario agrego nuevo comentario a la tarjeta. Comentario: " + TxComentario_1.Text;
                    string vCambioSuscripcion = "Buen día, se notifica que el usuario: " + Session["USUARIO_AD"].ToString() + ", agrego nuevo comentario a la tarjeta: " + vEx + "-" + TxTitulo.Text + ", Comentario: " + TxComentario_1.Text;

                    vQuery = "GESTIONES_Solicitud 4,'" + vEx + "','" + vCambio + "','" + Session["USUARIO_AD"].ToString() + "'";
                    Int32 vInfo4 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    //DATOS COMENTARIOS
                    vQuery = "GESTIONES_Solicitud 14,'" + vEx + "'";
                    DataTable vDatosComentarios = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                    if (vDatosComentarios.Rows.Count > 0)
                    {
                        divComentarioLectura.Visible = true;
                        GvComentarioLectura.DataSource = vDatosComentarios;
                        GvComentarioLectura.DataBind();
                        Session["GESTIONES_TAREAS_COMENTARIOS"] = vDatosComentarios;
                    }
                    else
                    {
                        divComentarioLectura.Visible = false;
                        divAlertaComentario.Visible = true;
                    }

                    //ACTUALIZAR DATOS HISTORIAL
                    vQuery = "GESTIONES_Solicitud 15,'" + vEx + "'";
                    DataTable vDatosHistorial = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                    if (vDatosHistorial.Rows.Count > 0)
                    {
                        GvHistorial.DataSource = vDatosHistorial;
                        GvHistorial.DataBind();
                    }


                    vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vUsuarioCreo = vDatos.Rows[0]["usuarioCreo"].ToString();
                    string vEmailResponsable = vDatos.Rows[0]["emailResponsable"].ToString();
                    string vEmailUsuarioCreo = vDatos.Rows[0]["emailCreo"].ToString();
                    string vResponsable = vDatos.Rows[0]["responsable"].ToString();


                    string vUsuarioLogueado = Session["USUARIO"].ToString();
                    if (vUsuarioCreo != vResponsable)
                    {
                        string vAsunto = "Nuevo Comentario Tarjeta Kanban, Gestión Técnica: " + vEx;
                        string vTituloSuscripcion = "Gestión Técnica, Nuevo Comentario Tarjeta Kanban";
                        string vQuery5 = "GESTIONES_Solicitud 5,'" + vTituloSuscripcion + "','"
                                        + vEmailUsuarioCreo
                                        + "','" + vEmailResponsable
                                        + "','" + vAsunto + "','" + vCambioSuscripcion + "', '0','" + vEx + "'";
                        Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);
                    }
                    else if (vUsuarioLogueado != vResponsable)
                    {
                        string vAsunto = "Nuevo Comentario Tarjeta Kanban, Gestión Técnica: " + vEx;
                        string vTituloSuscripcion = "Gestión Técnica, Nuevo Comentario Tarjeta Kanban";
                        string vQuery5 = "GESTIONES_Solicitud 5,'" + vTituloSuscripcion + "','"
                                        + vEmailUsuarioCreo
                                        + "','" + vEmailResponsable
                                        + "','" + vAsunto + "','" + vCambioSuscripcion + "', '0','" + vEx + "'";
                        Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);
                    }
                    TxComentario_1.Text = "";
                    //Session["GESTIONES_ID_TARJETA_CERRAR"] = null;
                    Session["GESTIONES_TAREAS_COMENTARIOS"] = null;

                }

            }
            catch (Exception ex)
            {
                LbAlertaComentario_1.InnerText = ex.Message;
                divAlertaComentario_1.Visible = true;
            }
        }

        protected void TxComentario_1_TextChanged(object sender, EventArgs e)
        {
            divAlertaComentario_1.Visible = false;
        }

        //public void OpenModalLoad()
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "document.addEventListener(\"DOMContentLoaded\", function (event) { ModalTarjetaOpen(); });", true);
        //}

        protected void BtnConfirmarTarea_1_Click(object sender, EventArgs e)
        {
            try
            {
                LbAlertaGuardar.InnerText = "";

               String vEx = null;
                vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();

                string vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vResponsableTarjeta= vDatos.Rows[0]["responsable"].ToString();
                string vNombreResponsableTarjeta = vDatos.Rows[0]["nombre"].ToString();
                string vidOperativa = vDatos.Rows[0]["idOperativa"].ToString();
            
                Session["GESTIONES_RESPONSABLE_TARJETA_CERRAR"] = vResponsableTarjeta;
                Session["GESTIONES_NOMBRE_RESPONSABLE_TARJETA_CERRAR"] = vNombreResponsableTarjeta;

  
                if (DdlAccion.SelectedValue == "1")
                {
                    validacionesCerrarTarea();
                    int num = 0;
                    //Validar si el checkList esta vacio
                    if (vidOperativa != null && vidOperativa != "")
                    {
                        DataTable vDatosCheckListVerificar = (DataTable)Session["GESTIONES_VERIFICACION"];
                        if (vDatosCheckListVerificar != null)
                        {
                            foreach (GridViewRow row in GvCheckList.Rows)
                            {
                                string tipo = vDatosCheckListVerificar.Rows[num]["tipo"].ToString();
                                TextBox vValidacionTexto = (TextBox)row.Cells[2].FindControl("TxRespuesta");
                                string vContenidoValidacionTexto = vValidacionTexto.Text;

                                TextBox vValidacionImagen = (TextBox)row.Cells[3].FindControl("txtEvtTo");
                                string vContenidoValidacionImagen = vValidacionImagen.Text;


                                if (tipo == "Texto" && vContenidoValidacionTexto == "")
                                    throw new Exception("Favor completar todas las preguntas de la lista de verificacion");

                                if (tipo == "Imagen" && vContenidoValidacionImagen == "No")
                                    throw new Exception("Favor completar todas las preguntas de la lista de verificacion");

                                num = num + 1;
                            }
                        }
                    }


                    LbTituloCerrar.Text = "Está seguro de " + DdlAccion.SelectedItem.Text + ": " + vEx;
                    UpdatePanel14.Update();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCerrarOpen();", true);

                }
                else if (DdlAccion.SelectedValue == "3")
                {
                    validacionesCerrarTarea();
                    if (DdlMotivoEliminar.SelectedValue.Equals("0"))
                        throw new Exception("Falta que seleccione motivo por el cúal solicita eliminar la tarjeta.");

                    LbTituloCerrar.Text = "Está seguro de la " + DdlAccion.SelectedItem.Text + ": " + vEx;
                    divNuevasFechas.Visible = false;
                    divMotivoEliminar.Visible = true;
                    UpdatePanel14.Update();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCerrarOpen();", true);
                }
                else
                {

                    validacionesCerrarTarea();
                    vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vUsuarioCreo = vDatos.Rows[0]["usuarioCreo"].ToString();
                    string vResponsable = Session["USUARIO"].ToString();

                    if (vUsuarioCreo == vResponsable)
                    {
                        validacionesDetenerTarjeta();
                        cargarModalDetener();

                        GVDistribucion.DataSource = null;
                        GVDistribucion.DataBind();
                        Session["GESTIONES_TAREAS_MIN_DIARIOS"] = null;
                        DateTime fecha_inicio = DateTime.Parse(TxNewFechaInicio.Text.ToString());
                        DateTime fecha_fin = DateTime.Parse(TxNewFechaEntrega.Text.ToString());

                        String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
                        String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
                        String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

                        DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        DateTime vFechaInicio = DateTime.Parse(vFecha1);


                        vQuery = "GESTIONES_Solicitud 7,'" + vResponsableTarjeta + "'";
                        vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                        Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
                        Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

                        vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                        vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                        Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();

                        double vMinDiarios = 0;
                        double vWip = Convert.ToInt32(Session["GESTIONES_WIP"].ToString());

                        DataTable vData = new DataTable();
                        DataTable vDatosMin = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
                        vData.Columns.Add("id");
                        vData.Columns.Add("fecha");
                        vData.Columns.Add("min");
                        string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
                        string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");
                        DateTime vFechaInicioConver = DateTime.Parse(vFechaInicioSoli);
                        DateTime vFechaFinConver = DateTime.Parse(vFechaFinSoli);

                        string vHrInicialSoli = fecha_inicio.ToString("HH:mm");
                        string vHrFinalSoli = fecha_fin.ToString("HH:mm");

                        TimeSpan vHrInicialSoliConver = TimeSpan.Parse(vHrInicialSoli);
                        TimeSpan vHrFinSoliConver = TimeSpan.Parse(vHrFinalSoli);


                        DateTime vfechaActualDetener = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                        string vfechaActualDetenerEvaluar = vfechaActualDetener.ToString("dd/MM/yyyy");



                        vQuery = "GESTIONES_Generales 62,'" + vResponsableTarjeta +"','"+ vEx+"'";
                        vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        string vMinFaltantesDetener = vDatos.Rows[0]["minDiariosFaltantes"].ToString();

                        if (vFechaInicio.DayOfWeek != DayOfWeek.Saturday && vFechaInicio.DayOfWeek != DayOfWeek.Sunday)
                        {
                            TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
                            int businessDays = span.Days;
                            int fullWeekCount = businessDays / 7;

                            if (businessDays == 7)
                            {
                                businessDays = businessDays - 2;
                            }
                            else if (businessDays == 6)
                            {
                                businessDays = businessDays - 1;
                            }
                            else if (businessDays > fullWeekCount * 7)
                            {
                                int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
                                int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
                                if (lastDayOfWeek < firstDayOfWeek)
                                    lastDayOfWeek += 7;
                                if (firstDayOfWeek <= 6)
                                {
                                    if (lastDayOfWeek >= 7)
                                        businessDays -= 2;
                                    else if (lastDayOfWeek >= 6)
                                        businessDays -= 1;
                                }
                                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                                    businessDays -= 1;

                                //subtract the weekends during the full weeks in the interval
                                businessDays -= fullWeekCount + fullWeekCount;
                            }
                            int vDias = businessDays + 1;
                            Session["GESTIONES_DIAS"] = vDias;
                            vMinDiarios = Convert.ToInt32(vMinFaltantesDetener) / vDias;

                            int vCount = 0;
                            int vResta = 0;

                            double vMinsFaltante = 0;

                            if (vDias == 1)
                            {
                                if (vDatosMin == null)
                                    vDatosMin = vData.Clone();
                                if (vDatosMin != null)
                                {
                                    vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                                    vDatosMin.Rows.Add("1", vFechaInicioSoli, vMinDiarios);
                                }
                            }
                            else
                            {

                                //VALIDACION INICIO TAREA SI TIENE MIN DISPNIBLES
                                string vCantMinSolicitudes = "";
                                string vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaInicioSoli + "'";
                                DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();

                                if (vCantMinSolicitudes != "")
                                {
                                    if (Convert.ToDouble(vCantMinSolicitudes) >= Convert.ToDouble(vWip))
                                        throw new Exception("Nota: La fecha seleccionada inicio de la tarjeta ya no cuenta con mins disponibles, su WIP está al limite, favor cambiar la fecha de inicio para poder realizar una mejor distribución de su cargabilidad. Minutos registrados de cargabilidad: " + vCantMinSolicitudes + ", WIP límite establecido: " + vWip);
                                }


                                for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                                {
                                    if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                                    {
                                        vCount = vCount + 1;
                                        vResta = (vDias - vCount) + 1;

                                        if (vMinsFaltante != 0)
                                        {
                                            vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                            vMinsFaltante = 0;
                                        }

                                        string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                        vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaEvaluar + "'";
                                        vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                        vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                        double vSobranteWIPCreacion = 0;

                                        if (vCantMinSolicitudes.Equals(""))
                                        {
                                            vSobranteWIPCreacion = vWip;
                                        }
                                        else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                        {
                                            vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                        }
                                        else
                                        {
                                            vSobranteWIPCreacion = 0;
                                        }


                                        if (vMinDiarios <= vSobranteWIPCreacion)
                                        {
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                            vMinDiarios = vSobranteWIPCreacion;
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (vMinsFaltante != 0)
                                throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

                        }
                        else if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday || vFechaInicio.DayOfWeek == DayOfWeek.Sunday)
                        {
                            TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
                            int businessDays = span.Days;
                            int fullWeekCount = businessDays / 7;

                            if (businessDays == 7)
                            {
                                businessDays = businessDays - 2;
                            }
                            else if (businessDays == 6)
                            {
                                businessDays = businessDays - 1;
                            }
                            else if (businessDays > fullWeekCount * 7)
                            {
                                int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
                                int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
                                if (lastDayOfWeek < firstDayOfWeek)
                                    lastDayOfWeek += 7;
                                if (firstDayOfWeek <= 6)
                                {
                                    if (lastDayOfWeek >= 7)
                                        businessDays -= 2;
                                    else if (lastDayOfWeek >= 6)
                                        businessDays -= 1;
                                }
                                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                                    businessDays -= 1;

                                //subtract the weekends during the full weeks in the interval
                                businessDays -= fullWeekCount + fullWeekCount;
                            }
                            int vDias = businessDays + 1;
                            Session["GESTIONES_DIAS"] = vDias;
                            
                            LbDiaNoHabil.Text = "Se debe iniciar a trabajar en la tarjeta un día de trabajo no hábil";
                            divDiaNoHabil.Visible = true;
                            if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday)
                            {
                                if (vDias == 1)
                                {
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                                        vDatosMin.Rows.Add("1", vFechaInicioSoli, vMinFaltantesDetener);
                                    }
                                }
                                else
                                {
                                    vDias = vDias + 2;
                                    vMinDiarios = Convert.ToInt32(vMinFaltantesDetener) / vDias;
                                    DateTime vFechaFinConverDomingo = vFechaInicioConver.AddDays(1);
                                    DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
                                    int vCount = 0;
                                    int vResta = 0;
                                    double vMinsFaltante = 0;

                                    for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
                                    {
                                        string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                        if (vDatosMin == null)
                                            vDatosMin = vData.Clone();
                                        if (vDatosMin != null)
                                        {
                                            if (vDatosMin.Rows.Count < 1)
                                            {
                                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                            }
                                            else
                                            {
                                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                            }
                                        }

                                    }

                                    for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                                    {
                                        if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                                        {
                                            vCount = vCount + 1;
                                            vResta = (vDias - vCount) + 1;

                                            if (vMinsFaltante != 0)
                                            {
                                                vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                                vMinsFaltante = 0;
                                            }

                                            string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                            string vCantMinSolicitudes = "";
                                            string vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaEvaluar + "'";
                                            DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                            vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                            double vSobranteWIPCreacion = 0;

                                            if (vCantMinSolicitudes.Equals(""))
                                            {
                                                vSobranteWIPCreacion = vWip;
                                            }
                                            else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                            {
                                                vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                            }
                                            else
                                            {
                                                vSobranteWIPCreacion = 0;
                                            }


                                            if (vMinDiarios <= vSobranteWIPCreacion)
                                            {
                                                if (vDatosMin == null)
                                                    vDatosMin = vData.Clone();
                                                if (vDatosMin != null)
                                                {
                                                    if (vDatosMin.Rows.Count < 1)
                                                    {
                                                        vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                    }
                                                    else
                                                    {
                                                        vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                                vMinDiarios = vSobranteWIPCreacion;
                                                if (vDatosMin == null)
                                                    vDatosMin = vData.Clone();
                                                if (vDatosMin != null)
                                                {
                                                    if (vDatosMin.Rows.Count < 1)
                                                    {
                                                        vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                    }
                                                    else
                                                    {
                                                        vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (vMinsFaltante != 0)
                                        throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);
                                }
                            }
                            else
                            {
                                vDias = vDias;
                                vMinDiarios = Convert.ToInt32(vMinFaltantesDetener) / vDias;
                                DateTime vFechaFinConverDomingo = vFechaInicioConver;
                                DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
                                int vCount = 0;
                                int vResta = 0;
                                double vMinsFaltante = 0;

                                for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
                                {
                                    string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        if (vDatosMin.Rows.Count < 1)
                                        {
                                            vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                        }
                                        else
                                        {
                                            vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                        }
                                    }

                                }

                                for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                                {
                                    if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                                    {
                                        vCount = vCount + 1;
                                        vResta = (vDias - vCount) + 1;

                                        if (vMinsFaltante != 0)
                                        {
                                            vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                            vMinsFaltante = 0;
                                        }

                                        string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                        string vCantMinSolicitudes = "";
                                        string vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaEvaluar + "'";
                                        DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                        vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                        double vSobranteWIPCreacion = 0;

                                        if (vCantMinSolicitudes.Equals(""))
                                        {
                                            vSobranteWIPCreacion = vWip;
                                        }
                                        else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                        {
                                            vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                        }
                                        else
                                        {
                                            vSobranteWIPCreacion = 0;
                                        }


                                        if (vMinDiarios <= vSobranteWIPCreacion)
                                        {
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                            vMinDiarios = vSobranteWIPCreacion;
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                    }
                                }

                                if (vMinsFaltante != 0)
                                    throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

                            }




                        }


                        Session["GESTIONES_TAREAS_MIN_DIARIOS"] = vDatosMin;
                        GVDistribucion.DataSource = vDatosMin;
                        GVDistribucion.DataBind();


                        TxTimeModal.Text = vMinFaltantesDetener + " Mins";
                        TxEntregaModal.Text = TxNewFechaEntrega.Text;
                        TxInicioModal.Text = TxNewFechaInicio.Text;
                        UpdatePanel6.Update();

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaConfirmarOpen();", true);
                    }
                    else
                    {
                        validacionesCerrarTarea();
                        LbTituloCerrar.Text = "Está seguro de la " + DdlAccion.SelectedItem.Text + ": " + vEx;
                        UpdatePanel14.Update();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCerrarOpen();", true);
                    }

                }


                //OpenModalLoad();

            }
            catch (Exception ex)
            {
                LbAlertaGuardar.InnerText = ex.Message;
                divAlertaGuardar.Visible = true;
                //OpenModalLoad();
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "document.addEventListener(\"DOMContentLoaded\", function (event) { ModalTarjetaOpen(); });", true);
            }
        }

        protected void BtnCancelarTarea_1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaClose();", true);
            Response.Redirect("/pages/miTablero.aspx");
            cargarInicialTarjeta();
            cargarData();
            limpiarCreacionTarea();
        }

        protected void DdlTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vIdRol = Session["ID_ROL_USUARIO"].ToString();
            if (DdlTipoBusqueda.SelectedValue == "2")//Reporte Equipo Trabajo
            {
                DdlEquipoTrabajo.Visible = true;
                DdlColaborador.Visible = false;

                if (vIdRol == "1")
                {
                    DdlEquipoTrabajo.Items.Clear();
                    String vQuery = "GESTIONES_Solicitud 27,'" + Session["USUARIO"].ToString() + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlEquipoTrabajo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlEquipoTrabajo.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                }
                else if (vIdRol == "3" || vIdRol == "4" || vIdRol == "5")
                {
                    DdlEquipoTrabajo.Items.Clear();
                    String vQuery = "GESTIONES_Solicitud 25";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlEquipoTrabajo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlEquipoTrabajo.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                }
            }
            else if (DdlTipoBusqueda.SelectedValue == "3")//Reporte Colaborador
            {
                DdlEquipoTrabajo.Visible = false;
                DdlColaborador.Visible = true;

                if (vIdRol == "1")
                {
                    DdlColaborador.Items.Clear();
                    String vQuery = "GESTIONES_Solicitud 28,'" + Session["USUARIO"].ToString() + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlColaborador.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlColaborador.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                }
                else if (vIdRol == "3" || vIdRol == "4" || vIdRol == "5")
                {
                    DdlColaborador.Items.Clear();
                    String vQuery = "GESTIONES_Solicitud 26";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlColaborador.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlColaborador.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                }

            }
            else if (DdlTipoBusqueda.SelectedValue == "4")
            {
                Response.Redirect("miTablero.aspx?et=" + Session["USUARIO"].ToString());
            }
            else
            {
                DdlEquipoTrabajo.Visible = false;
                DdlColaborador.Visible = false;
                Response.Redirect("/pages/miTablero.aspx");
            }
        }

        protected void DdlEquipoTrabajo_SelectedIndexChanged(object sender, EventArgs e)
        {

            Response.Redirect("miTablero.aspx?et=" + DdlEquipoTrabajo.SelectedValue);


            //cargarDataEquipoTrabajo(DdlEquipoTrabajo.SelectedValue);
            //UpdatePanel19.Update();
        }
        void cargarDataEquipoTrabajo(String et)
        {

            String equipoTrabajo = et;

            string vQuery = "GESTIONES_Generales 44,'" + equipoTrabajo + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnCola = vDatos.Rows[0]["cantCola"].ToString();

            vQuery = "GESTIONES_Generales 45,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnEjecucion = vDatos.Rows[0]["cantEjecucion"].ToString();

            vQuery = "GESTIONES_Generales 46,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vCompletadasHoy = vDatos.Rows[0]["cantCompletadasHoy"].ToString();

            vQuery = "GESTIONES_Generales 47,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vAtrasado = vDatos.Rows[0]["cantAtrasado"].ToString();

            vQuery = "GESTIONES_Generales 48,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vDetenidas = vDatos.Rows[0]["cantDetenidas"].ToString();

            LbEnCola.Text = vEnCola;
            LbEjecucion.Text = vEnEjecucion;
            LbCompletados.Text = vCompletadasHoy;
            LbAtrasados.Text = vAtrasado;
            LbDetenidas.Text = vDetenidas;
            string vString = "";
            string vTest = "";
            vQuery = "GESTIONES_Generales 49,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = "";
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }



                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                                         "$(function () {" + Environment.NewLine +
                                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                                         "document.getElementById('" + TxTitulo.ClientID + "').value =$(this).data('titulo');" + Environment.NewLine +
                                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                                         "});" + Environment.NewLine +
                                         "});" + Environment.NewLine +
                                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEnCola.Text = vString;
            LitEnCola.Text = vTest;
            

            //SOLICITUDES EN EJECUCIÓN
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 50,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEjecucion.Text = vString;
            LitEnEjecucion.Text = vTest;


            //SOLICITUDES ATRASADAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 51,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = "";
                ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }



                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesAtrasadas.Text = vString;
            LitAtrasados.Text = vTest;

            //SOLICITUDES COMPLETADOS HOY
            vString = "";
            vTest = "";
            DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            vQuery = "GESTIONES_Generales 52,'" + equipoTrabajo + "','" + vfechaActualCorta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vEstadoNombre = "", vColorEstado = "", vFechaInicio = "", vEmpleado = "", vColorHeader = "";

                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vEstadoNombre = vDatos.Rows[i]["estado"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vEstadoNombre == "Realizado a Tiempo")
                { vColorEstado = "success";
                    //BtnConfirmarTarea_1.Visible = false;
                }
                else
                {

                    vColorEstado = "danger";
                    //BtnConfirmarTarea_1.Visible = false;
                }


                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }

                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                "<h5><span class='label label-" + vColorEstado + "'>" + vEstadoNombre + "</span></h5><br>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";


                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;

            }
            LitNotificacionesCompletadosHoy.Text = vString;
            LitCompletadosHoy.Text = vTest;



            //SOLICITUDES DETENIDAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 53,'" + equipoTrabajo + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesDetenidas.Text = vString;
            LitDetenidas.Text = vTest;

        }

        void cargarDataTareasAsignadas(String et)
        {

            String personaAsigno = et;

            string vQuery = "GESTIONES_Generales 72,'" + personaAsigno + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnCola = vDatos.Rows[0]["cantCola"].ToString();

            vQuery = "GESTIONES_Generales 73,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnEjecucion = vDatos.Rows[0]["cantEjecucion"].ToString();

            vQuery = "GESTIONES_Generales 74,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vCompletadasHoy = vDatos.Rows[0]["cantCompletadasHoy"].ToString();

            vQuery = "GESTIONES_Generales 75,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vAtrasado = vDatos.Rows[0]["cantAtrasado"].ToString();

            vQuery = "GESTIONES_Generales 76,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vDetenidas = vDatos.Rows[0]["cantDetenidas"].ToString();

            LbEnCola.Text = vEnCola;
            LbEjecucion.Text = vEnEjecucion;
            LbCompletados.Text = vCompletadasHoy;
            LbAtrasados.Text = vAtrasado;
            LbDetenidas.Text = vDetenidas;
            string vString = "";
            string vTest = "";
            vQuery = "GESTIONES_Generales 77,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = "";
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }



                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                                         "$(function () {" + Environment.NewLine +
                                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                                         "document.getElementById('" + TxTitulo.ClientID + "').value =$(this).data('titulo');" + Environment.NewLine +
                                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                                         "});" + Environment.NewLine +
                                         "});" + Environment.NewLine +
                                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEnCola.Text = vString;
            LitEnCola.Text = vTest;


            //SOLICITUDES EN EJECUCIÓN
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 78,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEjecucion.Text = vString;
            LitEnEjecucion.Text = vTest;


            //SOLICITUDES ATRASADAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 79,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = "";
                ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();
                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }



                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesAtrasadas.Text = vString;
            LitAtrasados.Text = vTest;

            //SOLICITUDES COMPLETADOS HOY
            vString = "";
            vTest = "";
            DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            vQuery = "GESTIONES_Generales 80,'" + personaAsigno + "','" + vfechaActualCorta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vEstadoNombre = "", vColorEstado = "", vFechaInicio = "", vEmpleado = "", vColorHeader = "";

                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vEstadoNombre = vDatos.Rows[i]["estado"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vEstadoNombre == "Realizado a Tiempo")
                {
                    vColorEstado = "success";
                    //BtnConfirmarTarea_1.Visible = false;
                }
                else
                {

                    vColorEstado = "danger";
                    //BtnConfirmarTarea_1.Visible = false;
                }


                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }

                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                "<h5><span class='label label-" + vColorEstado + "'>" + vEstadoNombre + "</span></h5><br>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";


                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;

            }
            LitNotificacionesCompletadosHoy.Text = vString;
            LitCompletadosHoy.Text = vTest;



            //SOLICITUDES DETENIDAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 81,'" + personaAsigno + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vEmpleado = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vEmpleado = vDatos.Rows[i]["empleado"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "<br><br><h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> RESPONSABLE:</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'><strong>" + vEmpleado + "</strong></h6>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesDetenidas.Text = vString;
            LitDetenidas.Text = vTest;

        }

        protected void DdlColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("miTablero.aspx?vColaborador=" + DdlColaborador.SelectedValue);

            //cargarDataColaborador();
            //UpdatePanel19.Update();
        }
        void cargarDataColaborador(String vColaborador)
        {
            String vColaboradorBusqueda = vColaborador;

            string vQuery = "GESTIONES_Generales 2,'" + vColaboradorBusqueda + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnCola = vDatos.Rows[0]["cantCola"].ToString();

            vQuery = "GESTIONES_Generales 3,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vEnEjecucion = vDatos.Rows[0]["cantEjecucion"].ToString();

            vQuery = "GESTIONES_Generales 4,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vCompletadasHoy = vDatos.Rows[0]["cantCompletadasHoy"].ToString();

            vQuery = "GESTIONES_Generales 5,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vAtrasado = vDatos.Rows[0]["cantAtrasado"].ToString();

            vQuery = "GESTIONES_Generales 40,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vDetenidas = vDatos.Rows[0]["cantDetenidas"].ToString();

            LbEnCola.Text = vEnCola;
            LbEjecucion.Text = vEnEjecucion;
            LbCompletados.Text = vCompletadasHoy;
            LbAtrasados.Text = vAtrasado;
            LbDetenidas.Text = vDetenidas;
            string vString = "";
            string vTest = "";
            vQuery = "GESTIONES_Generales 21,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }

                vString += "<div class='card'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEnCola.Text = vString;
            LitEnCola.Text = vTest;



            //SOLICITUDES EN EJECUCIÓN
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 22,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                vString += "<div class='card'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesEjecucion.Text = vString;
            LitEnEjecucion.Text = vTest;


            //SOLICITUDES ATRASADAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 23,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                vString += "<div class='card'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesAtrasadas.Text = vString;
            LitAtrasados.Text = vTest;

            //SOLICITUDES COMPLETADOS HOY
            vString = "";
            vTest = "";
            DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            vQuery = "GESTIONES_Generales 24,'" + vColaboradorBusqueda + "','" + vfechaActualCorta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vEstadoNombre = "", vColorEstado = "", vFechaInicio = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vEstadoNombre = vDatos.Rows[i]["estado"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vEstadoNombre == "Realizado a Tiempo")
                { vColorEstado = "success";
                    //BtnConfirmarTarea_1.Visible = false;
                }
                else
                {
                    vColorEstado = "danger";
                    //BtnConfirmarTarea_1.Visible = false;
                }

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }

                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                "<h5><span class='label label-" + vColorEstado + "'>" + vEstadoNombre + "</span></h5><br>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";


                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;

            }
            LitNotificacionesCompletadosHoy.Text = vString;
            LitCompletadosHoy.Text = vTest;


            //SOLICITUDES DETENIDAS
            vString = "";
            vTest = "";
            vQuery = "GESTIONES_Generales 41,'" + vColaboradorBusqueda + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "", vColorHeader = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
                vColorHeader = vDatos.Rows[i]["colorTarjeta"].ToString();

                if (vDatos.Rows[i]["prioridad"].ToString() == "Máxima Prioridad")
                {
                    vColor = "badge-danger";
                    vColorBoton = "btn-danger";
                    vColorPrioridad = "danger";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Alta")
                {
                    vColor = "bg-primary";
                    vColorBoton = "btn-primary";
                    vColorPrioridad = "primary";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Normal")
                {
                    vColor = "bg-warning";
                    vColorBoton = "btn-warning";
                    vColorPrioridad = "warning";
                }
                else if (vDatos.Rows[i]["prioridad"].ToString() == "Baja")
                {
                    vColor = "badge-info";
                    vColorBoton = "btn-info";
                    vColorPrioridad = "info";
                }


                vString += "<div class='card'>" +
                //"<div class='card-header " + vColor + " text-white'>" +
                "<div class='card-header text-white' style='background-color:" + vColorHeader + ";'>" +
                "<h6 class='m-b-0 text-white'>ID TARJETA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark' style='font-size:9px'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted' style='font-size:11px'><i class='fa fa-calendar'></i>  ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'style='font-size:11px'> PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center'>" +
                "<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn btn-circle fa fa-clipboard' style='background-color: " + vColorHeader + "; color: #ffffff;'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                //"<button id=\"btnModal" + vTicket + "\"  type=\"button\" class='btn " + vColorBoton + " btn-circle fa fa-clipboard'" + " \" data-toggle=\"modal\" data-target=\"#ModalTarjeta\" data-titulo=\"" + vTicket + "\"></button>" +
                "</div>" +
                "</div>" +
                "</div>";

                vTest += "<script type=\"text/javascript\" >" + Environment.NewLine +
                         "$(function () {" + Environment.NewLine +
                         "$(\"#btnModal" + vTicket + "\").click(function () {" + Environment.NewLine +
                         "document.getElementById('" + TxTitulo.ClientID + "').value = $(this).data('titulo');" + Environment.NewLine +
                         "__doPostBack('" + TxTitulo.ClientID + "', '');" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "});" + Environment.NewLine +
                         "</script>" + Environment.NewLine;
            }
            LitNotificacionesDetenidas.Text = vString;
            LitDetenidas.Text = vTest;


        }

        protected void BtnBusqueda_Click1(object sender, EventArgs e)
        {
            DivBusquedaReportes.Visible = true;
            UpdatePanel17.Update();
        }

        protected void DdlResponsable_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable_1.SelectedValue + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
                Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

                DdlTipoGestion_1.Items.Clear();
                DdlTipoGestion_1.Enabled = true;
                vQuery = "GESTIONES_Generales 1,'" + Session["GESTIONES_TEAMS"].ToString() + "'";
                DataTable vDatosTipo = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlTipoGestion_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatosTipo.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosTipo.Rows)
                    {
                        DdlTipoGestion_1.Items.Add(new ListItem { Value = item["idTipoGestion"].ToString(), Text = item["nombreGestion"].ToString() });
                    }
                }

                vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        protected void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
            String vEx = null;
            vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();

            string vQueryEstado = "GESTIONES_Solicitud 12,'" + vEx + "'";
            DataTable vDatosEstado = vConexionGestiones.obtenerDataTableGestiones(vQueryEstado);
            string vEstadoTarjeta = vDatosEstado.Rows[0]["idEstado"].ToString();
            string vIdOperativa = vDatosEstado.Rows[0]["idOperativa"].ToString();

                int num = 0;
                if (vIdOperativa != null && vIdOperativa != "")
                {
                    DataTable vDatosCheckListVerificar = (DataTable)Session["GESTIONES_VERIFICACION"];
                    if (vDatosCheckListVerificar != null)
                    {
                        foreach (GridViewRow row in GvCheckList.Rows)
                        {
                            string tipo = vDatosCheckListVerificar.Rows[num]["tipo"].ToString();
                            string pregunta = vDatosCheckListVerificar.Rows[num]["pregunta"].ToString();
                            string idPregunta = vDatosCheckListVerificar.Rows[num]["id"].ToString();
                            TextBox vValidacionTexto = (TextBox)row.Cells[2].FindControl("TxRespuesta");

                            String vArchivo = String.Empty;
                            String vExtension = String.Empty;
                            string vNombreDepot1 = String.Empty;

                            if (tipo == "Texto")
                            {
                                vArchivo = vValidacionTexto.Text;
                            }else if (tipo == "Imagen")
                            {
                                FileUpload vFuRespuesta = (FileUpload)row.Cells[2].FindControl("FuRespuesta");                               
                                HttpPostedFile bufferDeposito1T = vFuRespuesta.PostedFile;
                                byte[] vFileDeposito1 = null;
                                

                                if (bufferDeposito1T != null)
                                {
                                    vNombreDepot1 = vFuRespuesta.FileName;
                                    Stream vStream = bufferDeposito1T.InputStream;
                                    BinaryReader vReader = new BinaryReader(vStream);
                                    vFileDeposito1 = vReader.ReadBytes((int)vStream.Length);
                                    vExtension = System.IO.Path.GetExtension(vFuRespuesta.FileName);
                                }
                                
                                if (vFileDeposito1 != null)
                                    vArchivo = Convert.ToBase64String(vFileDeposito1);
                            }

                            string vQueryCheckList = "GESTIONES_Solicitud 43,'" + vEx + "','" + vIdOperativa + "','" + pregunta + "','" + vArchivo + "','" + idPregunta + "','"+ vNombreDepot1+"','"+ vExtension+"'";
                            Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQueryCheckList);
                            num = num + 1;
                        }
                    }
                }

            string vResponsableTarjeta = Session["GESTIONES_RESPONSABLE_TARJETA_CERRAR"].ToString();
            string vNombreResponsableTarjeta =Session["GESTIONES_NOMBRE_RESPONSABLE_TARJETA_CERRAR"].ToString();

            String vFormato = "dd/MM/yyyy HH:mm"; //"dd/MM/yyyy HH:mm:ss"
            String vFechaInicioTarea = Convert.ToDateTime(TxFechaInicio_1.Text).ToString(vFormato);
            //DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            string vfechaActualString= DateTime.Now.ToString("dd/MM/yyyy");

            DateTime fecha_inicio = DateTime.Parse(TxFechaInicio_1.Text.ToString());
            DateTime vFechaInicio = Convert.ToDateTime(fecha_inicio.ToString("dd/MM/yyyy HH:mm"));

            DateTime fecha_entrega = DateTime.Parse(TxFechaEntrega_1.Text.ToString());
            DateTime vFechaEntrega = Convert.ToDateTime(fecha_entrega.ToString("dd/MM/yyyy HH:mm"));


            //string vFechaEntrega_60Mins = Convert.ToDateTime(vFechaEntrega).AddMinutes(60).ToString("yyyy-MM-dd HH:mm");

            string vFechaEntregaFinDia = Convert.ToDateTime(vFechaEntrega).AddDays(3).ToString("yyyy-MM-dd");
            string vFechaEntregaTarjeta = Convert.ToDateTime(vFechaEntrega).ToString("yyyy-MM-dd");
            string vFechaActualConvertida = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            string vidEstado = "";
            string vidEstadoTexto = "";
            string vtarjetaEstado = "";
            string vCambio = "";

            double vMinRestante = 0;
            double vSumMinutos = 0;
            double vMinActual = 0;
            string vidCargabilidadMin = "";

            string vFechaEntregaBusqueda = Convert.ToDateTime(vfechaActualCorta).AddDays(2).ToString("dd/MM/yyyy");
            string vFechaCierraTarea = Convert.ToDateTime(vfechaActualCorta).ToString("dd/MM/yyyy");

            string vFechaRealEntrega = Convert.ToDateTime(fecha_entrega).ToString("dd/MM/yyyy");

            string vEstadoCargabilidad = "";
            if (DdlAccion.SelectedValue == "1")
            {
                if (Convert.ToDateTime(vFechaActualConvertida) < Convert.ToDateTime(vFechaEntregaFinDia))
                {
                    vidEstado = "5";
                    vidEstadoTexto = "Realizado a Tiempo";
                    vEstadoCargabilidad = "4";

                    string vQuery = "GESTIONES_Generales 64,'" + vResponsableTarjeta + "','" + vEx + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vMinRestanteString = vDatos.Rows[0]["minDiariosFaltantes"].ToString();

                    if (vMinRestanteString == "")
                    {
                        vMinRestante = 0;
                    }
                    else
                    {
                        vMinRestante = Convert.ToDouble(vMinRestanteString);
                    }

                    string correlativo = "1";

                    if (vEstadoTarjeta == "1")
                    {
                        string vQueryCant = "GESTIONES_Solicitud 38,'" + vResponsableTarjeta + "','" + vEx + "','"+ vfechaActualString+"'";
                        DataTable vDatosCant = vConexionGestiones.obtenerDataTableGestiones(vQueryCant);
                        string vCantFechas = vDatosCant.Rows[0]["cant"].ToString();

                        if (vCantFechas=="0")
                        {
                            String vQuery6 = "GESTIONES_Solicitud 11,'" + vEx +
                                            "','" + correlativo +
                                            "','" + vfechaActualString +
                                            "','" + vMinRestante +
                                             "','" + vEstadoCargabilidad +
                                             "','" + vResponsableTarjeta + "'";
                            Int32 vInfo6 = vConexionGestiones.ejecutarSqlGestiones(vQuery6);

                            vQuery = "GESTIONES_Generales 67,'" + vEx + "'";
                            Int32 vInfoActualizar = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                        }
                        else
                        {
                            vQuery = "GESTIONES_Generales 69,'" + vResponsableTarjeta + "','" + vfechaActualString + "','" + vEx + "'";
                            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                            vidCargabilidadMin = vDatos.Rows[0]["id"].ToString();

                            vQuery = "GESTIONES_Solicitud 39,'" + vidCargabilidadMin + "','" + vResponsableTarjeta + "','" + vEstadoCargabilidad + "','"+ vMinRestante+"'";
                            Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                            vQuery = "GESTIONES_Generales 67,'" + vEx + "'";
                            Int32 vInfoActualizar = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                        }

                    }else if (vEstadoTarjeta == "2")
                    {

                        string vQueryCant = "GESTIONES_Solicitud 38,'" + vResponsableTarjeta + "','" + vEx + "','" + vfechaActualString + "'";
                        DataTable vDatosCant = vConexionGestiones.obtenerDataTableGestiones(vQueryCant);
                        string vCantFechas = vDatosCant.Rows[0]["cant"].ToString();

                        if (vCantFechas != "0")
 
                        {
                            vQuery = "GESTIONES_Generales 69,'" + vResponsableTarjeta + "','" + vfechaActualString + "','" + vEx + "'";
                            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                            vidCargabilidadMin = vDatos.Rows[0]["id"].ToString();

                            vQuery = "GESTIONES_Solicitud 39,'" + vidCargabilidadMin + "','" + vResponsableTarjeta + "','" + vEstadoCargabilidad + "','" + vMinRestante + "'";
                            Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                            vQuery = "GESTIONES_Generales 67,'" + vEx + "'";
                            Int32 vInfoActualizar = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                        }

                    }
                    else if (vEstadoTarjeta == "4")
                    {
                        vQuery = "GESTIONES_Generales 70,'" + vResponsableTarjeta  + "','" + vEx + "'";
                        vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        vidCargabilidadMin = vDatos.Rows[0]["id"].ToString();

                        vQuery = "GESTIONES_Solicitud 39,'" + vidCargabilidadMin + "','" + vResponsableTarjeta + "','" + vEstadoCargabilidad + "','" + vMinRestante + "'";
                        Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                    }
                    else if (vEstadoTarjeta == "3")
                    {
                        string vQueryCant = "GESTIONES_Solicitud 38,'" + vResponsableTarjeta + "','" + vEx + "','" + vfechaActualString + "'";
                        DataTable vDatosCant = vConexionGestiones.obtenerDataTableGestiones(vQueryCant);
                        string vCantFechas = vDatosCant.Rows[0]["cant"].ToString();

                        if (vCantFechas == "0")
                        {
                            String vQuery6 = "GESTIONES_Solicitud 11,'" + vEx +
                                            "','" + correlativo +
                                            "','" + vfechaActualString +
                                            "','" + vMinRestante +
                                             "','" + vEstadoCargabilidad +
                                             "','" + vResponsableTarjeta + "'";
                            Int32 vInfo6 = vConexionGestiones.ejecutarSqlGestiones(vQuery6);

                            vQuery = "GESTIONES_Generales 67,'" + vEx + "'";
                            Int32 vInfoActualizar = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                        }
                        else
                        {
                            vQuery = "GESTIONES_Generales 69,'" + vResponsableTarjeta + "','" + vfechaActualString + "','" + vEx + "'";
                            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                            vidCargabilidadMin = vDatos.Rows[0]["id"].ToString();

                            vQuery = "GESTIONES_Solicitud 39,'" + vidCargabilidadMin + "','" + vResponsableTarjeta + "','" + vEstadoCargabilidad + "','" + vMinRestante + "'";
                            Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                            vQuery = "GESTIONES_Generales 67,'" + vEx + "'";
                            Int32 vInfoActualizar = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                        }


                    }



                }
                else
                {
                    vidEstado = "6";
                    vidEstadoTexto = "Realizado fuera de tiempo";
                    vEstadoCargabilidad = "4";

                    string vQuery = "GESTIONES_Generales 64,'" + vResponsableTarjeta + "','" + vEx + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vMinRestanteString = vDatos.Rows[0]["minDiariosFaltantes"].ToString();

                    if (vMinRestanteString == "")
                    {
                        vMinRestante = 0;
                    }
                    else
                    {
                        vMinRestante = Convert.ToDouble(vMinRestanteString);
                    }


                    vQuery = "GESTIONES_Generales 70,'" + vResponsableTarjeta + "','" + vEx + "'";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    vidCargabilidadMin = vDatos.Rows[0]["id"].ToString();

                    vQuery = "GESTIONES_Solicitud 39,'" + vidCargabilidadMin + "','" + vResponsableTarjeta + "','" + vEstadoCargabilidad + "','" + vMinRestante + "'";
                    Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                }
                vCambio = "Finalizar Tarjeta, Estado: " + vidEstadoTexto + ", Detalle: " + TxDetalle.Text;

                    
            }
            else if (DdlAccion.SelectedValue == "3")
            {
                vidEstado = "10";
                vidEstadoTexto = "Solicitud Eliminar Tarjeta";
                vEstadoCargabilidad = "6";
                vCambio = vidEstadoTexto + ", Detalle: " + DdlMotivoEliminar.SelectedItem.ToString() + ", " + TxDetalle.Text;
            }
            else
            {
                vidEstado = "9";
                vidEstadoTexto = "Solicitud Tarjeta a Estado Detenido";
                vEstadoCargabilidad = "3";
                vCambio = vidEstadoTexto + ", Detalle: " + TxDetalle.Text;
            }



            string vMensaje = "";
            if (DdlAccion.SelectedValue == "1")
            {
                    string escalacion = "";
                    if (ckEscalacion.SelectedValue == "")
                    {
                        escalacion = "No";
                    }
                    else
                    {
                        escalacion = "Si";
                    }


                string vNombreDepot1 = String.Empty;
                HttpPostedFile bufferDeposito1T = FuSolucion.PostedFile;
                byte[] vFileDeposito1 = null;
                String vExtension = String.Empty;

                if (bufferDeposito1T != null)
                {
                    vNombreDepot1 = FuSolucion.FileName;
                    Stream vStream = bufferDeposito1T.InputStream;
                    BinaryReader vReader = new BinaryReader(vStream);
                    vFileDeposito1 = vReader.ReadBytes((int)vStream.Length);
                    vExtension = System.IO.Path.GetExtension(FuSolucion.FileName);
                }
                String vArchivo = String.Empty;
                if (vFileDeposito1 != null)
                    vArchivo = Convert.ToBase64String(vFileDeposito1);

                //ACTUALIZAR LA SOLICITUD
                string vQuery = "GESTIONES_Solicitud 44,'" + vEx + "','" + vidEstado + "','" + TxDetalle.Text + "','" + Session["USUARIO"].ToString() + "','" + vArchivo + "','"+ escalacion+"'";
                Int32 vInfo1 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                //GUARDAR HISTORIAL
                vQuery = "GESTIONES_Solicitud 4,'" + vEx + "','" + vCambio + "','" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                vQuery = "GESTIONES_Solicitud 7,'" + vResponsableTarjeta + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();

                vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();
                Session["GESTIONES_NOMBRE_JEFE"] = vDatos.Rows[0]["nombreJefe"].ToString();
                Session["GESTIONES_NOMBRE_SUPLENTE"] = vDatos.Rows[0]["nombreSuplente"].ToString();

                //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                string vAsunto = "Tarjeta Kanban Finalizada, Gestiones Técnicas: " + vEx;
                string vCorreosCopia = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                string vQuery5 = "GESTIONES_Solicitud 5,'Tarjeta Kanban Finalizada','"
                 + Session["GESTIONES_CORREO_RESPONSABLE"].ToString()
                + "','" + vCorreosCopia + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + vEx + "'";
                Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);

               
                //if (vMinRestante != 0 )
                //{
                //    vQuery = "GESTIONES_Generales 67,'" + vEx + "','" + vFechaEntregaBusqueda + "'";
                //    Int32 vInfoActualizar = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                //    vQuery = "GESTIONES_Generales 66,'" + vidCargabilidadMin + "','" + vFechaCierraTarea + "','"+ vResponsableTarjeta+"','"+ vSumMinutos+"'";
                //    Int32 vInfoUpdateFechaActual = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                //}

                //CAMBIAR EL ESTADO DE LA CARGABILIDAD
                //CAMBIO vQuery = "GESTIONES_Solicitud 22,'" + vEx + "','" + Session["USUARIO"].ToString() + "','" + vEstadoCargabilidad + "'";
                //vQuery = "GESTIONES_Solicitud 35,'" + vidCargabilidadMin + "','" + Session["USUARIO"].ToString() + "','" + vEstadoCargabilidad + "'";
                //Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                if (vInfo2 == 1)
                {

                    TxDetalle.Text = "";
                    DdlAccion.SelectedIndex = -1;
                    vMensaje = "Tarjeta cerrada con éxito";
                    Mensaje(vMensaje, WarningType.Success);
                    Response.Redirect("/pages/miTablero.aspx");
                }

                limpiarCreacionTarea();

            }
            if (DdlAccion.SelectedValue == "3")
            {
               

                string vQuery = "GESTIONES_Solicitud 32,'" + vEx + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vidEstadoActual = vDatos.Rows[0]["idEstado"].ToString();

                //ACTUALIZAR LA SOLICITUD
                vQuery = "GESTIONES_Solicitud 31,'" + vEx + "','" + vidEstado + "','" + TxDetalle.Text + "','" + Session["USUARIO"].ToString() + "','" + vidEstadoActual + "','" + DdlMotivoEliminar.SelectedValue + "'";
                Int32 vInfo1 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                //GUARDAR HISTORIAL
                vQuery = "GESTIONES_Solicitud 4,'" + vEx + "','" + vCambio + "','" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                vQuery = "GESTIONES_Solicitud 7,'" + vResponsableTarjeta + "'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();

                vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();
                Session["GESTIONES_NOMBRE_JEFE"] = vDatos.Rows[0]["nombreJefe"].ToString();
                Session["GESTIONES_NOMBRE_SUPLENTE"] = vDatos.Rows[0]["nombreSuplente"].ToString();

                //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                string vAsunto = "Solicitud Tarjeta a Eliminar, Gestiones Técnicas: " + vEx;
                string vCorreo = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                string vQuery5 = "GESTIONES_Solicitud 5,'Solicitud Tarjeta Kanban a Eliminar','"
                 + vCorreo
                + "','" + Session["GESTIONES_CORREO_RESPONSABLE"].ToString() + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + vEx + "'";
                Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);

                //CAMBIAR EL ESTADO DE LA CARGABILIDAD
                vQuery = "GESTIONES_Solicitud 22,'" + vEx + "','" + Session["USUARIO"].ToString() + "','" + vEstadoCargabilidad + "'";
                Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);


                if (vInfo1 == 1)
                {
                    TxDetalle.Text = "";
                    DdlMotivoEliminar.SelectedIndex = -1;
                    vMensaje = "Solicitud Tarjeta a Eliminar con éxito";
                    Mensaje(vMensaje, WarningType.Success);
                    Response.Redirect("/pages/miTablero.aspx");
                }


            }
            else
            {
                string vArchivo = "";
                //ACTUALIZAR LA SOLICITUD
                string vQuery = "GESTIONES_Solicitud 16,'" + vEx + "','" + vidEstado + "','" + TxDetalle.Text + "','" + Session["USUARIO"].ToString() + "','" + vArchivo + "'";
                Int32 vInfo1 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                //GUARDAR HISTORIAL
                vQuery = "GESTIONES_Solicitud 4,'" + vEx + "','" + vCambio + "','" + Session["USUARIO"].ToString() + "'";
                Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                vQuery = "GESTIONES_Solicitud 7,'" + vResponsableTarjeta + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();

                vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();
                Session["GESTIONES_NOMBRE_JEFE"] = vDatos.Rows[0]["nombreJefe"].ToString();
                Session["GESTIONES_NOMBRE_SUPLENTE"] = vDatos.Rows[0]["nombreSuplente"].ToString();



                //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                string vAsunto = "Solicitud Tarjeta Kanban a Estado Detenida, Gestiones Técnicas: " + vEx;
                string vCorreo = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                string vQuery5 = "GESTIONES_Solicitud 5,'Solicitud Tarjeta Kanban a Detenido','"
                 + vCorreo
                + "','" + Session["GESTIONES_CORREO_RESPONSABLE"].ToString() + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + vEx + "'";
                Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);

                //CAMBIAR EL ESTADO DE LA CARGABILIDAD
                vQuery = "GESTIONES_Solicitud 22,'" + vEx + "','" + Session["USUARIO"].ToString() + "','" + vEstadoCargabilidad + "'";
                Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                if (vInfo1 == 1)
                {
                    TxDetalle.Text = "";
                    DdlAccion.SelectedIndex = -1;
                    Response.Redirect("/pages/miTablero.aspx");
                }
            }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }


        private void validacionesDetenerTarjeta()
        {

            if (DdlAccion.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione la acción a realizar.");

            if (TxDetalle.Text.Equals(""))
                throw new Exception("Falta que ingrese motivo por el cual está solicitando detener la tarjeta.");

            if (TxNewFechaInicio.Text.Equals(""))
                throw new Exception("Falta que ingrese la nueva fecha de inicio.");

            if (TxNewFechaEntrega.Text.Equals(""))
                throw new Exception("Falta que ingrese la nueva fecha de entrega.");

            DateTime fecha_inicio = DateTime.Parse(TxNewFechaInicio.Text.ToString());
            DateTime fecha_fin = DateTime.Parse(TxNewFechaEntrega.Text.ToString());

            string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
            string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");

            DateTime fecha_actual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            DateTime fecha_actualIngreso = Convert.ToDateTime(fecha_inicio.ToString("dd/MM/yyyy HH:mm"));
            DateTime fecha_actualEntrega = Convert.ToDateTime(fecha_fin.ToString("dd/MM/yyyy HH:mm"));


            if (fecha_actualIngreso > fecha_actualEntrega)
                throw new Exception("Favor verificar la nueva fecha de inicio, no puede ser mayor que la nueva fecha de entrega");

            if (fecha_actualEntrega < fecha_actualIngreso)
                throw new Exception("Favor verificar la nueva fecha de entrega, no puede ser menor que la nueva fecha de inicio");
        }

        protected void DdlAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlAccion.SelectedValue == "1")
            {
                divNuevasFechas.Visible = false;
                divSolucionAdjunto.Visible = true;
                divMotivoEliminar.Visible = false;

            }
            else if (DdlAccion.SelectedValue == "3")
            {
                divNuevasFechas.Visible = false;
                divSolucionAdjunto.Visible = false;
                divMotivoEliminar.Visible = true;
            }
            else
            {

                string vEx = "";
                vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();

                string vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vUsuarioCreo = vDatos.Rows[0]["usuarioCreo"].ToString();
                string vResponsable = Session["USUARIO"].ToString();

                if (vUsuarioCreo == vResponsable)
                {
                    divNuevasFechas.Visible = true;
                    divSolucionAdjunto.Visible = false;
                }
                else
                {
                    divNuevasFechas.Visible = false;
                    divSolucionAdjunto.Visible = false;
                }

            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////Todo Relacionado Tarjeta Operativa////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void BtnAddOperativa_Click(object sender, EventArgs e)
        {

            CkTiempoIlimitado.SelectedValue = "1";
            LbTituloCrearOperativa.Text = "Crear Tarjeta Kanban Operativa";
            UpdatePanel22.Update();
            cargarInicialTarjetaOperativa();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCrearOpeOpen();", true);
        }


        void cargarInicialTarjetaOperativa()
        {
            try
            {
                string vIdRol = Session["ID_ROL_USUARIO"].ToString();
                TxFechaSolicitudOperativa.Text = Convert.ToString(DateTime.Now);

                String vQuery = "";
                if (vIdRol == "1")
                {
                    DdlResponsableOperativa.Items.Clear();
                    DdlResponsableOperativa.Items.Clear();
                    vQuery = "GESTIONES_Solicitud 28,'" + Session["USUARIO"].ToString() + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlResponsableOperativa.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlResponsableOperativa.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });                           
                        }
                    }

                }
                else if (vIdRol == "3" || vIdRol == "4" || vIdRol == "5")
                {
                    DdlResponsableOperativa.Items.Clear();
                    vQuery = "GESTIONES_Solicitud 26";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlResponsableOperativa.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlResponsableOperativa.Items.Add(new ListItem { Value = item["CodEmpleado"].ToString(), Text = item["nombre"].ToString() });                          
                        }
                    }
                }
                else if (vIdRol == "2")
                {
                    DdlResponsableOperativa.Items.Clear();
                    vQuery = "GESTIONES_Generales 38,'" + Session["USUARIO"].ToString() + "'";
                    DataTable vDatosResponsables = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlResponsableOperativa.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatosResponsables.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatosResponsables.Rows)
                        {
                            DdlResponsableOperativa.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }
                }


                //DdlMotivoEliminar.Items.Clear();
                //vQuery = "GESTIONES_Solicitud 29";
                //DataTable vDatosEliminarTarjeta = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                //DdlMotivoEliminar.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });

                //if (vDatosEliminarTarjeta.Rows.Count > 0)
                //{
                //    foreach (DataRow item in vDatosEliminarTarjeta.Rows)
                //    {
                //        DdlMotivoEliminar.Items.Add(new ListItem { Value = item["idMotivo"].ToString(), Text = item["motivo"].ToString() });
                //    }
                //}
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        void validacionesCrearSolicitudOperativa() { }
        protected void DdlFrecuencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlFrecuencia.SelectedValue == "1")
            {
                DivFrecuenciaDiaria.Visible = true;
            }
            else
            {
                DivFrecuenciaDiaria.Visible = false;
            }
        }

        protected void DdlResponsableOperativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsableOperativa.SelectedValue + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
                Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

                DdlGestionOperativa.Items.Clear();
                DdlGestionOperativa.Enabled = true;
                vQuery = "GESTIONES_Generales 1,'" + Session["GESTIONES_TEAMS"].ToString() + "'";
                DataTable vDatosTipo = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlGestionOperativa.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatosTipo.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosTipo.Rows)
                    {
                        DdlGestionOperativa.Items.Add(new ListItem { Value = item["idTipoGestion"].ToString(), Text = item["nombreGestion"].ToString() });
                    }
                }
                vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }

        protected void BtnEnviarOperativa_Click(object sender, EventArgs e)
        {
            try
            {
                validacionesCrearSolicitudOperativa();//PENDIENTE no se ha creado

                //GVDistribucionOperativa.DataSource = null;
                //GVDistribucionOperativa.DataBind();
                //Session["GESTIONES_TAREAS_MIN_DIARIOS"] = null;
                //DateTime fecha_inicio = DateTime.Parse(TxFechaInicioOperativa.Text.ToString());
                ////DateTime fecha_fin = DateTime.Parse(TxFechaEntregaOperativa.Text.ToString());

                //String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
                //String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
                ////String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

                //DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                //DateTime vFechaInicio = DateTime.Parse(vFecha1);

                //double vMinDiarios = 0;
                //double vWip = Convert.ToInt32(Session["GESTIONES_WIP"].ToString());

                //DataTable vData = new DataTable();
                //DataTable vDatosMin = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
                //vData.Columns.Add("id");
                //vData.Columns.Add("fecha");
                //vData.Columns.Add("min");
                //string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
                ////string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");
                //DateTime vFechaInicioConver = DateTime.Parse(vFechaInicioSoli);
                ////DateTime vFechaFinConver = DateTime.Parse(vFechaFinSoli);

                //string vHrInicialSoli = fecha_inicio.ToString("HH:mm");
                ////string vHrFinalSoli = fecha_fin.ToString("HH:mm");

                //TimeSpan vHrInicialSoliConver = TimeSpan.Parse(vHrInicialSoli);
                ////TimeSpan vHrFinSoliConver = TimeSpan.Parse(vHrFinalSoli);




                //Session["GESTIONES_TAREAS_MIN_DIARIOS"] = vDatosMin;
                //GVDistribucionOperativa.DataSource = vDatosMin;
                //GVDistribucionOperativa.DataBind();


                cargarModalOperativa();
          
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCrearOpeOpenConfirmar();", true);

            }
            catch (Exception ex)
            {
                LbAdvertencia.InnerText = ex.Message;
                divAlertaGeneral.Visible = true;
            }
        }



        //void calculoDiasOperativas()
        //{
        //    DateTime fecha_inicio = DateTime.Parse(TxFechaInicioOperativa.Text.ToString());
        //    DateTime fecha_fin = DateTime.Parse(TxFechaEntregaOperativa.Text.ToString());

        //    String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
        //    String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);

        //    DateTime vFechaInicio = DateTime.Parse(vFecha1);

        //    string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
        //    string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");
        //    DateTime vFechaInicioConver = DateTime.Parse(vFechaInicioSoli);
        //    DateTime vFechaFinConver = DateTime.Parse(vFechaFinSoli);

        //    TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
        //    int businessDays = span.Days;
        //    int fullWeekCount = businessDays / 7;

        //    if (businessDays == 7)
        //    {
        //        businessDays = businessDays - 2;
        //    }
        //    else if (businessDays == 6)
        //    {
        //        businessDays = businessDays - 1;
        //    }
        //    else if (businessDays > fullWeekCount * 7)
        //    {
        //        int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
        //        int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
        //        if (lastDayOfWeek < firstDayOfWeek)
        //            lastDayOfWeek += 7;
        //        if (firstDayOfWeek <= 6)
        //        {
        //            if (lastDayOfWeek >= 7)
        //                businessDays -= 2;
        //            else if (lastDayOfWeek >= 6)
        //                businessDays -= 1;
        //        }
        //        else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
        //            businessDays -= 1;
        //    }
        //    //subtract the weekends during the full weeks in the interval
        //    businessDays -= fullWeekCount + fullWeekCount;

        //    if (businessDays < 1)
        //    {
        //        businessDays = businessDays * -1;
        //    }
        //    else
        //    {
        //        businessDays = businessDays;
        //    }

        //    int vDias = businessDays + 1;
        //    Session["GESTIONES_DIAS"] = vDias;
        //}


        private void cargarModalOperativa()
        {
            LbTituloConfirmarOpe.Text = "Crear Tarjeta Operativa para: " + DdlResponsableOperativa.SelectedItem.ToString();
            UpdatePanel27.Update();
            TxTituloConfirmarOpe.Text = TxTituloOperativa.Text;
            TxPrioridadConfirmarOpe.Text = DdlPrioridadOperativa.SelectedItem.ToString();
            TxTiempoConfirmarOpe.Text = TxMinProductivoOperativa.Text + " Mins";
            TxInicioConfirmarOpe.Text = TxFechaInicioOperativa.Text;
            TxEntregaConfirmarOpe.Text = TxDiasEntregaOperativa.Text;

            UpdatePanel28.Update();
        }

        protected void CkTiempoIlimitado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CkTiempoIlimitado.SelectedValue == "")
            {
                TxFechaFinTarjeta.Visible = true;
                TxNoVence.Visible = false;
            }
            else
            {
                TxFechaFinTarjeta.Visible = false;
                TxNoVence.Visible = true;
            }
        }

        protected void BtnOpcion_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxOpcion.Text == "" || TxOpcion.Text == string.Empty)
                    throw new Exception("El campo de Opción está vacio.");

                if (DdlTipoOpcion.SelectedValue.Equals("0"))
                    throw new Exception("Falta que seleccione tipo de dato.");

                LbOpcionAlerta.Visible = false;

                    int numRows = 0;
                    if (Session["GESTIONES_OPCIONES_LISTA"] == null)
                    {
                        numRows = 0;
                    }
                    else
                    {
                        numRows = GvOpcionesLista.Rows.Count;
                    }

                    DataTable vData = new DataTable();
                    DataTable vDatos = new DataTable();
                    vData.Columns.Add("idOpcion");
                    vData.Columns.Add("pregunta");
                    vData.Columns.Add("tipo");

                    string vConteo = "";
                    if (numRows > 0)
                    {
                        vDatos = (DataTable)Session["GESTIONES_OPCIONES_LISTA"];
                        Session["GESTIONES_CONTEO_LISTA"] = Convert.ToInt32(Session["GESTIONES_CONTEO_LISTA"]) + 1;
                        vConteo = Session["GESTIONES_CONTEO_LISTA"].ToString();
                        vDatos.Rows.Add(vConteo, TxOpcion.Text, DdlTipoOpcion.SelectedItem);
                    }
                    else
                    {
                        vDatos = vData.Clone();

                        Session["GESTIONES_CONTEO_LISTA"] = "1";
                        vConteo = "1";
                        vDatos.Rows.Add(vConteo, TxOpcion.Text, DdlTipoOpcion.SelectedItem);
                    }

                GvOpcionesLista.DataSource = vDatos;
                GvOpcionesLista.DataBind();
                Session["GESTIONES_OPCIONES_LISTA"] = vDatos;
               

                TxOpcion.Text = "";
                DdlTipoOpcion.SelectedIndex = -1;
                UpdatePanel31.Update();
            }
            catch (Exception ex)
            {
                LbOpcionAlerta.InnerText = ex.Message;

                divAlertaOpcion.Visible = true;
                LbOpcionAlerta.Visible = true;
            }
        }

        protected void GvOpcionesLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    string vId = e.CommandArgument.ToString();
                    if (Session["GESTIONES_OPCIONES_LISTA"] != null)
                    {
                        DataTable vDatos = (DataTable)Session["GESTIONES_OPCIONES_LISTA"];
                        DataRow[] result = vDatos.Select("idOpcion = '" + vId + "'");
                        foreach (DataRow row in result)
                        {
                            if (row["idOpcion"].ToString().Contains(vId))
                            {
                                //LLENAR TABLA DE DATOS A ELIMINAR
                                vDatos.Rows.Remove(row);
                            }
                        }

                        GvOpcionesLista.DataSource = vDatos;
                        GvOpcionesLista.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvOpcionesLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvOpcionesLista.PageIndex = e.NewPageIndex;
                GvOpcionesLista.DataSource = (DataTable)Session["GESTIONES_OPCIONES_LISTA"];
                GvOpcionesLista.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnPesonalAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (DdlNotificar.SelectedValue.Equals("0"))
                    throw new Exception("Falta que seleccione la persona a quien quiere notificar.");

                LbNotificacionAlerta.Visible = false;

                int numRows = 0;
                if (Session["GESTIONES_NOTIFICACION_LISTA"] == null)
                {
                    numRows = 0;
                }
                else
                {
                    numRows = GvNotificacion.Rows.Count;
                }

                DataTable vData = new DataTable();
                DataTable vDatos = new DataTable();
                vData.Columns.Add("idNotificacion");
                vData.Columns.Add("usuario");               
                vData.Columns.Add("nombre");

                string vConteo = "";
                if (numRows > 0)
                {
                    vDatos = (DataTable)Session["GESTIONES_NOTIFICACION_LISTA"];
                    Session["GESTIONES_CONTEO_NOTIFICACION"] = Convert.ToInt32(Session["GESTIONES_CONTEO_NOTIFICACION"]) + 1;
                    vConteo = Session["GESTIONES_CONTEO_NOTIFICACION"].ToString();
                    vDatos.Rows.Add(vConteo, DdlNotificar.SelectedValue, DdlNotificar.SelectedItem);
                }
                else
                {
                    vDatos = vData.Clone();

                    Session["GESTIONES_CONTEO_NOTIFICACION"] = "1";
                    vConteo = "1";
                    vDatos.Rows.Add(vConteo, DdlNotificar.SelectedValue, DdlNotificar.SelectedItem);
                }

                GvNotificacion.DataSource = vDatos;
                GvNotificacion.DataBind();
                Session["GESTIONES_NOTIFICACION_LISTA"] = vDatos;

                DdlNotificar.SelectedIndex = -1;
                UpdatePanel30.Update();
            }
            catch (Exception ex)
            {
                LbNotificacionAlerta.InnerText = ex.Message;

                divAlertaNotificacion.Visible = true;
                LbNotificacionAlerta.Visible = true;
            }
        }

        protected void GvNotificacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvNotificacion.PageIndex = e.NewPageIndex;
                GvNotificacion.DataSource = (DataTable)Session["GESTIONES_NOTIFICACION_LISTA"];
                GvNotificacion.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvNotificacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    string vId = e.CommandArgument.ToString();
                    if (Session["GESTIONES_NOTIFICACION_LISTA"] != null)
                    {
                        DataTable vDatos = (DataTable)Session["GESTIONES_NOTIFICACION_LISTA"];
                        DataRow[] result = vDatos.Select("idNotificacion = '" + vId + "'");
                        foreach (DataRow row in result)
                        {
                            if (row["idNotificacion"].ToString().Contains(vId))
                            {
                                //LLENAR TABLA DE DATOS A ELIMINAR
                                vDatos.Rows.Remove(row);
                            }
                        }

                        GvNotificacion.DataSource = vDatos;
                        GvNotificacion.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnAddComenOperativa_Click(object sender, EventArgs e)
        {
            try
            {
                string vUsuarioAD = Session["USUARIO_AD"].ToString();
                divAlertaComentarioOperativa.Visible = false;
                      
                    if (TxComentarioOperativa.Text == "" || TxComentarioOperativa.Text == string.Empty)
                        throw new Exception("El campo de comentario está vacío.");

                    string hora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    int numRows = 0;
                    if (Session["GESTIONES_TAREAS_COMENTARIOS_OPERATIVAS"] == null)
                    {
                        numRows = 0;
                    }
                    else
                    {
                        numRows = GvComentariosOperativas.Rows.Count;
                    }


                    DataTable vData = new DataTable();
                    DataTable vDatos = new DataTable();
                    vData.Columns.Add("idComentario");
                    vData.Columns.Add("usuario");
                    vData.Columns.Add("comentario");

                    string vConteo = "";
                    if (numRows > 0)
                    {
                        vDatos = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS_OPERATIVAS"];

                        Session["GESTIONES_CONTEO_COMENTARIO_OPERATIVAS"] = Convert.ToInt32(Session["GESTIONES_CONTEO_COMENTARIO_OPERATIVAS"]) + 1;
                        vConteo = Session["GESTIONES_CONTEO_COMENTARIO_OPERATIVAS"].ToString();
                        vDatos.Rows.Add(vConteo, vUsuarioAD + ' ' + hora, TxComentarioOperativa.Text);
                    }
                    else
                    {
                        vDatos = vData.Clone();

                        Session["GESTIONES_CONTEO_COMENTARIO_OPERATIVAS"] = "1";
                        vConteo = "1";
                        vDatos.Rows.Add(vConteo, vUsuarioAD + ' ' + hora, TxComentarioOperativa.Text);

                    }

                GvComentariosOperativas.DataSource = vDatos;
                GvComentariosOperativas.DataBind();
                Session["GESTIONES_TAREAS_COMENTARIOS_OPERATIVAS"] = vDatos;
                divComentarioOperativa.Visible = true;
                TxComentarioOperativa.Text = "";
                
            }
            catch (Exception ex)
            {
                divAlertaComentarioOperativa.InnerText = ex.Message;
                divComentarioOperativa.Visible = true;
            }
        }

        protected void GvComentariosOperativas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Eliminar")
                {
                    string vId = e.CommandArgument.ToString();
                    if (Session["GESTIONES_TAREAS_COMENTARIOS_OPERATIVAS"] != null)
                    {
                        DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS_OPERATIVAS"];
                        DataRow[] result = vDatos.Select("idComentario = '" + vId + "'");
                        foreach (DataRow row in result)
                        {
                            if (row["idComentario"].ToString().Contains(vId))
                            {
                                //LLENAR TABLA DE DATOS A ELIMINAR
                                vDatos.Rows.Remove(row);
                            }
                        }

                        GvComentariosOperativas.DataSource = vDatos;
                        GvComentariosOperativas.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvComentariosOperativas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvComentariosOperativas.PageIndex = e.NewPageIndex;
                GvComentariosOperativas.DataSource = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS_OPERATIVAS"];
                GvComentariosOperativas.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

  

        protected void BtnEnviarOperativaConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string vIlimitado = "";
                string vLunes = "";
                string vMartes = "";
                string vMiercoles = "";
                string vJueves = "";
                string vViernes = "";
                string vSabado = "";
                string vDomingo = "";


                if (CkTiempoIlimitado.SelectedValue == "1")
                    vIlimitado = "Si";
                else
                    vIlimitado = "No";


                vDomingo = ckDomingo.SelectedValue == "1" ? "Si" : "No";
                vLunes = ckLunes.SelectedValue == "1" ? "Si" : "No";
                vMartes = ckMartes.SelectedValue == "1" ? "Si" : "No";
                vMiercoles = ckMiercoles.SelectedValue == "1" ? "Si" : "No";
                vJueves = ckJueves.SelectedValue == "1" ? "Si" : "No";
                vViernes = ckViernes.SelectedValue == "1" ? "Si" : "No";
                vSabado = ckSabado.SelectedValue == "1" ? "Si" : "No";



                DateTime fecha_inicio = DateTime.Parse(TxFechaInicioOperativa.Text.ToString());
                DateTime vFechaInicio = Convert.ToDateTime(fecha_inicio.ToString("dd/MM/yyyy HH:mm"));

                int vInfo1 = 1;
                String vQuery1 = "GESTIONES_SolicitudOperaciones 1,'" 
                                   + TxTituloOperativa.Text
                                   + "','" + TxMinProductivoOperativa.Text
                                   + "','" + TxDiasEntregaOperativa.Text
                                   + "','" + TxDescripcionOperativa.Text
                                   + "','" + DdlResponsableOperativa.SelectedValue
                                   + "','" + DdlPrioridadOperativa.SelectedValue
                                   + "','" + DdlGestionOperativa.SelectedValue
                                   + "','" + Session["USUARIO"].ToString()
                                   + "','" + Session["GESTIONES_TEAMS"].ToString()
                                   + "','" + vIlimitado
                                   + "','" + TxFechaSolicitudOperativa.Text
                                   + "','" + TxFechaFinTarjeta.Text
                                   + "','" + DdlFrecuencia.SelectedValue
                                   + "','" + vFechaInicio + "',1,'" + vDomingo +"','"+ vLunes + "','" + vMartes + "','" + vMiercoles + "','" + vJueves + "','" + vViernes + "','" + vSabado+"'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery1);
                string idSolicitud = vDatos.Rows[0]["idOperativa"].ToString();



                DataTable vDatosComentariosOperativas = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS_OPERATIVAS"];
                if (vDatosComentariosOperativas != null)
                {
                    for (int num = 0; num < vDatosComentariosOperativas.Rows.Count; num++)
                    {
                        string usuario = vDatosComentariosOperativas.Rows[num]["usuario"].ToString();
                        string comentario = vDatosComentariosOperativas.Rows[num]["comentario"].ToString();

                        String vQuery2 = "GESTIONES_SolicitudOperaciones 2,'" + idSolicitud +
                            "','" + comentario +
                            "','" + usuario + "'";
                        Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery2);
                    }
                }

                
                DataTable vDatosNotificarOperativa = (DataTable)Session["GESTIONES_NOTIFICACION_LISTA"];
                if (vDatosNotificarOperativa != null)
                {
                    for (int num = 0; num < vDatosNotificarOperativa.Rows.Count; num++)
                    {
                        string usuario = vDatosNotificarOperativa.Rows[num]["usuario"].ToString();

                        String vQuery2 = "GESTIONES_SolicitudOperaciones 3,'" + idSolicitud +
                            "','" + usuario +
                            "',1";
                        Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery2);
                    }
                }

      
                DataTable vDatosCheckListOperativa = (DataTable)Session["GESTIONES_OPCIONES_LISTA"];
                if (vDatosCheckListOperativa != null)
                {
                    for (int num = 0; num < vDatosCheckListOperativa.Rows.Count; num++)
                    {
                        string pregunta = vDatosCheckListOperativa.Rows[num]["pregunta"].ToString();
                        string tipo = vDatosCheckListOperativa.Rows[num]["tipo"].ToString();

                        String vQuery2 = "GESTIONES_SolicitudOperaciones 4,'" + idSolicitud +
                            "','" + pregunta +  "','"+ tipo +  "',1";
                        Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery2);
                    }
                }



                if (vInfo1 == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCrearOpeClose();", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCrearOpeCloseConfirmar();", true);
                    Response.Redirect("/pages/miTablero.aspx");
                }
          

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        {          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataRowView vData = (DataRowView)e.Row.DataItem;
                DataRow vDataRow = (DataRow)vData.Row;
                string vTipo = e.Row.Cells[1].Text;


                TextBox vPaso1 = (TextBox)e.Row.FindControl("TxRespuesta");
                //HtmlInputFile vPaso2 = (HtmlInputFile)e.Row.FindControl("FuRespuesta");
                FileUpload vPaso2 = (FileUpload)e.Row.FindControl("FuRespuesta");

                switch (vTipo)
                {
             
                    case "Texto":
                        vPaso1.Visible = true;
                        vPaso2.Visible = false;
                        break;
                    case "Imagen":
                        vPaso1.Visible = false;
                        vPaso2.Visible = true;
                        break;
                   
                };


                e.Row.Cells[1].Visible = false;











                //string vTipo = e.Row.Cells[0].Text;

                //if (vTipo.Equals("Texto"))
                //{
                //    e.Row.Cells[2].Controls[0].Visible= true;
                //    e.Row.Cells[2].Controls[1].Visible = false;
                //    UpdatePanel32.Update();
                //}      
                //else 
                //{
                //    e.Row.Cells[2].Controls[0].Visible = false;
                //    e.Row.Cells[2].Controls[1].Visible = true;
                //    UpdatePanel32.Update();

                //}


            }
            
        }

        protected void BtnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                String vEx = null;
                vEx = Session["GESTIONES_ID_TARJETA_CERRAR"].ToString();

                string vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                string vResponsableTarjeta = vDatos.Rows[0]["responsable"].ToString();
                string vNombreResponsableTarjeta = vDatos.Rows[0]["nombre"].ToString();
                string vidOperativa = vDatos.Rows[0]["idOperativa"].ToString();

                Session["GESTIONES_RESPONSABLE_TARJETA_CERRAR"] = vResponsableTarjeta;
                Session["GESTIONES_NOMBRE_RESPONSABLE_TARJETA_CERRAR"] = vNombreResponsableTarjeta;


                if (DdlAccion.SelectedValue == "1")
                {
                    validacionesCerrarTarea();

                    //Validar si el checkList esta vacio
                    int num = 0;

                    if (vidOperativa != null && vidOperativa != "")
                    {
                        DataTable vDatosCheckListVerificar = (DataTable)Session["GESTIONES_VERIFICACION"];
                        if (vDatosCheckListVerificar != null)
                        {
                            foreach (GridViewRow row in GvCheckList.Rows)
                            {
                                string tipo = vDatosCheckListVerificar.Rows[num]["tipo"].ToString();
                                TextBox vValidacionTexto = (TextBox)row.Cells[2].FindControl("TxRespuesta");
                                string vContenidoValidacionTexto = vValidacionTexto.Text;

                                TextBox vValidacionImagen = (TextBox)row.Cells[3].FindControl("txtEvtTo");
                                string vContenidoValidacionImagen = vValidacionImagen.Text;


                                if (tipo == "Texto" && vContenidoValidacionTexto == "")
                                    throw new Exception("Favor completar todas las preguntas de la lista de verificacion");

                                if (tipo == "Imagen" && vContenidoValidacionImagen == "No")
                                    throw new Exception("Favor completar todas las preguntas de la lista de verificacion");

                                num = num + 1;
                            }
                        }
                    }

                    LbTituloCerrar.Text = "Está seguro de " + DdlAccion.SelectedItem.Text + ": " + vEx;
                    UpdatePanel14.Update();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCerrarOpen();", true);

                }
                else if (DdlAccion.SelectedValue == "3")
                {
                    validacionesCerrarTarea();
                    if (DdlMotivoEliminar.SelectedValue.Equals("0"))
                        throw new Exception("Falta que seleccione motivo por el cúal solicita eliminar la tarjeta.");
                }
                else
                {

                    validacionesCerrarTarea();
                    vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string vUsuarioCreo = vDatos.Rows[0]["usuarioCreo"].ToString();
                    string vResponsable = Session["USUARIO"].ToString();

                    if (vUsuarioCreo == vResponsable)
                    {
                        validacionesDetenerTarjeta();
                        cargarModalDetener();

                        GVDistribucion.DataSource = null;
                        GVDistribucion.DataBind();
                        Session["GESTIONES_TAREAS_MIN_DIARIOS"] = null;
                        DateTime fecha_inicio = DateTime.Parse(TxNewFechaInicio.Text.ToString());
                        DateTime fecha_fin = DateTime.Parse(TxNewFechaEntrega.Text.ToString());

                        String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
                        String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
                        String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

                        DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        DateTime vFechaInicio = DateTime.Parse(vFecha1);


                        vQuery = "GESTIONES_Solicitud 7,'" + vResponsableTarjeta + "'";
                        vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                        Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
                        Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

                        vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                        vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                        Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();

                        double vMinDiarios = 0;
                        double vWip = Convert.ToInt32(Session["GESTIONES_WIP"].ToString());

                        DataTable vData = new DataTable();
                        DataTable vDatosMin = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
                        vData.Columns.Add("id");
                        vData.Columns.Add("fecha");
                        vData.Columns.Add("min");
                        string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
                        string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");
                        DateTime vFechaInicioConver = DateTime.Parse(vFechaInicioSoli);
                        DateTime vFechaFinConver = DateTime.Parse(vFechaFinSoli);

                        string vHrInicialSoli = fecha_inicio.ToString("HH:mm");
                        string vHrFinalSoli = fecha_fin.ToString("HH:mm");

                        TimeSpan vHrInicialSoliConver = TimeSpan.Parse(vHrInicialSoli);
                        TimeSpan vHrFinSoliConver = TimeSpan.Parse(vHrFinalSoli);


                        DateTime vfechaActualDetener = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                        string vfechaActualDetenerEvaluar = vfechaActualDetener.ToString("dd/MM/yyyy");



                        vQuery = "GESTIONES_Generales 62,'" + vResponsableTarjeta + "','" + vEx + "'";
                        vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        string vMinFaltantesDetener = vDatos.Rows[0]["minDiariosFaltantes"].ToString();

                        if (vFechaInicio.DayOfWeek != DayOfWeek.Saturday && vFechaInicio.DayOfWeek != DayOfWeek.Sunday)
                        {
                            TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
                            int businessDays = span.Days;
                            int fullWeekCount = businessDays / 7;

                            if (businessDays == 7)
                            {
                                businessDays = businessDays - 2;
                            }
                            else if (businessDays == 6)
                            {
                                businessDays = businessDays - 1;
                            }
                            else if (businessDays > fullWeekCount * 7)
                            {
                                int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
                                int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
                                if (lastDayOfWeek < firstDayOfWeek)
                                    lastDayOfWeek += 7;
                                if (firstDayOfWeek <= 6)
                                {
                                    if (lastDayOfWeek >= 7)
                                        businessDays -= 2;
                                    else if (lastDayOfWeek >= 6)
                                        businessDays -= 1;
                                }
                                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                                    businessDays -= 1;

                                //subtract the weekends during the full weeks in the interval
                                businessDays -= fullWeekCount + fullWeekCount;
                            }
                            int vDias = businessDays + 1;
                            Session["GESTIONES_DIAS"] = vDias;
                            vMinDiarios = Convert.ToInt32(vMinFaltantesDetener) / vDias;

                            int vCount = 0;
                            int vResta = 0;

                            double vMinsFaltante = 0;

                            if (vDias == 1)
                            {
                                if (vDatosMin == null)
                                    vDatosMin = vData.Clone();
                                if (vDatosMin != null)
                                {
                                    vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                                    vDatosMin.Rows.Add("1", vFechaInicioSoli, vMinDiarios);
                                }
                            }
                            else
                            {

                                //VALIDACION INICIO TAREA SI TIENE MIN DISPNIBLES
                                string vCantMinSolicitudes = "";
                                string vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaInicioSoli + "'";
                                DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();

                                if (vCantMinSolicitudes != "")
                                {
                                    if (Convert.ToDouble(vCantMinSolicitudes) >= Convert.ToDouble(vWip))
                                        throw new Exception("Nota: La fecha seleccionada inicio de la tarjeta ya no cuenta con mins disponibles, su WIP está al limite, favor cambiar la fecha de inicio para poder realizar una mejor distribución de su cargabilidad. Minutos registrados de cargabilidad: " + vCantMinSolicitudes + ", WIP límite establecido: " + vWip);
                                }


                                for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                                {
                                    if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                                    {
                                        vCount = vCount + 1;
                                        vResta = (vDias - vCount) + 1;

                                        if (vMinsFaltante != 0)
                                        {
                                            vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                            vMinsFaltante = 0;
                                        }

                                        string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                        vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaEvaluar + "'";
                                        vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                        vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                        double vSobranteWIPCreacion = 0;

                                        if (vCantMinSolicitudes.Equals(""))
                                        {
                                            vSobranteWIPCreacion = vWip;
                                        }
                                        else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                        {
                                            vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                        }
                                        else
                                        {
                                            vSobranteWIPCreacion = 0;
                                        }


                                        if (vMinDiarios <= vSobranteWIPCreacion)
                                        {
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                            vMinDiarios = vSobranteWIPCreacion;
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (vMinsFaltante != 0)
                                throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

                        }
                        else if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday || vFechaInicio.DayOfWeek == DayOfWeek.Sunday)
                        {
                            TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
                            int businessDays = span.Days;
                            int fullWeekCount = businessDays / 7;

                            if (businessDays == 7)
                            {
                                businessDays = businessDays - 2;
                            }
                            else if (businessDays == 6)
                            {
                                businessDays = businessDays - 1;
                            }
                            else if (businessDays > fullWeekCount * 7)
                            {
                                int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
                                int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
                                if (lastDayOfWeek < firstDayOfWeek)
                                    lastDayOfWeek += 7;
                                if (firstDayOfWeek <= 6)
                                {
                                    if (lastDayOfWeek >= 7)
                                        businessDays -= 2;
                                    else if (lastDayOfWeek >= 6)
                                        businessDays -= 1;
                                }
                                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                                    businessDays -= 1;

                                //subtract the weekends during the full weeks in the interval
                                businessDays -= fullWeekCount + fullWeekCount;
                            }
                            int vDias = businessDays + 1;
                            Session["GESTIONES_DIAS"] = vDias;

                            LbDiaNoHabil.Text = "Se debe iniciar a trabajar en la tarjeta un día de trabajo no hábil";
                            divDiaNoHabil.Visible = true;
                            if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday)
                            {
                                if (vDias == 1)
                                {
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                                        vDatosMin.Rows.Add("1", vFechaInicioSoli, vMinFaltantesDetener);
                                    }
                                }
                                else
                                {
                                    vDias = vDias + 2;
                                    vMinDiarios = Convert.ToInt32(vMinFaltantesDetener) / vDias;
                                    DateTime vFechaFinConverDomingo = vFechaInicioConver.AddDays(1);
                                    DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
                                    int vCount = 0;
                                    int vResta = 0;
                                    double vMinsFaltante = 0;

                                    for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
                                    {
                                        string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                        if (vDatosMin == null)
                                            vDatosMin = vData.Clone();
                                        if (vDatosMin != null)
                                        {
                                            if (vDatosMin.Rows.Count < 1)
                                            {
                                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                            }
                                            else
                                            {
                                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                            }
                                        }

                                    }

                                    for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                                    {
                                        if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                                        {
                                            vCount = vCount + 1;
                                            vResta = (vDias - vCount) + 1;

                                            if (vMinsFaltante != 0)
                                            {
                                                vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                                vMinsFaltante = 0;
                                            }

                                            string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                            string vCantMinSolicitudes = "";
                                            string vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaEvaluar + "'";
                                            DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                            vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                            double vSobranteWIPCreacion = 0;

                                            if (vCantMinSolicitudes.Equals(""))
                                            {
                                                vSobranteWIPCreacion = vWip;
                                            }
                                            else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                            {
                                                vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                            }
                                            else
                                            {
                                                vSobranteWIPCreacion = 0;
                                            }


                                            if (vMinDiarios <= vSobranteWIPCreacion)
                                            {
                                                if (vDatosMin == null)
                                                    vDatosMin = vData.Clone();
                                                if (vDatosMin != null)
                                                {
                                                    if (vDatosMin.Rows.Count < 1)
                                                    {
                                                        vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                    }
                                                    else
                                                    {
                                                        vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                                vMinDiarios = vSobranteWIPCreacion;
                                                if (vDatosMin == null)
                                                    vDatosMin = vData.Clone();
                                                if (vDatosMin != null)
                                                {
                                                    if (vDatosMin.Rows.Count < 1)
                                                    {
                                                        vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                    }
                                                    else
                                                    {
                                                        vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (vMinsFaltante != 0)
                                        throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);
                                }
                            }
                            else
                            {
                                vDias = vDias;
                                vMinDiarios = Convert.ToInt32(vMinFaltantesDetener) / vDias;
                                DateTime vFechaFinConverDomingo = vFechaInicioConver;
                                DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
                                int vCount = 0;
                                int vResta = 0;
                                double vMinsFaltante = 0;

                                for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
                                {
                                    string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                    if (vDatosMin == null)
                                        vDatosMin = vData.Clone();
                                    if (vDatosMin != null)
                                    {
                                        if (vDatosMin.Rows.Count < 1)
                                        {
                                            vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                        }
                                        else
                                        {
                                            vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                        }
                                    }

                                }

                                for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                                {
                                    if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
                                    {
                                        vCount = vCount + 1;
                                        vResta = (vDias - vCount) + 1;

                                        if (vMinsFaltante != 0)
                                        {
                                            vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                            vMinsFaltante = 0;
                                        }

                                        string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                        string vCantMinSolicitudes = "";
                                        string vQuerys = "GESTIONES_Solicitud 9,'" + vResponsableTarjeta + "','" + vFechaEvaluar + "'";
                                        DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                        vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                                        double vSobranteWIPCreacion = 0;

                                        if (vCantMinSolicitudes.Equals(""))
                                        {
                                            vSobranteWIPCreacion = vWip;
                                        }
                                        else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
                                        {
                                            vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
                                        }
                                        else
                                        {
                                            vSobranteWIPCreacion = 0;
                                        }


                                        if (vMinDiarios <= vSobranteWIPCreacion)
                                        {
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
                                            vMinDiarios = vSobranteWIPCreacion;
                                            if (vDatosMin == null)
                                                vDatosMin = vData.Clone();
                                            if (vDatosMin != null)
                                            {
                                                if (vDatosMin.Rows.Count < 1)
                                                {
                                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
                                                }
                                                else
                                                {
                                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
                                                }
                                            }
                                        }
                                    }
                                }

                                if (vMinsFaltante != 0)
                                    throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

                            }




                        }


                        Session["GESTIONES_TAREAS_MIN_DIARIOS"] = vDatosMin;
                        GVDistribucion.DataSource = vDatosMin;
                        GVDistribucion.DataBind();


                        TxTimeModal.Text = vMinFaltantesDetener + " Mins";
                        TxEntregaModal.Text = TxNewFechaEntrega.Text;
                        TxInicioModal.Text = TxNewFechaInicio.Text;
                        UpdatePanel6.Update();

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaConfirmarOpen();", true);
                    }
                    else
                    {
                        validacionesCerrarTarea();
                        LbTituloCerrar.Text = "Está seguro de la " + DdlAccion.SelectedItem.Text + ": " + vEx;
                        UpdatePanel14.Update();
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaCerrarOpen();", true);
                    }

                }



                BtnConfirmarTarea_1.Visible = true;

            }
            catch (Exception ex)
            {
                LbAlertaGuardar.InnerText = ex.Message;
                divAlertaGuardar.Visible = true;
                //OpenModalLoad();
            }
        }
    }
}