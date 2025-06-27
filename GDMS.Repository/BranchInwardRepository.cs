using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Data; 
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;

namespace Kotak.Repository
{
    public class BranchInwardRepository : BaseRepository, IBranchInward<BranchInwardEntity>
    {

        public IEnumerable<BranchInwardEntity> Get()
        {
            try
            {
                IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "GetBranch", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public BranchInwardEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERID", id);
                return SqlMapper.Query<BranchInwardEntity>(ConnectionString, "GetDepartmentList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
 
        
        public string Add(BranchInwardEntity entity)
        {
            try
            {
                return AddUpdate(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string Update(BranchInwardEntity entity)
        {
            try
            {
                return AddUpdate(entity);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_DeleteBranchdata", param: parameters, commandType: CommandType.StoredProcedure);
                string val = parameters.Get<string>("@Msg");
                if (!string.IsNullOrEmpty(val))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string AddUpdate(BranchInwardEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();            
            parameters.Add("@BatchNo", entity.BatchNo);
            parameters.Add("@appl", entity.appl);
            parameters.Add("@apac", entity.apac);
            parameters.Add("@document_type", entity.document_type);
            parameters.Add("@UserID", entity.CreatedBy);            
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_AddEditBranchinward", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }


        public string UpdateBatchNo(BranchInwardEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BatchNo", entity.BatchNo);            
            parameters.Add("@pod_no", entity.pod_no);
            parameters.Add("@courier_name", entity.courier_name);
            parameters.Add("@UserID", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_UpdateBatchNo", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        //private string AddEditAppacdetails(BranchInwardEntity entity)
        //{
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@iID", entity.id);
        //    parameters.Add("@BatchNo", entity.BatchNo);
        //    parameters.Add("@appl", entity.appl);
        //    parameters.Add("@apac", entity.apac);
        //    parameters.Add("@document_type", entity.document_type);
        //    parameters.Add("@UserID", entity.CreatedBy);
        //    //parameters.Add("@SFCode", entity.SFCode);
        //    //parameters.Add("@SFName", entity.SFName);
        //    //parameters.Add("@Zone", entity.Zone);
        //    //parameters.Add("@Region", entity.Region);
        //    //parameters.Add("@HubName", entity.HubName);
        //    parameters.Add("@MSG", "", direction: ParameterDirection.Output);
        //    SqlMapper.Execute(ConnectionString, "sp_AddEditBranchinward", param: parameters, commandType: CommandType.StoredProcedure);

        //    return parameters.Get<string>("@MSG");
        //}




        public IEnumerable<BranchInwardEntity> GetAppacDetails(BranchInwardEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@BatchNo", entity.BatchNo);
            parameters.Add("@appl", entity.appl);
            parameters.Add("@apac", entity.apac);
            parameters.Add("@document_type", entity.document_type);
            parameters.Add("@UserID", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "sp_GetAppacDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
            string message=   parameters.Get<string>("@MSG");           
             
            return resultList;

            //DynamicParameters parameters = new DynamicParameters();            
            //parameters.Add("@BatchNo", entity.BatchNo);
            //parameters.Add("@appl", entity.appl);
            //parameters.Add("@apac", entity.apac);
            //parameters.Add("@document_type", entity.document_type);
            //parameters.Add("@UserID", entity.CreatedBy);
            ////parameters.Add("@HubName", entity.HubName);
            //parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            //SqlMapper.Execute(ConnectionString, "sp_GetAppacDetails", param: parameters, commandType: CommandType.StoredProcedure);

            //return parameters.Get<string>("@MSG");
        }

        public IEnumerable<BranchInwardEntity> GetBatchDetails(string BatchNo, string USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@BatchNo", BatchNo);
                IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "GetBatchDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BranchInwardEntity> getPODdetails(string UserId)
        {

            DynamicParameters parameters = new DynamicParameters();             
            parameters.Add("@UserID", UserId);           
            IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "sp_GetPODPending", parameters, commandType: CommandType.StoredProcedure).ToList();
           
            return resultList; 
        }

        public string PODdetailsEntry(BranchInwardEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pod_no", entity.pod_no);
            parameters.Add("@batch_no", entity.batch_no);         
            parameters.Add("@courier_name", entity.courier_name);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_AddEditPODDetailsEntry", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");
            return result;  //parameters.Get<string>("@MSG");
        }

        public IEnumerable<BranchInwardEntity> GetBatchdetailsByBatchNo(string BatchNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                
                parameters.Add("@BatchNo", BatchNo);
                IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "SP_GetBatchDetailsbybatchno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BranchInwardEntity> getPODAckPending(string UserId)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserID", UserId);
            IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "sp_GetPODACKPending", parameters, commandType: CommandType.StoredProcedure).ToList();

            return resultList;
        }


        public string PODAckdetailsEntry(BranchInwardEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@pod_no", entity.pod_no);
            parameters.Add("@new_pod_no", entity.new_pod_no);
            parameters.Add("@batch_no", entity.batch_no);
            parameters.Add("@courier_name", entity.courier_name);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_AddEditPODACKDetailsEntry", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");
            return result;  //parameters.Get<string>("@MSG");
        }

        public IEnumerable<BranchInwardEntity> getDocumentdetails(string appl, string apac)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@appl", appl);
            parameters.Add("@apac", apac);           
            IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "SP_GetDocumentByApp", parameters, commandType: CommandType.StoredProcedure).ToList();

            return resultList;

         //   DynamicParameters parameters = new DynamicParameters();
         ////   parameters.Add("@BatchNo", entity.BatchNo);
         //   parameters.Add("@appl", entity.appl);
         //   parameters.Add("@apac", entity.apac);
         //  // parameters.Add("@document_type", entity.document_type);
         //   //parameters.Add("@UserID", entity.CreatedBy);
         //   parameters.Add("@MSG", "", direction: ParameterDirection.Output);
         //   IList<BranchInwardEntity> resultList = SqlMapper.Query<BranchInwardEntity>(ConnectionString, "SP_GetDocumentByApp", parameters, commandType: CommandType.StoredProcedure).ToList();
         //   string message = parameters.Get<string>("@MSG");

         //   return resultList;

            //DynamicParameters parameters = new DynamicParameters();            
            //parameters.Add("@BatchNo", entity.BatchNo);
            //parameters.Add("@appl", entity.appl);
            //parameters.Add("@apac", entity.apac);
            //parameters.Add("@document_type", entity.document_type);
            //parameters.Add("@UserID", entity.CreatedBy);
            ////parameters.Add("@HubName", entity.HubName);
            //parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            //SqlMapper.Execute(ConnectionString, "sp_GetAppacDetails", param: parameters, commandType: CommandType.StoredProcedure);

            //return parameters.Get<string>("@MSG");
        }


        public bool DeleteApack(string batch_no, string appl, string apac)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@batch_no", batch_no);
                parameters.Add("@appl", appl);
                parameters.Add("@apac", apac);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_DeleteApackdata", param: parameters, commandType: CommandType.StoredProcedure);
                string val = parameters.Get<string>("@Msg");
                if (!string.IsNullOrEmpty(val))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
