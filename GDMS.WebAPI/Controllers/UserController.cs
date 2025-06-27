using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.WebAPI.Extensions;
using System.Web.Http.Cors;

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
	{
		IUserRepository<AdminEntity> _userRepositary;
		public UserController(IUserRepository<AdminEntity> obj)
		{
			_userRepositary = obj;
		}
		IRepository<AdminEntity> _objRepositary;
		public UserController(IRepository<AdminEntity> obj)
		{
			_objRepositary = obj;
		}
		[HttpPost]
		[Route("api/User/SignIn")]
		public HttpResponseMessage AdminLogin([FromBody]AdminEntity objAdmin)
		{
			try
			{
				AdminEntity Admin = _userRepositary.UserLogin(objAdmin.userid, SecurityExtensions.EncryptPassword(objAdmin.pwd));
				if (Admin != null)
				{
					//AdminEntity currentAdmin = Admin.Where(x => x.userid == objAdmin.userid
					//	 && x.pwd == SecurityExtensions.EncryptPassword(objAdmin.pwd)).SingleOrDefault();
					string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

					Admin.User_Token = token;
					string userToken = _userRepositary.UpdateUserToken(Admin);
					if (!string.IsNullOrEmpty(userToken))
					{
						//return Request.CreateResponse(HttpStatusCode.OK);
						return Request.CreateResponse(HttpStatusCode.OK, userToken);
					}
					else
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
					}


				}
				else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error");

			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpPost]
		[Route("api/User/ForgotPassword")]
		public HttpResponseMessage ForgotPassword([FromUri] string userId)
		{
			try
			{
				if (string.IsNullOrEmpty(userId))
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid User Id");
				}

				var Admin = _objRepositary.Get();
				if (Admin != null)
				{
					var curUser = Admin.Where(x => x.userid == userId).SingleOrDefault();
					if (curUser != null)
					{
						string newPassword = CreateRandomPassword();
						//AdminEntity objAdmin = new AdminEntity();
						//objAdmin.id = curUser.id;
						//objAdmin.userid = userId;
						//objAdmin.pwd = SecurityExtensions.EncryptPassword(newPassword);
						curUser.User_Token = null;
						curUser.pwd= SecurityExtensions.EncryptPassword(newPassword);
						string encryptedPwd = _objRepositary.Update(curUser);
						if (!string.IsNullOrEmpty(encryptedPwd))
						{
							SendPasswordEmail(curUser.email, newPassword);
							return Request.CreateResponse(HttpStatusCode.OK, "Password changed successfully!!!");
						}
						else
						{
							return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
						}

					}
					else
					{
						return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email not registered.");
					}
				}

				//SendEmail
				//int val = _adminRepositary.Add(Admin);
				return Request.CreateResponse(HttpStatusCode.OK, "");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
		private static string CreateRandomPassword(int length = 8)
		{
			// Create a string of characters, numbers, special characters that allowed in the password  
			string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
			Random random = new Random();

			// Select one random character at a time from the string  
			// and create an array of chars  
			char[] chars = new char[length];
			for (int i = 0; i < length; i++)
			{
				chars[i] = validChars[random.Next(0, validChars.Length)];
			}
			return new string(chars);
		}

		private void SendPasswordEmail(string email, string password)
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

				sb.Append("<tr><td align='left' colspan = '4'>Your new password is:  " + password + "</td></tr>");

				sb.Append("</br>");
				sb.Append("</br>");

				sb.Append("<tr><td align='left' colspan = '4'>Thanks & Regards,</td></tr>");
				sb.Append("<tr><td align='left' colspan = '4'>Kotak Team.</td></tr>");


				sb.Append("</table>");

				mail.Body = sb.ToString();//sbody.ToString();

				mail.IsBodyHtml = true;

				try
				{

					/*Local Settings*/
					/*
							SmtpClient client = new SmtpClient();
							client.DeliveryMethod = SmtpDeliveryMethod.Network;
							client.EnableSsl = true;
							client.Host = "smtp.gmail.com";//"relay-hosting.secureserver.net";
							client.Port = 587;//25;
					*/

					//Configure an SmtpClient to send the mail.            
					SmtpClient client = new SmtpClient();
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					client.EnableSsl = false;
					client.Host = "relay-hosting.secureserver.net";
					client.Port = 25;

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
}
