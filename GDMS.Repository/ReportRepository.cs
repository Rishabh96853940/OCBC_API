using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Threading.Tasks;

namespace Kotak.Repository
{
    public class ReportRepository : BaseRepository, IReport<ReportEntity>
    {

        public IEnumerable<ReportEntity> Get()
        {
            try
            {
                IList<ReportEntity> resultList = SqlMapper.Query<ReportEntity>(ConnectionString, "GetBranch", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public IEnumerable<ReportEntity> GetFileInventoryReport(ReportEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<ReportEntity> resultList = SqlMapper.Query<ReportEntity>(ConnectionString, "GetFileInventoryReport", parameters, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
                //return SqlMapper.Query<ReportEntity>(ConnectionString, "GetFileInventoryReport", parameters, commandType: CommandType.StoredProcedure, commandTimeout: 120);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<ReportEntity> GetFileInventoryReportByDisDate(ReportEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<ReportEntity> resultList = SqlMapper.Query<ReportEntity>(ConnectionString, "GetFileInventoryReportByDisDate", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        public IEnumerable<ReportEntity> GetFileInventoryUploadReport(ReportEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<ReportEntity> resultList = SqlMapper.Query<ReportEntity>(ConnectionString, "GetFileInventoryUploadReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        } 

        public IEnumerable<ReportEntity> GetDumpReport(ReportEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<ReportEntity> resultList = SqlMapper.Query<ReportEntity>(ConnectionString, "SP_GetDumpReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
                //return SqlMapper.Query<BranchMappingEntity>(ConnectionString, "SP_GetBranchMappingUser", parameters, commandType: CommandType.StoredProcedure).FirstOrDToListefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ReportEntity> GetItemStatusReport(ReportEntity entity)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERId", entity.CreatedBy);
                parameters.Add("@FromDate", entity.FromDate);
                parameters.Add("@ToDate", entity.ToDate);
                parameters.Add("@status", entity.status);
                IList<ReportEntity> resultList = SqlMapper.Query<ReportEntity>(ConnectionString, "sp_GetItemStatusReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {
                throw;
            }
        } 
 
    }
}
