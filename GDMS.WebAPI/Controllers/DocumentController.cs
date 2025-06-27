
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
    public class DocumentController : ApiController
    {
        IDocument<DocumentEntity> objRepositary;
        public DocumentController(IDocument<DocumentEntity> obj)
        {
            objRepositary = obj;
        }


        [HttpGet]
        [Route("api/DocumentController/GetList")]
        public HttpResponseMessage GetList([FromUri] string user_Token, int userid)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))
                {
                    var objData = objRepositary.GetDetails(userid);
                    if (objData == null || !objData.Any())
                        objData = new List<DocumentEntity>();

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



        [HttpGet]

        [Route("api/DocumentController/GetDocumentsNamesList")]

        public HttpResponseMessage GetDocumentsNamesList([FromUri] string user_Token, int userid)

        {

            try

            {

                if (ValidateTokenExtension.ValidateToken(user_Token))


                {

                    var objData = objRepositary.GetDocumentsNamesList(userid);
                    if (objData == null || !objData.Any())
                        objData = new List<DocumentEntity>();

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

        [HttpGet]

        [Route("api/DocumentController/GetDocumentsNamesListForEdit")]

        public HttpResponseMessage GetDocumentsNamesListForEdit([FromUri] string user_Token, int userid)

        {

            try

            {

                if (ValidateTokenExtension.ValidateToken(user_Token))

                {

                    var objData = objRepositary.GetDocumentsNamesListForEdit(userid);
                    if (objData == null || !objData.Any())
                        objData = new List<DocumentEntity>();

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




        [HttpGet]

        [Route("api/DocumentController/GetDeptCodeList")]

        public HttpResponseMessage GetDeptCodeList([FromUri] string user_Token, int userid)

        {

            try

            {

                if (ValidateTokenExtension.ValidateToken(user_Token))


                {

                    var objData = objRepositary.GetDeptCodeList(userid);
                    if (objData == null || !objData.Any())
                        objData = new List<DocumentEntity>();

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

        [Route("api/DocumentController/InsertDetails")]

        public HttpResponseMessage InsertDocumentDetails([FromUri] string user_Token, [FromBody] DocumentEntity model)

        {

            try

            {

                if (ValidateTokenExtension.ValidateToken(user_Token))


                {

                    var message = objRepositary.InsertDocumentDetails(model);

                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = message });

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

        [Route("api/DocumentController/GetDepartmentByDetails")]

        public HttpResponseMessage GetDepartmentByDetails(

    [FromUri] string user_Token,

    [FromBody] DocumentEntity model)

        {

            try

            {

                if (ValidateTokenExtension.ValidateToken(user_Token))


                {

                    var departmentData = objRepositary.GetDepartmentByDocumentDetails(model);

                    return Request.CreateResponse(HttpStatusCode.OK, departmentData);

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
        [Route("api/DocumentController/Insert")]
        public HttpResponseMessage InsertDocument([FromUri] string user_Token, [FromBody] DocumentEntity model)
        {
            try
            {
                if (ValidateTokenExtension.ValidateToken(user_Token))

                {
                    var message = objRepositary.InsertDocument(model);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = message });
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
        [Route("api/DocumentDetails/Delete")]
        public HttpResponseMessage DeleteDocumentDetail([FromBody] DocumentEntity objEntity)
        {
            var response = new HttpResponseMessage();

            if (true)
            {
                bool result = objRepositary.DeleteDocumentDetailMaster(Convert.ToInt32(objEntity.Id), Convert.ToInt32(objEntity.documentID), Convert.ToInt32(objEntity.userid));

                if (result)
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
