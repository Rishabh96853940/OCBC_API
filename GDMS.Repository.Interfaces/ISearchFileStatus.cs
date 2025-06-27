using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface ISearchFileStatus<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);
		string Add(T entity);
		bool Delete(int id);
        string DeleteFile(T entity);

        string DeleteFullFile(T entity);

        string favourite(T entity);
        
        string Update(T entity);

        IEnumerable<T> GetDetails(int TempID,string FileNo);

        IEnumerable<T> getSearchParameterList(int TempID);

        IEnumerable<T> getSearchData(T entity);

        IEnumerable<T> getSearchDataByFolderStructure(int ID,int TemplateID);

        IEnumerable<T> getSearchDataFilestorage(int ID, int TemplateID);
        
        IEnumerable<T> getSearchDataFSByFileNo(int ID, string FileNo);

        IEnumerable<T> GetDocumentDetails(int ID, string FileNo);

        IEnumerable<T> GetVersionDetails(string FileNo);

        IEnumerable<T> SearchBulkFile(int ID, int searchParameter, string SearchVal);        

        string DownloadFile(T entity);

        string DownloadFileForEmail(T entity);

        string UpdateShareLinkStatus(T entity);


        string DownloadVersionFile(T entity);

        string DownloadShareLinkFile(string RN, string TempPath);

        IEnumerable<T> GetBasicSearchDataByFilter(int UserID, int TemplateID, string SearchValues);

        string DownloadFileFromDB(int ID, string FileNo);


        string ViewFile(int ID, string FileNo);

        bool UpdateEmailStatus(string FileNo, string ToEmailID);
               

        string Setfavourite(T entity);
        string SetArchive(T entity);
        string UploadInserstionFiles(T entity);

        IEnumerable<T> GetGlobalSearch(int UserID, int TemplateID, string SearchValues, string TempIDList);


        IEnumerable<T> GetBulkFile(string FileNo,string ToEmailID);

        IEnumerable<T> getSearchDataByFilter(int UserID, int TemplateID, int BranchID, int SearchParamterID, string SearchValues,int SubfolderID,int DeptID);

        IEnumerable<T> GetSearchByAdvanced(int UserID, int TemplateID, int DocID, string TempIDList);

        IEnumerable<T> DeleteBulkFiles(int UserID, string FileNo);

        IEnumerable<T> DeleteMetadata(string FileNo);


        string OnDynamicFilterData(int UserID, int TemplateID, int DocID, string Condition, string SearchBy, string Operator, string SearchValues, string SearchValToDate);



        //IEnumerable<T> GetBranchDetailsUserWise(int id);

        //bool ActiveDeActive(T entity);
    }
}
