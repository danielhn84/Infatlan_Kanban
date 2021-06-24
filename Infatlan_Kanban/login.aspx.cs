using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Infatlan_Kanban.classes;

namespace Infatlan_Kanban
{
    public partial class login : System.Web.UI.Page
    {
        db vConexion = new db();
        db vConexionGestiones = new db();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LbHoraFecha.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                generales vGenerales = new generales();
                LdapService vLdap = new LdapService();
                Boolean vLogin = vLdap.ValidateCredentials("ADBancat.hn", TxUsername.Text, TxPassword.Text);
                //Boolean vLogin = true;

                if (vLogin)
                {
                    DataTable vDatos = new DataTable();
                    String vQuery = "GESTIONES_Login 1,'" + TxUsername.Text + "','" + vGenerales.MD5Hash(TxPassword.Text) + "'";
                    vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

                    if (vDatos.Rows.Count < 1)
                    {
                        Session["AUTH"] = false;
                        throw new Exception("Usuario o contraseña incorrecta.");
                    }

                    foreach (DataRow item in vDatos.Rows)
                    {
                        Session["AUTHCLASS"] = vDatos;
                        Session["USUARIO"] = item["codEmpleado"].ToString();
                        Session["ID_ROL_USUARIO"] = item["idRol"].ToString();
                        Session["AUTH"] = true;

                        Response.Redirect("/default.aspx");
                    }
                }
                else
                {
                    Session["AUTH"] = false;
                    throw new Exception("Usuario o contraseña incorrecta.");
                }
            }
            catch (Exception Ex)
            {
                LbMensaje.Text = "Usuario o contraseña incorrecta.";
                String vErrorLog = Ex.Message;
            }
        }
    }
}