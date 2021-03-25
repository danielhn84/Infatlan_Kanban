using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban.classes;
using System.Data;
using System.Net;



namespace Infatlan_Kanban.pages
{
    public partial class gestiones : System.Web.UI.Page
    {
        db vConexion = new db();
        db vConexionGestiones = new db();

        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            select2();
            if (!Page.IsPostBack)
            {
                if (Convert.ToBoolean(Session["AUTH"]))
                {
                    cargarDatos();
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
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarModal();
                string vQuery = "GESTIONES_Generales 26";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                if (vDatos.Rows.Count > 0)
                {
                    LBTeams.Items.Clear();
                    foreach (DataRow item in vDatos.Rows)
                    {
                        LBTeams.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["idTeams"].ToString() + " - " + item["nombre"].ToString() });
                    }
                }
                LbIdGestion.Text = "Crear Nueva Gestión";
                divTeams.Visible = true;
                BtnAddTeams.Visible = false;
                Session["GESTIONES_ID"] = null;
                GvGestionesTeams.DataSource = null;
                GvGestionesTeams.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        private void validarDatos()
        {
            if (TxGestion.Text == "" || TxGestion.Text == string.Empty)
                throw new Exception("Favor ingrese tipo de gestión.");

            if (LBTeams.SelectedIndex == -1)
                throw new Exception("Por favor seleccione equipos de trabajo que tendra la gestión.");
        }
        void limpiarModal()
        {
            TxGestion.Text = string.Empty;
            LBTeams.Items.Clear();
            DivMensaje.Visible = false;
            Session["GESTIONES_ID"] = null;
            togBtn.Checked = false;
        }
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {          
                String vMensaje = "";
                String vQuery = "";
                int vInfo = 0;
                int vConteo = 0;

                DataTable vDataRef = new DataTable();
                vDataRef.Columns.Add("idTeams");

                if (HttpContext.Current.Session["GESTIONES_ID"] == null)
                {
                    validarDatos();
                    Boolean vValidacion= togBtn.Checked;
                    String vValidacionQuery = Convert.ToString(vValidacion) == "True" ? "Si" : "No";
                    vQuery = "GESTIONES_Generales 10,'" + TxGestion.Text + "','1','" + Session["USUARIO"].ToString()+"','"+ vValidacionQuery+"'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string idGestion = vDatos.Rows[0]["idTipoGestion"].ToString();

                    for (int i = 0; i < LBTeams.Items.Count; i++)
                    {
                        if (LBTeams.Items[i].Selected)
                        {
                            vDataRef.Rows.Add(LBTeams.Items[i].Value);
                        }
                    }
                    if (vDataRef.Rows.Count > 0)
                    {
                        for (int i = 0; i < vDataRef.Rows.Count; i++)
                        {
                            vQuery = "GESTIONES_Generales 27,'" + vDataRef.Rows[i]["idTeams"].ToString() + "','" + idGestion + "',1";
                            int vResp = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                            vConteo = vConteo + 1;
                        }
                    }
                    vMensaje = "Tipo de gestión registrado con éxito";
                }
                else
                {
                    if (TxGestion.Text == "" || TxGestion.Text == string.Empty)
                        throw new Exception("Favor ingrese tipo de gestión.");

                    Boolean vValidacion = togBtn.Checked;
                    String vValidacionQuery = Convert.ToString(vValidacion) == "True" ? "Si" : "No";

                    vQuery = "GESTIONES_Generales 12,'" + Session["GESTION_ID"].ToString() + "','" + TxGestion.Text + "','" + Session["USUARIO"].ToString() + "','"+ vValidacionQuery + "'";
                    vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    string vIdGestion = Session["GESTIONES_ID"].ToString();
                    vQuery = "[GESTIONES_Generales] 28,'" + vIdGestion + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                    if (vDatos.Rows.Count == 0)
                    {
                        vQuery = "[GESTIONES_Generales] 55,'" + vIdGestion + "','"+ Session["USUARIO"].ToString() + "'";
                        vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                    }
                    limpiarModal();
                    vMensaje = "Tipo de gestión actualizado con éxito";

                }


                if (vConteo == vDataRef.Rows.Count)
                {
                    Mensaje(vMensaje, WarningType.Success);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
            }
            catch (Exception ex)
            {
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
            }
        }
        private void cargarDatos()
        {
            try
            {
                String vQuery = "GESTIONES_Generales 39";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["GESTIONES_TIPOS"] = vDatos;
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable vDatos = new DataTable();
                String vQuery = "";
                string vIdGestion = e.CommandArgument.ToString();
                Session["GESTIONES_ID"] = vIdGestion;
                if (e.CommandName == "EditarGestion")
                {
                    DivMensaje.Visible = false;
                    BtnAddTeams.Visible = true;
                    LbIdGestion.Text = "Editar Tipo Gestión " + vIdGestion;
                    Session["GESTION_ID"] = vIdGestion;
                    //DivEstado.Visible = true;
                    vQuery = "[GESTIONES_Generales] 11," + vIdGestion + "";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    TxGestion.Text = vDatos.Rows[0]["nombreGestion"].ToString();
                    
                    String vValidacionQuery = vDatos.Rows[0]["validacion"].ToString() == "Si" ? "True" : "False";
      

                    togBtn.Checked= Convert.ToBoolean(vValidacionQuery);

                    vQuery = "[GESTIONES_Generales] 28," + vIdGestion + "";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    GvGestionesTeams.DataSource = vDatos;
                    GvGestionesTeams.DataBind();
                    Session["GESTIONES_LISTADO_GESTIONES"] = vDatos;
                    divTeams.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["GESTIONES_TIPOS"];
                GVBusqueda.DataBind();
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
                cargarDatos();
                String vBusqueda = TxBusqueda.Text;
                DataTable vDatos = (DataTable)Session["GESTIONES_TIPOS"];
                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombreGestion").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idTipoGestion"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idTipoGestion");
                    vDatosFiltrados.Columns.Add("nombreGestion");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idTipoGestion"].ToString(),
                            item["nombreGestion"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["GESTIONES_TIPOS"] = vDatosFiltrados;
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void GvGestionesTeams_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String vMensaje = "";
            if (e.CommandName == "Eliminar")
            {
                string vId = e.CommandArgument.ToString();
                string vIdGestion = Session["GESTIONES_ID"].ToString();
                try
                {
                    string vQuery = "[GESTIONES_Generales] 29,'" + vId + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    vQuery = "[GESTIONES_Generales] 28," + vIdGestion + "";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    GvGestionesTeams.DataSource = vDatos;
                    GvGestionesTeams.DataBind();
                    UpTeamsGestiones.Update();
                }
                catch (Exception ex)
                {
                    Mensaje(ex.Message, WarningType.Danger);
                }
            }
        }
        protected void BtnAddTeams_Click(object sender, EventArgs e)
        {
            try
            {
                lbAdvertenciaTeams.Text = "";
                divTeamsSeleccionado.Visible = false;
                UpdatePanel3.Update();

                LbTitulo.Text = "Agregar Equipo de Trabajo";
                DdlTeams.Items.Clear();
                string vQuery = "GESTIONES_Generales 26";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlTeams.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                if (vDatos.Rows.Count > 0)
                {
                    DdlTeams.Items.Clear();
                    foreach (DataRow item in vDatos.Rows)
                    {
                        DdlTeams.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["nombre"].ToString() });
                    }
                }

                UpdatePanel3.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTeams();", true);

            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
        protected void BtnAnadirTeams_Click(object sender, EventArgs e)
        {
            try
            {
                lbAdvertenciaTeams.Text = "";
                divTeamsSeleccionado.Visible = false;
                UpdatePanel3.Update();

                validarAgregarTeams();

                string vQuery = "GESTIONES_Generales 27,'" + DdlTeams.SelectedValue + "','" + Session["GESTIONES_ID"].ToString() + "',1";
                int vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                if (vInfo == 1)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "cerrarModalTeams();", true);
                    vQuery = "[GESTIONES_Generales] 28," + Session["GESTIONES_ID"].ToString() + "";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    GvGestionesTeams.DataSource = vDatos;
                    GvGestionesTeams.DataBind();
                    UpTeamsGestiones.Update();
                    lbAdvertenciaTeams.Text = "";
                    divTeamsSeleccionado.Visible = false;
                    UpdatePanel3.Update();

            }
            catch (Exception ex)
            {
                lbAdvertenciaTeams.Text = ex.Message;
                divTeamsSeleccionado.Visible = true;
            }
        }


        void validarAgregarTeams()
        {

            string vQuery = "[GESTIONES_Generales] 28," + Session["GESTION_ID"].ToString() + "";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            if (vDatos.Rows.Count > 0)
            {
                for (int i = 0; i < vDatos.Rows.Count; i++)
                {
                    if (vDatos.Rows[i]["idTeams"].ToString() == DdlTeams.SelectedValue)
                        throw new Exception("Equipo de trabajo ya está agregado a la lista.");
                }
            }

        }

        protected void DdlTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbAdvertenciaTeams.Text = "";
                divTeamsSeleccionado.Visible = false;
                UpdatePanel3.Update();

                validarAgregarTeams();
            }
            catch (Exception ex)
            {
                lbAdvertenciaTeams.Text = ex.Message;
                divTeamsSeleccionado.Visible = true;
            }
        }

        protected void GvGestionesTeams_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvGestionesTeams.PageIndex = e.NewPageIndex;
                GvGestionesTeams.DataSource = (DataTable)Session["GESTIONES_LISTADO_GESTIONES"];
                GvGestionesTeams.DataBind();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void GVBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session["GESTIONES_ID"] == null)
            {
                limpiarModal();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
            }
            else
            {
                string vIdGestion = Session["GESTIONES_ID"].ToString();
                string vQuery = "[GESTIONES_Generales] 28,'" + vIdGestion + "'";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                if (vDatos.Rows.Count == 0)
                {
                    vQuery = "[GESTIONES_Generales] 55,'" + vIdGestion + "','" + Session["USUARIO"].ToString() + "'";
                    Int32 vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                }

                limpiarModal();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
            }
        }

        protected void BtnDescargar_Click(object sender, EventArgs e)
        {


            String vQuery = "GESTIONES_Generales 39";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            if (vDatos.Rows.Count > 0)
            {
                ReportExecutionService.ReportExecutionService vRSE = new ReportExecutionService.ReportExecutionService();
                vRSE.Credentials = new NetworkCredential("report_user", "kEbn2HUzd$Fs2T", "adbancat.hn");
                vRSE.Url = "http://10.128.0.52/reportserver/reportexecution2005.asmx";

                vRSE.ExecutionHeaderValue = new ReportExecutionService.ExecutionHeader();
                var vEInfo = new ReportExecutionService.ExecutionInfo();
                vEInfo = vRSE.LoadReport("/GestionesTecnicas/GestionesListadoActivas", null);

                string Parametro1 = "1";
                List<ReportExecutionService.ParameterValue> vParametros = new List<ReportExecutionService.ParameterValue>();
                vParametros.Add(new ReportExecutionService.ParameterValue { Name = "Parametro1", Value = Parametro1 });
                //vParametros.Add(new ReportExecutionService.ParameterValue { Name = "PARAM2", Value = Parametro2 });
                //vParametros.Add(new ReportExecutionService.ParameterValue { Name = "PARAM3", Value = Parametro3 });

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
                Response.AddHeader("Content-disposition", "attachment;filename=Gestiones.xls");
                Response.End();

            }

        }
    }
}