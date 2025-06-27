using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace Kotak.Repository
{
    public class InwardRepository : BaseRepository, InwardRepository<InwardEntity> 
	{
		public IEnumerable<InwardEntity> Get()
		{
			try
			{
				IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "usp_GetTemplateFields", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public InwardEntity Get(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@id", id);
				return SqlMapper.Query<InwardEntity>(ConnectionString, "usp_GetTemplateFieldsDataByID", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}
        
      
		public string Update(InwardEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();

                //parameters.Add("@id", entity.id);                
                //parameters.Add("@IndexField", entity.IndexField);
                //parameters.Add("@DisplayName", entity.DisplayName);
                //parameters.Add("@FieldType", entity.FieldType);
                //parameters.Add("@MinLenght", entity.MinLenght);
                //parameters.Add("@MaxLenght", entity.MaxLenght);
                //parameters.Add("@MasterValues", entity.ListData);
                //parameters.Add("@TemplateID", entity.TemplateID);
                //parameters.Add("@IsMandatory", entity.IsMandatory);
                //parameters.Add("@IsAuto", entity.IsAuto);
                //parameters.Add("@BID", entity.BranchID);
                parameters.Add("@oId", "", direction: ParameterDirection.Output);

				SqlMapper.Execute(ConnectionString, "SP_UpdateIndexFilds", param: parameters, commandType: CommandType.StoredProcedure);

				return parameters.Get<string>("@oId");

			}
			catch (Exception)
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
				SqlMapper.Execute(ConnectionString, "usp_DeleteIndexData", param: parameters, commandType: CommandType.StoredProcedure);
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

        public bool Delete(string InvoiceNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PODNo", InvoiceNo);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_DeletePOD", param: parameters, commandType: CommandType.StoredProcedure);
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

        public bool DeleteBarcode(string BarcodeNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BarcodeNo", BarcodeNo);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_Deletebarcode", param: parameters, commandType: CommandType.StoredProcedure);
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

        public IEnumerable<InwardEntity> GetDetails(int TemplateID, string FileNo)
        {
            try
            {
                //DataTable dt = getData("EXEC SP_LoadFieldsByTempID " + TemplateID + ", '" + FileNo + "'");

                List<InwardEntity> LoadField = new List<InwardEntity>();
                //LoadField = ConvertDataTable<InwardEntity>(dt);            

                return LoadField;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    
        public IEnumerable<InwardEntity> GetBranchDetailsUserWise(int TemplateID,string FileNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TemplateID", TemplateID);
                parameters.Add("@FileNo", FileNo);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_LoadFieldsByTempID", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetPendingINVData()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetPendingINVData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string PODEntry(InwardEntity entity)
        {


            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@PODNo", entity.PODNo);
            parameters.Add("@BatchNo", entity.BatchNo);
            //parameters.Add("@DespatchedDate", entity.DespatchedDate);
            parameters.Add("@CourierName", entity.CourierName);
            parameters.Add("@Vouchers", entity.Vouchers);
            parameters.Add("@Registers", entity.Registers);
            parameters.Add("@ManualReceipts", entity.ManualReceipts);
            parameters.Add("@SRforms", entity.SRforms);
            // parameters.Add("@DocumentType", entity.DocumentType);
            //parameters.Add("@FileNo", entity.FileNo);
            //parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_AddEditPODEntry_JBUS", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message"); 
            return result;  //parameters.Get<string>("@MSG");
        }

        public string PODdetailsEntry(InwardEntity entity)
        {


            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@PODNo", entity.PODNo);
            parameters.Add("@BatchNo", entity.BatchNo);
            //parameters.Add("@DespatchedDate", entity.DespatchedDate);
            parameters.Add("@CourierName", entity.CourierName);
            parameters.Add("@SRforms", entity.SRforms);
            parameters.Add("@Vouchers", entity.Vouchers);
            parameters.Add("@Registers", entity.Registers);
            parameters.Add("@ManualReceipts", entity.ManualReceipts);
            // parameters.Add("@DocumentType", entity.DocumentType);
            //parameters.Add("@FileNo", entity.FileNo);
            //parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_AddEditPODDetailsEntry_JBUS", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");
            return result;  //parameters.Get<string>("@MSG");
        }
                        

        public string updateFileStatus(InwardEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@FileNo", entity.FileNo);
            parameters.Add("@CartonNo", entity.CartonNo);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_AddEditOutward_JB", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");

            return result;  //parameters.Get<string>("@MSG");
        }
 
        public string PODAcknowledge(InwardEntity entity)
        {
             
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PODNo", entity.PODNo);
            parameters.Add("@NewPODNo", entity.NewPODNo);            
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_PODAcknowledge_JB", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message"); 
            return result;  //parameters.Get<string>("@MSG");
        }

        public string SendBackBranch(InwardEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PODNo", entity.PODNo);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_PODsendBack_JB", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");
            return result;  //parameters.Get<string>("@MSG");
        }


        public string InwardEntry(InwardEntity entity)
            {  

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@AccountNo", entity.AccountNo);
            parameters.Add("@BatchNo", entity.BatchNo);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_UpdateBRInward", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");

            return result;  //parameters.Get<string>("@MSG");
        }


        public string AcknowledgePOD(InwardEntity entity)
        {
         
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@LAN", entity.LAN);
            parameters.Add("@PODNo", entity.PODNo);
            parameters.Add("@BatchNo", entity.BatchNo);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_Acknowledge", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");
            return result;  //parameters.Get<string>("@MSG");
        }

        public string UpdateReceivedPOD(InwardEntity entity)
        {
             
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PODNo", entity.PODNo);
                parameters.Add("@BatchNo", entity.BatchNo);
                parameters.Add("@LAN", entity.LAN);
            parameters.Add("@FileNo", entity.FileNo);
         //   parameters.Add("@ChildBarcode", entity.ChildBarcode);
            parameters.Add("@CreatedBy", entity.CreatedBy);
                parameters.Add("@Message", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_UpdateFileACK", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Message");
             
            return result;  //parameters.Get<string>("@MSG");
        }

        public string FileACkMissing(InwardEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PODNo", entity.PODNo);
            parameters.Add("@BatchNo", entity.BatchNo);
            parameters.Add("@LAN", entity.LAN);
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_UpdateFileACKMissing", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");
            return result;  //parameters.Get<string>("@MSG");
        }

        public string updatefilestatus(InwardEntity entity)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AccountNo", entity.AccountNo);
            parameters.Add("@Status", entity.Status);            
            parameters.Add("@CreatedBy", entity.CreatedBy);
            parameters.Add("@Message", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "SP_updatefilestatus_JB", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Message");
            return result;  //parameters.Get<string>("@MSG");
        } 

        public IEnumerable<InwardEntity> GetInwardData(int CompanyID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CompanyID", CompanyID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetInwardData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InwardEntity> GetPendingData(string UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetInvData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }        
        
        public IEnumerable<InwardEntity> GetBarcodeData(string CaseNo, string status)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CaseNo", CaseNo);
                parameters.Add("@status", status);                
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetBarcodeData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InwardEntity> GetPODDetailsEntry(string PODNO)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PODNO", PODNO);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetPODDetailsEntry", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InwardEntity> GetBatchDetails(string BatchNo,int USERId)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BatchNo", BatchNo);
                parameters.Add("@USERId", USERId);
                
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetBatchDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
 
         
        public IEnumerable<InwardEntity> GetBatchClose(string BatchNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BatchNo", BatchNo);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "UdpateBatchClose", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    
        public IEnumerable<InwardEntity> SearchRecords(int CompanyID,string FileNo,string SearchBy)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();               
                parameters.Add("@FileNo", FileNo);
                parameters.Add("@SearchBy", SearchBy);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetSearchrecordDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InwardEntity> SearchRecordsByFulletron(string FileNo, string SearchBy)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                parameters.Add("@SearchBy", SearchBy);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetSearchrecord_JB", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InwardEntity> GetStatusReport(InwardEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();    
                parameters.Add("@Producttype", obj.ProductType);
                parameters.Add("@Zone", obj.Zone);
                parameters.Add("@USERID", obj.CreatedBy);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetstatusreportZonewise", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetPODReport(InwardEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Producttype", obj.ProductType);
                parameters.Add("@Zone", obj.Zone);
                parameters.Add("@USERID", obj.CreatedBy);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "Getstatusreport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetBatchDetailsReport(InwardEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();


                parameters.Add("@DATETO", obj.DATETO);
                parameters.Add("@DATEFROM", obj.DATEFROM);
              //  parameters.Add("@DateBy", obj.DateBy);                
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetBatchDetailsReports", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetFileStatusReport(InwardEntity obj)
        {
            try//SP_GetPODInwardReport
            {
                DynamicParameters parameters = new DynamicParameters();
                                
                parameters.Add("@DATETO", obj.DATETO);
                parameters.Add("@DATEFROM", obj.DATEFROM);
                parameters.Add("@SearchBy", obj.SearchBy);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetPODInwardReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;              
            }
        }

        public IEnumerable<InwardEntity> GetInwardREport(InwardEntity obj)
        {
            try//SP_GetPODInwardReport
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@DATETO", obj.DATETO);
                parameters.Add("@DATEFROM", obj.DATEFROM);
               // parameters.Add("@PODStatus", obj.PODStatus);                
                //parameters.Add("@SearchBy", obj.SearchBy);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetInwardREport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
                        
        public IEnumerable<InwardEntity> UpdateLanDetails(string BatchNo, string LAN,int UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LAN", LAN);
                parameters.Add("@BatchNo", BatchNo);
                parameters.Add("@UserID", UserID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "UpdateLanDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //string BatchNo, string LanNo

        public IEnumerable<InwardEntity> GetAccountDetails(string AccountNo, string ProductType, string UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountNo", AccountNo);
               parameters.Add("@ProductType", ProductType);
                parameters.Add("@UserID", UserID); 
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetAccountDetails", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetAccountdata(string AccountNo, string UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountNo", AccountNo);
                parameters.Add("@UserID", UserID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetAccountData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> SearchRecordsByFilter(string FileNo, string SearchBy, string UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                parameters.Add("@SearchBy", SearchBy);
                parameters.Add("@UserID", UserID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetSearchrecord_JB", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InwardEntity> GetLANDetailsByUserAndBatchNo(string LAN, string BatchNo,int UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LAN", LAN);
                parameters.Add("@BatchNo", BatchNo);
                parameters.Add("@UserID", UserID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetLANDetailsByUserAndBatchNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetBatchdetailsByBatchNo(string BatchNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BatchNo", BatchNo);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "SP_GetBatchDetailsbybatchno", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetBatchDetails(string BatchNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BatchNo", BatchNo);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetBatchDetailsBy", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<InwardEntity> GetPODDetailsFulletron()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@LAN", LAN);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetPOD_JB", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
         
        public IEnumerable<InwardEntity> GetDumpdataSearch(string UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "Searchrecord_JB", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
         
        public IEnumerable<InwardEntity> GetPODDetailsACK(string UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);
                IList<InwardEntity> resultList = SqlMapper.Query<InwardEntity>(ConnectionString, "GetPOD_JB_US_ACk", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public string PODdetailsEntry(InwardEntity entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
