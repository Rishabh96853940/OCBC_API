
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

namespace Kotak.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentMasterController : ApiController
    {
        IDepartmentMaster<DepartmentMasterEntity> objRepositary;
        public DepartmentMasterController(IDepartmentMaster<DepartmentMasterEntity> obj)
        {
            objRepositary = obj;
        }


        [HttpGet]
        [Route("api/DepartmentMaster/GetList")]
        public HttpResponseMessage GetList([FromUri] string user_Token, int userid)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    var objData = objRepositary.GetDetails(userid);
                    if (objData == null || !objData.Any())
                        objData = new List<DepartmentMasterEntity>();

                    return Request.CreateResponse(HttpStatusCode.OK, objData);
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
        [Route("api/DepartmentMaster/Add")]
        public HttpResponseMessage Add([FromUri] string user_Token, [FromBody] DepartmentMasterEntity model)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    if (model.userid == null || model.userid == 0)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User ID is required.");

                    var resultMessage = objRepositary.AddDepartment(model);
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
        [Route("api/DepartmentMaster/Update")]
        public HttpResponseMessage Update([FromUri] string user_Token, [FromBody] DepartmentMasterEntity model)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    if (model.Id == 0 || model.userid == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid ID or User ID.");

                    var resultMessage = objRepositary.UpdateDepartment(model);
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
        [Route("api/DepartmentMaster/GetDepartmentUserMappings")]
        public HttpResponseMessage GetDepartmentUserMappings([FromUri] string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    var objData = objRepositary.GetDepartmentUserMappings();
                    if (objData == null || !objData.Any())
                        objData = new List<DepartmentMasterEntity>();

                    return Request.CreateResponse(HttpStatusCode.OK, objData);
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
        [Route("api/DepartmentMaster/DeleteDepartment")]
        public HttpResponseMessage DeleteDepartment([FromBody] DepartmentMasterEntity objEntity)
        {
            var response = new HttpResponseMessage();
            if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
            {
                if (objRepositary.DeleteDepartment(Convert.ToInt32(objEntity.Id)))
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
        [Route("api/DepartmentMaster/Delete")]
        public HttpResponseMessage Delete([FromBody] DepartmentMasterEntity objEntity)
        {
            var response = new HttpResponseMessage();
            if (ValidateTokenExtension.ValidateToken(objEntity.User_Token))
            {
                if (objRepositary.Delete(Convert.ToInt32(objEntity.Id)))
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
        [Route("api/DepartmentMaster/GetSysUsers")]
        public HttpResponseMessage GetSysUsers([FromUri] string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    var objData = objRepositary.GetSysUsers();
                    if (objData == null || !objData.Any())
                        objData = new List<DepartmentMasterEntity>();

                    return Request.CreateResponse(HttpStatusCode.OK, objData);
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


        [HttpGet]
        [Route("api/DepartmentMaster/GetDepartmentsLists")]
        public HttpResponseMessage GetDepartmentsLists([FromUri] string user_Token)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetDepartmentsLists();
                    if (objData == null || !objData.Any())
                        objData = new List<DepartmentMasterEntity>();

                    return Request.CreateResponse(HttpStatusCode.OK, objData);
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
        [Route("api/DepartmentMaster/Insert")]
        public HttpResponseMessage InsertDepartmentMapping([FromUri] string user_Token, [FromBody] DepartmentMasterEntity model)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    if (model.userid == 0)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User ID is required.");

                    var resultMessage = objRepositary.InsertDepartmentMapping(model);

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
        [Route("api/DepartmentMaster/BulkInsert")]
        public HttpResponseMessage BulkInsertDepartmentMapping([FromUri] string user_Token, [FromBody] DepartmentMasterEntity model)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    bool hasInserts = model.departmentIDs != null && model.departmentIDs.Any();
                    bool hasDeletes = model.deleteDepartmentIDs != null && model.deleteDepartmentIDs.Any();

                    if (model.userid == 0 || model.sysUserID == 0 || (!hasInserts && !hasDeletes))
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No valid department mapping data provided.");
                    }

                    var msg = objRepositary.BulkInsertDepartmentMapping(model);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = msg });
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
        [Route("api/DepartmentMaster/GetDepartmentMapDetailsByID")]
        public HttpResponseMessage GetDepartmentMapDetailsByID([FromUri] string user_Token, int userid)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    var objData = objRepositary.GetDepartmentMapDetailsByID(userid);
                    if (objData == null || !objData.Any())
                        objData = new List<DepartmentMasterEntity>();

                    return Request.CreateResponse(HttpStatusCode.OK, objData);
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
