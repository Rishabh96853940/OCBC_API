using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Data; 
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;

namespace Kotak.Repository
{
    public class DepartmentRepository : BaseRepository, IRepository<DepartmentEntity>
    {

        public IEnumerable<DepartmentEntity> Get()
        {
            try
            {
                IList<DepartmentEntity> resultList = SqlMapper.Query<DepartmentEntity>(ConnectionString, "GetBranch", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DepartmentEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERID", id);
                return SqlMapper.Query<DepartmentEntity>(ConnectionString, "GetDepartmentList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
 
        
        public string Add(DepartmentEntity entity)
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
        public string Update(DepartmentEntity entity)
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
                SqlMapper.Execute(ConnectionString, "SP_DeleteBranchdata", param: parameters, commandType: CommandType.StoredProcedure);
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
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@iID", entity.id);
            parameters.Add("@SFCode", entity.SFCode);
            parameters.Add("@SFName", entity.SFName);
            parameters.Add("@Zone", entity.Zone);
            parameters.Add("@Region", entity.Region);
            parameters.Add("@HubName", entity.HubName);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_AddEditBranch", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }

        public string SFTpFileUpdate(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateFileCount(int FileCount, string CustomerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetFileStatus(int RegionID, int CustomerID)
        {
            throw new NotImplementedException();
        }

        public string InsertBranchDeatils(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public string UpdateBranchDeatils(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBranchDeatils(int id, int userid)
        {
            throw new NotImplementedException();
        }

        public DepartmentEntity GetbranchDeatilsById(int id)
        {
            throw new NotImplementedException();
        }

        public DepartmentEntity GetbranchDeatilsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DepartmentEntity> IRepository<DepartmentEntity>.GetbranchDeatilsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentEntity> GetCrownBranchList()
        {
            throw new NotImplementedException();
        }

        public string InsertCrownBranchDeatils(DepartmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public DepartmentEntity GetCrownbranchDeatilsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
