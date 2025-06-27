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
	public class BranchMappingRepository : BaseRepository, IMappingchkRepository<BranchMappingEntity>
	{
		public IEnumerable<BranchMappingEntity> Get()
		{
			try
			{
				IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingList", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{
				throw;
			}
		}
         
        public BranchMappingEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                //IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingList", parameters, commandType: CommandType.StoredProcedure).ToList();
                //return resultList;
               //  return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser",  commandType: CommandType.StoredProcedure).FirstOrDToListefault();

                return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        //public IEnumerable<BranchMappingEntity> GetDetails(int id)
        //{
        //    try
        //    {
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("@UserID", id);
        //        IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingList", parameters, commandType: CommandType.StoredProcedure).ToList();
        //        return resultList;


        //        //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

            
        public string Add(BranchMappingEntity entity)
		{
			try
			{
				return AddUpdate(entity);
			}
			catch (Exception)
			{
				throw;
			}
		}

        public string CrownAdd(BranchMappingEntity entity)
        {
            try
            {
                return CrownAddUpdate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Update(BranchMappingEntity entity)
		{
			try
			{
				return AddUpdate(entity);

			}
			catch (Exception)
			{
				throw;
			}
		}
        
            public bool CrownDelete(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteCrownMapping", param: parameters, commandType: CommandType.StoredProcedure);
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
        public bool Delete(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteBranchMapping", param: parameters, commandType: CommandType.StoredProcedure);
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

        public bool Delete(int Branchid, int userID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Branchid);
                parameters.Add("@userID", userID);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteBranchMappingByUserID", param: parameters, commandType: CommandType.StoredProcedure);
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

        public bool DeleteUser(int userID,int DeptID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", userID);
                parameters.Add("@DeptID", DeptID);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteUserBranchMapping", param: parameters, commandType: CommandType.StoredProcedure);
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

        public bool DeleteUserCrown(int userID, int DeptID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", userID);
                parameters.Add("@DeptID", DeptID);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteUserCrownMapping", param: parameters, commandType: CommandType.StoredProcedure);
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

        public bool DeleteRegion(int userID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", userID);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteBranchregionMapping", param: parameters, commandType: CommandType.StoredProcedure);
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

        //

        private string AddUpdate(BranchMappingEntity entity)
		{
            string[] strBranch = entity.checkedList.Split('#');

            if (strBranch.Length>0)
            {
                DeleteUser(Convert.ToInt32(entity.UserID) , Convert.ToInt32(entity.DeptID));

                for (int i=0; i < strBranch.Length-1; i++)
                { 
                DynamicParameters parameters = new DynamicParameters();
			    parameters.Add("@iID", entity.id);
			    parameters.Add("@BranchName", strBranch[i]);
			    parameters.Add("@UserName", entity.UserID);
                    parameters.Add("@DeptID", entity.DeptID);
                    parameters.Add("@userid", entity.CreatedBy);
			
			    parameters.Add("@MSG", "0", direction: ParameterDirection.Output);
			    SqlMapper.Execute(ConnectionString, "SP_AddEditBranchMapping", param: parameters, commandType: CommandType.StoredProcedure);
                }

                // return parameters.Get<string>("@MSG");
                return "Branch Addead Succesfully..";
            }
            return "Invalid Branch";
        }

        private string CrownAddUpdate(BranchMappingEntity entity)
        {
            string[] strBranch = entity.checkedList.Split('#');

            if (strBranch.Length > 0)
            {
                DeleteUserCrown(Convert.ToInt32(entity.UserID), Convert.ToInt32(entity.DeptID));

                for (int i = 0; i < strBranch.Length - 1; i++)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@iID", entity.id);
                    parameters.Add("@BranchName", strBranch[i]);
                    parameters.Add("@UserName", entity.UserID);
                    parameters.Add("@DeptID", entity.DeptID);
                    parameters.Add("@userid", entity.CreatedBy);

                    parameters.Add("@MSG", "0", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "SP_CrownAddEditBranchMapping", param: parameters, commandType: CommandType.StoredProcedure);
                }

                // return parameters.Get<string>("@MSG");
                return "Branch Addead Succesfully..";
            }
            return "Invalid Branch";
        }



        public string regionmappingCreate(BranchMappingEntity entity)
        {
            string[] strBranch = entity.checkedList.Split('#');

            if (strBranch.Length > 0)
            {
                DeleteRegion(Convert.ToInt32(entity.DeptID));

                for (int i = 0; i < strBranch.Length - 1; i++)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@iID", entity.id);
                    parameters.Add("@BranchName", strBranch[i]);
                    parameters.Add("@DeptID", entity.DeptID);
                    parameters.Add("@userid", entity.CreatedBy);

                    parameters.Add("@MSG", "0", direction: ParameterDirection.Output);
                    SqlMapper.Execute(ConnectionString, "SP_AddEditBranchRegionMapping", param: parameters, commandType: CommandType.StoredProcedure);
                }

                // return parameters.Get<string>("@MSG");
                return "Branch Addead Succesfully..";
            }
            return "Invalid Branch";
        }


        public IEnumerable<BranchMappingEntity> GetDetails(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<BranchMappingEntity> GetCrownDetails(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetCrownBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BranchMappingEntity> GetDetailsRegion(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingRegion", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }



        public IEnumerable<BranchMappingEntity> GetCrownDetailsRegion(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingRegion", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<BranchMappingEntity> GetBranchDetailsUserWise(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingList", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<BranchMappingEntity> GetCrownBranchDetailsUserWise(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetCrownBranchMappingList", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BranchMappingEntity> GetBranchDetailsRegionWise(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DeptID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchListByRegionwise", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<BranchMappingEntity> GetBranchByDeptUserWise(int id,int deptid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DeptID", deptid);
                parameters.Add("@UserID", id);
                IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchListByRegionUserwise", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Barcoding(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string PDCBarcoding(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string UpdateAuditorStatus(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string UpdateRackNo(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string DumpUpload(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string Invupload(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string BulkUserUpload(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string uploadCSV(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string AddEditDump(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string AvanseUploaDumpData(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string CreateBulkRequest(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string ItemStatusupload(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public string AccessUploadData(BranchMappingEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNextRefillingAccessNumber(int userId)
        {
            throw new NotImplementedException();
        }

        //string IMappingchkRepository<BranchMappingEntity>.regionmappingCreate(BranchMappingEntity entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
