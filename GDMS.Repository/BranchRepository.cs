using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;

namespace Kotak.Repository
{
public	class BranchRepository : BaseRepository, IRepository<BranchEntity>
	{
        
        public IEnumerable<BranchEntity> Get()
		{
			try
			{
				IList<BranchEntity> resultList = SqlMapper.Query<BranchEntity>(ConnectionString, "GetBranches", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

        public IEnumerable<BranchEntity> GetCrownBranchList()
        {
            try
            {
                IList<BranchEntity> resultList = SqlMapper.Query<BranchEntity>(ConnectionString, "GetCrownBranch", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public BranchEntity Get(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@USERID", id);
				return SqlMapper.Query<BranchEntity>(ConnectionString, "GetBranchList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}

              
        public string Add(BranchEntity entity)
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
		public string Update(BranchEntity entity)
		{
			try
			{
				return InsertBranchDeatils(entity);				

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

                if (val.Trim()== "Deleted successfully")
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

		private string AddUpdate(BranchEntity entity)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@iID", entity.id);
			parameters.Add("@BranchName", entity.BranchName);
			parameters.Add("@BranchCode", entity.BranchCode);
			parameters.Add("@address", entity.address);
			parameters.Add("@MSG","", direction: ParameterDirection.Output);		
			SqlMapper.Execute(ConnectionString, "sp_AddEditBranch", param: parameters, commandType: CommandType.StoredProcedure);

			return parameters.Get<string>("@MSG");
		}

		public string SFTpFileUpdate(AdminEntity entity)
		{
			throw new NotImplementedException();
		}

        public string SFTpFileUpdate(BranchEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateFileCount(int FileCount, string CustomerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BranchEntity> GetFileStatus(int RegionID, int CustomerID)
        {
            throw new NotImplementedException();
        }

        public string InsertBranchDeatils(BranchEntity entity)
        {
            try
			{
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@iID", entity.id);
                parameters.Add("@branch_code", entity.branch_code);
                parameters.Add("@branch_name", entity.branch_name);
                parameters.Add("@address", entity.address);
                parameters.Add("@crown_branch_name", entity.crown_branch_name);
                parameters.Add("@created_by", entity.userid);          
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);              
                SqlMapper.Execute(ConnectionString, "SP_InsertBranchDeatils", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Msg");
                return result;
            }
			catch(Exception )
			{
				throw;
			}
        }


        public string InsertCrownBranchDeatils(BranchEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@iID", entity.id);
                parameters.Add("@crown_branch_name", entity.crown_branch_name);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "SP_InsertCrownBranchDeatils", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Msg");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string UpdateBranchDeatils(BranchEntity entity)
        {
            //string token = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            //   int request_id = random.Next(100000, 1000000);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@branch_code", entity.branch_code);
            parameters.Add("@branch_name", entity.branch_name);
            parameters.Add("@id", entity.id);
            //parameters.Add("@main_file_count", entity.main_file_count);
          //  parameters.Add("@collateral_file_count", entity.collateral_file_count);
            //parameters.Add("@satus", entity.collateral_file_count);
            parameters.Add("@updated_by", entity.userid);

            parameters.Add("@Msg", "", direction: ParameterDirection.Output);

            SqlMapper.Execute(ConnectionString, "SP_UpdateBranchDeatil", param: parameters, commandType: CommandType.StoredProcedure);
            string result = parameters.Get<string>("@Msg");
            return result;
        }

        public bool DeleteBranchDeatils(int id,int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                parameters.Add("@delete_by", userid);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteBranchDeatils", param: parameters, commandType: CommandType.StoredProcedure);
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

        public BranchEntity GetbranchDeatilsById(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return SqlMapper.Query<BranchEntity>(ConnectionString, "GetBranchesdeatilsById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

            
        }

        public BranchEntity GetCrownbranchDeatilsById(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return SqlMapper.Query<BranchEntity>(ConnectionString, "GetCrownBranchesdeatilsById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }


        }

        public IEnumerable<BranchEntity> GetbranchDeatilsByUserId(int User_Id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", User_Id);
                IList<BranchEntity> resultList = SqlMapper.Query<BranchEntity>(ConnectionString, "SP_GetbranchDeatilsByUserId", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //  return SqlMapper.Query<BranchEntity>(ConnectionString, "SP_GetbranchDeatilsByUserId", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
