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


using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MailController : ApiController
    {
		IMail<MailEntity> objRepositary;
		public MailController(IMail<MailEntity> obj)
		{
			objRepositary = obj;
		}

		//[HttpGet]
		//[Route("api/Mail/GetBranchList")]
		//public HttpResponseMessage GetBranchList([FromUri] string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var homeBanner = objRepositary.Get();
		//			if (homeBanner != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
		//[Route("api/Mail/GetBranchDetails")]
		//public HttpResponseMessage GetBranchDetails([FromUri] int homeBannerID, string user_Token)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			var homeBanner = objRepositary.Get(homeBannerID);
		//			if (homeBanner != null)
		//				return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
		[Route("api/Mail/SendEmail")]
		public HttpResponseMessage Post([FromBody] MailEntity obj)
		{
			string strFileNo = string.Empty;
			try
			{
				if (ValidateTokenExtension.ValidateToken(obj.User_Token))	
				{
					DeleteFiles(obj.CreatedBy);
					 strFileNo = obj.FileNo;
					string[] strfiles = strFileNo.Split(',');
					string val = "";
					string FIleList = "";
					for (int i = 0; i < strfiles.Length-1; i++)
					{
						obj.FileNo = strfiles[i];
						  val = objRepositary.AddEmailDetails(obj);
						FIleList += strfiles[i] + ',';
					}

					//string val = objRepositary.AddEmailDetails(obj);
					
					ShareFileByAttachment(strFileNo, obj.CreatedBy,obj.ToEmailID,obj.predefined,obj.Subject,obj.htmlContent);

					bool  _Flag= objRepositary.Update(FIleList);

					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex, "MSG1");
				//	bool _Flag = objRepositary.Update(FIleList);
				bool _Flag = objRepositary.UpdateErrorLog(strFileNo, ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		[HttpPost]
		[Route("api/Mail/SendEmailBulkFiles")]
		public HttpResponseMessage AddEmailDetails([FromBody] MailEntity obj)
		{
			string strFileNo = string.Empty;
			try
			{
				if (ValidateTokenExtension.ValidateToken(obj.User_Token))
				{
					DeleteFiles(obj.CreatedBy);
					 strFileNo = obj.FileNo;
					string[] strfiles = strFileNo.Split(',');
					string val = "";

					//bool _Flag = objRepositary.Update(strFileNo);

					for (int i = 0; i < strfiles.Length-1; i++)
					{
						obj.FileNo = strfiles[i];
						  val = objRepositary.Add(obj);
					}				

					ShareFileBylink(strFileNo,obj.ToEmailID, obj.CreatedBy,obj.predefined,obj.Subject,obj.htmlContent);

					bool _Flag = objRepositary.Update(strFileNo);

					return Request.CreateResponse(HttpStatusCode.OK, val);
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				LogError(ex, "MSG2");
				bool _Flag = objRepositary.UpdateErrorLog(strFileNo, ex.Message);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
		
		public void ShareFileBylink(string  FileNo,string ToEmailID,int UserID,bool predefined,string subject ,string htmlcontent)
		{

		//	var strMaildDetails= objRepositary.GetBulkFile(FileNo, ToEmailID);
			DataTable dt = getData("EXEC SP_GetMailListByShareLinkFullFile '" + FileNo + "','" + ToEmailID + "'");
			var mailMessage = new MailMessage();
		//	LogErrorDetails("DT1");
			//LogErrorDetails(dt.Rows.Count.ToString());

			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
			//	mailMessage.To.Add(new MailAddress(ToEmailID));

			string[] strfiles = ToEmailID.Split(',');

			for (int i = 0; i < strfiles.Length - 1; i++)
			{
				mailMessage.To.Add(new MailAddress(strfiles[i]));
				//obj.FileNo = strfiles[i];
				//            val = objRepositary.AddEmailDetails(obj);
				//            FIleList += strfiles[i] + ',';
			}

			DataTable dt2 = getData("EXEC GetUserdetails " + UserID + "");
//			LogErrorDetails("DT2");
	//		LogErrorDetails(dt2.Rows.Count.ToString());

			string sendername = dt2.Rows[0][0].ToString();
			string senderEmailID = dt2.Rows[0][1].ToString();


			//mailMessage.From = new MailAddress(strMaildDetails.FromEmailID);
			//mailMessage.To.Add(new MailAddress(strMaildDetails.ToEmailID));




			mailMessage.IsBodyHtml = true;
			
			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();
			body.AppendFormat("Hello, {0}\n\n", "");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");

			body.AppendFormat(sendername +"  "+"has shared the following documents with you. You can access them by clicking the link provided against each document.");
			
			body.AppendLine("<br/>");
			body.AppendLine(@"Please reach out to "+ senderEmailID  + " the sender for any questions you may have. ");
			body.AppendLine("<br/>");

			string URL = ConfigurationManager.AppSettings["URL"];
			//body.AppendLine("<a href=" + URL + " >Click Here to download file </a>");

			//DataTable dt = ToDataTable(strMaildDetails<MailEntity>);
			MailEntity maile = new MailEntity();

			//StringBuilder sb = new StringBuilder();

			//Table start.
			//body.AppendLine("<hr style=height: 2px; border - width:0; color: gray; background - color:red>");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");

			//Adding HeaderRow.
			body.Append("<tr>");
			foreach (DataColumn column in dt.Columns)
			{
				body.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
			}
			//body.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + "Link" + "</th>");
			body.Append("</tr>");


			//Adding DataRow.
			foreach (DataRow row in dt.Rows)
			{
				body.Append("<tr>");
				foreach (DataColumn column in dt.Columns)
				{
					if (column.ColumnName.ToString() == "Link")
					{
						string Link = URL + row[column.ColumnName].ToString();
						//body.AppendLine(file.FileNo + "  :-  " + "<a href=" + Link + " >Click here to download file </a>");
						body.Append("<td style='width:100px;border: 1px solid #ccc'>" + "<a href=" + Link + " >Click here to download file </a>" + "</td>");
					}
					else
					{
						body.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
					}
				}
				//body.Append("<td style='width:100px;border: 1px solid #ccc'>" + "Link" + "</td>");
				body.Append("</tr>");
			}

		
			//Table end.
			body.Append("</table>");
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
			body.AppendLine("<br/>");
			body.AppendLine("Regards,");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("DMS Team");
			body.AppendLine("<br/>");

			mailMessage.Subject = sendername + "  " + "  has shared document with you";
			mailMessage.Body = body.ToString();

			//if (predefined == true)
			//{
			//	mailMessage.Subject = subject;
			//	mailMessage.Body = htmlcontent;
			//}
			//else
			//{
			//	mailMessage.Subject = sendername + "  " + "  has shared document with you";
			//	mailMessage.Body = body.ToString();
			//}


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

		private void DeleteFiles(int UserID)
		{

			try
			{
				var TempPath = gettemppath(UserID);
				//  var TempPath = ConfigurationManager.AppSettings["temp"].ToString();

				System.IO.DirectoryInfo di = new DirectoryInfo(TempPath);

				foreach (FileInfo file in di.EnumerateFiles())
				{
					file.Delete();
				}
				foreach (DirectoryInfo dir in di.EnumerateDirectories())
				{
					dir.Delete(true);
				}
			}
			catch (IOException)
			{

			}

			finally { }



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


		private void LogErrorDetails( string msg)
		{
			var TempPath = (ConfigurationManager.AppSettings["temp"].ToString());
			string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			message += string.Format("Message: {0}", msg);
			message += Environment.NewLine;
			message += string.Format("StackTrac: {0}", msg);
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


		//public void ShareFileByAttachment(string FileNo)
		//{

		//	var strMaildDetails = objRepositary.GetBulkFile(FileNo);
		//	//DataTable dt = getData("EXEC SP_GetMailListByFileNo '" + FileNo + "'");

		//	DataSet ds = GetDataset("EXEC SP_GetMailListByFileNo '" + FileNo + "'");
		//	DataTable dtRows = ds.Tables[1];
		//	DataTable dtCol = ds.Tables[0];
		//	var mailMessage = new MailMessage();

		//	foreach (var file in strMaildDetails)
		//	{
		//		mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
		//		mailMessage.To.Add(new MailAddress(file.ToEmailID));
		//		//zip.AddFile(file.FilePath, file.FileNo);
		//		break;
		//		// zip.Save();
		//	}			

		//	mailMessage.Subject = "Crown DMS - check docuemnts";
		//	mailMessage.IsBodyHtml = true;
		//	//string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
		//	// = "Body containing <strong>HTML</strong>";

		//	//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

		//	var body = new System.Text.StringBuilder();
		//	body.AppendFormat("Dear Sir/Madam , {0}\n", "");
		//	body.AppendLine("<br/>");
		//	body.AppendLine("<br/>");
		//	body.AppendFormat("Scan On Demand, {0}\n", "");
		//	body.AppendLine("<br/>");

		//	body.AppendLine(@"Please find attached");
		//	body.AppendLine("<br/>");

		//	body.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");

		//	//Adding HeaderRow.
		//	body.Append("<tr>");
		//	foreach (DataRow row in dtRows.Rows)
		//	{
		//		body.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + row["DisplayName"] + "</th>");
		//	}
		//	body.Append("</tr>");

		//	int Cnt = 1;
		//	//Adding DataRow.
		//	foreach (DataRow row in dtRows.Rows)
		//	{
		//		body.Append("<tr>");
		//		foreach (DataColumn column in dtCol.Columns)
		//		{
		//			body.Append("<td style='width:100px;border: 1px solid #ccc'>" + row["Ref"+Cnt.ToString()].ToString() + "</td>");
		//			Cnt++;
		//		}
		//		body.Append("</tr>");
		//	}


		//	//Table end.
		//	body.Append("</table>");

		//	body.AppendLine("<br/>");

		//	body.AppendLine("<br/>");

		//	//body.AppendLine("<a href=" + URL + " >Click Here to download file </a>");
		//	body.AppendLine("Regards,");
		//	body.AppendLine("<br/>");
		//	body.AppendLine("DMS Team,");
		//	mailMessage.Body = body.ToString();

		//	//mailMessage.Body = body;


		//	var TempPath = ConfigurationManager.AppSettings["temp"].ToString();
		//	//   var zip = ZipFile.Open(TempPath +"\\MultipleFiles.zip"+ , ZipArchiveMode.Create);

		//	string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
		//	ZipFile zip = new ZipFile(TempPath + "\\" + zipName);
		//	//  zip.Save();


		//	//var objData = ObjSearch.SearchBulkFile(ID, 1, _fileName);

		//	foreach (var file in strMaildDetails)
		//	{
		//		zip.AddFile(file.FilePath, file.FileNo);
		//		// zip.Save();
		//	}
		//	zip.Save();

		//	System.Net.Mail.Attachment attachment;
		//	attachment = new System.Net.Mail.Attachment(TempPath + "\\" + zipName);
		//	mailMessage.Attachments.Add(attachment);

		//	//if (strMaildDetails.IsAttachment == "T")
		//	//{

		//	//}

		//	SmtpClient smtp = new SmtpClient();
		//	smtp.Host = ConfigurationManager.AppSettings["Host"];
		//	//smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
		//	System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
		//	NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
		//	NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
		//	smtp.UseDefaultCredentials = false;
		//	smtp.Credentials = NetworkCred;
		//	smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
		//	smtp.Send(mailMessage);
		//}


		//[HttpPost]
		//[Route("api/Mail/Update")]
		//public HttpResponseMessage Update([FromBody] MailEntity obj)
		//{
		//	try
		//	{
		//		if (ValidateTokenExtension.ValidateToken(obj.User_Token))
		//		{
		//			//if (!string.IsNullOrEmpty(objRepositary.Update(obj)))
		//			//{
		//			//	return Request.CreateResponse(HttpStatusCode.OK, "Branch is updated successfully!!");
		//			//}
		//			//else
		//			//{
		//			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//			//}
		//		}
		//		else
		//			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
		//	}
		//	catch (Exception ex)
		//	{
		//		return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
		//	}
		//}


		//[HttpPost]
		//[Route("api/Mail/Delete")]
		//public HttpResponseMessage Delete([FromBody] MailEntity obj)
		//{
		//	var response = new HttpResponseMessage();
		//	if (ValidateTokenExtension.ValidateToken(obj.User_Token))
		//	{
		//		if (objRepositary.Delete(Convert.ToInt32(obj.id)))
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


		//[HttpGet]
		//[Route("api/Mail/DownloadBulkTagFile")]
		//public HttpResponseMessage DownloadBulkTagFile([FromUri] int ID, int DocID, string _fileName, string user_Token)
		//{
		//	try
		//	{

		//		ISearchFileStatus<SearchFileStatusEntity> ObjTagFiles;

		//		string[] strFiles = _fileName.Split(',');
		//		var TempPath = ConfigurationManager.AppSettings["temp"].ToString();

		//		if (ValidateTokenExtension.ValidateToken(user_Token))
		//		{
		//			string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
		//			ZipFile zip = new ZipFile(TempPath + "\\" + zipName);

		//			foreach (var file in strFiles)
		//			{

		//				string[] _strtagname = file.Split('_');

		//				SearchFileStatusEntity objEntity = new SearchFileStatusEntity();
		//				objEntity.TempPath = TempPath;
		//				objEntity.User_Token = user_Token;
		//				objEntity.ACC = file;
		//				objEntity.DocID = Convert.ToInt32(_strtagname[1]);
		//				objEntity.AccNo = _strtagname[0];
		//				objEntity.CreatedBy = ID;
		//				var objData = ObjTagFiles.DownloadFile(objEntity);

		//				// var objData = objRepositary.SearchBulkFile(ID, 1, _fileName);
		//				zip.AddFile(objData, _strtagname[0]);
		//			}

		//			zip.Save();

		//			using (MemoryStream memoryStream = new MemoryStream())
		//			{
		//				// zip.Save(memoryStream);
		//				//return File(memoryStream.ToArray(), "application/zip", zipName);

		//				HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
		//				byte[] bytes = System.IO.File.ReadAllBytes(TempPath + "\\" + zipName);

		//				result.Content = new ByteArrayContent(bytes);

		//				result.Content.Headers.ContentLength = bytes.LongLength;

		//				var dataStream = new MemoryStream(bytes);
		//				result.Content = new StreamContent(dataStream);
		//				result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
		//				result.Content.Headers.ContentDisposition.FileName = zipName;
		//				result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
		//				return result;
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


		public void ShareFileByAttachment(string FileNo,int UserID,string ToEmailID,bool predefined,string subject,string htmlcontent)
		{ 
			var strMaildDetails = objRepositary.GetBulkFile(FileNo, ToEmailID);
			//LogErrorDetails("DT1");
			
			DataTable dt = getData("EXEC SP_GetMailListByFileNo '" + FileNo + "'");
			//LogErrorDetails(dt.Rows.Count.ToString());
			DataTable dt2 = getData("EXEC GetUserdetails " + UserID + "");
			//LogErrorDetails("DT1");

			//LogErrorDetails(dt2.Rows.Count.ToString());

			string sendername = dt2.Rows[0][0].ToString();
			string senderEmailID = dt2.Rows[0][1].ToString();

			var mailMessage = new MailMessage();

			mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
		//	mailMessage.To.Add(new MailAddress(ToEmailID));

            string[] strfiles = ToEmailID.Split(',');
            			
            for (int i = 0; i < strfiles.Length - 1; i++)
            {
				mailMessage.To.Add(new MailAddress(strfiles[i]));
				//obj.FileNo = strfiles[i];
    //            val = objRepositary.AddEmailDetails(obj);
    //            FIleList += strfiles[i] + ',';
            }


            //foreach (var file in strMaildDetails)
            //{

            //	//zip.AddFile(file.FilePath, file.FileNo);
            //	break;
            //	// zip.Save();
            //}

            //mailMessage.Subject = sendername +" " +" has shared document with you";
			
			mailMessage.IsBodyHtml = true;
			//string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
			// = "Body containing <strong>HTML</strong>";

			//string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

			var body = new System.Text.StringBuilder();
			body.AppendFormat("Hello, {0}\n", "");
			body.AppendLine("<br/>");
			body.AppendFormat(sendername + "  " + "has shared the documents with attachment to you.");

			//body.AppendFormat(sendername + "  " + "has shared the following documents with you. You can access them by clicking the link provided against each document.");


			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine(@"Please reach out to "+ senderEmailID  + "  for any questions you may have. ");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");

			//Adding HeaderRow.
			body.Append("<tr>");
			foreach (DataColumn column in dt.Columns)
			{
				body.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
			}
			body.Append("</tr>");


			//Adding DataRow.
			foreach (DataRow row in dt.Rows)
			{
				body.Append("<tr>");
				foreach (DataColumn column in dt.Columns)
				{
					body.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
				}
				body.Append("</tr>");
			}


			//Table end.
			body.Append("</table>");

			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			body.AppendLine("Do not reply to this email as its mailbox is not monitored by a human or by the sender of this email. ");

			body.AppendLine("<br/>");
			body.AppendLine("<br/>");
			//body.AppendLine("<a href=" + URL + " >Click Here to download file </a>");
			body.AppendLine("Regards,");
			body.AppendLine("<br/>");
			body.AppendLine("DMS Team");
			//mailMessage.Body = body.ToString();

			mailMessage.Subject = sendername + "  " + "  has shared document with you";
			mailMessage.Body = body.ToString();

			//if (predefined == true  && predefined == true)
			//{
			//	mailMessage.Subject = subject;
			//	mailMessage.Body = htmlcontent;
			//}
			//else
			//{
			//	mailMessage.Subject = sendername + "  " + "  has shared document with you";
			//	mailMessage.Body = body.ToString();
			//}

			//mailMessage.Body = body;

			//	var TempPath = ConfigurationManager.AppSettings["temp"].ToString();
			var TempPath = gettemppath(UserID);

			//   var zip = ZipFile.Open(TempPath +"\\MultipleFiles.zip"+ , ZipArchiveMode.Create);
			//string zipName = "Zip_" + Guid.NewGuid().ToString() + ".zip";
			string zipName = "Zip_" + Guid.NewGuid().ToString() + ".zip";

			//string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
			ZipFile zip = new ZipFile(TempPath + "\\" + zipName);
			//  zip.Save();


			//var objData = ObjSearch.SearchBulkFile(ID, 1, _fileName);

			foreach (var file in strMaildDetails)
			{
				//string Filetype =;

				string pathc = Downloadfilefrom_AWS(file.Cloudpath, file.FileNo, file.isEncrypted);
				//string pathc = Downloadfilefrom_AWS(file.Cloudpath, file.FileNo);
				var Fname = Path.Combine(TempPath, file.FileNo + Path.GetExtension(pathc));
				File.Copy(pathc, Fname, true);

				zip.AddFile(Fname,"");
				// zip.Save();
			}
			zip.Save();

			System.Net.Mail.Attachment attachment;
			attachment = new System.Net.Mail.Attachment(TempPath + "\\" + zipName);
			mailMessage.Attachments.Add(attachment);

			//if (strMaildDetails.IsAttachment == "T")
			//{

			//}

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

		private string Downloadfilefrom_AWS(string filePath, string FileNo, string isEncrypted)
		{


			string Filetype = Path.GetExtension(filePath);
			RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
			//  IAmazonS3 client = new AmazonS3Client("AKIA5Y6BUUNWCEG44GQL", "b43GzXnwDzNrGm97RkQ1+8h0hf5BX37dkTd5KR6p", bucketRegion);

			IAmazonS3 client = new AmazonS3Client("AKIA5Y6BUUNWCEG44GQL", "b43GzXnwDzNrGm97RkQ1+8h0hf5BX37dkTd5KR6p", bucketRegion);
			//  string Filetype = ".pdf";
			GetObjectRequest request1 = new GetObjectRequest();
			request1.BucketName = "crown-dms";
			request1.Key = filePath;
			GetObjectResponse response1 = client.GetObject(request1);
			string tmpfilename = ConfigurationManager.AppSettings["temp"].ToString() + "/" + Guid.NewGuid().ToString() + Filetype;
			// Use a temporary file name to download the zip file.
			//string tmpfilename = System.IO.Path.GetTempFileName();
			response1.WriteResponseStreamToFile(tmpfilename);

			if (isEncrypted == "Y")
			{

				string privateKeyPath = ConfigurationManager.AppSettings["keypath"];
				string privateKeyPassword = ConfigurationManager.AppSettings["privateKeyPassword"];
				var publicKeyPath = privateKeyPath + "" + ".public";

				string otmpfilename = ConfigurationManager.AppSettings["temp"].ToString() + "/" + FileNo + Filetype;

				if (!File.Exists(privateKeyPath))
				{
					var keys = AsymmetricProvider.GenerateNewKeyPair(2048);
					AsymmetricProvider.WritePublicKey(publicKeyPath, keys.PublicKey);
					AsymmetricProvider.WritePrivateKey(privateKeyPath, keys.PrivateKey, privateKeyPassword);
				}

				// Encrypt the file

				// Decrypt it again to compare against the source file
				var privateKey = AsymmetricProvider.ReadPrivateKey(privateKeyPath, privateKeyPassword);

				AsymmetricProvider.DecryptFile(tmpfilename, otmpfilename, privateKey);
				return otmpfilename;
			}
			else
			{
				return tmpfilename;
			}
		}




		[HttpGet]
		[Route("api/Mail/DownloadFile")]
		public HttpResponseMessage DownloadFile([FromUri] string RN)
		{
			try
			{
				if (1==1)
				{
					var objData = objRepositary.DownloadFile(RN);

				 // var objData = @"D:\Application\14-Jully-2020\UI\src\assets\A000758829.pdf"; // objRepositary.DownloadFile(objEntity);

					HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
					byte[] bytes = System.IO.File.ReadAllBytes(objData);
					result.Content = new ByteArrayContent(bytes);

					result.Content.Headers.ContentLength = bytes.LongLength;

					var dataStream = new MemoryStream(bytes);
					result.Content = new StreamContent(dataStream);
					result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
					result.Content.Headers.ContentDisposition.FileName = RN + ".pdf";
					result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
					return result;


				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}


		private string gettemppath(int userID)
		{
			var TempPath = Path.Combine(ConfigurationManager.AppSettings["temp"].ToString(), userID.ToString());

			if (!Directory.Exists(TempPath))
			{
				Directory.CreateDirectory(TempPath);
			}
			return TempPath;
		}


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
	}
}
