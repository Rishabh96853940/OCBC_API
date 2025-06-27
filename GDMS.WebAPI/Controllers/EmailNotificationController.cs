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

namespace AvanseAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailNotificationController : ApiController
    {
        private readonly IEmailNotification<EmailNotificationEntity> _request;
        public EmailNotificationController(IEmailNotification<EmailNotificationEntity> request)
        {
            _request = request;
        }
        [HttpGet]
        [Route("api/EmailNotification/GetEmailNotificationAll")]
        public HttpResponseMessage GetEmailNotificationAll([FromUri]   string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.Get();
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
        [Route("api/EmailNotification/InsertEmailNotification")]
        public HttpResponseMessage InsertEmailNotification([FromBody] EmailNotificationEntity objEntity)
       {
            try
            {
                if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
                {
                    var objData = _request.InsertUpdateEmailNotification(objEntity);
                    if (objData != null)
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error");
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
        //[Route("api/EmailNotificationController/UpdatePickupRequest")]
        //public HttpResponseMessage UpdatePickupRequest([FromBody] EmailNotificationEntity objEntity)
        //{
        //    try
        //    {
        //        if ((ValidateTokenExtension.ValidateToken(objEntity.User_Token)))
        //        {
        //            var objData = _request.UpdatePickupRequest(objEntity);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}


        [HttpGet]
        [Route("api/EmailNotification/GetEmailNotificationAllByID")]
        public HttpResponseMessage GetEmailNotificationAllByID([FromUri] int id, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.Get(id);
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
        [Route("api/EmailNotification/DeleteEmailNotification")]
        public HttpResponseMessage DeleteEmailNotification([FromUri] string id, string user_Token,int userid)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = _request.DeleteEmailNotification(id,userid);
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
    }
}
