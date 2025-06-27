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
    public class BranchMasterController : ApiController
    {
		IRepository<BranchEntity> objRepositary;
		public BranchMasterController(IRepository<BranchEntity> obj)
		{
			objRepositary = obj;
		}
		[HttpGet]
		[Route("api/BranchMaster/GetBranchList")]
		public HttpResponseMessage GetBranchList([FromUri] string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var homeBanner = objRepositary.Get();
					if (homeBanner != null)
						return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/GetCrownBranchList")]
        public HttpResponseMessage GetCrownBranchList([FromUri] string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var homeBanner = objRepositary.GetCrownBranchList();
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/GetbranchDeatilsById")]
        public HttpResponseMessage GetbranchDeatilsById([FromUri] int id, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var homeBanner = objRepositary.GetbranchDeatilsById(id);
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/GetCrownbranchDeatilsById")]
        public HttpResponseMessage GetCrownbranchDeatilsById([FromUri] int id, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var homeBanner = objRepositary.GetCrownbranchDeatilsById(id);
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/InsertBranchDeatils")]
        public HttpResponseMessage InsertBranchDeatils([FromBody] BranchEntity branchEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(branchEntity.User_Token))
                {
                    var homeBanner = objRepositary.InsertBranchDeatils(branchEntity);
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/InsertCrownBranchDeatils")]
        public HttpResponseMessage InsertCrownBranchDeatils([FromBody] BranchEntity branchEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(branchEntity.User_Token))
                {
                    var homeBanner = objRepositary.InsertCrownBranchDeatils(branchEntity);
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/UpdateCrownBranchDeatils")]
        public HttpResponseMessage UpdateCrownBranchDeatils([FromBody] BranchEntity branchEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(branchEntity.User_Token))
                {
                    var homeBanner = objRepositary.InsertCrownBranchDeatils(branchEntity);
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/UpdateBranchDeatils")]
        public HttpResponseMessage UpdateBranchDeatils([FromBody] BranchEntity branchEntity)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(branchEntity.User_Token))
                {
                    var homeBanner = objRepositary.InsertBranchDeatils(branchEntity);
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/DeleteBranchDeatils")]
        public HttpResponseMessage DeleteBranchDeatils([FromUri] int id, string user_Token, int userid)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData =objRepositary.DeleteBranchDeatils(id, userid);
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
		[Route("api/BranchMaster/GetBranchDetails")]
		public HttpResponseMessage GetBranchDetails([FromUri] int homeBannerID, string user_Token)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(user_Token))
				{
					var homeBanner = objRepositary.Get(homeBannerID);
					if (homeBanner != null)
						return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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
        [Route("api/BranchMaster/GetbranchDeatilsByUserId")]
        public HttpResponseMessage GetbranchDeatilsByUserId([FromUri] int USER_ID, string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var homeBanner = objRepositary.GetbranchDeatilsByUserId(USER_ID);
                    if (homeBanner != null)
                        return Request.CreateResponse(HttpStatusCode.OK, homeBanner);
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


        //GetbranchDeatilsByUserId

        [HttpPost]
		[Route("api/BranchMaster/Create")]
		public HttpResponseMessage Post([FromBody]BranchEntity objBranch)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objBranch.User_Token))
				{

					string val = objRepositary.Add(objBranch);
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
		[Route("api/BranchMaster/Update")]
		public HttpResponseMessage Update([FromBody]BranchEntity objBranch)
		{
			try
			{
				if (ValidateTokenExtension.ValidateToken(objBranch.User_Token))
				{
					if (!string.IsNullOrEmpty(objRepositary.Update(objBranch)))
					{
						return Request.CreateResponse(HttpStatusCode.OK, "Branch is updated successfully!!");
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
		[Route("api/BranchMaster/Delete")]
		public HttpResponseMessage Delete([FromBody]BranchEntity objBranch)
		{
			var response = new HttpResponseMessage();
			if (ValidateTokenExtension.ValidateToken(objBranch.User_Token))
			{
				if (objRepositary.Delete(Convert.ToInt32(objBranch.id)))
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
