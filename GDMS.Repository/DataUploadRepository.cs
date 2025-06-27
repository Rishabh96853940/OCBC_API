using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Data; 
using Dapper;
using GDMS.Entities;
using GDMS.Repository.Interfaces;

namespace GDMS.Repository
{
    public class DataUploadRepository : BaseRepository, IRepository<DataUploadEntity>
    {

        public IEnumerable<DataUploadEntity> Get()
        {
            try
            {
                IList<DataUploadEntity> resultList = SqlMapper.Query<DataUploadEntity>(ConnectionString, "GetDepartment", commandType: CommandType.StoredProcedure).ToList();
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
                return SqlMapper.Query<DataUploadEntity>(ConnectionString, "GetDepartmentList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
                SqlMapper.Execute(ConnectionString, "usp_DeleteDepartment", param: parameters, commandType: CommandType.StoredProcedure);
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
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@iID", entity.id);
            parameters.Add("@DepartmentName", entity.DepartmentName);
            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "Usp_AddEditDepartment", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }
    }
}
