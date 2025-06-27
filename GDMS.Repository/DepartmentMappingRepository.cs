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
	public class DepartmentMappingRepository : BaseRepository, IMappingchkRepository<DepartmentEntity>
	{
		public IEnumerable<DepartmentEntity> Get()
		{
			try
			{
				IList<DepartmentEntity> resultList = SqlMapper.Query<DepartmentEntity>(ConnectionString, "SP_GetDepartmentMappingList", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception)
			{
				throw;
			}
		}

		
        public DepartmentEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                //IList<BranchMappingEntity> resultList = SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingList", parameters, commandType: CommandType.StoredProcedure).ToList();
                //return resultList;
               //  return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser",  commandType: CommandType.StoredProcedure).FirstOrDToListefault();

                return SqlMapper.Query<DepartmentEntity>(ConnectionString, "SP_GetDepartmentMappingList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

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

            
        public string Add(DepartmentEntity entity)
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
		public string Update(DepartmentEntity entity)
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
		public bool Delete(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Id", id);
				parameters.Add("@Msg","", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "usp_DeleteDepartmentMapping", param: parameters, commandType: CommandType.StoredProcedure);
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
                SqlMapper.Execute(ConnectionString, "usp_DeleteDepartmentMappingByUserID", param: parameters, commandType: CommandType.StoredProcedure);
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

        public bool DeleteUser(int userID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", userID);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteUserDepartmentMapping", param: parameters, commandType: CommandType.StoredProcedure);
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

        private string AddUpdate(DepartmentEntity entity)
		{
            string[] strBranch = entity.checkedList.Split('#');

            if (strBranch.Length>0)
            {
                DeleteUser(Convert.ToInt32(entity.UserID));

                for (int i=0; i < strBranch.Length-1; i++)
                { 
                DynamicParameters parameters = new DynamicParameters();
			    parameters.Add("@iID", entity.id);
			    parameters.Add("@DepartmentName", strBranch[i]);
			    parameters.Add("@UserName", entity.UserID);
			    parameters.Add("@userid", entity.CreatedBy);
			
			    parameters.Add("@MSG", "0", direction: ParameterDirection.Output);
			    SqlMapper.Execute(ConnectionString, "SP_AddEditDepartmentMapping", param: parameters, commandType: CommandType.StoredProcedure);
                }

                // return parameters.Get<string>("@MSG");
                return "Branch Addead Succesfully..";
            }
            return "Invalid Branch";
        }

        public IEnumerable<DepartmentEntity> GetDetails(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<DepartmentEntity> resultList = SqlMapper.Query<DepartmentEntity>(ConnectionString, "SP_GetDepartmentMappingUser", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DepartmentEntity> GetBranchDetailsUserWise(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<DepartmentEntity> resultList = SqlMapper.Query<DepartmentEntity>(ConnectionString, "SP_GetDepartmentMappingList", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string regionmappingCreate(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetBranchDetailsRegionWise(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetDetailsRegion(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", id);
                IList<DepartmentEntity> resultList = SqlMapper.Query<DepartmentEntity>(ConnectionString, "SP_GetDepartmentByUser", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;


                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<DepartmentEntity> GetBranchByDeptUserWise(int id, int deptid)
        {
            throw new NotImplementedException();
        }

        public string Barcoding(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string PDCBarcoding(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string UpdateAuditorStatus(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string UpdateRackNo(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string DumpUpload(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string Invupload(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string BulkUserUpload(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string uploadCSV(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string AddEditDump(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string AvanseUploaDumpData(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string CreateBulkRequest(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string CrownAdd(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetCrownDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetCrownDetailsRegion(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetCrownBranchDetailsUserWise(int id)
        {
            throw new NotImplementedException();
        }

        public bool CrownDelete(int id)
        {
            throw new NotImplementedException();
        }

        public string ItemStatusupload(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string AccessUploadData(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNextRefillingAccessNumber(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
