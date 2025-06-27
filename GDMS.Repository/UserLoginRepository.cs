using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kotak.Repository
{
	public class UserLoginRepository : BaseRepository, IUser<UserLoginEntity>
	{
		//public IEnumerable<UserLoginEntity> Get()
		//{
		//	try
		//	{
		//		IList<UserLoginEntity> resultList = SqlMapper.Query<UserLoginEntity>(ConnectionString, "usp_GetUsers", commandType: CommandType.StoredProcedure).ToList();
		//		return resultList;
		//	}
		//	catch (Exception)
		//	{

		//		throw;
		//	}
		//}

		//public UserLoginEntity Get(int id)
		//{
		//	try
		//	{
		//		DynamicParameters parameters = new DynamicParameters();
		//		parameters.Add("@id", id);
		//		return SqlMapper.Query<UserLoginEntity>(ConnectionString, "usp_GetUsers", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}
        
  //      public string Add(UserLoginEntity entity)
		//{
		//	try
		//	{
		//		DynamicParameters parameters = new DynamicParameters();
		//		parameters.Add("@username", entity.username);
		//		parameters.Add("@password", entity.password);			 
		//		//parameters.Add("@BID", entity.BranchID);
		//		parameters.Add("@oId", "", direction: ParameterDirection.Output);
		//		//("@oId", 0, direction: ParameterDirection.Output);				
		//		SqlMapper.Execute(ConnectionString, "sp_CheckUserLogin", param: parameters, commandType: CommandType.StoredProcedure);

		//		return parameters.Get<string>("@oId");
		//	}
		//	catch (Exception ex)
		//	{                
		//		throw;
		//	}
		//}
		public string Update(UserLoginEntity entity)
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

		public string UpdateUserToken(UserLoginEntity entity)
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


  //      public UserLoginEntity UserLogin(string userId, string password)
		//{
  //          try
  //          {
  //              DynamicParameters parameters = new DynamicParameters();
  //              parameters.Add("@username", userId);
  //              parameters.Add("@password", password);
  //              return SqlMapper.Query<UserLoginEntity>(ConnectionString, "sp_CheckUserLogin", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
  //          }
  //          catch (Exception)
  //          {
  //              throw;
  //          }

  //      }
        
        public IEnumerable<UserLoginEntity> UserLogin(UserLoginEntity entity)
        {
            try
            {
				string token = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

				DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@username", entity.username);
                parameters.Add("@password", entity.password);
				parameters.Add("@token", token);
				IList<UserLoginEntity> resultList = SqlMapper.Query<UserLoginEntity>(ConnectionString, "sp_CheckUserLogin", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

		public IEnumerable<UserLoginEntity> Forgotpassword(UserLoginEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@username", entity.username);			
				IList<UserLoginEntity> resultList = SqlMapper.Query<UserLoginEntity>(ConnectionString, "SP_Forgotpassword", parameters, commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{

				throw;
			}
		}


		public IEnumerable<UserLoginEntity> Sendmailtocustomer(UserLoginEntity entity)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@username", entity.username);
				IList<UserLoginEntity> resultList = SqlMapper.Query<UserLoginEntity>(ConnectionString, "SP_SendMailToCustomer", parameters, commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{

				throw;
			}
		}		 

		public string UpdateMailStatus(string EmailID)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@EmailID", EmailID);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "sp_UpdateMailStatus", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");
				if (!string.IsNullOrEmpty(val))
				{
					return "Mail sent";
				}
				return "";
			}
			catch (Exception)
			{
				throw;
			}
		}

		public string Logout(int ID)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@ID", ID);				 
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "SP_Logout", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");
				if (!string.IsNullOrEmpty(val))
				{
					return "Password reset";
				}
				return "";
			}
			catch (Exception)
			{
				throw;
			}
		}

		public string Changepassword(int ID, string password, string currentpwd)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@ID", ID);
                parameters.Add("@currentpwd", currentpwd);
                parameters.Add("@password", password);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "SP_Changepassword", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");
				if (!string.IsNullOrEmpty(val))
				{
					return val;
				}
				return "";
			}
			catch (Exception)
			{
				throw;
			}
		}



	}
}
