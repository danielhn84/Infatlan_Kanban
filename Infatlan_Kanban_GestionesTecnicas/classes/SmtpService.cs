using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.UI;


namespace Infatlan_Kanban_GestionesTecnicas.classes
{
    public enum typeBody{
        EnvioCorreo           
    }

    public class SmtpService : Page{

        public SmtpService() { }

        public Boolean EnviarMensaje(String To, typeBody Body, String Usuario, String Nombre, String vMessage = null){
            Boolean vRespuesta = false;
            try{
                MailMessage mail = new MailMessage("STYC<" + ConfigurationManager.AppSettings["SmtpFrom"] + ">", To);
                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = ConfigurationManager.AppSettings["SmtpServer"];
                mail.Subject = "Recursos Humanos - Información de empleado";
                mail.IsBodyHtml = true;

                switch (Body){                   
                     case typeBody.EnvioCorreo:
                        mail.AlternateViews.Add(CreateHtmlMessage(PopulateBody(
                            Usuario,
                            "Agencias:",
                            ConfigurationManager.AppSettings["Host"] + "/pages/configuraciones/creacionAgencia.aspx",
                            Nombre
                            ), Server.MapPath("/assets/images/logo.png")));
                        break;
                }
                client.Send(mail);
                vRespuesta = true;
            }catch (System.Net.Mail.SmtpException Ex){
                String vError = Ex.Message;
                throw;
            }catch (Exception Ex){
                throw;
            }
            return vRespuesta;
        }
        
        private AlternateView CreateHtmlMessage(string message, string logoPath){
            var inline = new LinkedResource(logoPath, "image/png");
            inline.ContentId = "companyLogo";

            var alternateView = AlternateView.CreateAlternateViewFromString(
                                    message,
                                    Encoding.UTF8,
                                    MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);

            return alternateView;
        }

        public string PopulateBody(string vNombre, string vTitulo, string vUrl, string vDescripcion){
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("/mail/TemplateMail.html"))){
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Host}", ConfigurationManager.AppSettings["Host"]);
            body = body.Replace("{Nombre}", vNombre);
            body = body.Replace("{Titulo}", vTitulo);
            body = body.Replace("{Url}", vUrl);
            body = body.Replace("{Descripcion}", vDescripcion);
            return body;
        }

        public string PopulateBodyES(string vNombre, string vDescripcion){
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("/mail/TemplateMail.html"))){
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Host}", ConfigurationManager.AppSettings["Host"]);
            body = body.Replace("{Nombre}", vNombre);
            body = body.Replace("{Titulo}", "");
            body = body.Replace("{Descripcion}", vDescripcion);
            return body;
        }


    }
}