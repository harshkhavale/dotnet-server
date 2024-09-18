using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SportsClubApi.Services
{
    public class MessageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IConfiguration configuration, ILogger<MessageService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            var host = smtpSettings.GetValue<string>("Host");
            var port = smtpSettings.GetValue<int>("Port");
            var username = smtpSettings.GetValue<string>("Username");
            var password = smtpSettings.GetValue<string>("Password");
            var from = smtpSettings.GetValue<string>("From");
            var ssl = smtpSettings.GetValue<bool>("EnableSsl");

            try
            {
                using (var smtpClient = new SmtpClient(host))
                {
                    smtpClient.Port = port;
                    smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
                    smtpClient.EnableSsl = ssl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(from),
                        Subject = subject,
                        Body = htmlContent,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toEmail);

                    await smtpClient.SendMailAsync(mailMessage);
                    _logger.LogInformation($"Email sent to {toEmail} with subject {subject}");
                }
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error sending email.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email.");
                throw;
            }
        }

        public async Task SendMobileOTP(string mobileNo, string otp)
        {
            var sbPostData = $"user=vikasmsg&key=367ef3765cXX&mobile={mobileNo}&message=Dear Customer, Your OTP is {otp} for IOSC, Please do not share this OTP. Regards&senderid=OTPSSS&accusage=1&entityid=1201159543060917386&tempid=1207161729866691748";

            try
            {
                string sendSMSUri = "http://sms.bulkssms.com/submitsms.jsp?";
                System.Net.HttpWebRequest httpWReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(sendSMSUri);
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData);

                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)httpWReq.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseString = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending mobile OTP.");
                throw;
            }
        }
    }
}
