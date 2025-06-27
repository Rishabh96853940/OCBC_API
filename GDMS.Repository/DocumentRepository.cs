

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using Dapper;


namespace Kotak.Repository
{
    public class DocumentRepository : BaseRepository, IDocument<DocumentEntity>
    {

        public IEnumerable<DocumentEntity> GetDetails(int userid)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userid);

                IList<DocumentEntity> resultList = SqlMapper
                    .Query<DocumentEntity>(
                        ConnectionString,
                        "usp_GetDocumentWithDetailsByUser",
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public IEnumerable<DocumentEntity> GetDocumentsNamesList(int userid)

        {

            try

            {

                var parameters = new DynamicParameters();

                parameters.Add("@UserID", userid);



                IList<DocumentEntity> resultList = SqlMapper

                    .Query<DocumentEntity>(

                        ConnectionString,

                        "GetDocumentsNamesList",

                        param: parameters,

                        commandType: CommandType.StoredProcedure

                    ).ToList();



                return resultList;

            }

            catch (Exception ex)

            {

                throw;

            }

        }



        public IEnumerable<DocumentEntity> GetDocumentsNamesListForEdit(int userid)

        {

            try

            {

                var parameters = new DynamicParameters();

                parameters.Add("@UserID", userid);



                IList<DocumentEntity> resultList = SqlMapper

                    .Query<DocumentEntity>(

                        ConnectionString,

                        "GetDocumentsNamesListForEdit",

                        param: parameters,

                        commandType: CommandType.StoredProcedure

                    ).ToList();



                return resultList;

            }

            catch (Exception ex)

            {

                throw;

            }

        }









        public IEnumerable<DocumentEntity> GetDeptCodeList(int userid)

        {

            try

            {

                var parameters = new DynamicParameters();

                parameters.Add("@UserID", userid);



                IList<DocumentEntity> resultList = SqlMapper

                    .Query<DocumentEntity>(

                        ConnectionString,

                        "GetDeptCodeList",

                        param: parameters,

                        commandType: CommandType.StoredProcedure

                    ).ToList();



                return resultList;

            }

            catch (Exception ex)

            {

                throw;

            }

        }





        public string InsertDocumentDetails(DocumentEntity entity)

        {

            try

            {

                var parameters = new DynamicParameters();

                parameters.Add("@Id", entity.Id);

                parameters.Add("@DocumentId", Convert.ToInt32(entity.DocumentType));

                parameters.Add("@DepartmentID", entity.departmentID);

                parameters.Add("@DocumentDetails", entity.DetailDocumentType);

                parameters.Add("@ReteionPeriod", Convert.ToInt32(entity.RetentionPeriod));

                parameters.Add("@userid", entity.userid);

                parameters.Add("@Message", "", direction: ParameterDirection.Output, size: 100);



                SqlMapper.Execute(

                    ConnectionString,

                    "AddDocumentDetailsMaster",

                    param: parameters,

                    commandType: CommandType.StoredProcedure

                );



                string result = parameters.Get<string>("@Message");

                return result;

            }

            catch (Exception ex)

            {

                throw;

            }

        }



        public DocumentEntity GetDepartmentByDocumentDetails(DocumentEntity entity)

        {

            try

            {

                var parameters = new DynamicParameters();

                parameters.Add("@DocumentType", entity.DocumentType);

                parameters.Add("@DetailDocumentType", entity.DetailDocumentType);

                parameters.Add("@RetentionPeriod", entity.RetentionPeriod);

                DocumentEntity result = SqlMapper.Query<DocumentEntity>(
                    ConnectionString,
                    "GetDepartmentByDocumentDetails",
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string InsertDocument(DocumentEntity entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DocumentName", entity.DocumentType);
                parameters.Add("@documentID", entity.documentID);
                parameters.Add("@UserID", entity.userid);
                parameters.Add("@Message", "", direction: ParameterDirection.Output);


                string message = SqlMapper
                    .Query<string>(
                        ConnectionString,
                        "usp_AddDocuments",
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();


                string result = parameters.Get<string>("@Message");

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public bool DeleteDocumentDetailMaster(int id, int documentID, int userId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@documentID", documentID);
                parameters.Add("@userid", userId);
                parameters.Add("@Msg", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "usp_DeleteDocumentDetailMasterById", param: parameters, commandType: CommandType.StoredProcedure);

                string resultMsg = parameters.Get<string>("@Msg");
                return !string.IsNullOrEmpty(resultMsg) && resultMsg.ToLower().Contains("successfully");

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
