using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Dapper;
using Kotak.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Kotak.Repository
{
    public class AvansePickupRequestRepository : BaseRepository, IAvansePickupRequest<AvansePickupRequestEntity>
    {
        private static readonly Random random = new Random();

        public string Add(AvansePickupRequestEntity entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<AvansePickupRequestEntity> Get(int userid, int userid2)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", userid);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "usp_GetAvansePickupRequest", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public AvansePickupRequestEntity Get(int request_id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", request_id);
                return SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "usp_GetAvansePickupRequestDeatilsById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string InsertUpdatePickupRequest(AvansePickupRequestEntity entity)
        {

            try
            {
                //string token = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
             //   int request_id = random.Next(100000, 1000000);

                DynamicParameters parameters = new DynamicParameters();
             parameters.Add("@request_id",entity. request_id);
                parameters.Add("@service_type", entity.service_type);
                parameters.Add("@document_type", entity.document_type);
                parameters.Add("@main_file_count", entity.main_file_count);
                parameters.Add("@collateral_file_count", entity.collateral_file_count);
                parameters.Add("@remark", entity.remark);
                parameters.Add("@branch_id", entity.branch_id);
                parameters.Add("@created_by", entity.userid);
              //  parameters.Add("@updated_by", entity.Id);
              //  parameters.Add("@created_date", DateTime.Now);
                //parameters.Add("@updated_date", DateTime.Now);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                //IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "SP_InsertUpdatePickupRequest", parameters, commandType: CommandType.StoredProcedure).ToList();
                //return resultList;
                SqlMapper.Execute(ConnectionString, "SP_InsertUpdatePickupRequest", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Msg");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string UpdatePickupRequest(AvansePickupRequestEntity entity)
        {

            try
            {
                //string token = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                //   int request_id = random.Next(100000, 1000000);

                DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("@request_id", entity.request_id);
                parameters.Add("@service_type", entity.service_type);
                parameters.Add("@document_type", entity.document_type);
                parameters.Add("@main_file_count", entity.main_file_count);
                parameters.Add("@remark", entity.remark);
                parameters.Add("@collateral_file_count", entity.collateral_file_count);
                //parameters.Add("@satus", entity.collateral_file_count);
                parameters.Add("@updated_by", entity.userid);
            
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                
                SqlMapper.Execute(ConnectionString, "SP_UpdatePickupRequest", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Msg");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool DeletePickupRequest(string request_id,int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", request_id);
                parameters.Add("@delete_by", userid);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeletePickupRequest", param: parameters, commandType: CommandType.StoredProcedure);
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

        public IEnumerable<AvansePickupRequestEntity> GetAvansePickupRequestByACK(int UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "usp_GetAvansePickupRequestByACK", parameters,commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string UpdatePickupRequestACK(AvansePickupRequestEntity entity)
        {

            try
            {
                //string token = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                //   int request_id = random.Next(100000, 1000000);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@pra_remark", entity.pra_remark);
                parameters.Add("@user_id", entity.userid);

                parameters.Add("@MSG", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "sp_update_request_ack", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string AddEditPickupRequestSchedule(AvansePickupRequestEntity entity)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@schedule_date", entity.schedule_date);
                parameters.Add("@vehicle_number", entity.vehicle_number);
                parameters.Add("@escort_name", entity.escort_name);
                parameters.Add("@escort_number", entity.escort_number);
                parameters.Add("@pickup_address", entity.pickup_address);
                parameters.Add("@reschedule_date", entity.reschedule_date);
                parameters.Add("@reschedule_reason", entity.reschedule_reason);
                parameters.Add("@user_id", entity.userid);
                parameters.Add("@Message", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "SP_AddEdit_Pickup_Request_schedule", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Message");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetPickupRequestSchedule(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@user_id", userid);

                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "SP_Get_Pickup_request_schedule", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetFileInventory(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@user_id", userid);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "SP_Get_Pickup_request_schedule_File_Inv", parameters,commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public IEnumerable<AvansePickupRequestEntity> GetFileInventoryQC(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@user_id", userid);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "SP_Get_Pickup_request_schedule_File_Inv_QC", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetFileInwardByRequestId(string request_id, int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", request_id);              
                parameters.Add("@UserID", userid);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "sp_Get_File_Inventory", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetFileInwardByLaNo(string LanNo, string request_no,string document_tyype, string service_type, int UserID )
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LanNo", LanNo);
                parameters.Add("@RequestNo", request_no); 
                parameters.Add("@service_type", service_type);
                parameters.Add("@DocumentType", document_tyype);
                parameters.Add("@UserId", UserID);

                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "sp_Get_File_By_LanNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetFileInventoryByCartonNo(string request_id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", request_id);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "sp_Get_File_Inventory_By_carton_no", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<AvansePickupRequestEntity> GetFileInventoryByRequestNo(string request_id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", request_id);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "sp_Get_File_Inventory_By_request_no", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetFileInventoryByLanNo(string LanNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LanNo", LanNo);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "sp_get_data_by_lan_no", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AddEditFileInventory(AvansePickupRequestEntity entity)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();            
                parameters.Add("@file_no", entity.file_no);
                parameters.Add("@lan_no", entity.lan_no);
                parameters.Add("@carton_no", entity.carton_no);
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@document_type", entity.document_type); 
                parameters.Add("@service_type", entity.service_type);
                parameters.Add("@userid", entity.userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "sp_add_edit_file_inventory", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string UpdatePackCarton(AvansePickupRequestEntity entity)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@carton_no", entity.carton_no);
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@userid", entity.userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "sp_update_pack_carton", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string CloseRequest(AvansePickupRequestEntity entity)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@userid", entity.userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "sp_update_request_closed", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public string AddEditFileInventoryQC(AvansePickupRequestEntity entity)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@file_no", entity.file_no);
                parameters.Add("@lan_no", entity.lan_no);
                parameters.Add("@carton_no", entity.carton_no);
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@userid", entity.userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "sp_add_edit_file_inventory_QC", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string UpdateClosedRequest(AvansePickupRequestEntity entity)
        {

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@carton_no", entity.carton_no);
                parameters.Add("@lan_no", entity.lan_no);
                parameters.Add("@id", entity.id);
                parameters.Add("@file_no", entity.file_no);
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@userid", entity.userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "sp_update_closed_request", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string GetAvancePickUpRequestNo(int userid,string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", request_no);
                parameters.Add("@UserID", userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_Get_Pickup_request_number", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");

                return result.ToString();
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<AvansePickupRequestEntity> GetPickupRequestHistory(string request_no, int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RequestNo", request_no);
                parameters.Add("@USERId", USERId);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "GetPickupRequestHistory", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public string GetAvanceRefillingRequestNumber(int userid, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", request_no);
                parameters.Add("@UserID", userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_Get_reffiling_request_number", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");

                return result.ToString();
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetCartonNoByRequestId(int userid, string request_no)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", request_no);
                parameters.Add("@UserID", userid);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "GetCartonNoByRequestNo", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<AvansePickupRequestEntity> GetPickupRequestReport(AvansePickupRequestEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "GetPickupRequestReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetFileInventoryReport(AvansePickupRequestEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "GetFileInventoryReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<AvansePickupRequestEntity> GetPickupRequestDashboardData(int USERId, string status, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null, string monthyear = null)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", USERId);
                parameters.Add("@status", status);
                parameters.Add("@timeperiod", timeperiod);
                parameters.Add("@FromDate", fromDate);
                parameters.Add("@ToDate", toDate);
                parameters.Add("@monthyear", monthyear);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "usp_Dashboard", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<AvansePickupRequestEntity> GetPickupRequestDashboardDataCount(int USERId, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null, string monthyear = null)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", USERId);
                parameters.Add("@timeperiod", timeperiod);
                parameters.Add("@FromDate", fromDate);
                parameters.Add("@ToDate", toDate);
                parameters.Add("@monthyear", monthyear);
                IList<AvansePickupRequestEntity> resultList = SqlMapper.Query<AvansePickupRequestEntity>(ConnectionString, "usp_DashboardCount", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string AddPickedUpDate(AvansePickupRequestEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_id", entity.request_id);
                parameters.Add("@pickedup_date", entity.pickedup_date);
                parameters.Add("@pickedup_by", entity.CreatedBy);
                parameters.Add("@pickedup_remark", entity.pickedup_remark);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);

                SqlMapper.Execute(ConnectionString, "SP_AddPickedUpDate", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
