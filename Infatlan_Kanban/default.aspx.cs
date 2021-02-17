using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infatlan_Kanban.classes;
using System.Data;
using System.IO;

namespace Infatlan_Kanban
{
    public partial class _default : System.Web.UI.Page
    {
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

                    //Session["GESTIONES_TAREAS_FECHA_INICIO"] = "01/12/2020";
                    //Session["GESTIONES_TAREAS_FECHA_FINAL"] = "27/01/2021";
                    obtenerEstados();
                    obtenerTipoGestion();
                    obtenerTareasCerradas();
                    obtenerCargabilidadApilado();
                    obtenerCargabilidad();
                    //UpGraficos.Update();
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }

        }

        public string obtenerCargabilidad()
        {
            string vQuery = "GESTIONES_Generales 9,'" + Session["USUARIO"].ToString() + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            var strDatos = "[['Fecha','Min','WIP'],";
            if (vDatos.Rows.Count > 0)
            {
                divGraficoCargabilidad.Visible = true;
                divImagenCragabilidad.Visible = false;
                foreach (DataRow item in vDatos.Rows)
                {
                    strDatos = strDatos + "[";
                    strDatos = strDatos + "'" + item["fecha"].ToString() + "'," + item["minutos"].ToString() + "," + item["wip"].ToString();
                    strDatos = strDatos + "],";
                }
            }
            else
            {
                divGraficoCargabilidad.Visible = false;
                divImagenCragabilidad.Visible = true;
            }
            strDatos = strDatos + "]";
            return strDatos;
        }

        public string obtenerCargabilidadApilado()
        {
            var strDatosConcat = "";
            string vQuery = "GESTIONES_Generales 18,'"+ Session["USUARIO"].ToString() + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);
            string encabezado = "";
            if (vDatos.Rows.Count > 0)
            {
                divGraficoApilado.Visible = true;
                divImagenApilado.Visible = false;

                foreach (DataRow item in vDatos.Rows)
                {
                    encabezado = encabezado + "'" + item["idSolicitud"].ToString() + "',";
                }
            }
            else
            {
                divImagenApilado.Visible = true;
                divGraficoApilado.Visible = false;
            }
            encabezado = encabezado + " { role: 'annotation' } ],";


            vQuery = "GESTIONES_Generales 19";
            DataTable vDatosFechas = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            var strDatos = "";
            if (vDatosFechas.Rows.Count > 0)
            {
                for (int i = 0; i < vDatosFechas.Rows.Count; i++)
                {
                    strDatos += "['" + vDatosFechas.Rows[i]["fecha"].ToString() + "',";
                    vQuery = "GESTIONES_Generales 20,'" + vDatosFechas.Rows[i]["fecha"].ToString() +"','"+ Session["USUARIO"].ToString() + "'"; ;
                    DataTable vDatosCuerpo = vConexionGestiones.obtenerDataTableGestiones(vQuery);
                    foreach (DataRow item in vDatosCuerpo.Rows)
                    {
                        if (vDatosFechas.Rows[i]["fecha"].ToString() == item["fecha"].ToString())
                        {
                            strDatos += item["min"].ToString() + ",";
                        }
                        else
                        {
                            break;
                        }

                    }
                    strDatos += "''],";
                }
            }
            strDatos = strDatos + "]";


            strDatosConcat += "[['Genre'," + encabezado + strDatos;
            return strDatosConcat;
        }

        public string obtenerEstados()
        {

            string vQuery = "GESTIONES_Generales 6,'" + Session["USUARIO"].ToString() + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            var strDatos = "[['Estado', 'Tareas'],";
            if (vDatos.Rows.Count > 0)
            {
                divGraficoEstados.Visible = true;
                divImagenEstados.Visible = false;
                foreach (DataRow item in vDatos.Rows)
                {
                    strDatos = strDatos + "[";
                    strDatos = strDatos + "'" + item["estado"].ToString() + "'," + item["tareas"].ToString();
                    strDatos = strDatos + "],";
                }
            }else
            {
                divImagenEstados.Visible = true;
                divGraficoEstados.Visible = false;
            }
            strDatos = strDatos + "]";
            return strDatos;
        }

        public string obtenerTipoGestion()
        {

            string vQuery = "GESTIONES_Generales 7,'" + Session["USUARIO"].ToString() + "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            var strDatos = "[['Gestión', 'Tareas'],";
            if (vDatos.Rows.Count > 0)
            {
                divGraficoGestiones.Visible = true;
                divImagenGestiones.Visible = false;
                foreach (DataRow item in vDatos.Rows)
                {
                    strDatos = strDatos + "[";
                    strDatos = strDatos + "'" + item["gestion"].ToString() + "'," + item["tareas"].ToString();
                    strDatos = strDatos + "],";
                }
            }
            else
            {
                divImagenGestiones.Visible = true;
                divGraficoGestiones.Visible = false;
            }
            strDatos = strDatos + "]";
            return strDatos;
        }


        public string obtenerTareasCerradas()
        {
            string vQuery = "GESTIONES_Generales 8,'" + Session["USUARIO"].ToString()+ "'";
            DataTable vDatos = vConexionGestiones.obtenerDataTableGestiones(vQuery);

            var strDatos = "[['Estado', 'Tareas'],";
            if (vDatos.Rows.Count > 0)
            {
                divGraficoEstadosCerrados.Visible = true;
                divImagenCerradas.Visible = false;
                foreach (DataRow item in vDatos.Rows)
                {
                    strDatos = strDatos + "[";
                    strDatos = strDatos + "'" + item["estado"].ToString() + "'," + item["tareas"].ToString();
                    strDatos = strDatos + "],";
                }
            }else
            {
                divGraficoEstadosCerrados.Visible = false;
                divImagenCerradas.Visible = true;
            }
            strDatos = strDatos + "]";
            return strDatos;
        }

        protected void TxFechaInicio_TextChanged(object sender, EventArgs e)
        {
            //if (TxFechaInicio.Text != "" && TxFechaEntrega.Text != "")
            //{
            //    string fecha_inicio = TxFechaInicio.Text.ToString();
            //    string fecha_fin = TxFechaEntrega.Text.ToString();

            //    String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
            //    String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
            //    String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

            //    Session["GESTIONES_TAREAS_FECHA_INICIO"] = vFecha1;
            //    Session["GESTIONES_TAREAS_FECHA_FINAL"] = vFecha2;

            //    obtenerEstados();
            //    obtenerTipoGestion();
            //    obtenerTareasCerradas();
            //}
        }

        protected void TxFechaEntrega_TextChanged(object sender, EventArgs e)
        {
            //if (TxFechaInicio.Text != "" && TxFechaEntrega.Text != "")
            //{
            //    string fecha_inicio = TxFechaInicio.Text.ToString();
            //    string fecha_fin = TxFechaEntrega.Text.ToString();

            //    String vFormato = "dd/MM/yyyy"; //"dd/MM/yyyy HH:mm:ss"
            //    String vFecha1 = Convert.ToDateTime(fecha_inicio).ToString(vFormato);
            //    String vFecha2 = Convert.ToDateTime(fecha_fin).ToString(vFormato);

            //    Session["GESTIONES_TAREAS_FECHA_INICIO"] = vFecha1;
            //    Session["GESTIONES_TAREAS_FECHA_FINAL"] = vFecha2;

            //    obtenerEstados();
            //    obtenerTipoGestion();
            //    obtenerTareasCerradas();
            //}

        }

        protected void BtnBusqueda_Click(object sender, EventArgs e)
        {
            DivBusqueda.Visible = true;
            UpdatePanel8.Update();
        }
    }
}