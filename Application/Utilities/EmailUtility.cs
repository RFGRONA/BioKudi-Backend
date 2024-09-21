using System.Net.Mail;
using System.Net;
using Serilog;

namespace Biokudi_Backend.Application.Utilities
{
    public class EmailUtility 
    {
        private readonly SmtpClient client;
        private MailMessage email;
        private readonly string Host;
        private readonly int Port;
        private readonly string User;
        private readonly string Password;
        private readonly bool EnabledSSL = true;

        public EmailUtility(IConfiguration configuration)
        {
            Host = configuration["Smtp:Host"] ?? throw new ArgumentNullException(nameof(configuration), "Smtp:Host is null");
            Port = int.Parse(configuration["Smtp:Port"] ?? throw new ArgumentNullException(nameof(configuration), "Smtp:Port is null"));
            User = configuration["Smtp:User"] ?? throw new ArgumentNullException(nameof(configuration), "Smtp:User is null");
            Password = configuration["Smtp:Password"] ?? throw new ArgumentNullException(nameof(configuration), "Smtp:Password is null");

            client = new SmtpClient(Host, Port)
            {
                EnableSsl = EnabledSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(User, Password)
            };
        }

        public void SendEmail(string destiny, string affair, string message)
        {
            try
            {
                if(!EmailValidatorUtility.ValidateEmailAsync(destiny).Result) throw new Exception("Invalid email");
                email = new MailMessage(User, destiny, affair, message);
                email.IsBodyHtml = true;
                client.Send(email);
            }
            catch (Exception ex)
            {
                Log.Error("ERROR SENDING EMAIL: ", ex);
            }
        }

        public string CreateAccountAlert(string name)
        {
            string email = "<!DOCTYPE html><html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'><head><title></title><meta content='text/html; charset=utf-8' http-equiv='Content-Type'/><meta content='width=device-width, initial-scale=1.0' name='viewport'/><style>body {margin: 0;padding: 0;background-color: #ffffff;-webkit-text-size-adjust: none;text-size-adjust: none;text-align: center;color: #000000;font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;}a[x-apple-data-detectors] {color: inherit !important;text-decoration: inherit !important;}p {line-height: 1.6;color: #202020;text-align: center;}.button_block {text-align: center;}.alignment {display: inline-block;}.button_block a {margin-top: 10px;text-decoration: none;display: inline-block;color: #ffffff;background-color: #002027;border-radius: 15px;padding: 8px 20px;font-size: 16px;text-align: center;}h1 {color: #002027;margin-bottom: 20px;text-align: center;}.footer {color: #F67924;font-size: 14px;line-height: 1.6;text-align: center;padding: 10px;}.image-container {text-align: center;padding: 1em;background: #D8EECE;}.image-container img {display: inline-block;max-width: 100%;height: auto;}</style></head><body><table border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'><tr><td><table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'><tr><td><table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='680'><tr><td><div class='image-container'><img align='center' alt='company Logo' class='icon' height='auto' src='https://i.imgur.com/UOCk6pM.png' style='display: block; height: auto; margin: 0 auto; border: 0; min-width: 20em;' width='117'/></div><h1>Bienvenido " + name + "</h1><p>Se has registrado satisfactoriamente. </p><p>Esperamos pueda disfrutar de la página y de todo lo que tenemos para ofrecerte. </p><div></div><div class='footer'><em><strong>Este correo se genera automáticamente, por favor no lo responda.</strong></em><em><br>Si necesita contactarnos puedes hacerlo por medio de un ticket.</em></div></td></tr></table></td></tr></table></td></tr></table></body></html>";
            return email;
        }

        public string ticketEmail(int idticket)
        {
            string email = "<!DOCTYPE html><html lang='es' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'><head><title></title><meta content='text/html; charset=utf-8' http-equiv='Content-Type'/><meta content='width=device-width, initial-scale=1.0' name='viewport'/><style>body {margin: 0;padding: 0;background-color: #ffffff;-webkit-text-size-adjust: none;text-size-adjust: none;text-align: center;color: #000000;font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;}a[x-apple-data-detectors] {color: inherit !important;text-decoration: inherit !important;}p {line-height: 1.6;color: #202020;text-align: center;}.button_block {text-align: center;}.alignment {display: inline-block;}.button_block a {margin-top: 10px;text-decoration: none;display: inline-block;color: #ffffff;background-color: #002027;border-radius: 15px;padding: 8px 20px;font-size: 16px;text-align: center;}h1 {color: #002027;margin-bottom: 20px;text-align: center;}.footer {color: #476930;font-size: 14px;line-height: 1.6;text-align: center;padding: 10px;}.image-container {text-align: center;padding: 1em;background: #F5D4AC;}.image-container img {display: inline-block;max-width: 100%;height: auto;}</style></head><body><table border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'><tr><td><table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'><tr><td><table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='680'><tr><td><div class='image-container'><img align='center' alt='company Logo' class='icon' height='auto' src='https://i.imgur.com/UOCk6pM.png' style='display: block; height: auto; margin: 0 auto; border: 0; min-width: 20em;' width='117'/></div><h1>Generación de Ticket #" + idticket + "</h1><p>Su ticket ha sido enviado satisfactoriamente.</p><p>Pronto un administrador se pondrá al tanto de su caso y se comunicará por este medio.<br> Le pedimos esté pendiente de la bandeja de entrada o spam.</p><div></div><div class='footer'><em><strong>Este correo se genera automáticamente, por favor no lo responda.</strong></em></div></td></tr></table></td></tr></table></td></tr></table></body></html>";
            return email;

        }

        public string answerEmail(string name, string answer, int idticket)
        {
            string email = "<!DOCTYPE html><html lang='es' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'><head><title></title><meta content='text/html; charset=utf-8' http-equiv='Content-Type'/><meta content='width=device-width, initial-scale=1.0' name='viewport'/><style>body {margin: 0;padding: 0;background-color: #ffffff;-webkit-text-size-adjust: none;text-size-adjust: none;text-align: center;color: #000000;font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;}a[x-apple-data-detectors] {color: inherit !important;text-decoration: inherit !important;}p {line-height: 1.6;color: #202020;text-align: center;}.button_block {text-align: center;}.alignment {display: inline-block;}.button_block a {margin-top: 10px;text-decoration: none;display: inline-block;color: #ffffff;background-color: #002027;border-radius: 15px;padding: 8px 20px;font-size: 16px;text-align: center;}h1 {color: #002027;margin-bottom: 20px;text-align: center;}.footer {color: #476930;font-size: 14px;line-height: 1.6;text-align: center;padding: 10px;}.image-container {text-align: center;padding: 1em;background: #FEECD6;}.image-container img {display: inline-block;max-width: 100%;height: auto;}</style></head><body><table border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'><tr><td><table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'><tr><td><table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='680'><tr><td><div class='image-container'><img align='center' alt='company Logo' class='icon' height='auto' src='https://i.imgur.com/UOCk6pM.png' style='display: block; height: auto; margin: 0 auto; border: 0; min-width: 20em;' width='117'/></div><h1>Respuesta Ticket #" + idticket + "</h1><p>" + answer + "</p><p>Atentamente, " + name + "</p><div></div><div class='footer'><em><strong>Para seguimiento de su caso o requerir de más información, responda a este correo.</strong></em></div></td></tr></table></td></tr></table></td></tr></table></body></html>";
            return email;

        }
    }
}
