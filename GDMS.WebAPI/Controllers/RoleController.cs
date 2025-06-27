using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.WebAPI.Extensions;
using System.Web.Http.Cors;
using System.Configuration;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
	{
        IRoleRepository<RoleEntity> objRepositary;
		public RoleController(IRoleRepository<RoleEntity> obj)
		{
			objRepositary = obj;
		}
		[HttpGet]
		[Route("api/Role/GetList")]
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
		[Route("api/Role/GetDetails")]
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
		[HttpPost]
		[Route("api/Role/Create")]
		public HttpResponseMessage Post([FromBody]RoleEntity objEntity)
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
        [Route("api/Role/Update")]
        public HttpResponseMessage Update([FromBody] RoleEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {
                    string val = objRepositary.Update(objEntity);
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
		[Route("api/Role/Delete")]
		public HttpResponseMessage Delete([FromBody] RoleEntity objEntity)
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
        [Route("api/Role/GetPageList")]
        public HttpResponseMessage GetPageList([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetPageList(ID);
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
        [Route("api/Role/GetPageListByYserwise")]
        public HttpResponseMessage GetPageListByYserwise([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetPageListByYserwise(ID);
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
        [Route("api/Role/GetRightList")]
        public HttpResponseMessage GetRightList([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetRightList(ID);
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
