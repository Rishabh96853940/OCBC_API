using Kotak.Entities;
using Kotak.WebAPI.Extensions;
using Kotak.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Kotak.WebAPI.Extensions;
using System.Net.Mail;
using static System.Net.WebRequestMethods;
using System.Data;
using System.Data.SqlClient;
using Org.BouncyCastle.Asn1.Ocsp;

namespace AvanseAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AvansePickupRequestController : ApiController
    {
        private readonly IAvansePickupRequest<AvansePickupRequestEntity> _request;
        public AvansePickupRequestController(IAvansePickupRequest<AvansePickupRequestEntity> request)
        {
            _request = request;
        }


        [HttpGet]
        [Route("api/AvansePickupRequest/GetAvansePickupRequestAllDeatils")]
        public HttpResponseMessage GetAvansePickupRequestAllDeatils([FromUri] string user_Token, int UserID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.Get(UserID, UserID);
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

        [HttpPost]
        [Route("api/AvansePickupRequest/InsertUpdatePickupRequest")]
        public HttpResponseMessage InsertUpdatePickupRequest([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.InsertUpdatePickupRequest(objEntity);

                    sendMailPickUpRequest(objEntity.branch_id, objEntity);

                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        private string sendMailPickUpRequest(int branch_code, AvansePickupRequestEntity objEntity)
        {

            string service_type = "Pickup Request Raise";

            DataTable dt = getData("EXEC sp_get_email_data_Crown '" + service_type + "'," + branch_code + ",'" + objEntity.request_id + "'");

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
                subject = dt.Rows[i]["subject"].ToString()+"-"+ objEntity.request_id;
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + objEntity.request_id + Environment.NewLine +
                "Request By = \"" + CreatedBy + "\"" + Environment.NewLine +
                "Request Date = \"" + formattedRequestDate + "\""; ;
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
        private string sendMailPickUpACKRequest(int user_id, AvansePickupRequestEntity objEntity)
        {

            ///   string service_type = "Pickup Request Accept by Crown";

            DataTable dt = getData("EXEC sp_get_email_data_pickup_ack '" + objEntity.request_id + "'," + user_id + "");

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
                subject = dt.Rows[i]["subject"].ToString();
                CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + objEntity.request_id + Environment.NewLine +
        "Acknowledge By = \"" + CreatedBy + "\"" + Environment.NewLine +
        "Acknowledge Date = \"" + formattedRequestDate + "\""; ;
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

        private string sendMailPickUpACKRequestSchedule(int user_id, AvansePickupRequestEntity objEntity)
        {

            string service_type = "Schedule Pickup by Crown";

            DataTable dt = getData("EXEC SP_get_email_data_pickup_Schedule'" + objEntity.request_id + "'," + user_id);

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
                Body = dt.Rows[i]["Body"].ToString();
                Body = dt.Rows[i]["Body"].ToString() + Environment.NewLine + "Request Number = " + objEntity.request_id + Environment.NewLine +
"Schedule By = \"" + CreatedBy + "\"" + Environment.NewLine +
"Schedule Date = \"" + formattedRequestDate + "\""; ;
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
        [Route("api/AvansePickupRequest/GetPickupRequestHistory")]
        public HttpResponseMessage GetPickupRequestHistory(string request_no, [FromUri] int USERId, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetPickupRequestHistory(request_no, USERId);
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
        [Route("api/AvansePickupRequest/Dashboard")]
        public HttpResponseMessage GetPickupRequestDashboardData([FromUri] int USERId, string user_Token, string status, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null, string monthyear = null)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetPickupRequestDashboardData(USERId, status, timeperiod, fromDate, toDate, monthyear);
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
        [Route("api/AvansePickupRequest/DashboardCount")]
        public HttpResponseMessage GetPickupRequestDashboardDataCount([FromUri] int USERId, string user_Token, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null, string monthyear = null)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetPickupRequestDashboardDataCount(USERId, timeperiod, fromDate, toDate, monthyear);
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
        [Route("api/AvansePickupRequest/UpdatePickupRequest")]
        public HttpResponseMessage UpdatePickupRequest([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.UpdatePickupRequest(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/AvansePickupRequest/UpdateClosedRequest")]
        public HttpResponseMessage UpdateClosedRequest([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.UpdateClosedRequest(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        [HttpGet]
        [Route("api/AvansePickupRequest/GetAvansePickupRequestDeatilsById")]
        public HttpResponseMessage GetAvansePickupRequestDeatilsById([FromUri] int request_id, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.Get(request_id);
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
        [Route("api/AvansePickupRequest/DeletePickupRequest")]
        public HttpResponseMessage DeletePickupRequest([FromUri] string request_id, string user_Token, int userid)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.DeletePickupRequest(request_id, userid);
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
        [Route("api/AvansePickupRequest/GetAvansePickupRequestByACK")]
        public HttpResponseMessage GetAvansePickupRequestByACK([FromUri] string user_Token,int UserID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetAvansePickupRequestByACK(UserID);
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

        [HttpPost]
        [Route("api/AvansePickupRequest/UpdatePickupRequestACK")]
        public HttpResponseMessage UpdatePickupRequestACK([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.UpdatePickupRequestACK(objEntity);

                    sendMailPickUpACKRequest(objEntity.userid, objEntity);

                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/AvansePickupRequest/AddEditPickupRequestSchedule")]
        public HttpResponseMessage AddEditPickupRequestSchedule([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.AddEditPickupRequestSchedule(objEntity);
                    sendMailPickUpACKRequestSchedule(objEntity.userid, objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        [HttpGet]
        [Route("api/AvansePickupRequest/GetPickupRequestSchedule")]
        public HttpResponseMessage GetPickupRequestSchedule([FromUri] int userid, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetPickupRequestSchedule(userid);
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

        [HttpGet]
        [Route("api/AvansePickupRequest/GetFileInventory")]
        public HttpResponseMessage GetFileInventory([FromUri] string user_Token,int userid)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetFileInventory(userid);
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
        [Route("api/AvansePickupRequest/GetFileInventoryQC")]
        public HttpResponseMessage GetFileInventoryQC([FromUri] int userid, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetFileInventoryQC(userid);
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
        [Route("api/AvansePickupRequest/GetFileInwardByRequestId")]
        public HttpResponseMessage GetFileInwardByRequestId([FromUri] string request_id, int userid, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetFileInwardByRequestId(request_id, userid);
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
        [Route("api/AvansePickupRequest/GetFileInwardByLaNo")]
        public HttpResponseMessage GetFileInwardByLaNo([FromUri] string LanNo, string request_no, string user_Token, string document_type, string service_type, int UserID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetFileInwardByLaNo(LanNo, request_no, document_type, service_type, UserID);
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
        [Route("api/AvansePickupRequest/GetFileInventoryByCartonNo")]
        public HttpResponseMessage GetFileInventoryByCartonNo([FromUri] string request_id, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetFileInventoryByCartonNo(request_id);
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
        [Route("api/AvansePickupRequest/GetFileInventoryByRequestNo")]
        public HttpResponseMessage GetFileInventoryByRequestNo([FromUri] string request_id, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetFileInventoryByRequestNo(request_id);
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
        [Route("api/AvansePickupRequest/AddEditFileInventory")]
        public HttpResponseMessage AddEditFileInventory([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.AddEditFileInventory(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }


        [HttpPost]
        [Route("api/AvansePickupRequest/AddEditFileInventoryQC")]
        public HttpResponseMessage AddEditFileInventoryQC([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.AddEditFileInventoryQC(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }



        [HttpGet]
        [Route("api/AvansePickupRequest/GetFileInventoryByLanNo")]
        public HttpResponseMessage GetFileInventoryByLanNo([FromUri] string LanNo, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetFileInventoryByLanNo(LanNo);
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
        [Route("api/AvansePickupRequest/UpdatePackCarton")]
        public HttpResponseMessage UpdatePackCarton([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.UpdatePackCarton(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/AvansePickupRequest/CloseRequest")]
        public HttpResponseMessage CloseRequest([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.CloseRequest(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }
        [HttpGet]
        [Route("api/AvansePickupRequest/GetAvancePickUpRequestNo")]
        public HttpResponseMessage GetAvancePickUpRequestNo([FromUri] int userId, string user_Token, string request_no)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetAvancePickUpRequestNo(userId, request_no);
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
        [Route("api/Refilling/GetAvanceRefillingRequestNumber")]
        public HttpResponseMessage GetAvanceRefillingRequestNumber([FromUri] int userId, string user_Token, string request_no)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.GetAvanceRefillingRequestNumber(userId, request_no);
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
        [Route("api/Refilling/GetCartonNoByRequestId")]
        public HttpResponseMessage GetCartonNoByRequestId([FromUri] int userId, string user_Token, string request_no)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    string objData = _request.GetCartonNoByRequestId(userId, request_no);
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
        [Route("api/AvansePickupRequest/GetPickupRequestReport")]
        public HttpResponseMessage GetPickupRequestReport([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = _request.GetPickupRequestReport(objEntity);
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
        [Route("api/AvansePickupRequest/AddPickedUpDate")]
        public HttpResponseMessage AddPickedUpDate([FromBody] AvansePickupRequestEntity objEntity)
        {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.AddPickedUpDate(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }


        //[HttpPost]
        //[Route("api/AvansePickupRequest/GetFileInventoryReport")]
        //public HttpResponseMessage GetFileInventoryReport([FromBody] AvansePickupRequestEntity objEntity)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
        //        {

        //            var objData = _request.GetFileInventoryReport(objEntity);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");


        //            //string val = objRepositary.GetRetrievalReport(objEntity);
        //            //return Request.CreateResponse(HttpStatusCode.OK, val);
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError(ex);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
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

    }
}
