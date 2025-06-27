using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IStatus<T> where T : class
    {
        IEnumerable<T> GetStatusReport(T entity);

        IEnumerable<T> GetEmailLog(T entity);        

        IEnumerable<T> GetMetaDataReport(T entity);

        IEnumerable<T> GetMetaDataReportByCustomer(T entity);        

        IEnumerable<T> TagStatusReport(T entity);

        IEnumerable<T> GetSpaceReport(T entity);        

        IEnumerable<T> GetActivityReport(T entity);
        IEnumerable<T> GetMetaDataReportByFileNo(string FileNo,int UserID);

        IEnumerable<T> GetActivityReportByFileNo(string FileNo);
        IEnumerable<T> GetDashboardFileData(int userID);

        IEnumerable<T> GetStatusCount(string FileNo);

        IEnumerable<T> GetStatusActivityCountPieCHart(string FileNo);

        IEnumerable<T> GetDashboardData(int userID);

        IEnumerable<T> DownloaddashboardData(int userID, string type);        

        IEnumerable<T> GetActivityCount(int Userid);
        IEnumerable<T> GetFileStorageData(int UserID , string FileNo,string parentFileNo, int TemplateID);


        IEnumerable<T> GetTemplateIDByFileNo(string FileNo);         
        IEnumerable<T> GetActivityUserLog(int Userid);

        IEnumerable<T> CheckAccessRight(int RoleID,  string PageName);

        IEnumerable<T> GetTreeStructure(int UserID, int TemplateID);

        IEnumerable<T> GetFileStorageDataByGlobalSearch(int UserID, string FileNo, string parentFileNo, int TemplateID);

        IEnumerable<T> GetTreeStructureByGlobalSearch(int UserID, int TemplateID);

        IEnumerable<T> GetItemstatusreportList(T entity);


    }
}
