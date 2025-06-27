using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.WebAPI.Extensions;
using System.Web.Http.Cors;
using Kotak.Repository;

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InventoryDoneController : ApiController
    {
        Iinventory<InventoryEntity> objRepositary;
        public InventoryDoneController(Iinventory<InventoryEntity> obj)
        {
            objRepositary = obj;
        }

        [HttpGet]
        [Route("api/InventoryDone/GetList")]
        public HttpResponseMessage GetList([FromUri] string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                //if (true)
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



    }
}