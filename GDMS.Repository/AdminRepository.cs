using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Linq;
namespace Kotak.Repository
{
	public class AdminRepository : BaseRepository, IRepository<AdminEntity>, IUserRepository<AdminEntity>
	{
		public IEnumerable<AdminEntity> Get()
		{
			try
			{
				IList<AdminEntity> resultList = SqlMapper.Query<AdminEntity>(ConnectionString, "usp_GetUsers", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{

				throw;
			}
		}


		public AdminEntity Get(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@id", id);
				return SqlMapper.Query<AdminEntity>(ConnectionString, "usp_GetUsers", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}
        
        public string Add(AdminEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@name", entity.name);
				parameters.Add("@userid", entity.userid);
				parameters.Add("@email", entity.email);
				parameters.Add("@mobile", entity.mobile);
				parameters.Add("@pwd", entity.pwd);
				parameters.Add("@remarks", entity.remarks);
				parameters.Add("@role", entity.sysRoleID);
				parameters.Add("@UserType", entity.UserType);
				parameters.Add("@AccountType", entity.AccountType);
				//parameters.Add("@BID", entity.BranchID);
				parameters.Add("@oId", "0", direction: ParameterDirection.Output);
				//("@oId", 0, direction: ParameterDirection.Output);				
				SqlMapper.Execute(ConnectionString, "sp_createUser", param: parameters, commandType: CommandType.StoredProcedure);

				return parameters.Get<string>("@oId");


			}
			catch (Exception ex)
			{
                
				throw;
			}
		}
		public string Update(AdminEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@id", entity.id);
				parameters.Add("@name", entity.name);
				parameters.Add("@userid", entity.userid);
				parameters.Add("@email", entity.email);
				parameters.Add("@mobile", entity.mobile);
				parameters.Add("@pwd", entity.pwd);
				parameters.Add("@remarks", entity.remarks);
				parameters.Add("@role", entity.sysRoleID);
				parameters.Add("@UserType", entity.UserType);
				parameters.Add("@AccountType", entity.AccountType);
				//parameters.Add("@BID", entity.BranchID);
				parameters.Add("@oId", "", direction: ParameterDirection.Output);

				SqlMapper.Execute(ConnectionString, "sp_updateUser", param: parameters, commandType: CommandType.StoredProcedure);

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
				parameters.Add("@Msg","", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "usp_DeleteUser", param: parameters, commandType: CommandType.StoredProcedure);
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

		public bool CheckToken(string Token)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Token", Token);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "SP_CheckToken", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");
				if (string.IsNullOrEmpty(val))
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
		public string UpdateUserToken(AdminEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@id", entity.id);
				parameters.Add("@oId", 0, direction: ParameterDirection.Output);

				parameters.Add("@User_Token", entity.User_Token);
				SqlMapper.Execute(ConnectionString, "sp_updateUser", param: parameters, commandType: CommandType.StoredProcedure);

				return parameters.Get<string>("@oId");

			}
			catch (Exception)
			{
				throw;
			}
		}
        public AdminEntity UserLogin(string userId, string password)
		{
			throw new NotImplementedException();
		}

        public string SFTpFileUpdate(AdminEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateFileCount(int FileCount, string CustomerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdminEntity> GetFileStatus(int RegionID, int CustomerID)
        {
            throw new NotImplementedException();
        }

        public string InsertBranchDeatils(AdminEntity entity)
        {
            throw new NotImplementedException();
        }

        public string UpdateBranchDeatils(AdminEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBranchDeatils(int id, int userid)
        {
            throw new NotImplementedException();
        }

        public AdminEntity GetbranchDeatilsById(int id)
        {
            throw new NotImplementedException();
        }

        public AdminEntity GetbranchDeatilsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<AdminEntity> IRepository<AdminEntity>.GetbranchDeatilsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdminEntity> GetCrownBranchList()
        {
            throw new NotImplementedException();
        }

        public string InsertCrownBranchDeatils(AdminEntity entity)
        {
            throw new NotImplementedException();
        }

        public AdminEntity GetCrownbranchDeatilsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
