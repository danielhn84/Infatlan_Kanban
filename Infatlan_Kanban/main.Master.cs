using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using Infatlan_Kanban.classes;


namespace Infatlan_Kanban
{
    public partial class main : System.Web.UI.MasterPage
    {
        db vConexionGestiones = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.ToBoolean(Session["AUTH"]))
                    Response.Redirect("/login.aspx");
                

                if (!Page.IsPostBack)
                {
                    DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper();
                    Session["USUARIO"] = vDatos.Rows[0]["codEmpleado"].ToString();
                    Session["USUARIO_AD"] = vDatos.Rows[0]["adUser"].ToString();
                    Session["GESTIONES_TEAMS_USER_LOGEADO"] = vDatos.Rows[0]["idTeams"].ToString();
                    Session["GESTIONES_ID_JEFE_USER"] = vDatos.Rows[0]["idJefe"].ToString();
                    Session["GESTIONES_ID_SUPLENTE_USER"] = vDatos.Rows[0]["idSuplente"].ToString();

                    //TxUser.Value = vDatos.Rows[0]["codEmpleado"].ToString();
                    EmpleadosRol();

                }
            }
            catch (Exception ex)
            {
                String vError = ex.Message;
            }
        }


        void EmpleadosRol()
        {
            int vRol = 0;
            string vQuery2 = "GESTIONES_Generales 43,'" + Session["USUARIO_AD"].ToString() + "'";
            DataTable vDatos2 = vConexionGestiones.obtenerDataTableGestiones(vQuery2);
            foreach (DataRow item in vDatos2.Rows)
            {
                vRol = item["idRol"].ToString() == "" || item["idRol"].ToString() == null ? 0 : Convert.ToInt32(item["idRol"].ToString());
                Session["vRol"] = vRol;
            }
            if (vRol == 0)//SIN NINGUN ROL ASIGNADO
            {
                LiTablero.Visible = false;
                LiTarjeta.Visible = false;
                LiConfig.Visible = false;
                LiDashboard.Visible = false;
                LiUsuario.Visible = false;
                LiGestiones.Visible = false;
                LiEquipos.Visible = false;
            }
            else if (vRol == 1)//JEFE/SUPLENTE
            {
                LiDashboard.Visible = true;
                LiTablero.Visible = true;
                LiTarjeta.Visible = true;

                LiOperativas.Visible = false;
                LiEliminar.Visible = false;
                LiReasignar.Visible = false;
                LiDetener.Visible = false;

                LiConfig.Visible = true;
                LiUsuario.Visible = true;
                LiGestiones.Visible = false;
                LiEquipos.Visible = false;
            }
            else if (vRol == 2)//COLABORADOR
            {
                LiDashboard.Visible = true;
                LiTablero.Visible = true;
                LiTarjeta.Visible = true;

                LiOperativas.Visible = false;
                LiEliminar.Visible = false;
                LiReasignar.Visible = false;
                LiDetener.Visible = false;

                LiConfig.Visible = false;
                LiUsuario.Visible = false;
                LiGestiones.Visible = false;
                LiEquipos.Visible = false;
            }
            else if (vRol == 5)//ADMINISTRADOR
            {
                LiDashboard.Visible = true;
                LiTablero.Visible = true;
                LiTarjeta.Visible = true;
                LiConfig.Visible = true;
                LiUsuario.Visible = true;
                LiGestiones.Visible = true;
                LiEquipos.Visible = true;

                LiOperativas.Visible = false;
                LiEliminar.Visible = false;
                LiReasignar.Visible = false;
                LiDetener.Visible = false;

            }
            else if (vRol == 4)//REPORTE
            {
                LiDashboard.Visible = false;
                LiTablero.Visible = true;
                LiTarjeta.Visible = false;
                LiConfig.Visible = false;
                LiUsuario.Visible = false;
                LiGestiones.Visible = false;
                LiEquipos.Visible = false;
            }
        }
    }
}