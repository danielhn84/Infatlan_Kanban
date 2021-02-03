using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban_GestionesTecnicas.classes;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;
using Infatlan_Kanban.classes;

namespace Infatlan_Kanban.pages
{
    public partial class creacionTareas : System.Web.UI.Page
    {
        db vConexion = new db();
        db vConexionGestiones = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

                string vEx = Request.QueryString["ex"];
                string vTarjetaCerrada = Request.QueryString["exLectura"];
                string vTarjetaReasignar = Request.QueryString["exReasignar"];
                string vTarjetaDetener = Request.QueryString["exDetener"];
                if (vTarjetaReasignar == null) {
                    Session["GESTIONES_REASIGNAR_TAREA"] = "";
                } else {
                    Session["GESTIONES_REASIGNAR_TAREA"] = vTarjetaReasignar;
                }


            if (vTarjetaDetener == null){
                Session["GESTIONES_DETENER_TAREA"] = "";
            }else{
                Session["GESTIONES_DETENER_TAREA"] = vTarjetaDetener;
            }



            if (!Page.IsPostBack)
            {

                cargarReasignarSolicitudes();
                cargarDetenerSolicitudes();
                cargarInicialMisSolicitudes();

                if (string.IsNullOrEmpty(vEx))
                {
                    LbTituloTarjeta.InnerText = "Creación Tarjeta Kanban";
                    DdlTipoGestion.Enabled = false;
                    cargarInicial();
                    
                    //cargarInicialMisSolicitudes();
                    //cargarReasignarSolicitudes();
                }
                else if (vEx != null && vTarjetaCerrada == null && vTarjetaReasignar == null && vTarjetaDetener==null)
                {
                    LbTituloTarjeta.InnerText = "Cerrar Tarjeta Kanban";
                    cargarInicial();
                    cargarGestiones();
                    cargarDatosTarjeta();
                    cargarInicialMisSolicitudes();
                }
                else if (vEx != null && vTarjetaCerrada != null && vTarjetaReasignar == null && vTarjetaDetener==null)
                {
                    LbTituloTarjeta.InnerText = "Tarjeta Kanban Cerrada";
                    cargarInicial();
                    cargarGestiones();
                    cargarDatosTarjeta();
                    divComentariosTexbox.Visible = false;

                    //DATOS SOLUCION
                    string vQuery = "GESTIONES_Solicitud 17,'" + vEx + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    TxDetalle.Text = vDatos.Rows[0]["detalleFinalizo"].ToString();
                    TxDetalle.ReadOnly = true;

                    divAccion.Visible = false;
                    //divAdjuntoSolucion.Visible = false;

                    divBotonesPrincipales.Visible = false;
                    divBotonesLecturaTarjeta.Visible = true;
                }
                else if (vEx != null && vTarjetaReasignar != null && vTarjetaCerrada == null && vTarjetaDetener==null)
                {
                    LbTituloTarjeta.InnerText = "Reasignar Tarjeta Kanban";
                    cargarInicial();
                    divSolucionAdjunto.Visible = false;
                    cargarGestiones();
                    cargarDatosTarjeta();

                }
            }
        }
        void camposDeshabilitados()
        {
            TxFechaSolicitud.ReadOnly = true;
            TxTitulo.ReadOnly = true;
            TxDescripcion.ReadOnly = true;
            DdlResponsable.Enabled = false;
            TxMinProductivo.ReadOnly = true;
            TxFechaInicio.ReadOnly = true;
            TxFechaEntrega.ReadOnly = true;
            DdlPrioridad.Enabled = false;
            DdlTipoGestion.Enabled = false;

            lbNombre.Visible = false;
            FuAdjunto.Visible = false;
            BtnAddAdjunto.Visible = false;

            lbComentario.Visible = true;
            TxComentario.Visible = true;
            BtnAddComentario.Visible = true;
            divComentario.Visible = false;

            tabHistorial.Visible = true;
            tabSolucion.Visible = true;

        }
        void cargarGestiones()
        {
            string vQuery = "GESTIONES_Generales 39";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            DdlTipoGestion.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
            if (vDatos.Rows.Count > 0)
            {
                foreach (DataRow item in vDatos.Rows)
                {
                    DdlTipoGestion.Items.Add(new ListItem { Value = item["idTipoGestion"].ToString(), Text = item["nombreGestion"].ToString() });
                }
            }

        }
        void cargarDatosTarjeta()
        {
            camposDeshabilitados();

            String vEx = Request.QueryString["ex"];
            //DATOS GENERALES
            string vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            string vFormato = "yyyy-MM-ddTHH:mm";
            string vFechaInicio = vDatos.Rows[0]["fechaInicio"].ToString();
            string vFechaFin = vDatos.Rows[0]["fechaEntrega"].ToString();
            TxFechaSolicitud.Text = vDatos.Rows[0]["fechaEnvio"].ToString();
            TxTitulo.Text = vDatos.Rows[0]["titulo"].ToString();
            TxDescripcion.Text = vDatos.Rows[0]["descripcion"].ToString();
            DdlResponsable.SelectedValue = vDatos.Rows[0]["responsable"].ToString();
            TxMinProductivo.Text = vDatos.Rows[0]["minSolicitud"].ToString();
            TxFechaInicio.Text = Convert.ToDateTime(vFechaInicio).ToString(vFormato);
            TxFechaEntrega.Text = Convert.ToDateTime(vFechaFin).ToString(vFormato);
            DdlPrioridad.SelectedValue = vDatos.Rows[0]["prioridad"].ToString();
            DdlTipoGestion.SelectedValue = vDatos.Rows[0]["idTipoGestion"].ToString();
            Session["GESTIONES_USUARIO_CREO"] = vDatos.Rows[0]["usuarioCreo"].ToString();

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


            string vReasignarTarjeta = null;
            if (string.IsNullOrEmpty(Session["GESTIONES_REASIGNAR_TAREA"].ToString()))
            {
                vReasignarTarjeta = null;
            }
            else
            {
                vReasignarTarjeta = Session["GESTIONES_REASIGNAR_TAREA"].ToString();
            }


            if (vReasignarTarjeta != null)
            {
                divBotonesPrincipales.Visible = true;
                divBotonesLecturaTarjeta.Visible = false;
                tabReasignacion.Visible = true;
                TxFechaSolicitud.Text = Convert.ToString(DateTime.Now);
                DdlResponsable.Enabled = true;
                TxFechaInicio.ReadOnly = false;
                TxFechaEntrega.ReadOnly = false;
                divComentariosTexbox.Visible = false;
                DdlAccion.Enabled = false;
                TxDetalle.ReadOnly = true;

                //DETALLE XQ SE DEBE REASIGNAR
                vQuery = "GESTIONES_Solicitud 21,'" + vEx + "'";
                DataTable vDatosDetalle = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                TxDetalle.Text = vDatosDetalle.Rows[0]["detalleFinalizo"].ToString();
                DdlAccion.SelectedIndex = 1;


            }


        }
        void cargarReasignarSolicitudes()
        {
            try
            {
                LbTituloModificarTarjeta.InnerText = "Reasignación de Tarjeta";

                String vQuery = "GESTIONES_Solicitud 20,'" + Session["USUARIO"].ToString() + "','"+ Session["USUARIO"].ToString()+"'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                GvReasignar.DataSource = vDatos;
                GvReasignar.DataBind();
                //UpTablaReasignar.Update();
                Session["GESTIONES_REASIGNAR_TARJETAS"] = vDatos;


            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        void cargarDetenerSolicitudes()
        {
            try
            {
                LbTituloDetenerTarjeta.InnerText = "Detener Tarjeta Kanban";

                String vQuery = "GESTIONES_Solicitud 23,'" + Session["USUARIO"].ToString() + "','" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                GvDetener.DataSource = vDatos;
                GvDetener.DataBind();
                Session["GESTIONES_DETENER_TARJETAS"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        void cargarInicialMisSolicitudes()
        {
            try
            {
                LbTituloTarjetaCerrada.InnerText = "Mis Tarjetas Cerradas";

                String vQuery = "GESTIONES_Solicitud 18,'" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                GvSolicitudes.DataSource = vDatos;
                GvSolicitudes.DataBind();
                UpMisSolicitudes.Update();
                Session["GESTIONES_MIS_SOLICITUDES"] = vDatos;


            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        void cargarInicial()
        {
            try
            {
                TxFechaSolicitud.Text = Convert.ToString(DateTime.Now);

                String vQuery = "GESTIONES_Generales 37,'" + Session["USUARIO"].ToString() + "','"+ Session["USUARIO"].ToString()+"'";
                DataTable vDatosResponsables = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlResponsable.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatosResponsables.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosResponsables.Rows)
                    {
                        DdlResponsable.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                    }
                }



            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }

        }
        protected void BtnAddAdjunto_Click(object sender, EventArgs e)
        {
            try
            {
               divAlertaAdjunto.Visible = false;
                if (FuAdjunto.HasFile == false) { 
                    LbDuplicadoAdjunto.InnerText = "No ha seleccionado ningun archivo adjunto.";
                    divAlertaAdjunto.Visible = true;}

                String vNombreArchivo = String.Empty;
                HttpPostedFile bufferDeposito1T = FuAdjunto.PostedFile;
                byte[] vFileDepositoAdjunto = null;
                String vExtensionAdjunto = String.Empty;

                if (FuAdjunto.HasFile)
                {
                    if (bufferDeposito1T != null)
                    {
                        vNombreArchivo = FuAdjunto.FileName;
                        Stream vStream = bufferDeposito1T.InputStream;
                        BinaryReader vReader = new BinaryReader(vStream);
                        vFileDepositoAdjunto = vReader.ReadBytes((int)vStream.Length);
                        vExtensionAdjunto = System.IO.Path.GetExtension(FuAdjunto.FileName);
                    }

                    String vArchivoAdjunto = String.Empty;
                    if (vFileDepositoAdjunto != null)
                    {
                        vArchivoAdjunto = Convert.ToBase64String(vFileDepositoAdjunto);
                    }
                    else
                    {
                        vArchivoAdjunto = "";
                    }

                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
                    vData.Columns.Add("idAdjunto");
                    vData.Columns.Add("nombre");
                    vData.Columns.Add("ruta");

                    if (vDatos == null)
                        vDatos = vData.Clone();

                    if (vDatos != null)
                    {
                        Session["GESTIONES_CONTEO_ADJUNTOS"] = Convert.ToInt32(Session["GESTIONES_CONTEO_ADJUNTOS"]) + 1;
                        if (vDatos.Rows.Count < 1)
                        {
                            vDatos.Rows.Add(Session["GESTIONES_CONTEO_ADJUNTOS"].ToString(), vNombreArchivo, vArchivoAdjunto);

                        }
                        else
                        {
                            Boolean vRegistered = false;
                            for (int i = 0; i < vDatos.Rows.Count; i++)
                            {
                                if (vNombreArchivo == vDatos.Rows[i]["nombre"].ToString())
                                {
                                    LbDuplicadoAdjunto.InnerText = "Ya tiene ingresado un adjunto con el mismo nombre " + vNombreArchivo;
                                    divAlertaAdjunto.Visible = true;
                                    vRegistered = true;
                                }
                            }

                            if (!vRegistered)
                                vDatos.Rows.Add(Session["GESTIONES_CONTEO_ADJUNTOS"].ToString(), vNombreArchivo, vArchivoAdjunto);

                        }
                    }

                    GvAdjunto.DataSource = vDatos;
                    GvAdjunto.DataBind();
                    Session["GESTIONES_TAREAS_ADJUNTO"] = vDatos;
                    divAdjunto.Visible = true;
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void BtnAddComentario_Click(object sender, EventArgs e)
        {
            try
            {
                string vEx = Request.QueryString["ex"];
                if (vEx != null )
                {
                    string usuario = Session["USUARIO_AD"].ToString() + ' ' + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    String vQuery = "GESTIONES_Solicitud 2,'" + vEx + "','" + TxComentario.Text + "','" + usuario + "'";
                    Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    string vCambio = "Usuario agrego nuevo comentario a la tarjeta. Comentario: " + TxComentario.Text;
                    string vCambioSuscripcion = "Buen día, se notifica que el usuario: " + Session["USUARIO_AD"].ToString() + ", agrego nuevo comentario a la tarjeta: " + vEx + "-" + TxTitulo.Text + ", Comentario: " + TxComentario.Text;

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

                }
                else
                {

                   if (TxComentario.Text == "" || TxComentario.Text == string.Empty)
                        throw new Exception("El campo de comentario está vacío.");


                    string hora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    DataTable vData = new DataTable();
                    DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
                    vData.Columns.Add("idComentario");
                    vData.Columns.Add("usuario");
                    vData.Columns.Add("comentario");

                   
                    if (vDatos == null)
                        vDatos = vData.Clone();
                     Session["GESTIONES_CONTEO_COMENTARIO"] = Convert.ToInt32(Session["GESTIONES_CONTEO_COMENTARIO"]) + 1;
                    if (vDatos != null)
                    {
                        if (vDatos.Rows.Count < 1)
                        {
                            vDatos.Rows.Add(Session["GESTIONES_CONTEO_COMENTARIO"].ToString(), Session["USUARIO_AD"].ToString() + ' ' + hora, TxComentario.Text);
                        }
                        else
                        {
                            vDatos.Rows.Add(Session["GESTIONES_CONTEO_COMENTARIO"].ToString(), Session["USUARIO_AD"].ToString() + ' ' + hora, TxComentario.Text);
                        }
                    }

                    GvComentario.DataSource = vDatos;
                    GvComentario.DataBind();
                    Session["GESTIONES_TAREAS_COMENTARIOS"] = vDatos;
                    divComentario.Visible = true;
                    TxComentario.Text = "";
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
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
        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string vReasignarTarea = Session["GESTIONES_REASIGNAR_TAREA"].ToString();


                string vEx = Request.QueryString["ex"];
                if (vEx != null &&( vReasignarTarea == null || vReasignarTarea == ""))
                {
                    validacionesCerrarTarea();



                    String vNombreArchivo = String.Empty;
                    HttpPostedFile bufferDeposito1T = FuSolucion.PostedFile;
                    byte[] vFileDepositoAdjunto = null;
                    String vExtensionAdjunto = String.Empty;

                    if (FuSolucion.HasFile)
                    {
                    }

                        DivEstados.Visible = false;
                    TxDetalleModal.Visible = true;
                    lbDetalleModal.Visible = true;
                    LbTitulo.Text = DdlAccion.SelectedItem + ": " + vEx;
                    UpTitulo.Update();
                    TxTituloModal.Text = TxTitulo.Text;
                    TxPrioridadModal.Text = DdlPrioridad.SelectedItem.ToString();
                    TxTimeModal.Text = TxMinProductivo.Text +" Mins";
                    TxGestionModal.Text = DdlTipoGestion.SelectedItem.ToString();
                    TxEntregaModal.Text = TxFechaEntrega.Text;
                    TxInicioModal.Text = TxFechaInicio.Text;
                    TxDetalleModal.Text = TxDetalle.Text;
                    divMensajeCargabilidad.Visible = false;
                    UpdatePanel3.Update();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModalConfirmacionMP();", true);
                }
                else
                {
                    validacionesCrearSolicitud();

                    if (vReasignarTarea != string.Empty || vReasignarTarea!="")
                        validacionesReasignarTarjeta();

                    GVDistribucion.DataSource = null;
                    GVDistribucion.DataBind();
                    Session["GESTIONES_TAREAS_MIN_DIARIOS"] = null;

                    DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
                    DateTime fecha_fin = DateTime.Parse(TxFechaEntrega.Text.ToString());

                    String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
                    String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
                    String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

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
                                    string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaEvaluar + "'";
                                    DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                    string vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
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


                                    if (vMinDiarios < vSobranteWIPCreacion)
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
                        int vDiasTarjeta = Convert.ToInt32(Session["GESTIONES_DIAS"].ToString());
                        if (vDiasTarjeta == 1)
                        {
                            if (vDatosMin == null)
                                vDatosMin = vData.Clone();
                            if (vDatosMin != null)
                            {
                                vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
                                vDatosMin.Rows.Add("1", vFechaInicioSoli, TxMinProductivo.Text);
                            }
                        }
                        else if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday && vDiasTarjeta == 2)
                        {
                            int vCount = 0;
                            int vResta = 0;
                            double vMinsFaltante = 0;
                            vMinDiarios = Convert.ToInt32(TxMinProductivo.Text) / vDiasTarjeta;
                            for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
                            {
                                vCount = vCount + 1;
                                vResta = (vDiasTarjeta - vCount) + 1;

                                if (vMinsFaltante != 0)
                                {
                                    vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
                                    vMinsFaltante = 0;
                                }

                                string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
                                string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaEvaluar + "'";
                                DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                                string vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
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


                                if (vMinDiarios < vSobranteWIPCreacion)
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

                            if (vMinsFaltante != 0)
                                throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);
                        }
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
                    divComentariosAdjuntos.Visible = true;
                    cargarModal();

                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "abrirModalConfirmacionMP();", true);

                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
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

            LbEnCola.Text = vEnCola;
            LbEjecucion.Text = vEnEjecucion;
            LbDetenidas.Text = vDetenidas;
            LbAtrasado.Text = vAtrasado;

            LbTitulo.Text = "Información General: " + DdlResponsable.SelectedItem.ToString();
            UpTitulo.Update();
            TxTituloModal.Text = TxTitulo.Text;
            TxPrioridadModal.Text = DdlPrioridad.SelectedItem.ToString();
            TxTimeModal.Text = TxMinProductivo.Text + " Mins";
            TxGestionModal.Text = DdlTipoGestion.SelectedItem.ToString();
            TxEntregaModal.Text = TxFechaEntrega.Text;
            TxInicioModal.Text = TxFechaInicio.Text;
            UpdatePanel3.Update();
        }
        protected void BtnConfirmarTarea_Click(object sender, EventArgs e)
        {
            try
            {
                string vEx = Request.QueryString["ex"];
                string vReasignarTarea = Session["GESTIONES_REASIGNAR_TAREA"].ToString();
                if (vReasignarTarea != string.Empty || vReasignarTarea!="" )
                {



                }
                else
                {
                    String vFormato = "dd/MM/yyyy HH:mm"; //"dd/MM/yyyy HH:mm:ss"
                    String vFechaInicioTarea = Convert.ToDateTime(TxFechaInicio.Text).ToString(vFormato);
                    DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    DateTime vfechaActualCorta = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));

                    DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
                    DateTime vFechaInicio = Convert.ToDateTime(fecha_inicio.ToString("dd/MM/yyyy HH:mm"));

                    DateTime fecha_entrega = DateTime.Parse(TxFechaEntrega.Text.ToString());
                    DateTime vFechaEntrega = Convert.ToDateTime(fecha_entrega.ToString("dd/MM/yyyy HH:mm"));

                    string vidEstado = "";
                    string vidEstadoTexto = "";
                    string vtarjetaEstado = "";
                    string vCambio= "";


                    if (string.IsNullOrEmpty(vEx))
                    {
                        if (vFechaInicio < vfechaActual)
                        {
                            vidEstado = "2";
                            vidEstadoTexto = "En Ejecución";
                            vtarjetaEstado = "1";
                        }
                        else
                        {
                            vidEstado = "1";
                            vidEstadoTexto = "En Cola";
                            vtarjetaEstado = "0";
                        }

                        vCambio = "Creación de tarjeta, Estado: " + vidEstadoTexto + ", Prioridad: " + DdlPrioridad.SelectedItem;

                        String vQuery1 = "GESTIONES_Solicitud 1,'" + TxTitulo.Text
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

                        DataTable vDatosAdjuntos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
                        if (vDatosAdjuntos != null)
                        {
                            for (int num = 0; num < vDatosAdjuntos.Rows.Count; num++)
                            {
                                string ruta = vDatosAdjuntos.Rows[num]["ruta"].ToString();
                                string nombre = vDatosAdjuntos.Rows[num]["nombre"].ToString();

                                String vQuery3 = "GESTIONES_Solicitud 3,'" + idSolicitud +
                                    "','" + ruta +
                                    "','" + nombre + "'";
                                Int32 vInfo3 = vConexionGestiones.ejecutarSqlGestiones(vQuery3);
                            }
                        }

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

                        string vQuery4 = "GESTIONES_Solicitud 4,'" + idSolicitud +
                            "','" + vCambio
                            + "','" + Session["USUARIO_AD"].ToString() + "'";
                        Int32 vInfo4 = vConexionGestiones.ejecutarSqlGestiones(vQuery4);

                        string vCorreosCopia = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                        string vAsunto = "Creación Tarjeta Kanban, Gestiones Técnicas: " + idSolicitud;
                        string vQuery5 = "GESTIONES_Solicitud 5,'Creacion Tarjeta Kanban','"
                         + Session["GESTIONES_CORREO_RESPONSABLE"].ToString()
                        + "','" + vCorreosCopia + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + idSolicitud + "'";
                        Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);


                        if (vInfo5 == 1)
                        {
                            limpiarCreacionTarea();
                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModalConfirmacionMP();", true);
                            Response.Redirect("/pages/miTablero.aspx?ex=2");
                        }

                    }
                    else
                    {
                        string vEstadoCargabilidad = "";
                        if (DdlAccion.SelectedValue == "2")
                        {
                            if (vfechaActual < vFechaEntrega)
                            {
                                vidEstado = "5";
                                vidEstadoTexto = "Realizado a Tiempo";
                                vEstadoCargabilidad = "1";
                            }
                            else
                            {
                                vidEstado = "6";
                                vidEstadoTexto = "Realizado fuera de tiempo";
                                vEstadoCargabilidad = "1";
                            }
                        }
                        else if(DdlAccion.SelectedValue == "1")
                        {
                            vidEstado = "8";
                            vidEstadoTexto = "Solicitud Re-asignación de Tarjeta";
                            vEstadoCargabilidad = "2";
                        }
                        else
                        {
                            vidEstado = "9";
                            vidEstadoTexto = "Solicitud Tarjeta a Estado Detenido";
                            vEstadoCargabilidad = "3";
                        }


                        vCambio = "Finalizar Tarjeta, Estado: " + vidEstadoTexto + ", Detalle: " + TxDetalle.Text;
                    
                        String vNombreDepot1 = String.Empty;
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
                        string vQuery = "GESTIONES_Solicitud 16,'" + vEx + "','" + vidEstado + "','" + TxDetalle.Text + "','" + Session["USUARIO"].ToString() + "','" + vArchivo + "'";
                        Int32 vInfo1 = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                        //GUARDAR HISTORIAL
                        vQuery = "GESTIONES_Solicitud 4,'" + vEx + "','" + vCambio + "','" + Session["USUARIO"].ToString() + "'";
                        Int32 vInfo2 = vConexionGestiones.ejecutarSqlGestiones(vQuery);



                        vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable.SelectedValue + "'";
                        DataTable vDatos = vConexion.obtenerDataTableGestiones(vQuery);
                        string vTeams = vDatos.Rows[0]["idTeams"].ToString();
                        Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();

                        vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
                        vDatos = vConexion.obtenerDataTableGestiones(vQuery);
                        Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                        Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();
                        Session["GESTIONES_NOMBRE_JEFE"] = vDatos.Rows[0]["nombreJefe"].ToString();
                        Session["GESTIONES_NOMBRE_SUPLENTE"] = vDatos.Rows[0]["nombreSuplente"].ToString();

                        if (DdlAccion.SelectedValue == "2")
                        {
                            //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                            string vAsunto = "Tarjeta Kanban Finalizada, Gestiones Técnicas: " + vEx;
                            string vCorreosCopia = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                            string vQuery5 = "GESTIONES_Solicitud 5,'Tarjeta Kanban Finalizada','"
                             + Session["GESTIONES_CORREO_RESPONSABLE"].ToString()
                            + "','" + vCorreosCopia + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + vEx + "'";
                            Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);
                        }else if(DdlAccion.SelectedValue == "1")
                        {
                            //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                            string vAsunto = "Solicitud Reasignación de Tarjeta Kanban, Gestiones Técnicas: " + vEx;
                            string vCorreo = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                            string vQuery5 = "GESTIONES_Solicitud 5,'Solicitud Reasignación de Tarjeta Kanban','"
                             + vCorreo
                            + "','" + Session["GESTIONES_CORREO_RESPONSABLE"].ToString() + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + vEx + "'";
                            Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);
                        }else if (DdlAccion.SelectedValue == "3")
                        {
                            //GUARDAR EN LA SUSCRIPCION TARJETA FINALIZADA
                            string vAsunto = "Solicitud Tarjeta Kanban a Estado Detenida, Gestiones Técnicas: " + vEx;
                            string vCorreo = Session["GESTIONES_CORREO_JEFE"].ToString() + ";" + Session["GESTIONES_CORREO_SUPLENTE"].ToString();

                            string vQuery5 = "GESTIONES_Solicitud 5,'Solicitud Tarjeta Kanban a Detenido','"
                             + vCorreo
                            + "','" + Session["GESTIONES_CORREO_RESPONSABLE"].ToString() + "','" + vAsunto + "','" + "Datos Generales Tarjeta', '0','" + vEx + "'";
                            Int32 vInfo5 = vConexionGestiones.ejecutarSqlGestiones(vQuery5);
                        }


                        //CAMBIAR EL ESTADO DE LA CARGABILIDAD
                        vQuery = "GESTIONES_Solicitud 22,'" + vEx + "','"+ Session["USUARIO"].ToString() + "','"+ vEstadoCargabilidad+"'";
                        Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);


                        if (vInfo2 == 1)
                        {
                            TxDetalle.Text = "";
                            DdlAccion.SelectedIndex = -1;
                            Response.Redirect("/pages/miTablero.aspx?ex=1");
                        }
                    }


                }






            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        private string GetExtension(string Extension)
        {
            switch (Extension)
            {
                case ".doc":
                    return "application/ms-word";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".ppt":
                    return "application/mspowerpoint";
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".zip":
                    return "application/zip";
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case ".wav":
                    return "audio/wav";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                    return "application/xml";
                default:
                    return "application/octet-stream";
            }
        }
        private void limpiarCreacionTarea()
        {
            TxTitulo.Text = string.Empty;
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

            GvAdjunto.DataSource = null;
            GvAdjunto.DataBind();

            GvComentario.DataSource = null;
            GvComentario.DataBind();

            divAdjunto.Visible = false;
            divComentario.Visible = false;
            //UpdatePanel1.Update();
        }
        protected void DdlResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable.SelectedValue + "'";
                DataTable vDatos = vConexion.obtenerDataTableGestiones(vQuery);
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
                vDatos = vConexion.obtenerDataTableGestiones(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void TxFechaEntrega_TextChanged(object sender, EventArgs e)
        {
            try
            {



            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        private void validacionesCerrarTarea()
        {
            if (DdlAccion.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione acción a realizar.");

            if (TxDetalle.Text.Equals(""))
                throw new Exception("Falta que ingrese ingrese detalle de la acción.");
        }
        private void validacionesCrearSolicitud()
        {

            string vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable.SelectedValue + "'";
            DataTable vDatos = vConexion.obtenerDataTableGestiones(vQuery);
            string vTeams = vDatos.Rows[0]["idTeams"].ToString();
            Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
            Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

            vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
            vDatos = vConexion.obtenerDataTableGestiones(vQuery);
            Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
            Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();

            Session["GESTIONES_HR_INICIO"] = vDatos.Rows[0]["hrInicio"].ToString();
            Session["GESTIONES_HR_FIN"] = vDatos.Rows[0]["hrFin"].ToString();
            Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();

            if (TxTitulo.Text.Equals(""))
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

            DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
            DateTime fecha_fin = DateTime.Parse(TxFechaEntrega.Text.ToString());
            string vWip = Session["GESTIONES_WIP"].ToString();

            string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
            string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");

            DateTime fecha_actual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            string fecha_actual_corta = fecha_actual.ToString("dd/MM/yyyy");
            DateTime fecha_actualIngreso = Convert.ToDateTime(fecha_inicio.ToString("dd/MM/yyyy HH:mm"));

            var timeSpan = fecha_fin - fecha_inicio;
            int vdiasSolicitud = Convert.ToInt16(timeSpan.TotalDays);
            Session["GESTIONES_DIAS_SOLICITUD"] = vdiasSolicitud;

            if (DdlTipoGestion.SelectedValue != "4" && fecha_actualIngreso < fecha_actual)  //VALIDACION DIFERENTE A INCIDENTES QUE NO PODRAN INGRESARLA A CUALQUIER HORA
            {
                throw new Exception("No se puede crear la solicitud, la fecha actual del sistema es mayor que la fecha de inicio de la solicitud. Nota: Solo para el tipo de gestion de Incidentes se pueden ingresar a cualquier hora del dia, las demas gestiones deben ser debidamente programadas. ");
            }

            if (vFechaInicioSoli != fecha_actual_corta && vdiasSolicitud == 0 && DdlTipoGestion.SelectedValue != "4") //VALIDACION SI SOLO ES CERO DIAS ES NO ES IGUAL A LA FECHA ACTUAL DEL SISTEMA SI REALIZA LA VALIDACION
            {
                //VALIDACION INICIO TAREA SI TIENE MIN DISPNIBLES
                string vCantMinSolicitudes = "";
                string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaInicioSoli + "'";
                DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();

                if (vCantMinSolicitudes != "")
                {
                    if (Convert.ToDouble(vCantMinSolicitudes) >= Convert.ToDouble(vWip))
                        throw new Exception("Nota: La fecha seleccionada inicio de la tarjeta ya no cuenta con mins disponibles, su WIP está al limite, favor cambiar la fecha de inicio para poder realizar una mejor distribución de su cargabilidad. Minutos registrados de cargabilidad: " + vCantMinSolicitudes + ", WIP establecido: " + vWip);
                }
            }


            if (DdlTipoGestion.SelectedValue != "4" && vdiasSolicitud != 0)
            {
                //VALIDACION INICIO TAREA SI TIENE MIN DISPNIBLES
                string vCantMinSolicitudes = "";
                string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaInicioSoli + "'";
                DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();

                if (vCantMinSolicitudes != "")
                {
                    if (Convert.ToDouble(vCantMinSolicitudes) >= Convert.ToDouble(vWip))
                        throw new Exception("Nota: La fecha seleccionada inicio de la tarjeta ya no cuenta con min disponibles, su WIP está al limite, favor cambiar la fecha de inicio para poder realizar una mejor distribución de su cargabilidad. Minutos registrados de cargabilidad: " + vCantMinSolicitudes + ", WIP establecido: " + vWip);
                }

                //VALIDACION FIN DE LA TAREA SI TIENE MINUTOS DISPONIBLES
                vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaFinSoli + "'";
                vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
                vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
                if (vCantMinSolicitudes != "")
                {
                    if (Convert.ToDouble(vCantMinSolicitudes) >= Convert.ToDouble(vWip))
                        throw new Exception("Nota: La fecha seleccionada fin de la tarjeta no cuenta con min disponibles su WIP está al limite, favor cambiar la fecha de entrega de la tarea para poder realizar una mejor distribución de su cargabilidad. Minutos registrados de cargabilidad: " + vCantMinSolicitudes + ", WIP establecido: " + vWip);
                }
            }
        }
        private void validacionesReasignarTarjeta()
        {
            if (DdlAccionReasignacion.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione acción a efectar, en la sección de reasignación.");

            if (TxComentarioReasignacion.Text.Equals(""))
                throw new Exception("Falta que ingrese comentario de la acción, en la sección de reasignación.");
        }
        private void calculoHoras()
        {
            if (TxFechaInicio.Text != "" && TxFechaEntrega.Text != "")
            {
                DateTime fecha_inicio = DateTime.Parse(TxFechaInicio.Text.ToString());
                DateTime fecha_fin = DateTime.Parse(TxFechaEntrega.Text.ToString());

                String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
                String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
                String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

                DateTime vFechaInicio = DateTime.Parse(vFecha1);

            }
        }
        protected void GVDistribucion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDistribucion.PageIndex = e.NewPageIndex;
            GVDistribucion.DataSource = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
            GVDistribucion.DataBind();
        }
        protected void DdlAccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (DdlAccion.SelectedValue == "1" || DdlAccion.SelectedValue == "3")
                //{
                //    divAdjuntoSolucion.Visible = false;
                //}
                //else
                //{
                //    divAdjuntoSolucion.Visible = true;
                //}
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("/sites/gestiones/pages/miTablero.aspx");

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {


            if (FuSolucion.HasFile)
            {
                string fileName = FuSolucion.FileName;

            }







            //tabSolucion.Attributes["class"] = "active";

            //tabSolucion.Attributes.Add("class", "nav-link active");
            //tabSolucion.Attributes.Add("data-toggle", "tab");
            //tabSolucion.Attributes.Add("role", "tabpanel");
            //tabSolucion.Attributes.Add("href", "#solucion");
            //solucion.Attributes.Add("runat", "server");
            //solucion.Attributes.Add("class", "tab-pane active");




        }
        void validacionReporteTarjetasCerradas()
        {
            if (TxInicioBusqueda.Text.Equals(""))
                throw new Exception("Falta que ingrese la fecha inicio para la respectiva búsqueda.");

            if (TxFinBusqueda.Text.Equals(""))
                throw new Exception("Falta que ingrese la fecha fin para la respectiva búsqueda.");

            if (Convert.ToDateTime(TxInicioBusqueda.Text) > Convert.ToDateTime(TxFinBusqueda.Text))
                throw new Exception("Fecha inicial no debe ser mayor que fecha final.");

            if (Convert.ToDateTime(TxFinBusqueda.Text) < Convert.ToDateTime(TxInicioBusqueda.Text))
                throw new Exception("Fecha final no debe ser menor que fecha inicial.");
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                validacionReporteTarjetasCerradas();

                String vQuery = "GESTIONES_Solicitud 19,'" + TxInicioBusqueda.Text + "','" + TxFinBusqueda.Text + "','" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                Session["GESTIONES_MIS_SOLICITUDES"] = null;
                GvSolicitudes.DataSource = null;
                GvSolicitudes.DataBind();

                if (vDatos.Rows.Count > 0)
                {
                    GvSolicitudes.DataSource = vDatos;
                    GvSolicitudes.DataBind();
                    UpMisSolicitudes.Update();
                    Session["GESTIONES_MIS_SOLICITUDES"] = vDatos;
                    divTarjetasCerradas.Visible = false;
                    UpdatePanel9.Update();
                }
                else
                {
                    divMisSolicitudes.Visible = false;
                    divTarjetasCerradas.Visible = true;
                    UpMisSolicitudes.Update();
                    UpdatePanel9.Update();
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                TxFinBusqueda.Text = "";
                TxInicioBusqueda.Text = "";
                Session["GESTIONES_MIS_SOLICITUDES"] = null;
                GvSolicitudes.DataSource = null;
                GvSolicitudes.DataBind();

                cargarInicialMisSolicitudes();

                String vQuery = "GESTIONES_Solicitud 18,'" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    divTarjetasCerradas.Visible = false;
                    UpdatePanel9.Update();
                    GvSolicitudes.DataSource = vDatos;
                    GvSolicitudes.DataBind();
                    UpMisSolicitudes.Update();
                    Session["GESTIONES_MIS_SOLICITUDES"] = vDatos;
                }
                else
                {
                    divMisSolicitudes.Visible = false;
                    divTarjetasCerradas.Visible = true;
                    UpMisSolicitudes.Update();
                    UpdatePanel9.Update();
                }

            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void BtnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                validacionReporteTarjetasCerradas();

                String vQuery = "GESTIONES_Solicitud 19,'" + TxInicioBusqueda.Text + "','" + TxFinBusqueda.Text + "','" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                Session["GESTIONES_MIS_SOLICITUDES"] = null;
                GvSolicitudes.DataSource = null;
                GvSolicitudes.DataBind();

                if (vDatos.Rows.Count > 0)
                {
                    string Parametro1 = TxInicioBusqueda.Text;
                    string Parametro2 = TxFinBusqueda.Text;
                    string Parametro3 = Session["USUARIO"].ToString();


                    ReportExecutionService.ReportExecutionService vRSE = new ReportExecutionService.ReportExecutionService();
                    vRSE.Credentials = new NetworkCredential("report_user", "kEbn2HUzd$Fs2T", "adbancat.hn");
                    vRSE.Url = "http://10.128.0.52/reportserver/reportexecution2005.asmx";

                    vRSE.ExecutionHeaderValue = new ReportExecutionService.ExecutionHeader();
                    var vEInfo = new ReportExecutionService.ExecutionInfo();
                    vEInfo = vRSE.LoadReport("/GestionesTecnicas/GestionesTarjetasCerradas", null);

                    List<ReportExecutionService.ParameterValue> vParametros = new List<ReportExecutionService.ParameterValue>();
                    vParametros.Add(new ReportExecutionService.ParameterValue { Name = "PARAM1", Value = Parametro1 });
                    vParametros.Add(new ReportExecutionService.ParameterValue { Name = "PARAM2", Value = Parametro2 });
                    vParametros.Add(new ReportExecutionService.ParameterValue { Name = "PARAM3", Value = Parametro3 });

                    vRSE.SetExecutionParameters(vParametros.ToArray(), "en-US");
                    String deviceinfo = "<DeviceInfo><Toolbar>false</Toolbar></DeviceInfo>";
                    String mime;
                    String encoding;
                    string[] stream;
                    ReportExecutionService.Warning[] warning;

                    byte[] vResultado = vRSE.Render("EXCEL", deviceinfo, out mime, out encoding, out encoding, out warning, out stream);

                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
                    byte[] bytFile = vResultado;
                    Response.OutputStream.Write(bytFile, 0, bytFile.Length);
                    Response.AddHeader("Content-disposition", "attachment;filename=Reporte.xls");
                    Response.End();

                    divTarjetasCerradas.Visible = false;
                    TxInicioBusqueda.Text = "";
                    TxFinBusqueda.Text = "";

                    cargarInicialMisSolicitudes();
                }
                else
                {
                    divTarjetasCerradas.Visible = true;
                }
            }
            catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        }
        protected void GvReasignar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reasignar")
            {
                string vIdTarjeta = e.CommandArgument.ToString();
                Session["GESTIONES_ID_TARJETA_REASIGNAR"] = vIdTarjeta;

                try
                {
                    LbTituloTarjeta.InnerText = "Reasignar Tarjeta Kanban";
                    Response.Redirect("/pages/creacionTareas.aspx?ex=" + vIdTarjeta + "&exReasignar=1");

                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }
        protected void GvReasignar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvReasignar.PageIndex = e.NewPageIndex;
                GvReasignar.DataSource = (DataTable)Session["GESTIONES_REASIGNAR_TARJETAS"];
                GvReasignar.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvAdjunto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Eliminar")
                {
                    string vId = e.CommandArgument.ToString();
                    if (Session["GESTIONES_TAREAS_ADJUNTO"] != null)
                    {
                        DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
                        DataRow[] result = vDatos.Select("idAdjunto = '" + vId + "'");
                        foreach (DataRow row in result)
                        {
                            if (row["idAdjunto"].ToString().Contains(vId))
                            {
                                //LLENAR TABLA DE DATOS A ELIMINAR
                                vDatos.Rows.Remove(row);
                            }
                        }

                        GvAdjunto.DataSource = vDatos;
                        GvAdjunto.DataBind();
                        //UpdatePanel1.Update();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //protected void BtnConfirmarAdjunto_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        String vNombreArchivo = String.Empty;
        //        HttpPostedFile bufferDeposito1T = FuAdjunto.PostedFile;
        //        byte[] vFileDepositoAdjunto = null;
        //        String vExtensionAdjunto = String.Empty;

        //        if (FuAdjunto.HasFile)
        //        {
        //            if (bufferDeposito1T != null)
        //            {
        //                vNombreArchivo = FuAdjunto.FileName;
        //                Stream vStream = bufferDeposito1T.InputStream;
        //                BinaryReader vReader = new BinaryReader(vStream);
        //                vFileDepositoAdjunto = vReader.ReadBytes((int)vStream.Length);
        //                vExtensionAdjunto = System.IO.Path.GetExtension(FuAdjunto.FileName);
        //            }

        //            String vArchivoAdjunto = String.Empty;
        //            if (vFileDepositoAdjunto != null)
        //            {
        //                vArchivoAdjunto = Convert.ToBase64String(vFileDepositoAdjunto);
        //            }
        //            else
        //            {
        //                vArchivoAdjunto = "";
        //            }

        //            DataTable vData = new DataTable();
        //            DataTable vDatos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
        //            vData.Columns.Add("idAdjunto");
        //            vData.Columns.Add("nombre");
        //            vData.Columns.Add("ruta");

        //            if (vDatos == null)
        //                vDatos = vData.Clone();

        //            if (vDatos != null)
        //            {
        //                Session["GESTIONES_CONTEO_ADJUNTOS"] = Convert.ToInt32(Session["GESTIONES_CONTEO_ADJUNTOS"]) + 1;
        //                if (vDatos.Rows.Count < 1)
        //                {
        //                    vDatos.Rows.Add(Session["GESTIONES_CONTEO_ADJUNTOS"].ToString(), vNombreArchivo, vArchivoAdjunto);

        //                }
        //                else
        //                {
        //                    Boolean vRegistered = false;
        //                    for (int i = 0; i < vDatos.Rows.Count; i++)
        //                    {
        //                        if (vNombreArchivo == vDatos.Rows[i]["nombre"].ToString())
        //                            throw new Exception("Ya tiene ingresado un adjunto con el mismo nombre.");                            
        //                    }

        //                    if (!vRegistered)
        //                        vDatos.Rows.Add(Session["GESTIONES_CONTEO_ADJUNTOS"].ToString(), vNombreArchivo, vArchivoAdjunto);
        //                }
        //            }

        //            GvAdjunto.DataSource = vDatos;
        //            GvAdjunto.DataBind();
        //            Session["GESTIONES_TAREAS_ADJUNTO"] = vDatos;
        //            divAdjunto.Visible = true;


        //        }
        //    }
        //    catch (Exception Ex) { Mensaje(Ex.Message, WarningType.Danger); }
        //}

        protected void GvAdjunto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvAdjunto.PageIndex = e.NewPageIndex;
                GvAdjunto.DataSource = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];
                GvAdjunto.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
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

        protected void TxBuscarReasignar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargarReasignarSolicitudes();
                String vBusqueda = TxBuscarReasignar.Text;
                DataTable vDatos = (DataTable)Session["GESTIONES_REASIGNAR_TARJETAS"];
                if (vBusqueda.Equals(""))
                {
                    GvReasignar.DataSource = vDatos;
                    GvReasignar.DataBind();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombreResponsable").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idSolicitud"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idSolicitud");
                    vDatosFiltrados.Columns.Add("titulo");
                    vDatosFiltrados.Columns.Add("descripcion");
                    vDatosFiltrados.Columns.Add("minSolicitud");
                    vDatosFiltrados.Columns.Add("fechaInicio");
                    vDatosFiltrados.Columns.Add("fechaEntrega");
                    vDatosFiltrados.Columns.Add("nombreGestion");
                    vDatosFiltrados.Columns.Add("prioridad");
                    vDatosFiltrados.Columns.Add("detalleFinalizo");
                    vDatosFiltrados.Columns.Add("nombreResponsable");
                    vDatosFiltrados.Columns.Add("nombreTeams");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idSolicitud"].ToString(),
                            item["titulo"].ToString(),
                            item["descripcion"].ToString(),
                            item["minSolicitud"].ToString(),
                            item["fechaInicio"].ToString(),
                            item["fechaEntrega"].ToString(),
                            item["nombreGestion"].ToString(),
                            item["prioridad"].ToString(),
                            item["detalleFinalizo"].ToString(),
                            item["nombreResponsable"].ToString(),
                            item["nombreTeams"].ToString()
                            );
                    }

                    GvReasignar.DataSource = vDatosFiltrados;
                    GvReasignar.DataBind();
                    Session["GESTIONES_REASIGNAR_TARJETAS"] = vDatosFiltrados;
                    UpTablaReasignar.Update();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvDetener_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detener")
            {
                string vIdTarjeta = e.CommandArgument.ToString();
                Session["GESTIONES_ID_TARJETA_DETENER"] = vIdTarjeta;

                try
                {
                    LbTituloTarjeta.InnerText = "Detener Tarjeta Kanban";
                    Response.Redirect("/pages/creacionTareas.aspx?ex=" + vIdTarjeta + "&exDetener=1");

                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }

        protected void GvDetener_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GvSolicitudes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvSolicitudes.PageIndex = e.NewPageIndex;
                GvSolicitudes.DataSource = (DataTable)Session["GESTIONES_MIS_SOLICITUDES"];
                GvSolicitudes.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }



    }
}