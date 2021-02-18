﻿using System;
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

                String vQuery = "GESTIONES_Solicitud 20,'" + Session["USUARIO"].ToString() + "','" + Session["USUARIO"].ToString() + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

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
    }
}