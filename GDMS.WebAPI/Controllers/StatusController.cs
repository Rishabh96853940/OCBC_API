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
    public class StatusController : ApiController
    {
        IStatus<StatusEntity> objRepositary;
		public StatusController(IStatus<StatusEntity> obj)
		{
			objRepositary = obj;
		}


        [HttpPost]
        [Route("api/Status/GetStatusReport")]
        public HttpResponseMessage GetStatusReport([FromBody]StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.GetStatusReport(obj);
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


        //--------------------------------------------------Newly added below variables----------

        [HttpPost]
        [Route("api/Status/GetItemstatusreportList")]
        public HttpResponseMessage GetList([FromBody] StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                //if (true)
                {
                    var objData = objRepositary.GetItemstatusreportList(obj);
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

        //--------------------------------------------------------------------

        [HttpPost]
        [Route("api/Status/GetEmailLog")]
        public HttpResponseMessage GetEmailLog([FromBody] StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.GetEmailLog(obj);
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
        [Route("api/Status/GetMetaDataReport")]
        public HttpResponseMessage GetMetaDataReport([FromBody]StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.GetMetaDataReport(obj);
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
        [Route("api/Status/GetMetaDataReportByCustomer")]
        public HttpResponseMessage GetMetaDataReportByCustomer([FromBody] StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.GetMetaDataReportByCustomer(obj);
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
        [Route("api/Status/TagStatusReport")]
        public HttpResponseMessage TagStatusReport([FromBody] StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.TagStatusReport(obj);
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
        [Route("api/Status/GetSpaceReport")]
        public HttpResponseMessage GetSpaceReport([FromBody] StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.GetSpaceReport(obj);
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
        [Route("api/Status/GetActivityReport")]
        public HttpResponseMessage GetActivityReport([FromBody]StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.GetActivityReport(obj);
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
        [Route("api/Status/GetDashboardFileData")]
        public HttpResponseMessage GetDashboardFileData(int userID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetDashboardFileData(userID);
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
        [Route("api/Status/GetDashboardData")]
        public HttpResponseMessage GetDashboardData(int userID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetDashboardData(userID);
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
        [Route("api/Status/DownloaddashboardData")]
        public HttpResponseMessage DownloaddashboardData(int userID, string Type, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.DownloaddashboardData(userID, Type);
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
        [Route("api/Status/GetMetaDataReportByFileNo")]
        public HttpResponseMessage GetMetaDataReportByFileNo([FromUri] string FileNo, string user_Token,int UserID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetMetaDataReportByFileNo(FileNo, UserID);
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
        [Route("api/Status/GetMetaDataFileNo")]
        public HttpResponseMessage GetMetaDataFileNo([FromBody] StatusEntity obj)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(obj.User_Token))
                {
                    var objData = objRepositary.GetMetaDataReportByFileNo(obj.ACC, obj.userID);
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
        [Route("api/Status/GetActivityReportByFileNo")]
        public HttpResponseMessage GetActivityReportByFileNo([FromUri] string FileNo, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetActivityReportByFileNo(FileNo);
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
        [Route("api/Status/GetStatusCount")]
        public HttpResponseMessage GetStatusCount([FromUri] string userID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetStatusCount(userID);
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
        [Route("api/Status/GetStatusActivityCountPieCHart")]
        public HttpResponseMessage GetStatusActivityCountPieCHart([FromUri] string FileNo, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetStatusActivityCountPieCHart(FileNo);
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


        //[HttpGet]
        //[Route("api/Status/GetDashboardFileData")]
        //public HttpResponseMessage GetDashboardFileData( string user_Token)
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




        [HttpGet]
        [Route("api/Status/GetActivityCount")]
        public HttpResponseMessage GetActivityCount([FromUri] int userID ,  string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetActivityCount(userID);
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
        [Route("api/Status/GetFileStorageData")]
        public HttpResponseMessage GetFileStorageData([FromUri] int UserID , string FileNo,string parentFileNo, string user_Token,int TemplateID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetFileStorageData(UserID, FileNo, parentFileNo, TemplateID);
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
        [Route("api/Status/GetTreeStructure")]
        public HttpResponseMessage GetTreeStructure([FromUri] int UserID,string user_Token, int TemplateID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetTreeStructure(UserID, TemplateID);
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
        [Route("api/Status/CheckAccessRight")]
        public HttpResponseMessage CheckAccessRight([FromUri] int RoleID, string PageName, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.CheckAccessRight(RoleID, PageName);
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
        [Route("api/Status/GetActivityUserLog")]
        public HttpResponseMessage GetActivityUserLog([FromUri] int userID , string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetActivityUserLog(userID);
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
        [Route("api/Status/GetTemplateIDByFileNo")]
        public HttpResponseMessage GetTemplateIDByFileNo([FromUri] string FileNo, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetTemplateIDByFileNo(FileNo);
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
        [Route("api/Status/GetTreeStructureByGlobalSearch")]
        public HttpResponseMessage GetTreeStructureByGlobalSearch([FromUri] int UserID, string user_Token, int TemplateID)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetTreeStructureByGlobalSearch(UserID, TemplateID);
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
    }
}
