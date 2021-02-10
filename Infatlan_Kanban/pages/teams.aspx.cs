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
    public partial class teams : System.Web.UI.Page
    {
        db vConexion = new db();
        db vConexionGestiones = new db();
        public void Mensaje(string vMensaje, WarningType type)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "text", "infatlan.showNotification('top','center','" + vMensaje + "','" + type.ToString().ToLower() + "')", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
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
        private void cargarDatos()
        {
            try
            {
                string vQuery = "";
                DataTable vDatos = new DataTable();
                vQuery = "GESTIONES_Generales 13";
                vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                if (vDatos.Rows.Count > 0)
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                    Session["GESTIONES_TEAMS"] = vDatos;
                }

                vQuery = "GESTIONES_General 6";
                vDatos = vConexion.obtenerDataTable(vQuery);
                DDLJefe.Items.Clear();
                DDLSuplente.Items.Clear();
                DDLJefe.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                DDLSuplente.Items.Add(new ListItem { Value = "0", Text = "Seleccione una opción" });
                foreach (DataRow item in vDatos.Rows)
                {
                    DDLJefe.Items.Add(new ListItem { Value = item["idEmpleado"].ToString(), Text = item["idEmpleado"].ToString() + " - " + item["nombre"].ToString() });
                    DDLSuplente.Items.Add(new ListItem { Value = item["idEmpleado"].ToString(), Text = item["idEmpleado"].ToString() + " - " + item["nombre"].ToString() });
                }
            } catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarModal();
                LbIdTeams.Text = "Crear Nuevo Equipo de Trabajo";
                DivEstado.Visible = false;
                Session["GESTIONES_ID"] = null;
                TxEstado.Text = "Activo";
                UpdatePanelModal.Update();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        void limpiarModal()
        {
            TxTeams.Text = string.Empty;
            TxWIP.Text = string.Empty;
            DDLJefe.SelectedIndex = -1;
            DDLSuplente.SelectedIndex = -1;
            TxHrInicio.Text = string.Empty;
            TxHrFin.Text = string.Empty;
            DivMensaje.Visible = false;
        }

        private void validarDatos()
        {
            if (TxTeams.Text == "" || TxTeams.Text == string.Empty)
                throw new Exception("Favor ingrese nombre del equipo de trabajo.");

            if (TxWIP.Text == "" || TxWIP.Text == string.Empty)
                throw new Exception("Favor ingrese el WIP correspondiente");

            if (TxHrInicio.Text == "" || TxHrInicio.Text == string.Empty)
                throw new Exception("Favor ingrese hora inicio");

            if (TxHrFin.Text == "" || TxHrFin.Text == string.Empty)
                throw new Exception("Favor ingrese hora fin");

            if (DDLJefe.SelectedValue == "0")
                throw new Exception("Falta que seleccione jefe del equipo de trabajo");
            
            if (DDLSuplente.SelectedValue == "0")
                throw new Exception("Falta que seleccione suplente del equipo de trabajo");

        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                validarDatos();
                String vMensaje = "";
                String vQuery = "";
                int vInfo;

                if (HttpContext.Current.Session["GESTIONES_ID"] == null)
                {
                    vQuery = "GESTIONES_Generales 14,'" + TxTeams.Text + "','" + TxWIP.Text + "','" + DDLJefe.SelectedValue + "','" + TxHrInicio.Text + "','" + TxHrFin.Text + "','" + Session["GESTIONES_CORREO_JEFE"].ToString() + "','" + Session["GESTIONES_NOMBRE_JEFE"].ToString() + "',1,'"+ DDLSuplente.SelectedValue+"','" + Session["GESTIONES_NOMBRE_SUPLENTE"].ToString()+"','"+ Session["GESTIONES_CORREO_SUPLENTE"].ToString()+"','"+ Session["USUARIO"].ToString()+"'";
                    DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    string idTeams = vDatos.Rows[0]["idTeams"].ToString();

                    vQuery = "GESTIONES_Generales 30,'" + idTeams + "','" + TxWIP.Text + "','" + Session["USUARIO"].ToString() + "'";
                    vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);

                    vMensaje = "Equipo de trabajo registrado con éxito";
                }
                else{
                    vQuery = "GESTIONES_Generales 16,'" + Session["GESTION_ID"].ToString() + "','" + TxTeams.Text + "','" + TxWIP.Text + "','" + DDLJefe.SelectedValue + "','" + TxHrInicio.Text + "','" + TxHrFin.Text + "','" + Session["GESTIONES_CORREO_JEFE"].ToString() + "','" + Session["GESTIONES_NOMBRE_JEFE"].ToString() + "','" + Session["USUARIO"].ToString() + "','"+ DDLSuplente.SelectedValue+"','"+ Session["GESTIONES_NOMBRE_SUPLENTE"].ToString()+"','" + Session["GESTIONES_CORREO_SUPLENTE"].ToString()+"','"+ DDLEstado.SelectedValue +"'";
                    vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                    vMensaje = "Equipo de trabajo actualizado con éxito";

                    vQuery = "GESTIONES_Generales 36,'" + Session["GESTION_ID"].ToString() + "','" + DDLJefe.SelectedValue + "','" + DDLSuplente.SelectedValue + "'";
                    int vInfoCambios = vConexionGestiones.ejecutarSqlGestiones(vQuery);


                    if (Session["GESTIONES_WIP_ACTUAL"].ToString() != TxWIP.Text)
                    {
                        vQuery = "GESTIONES_Generales 30,'" + Session["GESTION_ID"].ToString() + "','" + TxWIP.Text + "','" + Session["USUARIO"].ToString() + "'";
                        vInfo = vConexionGestiones.ejecutarSqlGestiones(vQuery);
                    }
                }

                if (vInfo == 1)
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
                UpdatePanelModal.Update();
            }
        }

        protected void DDLJefe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string vQuery = "";
                DataTable vDatos = new DataTable();
                vQuery = "GESTIONES_General 3,'" + DDLJefe.SelectedValue + "'";
                vDatos = vConexion.obtenerDataTable(vQuery);
                Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["emailEmpresa"].ToString();
                Session["GESTIONES_NOMBRE_JEFE"] = vDatos.Rows[0]["nombre"].ToString();
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
                string vIdTeams = e.CommandArgument.ToString();
                Session["GESTIONES_ID"] = vIdTeams;
                if (e.CommandName == "EditarTeams")
                {
                    DivMensaje.Visible = false;
                    LbIdTeams.Text = "Editar Equipo de Trabajo: " + vIdTeams;
                    Session["GESTION_ID"] = vIdTeams;
                    DivEstado.Visible = true;
                    vQuery = "[GESTIONES_Generales] 15," + vIdTeams + "";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    TxTeams.Text = vDatos.Rows[0]["nombre"].ToString();
                    TxWIP.Text = vDatos.Rows[0]["wip"].ToString();
                    DDLJefe.SelectedValue = vDatos.Rows[0]["idJefe"].ToString();
                    DDLSuplente.SelectedValue = vDatos.Rows[0]["idSuplente"].ToString();
                    TxHrInicio.Text = vDatos.Rows[0]["hrInicio"].ToString();
                    TxHrFin.Text = vDatos.Rows[0]["hrFin"].ToString();
                    Session["GESTIONES_CORREO_JEFE"] = vDatos.Rows[0]["correoJefe"].ToString();
                    Session["GESTIONES_NOMBRE_JEFE"] = vDatos.Rows[0]["nombreJefe"].ToString();
                    Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["correoSuplente"].ToString();
                    Session["GESTIONES_NOMBRE_SUPLENTE"] = vDatos.Rows[0]["nombreSuplente"].ToString();
                    Session["GESTIONES_WIP_ACTUAL"] = vDatos.Rows[0]["wip"].ToString();
                    TxEstado.Text = "Activo";
                    UpdatePanelModal.Update();


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLSuplente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string vQuery = "";
                DataTable vDatos = new DataTable();
                vQuery = "GESTIONES_General 3,'" + DDLSuplente.SelectedValue + "'";
                vDatos = vConexion.obtenerDataTable(vQuery);
                Session["GESTIONES_CORREO_SUPLENTE"] = vDatos.Rows[0]["emailEmpresa"].ToString();
                Session["GESTIONES_NOMBRE_SUPLENTE"] = vDatos.Rows[0]["nombre"].ToString();
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }

        protected void DDLEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxEstado.Text = "Activo";
            if (DDLEstado.SelectedValue.Equals("1"))
            {
                TxEstado.Text = "Activo";
            }
            else
            {
                TxEstado.Text = "Inactivo";
            }
            UpdatePanelModal.Update();
        }

        protected void GVBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GVBusqueda.PageIndex = e.NewPageIndex;
                GVBusqueda.DataSource = (DataTable)Session["GESTIONES_TEAMS"];
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
                DataTable vDatos = (DataTable)Session["GESTIONES_TEAMS"];
                if (vBusqueda.Equals(""))
                {
                    GVBusqueda.DataSource = vDatos;
                    GVBusqueda.DataBind();
                }else
                {
                    EnumerableRowCollection<DataRow> filtered = vDatos.AsEnumerable()
                        .Where(r => r.Field<String>("nombre").Contains(vBusqueda.ToUpper()));

                    Boolean isNumeric = int.TryParse(vBusqueda, out int n);

                    if (isNumeric)
                    {
                        if (filtered.Count() == 0)
                        {
                            filtered = vDatos.AsEnumerable().Where(r =>
                                Convert.ToInt32(r["idTeams"]) == Convert.ToInt32(vBusqueda));
                        }
                    }

                    DataTable vDatosFiltrados = new DataTable();
                    vDatosFiltrados.Columns.Add("idTeams");
                    vDatosFiltrados.Columns.Add("nombre");
                    vDatosFiltrados.Columns.Add("wip");
                    vDatosFiltrados.Columns.Add("nombreJefe");
                    vDatosFiltrados.Columns.Add("hrInicio");
                    vDatosFiltrados.Columns.Add("hrFin");

                    foreach (DataRow item in filtered)
                    {
                        vDatosFiltrados.Rows.Add(
                            item["idTeams"].ToString(),
                            item["nombre"].ToString(),
                            item["wip"].ToString(),
                            item["nombreJefe"].ToString(),
                            item["hrInicio"].ToString(),
                            item["hrFin"].ToString()
                            );
                    }

                    GVBusqueda.DataSource = vDatosFiltrados;
                    GVBusqueda.DataBind();
                    Session["GESTIONES_TEAMS"] = vDatosFiltrados;
                }
            }
            catch (Exception ex)
            {
                Mensaje(ex.Message, WarningType.Danger);
            }
        }
    }
}
