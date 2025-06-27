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
    public class BranchMappingController : ApiController
    {
        IMappingchkRepository<BranchMappingEntity> objRepositary;
		public BranchMappingController(IMappingchkRepository<BranchMappingEntity> obj)
		{
			objRepositary = obj;
		}
		[HttpGet]
		[Route("api/BranchMapping/GetList")]
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
        [Route("api/BranchMapping/GetBranchDetailsUserWise")]
        public HttpResponseMessage GetBranchDetailsUserWise([FromUri] int ID, string user_Token)
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
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
            }
        }


        [HttpGet]
        [Route("api/BranchMapping/GetCrownBranchDetailsUserWise")]
        public HttpResponseMessage GetCrownBranchDetailsUserWise([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetCrownBranchDetailsUserWise(ID);
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
		[Route("api/BranchMapping/GetBranchByDeptUserWise")]
		public HttpResponseMessage GetBranchByDeptUserWise([FromUri] int ID, string user_Token,int DeptID)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetBranchByDeptUserWise(ID, DeptID);
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
		[Route("api/BranchMapping/GetBranchDetailsRegionWise")]
		public HttpResponseMessage GetBranchDetailsRegionWise([FromUri] int ID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetBranchDetailsRegionWise(ID);
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
		[Route("api/BranchMapping/GetDetails")]
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
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}



        [HttpGet]
        [Route("api/BranchMapping/GetCrownDetails")]
        public HttpResponseMessage GetCrownDetails([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetCrownDetails(ID);
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
		[Route("api/BranchMapping/GetDetailsRegion")]
		
		
		public HttpResponseMessage GetDetailsRegion([FromUri] int ID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var objData = objRepositary.GetDetailsRegion(ID);
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
        [Route("api/BranchMapping/GetCrownDetailsRegion")]
        public HttpResponseMessage GetCrownDetailsRegion([FromUri] int ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetCrownDetailsRegion(ID);
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
		[Route("api/BranchMapping/Create")]
		public HttpResponseMessage Post([FromBody]BranchMappingEntity objEntity)
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
        [Route("api/BranchMapping/CrownCreate")]
        public HttpResponseMessage CrownCreate([FromBody] BranchMappingEntity objEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
                {

                    string val = objRepositary.CrownAdd(objEntity);
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
		[Route("api/BranchMapping/regionmappingCreate")]
		public HttpResponseMessage regionmappingCreate([FromBody] BranchMappingEntity objEntity)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
				{

					string val = objRepositary.regionmappingCreate(objEntity);
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
		[Route("api/BranchMapping/Update")]
		public HttpResponseMessage Update([FromBody]BranchMappingEntity objEntity)
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
				LogError(ex);
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Internal server error");
			}
		}
		[HttpPost]
		[Route("api/BranchMapping/Delete")]
		public HttpResponseMessage Delete([FromBody]BranchMappingEntity objEntity)
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
        [HttpPost]
        [Route("api/BranchMapping/CrownDelete")]
        public HttpResponseMessage CrownDelete([FromBody] BranchMappingEntity objEntity)
        {
            var response = new HttpResponseMessage();
            if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
            {
                if (objRepositary.CrownDelete(Convert.ToInt32(objEntity.id)))
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
