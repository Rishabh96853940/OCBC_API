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



namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {
  //      IDashboard<DashboardEntity> objRepositary;
		//public DashboardController(IDashboard<DashboardEntity> obj)
		//{
		//	objRepositary = obj;
		//}

        

        //[HttpGet]
        //[Route("api/Dashboard/GetStatusCount")]
        //public HttpResponseMessage GetStatusCount(string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.GetStatusCount("");
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}


        //[HttpGet]
        //[Route("api/Dashboard/GetStatusActivityCountPieCHart")]
        //public HttpResponseMessage GetStatusActivityCountPieCHart([FromUri] string FileNo, string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.GetStatusActivityCountPieCHart(FileNo);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}


        //[HttpGet]
        //[Route("api/Dashboard/GetDashboardData")]
        //public HttpResponseMessage GetDashboardData(int userID, string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.GetDashboardData(0);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}


        //[HttpGet]
        //[Route("api/Dashboard/GetDashboardFileData")]
        //public HttpResponseMessage GetDashboardFileData(int userID , string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.GetDashboardFileData(0);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}


        //[HttpGet]
        //[Route("api/Dashboard/GetActivityCount")]
        //public HttpResponseMessage GetActivityCount(string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.GetActivityCount(0);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}
              

        //[HttpGet]
        //[Route("api/Dashboard/CheckAccessRight")]
        //public HttpResponseMessage CheckAccessRight([FromUri] int RoleID, string PageName, string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.CheckAccessRight(RoleID, PageName);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}


        //[HttpGet]
        //[Route("api/Dashboard/GetActivityUserLog")]
        //public HttpResponseMessage GetActivityUserLog(string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.GetActivityUserLog();
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data not fount!!!");
        //        }
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid token!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
        //    }
        //}


    }
}
