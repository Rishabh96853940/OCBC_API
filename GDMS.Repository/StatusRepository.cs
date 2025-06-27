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
public	class StatusRepository : BaseRepository, IStatus<StatusEntity>
	{
		public IEnumerable<StatusEntity> GetStatusReport(StatusEntity obj)
		{
			try
			{
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TemplateID", obj.TemplateID);
                parameters.Add("@BranchID", obj.BranchID);
                parameters.Add("@_Flag", obj._Flag);                
                parameters.Add("@UserID", obj.CreatedBy);

                //parameters.Add("@DATETO", obj.DATETO);
                //parameters.Add("@DATEFROM", obj.DATEFROM);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "SP_StatusReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
			catch (Exception ex)
			{

				throw;
			}
		}


        public IEnumerable<StatusEntity> GetEmailLog(StatusEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                
                parameters.Add("@BranchID", obj.BranchID);               
                parameters.Add("@UserID", obj.CreatedBy);

                //parameters.Add("@DATETO", obj.DATETO);
                //parameters.Add("@DATEFROM", obj.DATEFROM);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "SP_EmailLogReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public IEnumerable<StatusEntity> GetActivityReport(StatusEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TemplateID", obj.TemplateID);
                parameters.Add("@ActiivtyID", obj.ActiivtyID);
                parameters.Add("@DATETO", obj.DATETO);
                parameters.Add("@DATEFROM", obj.DATEFROM);
                parameters.Add("@UserID", obj.CreatedBy);

                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "SP_GetActivityReports", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> TagStatusReport(StatusEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();              
                //parameters.Add("@DATETO", obj.DATETO);
                //parameters.Add("@DATEFROM", obj.DATEFROM);
                parameters.Add("@TemplateID", obj.TemplateID);
                //parameters.Add("@TagID", obj.TagID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "DocuMissingReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //--------------------------------------------------Newly added below variables

        public IEnumerable<StatusEntity> GetItemstatusreportList(StatusEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DATETO", obj.DATETO);
                parameters.Add("@DATEFROM", obj.DATEFROM);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetItemStatusReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                    return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //------------------------------------------------------------------

        public IEnumerable<StatusEntity> GetSpaceReport(StatusEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@DATETO", obj.DATETO);
                parameters.Add("@TemplateID", obj.TemplateID);
                parameters.Add("@BranchID", obj.BranchID);
                parameters.Add("@UserID", obj.CreatedBy);

                //parameters.Add("@TagID", obj.TagID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "SP_GetSpaceReport", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        

        public IEnumerable<StatusEntity> GetMetaDataReport(StatusEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@DATETO", obj.DATETO);
                //parameters.Add("@DATEFROM", obj.DATEFROM);
                parameters.Add("@TempalteID", obj.TemplateID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "MetaDataReportTempalteWise", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> GetMetaDataReportByFileNo(string FileNo,int UserID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                parameters.Add("@UserID", UserID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "MetaDataReportByFileNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> GetMetaDataReportByCustomer(StatusEntity obj)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@DATETO", obj.DATETO);
                parameters.Add("@BranchID", obj.BranchID);
                parameters.Add("@TempalteID", obj.TemplateID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetMetaDataReportByCustomer", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> GetActivityReportByFileNo(string FileNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FileNo", FileNo);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetActivityReportByFileNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
               
        public IEnumerable<StatusEntity> GetStatusCount(string FileNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", FileNo);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetStatusCount", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public IEnumerable<StatusEntity> GetStatusActivityCountPieCHart(string USERID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@USERID", USERID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetStatusActivityCountPieCHart", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> GetDashboardData(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@userid", userid);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetDashboardData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> GetDashboardFileData(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@userid", userid);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetDashboardFileuploadData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


      ////  public IEnumerable<StatusEntity> GetDashboardData(int userid)
      //  {
      //      try
      //      {
      //          DynamicParameters parameters = new DynamicParameters();
      //          parameters.Add("@userid", userid);
      //          IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetDashboardData", parameters, commandType: CommandType.StoredProcedure).ToList();
      //          return resultList;
      //      }
      //      catch (Exception ex)
      //      {

      //          throw;
      //      }
      //  }


        public IEnumerable<StatusEntity> GetActivityCount(int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@userid", userid);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetActivityCount", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public IEnumerable<StatusEntity> GetFileStorageData(int UserID , string FileNo,string parentFileNo,int TemplateID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@FileNo", FileNo);
                parameters.Add("@UserID", UserID);
                parameters.Add("@TemplateID", TemplateID);
                parameters.Add("@parentFileNo", parentFileNo);

                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetFileStorageData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public IEnumerable<StatusEntity> GetTreeStructure(int UserID,int TemplateID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();                
                parameters.Add("@UserID", UserID);
                parameters.Add("@TemplateID", TemplateID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "SP_TreeStructure", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public IEnumerable<StatusEntity> GetActivityUserLog(int userID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@userID", userID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetActivityUserLog", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> CheckAccessRight(int RoleID, string PageName)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoleID", RoleID);
                parameters.Add("@PageName", PageName);

                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "CheckAccessRight", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> DownloaddashboardData(int UserID, string Type)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);
                parameters.Add("@Type", Type);

                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "DownloaddashboardData", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public IEnumerable<StatusEntity> GetTemplateIDByFileNo(string FileNo)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@FileNo", FileNo);
                

                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetTemplateIDByFileNo", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<StatusEntity> GetFileStorageDataByGlobalSearch(int UserID, string FileNo, string parentFileNo, int TemplateID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@FileNo", FileNo);
                parameters.Add("@UserID", UserID);
                parameters.Add("@TemplateID", TemplateID);
                parameters.Add("@parentFileNo", parentFileNo);

                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetFileStorageDataByGlobalSearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public IEnumerable<StatusEntity> GetTreeStructureByGlobalSearch(int UserID, int TemplateID)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID);
                parameters.Add("@TemplateID", TemplateID);
                IList<StatusEntity> resultList = SqlMapper.Query<StatusEntity>(ConnectionString, "GetTreeStructureByGlobalSearch", parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
