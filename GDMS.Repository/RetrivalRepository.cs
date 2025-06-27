using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Data; 
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Text;

namespace Kotak.Repository
{
    public class RetrivalRepository : BaseRepository, IRetrival<RetrivalEntity>
    {

        public IEnumerable<RetrivalEntity> Get()
        {
            try
            {
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetBranch", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public RetrivalEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERID", id);
                return SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetDepartmentList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Add(RetrivalEntity entity)
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
        public string Update(RetrivalEntity entity)
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
                SqlMapper.Execute(ConnectionString, "SP_DeleteRetrivalrequest", param: parameters, commandType: CommandType.StoredProcedure);
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

        private string AddUpdate(RetrivalEntity entity)
        {

       //     string fbstatus = true;
         
            //if (entity.file_barcode_status == "" || entity.file_barcode_status == null)
            //{
            //    entity.file_barcode_status = false.ToString();
            //}
            
            //if (entity.property_barcode_status == "" || entity.property_barcode_status == null)
            //{
            //    entity.property_barcode_status = false.ToString();
            //}

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@iID", entity.id);
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@request_no", entity.request_no);
           // parameters.Add("@request_type", entity.request_type);
            parameters.Add("@request_reason", entity.request_reason);
           // parameters.Add("@dispatch_address", entity.dispatch_address);
            parameters.Add("@file_barcode_status", true);
            parameters.Add("@property_barcode_status", true);
            parameters.Add("@customer_name", entity.customer_name);
           // parameters.Add("@file_path", entity.file_path);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_AddEditRetrivalrequest", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public string UpdaterequestNo(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
         
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@request_type", entity.request_type);
            parameters.Add("@dispatch_address", entity.dispatch_address);
            parameters.Add("@file_path", entity.file_path);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_UpdateRequestNo", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public string podentry(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@iID", entity.id);
           parameters.Add("@item_number", entity.item_number);
            parameters.Add("@request_no", entity.request_number);
            parameters.Add("@workorder_number", entity.workorder_number);
            parameters.Add("@pod_number", entity.pod_number);
            parameters.Add("@courier_name", entity.courier_name);
            parameters.Add("@page_count", entity.page_count);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_item_send_to_dispatch", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public string updateScanCopy(RetrivalEntity entity)
        {             
            DynamicParameters parameters = new DynamicParameters();            
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@file_path", entity.file_path);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_updateScanCopy", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public string updateLoanCopy(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@file_path", entity.file_path);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_updateLoanCopy", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public IEnumerable<RetrivalEntity> GetBatchDetails(string BatchNo, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BatchNo", BatchNo);
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetBatchDetailsRetrival", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> GetRetrivaldetails(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetRetrivaldetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        public IEnumerable<RetrivalEntity> getRetrivalDispatch(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getRetrivalDispatch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> getRetrivalDispatchByRequestno(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetRetrivaldetailsByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> GetFileDetails(string CartonNo, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CartonNo", CartonNo);
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetLANDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<RetrivalEntity> GetBatchClose(string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", request_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_UdpateRequestClose", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POD Entry 
        

            public IEnumerable<RetrivalEntity> RetrievalDashboard(int USERId, string status)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@status", status);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "RetrievalDashboard", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<RetrivalEntity> getpoddetails(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getpoddetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ACK Copy

        public IEnumerable<RetrivalEntity> GetAck(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetAckdetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> getAckByRequestno(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getAckByRequestno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        public IEnumerable<RetrivalEntity> getAckdetailsbyRequestno(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getAckdetailsbyRequestno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ACkentry(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@iID", entity.id);
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@request_no", entity.request_no);           
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_ACKEntry", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }
        public string LoancloseCasesentry(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@iID", entity.id);
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@loan_close_file_path", entity.loan_close_file_path);
            
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_LoancloseCasesentry", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }
        //GetloanclosebyrequestNo

        public IEnumerable<RetrivalEntity> GetloanclosebyrequestNo(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetloanclosebyrequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<RetrivalEntity> Getloanclose(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_Getloanclose", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> GetDumpdataSearch(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetDumpdataSearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        
        public IEnumerable<RetrivalEntity> Getbasicsearch(int USERId, string File_No)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@USERId", USERId);
                parameters.Add("@File_No", File_No);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_Getbasicsearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }

        }      
        
        public IEnumerable<RetrivalEntity> PickupHistory(int USERId, string File_No)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@USERId", USERId);
                parameters.Add("@File_No", File_No);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetPickupHistory", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string podentrydump(RetrivalEntity entity)
        {


            int len = entity.upload_data.Length;

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[1];
                string strdata = entity.upload_data[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; 
                }

                if (strReffields[0] != "")
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@item_number", strReffields[0]);
                    parameters.Add("@request_no", entity.request_number);
                    parameters.Add("@workorder_number", entity.workorder_number);
                    parameters.Add("@pod_number", entity.pod_number);
                    parameters.Add("@courier_name", entity.courier_name);
                    parameters.Add("@page_count", entity.page_count);
                    parameters.Add("@userid", entity.CreatedBy);
                    parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "SP_item_send_to_dispatch", param: parameters, commandType: CommandType.StoredProcedure);

                    string result = parameters.Get<string>("@MSG");
                    stringBuilder.AppendLine(strReffields[0] + "-" + result);

                }
            }
            return stringBuilder.ToString();
        }
        public IEnumerable<RetrivalEntity> SearchRecordsByFilter(int USERId,string FileNo, int SearchBy)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                parameters.Add("@USERId", USERId);
                parameters.Add("@SearchBy", SearchBy);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_SearchRecordsByFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<RetrivalEntity> BasicSearchRecordsByFilter(int USERId, string FileNo, int SearchBy)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                parameters.Add("@USERId", USERId);
                parameters.Add("@SearchBy", SearchBy);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_BasicSearchRecordsByFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> GetRefilingdata(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetRefilingdata", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> GetRefillingAck(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetRefillingAck", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string RefillingAckentry(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();            
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@ref_request_no", entity.ref_request_no);             
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_RefillingAckentry", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public IEnumerable<RetrivalEntity> GetReffilingRequestNo(string request_no, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetReffilingRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> AckAndGetReffilingRequestBYRequestNo(string request_no, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                //parameters.Add("@item_number", item_number);
                //parameters.Add("@workorder_number", workorder_number);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "ACKaNDGetRefilingRequestDataByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RefillingAckItemNumber(string request_no, int USERId, string item_number, string workorder_number)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@USERId", USERId);
            parameters.Add("@request_no", request_no);
            parameters.Add("@item_number", item_number);
            parameters.Add("@workorder_number", workorder_number);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_ACKaNDGetRefilingRequestDataByRequestNo", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        //GetBarcode
        public IEnumerable<RetrivalEntity> GetBarcode(string FileBarcode)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();               
                parameters.Add("@FileBarcode", FileBarcode);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetBarcode", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RefRequestUdpate(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();          
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@FileBarcode", entity.FileBarcode);            
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_RefRequestUdpate", param: parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<string>("@MSG");
        }
        //RefRequestUdpatePODNumber

        public string RefRequestUdpatePODNumber(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
           
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@pod_number", entity.pod_number);
            parameters.Add("@courier_name", entity.courier_name);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_RefRequestUdpatePODNumber", param: parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<string>("@MSG");
        }
        public string UpdatePOD(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@pod_number", entity.pod_number);
            parameters.Add("@courier_name", entity.Courier_id);
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_UpdatePOD", param: parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<string>("@MSG");
        }
         
       // public string UpdateStatus(RetrivalEntity entity)
       // {
       //     DynamicParameters parameters = new DynamicParameters();
       //     parameters.Add("@status", entity.status);
       //     parameters.Add("@request_no", entity.request_no);
       //     parameters.Add("@remark", entity.remark);
       //     parameters.Add("@userid", entity.CreatedBy);
       //     parameters.Add("@MSG", "", direction: ParameterDirection.Output);
       //     SqlMapper.Execute(ConnectionString, "SP_UpdateStatus", param: parameters, commandType: CommandType.StoredProcedure);
       //
       //     return parameters.Get<string>("@MSG");
       // }
         
        public IEnumerable<RetrivalEntity> getRetrivalForUdpatestatus(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getRetrivalForUdpatestatus", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }   

        public IEnumerable<RetrivalEntity> getRetrivalByRequestno(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getRetrivalByRequestno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<RetrivalEntity> GetRetrievalReport(RetrivalEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getRetrivalReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


    

        public IEnumerable<RetrivalEntity> GetRefillingReport(RetrivalEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GetRefillingReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> Getoutreport(RetrivalEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_Getoutreport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> getdumpsearch(RetrivalEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@USERId", entity.CreatedBy);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_getdumpsearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddRetrivalRequest(RetrivalEntity entity)
        {
            
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", entity.request_number);
                parameters.Add("@ItemCode", entity.item_code);
                parameters.Add("@RetrivalType", entity.retrival_type);
                parameters.Add("@delivery_type", entity.delivery_type);
                parameters.Add("@retrieval_reason", entity.retrieval_reason);
                parameters.Add("@retrieval_reason", entity.retrieval_reason);
                parameters.Add("@remark", entity.remark);
                parameters.Add("@itemNo", entity.item_number);
                parameters.Add("@userid", entity.CreatedBy);
                parameters.Add("@page_count", entity.page_count);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_add_retrival_details", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG"); 

            return result;

        }

        public string AddPickupRequest(RetrivalEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_no", entity.request_number);
            parameters.Add("@pickup_date", entity.pickup_date);
            parameters.Add("@status", entity.status);
            parameters.Add("@userid", entity.userid);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_add_pick_up_and_ack", param: parameters, commandType: CommandType.StoredProcedure);

            string result = parameters.Get<string>("@MSG");





            return result;

        }


        public string UpdateStatus(RetrivalEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_no", entity.request_number);
            parameters.Add("@status", entity.status);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_update_retrival_status", param: parameters, commandType: CommandType.StoredProcedure);

            string result = parameters.Get<string>("@MSG");





            return result;

        }


        public string ClosedRetrivalRequest(string request_number,int userid, string dispatch_address)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_number", request_number);
            parameters.Add("@userid", userid);
            parameters.Add("@dispatch_address", dispatch_address);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_close_retrival_request", param: parameters, commandType: CommandType.StoredProcedure);

            string result = parameters.Get<string>("@MSG");





            return result;

        }

        public IEnumerable<RetrivalEntity> GetRequestNumber(int USERId,string request_number)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_number);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "sp_get_request_number", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> GetDataByRequestNumber(int USERId, string request_number)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_number);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "sp_get_retrival_request_by_user_id", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<RetrivalEntity> GetApprovalByRequestNo(int USERId, string request_number)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_number);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "sp_get_approval_by_request_number", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string GetAvanceRetrivalRequestNo(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_Get_RetrivalRequestNo", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");

                return result.ToString();
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
        

            public IEnumerable<RetrivalEntity> ApprovalRequestPending(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
               
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "sp_get_approval_pending", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        

             public IEnumerable<RetrivalEntity> GetRetrivalRequest(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetRetrievalRequest", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<RetrivalEntity> GetRefilingRequest(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetRefilingRequest", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<RetrivalEntity> GetRefilingRequestDataByRequestNo( int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetRefilingRequestDataByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<RetrivalEntity> CheckRefilingRequestByItemNo(int USERId, string item_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", item_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "CheckRefilingRequestByItemNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string CloseRefilingRequest(string request_number, int userid)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_number", request_number);
            parameters.Add("@userid", userid);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "closeRefilingRequest", param: parameters, commandType: CommandType.StoredProcedure);

            string result = parameters.Get<string>("@MSG");

            return result;

        }

        public IEnumerable<RetrivalEntity> GetRefilingRequestACK(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetRefilingRequestACK", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

          public IEnumerable<RetrivalEntity> GetRefilingRequestACKByRequestNo(int USERId, string requestNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetRefilingRequestACKByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddRefilingRequest(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_no", entity.request_number);
            parameters.Add("@item_no", entity.item_number);
            parameters.Add("@userid", entity.userid);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "AddRefilingRequest", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public IEnumerable<RetrivalEntity> GetRetrivalPprooval(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_GET_Approval_Details", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string AddEditApprovalDetails(RetrivalEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserID", entity.user_id);
            parameters.Add("@BranchID", entity.branch_id);
            parameters.Add("@created_by", entity.created_by);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_AddEditApproval", param: parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<string>("@MSG");
        }

        public bool DeleteApproval(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_DeleteApprovalData", param: parameters, commandType: CommandType.StoredProcedure);
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


        public IEnumerable<RetrivalEntity> GetApprovalUser(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);             
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "SP_Get_Approval_User", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }



        public IEnumerable<RetrivalEntity> RetrievalDashboard(int USERId, string status, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@status", status);
                parameters.Add("@timeperiod", timeperiod);
                parameters.Add("@FromDate", fromDate);
                parameters.Add("@ToDate", toDate);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "RetrievalDashboard", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string refilingdumpupload(RetrivalEntity entity)
        {


            int len = entity.CSVData.Length; 

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[1];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; 
                }

                if (strReffields[0] != "")
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@item_no", strReffields[0]);
                    parameters.Add("@request_no", entity.request_number);
                    parameters.Add("@userid", entity.CreatedBy);
                    parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "AddRefilingRequest", param: parameters, commandType: CommandType.StoredProcedure);

                    string result = parameters.Get<string>("@MSG");
                    stringBuilder.AppendLine(strReffields[0] + "-" + result);

                }
            }
            return stringBuilder.ToString();
        }

        public IEnumerable<RetrivalEntity> GetLanDetailsQuickSearch(int USERId, string lan_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@lan_no", lan_no);
                IList<RetrivalEntity> resultList = SqlMapper.Query<RetrivalEntity>(ConnectionString, "GetLanDetailsQuickSearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
