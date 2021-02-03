using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Infatlan_Kanban.classes
{
    public enum WarningType { 
        Success,
        Info,
        Warning,
        Danger
    }
    
    public class db
    {
        SqlConnection vConexion;
        SqlConnection vConexionGestiones;
        public db()
        { 
            vConexion = new SqlConnection(ConfigurationManager.AppSettings["SqlServer"]);
            vConexionGestiones = new SqlConnection(ConfigurationManager.AppSettings["SQLServerGestiones"]);
        }

        public DataTable obtenerDataTable(String vQuery)
        {
            DataTable vDatos = new DataTable();
            try
            {
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexion);
                vDataAdapter.Fill(vDatos);
            }
            catch
            {
                throw;
            }
            return vDatos;
        }
        public int ejecutarSql(String vQuery)
        {
            int vResultado = 0;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexion);
                vSqlCommand.CommandType = CommandType.Text;

                vConexion.Open();
                vResultado = vSqlCommand.ExecuteNonQuery();
                vConexion.Close();
            }
            catch (Exception Ex)
            {
                String vError = Ex.Message;
                vConexion.Close();
                throw;
            }
            return vResultado;
        }
        public int ejecutarSQLGetValue(String vQuery)
        {
            int vResultado = 0;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexion);
                vSqlCommand.CommandType = CommandType.Text;

                vConexion.Open();
                vResultado = (Int32)vSqlCommand.ExecuteScalar();
                vConexion.Close();
            }
            catch (Exception Ex)
            {
                String vError = Ex.Message;
                vConexion.Close();
                throw;
            }
            return vResultado;
        }
        public String ejecutarSQLGetValueString(String vQuery)
        {
            String vResultado = String.Empty;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexion);
                vSqlCommand.CommandType = CommandType.Text;

                vConexion.Open();
                vResultado = (String)vSqlCommand.ExecuteScalar();
                vConexion.Close();
            }
            catch (Exception Ex)
            {
                String vError = Ex.Message;
                vConexion.Close();
                throw;
            }
            return vResultado;
        }
        public Boolean ejecutarSQLGetValueBoolean(String vQuery)
        {
            Boolean vResultado = false;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexion);
                vSqlCommand.CommandType = CommandType.Text;

                vConexion.Open();
                vResultado = (Boolean)vSqlCommand.ExecuteScalar();
                vConexion.Close();
            }
            catch (Exception Ex)
            {
                String vError = Ex.Message;
                vConexion.Close();
                throw;
            }
            return vResultado;
        }


        public DataTable obtenerDataTableGestiones(String vQuery)
        {
            DataTable vDatos = new DataTable();
            try
            {
                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, vConexionGestiones);
                vDataAdapter.Fill(vDatos);
            }
            catch
            {
                throw;
            }
            return vDatos;
        }
        public int ejecutarSqlGestiones(String vQuery)
        {
            int vResultado = 0;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexionGestiones);
                vSqlCommand.CommandType = CommandType.Text;

                vConexionGestiones.Open();
                vResultado = vSqlCommand.ExecuteNonQuery();
                vConexionGestiones.Close();
            }
            catch (Exception Ex)
            {
                String vError = Ex.Message;
                vConexionGestiones.Close();
                throw;
            }
            return vResultado;
        }
        public int ejecutarSQLGetValueGestiones(String vQuery)
        {
            int vResultado = 0;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexionGestiones);
                vSqlCommand.CommandType = CommandType.Text;

                vConexionGestiones.Open();
                vResultado = (Int32)vSqlCommand.ExecuteScalar();
                vConexionGestiones.Close();
            }
            catch (Exception Ex)
            {
                String vError = Ex.Message;
                vConexionGestiones.Close();
                throw;
            }
            return vResultado;
        }
        public Boolean ejecutarSQLGetValueBooleanGestiones(String vQuery)
        {
            Boolean vResultado = false;
            try
            {
                SqlCommand vSqlCommand = new SqlCommand(vQuery, vConexionGestiones);
                vSqlCommand.CommandType = CommandType.Text;

                vConexionGestiones.Open();
                vResultado = (Boolean)vSqlCommand.ExecuteScalar();
                vConexionGestiones.Close();
            }
            catch (Exception Ex)
            {
                String vError = Ex.Message;
                vConexionGestiones.Close();
                throw;
            }
            return vResultado;
        }
    }
}
