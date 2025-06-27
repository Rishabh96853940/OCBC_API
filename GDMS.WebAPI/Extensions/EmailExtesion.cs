using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.WebAPI.Extensions
{
	public static class EmailExtesion
	{
		public static void SendSubscribeEmail(string email, string message)
		{
			using (StringWriter sw = new StringWriter())
			{
				StringBuilder sb = new StringBuilder();
				MailMessage mail = new MailMessage();
				//mail.To.Add("skyriseit19@gmail.com");
				mail.To.Add(email);
				mail.From = new MailAddress("youritteam1@gmail.com", "Kotak");
				mail.Subject = "Your new password";
				sb.Append("<table style='min-width: 300px; max-width: 100%;padding-top:1em; padding-bottom:1em;'  cellspacing='0' cellpadding='2' >");
				sb.Append("<tr><td align='left' colspan = '4'>Dear User,</td></tr>");
				sb.Append("</br>");

				sb.Append("<tr><td align='left' colspan = '4'>Subscribe message is:  " + message + "</td></tr>");

				sb.Append("</br>");
				sb.Append("</br>");

				sb.Append("<tr><td align='left' colspan = '4'>Thanks & Regards,</td></tr>");
				sb.Append("<tr><td align='left' colspan = '4'>Kotak Team.</td></tr>");


				sb.Append("</table>");

				mail.Body = sb.ToString();//sbody.ToString();

				mail.IsBodyHtml = true;
				SendEmail(mail);

			}
		}
		public static void SendCommentEmail(string email, string message)
		{
			using (StringWriter sw = new StringWriter())
			{
				StringBuilder sb = new StringBuilder();
				MailMessage mail = new MailMessage();
				//mail.To.Add("skyriseit19@gmail.com");
				mail.To.Add(email);
				mail.From = new MailAddress("youritteam1@gmail.com", "Kotak");
				mail.Subject = "Your new password";
				sb.Append("<table style='min-width: 300px; max-width: 100%;padding-top:1em; padding-bottom:1em;'  cellspacing='0' cellpadding='2' >");
				sb.Append("<tr><td align='left' colspan = '4'>Dear User,</td></tr>");
				sb.Append("</br>");

				sb.Append("<tr><td align='left' colspan = '4'>Contact comment is :  " + message + "</td></tr>");

				sb.Append("</br>");
				sb.Append("</br>");

				sb.Append("<tr><td align='left' colspan = '4'>Thanks & Regards,</td></tr>");
				sb.Append("<tr><td align='left' colspan = '4'>Kotak Team.</td></tr>");


				sb.Append("</table>");

				mail.Body = sb.ToString();//sbody.ToString();

				mail.IsBodyHtml = true;
				SendEmail(mail);

			}
		}

		public static void SendEmailToClient(string email, string type)
		{
			using (StringWriter sw = new StringWriter())
			{
				StringBuilder sb = new StringBuilder();
				MailMessage mail = new MailMessage();
				//mail.To.Add("skyriseit19@gmail.com");
				mail.To.Add(email);
				mail.From = new MailAddress("youritteam1@gmail.com", "Kotak");
				mail.Subject = "Thank you for  "+ type;
				sb.Append("<table style='min-width: 300px; max-width: 100%;padding-top:1em; padding-bottom:1em;'  cellspacing='0' cellpadding='2' >");
				sb.Append("<tr><td align='left' colspan = '4'>Dear User,</td></tr>");
				sb.Append("</br>");

				sb.Append("<tr><td align='left' colspan = '4'>Thank you for joining us.</td></tr>");

				sb.Append("</br>");
				sb.Append("</br>");

				sb.Append("<tr><td align='left' colspan = '4'>Thanks & Regards,</td></tr>");
				sb.Append("<tr><td align='left' colspan = '4'>Kotak Team.</td></tr>");


				sb.Append("</table>");

				mail.Body = sb.ToString();//sbody.ToString();

				mail.IsBodyHtml = true;
				SendEmail(mail);

			}
		}
		private static void SendEmail(MailMessage mail)
		{
			try
			{

				/*Local Settings*/

				SmtpClient client = new SmtpClient();
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.EnableSsl = true;
				client.Host = "smtp.gmail.com";//"relay-hosting.secureserver.net";
				client.Port = 587;//25;

				/*
							//Configure an SmtpClient to send the mail.            
							SmtpClient client = new SmtpClient();
							client.DeliveryMethod = SmtpDeliveryMethod.Network;
							client.EnableSsl = false;
							client.Host = "relay-hosting.secureserver.net";
							client.Port = 25;
*/
							//Setup credentials to login to our sender email address ("UserName", "Password")
							NetworkCredential credentials = new NetworkCredential("youritteam1@gmail.com", "Yit123!@#");
							client.UseDefaultCredentials = true;
							client.Credentials = credentials;
							

				//Send the msg
				client.Send(mail);
			}
			catch (Exception ex)
			{
				//HttpContext.Current.Response.Write(ex.Message);
				throw ex.InnerException;
			}
		}
	}
}
