using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;

namespace Kotak.Repository
{
public	class DataUplaodRepository : BaseRepository, IMappingchkRepository<DataUploadEntity>
	{
		public IEnumerable<DataUploadEntity> Get()
		{
			try
			{
				IList<DataUploadEntity> resultList = SqlMapper.Query<DataUploadEntity>(ConnectionString, "SP_GetFieldsDetailsNames", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public DataUploadEntity Get(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@USERID", id);
				return SqlMapper.Query<DataUploadEntity>(ConnectionString, "GetBranchList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}
                      
        public string Add(DataUploadEntity entity)
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
		public string Update(DataUploadEntity entity)
		{
			try
			{
				return UpdateFileStatus(entity);				

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
				SqlMapper.Execute(ConnectionString, "usp_DeleteBranch", param: parameters, commandType: CommandType.StoredProcedure);
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

		private string AddUpdate(DataUploadEntity entity)
		{
            
            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i =1; i < len; i++)
            {
                String[] strReffields = new String[15];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                DynamicParameters parameters = new DynamicParameters();               
                
                parameters.Add("@NewCBSAccountNo", strReffields[0]);
                parameters.Add("@ApplicationNo", strReffields[1]);
                parameters.Add("@CBSCUSTIC", strReffields[2]);
                parameters.Add("@Team", strReffields[3]);
                parameters.Add("@HandliedBy", strReffields[4]);
                parameters.Add("@ProductCode", strReffields[5]);
                parameters.Add("@Product", strReffields[6]);
                parameters.Add("@ProductDescription", strReffields[7]);
                parameters.Add("@JCCode", strReffields[8]);
                parameters.Add("@JCName", strReffields[9]);
                parameters.Add("@Zone", strReffields[10]);
                parameters.Add("@CustomerName", strReffields[11]);
                parameters.Add("@DBDate", strReffields[12]);
                parameters.Add("@FinalRemarks", strReffields[13]);
                parameters.Add("@DisbursedMonth", strReffields[14]);
                            

                parameters.Add("@UplaodBy", entity.CreatedBy);
                               

                //parameters.Add("@Ref16", strReffields[20]);
                //parameters.Add("@Ref17", strReffields[21]);
                //parameters.Add("@Ref18", strReffields[22]);
                //parameters.Add("@Ref19", strReffields[23]);
                //parameters.Add("@Ref20", strReffields[19]);

                parameters.Add("@userid", entity.CreatedBy);

                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_AddEditInwardDump", param: parameters, commandType: CommandType.StoredProcedure);

                string result= parameters.Get<string>("@MSG");

            }

   //         DynamicParameters parameters = new DynamicParameters();
			//parameters.Add("@iID", entity.id);
			//parameters.Add("@BranchName", entity.BranchName);			
			//parameters.Add("@MSG","", direction: ParameterDirection.Output);		
			//SqlMapper.Execute(ConnectionString, "sp_AddEditBranch", param: parameters, commandType: CommandType.StoredProcedure);


            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
		}


        private string UpdateFileStatus(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[5];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@BatchNo", strReffields[0]);
                parameters.Add("@FileNo", strReffields[1]);
                parameters.Add("@UserID", entity.UserID);
                parameters.Add("@UplaodBy", entity.CreatedBy); 
                parameters.Add("@userid", entity.CreatedBy);

                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_FileMoveToAud", param: parameters, commandType: CommandType.StoredProcedure);

                string result = parameters.Get<string>("@MSG");

            }

        
            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }

        public string UpdateAuditorStatus(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[3];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BatchNo", strReffields[0]);
                parameters.Add("@FileNo", strReffields[1]);               
                parameters.Add("@userid", entity.CreatedBy);

                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_ReturnFromAuditor", param: parameters, commandType: CommandType.StoredProcedure);

                string result = parameters.Get<string>("@MSG");

            }


            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }

        public string CreateBulkRequest(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[5];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                if (strReffields[0] != "")
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@itemNo", strReffields[0]);
                    parameters.Add("@request_no", entity.request_number);
                    parameters.Add("@ItemCode", entity.item_code);
                    parameters.Add("@RetrivalType", entity.retrival_type);
                    parameters.Add("@delivery_type", entity.delivery_type);
                    parameters.Add("@retrieval_reason", entity.retrieval_reason);
                    parameters.Add("@remark", entity.remark);
                    parameters.Add("@userid", entity.CreatedBy);

                    parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "sp_add_retrival_details", param: parameters, commandType: CommandType.StoredProcedure);

                    string result = parameters.Get<string>("@Msg");
                    stringBuilder.AppendLine(strReffields[0] + "-" + result);
                }
            }


            return stringBuilder.ToString();
            ;  //parameters.Get<string>("@MSG");
        }


        public string UpdateRackNo(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[3];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Barcode", strReffields[0]);
                parameters.Add("@RackNo", strReffields[1]);
                parameters.Add("@userid", entity.CreatedBy);

                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_UpdateRackNo", param: parameters, commandType: CommandType.StoredProcedure);

                string result = parameters.Get<string>("@MSG");

            }


            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }

        public string AddEditDump(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[25];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                if (strReffields[2] != null )
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@appl_apac", strReffields[2] +"_"+ strReffields[3]);
                    parameters.Add("@state", strReffields[0]);
                    parameters.Add("@region", strReffields[1]);
                    parameters.Add("@appl", strReffields[2]);
                    parameters.Add("@apac", strReffields[3]);
                    parameters.Add("@contract_no", strReffields[4]);
                    parameters.Add("@apac_effective_date", strReffields[5]);
                    parameters.Add("@product", strReffields[6]);
                    parameters.Add("@location", strReffields[7]);
                    parameters.Add("@sub_lcoation", strReffields[8]);
                    parameters.Add("@tenure", strReffields[9]);
                    parameters.Add("@maturity_date", strReffields[10]);
                    parameters.Add("@maln_party_id", strReffields[11]);
                    parameters.Add("@party_name", strReffields[12]);
                    parameters.Add("@agr_value", strReffields[13]);
                    parameters.Add("@eml_start_date", strReffields[14]);
                    parameters.Add("@pdc_type", strReffields[15]);
                    parameters.Add("@userid", entity.CreatedBy);
                    parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "SP_AddEditDump", param: parameters, commandType: CommandType.StoredProcedure);
                    string result = parameters.Get<string>("@MSG");
                }
            }
            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }
        public string Barcoding(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[5];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LANNo", strReffields[0]);             
                parameters.Add("@PropertyBarcode", strReffields[1]);
                parameters.Add("@UplaodBy", entity.CreatedBy); 
                //parameters.Add("@userid", entity.CreatedBy);

                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_Barcoding", param: parameters, commandType: CommandType.StoredProcedure);

                string result = parameters.Get<string>("@MSG");
            }
            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }
        public string DumpUpload(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[17];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                if (strReffields[0].Trim() != "")
                {

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@AccountNo", strReffields[0]);
                    parameters.Add("@AppNo", strReffields[1]);
                    parameters.Add("@CRN", strReffields[2]);
                    parameters.Add("@URN", strReffields[3]);
                    parameters.Add("@DBDate", strReffields[4]);
                    parameters.Add("@DBMonth", strReffields[5]);
                    parameters.Add("@DBYear", strReffields[6]);
                    parameters.Add("@ProductCode", strReffields[7]);
                    parameters.Add("@ProductType", strReffields[8]);
                    parameters.Add("@ProductName", strReffields[9]);
                    parameters.Add("@COD_OFFICR_ID", strReffields[10]);
                    parameters.Add("@CustomerName", strReffields[11]);
                    parameters.Add("@BranchCode", strReffields[12]);
                    parameters.Add("@BranchName", strReffields[13]);
                    parameters.Add("@Zone", strReffields[14]);
                    parameters.Add("@ClosedDate", strReffields[15]);

                    parameters.Add("@UplaodBy", entity.CreatedBy);
                    //parameters.Add("@userid", entity.CreatedBy);

                    parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "AddEditUnsecuredDump", param: parameters, commandType: CommandType.StoredProcedure);

                    string result = parameters.Get<string>("@MSG");
                }

            }
            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }


        public string Invupload(DataUploadEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@request_no", "");
                parameters.Add("@UserID", entity.id);
                parameters.Add("@branch_name", entity.branch_name);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_Get_Pickup_request_number_for_inventry", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");

                StringBuilder sb = new StringBuilder();
                int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;
                for (int i = 1; i < len; i++)
                {
                    String[] strReffields = new String[15];
                    string strdata = entity.CSVData[i];
                    string[] SColData = strdata.Split(',');
                    for (int k = 0; k < SColData.Length; k++)
                    {
                        strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                    }
                    DynamicParameters parameterss = new DynamicParameters();
                    parameterss.Add("@request_no", result.ToString());
                    parameterss.Add("@lan_no", strReffields[0]);
                    parameterss.Add("@secured_unsecured", strReffields[1]);
                    parameterss.Add("@zone_name", strReffields[2]);
                    parameterss.Add("@branch_name", entity.branch_name);
                    parameterss.Add("@app_branch_code", entity.app_branch_code);
                    parameterss.Add("@applicant_name", strReffields[3]);
                    parameterss.Add("@scheme_name", strReffields[4]);
                    parameterss.Add("@smemel_product", strReffields[5]);
                    parameterss.Add("@disb_date", strReffields[6]);
                    parameterss.Add("@disb_number", strReffields[7]);
                    parameterss.Add("@total_disb_amt", strReffields[8]);
                    parameterss.Add("@pos", strReffields[9]);
                    parameterss.Add("@bal_disb_amt", strReffields[10]);
                    parameterss.Add("@carton_no", strReffields[11]);
                    parameterss.Add("@file_no", strReffields[12]);
                    parameterss.Add("@item_status", strReffields[13]);
                    parameterss.Add("@uplaod_By", entity.CreatedBy);
                    parameterss.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "SP_AvanseUploaDumpData_for_inventory", param: parameterss, commandType: CommandType.StoredProcedure);
                    string results = parameterss.Get<string>("@MSG");
                    sb.AppendLine(strReffields[0] + ":- " + results);
                    if(strReffields[13] == "In"){

                    }
                    else
                    {

                    }

                }
                return sb.ToString();
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }

              //parameters.Get<string>("@MSG");
        }

        public string DeleteRetrievalRequest(string FileNo)
        {
            return "";
        }

        public string DeleteReffilingRequest(string FileNo)
        {
            return "";
        }


        public string PDCBarcoding(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[5];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LANNo", strReffields[0]);
                parameters.Add("@PDCBarcode", strReffields[1]);              
                parameters.Add("@UplaodBy", entity.CreatedBy);
                //parameters.Add("@userid", entity.CreatedBy);

                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_PDCBarcoding", param: parameters, commandType: CommandType.StoredProcedure);

                string result = parameters.Get<string>("@MSG");
            }
            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }
         
        public bool Delete(int id, int userid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataUploadEntity> GetDetails(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TempID", id);
                IList<DataUploadEntity> resultList = SqlMapper.Query<DataUploadEntity>(ConnectionString, "SP_GetFieldsDetailsName", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DataUploadEntity> GetBranchDetailsUserWise(int id)
        {
            throw new NotImplementedException();
        }

        public string regionmappingCreate(DataUploadEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataUploadEntity> GetBranchDetailsRegionWise(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataUploadEntity> GetDetailsRegion(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataUploadEntity> GetBranchByDeptUserWise(int id, int deptid)
        {
            throw new NotImplementedException();
        }


        public string BulkUserUpload(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[10];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@name", strReffields[0]);
                parameters.Add("@userid", strReffields[1]);
                parameters.Add("@email", strReffields[2]);
                parameters.Add("@pwd", strReffields[3]);
                parameters.Add("@mobile", strReffields[4]);
                parameters.Add("@Role", strReffields[5]);
                parameters.Add("@CreatedBy", entity.CreatedBy);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_AddEditBulkUser", param: parameters, commandType: CommandType.StoredProcedure);

                string result = parameters.Get<string>("@MSG");

            }

            //         DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@iID", entity.id);
            //parameters.Add("@BranchName", entity.BranchName);			
            //parameters.Add("@MSG","", direction: ParameterDirection.Output);		
            //SqlMapper.Execute(ConnectionString, "sp_AddEditBranch", param: parameters, commandType: CommandType.StoredProcedure);


            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }

        public string uploadCSV(DataUploadEntity entity)
        {

            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;

            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[2];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@lanno", strReffields[0]);
                parameters.Add("@request_no", entity.request_no);
                parameters.Add("@request_reason", entity.request_reason);
                parameters.Add("@userid", entity.CreatedBy);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_uploadCSV", param: parameters, commandType: CommandType.StoredProcedure);

                string result = parameters.Get<string>("@MSG");

            }

            //         DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@iID", entity.id);
            //parameters.Add("@BranchName", entity.BranchName);			
            //parameters.Add("@MSG","", direction: ParameterDirection.Output);		
            //SqlMapper.Execute(ConnectionString, "sp_AddEditBranch", param: parameters, commandType: CommandType.StoredProcedure);


            return "Record Save Succesfully";  //parameters.Get<string>("@MSG");
        }
        public string AvanseUploaDumpData(DataUploadEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;
            for (int i = 1; i < len; i++)
            {
                String[] strReffields = new String[10];
                string strdata = entity.CSVData[i];
                string[] SColData = strdata.Split(',');
                for (int k = 0; k < SColData.Length; k++)
                {
                    strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                }
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@lan_no", strReffields[0]);
                parameters.Add("@secured_unsecured", strReffields[1]);
                parameters.Add("@zone_name", strReffields[2]);
                parameters.Add("@branch_name", strReffields[3]);
                parameters.Add("@app_branch_code", strReffields[4]);
                parameters.Add("@applicant_name", strReffields[5]);
                parameters.Add("@scheme_name", strReffields[6]);
                parameters.Add("@disb_date", strReffields[7]);
                //parameters.Add("@smemel_product", strReffields[7]);
                //parameters.Add("@disb_number", strReffields[9]);
                //parameters.Add("@total_disb_amt", strReffields[10]);
                //parameters.Add("@pos", strReffields[11]);
                //parameters.Add("@bal_disb_amt", strReffields[12]);
                parameters.Add("@uplaod_By", entity.CreatedBy);
                parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_AvanseUploaDumpData", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@MSG");
                sb.AppendLine(strReffields[0] + ":- " + result);
                
            }
            return sb.ToString();  //parameters.Get<string>("@MSG");
        }

        public string CrownAdd(DataUploadEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataUploadEntity> GetCrownDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataUploadEntity> GetCrownDetailsRegion(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataUploadEntity> GetCrownBranchDetailsUserWise(int id)
        {
            throw new NotImplementedException();
        }

        public bool CrownDelete(int id)
        {
            throw new NotImplementedException();
        }


        public string ItemStatusupload(DataUploadEntity entity)
        {
            try
            { 
                StringBuilder sb = new StringBuilder();
                int len = entity.CSVData.Length; //   15;//entity.CSVData.Count<Int32>;
                for (int i = 1; i < len; i++)
                {
                    String[] strReffields = new String[5];
                    string strdata = entity.CSVData[i];
                    string[] SColData = strdata.Split(',');
                    for (int k = 0; k < SColData.Length; k++)
                    {
                        strReffields[k] = SColData[k]; // t.Rows[i][k].ToString();
                    }
                    DynamicParameters parameterss = new DynamicParameters();                    
                    parameterss.Add("@file_no", strReffields[0]);
                    parameterss.Add("@item_status", strReffields[1]);
                    //parameterss.Add("@carton_no", strReffields[2]);
                    //parameterss.Add("@item_status", strReffields[3]);
                    //parameterss.Add("@applicant_name", strReffields[4]);
                    //parameterss.Add("@crown_branch", strReffields[5]);
                    //parameterss.Add("@client_branch", strReffields[6]);
                    //parameterss.Add("@branch_name", entity.branch_name);
                    //parameterss.Add("@app_branch_code", entity.app_branch_code);
                    parameterss.Add("@uplaod_By", entity.CreatedBy);
                    parameterss.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "sp_update_item_status", param: parameterss, commandType: CommandType.StoredProcedure);
                    string results = parameterss.Get<string>("@MSG");
                    sb.AppendLine(strReffields[0] + ":- " + results);
                    //if (strReffields[13] == "In")
                    //{

                    //}
                    //else
                    //{

                    //}

                }
                return sb.ToString();
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {

                throw;
            }

            //parameters.Get<string>("@MSG");
        }
        public async Task<string> GetNextRefillingAccessNumber(int userId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CreatedBy", userId);

                return await connection.ExecuteScalarAsync<string>(
                    "GetNextRefillingAccessNumber", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public string AccessUploadData(DataUploadEntity entity)
        {
            if (entity.CSVData != null && entity.CSVData?.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                int len = entity.CSVData.Length;
                for (int i = 1; i < len; i++)
                {
                    String[] strReffields = new String[15];
                    string strdata = entity.CSVData[i];
                    string[] SColData = strdata.Split(',');
                    for (int k = 0; k < SColData.Length; k++)
                    {
                        strReffields[k] = SColData[k];
                    }
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@item_number", strReffields[0]);
                    parameters.Add("@workorder_number", strReffields[1]);
                    parameters.Add("@request_number", entity.refillingAccess_number);
                    parameters.Add("@created_by", entity.created_by);
                    parameters.Add("@item_Code", entity.item_code);

                    parameters.Add("@MSG", "", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "Insert_RefilingRequest", param: parameters, commandType: CommandType.StoredProcedure);
                    string result = parameters.Get<string>("@MSG");
                    sb.AppendLine(strReffields[0] + ":- " + result);

                }
                return sb.ToString();
            }
            DynamicParameters paramet = new DynamicParameters();
            paramet.Add("@item_number", entity.itemnumber);
            paramet.Add("@workorder_number", entity.workorderNO);
            paramet.Add("@request_number", entity.refillingAccess_number);
            paramet.Add("@created_by", entity.created_by);
            paramet.Add("@item_Code", entity.item_code);

            paramet.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "Insert_RefilingRequest", param: paramet, commandType: CommandType.StoredProcedure);
            string result1 = paramet.Get<string>("@MSG");
            return result1.ToString();

        }
    }
}
