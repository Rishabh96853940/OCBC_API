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

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportController : ApiController
    {
        IReport<ReportEntity> objRepositary;


        private readonly IReport<ReportEntity> _request;
        public ReportController(IReport<ReportEntity> obj)
        {
            objRepositary = obj;
        }

        [HttpPost]
        [Route("api/Report/GetFileInventoryReport")]
        public HttpResponseMessage GetFileInventoryReport([FromBody] ReportEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetFileInventoryReport(objEntity);
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
        [Route("api/Report/GetItemStatusReport")]
        public HttpResponseMessage GetItemStatusReport([FromBody] ReportEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetItemStatusReport(objEntity);
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
        [Route("api/Report/GetFileInventoryReportByDisDate")]
        public HttpResponseMessage GetFileInventoryReportByDisDate([FromBody] ReportEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetFileInventoryReportByDisDate(objEntity);
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
        [Route("api/Report/GetFileInventoryUploadReport")]
        public HttpResponseMessage GetFileInventoryUploadReport([FromBody] ReportEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetFileInventoryUploadReport(objEntity);
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
        [Route("api/Report/GetDumpReport")]
        public HttpResponseMessage GetDumpReport([FromBody] ReportEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    var objData = objRepositary.GetDumpReport(objEntity);
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
    }
}
