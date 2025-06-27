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
    public class DepartmentMappingController : ApiController
    {
        IMappingchkRepository<DepartmentEntity> objRepositary;
		public DepartmentMappingController(IMappingchkRepository<DepartmentEntity> obj)
		{
			objRepositary = obj;
		}
		[HttpGet]
		[Route("api/DepartmentMapping/GetList")]
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

        //[HttpGet]
        //[Route("api/BranchMapping/GetList")]
        //public HttpResponseMessage GetList([FromUri] int ID, string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.GetBranchDetailsUserWise(ID);
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
        [Route("api/DepartmentMapping/GetDepartmentDetailsUserWise")]
        public HttpResponseMessage GetDepartmentDetailsUserWise([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetBranchDetailsUserWise(ID);
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
		[Route("api/DepartmentMapping/GetDepartmentByUser")]
		public HttpResponseMessage GetDepartmentByUser([FromUri] int ID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetDetailsRegion(ID);
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
		[Route("api/DepartmentMapping/GetDetails")]
		public HttpResponseMessage GetDetails([FromUri] int ID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetDetails(ID);
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
        //[Route("api/BranchMapping/GetDetails")]
        //public HttpResponseMessage GetList([FromUri] int ID, string user_Token)
        //{
        //    try
        //    {
        //        if (ValidateTokenExtension.ValidateToken(user_Token))
        //        {
        //            var objData = objRepositary.Get(ID);
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


        [HttpPost]
		[Route("api/DepartmentMapping/Create")]
		public HttpResponseMessage Post([FromBody] DepartmentEntity objEntity)
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
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
		[HttpPost]
		[Route("api/DepartmentMapping/Update")]
		public HttpResponseMessage Update([FromBody] DepartmentEntity objEntity)
		{
			try
			{

				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					if (!string.IsNullOrEmpty(objRepositary.Update(objEntity)))
					{
						return Request.CreateResponse(HttpStatusCode.OK, "Document is updated successfully!!");
					}
					else
					{
						return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
					}
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
		[Route("api/DepartmentMapping/Delete")]
		public HttpResponseMessage Delete([FromBody] DepartmentEntity objEntity)
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
	}
}
