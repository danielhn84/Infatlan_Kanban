using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban_GestionesTecnicas.classes;
using System.Data;
using System.IO;
using System.Net;
using Infatlan_Kanban.classes;
using System.Drawing;

namespace Infatlan_Kanban.pages
{
    public partial class Tarjetas : System.Web.UI.Page
    {
        db vConexionGestiones = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            select2();
            string vIdRol = Session["ID_ROL_USUARIO"].ToString();
            if (vIdRol=="2")
            {
                nav_Reasignar.Visible = false;
                nav_tarjetaDetenido_tab.Visible = false;
                nav_tarjetasCerradas_tab.Visible = true;
            }
            else
            {
                nav_Reasignar.Visible = true;
                nav_tarjetaDetenido_tab.Visible = true;
                nav_tarjetasCerradas_tab.Visible = true;
            }
   
            if (!Page.IsPostBack)
            {
                select2();
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarInicialTarjeta();
                    cargarInicialMisSolicitudes();
                    cargarDetenerSolicitudes();
                    cargarReasignarSolicitudes();

                    divTXBusqueda.Visible = false;

                    lbInicio.Visible = false;
                    divTxInicio.Visible = false;
                    DivlbFin.Visible = false;
                    divFechaFin.Visible = false;
                    divBotones.Visible = false;
                    UpdatePanel9.Update();

                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }
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
        void cargarInicialMisSolicitudes()
        {
            try
            {
                LbTituloTarjeta.InnerText = "Mis Tarjetas Cerradas";

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
               Response.Redirect("/pages/Tarjetas.aspx");
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
        protected void GvDetener_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvDetener.PageIndex = e.NewPageIndex;
                GvDetener.DataSource = (DataTable)Session["GESTIONES_DETENER_TARJETAS"];
                GvDetener.DataBind();
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
                    LbTitulo.Text = "Detener Tarjeta Kanban: " + vIdTarjeta;
                    UpTitulo.Update();

                    cargarDatosTarjeta();
                    DdlTipoGestion_1.Enabled = false;
                    UPFormulario.Update();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaOpen();", true);
                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
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

        void camposHabilitados()
        {
            TxFechaSolicitud_1.ReadOnly = false;
            TxTitulo.ReadOnly = false;
            TxDescripcion_1.ReadOnly = false;
            DdlResponsable_1.Enabled = true;
            TxMinProductivo_1.ReadOnly = false;
            TxFechaInicio_1.ReadOnly = false;
            TxFechaEntrega_1.ReadOnly = false;
            DdlPrioridad_1.Enabled = true;
            DdlTipoGestion_1.Enabled = true;
        }
        void cargarDatosTarjeta()
        {
            camposDeshabilitados();
            tabAdjuntos.Visible = true;
            String vEx = "";
     
            if (Session["GESTIONES_ID_TARJETA_REASIGNAR"] == null)
            {
                vEx = Session["GESTIONES_ID_TARJETA_DETENER"].ToString();
            }
            else
            {
                vEx = Session["GESTIONES_ID_TARJETA_REASIGNAR"].ToString();
            }

                //DATOS GENERALES
                string vQuery = "GESTIONES_Solicitud 12,'" + vEx + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

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

            Session["GESTIONES_USUARIO_CREO"] = vDatos.Rows[0]["usuarioCreo"].ToString();


            vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable_1.SelectedValue + "'";
            DataTable vDatosTeams = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vTeams = vDatosTeams.Rows[0]["idTeams"].ToString();

            DdlTipoGestion_1.Items.Clear();
            DdlTipoGestion_1.Enabled = true;
            vQuery = "GESTIONES_Generales 1,'" + vTeams + "'";
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
                //LbAlertaComentario.InnerText = "Tarjeta no cuenta con comentarios, si desea puede adicionar";
                //divComentarioLectura.Visible = false;
                //divAlertaComentario.Visible = true;
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



        }
        void cargarInicialTarjeta()
        {
            try
            {                       
                DdlResponsable_1.Items.Clear();
                String vQuery = "GESTIONES_Generales 37,'" + Session["USUARIO"].ToString() + "','" + Session["USUARIO"].ToString() + "'";
                DataTable vDatosResponsables = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            
                DdlResponsable_1.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatosResponsables.Rows.Count > 0)
                {
                    foreach (DataRow item in vDatosResponsables.Rows)
                    {
                      
                        DdlResponsable_1.Items.Add(new ListItem { Value = item["codEmpleado"].ToString(), Text = item["nombre"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void cargarReasignarSolicitudes()
        {
            try
            {
                LbTituloModificarTarjeta.InnerText = "Reasignación de Tarjeta";


                String vQuery = "";
                DataTable vDatos = null;
                string vIdRol = Session["ID_ROL_USUARIO"].ToString();
                if (vIdRol == "5")//ADMINISTRADOR
                {
                    vQuery = "GESTIONES_Solicitud 29";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                }
                else
                {
                    vQuery = "GESTIONES_Solicitud 20,'" + Session["USUARIO"].ToString() + "','" + Session["USUARIO"].ToString() + "'";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                }

                GvReasignar.DataSource = vDatos;
                GvReasignar.DataBind();
                Session["GESTIONES_REASIGNAR_TARJETAS"] = vDatos;
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
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

        protected void GvReasignar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Reasignar")
            {
                string vIdTarjeta = e.CommandArgument.ToString();
                Session["GESTIONES_ID_TARJETA_REASIGNAR"] = vIdTarjeta;
                try
                {
                    LbTitulo.Text = "Reasignar Tarjeta Kanban: " + vIdTarjeta;
                    UpTitulo.Update();

                    cargarDatosTarjeta();
                    //camposHabilitados();
  
                    tabSolucion.Visible = false;
                    TxFechaEntrega_1.ReadOnly = false;
                    TxFechaInicio_1.ReadOnly = false;

                    DdlTipoGestion_1.Enabled = true;
                    DdlResponsable_1.Enabled = true;
                    DdlPrioridad_1.Enabled = true;
                    divNotasReasignar.Visible = true;
                    UPFormulario.Update();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaOpen();", true);
                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }


        protected void GvSolicitudes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string vPrioridad = e.Row.Cells[0].Text;
                string vEstado = e.Row.Cells[9].Text;


                if (vPrioridad.Equals("Baja"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#03a9f3");
                }
                else if (vPrioridad.Equals("M&#225;xima Prioridad"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#e46a76");
                }
                else if (vPrioridad.Equals("Alta"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#fb9678");
                }
                else if (vPrioridad.Equals("Normal"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#fec107");
                }


                //if (vEstado.Equals("Realizado fuera de tiempo"))
                //{
                //    e.Row.Cells[9].BackColor = Color.FromName("#FFA08D");
                //}
                //else
                //{
                //    e.Row.Cells[9].BackColor = Color.FromName("#76DE7E");
                //}
            }        
        }

        protected void BtnBusqueda_Click(object sender, EventArgs e)
        {
            DivBusquedaReporte.Visible = true;
            UpdatePanel9.Update();
        }

        protected void DdlTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DdlTipoBusqueda.SelectedValue == "1")
            {
                divTXBusqueda.Visible = true;

                lbInicio.Visible = false;
                divTxInicio.Visible = false;
                DivlbFin.Visible = false;
                divFechaFin.Visible = false;
                divBotones.Visible = false;
                UpdatePanel9.Update();
            }
            else if(DdlTipoBusqueda.SelectedValue=="2")
            {
                divTXBusqueda.Visible = false;

                lbInicio.Visible = true;
                divTxInicio.Visible = true;
                DivlbFin.Visible = true;
                divFechaFin.Visible = true;
                divBotones.Visible = true;
                UpdatePanel9.Update();

            }
            else
            {
                Response.Redirect("/pages/Tarjetas.aspx");
            }
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

        protected void TxBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {

                cargarInicialMisSolicitudes();
                String vBusqueda = TxBusqueda.Text;
                DataTable vDatos = (DataTable)Session["GESTIONES_MIS_SOLICITUDES"];
                if (vBusqueda.Equals(""))
                {
                    GvSolicitudes.DataSource = vDatos;
                    GvSolicitudes.DataBind();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("titulo").Contains(vBusqueda.ToUpper()));

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
                    vDatosFiltrados.Columns.Add("prioridad");
                    vDatosFiltrados.Columns.Add("idSolicitud");
                    vDatosFiltrados.Columns.Add("titulo");
                    vDatosFiltrados.Columns.Add("descripcion");
                    vDatosFiltrados.Columns.Add("minSolicitud");
                    vDatosFiltrados.Columns.Add("fechaInicio");
                    vDatosFiltrados.Columns.Add("fechaEntrega");
                    vDatosFiltrados.Columns.Add("nombreGestion");
                    vDatosFiltrados.Columns.Add("userCreo");
                    vDatosFiltrados.Columns.Add("nombreestado");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["prioridad"].ToString(),
                            item["idSolicitud"].ToString(),
                            item["titulo"].ToString(),
                            item["descripcion"].ToString(),
                            item["minSolicitud"].ToString(),
                            item["fechaInicio"].ToString(),
                            item["fechaEntrega"].ToString(),
                            item["nombreGestion"].ToString(),
                            item["userCreo"].ToString(),
                            item["nombreestado"].ToString()
                            );
                    }

                    GvSolicitudes.DataSource = vDatosFiltrados;
                    GvSolicitudes.DataBind();
                    Session["GESTIONES_MIS_SOLICITUDES"] = vDatosFiltrados;
                    UpMisSolicitudes.Update();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvDetener_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string vPrioridad = e.Row.Cells[0].Text;

                if (vPrioridad.Equals("Baja"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#03a9f3");
                }
                else if (vPrioridad.Equals("M&#225;xima Prioridad"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#e46a76");
                }
                else if (vPrioridad.Equals("Alta"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#fb9678");
                }
                else if (vPrioridad.Equals("Normal"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#fec107");
                }

            }
        }

        protected void TxBusquedaDetener_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                 cargarDetenerSolicitudes();
                String vBusqueda = TxBusquedaDetener.Text;
                DataTable vDatos = (DataTable)Session["GESTIONES_DETENER_TARJETAS"];
                if (vBusqueda.Equals(""))
                {
                    GvDetener.DataSource = vDatos;
                    GvDetener.DataBind();
                    UpdatePanel8.Update();
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
                    vDatosFiltrados.Columns.Add("prioridad");
                    vDatosFiltrados.Columns.Add("idSolicitud");
                    vDatosFiltrados.Columns.Add("titulo");
                    vDatosFiltrados.Columns.Add("minSolicitud");
                    vDatosFiltrados.Columns.Add("fechaInicio");
                    vDatosFiltrados.Columns.Add("fechaEntrega");
                    vDatosFiltrados.Columns.Add("nombreGestion");
                    vDatosFiltrados.Columns.Add("detalleFinalizo");
                    vDatosFiltrados.Columns.Add("nombreResponsable");
                    vDatosFiltrados.Columns.Add("nombreTeams");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["prioridad"].ToString(),
                            item["idSolicitud"].ToString(),
                            item["titulo"].ToString(),
                            item["minSolicitud"].ToString(),
                            item["fechaInicio"].ToString(),
                            item["fechaEntrega"].ToString(),
                            item["nombreGestion"].ToString(),
                            item["detalleFinalizo"].ToString(),
                            item["nombreResponsable"].ToString(),
                            item["nombreTeams"].ToString()
                            );
                    }

                    GvDetener.DataSource = vDatosFiltrados;
                    GvDetener.DataBind();
                    Session["GESTIONES_DETENER_TARJETAS"]= vDatosFiltrados;
                    UpdatePanel8.Update();
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GvReasignar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string vPrioridad = e.Row.Cells[0].Text;

                if (vPrioridad.Equals("Baja"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#03a9f3");
                }
                else if (vPrioridad.Equals("M&#225;xima Prioridad"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#e46a76");
                }
                else if (vPrioridad.Equals("Alta"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#fb9678");
                }
                else if (vPrioridad.Equals("Normal"))
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#fec107");
                }

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
                    UpTablaReasignar.Update();
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
                    vDatosFiltrados.Columns.Add("prioridad");
                    vDatosFiltrados.Columns.Add("idSolicitud");
                    vDatosFiltrados.Columns.Add("titulo");
                    vDatosFiltrados.Columns.Add("descripcion");
                    vDatosFiltrados.Columns.Add("minSolicitud");
                    vDatosFiltrados.Columns.Add("fechaInicio");
                    vDatosFiltrados.Columns.Add("fechaEntrega");
                    vDatosFiltrados.Columns.Add("nombreGestion");
                    vDatosFiltrados.Columns.Add("nombreResponsable");
                    vDatosFiltrados.Columns.Add("nombreTeams");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["prioridad"].ToString(),
                            item["idSolicitud"].ToString(),
                            item["titulo"].ToString(),
                            item["descripcion"].ToString(),
                            item["minSolicitud"].ToString(),
                            item["fechaInicio"].ToString(),
                            item["fechaEntrega"].ToString(),
                            item["nombreGestion"].ToString(),
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



        private void validacionesCrearSolicitud()
        {
            if (TxTitulo.Text.Equals(""))
                throw new Exception("Falta que ingrese el título de la tarea.");

            if (TxDescripcion_1.Text.Equals(""))
                throw new Exception("Falta que ingrese la descripción de la tarea.");

            if (DdlResponsable_1.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione responsable.");

            if (TxMinProductivo_1.Text.Equals(""))
                throw new Exception("Falta que ingrese tiempo productivo en (min).");

            if (DdlTipoGestion_1.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione tipo de gestión.");

            if (TxFechaEntrega_1.Text.Equals(""))
                throw new Exception("Falta que ingrese la fecha de entrega.");

            if (DdlPrioridad_1.SelectedValue.Equals("0"))
                throw new Exception("Falta que seleccione tipo de prioridad.");

            string vQuery = "GESTIONES_Generales 5,'" + DdlResponsable_1.SelectedValue + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vCantAtrasados = vDatos.Rows[0]["cantAtrasado"].ToString();
            if (Convert.ToInt32(vCantAtrasados) >= 5 && DdlTipoGestion_1.SelectedValue != "4")
                throw new Exception("Límite establecido de tareas atrasadas: 5, y usted actualmete tiene: " + vCantAtrasados + ", favor finalizar las tarjetas para que pyuedan asignarles nuevas.");


            vQuery = "GESTIONES_Solicitud 7,'" + DdlResponsable_1.SelectedValue + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string vTeams = vDatos.Rows[0]["idTeams"].ToString();
            Session["GESTIONES_CORREO_RESPONSABLE"] = vDatos.Rows[0]["email"].ToString();
            Session["GESTIONES_TEAMS"] = vDatos.Rows[0]["idTeams"].ToString();

            vQuery = "GESTIONES_Solicitud 8,'" + vTeams + "'";
            vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
            Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();

            Session["GESTIONES_HR_INICIO"] = vDatos.Rows[0]["hrInicio"].ToString();
            Session["GESTIONES_HR_FIN"] = vDatos.Rows[0]["hrFin"].ToString();
            Session["GESTIONES_WIP"] = vDatos.Rows[0]["wip"].ToString();


            DateTime fecha_inicio = DateTime.Parse(TxFechaInicio_1.Text.ToString());
            DateTime fecha_fin = DateTime.Parse(TxFechaEntrega_1.Text.ToString());
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


            if (DdlTipoGestion_1.SelectedValue != "4" && fecha_actualIngreso < fecha_actual_corta_masresta_evaluar)  //VALIDACION DIFERENTE A INCIDENTES QUE NO PODRAN INGRESARLA A CUALQUIER HORA           
                throw new Exception("No se puede crear la tarjeta, la fecha actual del sistema es mayor que la fecha de inicio de la tarjeta. Nota: Solo el tipo de gestión incidentes se pueden ingresar a cualquier hora del dia, las demas gestiones deben ser debidamente programadas. ");

            if (fecha_actualIngreso > fecha_actualEntrega)
                throw new Exception("Favor verificar la fecha de inicio, no puede ser mayor que la fecha de entrega");

            if (fecha_actualEntrega < fecha_actualIngreso)
                throw new Exception("Favor verificar la fecha de entrega, no puede ser menor que la fecha de inicio");
        }
        protected void BtnConfirmarTarea_1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    validacionesCrearSolicitud();

            //    GVDistribucion.DataSource = null;
            //    GVDistribucion.DataBind();
            //    Session["GESTIONES_TAREAS_MIN_DIARIOS"] = null;
            //    DateTime fecha_inicio = DateTime.Parse(TxFechaInicio_1.Text.ToString());
            //    DateTime fecha_fin = DateTime.Parse(TxFechaEntrega_1.Text.ToString());

            //    String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
            //    String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
            //    String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

            //    DateTime vfechaActual = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

            //    DateTime vFechaInicio = DateTime.Parse(vFecha1);

            //    double vMinDiarios = 0;
            //    double vWip = Convert.ToInt32(Session["GESTIONES_WIP"].ToString());

            //    DataTable vData = new DataTable();
            //    DataTable vDatosMin = (DataTable)Session["GESTIONES_TAREAS_MIN_DIARIOS"];
            //    vData.Columns.Add("id");
            //    vData.Columns.Add("fecha");
            //    vData.Columns.Add("min");
            //    string vFechaInicioSoli = fecha_inicio.ToString("dd/MM/yyyy");
            //    string vFechaFinSoli = fecha_fin.ToString("dd/MM/yyyy");
            //    DateTime vFechaInicioConver = DateTime.Parse(vFechaInicioSoli);
            //    DateTime vFechaFinConver = DateTime.Parse(vFechaFinSoli);

            //    string vHrInicialSoli = fecha_inicio.ToString("HH:mm");
            //    string vHrFinalSoli = fecha_fin.ToString("HH:mm");

            //    TimeSpan vHrInicialSoliConver = TimeSpan.Parse(vHrInicialSoli);
            //    TimeSpan vHrFinSoliConver = TimeSpan.Parse(vHrFinalSoli);


            //    if (vFechaInicio.DayOfWeek != DayOfWeek.Saturday && vFechaInicio.DayOfWeek != DayOfWeek.Sunday)
            //    {
            //        TimeSpan span = Convert.ToDateTime(vFechaFinConver) - Convert.ToDateTime(vFechaInicioConver);
            //        int businessDays = span.Days;
            //        int fullWeekCount = businessDays / 7;

            //        if (businessDays == 7)
            //        {
            //            businessDays = businessDays - 2;
            //        }
            //        else if (businessDays == 6)
            //        {
            //            businessDays = businessDays - 1;
            //        }
            //        else if (businessDays > fullWeekCount * 7)
            //        {
            //            int firstDayOfWeek = (int)vFechaInicioConver.DayOfWeek;
            //            int lastDayOfWeek = (int)vFechaFinConver.DayOfWeek;
            //            if (lastDayOfWeek < firstDayOfWeek)
            //                lastDayOfWeek += 7;
            //            if (firstDayOfWeek <= 6)
            //            {
            //                if (lastDayOfWeek >= 7)
            //                    businessDays -= 2;
            //                else if (lastDayOfWeek >= 6)
            //                    businessDays -= 1;
            //            }
            //            else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
            //                businessDays -= 1;

            //            //subtract the weekends during the full weeks in the interval
            //            businessDays -= fullWeekCount + fullWeekCount;
            //        }
            //        int vDias = businessDays + 1;
            //        Session["GESTIONES_DIAS"] = vDias;
            //        vMinDiarios = Convert.ToInt32(TxMinProductivo_1.Text) / vDias;

            //        int vCount = 0;
            //        int vResta = 0;

            //        double vMinsFaltante = 0;

            //        if (vDias == 1)
            //        {
            //            if (vDatosMin == null)
            //                vDatosMin = vData.Clone();
            //            if (vDatosMin != null)
            //            {
            //                vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
            //                vDatosMin.Rows.Add("1", vFechaInicioSoli, vMinDiarios);
            //            }
            //        }
            //        else
            //        {

            //            //VALIDACION INICIO TAREA SI TIENE MIN DISPNIBLES
            //            string vCantMinSolicitudes = "";
            //            string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable_1.SelectedValue + "','" + vFechaInicioSoli + "'";
            //            DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
            //            vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();

            //            if (vCantMinSolicitudes != "")
            //            {
            //                if (Convert.ToDouble(vCantMinSolicitudes) >= Convert.ToDouble(vWip))
            //                    throw new Exception("Nota: La fecha seleccionada inicio de la tarjeta ya no cuenta con mins disponibles, su WIP está al limite, favor cambiar la fecha de inicio para poder realizar una mejor distribución de su cargabilidad. Minutos registrados de cargabilidad: " + vCantMinSolicitudes + ", WIP límite establecido: " + vWip);
            //            }


            //            for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
            //            {
            //                if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
            //                {
            //                    vCount = vCount + 1;
            //                    vResta = (vDias - vCount) + 1;

            //                    if (vMinsFaltante != 0)
            //                    {
            //                        vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
            //                        vMinsFaltante = 0;
            //                    }

            //                    string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
            //                    vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable_1.SelectedValue + "','" + vFechaEvaluar + "'";
            //                    vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
            //                    vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
            //                    double vSobranteWIPCreacion = 0;

            //                    if (vCantMinSolicitudes.Equals(""))
            //                    {
            //                        vSobranteWIPCreacion = vWip;
            //                    }
            //                    else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
            //                    {
            //                        vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
            //                    }
            //                    else
            //                    {
            //                        vSobranteWIPCreacion = 0;
            //                    }


            //                    if (vMinDiarios <= vSobranteWIPCreacion)
            //                    {
            //                        if (vDatosMin == null)
            //                            vDatosMin = vData.Clone();
            //                        if (vDatosMin != null)
            //                        {
            //                            if (vDatosMin.Rows.Count < 1)
            //                            {
            //                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                            }
            //                            else
            //                            {
            //                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
            //                        vMinDiarios = vSobranteWIPCreacion;
            //                        if (vDatosMin == null)
            //                            vDatosMin = vData.Clone();
            //                        if (vDatosMin != null)
            //                        {
            //                            if (vDatosMin.Rows.Count < 1)
            //                            {
            //                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                            }
            //                            else
            //                            {
            //                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //        if (vMinsFaltante != 0)
            //            throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

            //    }
            //    else if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday || vFechaInicio.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        calculoDias();
            //        int vDias = Convert.ToInt32(Session["GESTIONES_DIAS"].ToString());
            //        LbDiaNoHabil.Text = "Se debe iniciar a trabajar en la tarjeta un día de trabajo no hábil";
            //        divDiaNoHabil.Visible = true;
            //        if (vFechaInicio.DayOfWeek == DayOfWeek.Saturday)
            //        {
            //            if (vDias == 1)
            //            {
            //                if (vDatosMin == null)
            //                    vDatosMin = vData.Clone();
            //                if (vDatosMin != null)
            //                {
            //                    vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
            //                    vDatosMin.Rows.Add("1", vFechaInicioSoli, TxMinProductivo.Text);
            //                }
            //            }
            //            else
            //            {
            //                vDias = vDias + 2;
            //                vMinDiarios = Convert.ToInt32(TxMinProductivo.Text) / vDias;
            //                DateTime vFechaFinConverDomingo = vFechaInicioConver.AddDays(1);
            //                DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
            //                int vCount = 0;
            //                int vResta = 0;
            //                double vMinsFaltante = 0;

            //                for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
            //                {
            //                    string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
            //                    if (vDatosMin == null)
            //                        vDatosMin = vData.Clone();
            //                    if (vDatosMin != null)
            //                    {
            //                        if (vDatosMin.Rows.Count < 1)
            //                        {
            //                            vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                        }
            //                        else
            //                        {
            //                            vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                        }
            //                    }

            //                }

            //                for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
            //                {
            //                    if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
            //                    {
            //                        vCount = vCount + 1;
            //                        vResta = (vDias - vCount) + 1;

            //                        if (vMinsFaltante != 0)
            //                        {
            //                            vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
            //                            vMinsFaltante = 0;
            //                        }

            //                        string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
            //                        string vCantMinSolicitudes = "";
            //                        string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaEvaluar + "'";
            //                        DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
            //                        vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
            //                        double vSobranteWIPCreacion = 0;

            //                        if (vCantMinSolicitudes.Equals(""))
            //                        {
            //                            vSobranteWIPCreacion = vWip;
            //                        }
            //                        else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
            //                        {
            //                            vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
            //                        }
            //                        else
            //                        {
            //                            vSobranteWIPCreacion = 0;
            //                        }


            //                        if (vMinDiarios <= vSobranteWIPCreacion)
            //                        {
            //                            if (vDatosMin == null)
            //                                vDatosMin = vData.Clone();
            //                            if (vDatosMin != null)
            //                            {
            //                                if (vDatosMin.Rows.Count < 1)
            //                                {
            //                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                                }
            //                                else
            //                                {
            //                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
            //                            vMinDiarios = vSobranteWIPCreacion;
            //                            if (vDatosMin == null)
            //                                vDatosMin = vData.Clone();
            //                            if (vDatosMin != null)
            //                            {
            //                                if (vDatosMin.Rows.Count < 1)
            //                                {
            //                                    vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                                }
            //                                else
            //                                {
            //                                    vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }

            //                if (vMinsFaltante != 0)
            //                    throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);
            //            }
            //        }
            //        else
            //        {
            //            vDias = vDias;
            //            vMinDiarios = Convert.ToInt32(TxMinProductivo.Text) / vDias;
            //            DateTime vFechaFinConverDomingo = vFechaInicioConver;
            //            DateTime vFechaInicioSemana = vFechaFinConverDomingo.AddDays(1);
            //            int vCount = 0;
            //            int vResta = 0;
            //            double vMinsFaltante = 0;

            //            for (DateTime fecha = vFechaInicioConver; fecha <= vFechaFinConverDomingo; fecha = fecha.AddDays(1))
            //            {
            //                string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
            //                if (vDatosMin == null)
            //                    vDatosMin = vData.Clone();
            //                if (vDatosMin != null)
            //                {
            //                    if (vDatosMin.Rows.Count < 1)
            //                    {
            //                        vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                    }
            //                    else
            //                    {
            //                        vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                    }
            //                }

            //            }

            //            for (DateTime fecha = vFechaInicioSemana; fecha <= vFechaFinConver; fecha = fecha.AddDays(1))
            //            {
            //                if (fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday)
            //                {
            //                    vCount = vCount + 1;
            //                    vResta = (vDias - vCount) + 1;

            //                    if (vMinsFaltante != 0)
            //                    {
            //                        vMinDiarios = (((vMinDiarios + vMinsFaltante) * vResta) + vMinsFaltante) / vResta;
            //                        vMinsFaltante = 0;
            //                    }

            //                    string vFechaEvaluar = Convert.ToDateTime(fecha).ToString(vFormato);
            //                    string vCantMinSolicitudes = "";
            //                    string vQuerys = "GESTIONES_Solicitud 9,'" + DdlResponsable.SelectedValue + "','" + vFechaEvaluar + "'";
            //                    DataTable vDato = vConexionGestiones.obtenerDataTableGestiones(vQuerys);
            //                    vCantMinSolicitudes = vDato.Rows[0]["minDiarios"].ToString();
            //                    double vSobranteWIPCreacion = 0;

            //                    if (vCantMinSolicitudes.Equals(""))
            //                    {
            //                        vSobranteWIPCreacion = vWip;
            //                    }
            //                    else if (Convert.ToDouble(vCantMinSolicitudes) <= vWip)
            //                    {
            //                        vSobranteWIPCreacion = vWip - Convert.ToDouble(vDato.Rows[0]["minDiarios"].ToString());
            //                    }
            //                    else
            //                    {
            //                        vSobranteWIPCreacion = 0;
            //                    }


            //                    if (vMinDiarios <= vSobranteWIPCreacion)
            //                    {
            //                        if (vDatosMin == null)
            //                            vDatosMin = vData.Clone();
            //                        if (vDatosMin != null)
            //                        {
            //                            if (vDatosMin.Rows.Count < 1)
            //                            {
            //                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                            }
            //                            else
            //                            {
            //                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        vMinsFaltante = vMinDiarios - vSobranteWIPCreacion;
            //                        vMinDiarios = vSobranteWIPCreacion;
            //                        if (vDatosMin == null)
            //                            vDatosMin = vData.Clone();
            //                        if (vDatosMin != null)
            //                        {
            //                            if (vDatosMin.Rows.Count < 1)
            //                            {
            //                                vDatosMin.Rows.Add("1", vFechaEvaluar, vMinDiarios);
            //                            }
            //                            else
            //                            {
            //                                vDatosMin.Rows.Add((vDatosMin.Rows.Count) + 1, vFechaEvaluar, vMinDiarios);
            //                            }
            //                        }
            //                    }
            //                }
            //            }

            //            if (vMinsFaltante != 0)
            //                throw new Exception("Nota:Debe extender la fecha de entrega debido que la distribución de la cargabilidad de los minutos hay un faltante de: " + vMinsFaltante);

            //        }




            //        //if (vDiasTarjeta == 1)
            //        //{
            //        //    if (vDatosMin == null)
            //        //        vDatosMin = vData.Clone();
            //        //    if (vDatosMin != null)
            //        //    {
            //        //        vFechaInicioSoli = vFechaInicio.ToString("dd/MM/yyyy");
            //        //        vDatosMin.Rows.Add("1", vFechaInicioSoli, TxMinProductivo.Text);
            //        //    }
            //        //}
            //    }


            //    Session["GESTIONES_TAREAS_MIN_DIARIOS"] = vDatosMin;
            //    GVDistribucion.DataSource = vDatosMin;
            //    GVDistribucion.DataBind();

            //    DataTable vDatosComentarios = (DataTable)Session["GESTIONES_TAREAS_COMENTARIOS"];
            //    DataTable vDatosAdjuntos = (DataTable)Session["GESTIONES_TAREAS_ADJUNTO"];

            //    if (vDatosComentarios == null && vDatosAdjuntos == null)
            //    {
            //        LbAdvertenciaModal.Text = "Nota: Los adjuntos y comentarios no son campos obligatorios. Para mayor seguridad se notifica que la tarjeta no cuenta con adjuntos ni comentarios. Si esta seguro dar clic en “Enviar”.";
            //    }
            //    else if (vDatosComentarios == null && vDatosAdjuntos != null)
            //    {
            //        LbAdvertenciaModal.Text = "Nota: Los adjuntos y comentarios no son campos obligatorios. Para mayor seguridad se notifica que la tarjeta no cuenta con comentarios. Si esta seguro dar clic en “Enviar”.";
            //    }
            //    else if (vDatosComentarios != null && vDatosAdjuntos == null)
            //    {
            //        LbAdvertenciaModal.Text = "Nota: Los adjuntos y comentarios no son campos obligatorios. Para mayor seguridad se notifica que la tarjeta no cuenta con adjuntos. Si esta seguro dar clic en “Enviar”.";
            //    }
            //    else
            //    {
            //        LbAdvertenciaModal.Text = "";
            //    }

            //    if (fecha_fin <= vfechaActual)
            //    {
            //        divSolucion.Visible = true;
            //        divAdjuntoSolucion.Visible = true;
            //        divTareaFinalizada.Visible = true;
            //        divDiaNoHabil.Visible = false;
            //        divPrioridad.Visible = false;
            //    }
            //    else
            //    {
            //        divSolucion.Visible = false;
            //        divAdjuntoSolucion.Visible = false;
            //        divTareaFinalizada.Visible = false;
            //        divPrioridad.Visible = true;
            //    }

            //    divComentariosAdjuntos.Visible = true;
            //    cargarModal();
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "ModalTarjetaConfirmarOpen();", true);

            //}
            //catch (Exception ex)
            //{
            //    LbAdvertencia.InnerText = ex.Message;
            //    divAlertaGeneral.Visible = true;
            //}
        }
    }
}