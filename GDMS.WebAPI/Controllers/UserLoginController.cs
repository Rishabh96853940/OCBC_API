using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.WebAPI.Extensions;
using System.Web.Http.Cors;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using Ionic.Zip;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kotak.WebAPI.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class UserLoginController : ApiController
	{
		IUser<UserLoginEntity> objRepositary;
		public UserLoginController(IUser<UserLoginEntity> obj)
		{
			objRepositary = obj;
		}

		//[HttpGet]
		//[Route("api/UserLogin/GetList")]
		//public HttpResponseMessage GetList([FromUri] string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData = objRepositary.Get();
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}


		//[HttpGet]
		//[Route("api/UserLogin/GetDetails")]
		//public HttpResponseMessage GetDetails([FromUri] int ID, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData = objRepositary.Get(ID);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}

		[HttpPost]
		[Route("api/UserLogin/Create")]
		public HttpResponseMessage Post([FromBody] UserLoginEntity objEntity)
		{
			try
			{
				//string val = objRepositary.UserLogin(objEntity);

				var objData = objRepositary.UserLogin(objEntity);
				if (objData != null)
					return Request.CreateResponse(HttpStatusCode.OK, objData);
				else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;

				//   return Request.CreateResponse(HttpStatusCode.OK, val);


			}
			catch (Exception ex)
			{
				LogError(ex, "msg");
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		private void LogError(Exception ex, string msg)
		{
			var TempPath = (ConfigurationManager.AppSettings["temp"].ToString());
			string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			message += string.Format("Message: {0}", ex.Message);
			message += Environment.NewLine;
			message += string.Format("StackTrac: {0}", msg);
			message += Environment.NewLine;
			message += string.Format("Source: {0}", ex.Source);
			message += Environment.NewLine;
			message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			string path = Path.Combine(TempPath, "ErrorLog.txt");
			using (StreamWriter writer = new StreamWriter(path, true))
			{
				writer.WriteLine(message);
				writer.Close();
			}
		}

		[HttpPost]
		[Route("api/UserLogin/Forgotpassword")]
		public HttpResponseMessage Forgotpassword([FromBody] UserLoginEntity objEntity)
		{
			try
			{
				//string val = objRepositary.UserLogin(objEntity);

				var objData = objRepositary.Forgotpassword(objEntity);
				if (objData.Count() > 0)
				{
					//string Email = objData[]

					foreach (var file in objData)
					{
						SendForgotpasswordEmail(objEntity.username, file.pwd, file.name);
						break;
					}									

					return Request.CreateResponse(HttpStatusCode.OK, "Password send on mail please check.");
				}
				else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;

				//   return Request.CreateResponse(HttpStatusCode.OK, val);


			}
			catch (Exception ex)
			{
                LogError(ex, "msg");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpPost]
		[Route("api/UserLogin/sendmailtocustomer")]
		public HttpResponseMessage sendmailtocustomer([FromBody] UserLoginEntity objEntity)
		{
			try
			{
				//string val = objRepositary.UserLogin(objEntity);

				var objData = objRepositary.Sendmailtocustomer(objEntity);
				if (objData.Count() > 0)
				{
					//string Email = objData[]

					foreach (var file in objData)
					{
						SendEmailTocustomer(file.email,file.pwd ,file.name,file.remarks);

						//SendEmailTocustomer(file.email, file.userid, file.name);
						var Status = objRepositary.UpdateMailStatus(file.email);


						//break;
					}

					return Request.CreateResponse(HttpStatusCode.OK, "Password send on mail please check.");
				}
				else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;

				//   return Request.CreateResponse(HttpStatusCode.OK, val);


			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		//[HttpPost]
		//[Route("api/UserLogin/Update")]
		//public HttpResponseMessage Update([FromBody]UserLoginEntity objEntity)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
		//		{

		//			if (!string.IsNullOrEmpty(objRepositary.Update(objEntity)))
		//			{
		//				return Request.CreateResponse(HttpStatusCode.OK, "Document is updated successfully!!");
		//			}
		//			else
		//			{
		//				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//			}
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}

		//      [HttpPost]
		//[Route("api/UserLogin/Delete")]
		//public HttpResponseMessage Delete([FromBody] UserLoginEntity objEntity)
		//{
		//	var response = new HttpResponseMessage();
		//	if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
		//	{
		//		if (objRepositary.Delete(Convert.ToInt32(objEntity.id)))
		//		{
		//			response = Request.CreateResponse(HttpStatusCode.OK, "Succsessfuly Deleted!!!");
		//		}
		//		else
		//		{
		//			response = Request.CreateResponse(HttpStatusCode.BadRequest, "Delete Failed!!!");
		//		}
		//	}
		//	else
		//	{
		//		response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid token!!!");
		//	}
		//	return response;
		//}


		[HttpPost]
		[Route("api/UserLogin/Changepassword")]
		public HttpResponseMessage Changepassword([FromBody] UserLoginEntity objEntity)
		{
			var response = new HttpResponseMessage();
			if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
			{

				var objdata = objRepositary.Changepassword(objEntity.CreatedBy, objEntity.pwd, objEntity.currentpwd);

				response = Request.CreateResponse(HttpStatusCode.OK, objdata);
				return response;
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid token!!!");
			}
			return response;
		}


		[HttpGet]
		[Route("api/UserLogin/Logout")]
		public HttpResponseMessage Logout([FromUri]  int UserID , string user_Token)
		{
			var response = new HttpResponseMessage();
			if (ValidateTokenExtension.ValidateToken(user_Token))
			{

				var objdata = objRepositary.Logout(UserID);

				response = Request.CreateResponse(HttpStatusCode.OK, objdata);
				return response;
			}
			else
			{
				response = Request.CreateResponse(HttpStatusCode.OK, "Invalid token!!!");
			}
			return response;
		}

		public void SendForgotpasswordEmail(string UserName,string Newpassword,string UName)
		{

			//var strMaildDetails = objRepositary.GetBulkFile(FileNo, ToEmailID);
			//DataTable dt = getData("EXEC SP_GetMailListByFileNo '" + FileNo + "'");

			var mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
			mailMessage.To.Add(new MailAddress(UserName));

			mailMessage.Subject = "Reset password";
			mailMessage.IsBodyHtml = true;
			//string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();
			body.AppendFormat("Dear {0}\n", UName +",");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendFormat("Your request for password reset is processed and here is your new password.");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			//body.AppendFormat("Password :", Newpassword);
			body.AppendFormat("Password  :{0}\n", Newpassword);
			body.AppendLine("<br/>");
			body.AppendFormat("Webapplication  :{0}\n", "https://avansedart.crownims.com");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendFormat("Password change request date & time:{0}\n", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
			body.AppendLine("<br/>");
			
			body.AppendFormat("If you have not requested a password reset, kindly report immediately to {0}\n", "Support team");
		//	body.AppendLine(@"If you have not requested a password reset, kindly report immediately to");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine(@"Thanks you!");
			body.AppendLine("<br/>");

			//Table end.
			body.Append("</table>");

			body.AppendLine("<br/>");


			//body.AppendLine("<a href=" + URL + " >Click Here to download file </a>");
			body.AppendLine("Regards,");
			body.AppendLine("<br/>");
			//body.AppendLine("Jana Bank Team");
			mailMessage.Body = body.ToString();


			SmtpClient smtp = new SmtpClient();
			smtp.Host = ConfigurationManager.AppSettings["Host"];
			smtp.EnableSsl = false;// Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
			System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
			NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
			NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = NetworkCred;
            smtp.EnableSsl = true;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
			smtp.Send(mailMessage);
		}

		public void SendEmailTocustomer(string EmailID, string Newpassword, string UName, string CCMail)
		{


            //string ccMail = "rajeshchonde@gmail.com,rajeshchonde2006@gmail.com";


        //   string[] CCId = CCMail.Split(',');
            

            //var strMaildDetails = objRepositary.GetBulkFile(FileNo, ToEmailID);
            //DataTable dt = getData("EXEC SP_GetMailListByFileNo '" + FileNo + "'");

            var mailMessage = new MailMessage();
			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
			mailMessage.To.Add(new MailAddress(EmailID));
            //mailMessage.CC.Add(new MailAddress(ccMail));

            //foreach (string CCEmail in CCId)
            //{
            //    if (CCEmail!="")
            //    {
            //        string strcc = CCEmail.Trim().Replace("\n", "");
            //    mailMessage.CC.Add(new MailAddress(strcc.Trim())); //Adding Multiple CC email Id  
            //    }
            //}

            //mailMessage.CC.Add(new MailAddress("nkumar@crownww.com"));

            mailMessage.Bcc.Add(new MailAddress("hnikam@crownww.com"));
         //   mailMessage.Bcc.Add(new MailAddress("rajeshchonde@gmail.com"));

            mailMessage.Subject = "Crown e-Storage System Upgrade | 2021-1";
			mailMessage.IsBodyHtml = true;
			//string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();

            //string strmsg = ""

            //body.Append("");

            //body.Append("<table cellpadding='0' cellspacing='0' class='es - content' align='center'>");
            //body.Append("<tbody>");
            //body.Append("<tr>");
            //body.Append("<td class='esd - stripe' align='center'>");
            //body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");

            //body.Append("<tbody>");
            //body.Append("<tr>");
            //body.Append("<td class='es - p20t es - p20r es - p20l esd - structure' align='left'");

            //body.Append("<table cellpadding='0' cellspacing='0' width='100 %'>");
            //body.Append("<tbody>");
            //body.Append("<tr>");
            //body.Append("<td align='left' class='esd - block - text'");
            //body.Append("<p style='font - size: 18px; color: #000000; font - family: 'Calibri Light','Helvetica Light',sans - serif; margin - bottom:0;> Dear e-Storage user </p>");

            //body.Append("<p style='font - size: 18px; color: #000000;font-family: 'Calibri Light','Helvetica Light',sans-serif; margin:0;>Greetings from Crown Records Management.</p>");

            //body.Append("<p style='color: #000000;font - family: 'Calibri Light', 'Helvetica Light', sans - serif; margin: 0;><span style='font - size:18px;'>We hope you’re safe and taking good care of yourself, and your family </span>in this pandemic.&nbsp;</p>");

            //body.Append("</td>");

            //body.Append(" </tr>");
            //body.Append(" </tbody>");

            //body.Append("</table>");
            //body.Append(" </td>");


            //body.Append(" </tr>");
            //body.Append(" </tbody>");

            //body.Append("</table>");
            //body.Append(" </td>");

            //body.Append(" </tr>");
            //body.Append(" </tbody>");

            //body.Append("</table>");
            //body.Append(" </td>");

            //body.Append(" </tr>");
            //body.Append(" </tbody>");

            //body.Append("</table>");

            //body.Append("<table cellpadding='0' cellspacing='0' class='es - content' align='center'>");
            //body.Append("<tbody>");
            //body.Append(" <tr>");

            //body.Append("<table cellpadding='0' cellspacing='0' class='es-content esd-header-popover' align='center'>");

            //				body.Append("<tbody>");

            //body.Append("<tr>");

            //						body.Append("<td class='esd-stripe' align='center'>");

            //body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");

            //								body.Append("<tbody>");

            //									body.Append("<tr>");

            //										body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");

            //											body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");

            //												body.Append("<tbody>");

            //													body.Append("<tr>");

            //														body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");

            //															body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");

            //																body.Append("<tbody>");

            //																	body.Append("<tr>");


            //						body.Append("<td align='left' class='esd-block-text'>");

            //						body.Append("<p style='color: #757b80; font-size: 12px; font-family: verdana, geneva, sans-serif;'><b>Subject line: Important | Crown e-Storage System Upgrade | 2021-1&nbsp;</b></p>");

            //															body.Append("</td>");

            //						body.Append("</tr>");

            //																body.Append("</tbody>");

            //															body.Append("</table>");

            //														body.Append("</td>");

            //													body.Append("</tr>");

            //												body.Append("</tbody>");

            //											body.Append("</table>");

            //										body.Append("</td>");

            //body.Append("</tr>");

            //								body.Append("</tbody>");

            //							body.Append("</table>");

            //						body.Append("</td>");

            //					body.Append("</tr>");

            //				body.Append("</tbody>");

            //			body.Append("</table>");





 //           < !DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
 //< html xmlns = "http://www.w3.org/1999/xhtml" xmlns: o = "urn:schemas-microsoft-com:office:office" >
     

 //    < head >
     
 //        < meta charset = "UTF-8" >
      
 //         < meta content = "width=device-width, initial-scale=1" name = "viewport" >
         
 //            < meta name = "x-apple-disable-message-reformatting" >
          
 //             < meta http - equiv = "X-UA-Compatible" content = "IE=edge" >
               
 //                  < meta content = "telephone=no" name = "format-detection" >
                  
 //                     < title ></ title >
 //                 </ head >
                  

 //                 < body >
                      body.Append("<div class='es-wrapper-color'>");
            body.Append("<table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-email-paddings' valign='top'>");
            //body.Append("<table cellpadding='0' cellspacing='0' class='es-content esd-header-popover' align='center'>");
            //body.Append("<tbody>");
            //body.Append("<tr>");
            //body.Append("<td class='esd-stripe' align='center'>");
            //body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            //body.Append("<tbody>");
            //body.Append("<tr>");
            //body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            //body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            //body.Append("<tbody>");
            //body.Append("<tr>");
            //body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            //body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            //body.Append("<tbody>");
            //body.Append("<tr>");

            //body.Append("<td align='left' class='esd-block-text'>");
            //body.Append("<p style='color: #757b80; font-size: 12px; font-family: verdana, geneva, sans-serif;'><b>Subject line: Important | Crown e-Storage System Upgrade | 2021-1&nbsp;</b></p>");
            //body.Append("</td>");
            //body.Append("</tr>");
            //body.Append("</tbody>");
            //body.Append("</table>");
            //body.Append("</td>");
            //body.Append("</tr>");
            //body.Append("</tbody>");
            //body.Append("</table>");
            //body.Append("</td>");
            //body.Append("</tr>");
            //body.Append("</tbody>");
            //body.Append("</table>");
            //body.Append("</td>");
            //body.Append("</tr>");
            //body.Append("</tbody>");
            //body.Append("</table>");

            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text'>");
            body.Append("<p style='font-size: 18px; color: #000000; font-family: 'Calibri Light','Helvetica Light',sans-serif; margin-bottom:0;'>Dear "+ UName +",&nbsp;</p>");
            body.Append("<p style='font-size: 18px; color: #000000;font-family: 'Calibri Light','Helvetica Light',sans-serif; margin:0;'>Greetings from Crown Records Management.</p>");
            body.Append("<p style='color: #000000;font-family: 'Calibri Light','Helvetica Light',sans-serif; margin:0;'><span style ='font-size:18px;'>We hope you’re safe and taking good care of yourself, and your family </span>in this pandemic.&nbsp;</p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr class='es-visible-simple-html-only es-mobile-hidden'>");
            body.Append("<td align='left' class='esd-block-text'>");
            body.Append("<p style='font-family: 'Calibri Light','Helvetica Light',sans-serif;font-size:18px;'>We thank you for the patronage towards Crown’s Digital Document Management Services “e-Storage”.&nbsp;I am writing to you to inform you about the system upgrade that will be rolled out effective from 1st of July 2021.&nbsp;The following part of this email highlights the key changes, their impact and timelines.&nbsp;</p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text' style='font-size: 17.79px; font - family: 'Calibri Light', 'Helvetica Light', sans - serif; '><b>Key Changes</b><br></td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("</tbody>");
            body.Append("</tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("</tbody>");
            body.Append("</tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("</tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("</tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text' esd-links-color='#15c'>");
            body.Append("<p style='font-family: 'Calibri Light','Helvetica Light',sans-serif;font-size: 18px;'>Starting 1st July 2021, The URL to access e-Storage will be&nbsp;<i><a href='https://e-storage.crownims.com/' target='_blank' style='color: #1155cc;'>https://e-storage.crownims.com</a></i></p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("</tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("</tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("</tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text' esd-links-color='#15c'>");
            body.Append("<p style='font-family: 'Calibri Light','Helvetica Light',sans-serif;font-size: 18px;margin:0;'>Your user ID to log in will remain the same as <a href='#' target='_blank' style='color: #1155cc;'><b>"+ EmailID + "</b></a></p>");
            body.Append("<p style='font-family: 'Calibri Light','Helvetica Light',sans-serif;font-size: 18px;margin:0 0 10px;'>You can create new password from here: <b> <i><a href=' https://e-storage.crownims.com/SOD/#/forgot-password' target='_blank' style='color: #1155cc;'> https://e-storage.crownims.com/SOD/#/forgot-password</a></i> </b></p>");
            body.Append("<p style='font-size: 12px;margin:0;font-family: 'Calibri Light','Helvetica Light',sans-serif;'><i>*We recommend you to change the password using the change password option after your first logon</i></p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text'>");
            body.Append("<p style='font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>All key functions of e-Storage ie., store, search, index, upload, download and share will remain the same in the new system, with the following upgrades,&nbsp;<br></p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>"); body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center' esdev-config='h1'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text'>");
            body.Append("<ul>");
            body.Append("<li style='line-height: 120%;font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>Improved email sharing functionality with unique links for each document shared via emails to non-e-Storage users</li>");
            body.Append("<li style='line-height: 120%;font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>Improved GUIX for searching, and viewing the desired documents</li>");
            body.Append("<li style='line-height: 120%;font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>Simplified search functionality for faster access of desired documents</li>");
            body.Append("<li style='line-height: 120%;font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>Allowed file formats in the new system are pdf, tiff, jpeg</li>");
            body.Append("<li style='line-height: 120%;font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>Access to reports & logs</li>");
            body.Append("<li style='line-height: 120%;font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>Optimised for mobile and tablet viewing</li>");

            body.Append("</ul>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text'>");
            body.Append("<p style='font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'><b>Impact & Timelines</b><br></p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            
                body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text' esd-links-color='#15c'>");
            body.Append("<p style='font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>The documents digitised and available in the current system will be migrated to the new system on or&nbsp;<i>before 25th of July.</i>&nbsp;We will intimate you in writing once the entire data is migrated, till the time the data is migrated entirely to the new system,&nbsp;<i>you will continue to have access to the existing system using the same credentials and URL:&nbsp;<a href='https://estorage.crownims.com/' target='_blank' style='color: #1155cc;'>https://estorage.crownims.com</a></i><i>&nbsp;</i><br></p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text' esd-links-color='#15c'>");
            body.Append("<p style='font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;'>Scan-on-Demand request received after 30th Jul will be made available on the new e-Storage (<a href='https://e-storage.crownims.com/' target='_blank' style='color: #1155cc;'>https://e-storage.crownims.com</a>)</p>");
            body.Append("<p style='display: none;'>&nbsp;</p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text'>");
            body.Append("<p style='font-size: 18px; font - family: 'Calibri Light', 'Helvetica Light', sans - serif;'>We will appreciate your cooperation during this period of migration and if you may face any challenge, you can reach out to our dedicated customer support created for rapid response during this change.&nbsp;</p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text' esd-links-color='#15c'>");
            body.Append("<p style='font-size: 18px; font - family: 'Calibri Light', 'Helvetica Light', sans - serif; margin: 0;'>Contacts:</p>");
    body.Append("<p style='font-size: 18px; font - family: 'Calibri Light', 'Helvetica Light', sans - serif; margin: 0;'>Himanshu Nikam&nbsp;Email:&nbsp;<a href='mailto: hnikam @crownww.com' target='_blank' style='color: #1155cc;'>hnikam@crownww.com</a>,&nbsp;Phone: +91 9967006046</p>");
  //  body.Append("<p style='font-size: 18px;font - family: 'Calibri Light', 'Helvetica Light', sans - serif; margin: 0;'>Dineshkumar MP Email:&nbsp;<a href='mailto: dkumarmp @crownww.com' target='_blank' style='color: #1155cc;'>dkumarmp@crownww.com</a>, Phone:&nbsp; +91 9967293338</p>");
          body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text'>");
            body.Append("<p style='font-size: 18px; font - family: 'Calibri Light', 'Helvetica Light', sans - serif;'>For more questions you can reach out to your account manager.&nbsp;</p>");
    body.Append("<p style='display: none;'>&nbsp;</p>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("<table cellpadding='0' cellspacing='0' class='es-content esd-footer-popover' align='center'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='esd-stripe' align='center'>");
            body.Append("<table bgcolor='#ffffff' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='900'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td class='es-p20t es-p20r es-p20l esd-structure' align='left'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td width='860' class='esd-container-frame' align='center' valign='top'>");
            body.Append("<table cellpadding='0' cellspacing='0' width='100%'>");
            body.Append("<tbody>");
            body.Append("<tr>");
            body.Append("<td align='left' class='esd-block-text' esd-links-color='#15c'>");
            body.Append("<p style='font-size: 18px; font-family: 'Calibri Light','Helvetica Light',sans-serif;margin:0;'>Thank you&nbsp;</p>");
            body.Append("<p style='font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;margin:0;'>Crown Records Management Team&nbsp;</p>");
            body.Append("<p style='font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;margin:0;'> South Asia</p>");           
            body.Append("<p style='font-size: 18px;font-family: 'Calibri Light','Helvetica Light',sans-serif;margin:0;'><a href='http://www.crownrms.com/' target='_blank' style='color: #1155cc;'>www.crownrms.com</a>&nbsp;</p>");
                     
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</td>");
            body.Append("</tr>");
            body.Append("</tbody>");
            body.Append("</table>");
            body.Append("</div>");
         //</ body >

                     //</ html >








                     //body.AppendFormat("Dear {0}\n", UName + ",");
                     //body.AppendLine("<br/>");
                     //body.AppendLine("<br/>");
                     //body.AppendFormat("Your request for password reset is processed and here is your new password.");
                     //body.AppendLine("<br/>");
                     //body.AppendLine("<br/>");
                     ////body.AppendFormat("Password :", Newpassword);
                     //body.AppendFormat("Password  :{0}\n", Newpassword);
                     //body.AppendLine("<br/>");
                     //body.AppendFormat("Webapplication  :{0}\n", "https://e-storage.crownims.com/sod");
                     //body.AppendLine("<br/>");
                     //body.AppendLine("<br/>");
                     //body.AppendLine("<br/>");
                     //body.AppendFormat("Password change request date & time:{0}\n", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                     //body.AppendLine("<br/>");

                     //body.AppendFormat("If you have not requested a password reset, kindly report immediately to {0}\n", "estoragesupport@crownims.com");
                     ////	body.AppendLine(@"If you have not requested a password reset, kindly report immediately to");
                     //body.AppendLine("<br/>");
                     //body.AppendLine("<br/>");
                     //body.AppendLine(@"Thanks you!");
                     //body.AppendLine("<br/>");

                     ////Table end.
                     //body.Append("</table>");

                     //body.AppendLine("<br/>");


                     ////body.AppendLine("<a href=" + URL + " >Click Here to download file </a>");
                     //body.AppendLine("Regards,");
                     //body.AppendLine("<br/>");
                     //body.AppendLine("DMS Team");

                     mailMessage.Body = body.ToString();

			SmtpClient smtp = new SmtpClient();
			smtp.Host = ConfigurationManager.AppSettings["Host"];
			smtp.EnableSsl = true;// Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
			System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
			NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
			NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = NetworkCred;
			smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
			smtp.Send(mailMessage);
		}


	}
}
