using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban_GestionesTecnicas.classes;
using System.Data;

namespace Infatlan_Kanban_GestionesTecnicas
{
    public partial class main : System.Web.UI.MasterPage
    {
        db vConexionGestiones = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            //LitUsuario.Text = "ANA CAROLINA AMADOR MEJIA";

            try
            {
                //if (!Convert.ToBoolean(Session["AUTH"]))
                //    Response.Redirect("/login.aspx");

                if (!Page.IsPostBack)
                {
                    String vUser = Request.QueryString["u"];
                    if (string.IsNullOrEmpty(vUser))
                    {
                        DataTable vDatosLogueo = (DataTable)Session["AUTHCLASS"];
                        Session["AUTHCLASS"] = vDatosLogueo;
                        Session["USUARIO"] = vDatosLogueo.Rows[0]["codEmpleado"].ToString();
                        Session["USUARIO_AD"] = vDatosLogueo.Rows[0]["adUser"].ToString();
                        Session["GESTIONES_TEAMS_USER_LOGEADO"] = vDatosLogueo.Rows[0]["idTeams"].ToString();
                        Session["GESTIONES_ID_JEFE_USER"] = vDatosLogueo.Rows[0]["idJefe"].ToString();
                        Session["GESTIONES_ID_SUPLENTE_USER"] = vDatosLogueo.Rows[0]["idSuplente"].ToString();

                    }
                    else
                    {
                        String vQuery = "GESTIONES_Generales 38, '" + vUser + "'";
                        DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                        if (vDatos.Rows.Count > 0)
                        {
                            Session["AUTHCLASS"] = vDatos;
                            Session["USUARIO"] = vDatos.Rows[0]["codEmpleado"].ToString();
                            Session["USUARIO_AD"] = vDatos.Rows[0]["adUser"].ToString();
                            Session["GESTIONES_TEAMS_USER_LOGEADO"] = vDatos.Rows[0]["idTeams"].ToString();
                            Session["GESTIONES_ID_JEFE_USER"] = vDatos.Rows[0]["idJefe"].ToString();
                            Session["GESTIONES_ID_SUPLENTE_USER"] = vDatos.Rows[0]["idSuplente"].ToString();
                            LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString();


                        }
                    }



                       
                
                   

                }
            }
            catch (Exception ex)
            {
                String vError = ex.Message;
            }
        }
    }
}