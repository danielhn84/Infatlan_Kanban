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
                //if (!Convert.ToBoolean(Session["AUTH"]))
                //    Response.Redirect("/login.aspx");

                if (!Page.IsPostBack)
                {
                    DataTable vDatos = (DataTable)Session["AUTHCLASS"];
                    LitUsuario.Text = vDatos.Rows[0]["nombre"].ToString().ToUpper();
                    Session["USUARIO"] = vDatos.Rows[0]["codEmpleado"].ToString();
                    Session["USUARIO_AD"] = vDatos.Rows[0]["adUser"].ToString();
                    Session["GESTIONES_TEAMS_USER_LOGEADO"] = vDatos.Rows[0]["idTeams"].ToString();
                    Session["GESTIONES_ID_JEFE_USER"] = vDatos.Rows[0]["idJefe"].ToString();
                    Session["GESTIONES_ID_SUPLENTE_USER"] = vDatos.Rows[0]["idSuplente"].ToString();


                    TxUser.Value = vDatos.Rows[0]["codEmpleado"].ToString();

                }
            }
            catch (Exception ex)
            {
                String vError = ex.Message;
            }
        }
    }
}