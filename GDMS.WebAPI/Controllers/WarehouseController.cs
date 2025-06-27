
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
using Kotak.Repository;
using iText.StyledXmlParser.Jsoup.Nodes;

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WarehouseController : ApiController
    {
        IWarehouse<WarehouseEntity> objRepositary;
        public WarehouseController(IWarehouse<WarehouseEntity> obj)
        {
            objRepositary = obj;
        }


        //[HttpGet]
        //[Route("api/WarehouseController/GetList")]
        //public HttpResponseMessage GetList([FromUri] string user_Token, int userid)
        //{
        //    try
        //    {
        //        // if (ValidateTokenExtension.ValidateToken(user_Token))
        //        if (true)
        //        {
        //            var objData = objRepositary.GetDetails(userid);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
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

        //[Route("api/WarehouseController/GetWarehouseList")]

        //public HttpResponseMessage GetWarehouseList([FromUri] string user_Token, int userid)
        //{
        //    try
        //    {
        //        if (true)
        //        {
        //            var objData = objRepositary.GetWarehouseList(userid);
        //            if (objData != null)
        //                return Request.CreateResponse(HttpStatusCode.OK, objData);
        //            else return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Internal server error"); ;
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
        [Route("api/Warehouse/GetWarehouseLists")]
        public HttpResponseMessage GetWarehouseLists([FromUri] string user_Token)
        {
            try
            {
                // if (ValidateTokenExtension.ValidateToken(user_Token))
                if (true)
                {
                    var objData = objRepositary.GetWarehouseLists();
                    if (objData != null && objData.Any())
                        return Request.CreateResponse(HttpStatusCode.OK, objData);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data found.");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token!!!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }



        [HttpPost]
        [Route("api/Warehouse/Add")]
        public HttpResponseMessage Add([FromUri] string user_Token, [FromBody] WarehouseEntity model)
        {
            try
            {
                // if (ValidateTokenExtension.ValidateToken(user_Token))
                if (true)
                {
                    if (model.userid == null || model.userid == 0)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User ID is required.");

                    var resultMessage = objRepositary.AddWarehouse(model);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = resultMessage });
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
        [Route("api/Warehouse/Update")]
        public HttpResponseMessage Update([FromUri] string user_Token, [FromBody] WarehouseEntity model)
        {
            try
            {
                // if (ValidateTokenExtension.ValidateToken(user_Token))
                if (true)
                {
                    if (model.Id == 0 || model.userid == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID or User ID.");

                    var resultMessage = objRepositary.UpdateWarehouse(model);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = resultMessage });
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
        [Route("api/Warehouse/DeleteWarehouse")]
        public HttpResponseMessage DeleteWarehouse(int id, string User_Token, string userid)
        {
            var response = new HttpResponseMessage();
            if (ValidateTokenExtension.ValidateToken(User_Token))
            {
                if (objRepositary.DeleteWarehouse(id))
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, "Successfully Deleted!!!");
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









    }
}
