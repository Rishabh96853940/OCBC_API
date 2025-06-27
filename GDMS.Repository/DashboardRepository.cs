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
public	class DashboardRepository : BaseRepository, IDashboard<DashboardEntity>
	{
		      
               
        public IEnumerable<DashboardEntity> GetStatusCount(string FileNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                IList<DashboardEntity> resultList = SqlMapper.Query<DashboardEntity>(ConnectionString, "GetStatusCount", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public IEnumerable<DashboardEntity> GetStatusActivityCountPieCHart(string USERID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERID", USERID);
                IList<DashboardEntity> resultList = SqlMapper.Query<DashboardEntity>(ConnectionString, "GetStatusActivityCountPieCHart", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        

        public IEnumerable<DashboardEntity> GetActivityCount(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@userid", userid);
                IList<DashboardEntity> resultList = SqlMapper.Query<DashboardEntity>(ConnectionString, "GetActivityCount", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public IEnumerable<DashboardEntity> GetFileStorageData(string FileNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@FileNo", FileNo);

                IList<DashboardEntity> resultList = SqlMapper.Query<DashboardEntity>(ConnectionString, "GetFileStorageData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<DashboardEntity> GetActivityUserLog()
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                
                IList<DashboardEntity> resultList = SqlMapper.Query<DashboardEntity>(ConnectionString, "GetActivityUserLog", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<DashboardEntity> CheckAccessRight(int RoleID, string PageName)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoleID", RoleID);
                parameters.Add("@PageName", PageName);

                IList<DashboardEntity> resultList = SqlMapper.Query<DashboardEntity>(ConnectionString, "CheckAccessRight", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }




    }
}
