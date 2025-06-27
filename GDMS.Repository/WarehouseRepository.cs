using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Data; 
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;

namespace Kotak.Repository
{
    public class WarehouseRepository : BaseRepository, IWarehouse<WarehouseEntity>
    {

        public IEnumerable<WarehouseEntity> Get()
        {
            try
            {
                IList<WarehouseEntity> resultList = SqlMapper.Query<WarehouseEntity>(ConnectionString, "GetBranch", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public WarehouseEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERID", id);
                return SqlMapper.Query<WarehouseEntity>(ConnectionString, "GetDepartmentList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
 
        
        public string Add(WarehouseEntity entity)
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
        public string Update(WarehouseEntity entity)
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

        //-------------------------Warehouse starts-----------------------------
        public IEnumerable<WarehouseEntity> GetWarehouseLists()
        {
            try
            {
                IList<WarehouseEntity> resultList = SqlMapper
                    .Query<WarehouseEntity>(
                        ConnectionString,
                        "usp_GetWarehouseList",
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public string AddWarehouse(WarehouseEntity entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WarehouseName", entity.WarehouseName);
                parameters.Add("@WarehouseDescription", entity.WarehouseDescription);
                parameters.Add("@IsActive", entity.IsActive);
                parameters.Add("@userid", entity.userid);

                string message = SqlMapper
                    .Query<string>(
                        ConnectionString,
                        "usp_AddWarehouseMaster",
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                return message;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        public string UpdateWarehouse(WarehouseEntity entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@WarehouseName", entity.WarehouseName);
                parameters.Add("@WarehouseDescription", entity.WarehouseDescription);
                parameters.Add("@IsActive", entity.IsActive);
                parameters.Add("@userid", entity.userid);

                string message = SqlMapper
                    .Query<string>(
                        ConnectionString,
                        "usp_UpdateWarehouseMaster",
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                return message;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //------------------------------------------------------------------------------------------------------------
        private string AddUpdate(WarehouseEntity entity)
        {
            DynamicParameters parameters = new DynamicParameters();
         

            parameters.Add("@MSG", "", direction: ParameterDirection.Output);
            SqlMapper.Execute(ConnectionString, "sp_AddEditBranch", param: parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<string>("@MSG");
        }
        //--------------------------------------------------------------------------------------------------------------
        public bool DeleteWarehouse(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "usp_DeleteWarehouse", param: parameters, commandType: CommandType.StoredProcedure);
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


    }
}
