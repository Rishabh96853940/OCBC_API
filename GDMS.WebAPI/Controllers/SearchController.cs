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
using System.Configuration;
using System.IO;
using System.Web;
using System.Data;
using System.Net.Mail;
using System.Data.SqlClient;
using Kotak.Entities;
using Kotak.Repository.Interfaces;

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchController : ApiController
    {
        ISearch<SearchEntity> objRepositary;
        public SearchController(ISearch<SearchEntity> obj)
        {
            objRepositary = obj;
        }
        [HttpGet]
        [Route("api/Search/GetList")]
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
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }
        [HttpGet]
        [Route("api/Search/GetDetails")]
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
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        [HttpGet]
        [Route("api/Search/GetBatchDetails")]
        public HttpResponseMessage GetBatchDetails([FromUri] string BatchNo, int USERId, string user_Token)
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
        [Route("api/Search/GetRetrivaldetails")]
        public HttpResponseMessage GetRetrivaldetails([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRetrivaldetails(USERId);
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
        [Route("api/Search/GetBatchClose")]
        public HttpResponseMessage GetBatchClose([FromUri] string request_no, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetBatchClose(request_no);
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
        [Route("api/Search/GetFileDetails")]
        public HttpResponseMessage GetFileDetails([FromUri] string LanNo, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetFileDetails(LanNo, 1);
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

        [HttpPost]
        [Route("api/Search/Create")]
        public HttpResponseMessage Post([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.Add(objEntity);
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
        [Route("api/Search/UpdatePOD")]
        public HttpResponseMessage UpdatePOD([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.UpdatePOD(objEntity);
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
        //[Route("api/Search/UpdaterequestNo")]
        //public HttpResponseMessage UpdaterequestNo([FromBody] RetrivalEntity objEntity)
        //{
        //	try
        //	{
        //		if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
        //		{

        //			string val = objRepositary.UpdaterequestNo(objEntity);
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
        [Route("api/Search/Upload")]
        public HttpResponseMessage Upload()
        {
            SearchEntity objEntity = new SearchEntity();
            var response = new HttpResponseMessage();
            try
            {
                int i = 0;
                int cntSuccess = 0;
                var uploadedFileNames = new List<string>();
                string result = string.Empty;

                string request_no = (HttpContext.Current.Request.Form.GetValues("request_no")[0]);
                string dispatch_address = (HttpContext.Current.Request.Form.GetValues("dispatch_address")[0]);
                string request_type = (HttpContext.Current.Request.Form.GetValues("request_type")[0]);
                int UserID = Convert.ToInt32(HttpContext.Current.Request.Form.GetValues("UserID")[0]);

                //objEntity.DocType = DocID;

                objEntity.request_no = request_no;
                objEntity.request_type = request_type;
                objEntity.dispatch_address = dispatch_address;
                objEntity.CreatedBy = UserID;

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[i];

                        //var TempPath = HttpContext.Current.Server.MapPath("~/temp/" + postedFile.FileName);
                        //var filePath = HttpContext.Current.Server.MapPath("~/Uploads/");

                        string strDate = DateTime.Now.ToString("dd-MM-yyyy");
                        string strpath = Path.Combine(ConfigurationManager.AppSettings["Upload"].ToString(), strDate);

                        if (!Directory.Exists(strpath))
                        {
                            Directory.CreateDirectory(strpath);
                        }

                        var filePath = strpath + "/" + postedFile.FileName; // HttpContext.Current.Server.MapPath("~/temp/" + postedFile.FileName);
                                                                            //	var filePath = ConfigurationManager.AppSettings["Upload"].ToString();   //HttpContext.Current.Server.MapPath("~/Uploads/");

                        try
                        {
                            postedFile.SaveAs(filePath);
                            objEntity.file_path = filePath;
                            objRepositary.UpdaterequestNo(objEntity);
                            //  File.Delete(TempPath);

                            uploadedFileNames.Add(httpRequest.Files[i].FileName);

                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                            throw ex;
                        }

                        i++;
                    }
                }
                else
                {
                    objEntity.file_path = "";
                    objRepositary.UpdaterequestNo(objEntity);
                }
                //result = cntSuccess.ToString() + " files uploaded succesfully.<br/>";

                //result += "<ul>";

                //foreach (var f in uploadedFileNames)
                //{
                //    result += "<li>" + f + "</li>";
                //}

                //result += "</ul>";             

                response = Request.CreateResponse(HttpStatusCode.OK, "File uploaded successfully.");
                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Internal server error");
                return response;
            }
        }

        [HttpPost]
        [Route("api/Search/podentry")]
        public HttpResponseMessage podentry([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.podentry(objEntity);
                    sendMailRetrivealDispatch(objEntity.request_id, objEntity.CreatedBy);

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


        private string sendMailRetrivealDispatch(string request_number, int USERId)
        {

            ///   string service_type = "Pickup Request Accept by Crown";

            DataTable dt = getData("EXEC sp_get_email_data_Retrieval_dispatch '" + request_number + "'," + USERId + "");

            string cc_email_id = string.Empty;
            string subject = string.Empty;
            string Body = string.Empty;
            string CreatedBy = string.Empty;
            string TO_EMAIL_ID = string.Empty;
            string FROM_EMAIL_ID = string.Empty;
            DateTime requestDate = DateTime.Now; // or whatever value you have

            // Formatting the request date in "dd-mm-yyyy" format
            string formattedRequestDate = requestDate.ToString("dd-MM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TO_EMAIL_ID += dt.Rows[i]["TO_EMAIL_ID"].ToString() + ",";
                cc_email_id = dt.Rows[i]["cc_email_id"].ToString();
                CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                subject = dt.Rows[i]["subject"].ToString();
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + request_number + Environment.NewLine +
"Dispatch By = \"" + CreatedBy + "\"" + Environment.NewLine +
"Dispatch Date = \"" + formattedRequestDate + "\"";
                FROM_EMAIL_ID = dt.Rows[i]["FROM_EMAIL_ID"].ToString();

            }

            //  string sendername = dt.Rows[] [0][0].ToString();
            //  string senderEmailID = dt.Rows[0][1].ToString();

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //	mailMessage.To.Add(new MailAddress(ToEmailID));

            string[] strfiles = TO_EMAIL_ID.Split(',');

            for (int i = 0; i < strfiles.Length - 1; i++)
            {
                mailMessage.To.Add(new MailAddress(strfiles[i]));

            }

            string[] str_cc_email_id = cc_email_id.Split(',');

            for (int i = 0; i < str_cc_email_id.Length - 1; i++)
            {
                mailMessage.CC.Add(new MailAddress(str_cc_email_id[i]));

            }

            mailMessage.IsBodyHtml = true;

            ////var mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //mailMessage.To.Add(new MailAddress(TO_EMAIL_ID));
            //mailMessage.CC.Add(new MailAddress(TO_EMAIL_ID));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = false;
            //string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
            // = "Body containing <strong>HTML</strong>";

            //string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

            //var body = new System.Text.StringBuilder();
            //body.AppendFormat(Body);

            mailMessage.Body = Body.ToString();


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
            return "";

        }



        private string sendMailRefillingAck(string request_number, int USERId)
        {

            ///   string service_type = "Pickup Request Accept by Crown";

            DataTable dt = getData("EXEC sp_get_email_data_Refilling_Ack '" + request_number + "'," + USERId + "");

            string cc_email_id = string.Empty;
            string subject = string.Empty;
            string Body = string.Empty;
            string CreatedBy = string.Empty;
            string TO_EMAIL_ID = string.Empty;
            string FROM_EMAIL_ID = string.Empty;
            DateTime requestDate = DateTime.Now; // or whatever value you have

            // Formatting the request date in "dd-mm-yyyy" format
            string formattedRequestDate = requestDate.ToString("dd-MM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TO_EMAIL_ID += dt.Rows[i]["TO_EMAIL_ID"].ToString() + ",";
                cc_email_id = dt.Rows[i]["cc_email_id"].ToString();
                CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                subject = dt.Rows[i]["subject"].ToString();
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + request_number + Environment.NewLine +
"Ack By = \"" + CreatedBy + "\"" + Environment.NewLine +
"Ack Date = \"" + formattedRequestDate + "\"";
                FROM_EMAIL_ID = dt.Rows[i]["FROM_EMAIL_ID"].ToString();

            }

            //  string sendername = dt.Rows[] [0][0].ToString();
            //  string senderEmailID = dt.Rows[0][1].ToString();

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //	mailMessage.To.Add(new MailAddress(ToEmailID));

            string[] strfiles = TO_EMAIL_ID.Split(',');

            for (int i = 0; i < strfiles.Length - 1; i++)
            {
                mailMessage.To.Add(new MailAddress(strfiles[i]));

            }

            string[] str_cc_email_id = cc_email_id.Split(',');

            for (int i = 0; i < str_cc_email_id.Length - 1; i++)
            {
                mailMessage.CC.Add(new MailAddress(str_cc_email_id[i]));

            }

            mailMessage.IsBodyHtml = true;

            ////var mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //mailMessage.To.Add(new MailAddress(TO_EMAIL_ID));
            //mailMessage.CC.Add(new MailAddress(TO_EMAIL_ID));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = false;
            //string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
            // = "Body containing <strong>HTML</strong>";

            //string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

            //var body = new System.Text.StringBuilder();
            //body.AppendFormat(Body);

            mailMessage.Body = Body.ToString();


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
            return "";

        }



        [HttpPost]
        [Route("api/Search/scanCopyUpload")]
        public HttpResponseMessage scanCopyUpload()
        {
            SearchEntity objEntity = new SearchEntity();
            var response = new HttpResponseMessage();
            try
            {
                int i = 0;

                var uploadedFileNames = new List<string>();
                string result = string.Empty;

                string request_no = (HttpContext.Current.Request.Form.GetValues("request_no")[0]);
                int UserID = Convert.ToInt32(HttpContext.Current.Request.Form.GetValues("UserID")[0]);

                //objEntity.DocType = DocID;

                objEntity.request_no = request_no;
                objEntity.CreatedBy = UserID;

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[i];

                        //var TempPath = HttpContext.Current.Server.MapPath("~/temp/" + postedFile.FileName);
                        //var filePath = HttpContext.Current.Server.MapPath("~/Uploads/");

                        string strDate = DateTime.Now.ToString("dd-MM-yyyy");
                        string strpath = Path.Combine(ConfigurationManager.AppSettings["Upload"].ToString(), strDate);

                        if (!Directory.Exists(strpath))
                        {
                            Directory.CreateDirectory(strpath);
                        }

                        var filePath = strpath + "/" + postedFile.FileName; // HttpContext.Current.Server.MapPath("~/temp/" + postedFile.FileName);

                        try
                        {
                            postedFile.SaveAs(filePath);
                            objEntity.file_path = filePath;
                            objEntity.lanno = Path.GetFileNameWithoutExtension(filePath);
                            objRepositary.updateScanCopy(objEntity);
                            //  File.Delete(TempPath);

                            uploadedFileNames.Add(httpRequest.Files[i].FileName);

                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                            throw ex;
                        }

                        i++;
                    }
                }
                else
                {
                    objRepositary.updateScanCopy(objEntity);

                }
                //result = cntSuccess.ToString() + " files uploaded succesfully.<br/>";

                //result += "<ul>";

                //foreach (var f in uploadedFileNames)
                //{
                //    result += "<li>" + f + "</li>";
                //}

                //result += "</ul>";             

                response = Request.CreateResponse(HttpStatusCode.OK, "File uploaded successfully.");
                return response;
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Internal server error");
                return response;
            }
        }

        [HttpPost]
        [Route("api/Search/ACkentry")]
        public HttpResponseMessage ACkentry([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.ACkentry(objEntity);
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
        //[Route("api/Search/Update")]
        //public HttpResponseMessage Update([FromBody] RetrivalEntity objEntity)
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

        [HttpPost]
        [Route("api/Search/Delete")]
        public HttpResponseMessage Delete([FromBody] SearchEntity objEntity)
        {
            var response = new HttpResponseMessage();
            if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
            {
                if (objRepositary.Delete(Convert.ToInt32(objEntity.id)))
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, "Succsessfuly Deleted!!!");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Delete Failed!!!");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid token!!!");
            }
            return response;
        }

        [HttpGet]
        [Route("api/Search/getpoddetails")]
        public HttpResponseMessage getpoddetails([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.getpoddetails(USERId);
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
        [Route("api/Search/RetrievalDashboard")]
        public HttpResponseMessage RetrievalDashboard([FromUri] int USERId, string user_Token, string status)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.RetrievalDashboard(USERId, status);
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
        [Route("api/Search/getRetrivalDispatch")]
        public HttpResponseMessage getRetrivalDispatch([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.getRetrivalDispatch(USERId);
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
        [Route("api/Search/getRetrivalDispatchByRequestno")]
        public HttpResponseMessage getRetrivalDispatchByRequestno([FromUri] int USERId, string request_no, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.getRetrivalDispatchByRequestno(USERId, request_no);
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

        //Ack copy

        [HttpGet]
        [Route("api/Search/GetAck")]
        public HttpResponseMessage GetAck([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetAck(USERId);
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
        [Route("api/Search/getAckByRequestno")]
        public HttpResponseMessage getAckByRequestno([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.getAckByRequestno(USERId);
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
        [Route("api/Search/getAckdetailsbyRequestno")]
        public HttpResponseMessage getAckdetailsbyRequestno([FromUri] int USERId, string request_no, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.getAckdetailsbyRequestno(USERId, request_no);
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
        [Route("api/Search/Getloanclose")]
        public HttpResponseMessage Getloanclose([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.Getloanclose(USERId);
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
        [Route("api/Search/GetloanclosebyrequestNo")]
        public HttpResponseMessage GetloanclosebyrequestNo([FromUri] int USERId, string request_no, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetloanclosebyrequestNo(USERId, request_no);
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
        [Route("api/Search/loanClosureCopyUpload")]
        public HttpResponseMessage loanClosureCopyUpload()
        {
            SearchEntity objEntity = new SearchEntity();
            var response = new HttpResponseMessage();
            try
            {
                int i = 0;
                var uploadedFileNames = new List<string>();
                string result = string.Empty;

                string request_no = (HttpContext.Current.Request.Form.GetValues("request_no")[0]);
                string lanno = (HttpContext.Current.Request.Form.GetValues("lanno")[0]);
                int UserID = Convert.ToInt32(HttpContext.Current.Request.Form.GetValues("UserID")[0]);

                //objEntity.DocType = DocID;

                objEntity.request_no = request_no;
                objEntity.lanno = lanno;
                objEntity.CreatedBy = UserID;

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[i];
                        string strDate = DateTime.Now.ToString("dd-MM-yyyy");
                        string strpath = Path.Combine(ConfigurationManager.AppSettings["Loan"].ToString(), strDate);

                        if (!Directory.Exists(strpath))
                        {
                            Directory.CreateDirectory(strpath);
                        }

                        var filePath = strpath + "/" + postedFile.FileName; // HttpContext.Current.Server.MapPath("~/temp/" + postedFile.FileName);

                        try
                        {
                            postedFile.SaveAs(filePath);
                            objEntity.file_path = filePath;
                            //	objEntity.lanno = Path.GetFileNameWithoutExtension(filePath);
                            objRepositary.updateLoanCopy(objEntity);
                            //  File.Delete(TempPath);
                            uploadedFileNames.Add(httpRequest.Files[i].FileName);
                        }
                        catch (Exception ex)
                        {
                            LogError(ex);
                            throw ex;
                        }

                        i++;
                    }
                }

                response = Request.CreateResponse(HttpStatusCode.OK, "File uploaded successfully.");
                return response;
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Internal server error");
                return response;
            }
        }

        [HttpPost]
        [Route("api/Search/LoancloseCasesentry")]
        public HttpResponseMessage LoancloseCasesentry([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.LoancloseCasesentry(objEntity);
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

        // Search 

        [HttpGet]
        [Route("api/Search/GetDumpdataSearch")]
        public HttpResponseMessage GetDumpdataSearch([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetDumpdataSearch(USERId);
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
        [Route("api/Search/Getbasicsearch")]
        public HttpResponseMessage Getbasicsearch([FromUri] int USERId, string user_Token, string CartonNo)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.Getbasicsearch(USERId, CartonNo);
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
        [Route("api/Search/PickupHistory")]
        public HttpResponseMessage PickupHistory([FromUri] int USERId, string user_Token, string File_No)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.PickupHistory(USERId, File_No);
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
        [Route("api/Search/podentrydump")]
        public HttpResponseMessage podentrydump([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.podentrydump(objEntity);
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


        [HttpGet]
        [Route("api/Search/SearchRecordsByFilter")]
        public HttpResponseMessage SearchRecordsByFilter([FromUri] int USERId, string user_Token, string FileNo, int SearchBy)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.SearchRecordsByFilter(USERId, FileNo, SearchBy);
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
        [Route("api/Search/BasicSearchRecordsByFilter")]
        public HttpResponseMessage BasicSearchRecordsByFilter([FromUri] int USERId, string user_Token, string FileNo, int SearchBy)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.BasicSearchRecordsByFilter(USERId, FileNo, SearchBy);
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

        // Reffiling  

        [HttpGet]
        [Route("api/Search/GetRefilingdata")]
        public HttpResponseMessage GetRefilingdata([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRefilingdata(USERId);
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
        [Route("api/Search/AddRefilingRequest")]
        public HttpResponseMessage Refillingfileentry([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.AddRefilingRequest(objEntity);
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

        [HttpGet]
        [Route("api/Search/GetRefillingAck")]
        public HttpResponseMessage GetRefillingAck([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRefillingAck(USERId);
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
        [Route("api/Search/RefillingAckentry")]
        public HttpResponseMessage RefillingAckentry([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.RefillingAckentry(objEntity);
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

        [HttpGet]
        [Route("api/Search/DownloadFileFromDB")]
        public HttpResponseMessage DownloadFileFromDB([FromUri] int ID, string file_path, string user_Token)
        {
            try
            {

                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    var objData = file_path;
                    string Filetype = Path.GetExtension(objData);
                    //  var objData = @"D:\Application\14April2020\A000758829.pdf"; // objRepositary.DownloadFile(objEntity);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    byte[] bytes = System.IO.File.ReadAllBytes(objData);
                    result.Content = new ByteArrayContent(bytes);

                    result.Content.Headers.ContentLength = bytes.LongLength;

                    var dataStream = new MemoryStream(bytes);
                    result.Content = new StreamContent(dataStream);
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "123123" + Filetype;
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

        [HttpGet]
        [Route("api/Search/DownlaodAttachment")]
        public HttpResponseMessage DownlaodAttachment([FromUri] int ID, string file_path, string user_Token)
        {
            try
            {

                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    var objData = file_path;
                    string Filetype = Path.GetExtension(objData);
                    //  var objData = @"D:\Application\14April2020\A000758829.pdf"; // objRepositary.DownloadFile(objEntity);

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    byte[] bytes = System.IO.File.ReadAllBytes(objData);
                    result.Content = new ByteArrayContent(bytes);

                    result.Content.Headers.ContentLength = bytes.LongLength;

                    var dataStream = new MemoryStream(bytes);
                    result.Content = new StreamContent(dataStream);
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "123123" + Filetype;
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

        [HttpGet]
        [Route("api/Search/GetReffilingRequestNo")]
        public HttpResponseMessage GetReffilingRequestNo([FromUri] string request_no, int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetReffilingRequestNo(request_no, USERId);
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
        [Route("api/Search/AckAndGetReffilingRequestBYRequestNo")]
        public HttpResponseMessage AckAndGetReffilingRequestBYRequestNo([FromUri] string request_no, int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.AckAndGetReffilingRequestBYRequestNo(request_no, USERId);
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
        [Route("api/Search/RefillingAckItemNumber")]
        public HttpResponseMessage RefillingAckItemNumber([FromUri] string item_number, string request_no, int USERId, string user_Token, string workorder_number)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.RefillingAckItemNumber(request_no, USERId, item_number, workorder_number);
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
        [Route("api/Search/GetBarcode")]
        public HttpResponseMessage GetBarcode([FromUri] string FileBarcode, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetBarcode(FileBarcode);
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
        [Route("api/Search/RefRequestUdpate")]
        public HttpResponseMessage RefRequestUdpate([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.RefRequestUdpate(objEntity);
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
        [Route("api/Search/RefRequestUdpatePODNumber")]
        public HttpResponseMessage RefRequestUdpatePODNumber([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.RefRequestUdpatePODNumber(objEntity);
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


        [HttpGet]
        [Route("api/Search/getRetrivalForUdpatestatus")]
        public HttpResponseMessage getRetrivalForUdpatestatus([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.getRetrivalForUdpatestatus(USERId);
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

        //[HttpPost]
        //[Route("api/Search/UpdateStatus")]
        //public HttpResponseMessage UpdateStatus([FromBody] RetrivalEntity objEntity)
        //{
        //	try
        //	{
        //		if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
        //		{
        //
        //			string val = objRepositary.UpdateStatus(objEntity);
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



        [HttpGet]
        [Route("api/Search/getRetrivalByRequestno")]
        public HttpResponseMessage getRetrivalByRequestno([FromUri] int USERId, string request_no, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.getRetrivalByRequestno(USERId, request_no);
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
        [Route("api/Search/GetRefillingReport")]
        public HttpResponseMessage GetRefillingReport([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetRefillingReport(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");


                    //string val = objRepositary.GetRetrievalReport(objEntity);
                    //return Request.CreateResponse(HttpStatusCode.OK, val);
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
        [Route("api/Search/Getoutreport")]
        public HttpResponseMessage Getoutreport([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.Getoutreport(objEntity);
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
        [Route("api/Search/getdumpsearch")]
        public HttpResponseMessage getdumpsearch([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.getdumpsearch(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");


                    //string val = objRepositary.GetRetrievalReport(objEntity);
                    //return Request.CreateResponse(HttpStatusCode.OK, val);
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
        [Route("api/Search/AddRetrivalRequestDetails")]
        public HttpResponseMessage AddRetrivalRequestDetails([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.AddRetrivalRequest(objEntity);
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
        [Route("api/Search/UpdateStatus")]
        public HttpResponseMessage UpdateStatus([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.UpdateStatus(objEntity);
                    sendMailRetrivealApproval(objEntity.request_id, objEntity.CreatedBy);
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


        public string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        [HttpGet]
        [Route("api/Search/ClosedRetrivalRequest")]
        public HttpResponseMessage ClosedRetrivalRequest([FromUri] int USERId, string request_number, string user_Token, string dispatch_address)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    string val = objRepositary.ClosedRetrivalRequest(request_number, USERId, dispatch_address);
                    sendMailRetrivealRequest(request_number, USERId);
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

        private string sendMailRetrivealRequest(string request_number, int USERId)
        {

            string service_type = "Retrieval Request by Avanse";

            DataTable dt = getData("EXEC sp_get_email_data_Crown_for_retreival '" + service_type + "'," + 0 + ",'" + request_number + "'");

            string cc_email_id = string.Empty;
            string subject = string.Empty;
            string Body = string.Empty;
            string TO_EMAIL_ID = string.Empty;
            string CreatedBy = string.Empty;
            string FROM_EMAIL_ID = string.Empty;
            DateTime requestDate = DateTime.Now; // or whatever value you have

            // Formatting the request date in "dd-mm-yyyy" format
            string formattedRequestDate = requestDate.ToString("dd-MM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TO_EMAIL_ID += dt.Rows[i]["TO_EMAIL_ID"].ToString() + ",";
                cc_email_id = dt.Rows[i]["cc_email_id"].ToString();
                CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                subject = dt.Rows[i]["subject"].ToString();
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + request_number + Environment.NewLine +
"Request By = \"" + CreatedBy + "\"" + Environment.NewLine +
"Request Date = \"" + formattedRequestDate + "\"";
                FROM_EMAIL_ID = dt.Rows[i]["FROM_EMAIL_ID"].ToString();

            }

            //  string sendername = dt.Rows[] [0][0].ToString();
            //  string senderEmailID = dt.Rows[0][1].ToString();

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //	mailMessage.To.Add(new MailAddress(ToEmailID));

            string[] strfiles = TO_EMAIL_ID.Split(',');

            for (int i = 0; i < strfiles.Length - 1; i++)
            {
                mailMessage.To.Add(new MailAddress(strfiles[i]));

            }

            string[] str_cc_email_id = cc_email_id.Split(',');

            for (int i = 0; i < str_cc_email_id.Length - 1; i++)
            {
                mailMessage.CC.Add(new MailAddress(str_cc_email_id[i]));

            }

            mailMessage.IsBodyHtml = true;




            ////var mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //mailMessage.To.Add(new MailAddress(TO_EMAIL_ID));
            //mailMessage.CC.Add(new MailAddress(TO_EMAIL_ID));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = false;
            //string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
            // = "Body containing <strong>HTML</strong>";

            //string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

            //var body = new System.Text.StringBuilder();
            //body.AppendFormat(Body);

            mailMessage.Body = Body.ToString();


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



            return "";

        }

        private string sendMailRefillingRequest(string request_number, int USERId)
        {

            string service_type = "Refilling Request by Avanse";

            DataTable dt = getData("EXEC sp_get_email_data_Crown_for_refilling '" + service_type + "'," + 0 + ",'" + request_number + "'");

            string cc_email_id = string.Empty;
            string subject = string.Empty;
            string Body = string.Empty;
            string CreatedBy = string.Empty;
            string TO_EMAIL_ID = string.Empty;
            string FROM_EMAIL_ID = string.Empty;
            DateTime requestDate = DateTime.Now; // or whatever value you have

            // Formatting the request date in "dd-mm-yyyy" format
            string formattedRequestDate = requestDate.ToString("dd-MM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TO_EMAIL_ID += dt.Rows[i]["TO_EMAIL_ID"].ToString() + ",";
                cc_email_id = dt.Rows[i]["cc_email_id"].ToString();
                subject = dt.Rows[i]["subject"].ToString();
                CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                subject = dt.Rows[i]["subject"].ToString();
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + request_number + Environment.NewLine +
"Request By = \"" + CreatedBy + "\"" + Environment.NewLine +
"Request Date = \"" + formattedRequestDate + "\"";
                FROM_EMAIL_ID = dt.Rows[i]["FROM_EMAIL_ID"].ToString();

            }

            //  string sendername = dt.Rows[] [0][0].ToString();
            //  string senderEmailID = dt.Rows[0][1].ToString();

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //	mailMessage.To.Add(new MailAddress(ToEmailID));

            string[] strfiles = TO_EMAIL_ID.Split(',');

            for (int i = 0; i < strfiles.Length - 1; i++)
            {
                mailMessage.To.Add(new MailAddress(strfiles[i]));

            }

            string[] str_cc_email_id = cc_email_id.Split(',');

            for (int i = 0; i < str_cc_email_id.Length - 1; i++)
            {
                mailMessage.CC.Add(new MailAddress(str_cc_email_id[i]));

            }

            mailMessage.IsBodyHtml = true;




            ////var mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //mailMessage.To.Add(new MailAddress(TO_EMAIL_ID));
            //mailMessage.CC.Add(new MailAddress(TO_EMAIL_ID));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = false;
            //string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
            // = "Body containing <strong>HTML</strong>";

            //string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

            //var body = new System.Text.StringBuilder();
            //body.AppendFormat(Body);

            mailMessage.Body = Body.ToString();


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



            return "";

        }

        private string sendMailRetrivealApproval(string request_number, int USERId)
        {

            ///   string service_type = "Pickup Request Accept by Crown";

            DataTable dt = getData("EXEC sp_get_email_data_Retrieval_ack '" + request_number + "'," + USERId + "");

            string cc_email_id = string.Empty;
            string subject = string.Empty;
            string Body = string.Empty;
            string CreatedBy = string.Empty;
            string TO_EMAIL_ID = string.Empty;
            string FROM_EMAIL_ID = string.Empty;
            DateTime requestDate = DateTime.Now; // or whatever value you have

            // Formatting the request date in "dd-mm-yyyy" format
            string formattedRequestDate = requestDate.ToString("dd-MM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TO_EMAIL_ID += dt.Rows[i]["TO_EMAIL_ID"].ToString() + ",";
                cc_email_id = dt.Rows[i]["cc_email_id"].ToString();
                CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                subject = dt.Rows[i]["subject"].ToString();
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + request_number + Environment.NewLine +
"Ack By = \"" + CreatedBy + "\"" + Environment.NewLine +
"Ack Date = \"" + formattedRequestDate + "\"";
                FROM_EMAIL_ID = dt.Rows[i]["FROM_EMAIL_ID"].ToString();

            }

            //  string sendername = dt.Rows[] [0][0].ToString();
            //  string senderEmailID = dt.Rows[0][1].ToString();

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //	mailMessage.To.Add(new MailAddress(ToEmailID));

            string[] strfiles = TO_EMAIL_ID.Split(',');

            for (int i = 0; i < strfiles.Length - 1; i++)
            {
                mailMessage.To.Add(new MailAddress(strfiles[i]));

            }

            string[] str_cc_email_id = cc_email_id.Split(',');

            for (int i = 0; i < str_cc_email_id.Length - 1; i++)
            {
                mailMessage.CC.Add(new MailAddress(str_cc_email_id[i]));

            }

            mailMessage.IsBodyHtml = true;

            ////var mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            //mailMessage.To.Add(new MailAddress(TO_EMAIL_ID));
            //mailMessage.CC.Add(new MailAddress(TO_EMAIL_ID));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = false;
            //string URL = ConfigurationManager.AppSettings["URL"] + strMaildDetails.RandomCode;
            // = "Body containing <strong>HTML</strong>";

            //string body = @"Hello, \n To Download File \n <a href = "+ URL + "> Click Here </a>";

            //var body = new System.Text.StringBuilder();
            //body.AppendFormat(Body);

            mailMessage.Body = Body.ToString();


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
            return "";

        }

        [HttpGet]
        [Route("api/Search/ApprovalRequestPending")]
        public HttpResponseMessage ApprovalRequestPending([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    var val = objRepositary.ApprovalRequestPending(USERId);
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


        [HttpGet]
        [Route("api/Search/GetApprovalByRequestNo")]
        public HttpResponseMessage GetApprovalByRequestNo([FromUri] int USERId, string user_Token, string request_number)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    var val = objRepositary.GetApprovalByRequestNo(USERId, request_number);
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





        [HttpGet]
        [Route("api/Search/GetRequestNumber")]
        public HttpResponseMessage GetRequestNumber([FromUri] int USERId, string request_number, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRequestNumber(USERId, request_number);
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
        [Route("api/Search/GetDataByRequestNumber")]
        public HttpResponseMessage GetDataByRequestNumber([FromUri] int USERId, string request_number, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetDataByRequestNumber(USERId, request_number);
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
        [Route("api/Search/GetRetrivalRequestNo")]
        public HttpResponseMessage GetRetrivalRequestNo([FromUri] int userId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetAvanceRetrivalRequestNo(userId);
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
        [Route("api/Search/GetRefillingRequest")]
        public HttpResponseMessage GetRefilingRequest([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRefilingRequest(USERId);
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
        [Route("api/Search/GetRetrivalRequest")]
        public HttpResponseMessage GetRetrivalRequest([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRetrivalRequest(USERId);
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
        [Route("api/Search/GetRefilingRequestDataByRequestNo")]
        public HttpResponseMessage GetRefilingRequestDataByRequestNo([FromUri] int USERId, string request_no, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRefilingRequestDataByRequestNo(USERId, request_no);
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
        [Route("api/Search/CheckRefilingRequestByItemNo")]
        public HttpResponseMessage CheckRefilingRequestByItemNo([FromUri] int USERId, string item_no, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRefilingRequestDataByRequestNo(USERId, item_no);
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
        [Route("api/Search/CloseRefilingRequest")]
        public HttpResponseMessage CloseRefilingRequest([FromUri] int USERId, string request_number, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    string val = objRepositary.CloseRefilingRequest(request_number, USERId);
                    sendMailRefillingRequest(request_number, USERId);
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


        [HttpGet]
        [Route("api/Search/GetRefilingRequestACK")]
        public HttpResponseMessage GetRefilingRequestACK([FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRefilingRequestACK(USERId);
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
        [Route("api/Search/GetRefilingRequestACKByRequestNo")]
        public HttpResponseMessage GetRefilingRequestACKByRequestNo([FromUri] int USERId, string user_Token, string request_no)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRefilingRequestACKByRequestNo(USERId, request_no);
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
        [Route("api/Search/AddPickupRequest")]
        public HttpResponseMessage AddPickupRequest([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.AddPickupRequest(objEntity);
                    sendMailRefillingAck(objEntity.request_id, objEntity.CreatedBy);
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
        [Route("api/Report/GetRetrievalReport")]
        public HttpResponseMessage GetRetrievalReport([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {
                    var objData = objRepositary.GetRetrievalReport(objEntity);
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
        [Route("api/Search/GetRetrivalPprooval")]
        public HttpResponseMessage GetRetrivalPprooval([FromUri] string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    var val = objRepositary.GetRetrivalPprooval(1);
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
        [Route("api/Search/AddEditApprovalDetails")]
        public HttpResponseMessage AddEditApprovalDetails([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {
                    var objData = objRepositary.AddEditApprovalDetails(objEntity);
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
        [Route("api/Search/DeleteApproval")]
        public HttpResponseMessage DeleteApproval([FromBody] SearchEntity objEntity)
        {
            var response = new HttpResponseMessage();
            if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
            {
                if (objRepositary.DeleteApproval(Convert.ToInt32(objEntity.id)))
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, "Succsessfuly Deleted!!!");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Delete Failed!!!");
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid token!!!");
            }
            return response;
        }



        [HttpGet]
        [Route("api/Search/GetApprovalUser")]
        public HttpResponseMessage GetApprovalUser([FromUri] string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {

                    var val = objRepositary.GetApprovalUser(1);
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


        [HttpGet]
        [Route("api/Search/RetrievalDashboard")]
        public HttpResponseMessage RetrievalDashboard([FromUri] int USERId, string user_Token, string status, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.RetrievalDashboard(USERId, status, timeperiod, fromDate, toDate);
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
        [Route("api/Search/refilingdumpupload")]
        public HttpResponseMessage refilingdumpupload([FromBody] SearchEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.refilingdumpupload(objEntity);
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

        [HttpGet]
        [Route("api/Search/GetLanDetailsQuickSearch")]
        public HttpResponseMessage GetLanDetailsQuickSearch([FromUri] int USERId, string LanNo, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetLanDetailsQuickSearch(USERId, LanNo);
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

    }
}
