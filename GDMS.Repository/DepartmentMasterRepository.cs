

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace Kotak.Repository
{
    public class DepartmentMasterRepository : BaseRepository, IDepartmentMaster<DepartmentMasterEntity>
    {

        public IEnumerable<DepartmentMasterEntity> GetDetails(int userid)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userid);

                IList<DepartmentMasterEntity> resultList = SqlMapper
                    .Query<DepartmentMasterEntity>(
                        ConnectionString,
                        "usp_GetDepartmentsBasicInfo",
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public string AddDepartment(DepartmentMasterEntity entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentName", entity.DepartmentName);
                parameters.Add("@DepartmentCode", entity.DepartmentCode);
                parameters.Add("@UserID", entity.userid);

                string message = SqlMapper
                    .Query<string>(
                        ConnectionString,
                        "usp_AddDepartmentMaster",
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


        public bool DeleteDepartment(int id)
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

        public string UpdateDepartment(DepartmentMasterEntity entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", entity.Id);
                parameters.Add("@DepartmentName", entity.DepartmentName);
                parameters.Add("@DepartmentCode", entity.DepartmentCode);
                parameters.Add("@UserID", entity.userid);

                string message = SqlMapper
                    .Query<string>(
                        ConnectionString,
                        "usp_UpdateDepartmentMaster",
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


        public IEnumerable<DepartmentMasterEntity> GetDepartmentUserMappings()
        {
            try
            {
                IList<DepartmentMasterEntity> resultList = SqlMapper
                    .Query<DepartmentMasterEntity>(
                        ConnectionString,
                        "usp_GetDepartmentUserMappings",
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public IEnumerable<DepartmentMasterEntity> GetSysUsers()
        {
            try
            {
                IList<DepartmentMasterEntity> resultList = SqlMapper
                    .Query<DepartmentMasterEntity>(
                        ConnectionString,
                        "usp_GetSysUsers",
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IEnumerable<DepartmentMasterEntity> GetDepartmentsLists()
        {
            try
            {
                IList<DepartmentMasterEntity> resultList = SqlMapper
                    .Query<DepartmentMasterEntity>(
                        ConnectionString,
                        "usp_GetDepartmentsList",
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string InsertDepartmentMapping(DepartmentMasterEntity entity)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@sysUserID", entity.sysUserID);
                parameters.Add("@departmentID", entity.departmentID);
                parameters.Add("@isApproved", entity.isApproved);
                parameters.Add("@userid", entity.userid);
                parameters.Add("@MSG", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                SqlMapper.Execute(
                    ConnectionString,
                    "usp_InsertDepartmentMapping",
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                return parameters.Get<string>("@MSG");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public string BulkInsertDepartmentMapping(DepartmentMasterEntity model)
        //{
        //    foreach (var deptId in model.departmentIDs)
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@sysUserID", model.sysUserID);
        //        parameters.Add("@departmentID", deptId);
        //        parameters.Add("@isApproved", model.isApproved);
        //        parameters.Add("@userid", model.userid);
        //        parameters.Add("@MSG", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

        //        SqlMapper.Execute(
        //            ConnectionString,
        //            "usp_InsertDepartmentMapping",
        //            param: parameters,
        //            commandType: CommandType.StoredProcedure
        //        );
        //        // Optionally collect each message if needed
        //    }

        //    return "Bulk Insert Completed";
        //}

        //public string BulkInsertDepartmentMapping(DepartmentMasterEntity model)
        //{
        //    var messages = new List<string>();


        //    if (model.deleteDepartmentIDs != null && model.deleteDepartmentIDs.Any())
        //    {
        //        foreach (var deptId in model.deleteDepartmentIDs)
        //        {
        //            var delParams = new DynamicParameters();
        //            delParams.Add("@sysUserID", model.sysUserID);
        //            delParams.Add("@departmentID", deptId);
        //            delParams.Add("@userid", model.userid);
        //            delParams.Add("@MSG", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

        //            SqlMapper.Execute(
        //                ConnectionString,
        //                "usp_DeleteDepartmentMapping",
        //                param: delParams,
        //                commandType: CommandType.StoredProcedure
        //            );

        //            messages.Add($"Delete: {delParams.Get<string>("@MSG")}");
        //        }
        //    }


        //    if (model.departmentIDs != null && model.departmentIDs.Any())
        //    {
        //        foreach (var deptId in model.departmentIDs)
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@sysUserID", model.sysUserID);
        //            parameters.Add("@departmentID", deptId);
        //            parameters.Add("@isApproved", model.isApproved);
        //            parameters.Add("@userid", model.userid);
        //            parameters.Add("@MSG", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

        //            SqlMapper.Execute(
        //                ConnectionString,
        //                "usp_InsertDepartmentMapping",
        //                param: parameters,
        //                commandType: CommandType.StoredProcedure
        //            );

        //            messages.Add($"Insert: {parameters.Get<string>("@MSG")}");
        //        }
        //    }

        //    return string.Join(" | ", messages);
        //}

        public string BulkInsertDepartmentMapping(DepartmentMasterEntity model)
        {
            try
            {
                if (model.deleteDepartmentIDs != null && model.deleteDepartmentIDs.Any())
                {
                    foreach (var deptId in model.deleteDepartmentIDs)
                    {
                        var delParams = new DynamicParameters();
                        delParams.Add("@sysUserID", model.sysUserID);
                        delParams.Add("@departmentID", deptId);
                        delParams.Add("@userid", model.userid);
                        delParams.Add("@MSG", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                        SqlMapper.Execute(
                            ConnectionString,
                            "usp_DeleteDepartmentMapping",
                            param: delParams,
                            commandType: CommandType.StoredProcedure
                        );
                    }
                }

                if (model.departmentIDs != null && model.departmentIDs.Any())
                {
                    foreach (var deptId in model.departmentIDs)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@sysUserID", model.sysUserID);
                        parameters.Add("@departmentID", deptId);
                        parameters.Add("@isApproved", model.isApproved);
                        parameters.Add("@userid", model.userid);
                        parameters.Add("@MSG", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                        SqlMapper.Execute(
                            ConnectionString,
                            "usp_InsertDepartmentMapping",
                            param: parameters,
                            commandType: CommandType.StoredProcedure
                        );
                    }
                }

                return "Department Mapping Updated Successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        public IEnumerable<DepartmentMasterEntity> GetDepartmentMapDetailsByID(int userid)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", userid);

                IList<DepartmentMasterEntity> resultList = SqlMapper
                    .Query<DepartmentMasterEntity>(
                        ConnectionString,
                        "usp_GetDepartmentsMapByUserID",
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                return resultList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




    }
}
