using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban.classes;
using System.Data;


namespace Infatlan_Kanban.pages
{
    public partial class usuarios : System.Web.UI.Page
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

        private void cargarDatos()
        {
            try
            {

                string vIdRol = Session["ID_ROL_USUARIO"].ToString();
                if (vIdRol == "1")
                {
                    DdlEquipo.Items.Clear();
                    String vQuery = "GESTIONES_Solicitud 27,'" + Session["USUARIO"].ToString() + "'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlEquipo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlEquipo.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }

                    string vQueryColaboradores = "";
                    DataTable vDatosColaboradores = new DataTable();
                    vQueryColaboradores = "GESTIONES_Generales 32,'" + Session["USUARIO"].ToString() + "'";
                    vDatosColaboradores = vConexionGestiones.obtenerDataTableGestiones(vQueryColaboradores);

                    if (vDatosColaboradores.Rows.Count > 0)
                    {
                        GVBusqueda.DataSource = vDatosColaboradores;
                        GVBusqueda.DataBind();
                        Session["GESTIONES_COLABORADOR_TEAMS"] = vDatosColaboradores;
                    }
                }
                else if (vIdRol == "3" || vIdRol == "4" || vIdRol == "5")
                {
                    DdlEquipo.Items.Clear();
                    String vQuery = "GESTIONES_Solicitud 25";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    DdlEquipo.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                    if (vDatos.Rows.Count > 0)
                    {
                        foreach (DataRow item in vDatos.Rows)
                        {
                            DdlEquipo.Items.Add(new ListItem { Value = item["idTeams"].ToString(), Text = item["nombre"].ToString() });
                        }
                    }


                    string vQueryColaboradores = "";
                    DataTable vDatosColaboradores = new DataTable();
                    vQueryColaboradores = "GESTIONES_Generales 54";
                    vDatosColaboradores = vConexionGestiones.obtenerDataTableGestiones(vQueryColaboradores);

                    if (vDatosColaboradores.Rows.Count > 0)
                    {
                        GVBusqueda.DataSource = vDatosColaboradores;
                        GVBusqueda.DataBind();
                        Session["GESTIONES_COLABORADOR_TEAMS"] = vDatosColaboradores;
                    }
                }



            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarModal();
                Session["GESTIONES_ID_EMPLEADO"] = null;
                DivColaborador.Visible = true;
                DivMensaje.Visible = false;
                UpdatePanelModal.Update();
                string vQuery = "GESTIONES_General 6";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                DdlColaborador.Items.Clear();

                DdlColaborador.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                foreach (DataRow item in vDatos.Rows)
                {
                    DdlColaborador.Items.Add(new ListItem { Value = item["idEmpleado"].ToString(), Text = item["idEmpleado"].ToString() + " - " + item["nombre"].ToString() });
                }

                vQuery = "GESTIONES_Generales 42";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlRol.Items.Clear();

                DdlRol.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                foreach (DataRow item in vDatos.Rows)
                {
                    DdlRol.Items.Add(new ListItem { Value = item["idRol"].ToString(), Text = item["rol"].ToString() });

                }

                LbTituloModal.Text = "Agregar Colaborador";
                UpdatePanelModal.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DdlColaborador_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DivMensaje.Visible = false;
                string vQuery = "GESTIONES_General 7,'"+ DdlColaborador.SelectedValue + "'";
                DataTable vDatos = vConexion.obtenerDataTable(vQuery);
                TxNombre.Text = vDatos.Rows[0]["nombre"].ToString();
                TxCorreo.Text = vDatos.Rows[0]["emailEmpresa"].ToString();
                TxCodigo.Text = vDatos.Rows[0]["idEmpleado"].ToString();
                TxUsuario.Text = vDatos.Rows[0]["adUser"].ToString();
                UpdatePanelModal.Update();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal()
        {
            TxNombre.Text = string.Empty;
            TxCorreo.Text = string.Empty;
            DdlColaborador.SelectedIndex = -1;
            DdlEstadoUsuario.SelectedIndex = -1;
            TxCodigo.Text = string.Empty;
            TxUsuario.Text = string.Empty;
            TxColor.Text = string.Empty;
            DivMensaje.Visible = false;
        }
        private void validarDatos()
        {
            if (HttpContext.Current.Session["GESTIONES_ID_EMPLEADO"] == null)
            {
                string vQuery = "[GESTIONES_Generales] 35";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    for (int i = 0; i < vDatos.Rows.Count; i++)
                    {
                        if (vDatos.Rows[i]["codEmpleado"].ToString() == DdlColaborador.SelectedValue)
                            throw new Exception("Colaborador ya está agregado al equipo de trabajo: "+ vDatos.Rows[i]["teams"].ToString());
                    }
                }
            }



            if (TxNombre.Text == "" || TxNombre.Text == string.Empty)
            throw new Exception("Favor verifiqué que el campo nombre no esté vacío.");

            if (TxCorreo.Text == "" || TxCorreo.Text == string.Empty)
                throw new Exception("Favor verifiqué que el campo correo no esté vacío.");

            if (TxCodigo.Text == "" || TxCodigo.Text == string.Empty)
                throw new Exception("Favor verifiqué que el campo código no esté vacío.");

            if (TxUsuario.Text == "" || TxUsuario.Text == string.Empty)
                throw new Exception("Favor verifiqué que el campo usuario no esté vacío.");

            if (DdlRol.SelectedValue.Equals("0"))
                throw new Exception("Favor verifiqué que el campo de rol sea válido.");

            if (DdlEquipo.SelectedValue.Equals("0"))
                throw new Exception("Favor verifiqué que el campo de equipo sea válido.");
        }

            protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                string vMensaje = "";
                String vQuery = "";
                int vInfo=0;

                if (HttpContext.Current.Session["GESTIONES_ID_EMPLEADO"] == null)
                {
                    validarDatos();
                    vQuery = "GESTIONES_Generales 31,'" + DdlEquipo.SelectedValue + "','" + TxNombre.Text + "','" + TxCorreo.Text + "','" + TxColor.Text + "','" + TxUsuario.Text + "','" + TxCodigo.Text + "','" + Session["USUARIO"].ToString() + "',1,'"+ Session["GESTIONES_ID_JEFE_USER"].ToString()+"','"+ Session["GESTIONES_ID_SUPLENTE_USER"]+"','"+ DdlRol.SelectedValue+"'";
                    vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                    vMensaje = "Colaborador registrado con éxito";
                }
                else
                {
                    vQuery = "GESTIONES_Generales 34,'" + Session["GESTIONES_ID_EMPLEADO"].ToString() + "','" + DdlEstadoUsuario.SelectedValue + "','" + TxColor.Text + "','" + Session["USUARIO"].ToString() + "','"+ DdlRol.SelectedValue+ "','"+ DdlEquipo.SelectedValue + "'";
                    vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                    vMensaje = "Colaborador actualizado con éxito";
                }
        
                if (vInfo == 1)
                {
                    Mensaje(vMensaje, WarningType.Success);
                    limpiarModal();
                    UpdatePanelModal.Update();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "cerrarModal();", true);
                    cargarDatos();
                }
             }
            catch (Exception ex)
            {
                LbAdvertencia.Text = ex.Message;
                DivMensaje.Visible = true;
                UpdatePanelModal.Update();
            }
        }

        protected void GVBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                string vQuery = "GESTIONES_Generales 42";
                DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                DdlRol.Items.Clear();

                DdlRol.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                foreach (DataRow item in vDatos.Rows)
                {
                    DdlRol.Items.Add(new ListItem { Value = item["idRol"].ToString(), Text = item["rol"].ToString() });

                }

                //DataTable vDatos = new DataTable();
                //String vQuery = "";
                string vIEmpleado = e.CommandArgument.ToString();
                Session["GESTIONES_ID_EMPLEADO"] = vIEmpleado;
                if (e.CommandName == "EditarEmpleado")
                {
                    DivMensaje.Visible = false;
                    LbTituloModal.Text = "Editar Empleado " + vIEmpleado;

                    vQuery = "[GESTIONES_Generales] 33," + vIEmpleado + "";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

               
                    TxNombre.Text = vDatos.Rows[0]["nombre"].ToString();
                    TxCorreo.Text = vDatos.Rows[0]["email"].ToString();
                    TxCodigo.Text = vDatos.Rows[0]["codEmpleado"].ToString();
                    TxUsuario.Text = vDatos.Rows[0]["adUser"].ToString();
                    DdlEstadoUsuario.SelectedValue ="1";
                    DdlRol.SelectedValue = vDatos.Rows[0]["IdRol"].ToString();
                    TxColor.Text = vDatos.Rows[0]["colorTarjeta"].ToString();
                    DdlEquipo.SelectedValue = vDatos.Rows[0]["idTeams"].ToString();
                    DivColaborador.Visible = false;

                    UpdatePanelModal.Update();
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
                GVBusqueda.DataSource = (DataTable)Session["GESTIONES_COLABORADOR_TEAMS"];
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
                DataTable vDatos = (DataTable)Session["GESTIONES_COLABORADOR_TEAMS"];
                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }
                else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("teams").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["codEmpleado"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idEmpleado");
                    vDatosFiltrados.Columns.Add("teams");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("email");
                    vDatosFiltrados.Columns.Add("adUser");
                    vDatosFiltrados.Columns.Add("codEmpleado");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idEmpleado"].ToString(),
                            item["teams"].ToString(),
                            item["nombre"].ToString(),
                            item["email"].ToString(),
                            item["adUser"].ToString(),
                            item["codEmpleado"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["GESTIONES_COLABORADOR_TEAMS"] = vDatosFiltrados;
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DdlEquipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}