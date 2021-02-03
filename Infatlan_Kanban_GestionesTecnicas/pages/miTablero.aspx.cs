using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban_GestionesTecnicas.classes;
using System.Data;
using System.IO;


namespace Infatlan_Kanban_GestionesTecnicas.pages
{
    public partial class miTablero : System.Web.UI.Page
    {
        db vConexionGestiones = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USUARIO"] = "2536";
            {
                String vEx = Request.QueryString["ex"];
                if (!Page.IsPostBack)
                {
                    if (vEx == null)
                    {
                        cargarData();
                    }
                    else if (vEx.Equals("1"))
                    {
                        String vRe = "Tarjeta finalizada con éxito..";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Pop", "window.alert('" + vRe + "')", true);
                        cargarData();
                    }
                }

            }
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

            LbEnCola.Text = vEnCola;
            LbEjecucion.Text = vEnEjecucion;
            LbCompletados.Text = vCompletadasHoy;
            LbAtrasados.Text = vAtrasado;
            string vString = "";

            vQuery = "GESTIONES_Generales 21,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = "";
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();
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
                "<div class='card-header " + vColor + " text-white'>" +
                "<h6 class='m-b-0 text-white'>ID TAREA: " + vTicket + "</h6>" +
                "</div>" +
                "<div class='card-body'>" +
                "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
                "<h6 class='card-subtitle mb-2 text-dark'><b>" + vGestion + "</b></h6><br>" +
                "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA INICIO:  " + vFechaInicio + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA ENTREGA: " + vFecha + "</h6>" +
                "<h6 class='card-subtitle mb-2 text-muted'>PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
                "<div class='col-12 text-center' >" +
                "<a href='creacionTareas.aspx?ex=" + vTicket + "'><i class='btn " + vColorBoton + " btn-circle fa fa-clipboard'></i></a>" +
                "</div>" +
                "</div>" +
                "</div>";
            }
            LitNotificacionesEnCola.Text = vString;

            //SOLICITUDES EN EJECUCIÓN
            vString = "";
            vQuery = "GESTIONES_Generales 22,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();

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
               "<div class='card-header " + vColor + " text-white'>" +
               "<h6 class='m-b-0 text-white'>ID TAREA: " + vTicket + "</h6>" +
               "</div>" +
               "<div class='card-body'>" +
               "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
               "<h6 class='card-subtitle mb-2 text-dark'><b>" + vGestion + "</b></h6><br>" +
               "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA INICIO:  " + vFechaInicio + "</h6>" +
               "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA ENTREGA: " + vFecha + "</h6>" +
               "<h6 class='card-subtitle mb-2 text-muted'>PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
               "<div class='col-12 text-center' >" +
               "<a href='creacionTareas.aspx?ex=" + vTicket + "'><i class='btn " + vColorBoton + " btn-circle fa fa-clipboard'></i></a>" +
               "</div>" +
               "</div>" +
               "</div>";
            }
            LitNotificacionesEjecucion.Text = vString;



            //SOLICITUDES ATRASADAS
            vString = "";
            vQuery = "GESTIONES_Generales 23,'" + Session["USUARIO"].ToString() + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vFechaInicio = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();

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
               "<div class='card-header " + vColor + " text-white'>" +
               "<h6 class='m-b-0 text-white'>ID TAREA: " + vTicket + "</h6>" +
               "</div>" +
               "<div class='card-body'>" +
               "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
               "<h6 class='card-subtitle mb-2 text-dark'><b>" + vGestion + "</b></h6><br>" +
               "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA INICIO:  " + vFechaInicio + "</h6>" +
               "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA ENTREGA: " + vFecha + "</h6>" +
               "<h6 class='card-subtitle mb-2 text-muted'>PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
               "<div class='col-12 text-center' >" +
               "<a href='creacionTareas.aspx?ex=" + vTicket + "'><i class='btn " + vColorBoton + " btn-circle fa fa-clipboard'></i></a>" +
               "</div>" +
               "</div>" +
               "</div>";
            }
            LitNotificacionesAtrasadas.Text = vString;

            //SOLICITUDES COMPLETADOS HOY
            vString = "";
            DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
            vQuery = "GESTIONES_Generales 24,'" + Session["USUARIO"].ToString() + "','" + vfechaActualCorta + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            for (int i = 0; i < vDatos.Rows.Count; i++)
            {
                String vColor = "";
                String vColorBoton = "";
                String vColorPrioridad = "";
                String vTicket = "", vTitulo = "", vGestion = "", vFecha = "", vPrioridad = "", vEstadoNombre = "", vColorEstado = "", vFechaInicio = ""; ;
                vTicket = vDatos.Rows[i]["idSolicitud"].ToString();
                vTitulo = vDatos.Rows[i]["titulo"].ToString();
                vGestion = vDatos.Rows[i]["nombreGestion"].ToString();
                vFecha = vDatos.Rows[i]["fechaEntrega"].ToString();
                vPrioridad = vDatos.Rows[i]["prioridad"].ToString();
                vEstadoNombre = vDatos.Rows[i]["estado"].ToString();
                vFechaInicio = vDatos.Rows[i]["fechaInicio"].ToString();

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
               "<div class='card-header " + vColor + " text-white'>" +
               "<h6 class='m-b-0 text-white'>ID TAREA: " + vTicket + "</h6>" +
               "</div>" +
               "<div class='card-body'>" +
               "<h5 class='card-title mb-2'>" + vTitulo + "</h5>" +
               "<h6 class='card-subtitle mb-2 text-dark'><b>" + vGestion + "</b></h6><br>" +
               "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA INICIO:  " + vFechaInicio + "</h6>" +
               "<h6 class='card-subtitle mb-2 text-muted'><i class='fa fa-calendar'></i> FECHA ENTREGA: " + vFecha + "</h6>" +
               "<h6 class='card-subtitle mb-2 text-muted'>PRIORIDAD: <span class='label label-" + vColorPrioridad + "'>" + vPrioridad + "</span></h6>" +
               "<div class='col-12 text-center'>" +
                "<h5><span class='label label-" + vColorPrioridad + "'>" + vEstadoNombre + "</span></h5><br>" +
               "<a href='creacionTareas.aspx?ex=" + vTicket + "&exLectura=1'><i class='btn " + vColorBoton + " btn-circle fa fa-clipboard'></i></a>" +
               "</div>" +
               "</div>" +
               "</div>";
            }
            LitNotificacionesCompletadosHoy.Text = vString;

        }
    }
}