using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http; 
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.WebAPI.Extensions;
using System.Web.Http.Cors;
using System.Configuration; 
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net.Mime;
using iTextSharp.text.pdf;
 
using iTextSharp.text;

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InwardController : ApiController
    {
		InwardRepository<InwardEntity> objRepositary;
		public InwardController(InwardRepository<InwardEntity> obj)
		{
			objRepositary = obj;
		}
		[HttpGet]
		[Route("api/Inward/GetList")]
		public HttpResponseMessage GetList([FromUri] string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.Get();
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpGet]
		[Route("api/Inward/GetDetails")]
		public HttpResponseMessage GetDetails([FromUri] int ID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.Get(ID);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

        [HttpGet]
        [Route("api/Inward/GetFieldsName")]
        public HttpResponseMessage GetFieldsName([FromUri] int id,string FileNo , string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetDetails(id, FileNo);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }
        

		[HttpGet]
		[Route("api/Inward/GetPODCode")]
		public HttpResponseMessage GetPODCode([FromUri] string PODNo, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = ""; // objRepositary.GetFileNo(PODNo, PODNo);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpGet]
		[Route("api/Inward/GetBarcodeData")]
		public HttpResponseMessage GetBarcodeData([FromUri] string CaseNo, string user_Token, string status)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = "";// objRepositary.GetBarcodeData(CaseNo, status);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
			 

		[HttpPost]
		[Route("api/Inward/PODdetailsEntry")]
		public HttpResponseMessage PODdetailsEntry([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{


					string val = objRepositary.PODdetailsEntry(objEntity);
					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


        [HttpPost]
        [Route("api/Inward/PODEntry")]
        public HttpResponseMessage PODEntry([FromBody] InwardEntity objEntity)
        {
            try
            {

                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {


                    string val = objRepositary.PODEntry(objEntity);
                    return Request.CreateResponse(HttpStatusCode.OK, val);
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
				LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

		//[HttpPost]
		//[Route("api/Inward/PODDetailsEntry")]
		//public HttpResponseMessage PODDetailsEntry([FromBody] InwardEntity objEntity)
		//{
		//	try
		//	{

		//		if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
		//		{


		//			string val = objRepositary.PODDetailsEntry(objEntity);
		//			return Request.CreateResponse(HttpStatusCode.OK, val);
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
		[Route("api/Inward/FileACk")]
		public HttpResponseMessage FileACk([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{


					string val = objRepositary.UpdateReceivedPOD(objEntity);
					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				//LogError();
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpPost]
		[Route("api/Inward/InwardEntry")]
		public HttpResponseMessage InwardEntry([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{


					string val = objRepositary.InwardEntry(objEntity);
					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				//LogError();
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		//[HttpPost]
		//[Route("api/Inward/PODDetailsEntry")]
		//public HttpResponseMessage PODDetailsEntry([FromBody] InwardEntity objEntity)
		//{
		//	try
		//	{

		//		if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
		//		{


		//			string val = objRepositary.InwardEntry(objEntity);
		//			return Request.CreateResponse(HttpStatusCode.OK, val);
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}
		

	   [HttpPost]
		[Route("api/Inward/PODAcknowledge")]
		public HttpResponseMessage PODAcknowledge([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					string val = objRepositary.PODAcknowledge(objEntity);
				   // SendMailPODACk(objEntity.PODNo, objEntity.CreatedBy);

					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}



		[HttpPost]
		[Route("api/Inward/SendBackBranch")]
		public HttpResponseMessage SendBackBranch([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{


					string val = objRepositary.SendBackBranch(objEntity);

					//SendMailBySendBackPOD(objEntity.PODNo, objEntity.CreatedBy);


					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpPost]
		[Route("api/Inward/AcknowledgePOD")]
		public HttpResponseMessage AcknowledgePOD([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					string val = ""; // objRepositary.AcknowledgePOD(objEntity);
					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
		 
		[HttpPost]
		[Route("api/Inward/UpdatePODCount")]
		public HttpResponseMessage UpdatePODCount([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					string val = "";// objRepositary.UpdateReceivedPOD(objEntity);

					//SendMailByPODReceived(val, objEntity.PODNO, objEntity.TotalReceivedInvoicecount, objEntity.TotalInvoicecount);

					return Request.CreateResponse(HttpStatusCode.OK, "Record Udpated succesfully");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpPost]
		[Route("api/Inward/updatefilestatus")]
		public HttpResponseMessage updatefilestatus([FromBody] InwardEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					string val =objRepositary.updatefilestatus(objEntity);

					//SendMailByPODReceived(val, objEntity.PODNO, objEntity.TotalReceivedInvoicecount, objEntity.TotalInvoicecount);

					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		public void SendMailBySendBackPOD(string PODNo,int USERid)
		{


			DataTable dt = getData("EXEC SP_GetEMAILID  '" + PODNo + "'");
			var mailMessage = new MailMessage();

			//DataTable dt2 = getData("EXEC GetUserdetails " + UserID + "");

			string ToEmailID = dt.Rows[0]["EmailID"].ToString();
			//string CCEmailID = dt.Rows[0]["EmailID"].ToString();

			//string sendername = dt.Rows[0][0].ToString();
			//string senderEmailID = dt.Rows[0][1].ToString();


			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
			mailMessage.To.Add(new MailAddress(ToEmailID));
			//mailMessage.CC.Add(new MailAddress(CCEmailID));




			//mailMessage.From = new MailAddress(strMaildDetails.FromEmailID);
			//mailMessage.To.Add(new MailAddress(strMaildDetails.ToEmailID));

			mailMessage.Subject = "POD Acknowledgement Notification";
			mailMessage.IsBodyHtml = true;

			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();
			body.AppendFormat("Hello, {0}\n\n", "");


			//body.AppendFormat("has shared the following documents with you. You can access them by clicking the link provided against each document.");

			body.AppendLine("<br/>");
			body.AppendLine(@"We have received the following consignment(s) sent by you at our center");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");


			//DataTable dt = ToDataTable(strMaildDetails<MailEntity>);
			MailEntity maile = new MailEntity();

			//StringBuilder sb = new StringBuilder();

			//Table start.
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");
			body.AppendLine("POD No -: " + PODNo);
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			//body.AppendLine("No. of Invoice dispatch count by UPL - " + sentcount);

			//body.AppendLine("<br/>");
			//body.AppendLine("No. of Invoice Received count by Crown - " + recievedcount);
			//body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("Do not reply to this email as its mailbox is not monitored by a human or by the sender of this email. ");

			body.AppendLine("<br/>");

			//foreach (var file in strMaildDetails)
			//{
			//	body.AppendLine("<br/>");
			//	string Link = URL + file.RandomCode;
			//	body.AppendLine(file.FileNo + "  :-  " + "<a href=" + Link  + " >Click here to download file </a>");
			//	// zip.Save();
			//}



			body.AppendLine("<br/>");
			body.AppendLine("Regards,");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");

			mailMessage.Body = body.ToString();

			//mailMessage.Body = body;

			//if (strMaildDetails.IsAttachment == "T")
			//{ 
			//System.Net.Mail.Attachment attachment;
			//attachment = new System.Net.Mail.Attachment(strMaildDetails.FilePath);
			//mailMessage.Attachments.Add(attachment);
			//}

			SmtpClient smtp = new SmtpClient();
			smtp.Host = ConfigurationManager.AppSettings["Host"];
			//smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
			System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
			NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
			NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = NetworkCred;
			///NetworkCred.EnableSsl = true;

			smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
			smtp.EnableSsl = true;
			smtp.Send(mailMessage);

		}


		public void SendMailPODACk(string PODNo, int USERid)
		{


			DataTable dt = getData("EXEC SP_GetEMAILID  '" + PODNo + "'");
			var mailMessage = new MailMessage();

			//DataTable dt2 = getData("EXEC GetUserdetails " + UserID + "");

			string ToEmailID = dt.Rows[0]["EmailID"].ToString();
			//string CCEmailID = dt.Rows[0]["EmailID"].ToString();

			//string sendername = dt.Rows[0][0].ToString();
			//string senderEmailID = dt.Rows[0][1].ToString();


			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
			mailMessage.To.Add(new MailAddress(ToEmailID));
			//mailMessage.CC.Add(new MailAddress(CCEmailID));




			//mailMessage.From = new MailAddress(strMaildDetails.FromEmailID);
			//mailMessage.To.Add(new MailAddress(strMaildDetails.ToEmailID));

			mailMessage.Subject = "POD Acknowledgement Notification";
			mailMessage.IsBodyHtml = true;

			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();
			body.AppendFormat("Hello, {0}\n\n", "");


			//body.AppendFormat("has shared the following documents with you. You can access them by clicking the link provided against each document.");

			body.AppendLine("<br/>");
			body.AppendLine(@"We have received the following consignment(s) sent by you at our center");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");


			//DataTable dt = ToDataTable(strMaildDetails<MailEntity>);
			MailEntity maile = new MailEntity();

			//StringBuilder sb = new StringBuilder();

			//Table start.
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");
			body.AppendLine("POD No -: " + PODNo);
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			//body.AppendLine("No. of Invoice dispatch count by UPL - " + sentcount);

			//body.AppendLine("<br/>");
			//body.AppendLine("No. of Invoice Received count by Crown - " + recievedcount);
			//body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("Do not reply to this email as its mailbox is not monitored by a human or by the sender of this email. ");

			body.AppendLine("<br/>");

			//foreach (var file in strMaildDetails)
			//{
			//	body.AppendLine("<br/>");
			//	string Link = URL + file.RandomCode;
			//	body.AppendLine(file.FileNo + "  :-  " + "<a href=" + Link  + " >Click here to download file </a>");
			//	// zip.Save();
			//}



			body.AppendLine("<br/>");
			body.AppendLine("Regards,");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");

			mailMessage.Body = body.ToString();

			//mailMessage.Body = body;

			//if (strMaildDetails.IsAttachment == "T")
			//{ 
			//System.Net.Mail.Attachment attachment;
			//attachment = new System.Net.Mail.Attachment(strMaildDetails.FilePath);
			//mailMessage.Attachments.Add(attachment);
			//}

			SmtpClient smtp = new SmtpClient();
			smtp.Host = ConfigurationManager.AppSettings["Host"];
			//smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
			System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
			NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
			NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = NetworkCred;
			///NetworkCred.EnableSsl = true;

			smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
			smtp.EnableSsl = true;
			smtp.Send(mailMessage);

		}


		public void SendMailByPODReceived(string EmailID,string PODNo, string recievedcount, string sentcount)
		{


			//DataTable dt = getData("EXEC SP_GetInwardDetailsByPODNO '" + PODNo + "','" + TerityCode + "'");
			var mailMessage = new MailMessage();

			//DataTable dt2 = getData("EXEC GetUserdetails " + UserID + "");

			string ToEmailID = EmailID;
			//string CCEmailID = dt.Rows[0]["EmailID"].ToString();

			//string sendername = dt.Rows[0][0].ToString();
			//string senderEmailID = dt.Rows[0][1].ToString();


			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
			mailMessage.To.Add(new MailAddress(ToEmailID));
			//mailMessage.CC.Add(new MailAddress(CCEmailID));




			//mailMessage.From = new MailAddress(strMaildDetails.FromEmailID);
			//mailMessage.To.Add(new MailAddress(strMaildDetails.ToEmailID));

			mailMessage.Subject = "POD Acknowledgement Notification";
			mailMessage.IsBodyHtml = true;

			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();
			body.AppendFormat("Hello, {0}\n\n", "");


			//body.AppendFormat("has shared the following documents with you. You can access them by clicking the link provided against each document.");

			body.AppendLine("<br/>");
			body.AppendLine(@"We have received the following consignment(s) sent by you at our Crown records center");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");


			//DataTable dt = ToDataTable(strMaildDetails<MailEntity>);
			MailEntity maile = new MailEntity();

			//StringBuilder sb = new StringBuilder();

			//Table start.
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");
			body.AppendLine("POD No - " + PODNo);
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("No. of Invoice dispatch count by UPL - " + sentcount);
			 
			body.AppendLine("<br/>");
			body.AppendLine("No. of Invoice Received count by Crown - " + recievedcount);
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("Do not reply to this email as its mailbox is not monitored by a human or by the sender of this email. ");

			body.AppendLine("<br/>");

			//foreach (var file in strMaildDetails)
			//{
			//	body.AppendLine("<br/>");
			//	string Link = URL + file.RandomCode;
			//	body.AppendLine(file.FileNo + "  :-  " + "<a href=" + Link  + " >Click here to download file </a>");
			//	// zip.Save();
			//}



			body.AppendLine("<br/>");
			body.AppendLine("Regards,");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");

			mailMessage.Body = body.ToString();

			//mailMessage.Body = body;

			//if (strMaildDetails.IsAttachment == "T")
			//{ 
			//System.Net.Mail.Attachment attachment;
			//attachment = new System.Net.Mail.Attachment(strMaildDetails.FilePath);
			//mailMessage.Attachments.Add(attachment);
			//}

			SmtpClient smtp = new SmtpClient();
			smtp.Host = ConfigurationManager.AppSettings["Host"];
			//smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
			System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
			NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
			NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = NetworkCred;
			///NetworkCred.EnableSsl = true;

			smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
			smtp.EnableSsl = true;
			smtp.Send(mailMessage);

		}

        [HttpPost]
        [Route("api/Inward/GetStatusReport")]
        public HttpResponseMessage GetStatusReport([FromBody] InwardEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetStatusReport(objEntity);

                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }


        [HttpPost]
		[Route("api/Inward/GetPendingINVData")]
		public HttpResponseMessage GetPendingINVData([FromBody] InwardEntity objEntity)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					var objData =objRepositary.GetPendingINVData();
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


        [HttpPost]
        [Route("api/Inward/GetPODReport")]
        public HttpResponseMessage GetPODReport([FromBody] InwardEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetPODReport(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }


        [HttpPost]
		[Route("api/Inward/GetBatchDetailsReport")]
		public HttpResponseMessage GetBatchDetailsReport([FromBody] InwardEntity objEntity)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					var objData = objRepositary.GetBatchDetailsReport(objEntity);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}



		[HttpPost]
		[Route("api/Inward/GetInwardREport")]
		public HttpResponseMessage GetInwardREport([FromBody] InwardEntity objEntity)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					var objData = objRepositary.GetInwardREport(objEntity);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}



        [HttpGet]
        [Route("api/Inward/GetPendingData")]
        public HttpResponseMessage GetPendingData([FromUri] string user_Token, string UserID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetPendingData(UserID);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        [HttpGet]
		[Route("api/Inward/GetInwardData")]
		public HttpResponseMessage GetInwardData([FromUri] int CompanyID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData =objRepositary.GetInwardData(CompanyID);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		//[HttpGet]
		//[Route("api/Inward/GetPODDetails")]
		//public HttpResponseMessage GetPODDetails([FromUri] string PODNO, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData =  objRepositary.GetTerritorydata(PODNO);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}

 
 


		//[HttpGet]
		//[Route("api/Inward/UpdateLanDetails")]
		//public HttpResponseMessage UpdateLanDetails([FromUri] string LAN, string BatchNo,int UserID, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData =objRepositary.UpdateLanDetails(BatchNo , LAN, UserID);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}


		[HttpGet]
		[Route("api/Inward/GetAccountDetails")]
		public HttpResponseMessage GetAccountDetails([FromUri] string AccountNo, string ProductType, string user_Token, string UserID)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData =objRepositary.GetAccountDetails(AccountNo, ProductType, UserID);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpGet]
		[Route("api/Inward/GetAccountdata")]
		public HttpResponseMessage GetAccountdata([FromUri] string AccountNo, string user_Token,string UserID)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetAccountdata(AccountNo, UserID);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		//[HttpGet]
		//[Route("api/Inward/GetLANByBatchNoACk")]
		//public HttpResponseMessage GetLANByBatchNoACk([FromUri] string LAN, string BatchNo, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData = objRepositary.GetLANByBatchNoACk(LAN, BatchNo);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}


		//[HttpGet]
		//[Route("api/Inward/GetCartonNoDetailsLAN")]
		//public HttpResponseMessage GetCartonNoDetailsLAN([FromUri] string CartonNo, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData = objRepositary.GetCartonNoDetailsLAN(CartonNo);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}




		//[HttpGet]
		//[Route("api/Inward/GetCheckerData")]
		//public HttpResponseMessage GetCheckerData([FromUri] string LAN, string BatchNo, int UserID, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData = objRepositary.GetCheckerData(LAN, BatchNo, UserID);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}

		//[HttpGet]
		//[Route("api/Inward/GetLANDetailsByUser")]
		//public HttpResponseMessage GetLANDetailsByUser([FromUri] string LAN, int UserID, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData = objRepositary.GetLANDetailsByUser(LAN, UserID);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}



		[HttpGet]
		[Route("api/Inward/GetLANDetailsByUserAndBatchNo")]
		public HttpResponseMessage GetLANDetailsByUserAndBatchNo([FromUri] string LAN, string BatchNo, int UserID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetLANDetailsByUserAndBatchNo(LAN, BatchNo, UserID);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
 


		[HttpGet]
		[Route("api/Inward/GetPODDetailsEntry")]
		public HttpResponseMessage GetPODDetailsEntry([FromUri] string PODNO, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData =objRepositary.GetPODDetailsEntry(PODNO);

					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpGet]
		[Route("api/Inward/GetPODDetailsFulletron")]
		public HttpResponseMessage GetPODDetailsFulletron([FromUri]  string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetPODDetailsFulletron();

					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpGet]
		[Route("api/Inward/GetDumpdataSearch")]
		public HttpResponseMessage GetDumpdataSearch([FromUri] string user_Token,string UserID)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData =objRepositary.GetDumpdataSearch(UserID);

					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpGet]
		[Route("api/Inward/GetPODDetailsACK")]
		public HttpResponseMessage GetPODDetailsACK([FromUri] string user_Token, string UserID)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetPODDetailsACK(UserID);

					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		//[HttpGet]
		//[Route("api/Inward/GetPODByLAN")]
		//public HttpResponseMessage GetPODByLAN([FromUri] string PODNO, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var objData =objRepositary.GetPODByLAN(PODNO);
		//			if (objData != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, objData);
		//			else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}


		[HttpGet]
		[Route("api/Inward/GetBatchDetails")]
		public HttpResponseMessage GetBatchDetails([FromUri] string BatchNo, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData =objRepositary.GetBatchDetails(BatchNo);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpGet]
		[Route("api/Inward/GetBatchDetails")]
		public HttpResponseMessage GetBatchDetails([FromUri] string BatchNo,int USERId, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetBatchDetails(BatchNo, USERId);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		[HttpGet]
		[Route("api/Inward/GetBatchClose")]
		public HttpResponseMessage GetBatchClose([FromUri] string BatchNo, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData =objRepositary.GetBatchClose(BatchNo);
					if (objData != null)
						return Request.CreateResponse(HttpStatusCode.OK, objData);
					else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		//[HttpGet]
		//[Route("api/Inward/PackPOD")]
		//public HttpResponseMessage PackPOD([FromUri] string PODNO, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{

		//			SendMailByPODDetails(PODNO);

		//			DataTable dt = getData("EXEC SP_UpdateMailStatus '" + PODNO+"'");

		//			//var objData = objRepositary.GetPODDetailsEntry(PODNO);

		//			return Request.CreateResponse(HttpStatusCode.OK, "Mail sent Successfully ");
					 
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError(ex);
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}


		public void SendMailByPODDetails(string PODNo)
		{


			DataTable dt = getData("EXEC SP_GetDetailsByPODNO '"+ PODNo +"'");
			var mailMessage = new MailMessage();
			string temp = ConfigurationManager.AppSettings["temp"] +"\\" + PODNo +".csv";

				CreateCSVFile(dt, temp);

			//DataTable dt2 = getData("EXEC GetUserdetails " + UserID + "");

			string ToEmailID = dt.Rows[0]["email"].ToString();
			//string CCEmailID = dt.Rows[0]["EmailID"].ToString();

			//string sendername = dt.Rows[0][0].ToString();
			//string senderEmailID = dt.Rows[0][1].ToString();


			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
			mailMessage.To.Add(new MailAddress(ToEmailID));
			//mailMessage.CC.Add(new MailAddress(CCEmailID));

			 
			mailMessage.Subject = "Inward Detail Entry Notification";
			mailMessage.IsBodyHtml = true;

			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();
			body.AppendFormat("Dear Sir/ Madam, {0}\n\n", "");


			//body.AppendFormat("has shared the following documents with you. You can access them by clicking the link provided against each document.");

			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine(@"Please find the attached file inwarding status of consignments received from you.");
			body.AppendLine("<br/>");

			string URL = ConfigurationManager.AppSettings["URL"];
			//body.AppendLine("<a href=" + URL + " >Click Here to download file </a>");

			//DataTable dt = ToDataTable(strMaildDetails<MailEntity>);
			MailEntity maile = new MailEntity();

			//StringBuilder sb = new StringBuilder();

			//Table start.
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");

			body.AppendLine("<br/>");


			//body.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");

			////Adding HeaderRow.
			//body.Append("<tr>");
			//foreach (DataColumn column in dt.Columns)
			//{
			//	body.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
			//}
			////body.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + "Link" + "</th>");
			//body.Append("</tr>");


			////Adding DataRow.
			//foreach (DataRow row in dt.Rows)
			//{
			//	body.Append("<tr>");
			//	foreach (DataColumn column in dt.Columns)
			//	{
			//		body.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
			//	}
			//	//body.Append("<td style='width:100px;border: 1px solid #ccc'>" + "Link" + "</td>");
			//	body.Append("</tr>");
			//}


			////Table end.
			//body.Append("</table>");




			 
			body.AppendLine("Do not reply to this email as its mailbox is not monitored by a human or by the sender of this email. ");

			body.AppendLine("<br/>");

			//foreach (var file in strMaildDetails)
			//{
			//	body.AppendLine("<br/>");
			//	string Link = URL + file.RandomCode;
			//	body.AppendLine(file.FileNo + "  :-  " + "<a href=" + Link  + " >Click here to download file </a>");
			//	// zip.Save();
			//}



			body.AppendLine("<br/>");
			body.AppendLine("Regards,");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");

			mailMessage.Body = body.ToString();

			//mailMessage.Body = body;

			//if (strMaildDetails.IsAttachment == "T")
			//{ 
			//System.Net.Mail.Attachment attachment;
			//attachment = new System.Net.Mail.Attachment(strMaildDetails.FilePath);
			//mailMessage.Attachments.Add(attachment);
			//}

			string FileName ="InvoiceDetails-" + PODNo + ".csv";

			Attachment attachment = new Attachment(temp);
			attachment.Name = FileName;
			mailMessage.Attachments.Add(attachment);

			SmtpClient smtp = new SmtpClient();
			smtp.Host = ConfigurationManager.AppSettings["Host"];
			//smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
			System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
			NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
			NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = NetworkCred;
			///NetworkCred.EnableSsl = true;

			smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
			smtp.EnableSsl = true;
			smtp.Send(mailMessage);

		}

		public void CreateCSVFile(DataTable dt, string strFilePath)
		{
			StreamWriter sw = new StreamWriter(strFilePath, false);
			int iColCount = dt.Columns.Count;
			int iRowcount = dt.Rows.Count;

			if (iRowcount != 0)
			{

				for (int i = 0; i < iColCount; i++)
				{
					sw.Write(dt.Columns[i]);
					if (i < iColCount - 1)
					{
						sw.Write(",");
					}
				}
				sw.Write(sw.NewLine);
				foreach (DataRow dr in dt.Rows)
				{
					for (int i = 0; i < iColCount; i++)
					{
						if (!Convert.IsDBNull(dr[i]))
						{
							sw.Write(dr[i].ToString());
						}
						if (i < iColCount - 1)
						{
							sw.Write(",");
						}
					}
					sw.Write(sw.NewLine);
				}
			}
			else
			{
				sw.Write("Error Not Found");
			}
			sw.Close();
		}

        //[HttpGet]
        //[Route("api/Inward/SearchRecords")]
        //public HttpResponseMessage SearchRecords([FromUri]  string  FileNo ,string user_Token, string SearchBy)
        //{
        //	try
        //	{
        //		if (ValidateTokenExtension.ValidateToken(user_Token))
        //		{
        //			var objData = objRepositary.SearchRecords(1,FileNo, SearchBy);
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


        [HttpGet]
        [Route("api/Inward/SearchRecordsByFilter")]
        public HttpResponseMessage SearchRecordsByFilter([FromUri] string FileNo, string user_Token, string SearchBy, string UserID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.SearchRecordsByFilter(FileNo, SearchBy, UserID);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        //[HttpGet]
        //[Route("api/Inward/GetPODDetails")]
        //public HttpResponseMessage GetFileUplaodDetails([FromUri] string  podno, string user_Token)
        //{
        //	try
        //	{
        //		if (ValidateTokenExtension.ValidateToken(user_Token))
        //		{
        //			var objData = objRepositary.GetFileUplaodDetails(podno.ToString());
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

        public DataTable getData(string sql)
		{
			DataSet ds = new DataSet();
			SqlDataAdapter ad = new SqlDataAdapter(sql, getConnectionString());
			DataTable t = new DataTable();
			ad.Fill(ds);

			DataTable dt;
			dt = ds.Tables[0];


			return dt;
		}

		public DataSet GetDataset(string sql)
		{
			DataSet ds = new DataSet();
			SqlDataAdapter ad = new SqlDataAdapter(sql, getConnectionString());
			DataTable t = new DataTable();
			ad.Fill(ds);

			DataTable dt;
			dt = ds.Tables[0];


			return ds;
		}

		public string getConnectionString()
		{
			return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
		}

		[HttpGet]
		[Route("api/Inward/DownloadFile")]
		public HttpResponseMessage DownloadFile([FromUri]  string BatchNo, string user_Token)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(user_Token))
				{

					var objData =objRepositary.GetBatchdetailsByBatchNo(BatchNo);
				//	string Filetype = Path.GetExtension(objData);


					var TempPath = Path.Combine(ConfigurationManager.AppSettings["temp"].ToString(), Guid.NewGuid().ToString() +".pdf");

					if (objData.Count() > 0)
					{
						foreach (var file1 in objData)
						{ 

                            GeneratePDFFile(file1.ToAddress,TempPath, file1.BatchNo, file1.PODNo, file1.DespatchedDate, file1.AccountNo, file1.CourierName, file1.BranchCode, file1.BranchName);
							                             
                        }
					}


						//if (!Directory.Exists(TempPath))
						//{
						//	Directory.CreateDirectory(TempPath);
						//}
						//return TempPath
						//
						//;

				

					//  var objData = @"D:\Application\14April2020\A000758829.pdf"; // objRepositary.DownloadFile(objEntity);

					HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
					byte[] bytes = System.IO.File.ReadAllBytes(TempPath);
					result.Content = new ByteArrayContent(bytes);

					result.Content.Headers.ContentLength = bytes.LongLength;

					var dataStream = new MemoryStream(bytes);
					result.Content = new StreamContent(dataStream);
					result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
					result.Content.Headers.ContentDisposition.FileName = "123123.pdf";
					result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
					return result;


				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex);

				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}

		private void LogError(Exception ex)
		{
			var TempPath = (ConfigurationManager.AppSettings["temp"].ToString());
			string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			message += string.Format("Message: {0}", ex.Message);
			message += Environment.NewLine;
			message += string.Format("StackTrace: {0}", ex.StackTrace);
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

		protected void GeneratePDFFile(string ToAddress ,string PDFFilepath,string BatchNo,string PODNo,string date,string LanCount,string CourierName, string BranchCode, string BranchName)
		{

			//string FromAddress = "Branch Code :- "+ BranchCode + " , Branch Name :- "+ BranchName;
			//string TOAddress= "Crown Records Management ,  119 / 1B 1K , Moopanar Street, Pandur Village , Permattunallur, Guduvancherry – 603202., Phone N0. 04435102751";



			//Create document  
			Document doc = new Document();
			//Create PDF Table  
			PdfPTable tableLayout = new PdfPTable(2);
			PdfPTable tableLayout1 = new PdfPTable(1);
			//Create a PDF file in specific path  
			PdfWriter.GetInstance(doc, new FileStream(PDFFilepath, FileMode.Create));
			//Open the PDF document  
			doc.Open();
			//Add Content to PDF  
			doc.Add(Add_Content_To_PDF(tableLayout,BatchNo,PODNo,date,LanCount.ToString(),CourierName, BranchCode, BranchName));

			doc.Add(Add_Address(tableLayout1, ToAddress) );



			// Closing the document  
			doc.Close();
			//		btnOpenPDFFile.Enabled = true;
			//	btnGeneratePDFFile.Enabled = false;
		}

		private PdfPTable Add_Address(PdfPTable tableLayout, string ToAddress)
		{
			float[] headers = {
		 
		100
	}; //Header Widths  
			tableLayout.SetWidths(headers); //Set the pdf headers  
			tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
											   //Add Title to the PDF file at the top  
			tableLayout.AddCell(new PdfPCell(new Phrase("Address", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 25, 1, iTextSharp.text.BaseColor.BLACK)))

			//tableLayout.AddCell(new PdfPCell(new Phrase("Creating PDF file using iTextsharp", new iTextSharp.text.Font(Font.Bold, 13, 1, new iTextSharp.text.BaseColor(153, 51, 0))))
			{
				Colspan = 6,
				Border = 1,
				PaddingBottom = 20,
				HorizontalAlignment = Element.ALIGN_CENTER
			});


			//{
			//    //Colspan = 6,
			//    //Border = 1,
			//    //PaddingBottom = 20,
			//    HorizontalAlignment = Element.ALIGN_CENTER;
			//});




			//content.AddCell(cell);

			//Add header  
			//AddCellToHeader(tableLayout, "Batch Reference Barcode");
			//AddCellToBody(tableLayout, "To Address");
			//AddCellToHeader(tableLayout, "From Address");

			AddCellToBodyAddress(tableLayout, ToAddress);
		//	AddCellToBody(tableLayout, FromAddress);

			return tableLayout;
		}
		private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, string BatchNo, string PODNo, string Ddate, string LanCount, string CourierName, string BranchCode, string BranchName)
		{
			float[] headers = {
		40,
		50
	}; //Header Widths  
			tableLayout.SetWidths(headers); //Set the pdf headers  
			tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
			
			//tableLayout.                                //Add Title to the PDF file at the top  

			iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance("c:\\temp\\Janabank.png");
			PdfPCell cell = new PdfPCell(myImage);

			tableLayout.AddCell(new PdfPCell(myImage));

			tableLayout.AddCell(new PdfPCell(new Phrase("Janabank Unsecured File Dispatch Details", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 14, 1, iTextSharp.text.BaseColor.BLACK)))

			//tableLayout.AddCell(new PdfPCell(new Phrase("Creating PDF file using iTextsharp", new iTextSharp.text.Font(Font.Bold, 13, 1, new iTextSharp.text.BaseColor(153, 51, 0))))
			{
				Colspan = 6,
				Border = 0,
				PaddingBottom = 20,
				HorizontalAlignment = Element.ALIGN_CENTER
			});
			//Add header  
			//AddCellToHeader(tableLayout, "Batch Reference Barcode");
			AddCellToHeader(tableLayout, "Name");
			AddCellToHeader(tableLayout, "Value");

			//AddCellToBody(tableLayout, "Batch Reference Barcode");
			//AddCellToBody(tableLayout, BatchNo);

			AddCellToBody(tableLayout, "Batch Reference ID");
			AddCellToBody(tableLayout, BatchNo);

			AddCellToBody(tableLayout, "BranchName");
			AddCellToBody(tableLayout, BranchName);
			
			AddCellToBody(tableLayout, "BranchCode");
			AddCellToBody(tableLayout, BranchCode);

			AddCellToBody(tableLayout, "Consignment No");
			AddCellToBody(tableLayout, PODNo);

			AddCellToBody(tableLayout, "Courier Name");
			AddCellToBody(tableLayout, CourierName);
			AddCellToBody(tableLayout, "Dispatch Date");

			AddCellToBody(tableLayout, Ddate);
			AddCellToBody(tableLayout, "No. of Account No.");
			AddCellToBody(tableLayout, LanCount.ToString());
		


			return tableLayout;
		}
		// Method to add single cell to the header  
		private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
		{
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 18, 1, iTextSharp.text.BaseColor.WHITE)))
			{
				HorizontalAlignment = Element.ALIGN_LEFT,
				Padding = 5,
				BackgroundColor = new iTextSharp.text.BaseColor(0, 51, 102)
			});
		}
		// Method to add single cell to the body  
		private static void AddCellToBody(PdfPTable tableLayout, string cellText)
		{
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 18, 1, iTextSharp.text.BaseColor.BLACK)))
			{
				HorizontalAlignment = Element.ALIGN_LEFT,
				Padding = 5,
				BackgroundColor = iTextSharp.text.BaseColor.WHITE
			});
		}

		private static void AddCellToBodyAddress(PdfPTable tableLayout, string cellText)
		{
			tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 25, 1, iTextSharp.text.BaseColor.BLACK)))
			{
				HorizontalAlignment = Element.ALIGN_LEFT,
				Padding = 5,
				BackgroundColor = iTextSharp.text.BaseColor.WHITE
			});
		}


	}
}
