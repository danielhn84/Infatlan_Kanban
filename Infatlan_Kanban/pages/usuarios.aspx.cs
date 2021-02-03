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
            //string vUser = Request.QueryString["u"];
            //string vQuery = "GESTIONES_Generales 38, '" + vUser + "'";
            //DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            //if (vDatos.Rows.Count > 0)
            //{
            //    Session["USUARIO"] = vDatos.Rows[0]["codEmpleado"].ToString();
            //    Session["USUARIO_AD"] = vDatos.Rows[0]["adUser"].ToString();
            //    Session["GESTIONES_TEAMS_USER_LOGEADO"] = vDatos.Rows[0]["idTeams"].ToString();
            //    Session["GESTIONES_ID_JEFE_USER"] = vDatos.Rows[0]["idJefe"].ToString();
            //    Session["GESTIONES_ID_SUPLENTE_USER"] = vDatos.Rows[0]["idSuplente"].ToString();
            //}

            //Session["USUARIO"] = "2536";
            //Session["GESTIONES_TEAMS_USER"] = "14";
            //Session["GESTIONES_ID_JEFE_USER"] = "80123";
            //Session["GESTIONES_ID_SUPLENTE_USER"] = "2536";
            if (!Page.IsPostBack)
            {
                cargarDatos();
            }

        }

        private void cargarDatos()
        {
            try
            {
                string vQuery = "";
                DataTable vDatos = new DataTable();
                vQuery = "GESTIONES_Generales 32,'"+ Session["GESTIONES_TEAMS_USER_LOGEADO"].ToString()+"'";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["GESTIONES_COLABORADOR_TEAMS"] = vDatos;
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
                    vQuery = "GESTIONES_Generales 31,'" + Session["GESTIONES_TEAMS_USER"].ToString() + "','" + TxNombre.Text + "','" + TxCorreo.Text + "','" + TxColor.Text + "','" + TxUsuario.Text + "','" + TxCodigo.Text + "','" + Session["USUARIO"].ToString() + "',1,'"+ Session["GESTIONES_ID_JEFE_USER"].ToString()+"','"+ Session["GESTIONES_ID_SUPLENTE_USER"]+"'";
                    vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                    vMensaje = "Colaborador registrado con éxito";
                }
                else
                {
                    vQuery = "GESTIONES_Generales 34,'" + Session["GESTIONES_ID_EMPLEADO"].ToString() + "','" + DdlEstadoUsuario.SelectedValue + "','" + TxColor.Text + "','" + Session["USUARIO"].ToString() + "'";
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
                DataTable vDatos = new DataTable();
                String vQuery = "";
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
                    TxColor.Text = vDatos.Rows[0]["colorTarjeta"].ToString();
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
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

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
    
    }
}