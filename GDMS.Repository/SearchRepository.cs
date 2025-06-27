using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Kotak.Repository;
using Kotak.Repository.Interfaces;
using Kotak.Entities;
using Dapper;
using System.Data;

namespace Kotak.Repository
{

    public class SearchRepository : BaseRepository, ISearch<SearchEntity>
    {

        public IEnumerable<SearchEntity> Get()
        {
            try
            {
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetBranch", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public SearchEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERID", id);
                return SqlMapper.Query<SearchEntity>(ConnectionString, "GetDepartmentList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Add(SearchEntity entity)
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
        public string Update(SearchEntity entity)
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

        private string AddUpdate(SearchEntity entity)
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
            parameters.Add("@lanno", entity.lanno);
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

        public string UpdaterequestNo(SearchEntity entity)
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

        public string podentry(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@iID", entity.id);
            parameters.Add("@item_number", entity.item_number);
            //parameters.Add("@request_no", entity.request_number);
            parameters.Add("@workorder_number", entity.workorder_number);
            parameters.Add("@pod_number", entity.pod_number);
            parameters.Add("@courier_name", entity.courier_name);
            parameters.Add("@page_count", entity.page_count);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_item_send_to_dispatch", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public string updateScanCopy(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@lanno", entity.lanno);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@file_path", entity.file_path);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_updateScanCopy", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public string updateLoanCopy(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@lanno", entity.lanno);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@file_path", entity.file_path);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_updateLoanCopy", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public IEnumerable<SearchEntity> GetBatchDetails(string BatchNo, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BatchNo", BatchNo);
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetBatchDetailsRetrival", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetRetrivaldetails(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetRetrivaldetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> getRetrivalDispatch(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getRetrivalDispatch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> getRetrivalDispatchByRequestno(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetRetrivaldetailsByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetFileDetails(string LanNo, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LanNo", LanNo);
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetLANDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<SearchEntity> GetBatchClose(string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", request_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_UdpateRequestClose", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POD Entry 


        public IEnumerable<SearchEntity> RetrievalDashboard(int USERId, string status)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@status", status);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "RetrievalDashboard", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<SearchEntity> getpoddetails(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getpoddetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ACK Copy

        public IEnumerable<SearchEntity> GetAck(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetAckdetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> getAckByRequestno(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getAckByRequestno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> getAckdetailsbyRequestno(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getAckdetailsbyRequestno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ACkentry(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@iID", entity.id);
            parameters.Add("@lanno", entity.lanno);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_ACKEntry", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }
        public string LoancloseCasesentry(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@iID", entity.id);
            parameters.Add("@lanno", entity.lanno);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@loan_close_file_path", entity.loan_close_file_path);

            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_LoancloseCasesentry", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }
        //GetloanclosebyrequestNo

        public IEnumerable<SearchEntity> GetloanclosebyrequestNo(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetloanclosebyrequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<SearchEntity> Getloanclose(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_Getloanclose", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetDumpdataSearch(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetDumpdataSearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<SearchEntity> Getbasicsearch(int USERId, string CartonNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@USERId", USERId);
                parameters.Add("@CartonNo", CartonNo);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_Getbasicsearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public IEnumerable<SearchEntity> PickupHistory(int USERId, string File_No)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@USERId", USERId);
                parameters.Add("@File_No", File_No);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetPickupHistory", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string podentrydump(SearchEntity entity)
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
                    parameters.Add("@request_no", entity.request_id);
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
        public IEnumerable<SearchEntity> SearchRecordsByFilter(int USERId, string FileNo, int SearchBy)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@SearchParamterID", SearchBy);
                parameters.Add("@SearchValues", FileNo);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "getSearchDataByFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<SearchEntity> getSearchDataByFilter(int UserID, int TemplateID, int BranchID, int SearchParamterID, string SearchValues, int SubfolderID, int DeptID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);
                parameters.Add("@TemplateID", TemplateID);
                parameters.Add("@DeptID", DeptID);
                //   parameters.Add("@DocID", DocID);
                parameters.Add("@BranchID", BranchID);
                parameters.Add("@SearchParamterID", SearchParamterID);
                parameters.Add("@SearchValues", SearchValues);
                parameters.Add("@SubfolderID", SubfolderID);

                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "getSearchDataByFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {


                throw;
            }
        }


        public IEnumerable<SearchEntity> BasicSearchRecordsByFilter(int USERId, string FileNo, int SearchBy)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                parameters.Add("@USERId", USERId);
                parameters.Add("@SearchBy", SearchBy);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_BasicSearchRecordsByFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetRefilingdata(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetRefilingdata", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetRefillingAck(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetRefillingAck", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string RefillingAckentry(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@lanno", entity.lanno);
            parameters.Add("@ref_request_no", entity.ref_request_no);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_RefillingAckentry", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public IEnumerable<SearchEntity> GetReffilingRequestNo(string request_no, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetReffilingRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> AckAndGetReffilingRequestBYRequestNo(string request_no, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                //parameters.Add("@item_number", item_number);
                //parameters.Add("@workorder_number", workorder_number);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "ACKaNDGetRefilingRequestDataByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
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
        public IEnumerable<SearchEntity> GetBarcode(string FileBarcode)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileBarcode", FileBarcode);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetBarcode", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RefRequestUdpate(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@lanno", entity.lanno);
            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@FileBarcode", entity.FileBarcode);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_RefRequestUdpate", param: parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<string>("@MSG");
        }
        //RefRequestUdpatePODNumber

        public string RefRequestUdpatePODNumber(SearchEntity entity)
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
        public string UpdatePOD(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@request_no", entity.request_no);
            parameters.Add("@pod_number", entity.pod_number);
            parameters.Add("@courier_name", entity.Courier_id);
            parameters.Add("@lanno", entity.lanno);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_UpdatePOD", param: parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<string>("@MSG");
        }

        // public string UpdateStatus(SearchEntity entity)
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

        public IEnumerable<SearchEntity> getRetrivalForUdpatestatus(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getRetrivalForUdpatestatus", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> getRetrivalByRequestno(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getRetrivalByRequestno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<SearchEntity> GetRetrievalReport(SearchEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getRetrivalReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }




        public IEnumerable<SearchEntity> GetRefillingReport(SearchEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GetRefillingReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> Getoutreport(SearchEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_Getoutreport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> getdumpsearch(SearchEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@USERId", entity.CreatedBy);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_getdumpsearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddRetrivalRequest(SearchEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_no", entity.request_id);
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

        public string AddPickupRequest(SearchEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_no", entity.request_id);
            parameters.Add("@pickup_date", entity.pickup_date);
            parameters.Add("@status", entity.status);
            parameters.Add("@userid", entity.userid);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_add_pick_up_and_ack", param: parameters, commandType: CommandType.StoredProcedure);

            string result = parameters.Get<string>("@MSG");





            return result;

        }


        public string UpdateStatus(SearchEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_no", entity.request_id);
            parameters.Add("@status", entity.status);
            parameters.Add("@userid", entity.CreatedBy);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_update_retrival_status", param: parameters, commandType: CommandType.StoredProcedure);

            string result = parameters.Get<string>("@MSG");





            return result;

        }


        public string ClosedRetrivalRequest(string request_number, int userid, string dispatch_address)
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

        public IEnumerable<SearchEntity> GetRequestNumber(int USERId, string request_number)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_number);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "sp_get_request_number", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetDataByRequestNumber(int USERId, string request_number)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_number);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "sp_get_retrival_request_by_user_id", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetApprovalByRequestNo(int USERId, string request_number)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_number);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "sp_get_approval_by_request_number", parameters, commandType: CommandType.StoredProcedure).ToList();
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


        public IEnumerable<SearchEntity> ApprovalRequestPending(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);

                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "sp_get_approval_pending", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<SearchEntity> GetRetrivalRequest(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetRetrievalRequest", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<SearchEntity> GetRefilingRequest(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetRefilingRequest", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<SearchEntity> GetRefilingRequestDataByRequestNo(int USERId, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", request_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetRefilingRequestDataByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<SearchEntity> CheckRefilingRequestByItemNo(int USERId, string item_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", item_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "CheckRefilingRequestByItemNo", parameters, commandType: CommandType.StoredProcedure).ToList();
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

        public IEnumerable<SearchEntity> GetRefilingRequestACK(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetRefilingRequestACK", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SearchEntity> GetRefilingRequestACKByRequestNo(int USERId, string requestNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@request_no", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetRefilingRequestACKByRequestNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddRefilingRequest(SearchEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@request_no", entity.request_id);
            parameters.Add("@item_no", entity.item_number);
            parameters.Add("@userid", entity.userid);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "AddRefilingRequest", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public IEnumerable<SearchEntity> GetRetrivalPprooval(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_GET_Approval_Details", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string AddEditApprovalDetails(SearchEntity entity)
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


        public IEnumerable<SearchEntity> GetApprovalUser(int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "SP_Get_Approval_User", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }



        public IEnumerable<SearchEntity> RetrievalDashboard(int USERId, string status, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@status", status);
                parameters.Add("@timeperiod", timeperiod);
                parameters.Add("@FromDate", fromDate);
                parameters.Add("@ToDate", toDate);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "RetrievalDashboard", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string refilingdumpupload(SearchEntity entity)
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
                    parameters.Add("@request_no", entity.request_id);
                    parameters.Add("@userid", entity.CreatedBy);
                    parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "AddRefilingRequest", param: parameters, commandType: CommandType.StoredProcedure);

                    string result = parameters.Get<string>("@MSG");
                    stringBuilder.AppendLine(strReffields[0] + "-" + result);

                }
            }
            return stringBuilder.ToString();
        }

        public IEnumerable<SearchEntity> GetLanDetailsQuickSearch(int USERId, string lan_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", USERId);
                parameters.Add("@lan_no", lan_no);
                IList<SearchEntity> resultList = SqlMapper.Query<SearchEntity>(ConnectionString, "GetLanDetailsQuickSearch", parameters, commandType: CommandType.StoredProcedure).ToList();
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
