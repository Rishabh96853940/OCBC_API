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
	public class RoleRepository : BaseRepository, IRoleRepository<RoleEntity>, IUserRepository<RoleEntity>
	{
		public IEnumerable<RoleEntity> Get()
		{
			try
			{
				IList<RoleEntity> resultList = SqlMapper.Query<RoleEntity>(ConnectionString, "usp_GetRoles", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public RoleEntity Get(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@id", id);
				return SqlMapper.Query<RoleEntity>(ConnectionString, "usp_GetRoles", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}


        //public RoleEntity GetPageList(int id)
        //{
        //    try
        //    {
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("@id", id);
        //        return SqlMapper.Query<RoleEntity>(ConnectionString, "usp_GetPageList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public RoleEntity GetRightList(int id)
        //{
        //    try
        //    {
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("@id", id);
        //        return SqlMapper.Query<RoleEntity>(ConnectionString, "usp_GetRightList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        public string Add(RoleEntity entity)
		{
            string result = "";
            try
            {

               string  role_rights = entity.pageRights.Remove(entity.pageRights.Length - 1, 1);

                string[] strrolelist = entity._PageIDAndChk.ToString().Split('#');
              
                if (entity.roleName !="")
                {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", entity.id);
                parameters.Add("@roleName", entity.roleName);
                parameters.Add("@remarks", entity.remarks);
                parameters.Add("@page_rights", role_rights);
                //parameters.Add("@BID", entity.BranchID);
                parameters.Add("@oId", "", direction: ParameterDirection.Output);
                //("@oId", 0, direction: ParameterDirection.Output);				
                SqlMapper.Execute(ConnectionString, "SP_CreateRole", param: parameters, commandType: CommandType.StoredProcedure);
                result=parameters.Get<string>("@oId");
            }

                for (int i = 0; i < strrolelist.Length-1; i++)
                {
                    string[] pageid = strrolelist[i].Split(',');

                    if (pageid[1] == "true" )
                    { 
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", entity.id);
                    parameters.Add("@roleName", entity.roleName);
                    parameters.Add("@page_id", pageid[0]);
                        parameters.Add("@pass", pageid[1]);
                        //parameters.Add("@BID", entity.BranchID);
                        parameters.Add("@oId", "", direction: ParameterDirection.Output);
                    //("@oId", 0, direction: ParameterDirection.Output);				
                    SqlMapper.Execute(ConnectionString, "SP_CreateMenuRights", param: parameters, commandType: CommandType.StoredProcedure);
                    //result = parameters.Get<string>("@oId");
                }

                }
            }
			catch (Exception ex)  
			{
				throw;
			}
            if(result=="2")
            {
                result = "Role Already Exists";
            }
            else { 
            result = "Record save Succesfully.";
            }
            return result;
        }
		public string Update(RoleEntity entity)
		{
			try
			{
                string role_rights = entity.pageRights.Remove(entity.pageRights.Length - 1, 1);

                string[] strrolelist = entity._PageIDAndChk.ToString().Split('#');
                DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@id", entity.id);
                parameters.Add("@roleName", entity.roleName);
                parameters.Add("@remarks", entity.remarks);
                parameters.Add("@page_rights", role_rights);
                //parameters.Add("@BID", entity.BranchID);
                parameters.Add("@oId", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "SP_UpdateRole", param: parameters, commandType: CommandType.StoredProcedure);

				var result = parameters.Get<string>("@oId");
                if(result== "Role Name Already Exists")
                {
                    return result;
                }
                for (int i = 0; i < strrolelist.Length - 1; i++)
                {
                    string[] pageid = strrolelist[i].Split(',');

                    //if (pageid[1] == "true")
                    //{
                        DynamicParameters parameter = new DynamicParameters();
                        parameter.Add("@id", entity.id);
                        parameter.Add("@roleName", entity.roleName);
                        parameter.Add("@page_id", pageid[0]);
                        parameter.Add("@pass", pageid[1]);
                        //parameters.Add("@BID", entity.BranchID);
                        parameter.Add("@oId", "", direction: ParameterDirection.Output);
                        //("@oId", 0, direction: ParameterDirection.Output);				
                        SqlMapper.Execute(ConnectionString, "SP_CreateMenuRights", param: parameter, commandType: CommandType.StoredProcedure);
                        //result = parameters.Get<string>("@oId");
                    //}

                }
                return result;

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
				SqlMapper.Execute(ConnectionString, "usp_DeleteRolemENU", param: parameters, commandType: CommandType.StoredProcedure);
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

		public string UpdateUserToken(RoleEntity entity)
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

		public RoleEntity UserLogin(string userId, string password)
		{
			throw new NotImplementedException();
		}

        public IEnumerable<RoleEntity> GetPageList(int id )
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);              
                IList<RoleEntity> resultList = SqlMapper.Query<RoleEntity>(ConnectionString, "usp_GetPageList", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<RoleEntity> GetPageListByYserwise(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                IList<RoleEntity> resultList = SqlMapper.Query<RoleEntity>(ConnectionString, "GetPageListByYserwise", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<RoleEntity> GetRightList(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                IList<RoleEntity> resultList = SqlMapper.Query<RoleEntity>(ConnectionString, "usp_GetRightList", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
