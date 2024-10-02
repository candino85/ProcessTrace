using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Servicios
{
    public class EmailSender
    {
        public static async Task NotificarContraseña(string user, string pass, string email)
        {
            string asunto = "Registro de usuario en ProcessTrace - UAI";
            string cuerpo = $"<p>Usted ha sido dado de alta en el sistema ProcessTrace, puede iniciar sesión utilizando las siguientes credenciales:</p>" +
                $"<b>Su usuario es:</b> {user} <br><b> Su contraseña es: </b>{pass}<br>" +
                $"Recuerde modificar su contraseña desde Sesion > Cambiar Clave.";

            await EnviarCorreo(email, asunto, cuerpo);
        }
        public static async Task EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                // Configura los detalles de autenticación de Gmail
                //string remitente = "andino.cristian@hotmail.com"; // Cambia esto por tu dirección de correo electrónico
                //string contraseña = "E7n4spRd=T"; // Cambia esto por tu contraseña

                string remitente = "candino@treeringws.com"; // Cambia esto por tu dirección de correo electrónico
                string contraseña = "tTS0RClD*k"; // Cambia esto por tu contraseña

                // Configura los detalles del correo electrónico
                MailMessage correo = new MailMessage(remitente, destinatario, asunto, cuerpo);
                correo.IsBodyHtml = true;

                // Configura el cliente SMTP
                SmtpClient clienteSmtp = new SmtpClient("smtp.office365.com", 587);
                clienteSmtp.EnableSsl= true;
                clienteSmtp.UseDefaultCredentials = false;
                clienteSmtp.Credentials = new NetworkCredential(remitente, contraseña);

                // Envía el correo electrónico
                await clienteSmtp.SendMailAsync(correo);
                clienteSmtp.Dispose();

                Console.WriteLine("Correo electrónico enviado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
            }
        }
    }
}
