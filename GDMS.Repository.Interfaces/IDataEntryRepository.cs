using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IDataEntryRepository<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);

        IEnumerable<T> GetPendingData(int UserID);

        IEnumerable<T> GetCheckerPending(int UserID);
               

        string Add(T entity, String[] strReffields);
		bool Delete(int id);
        bool Delete(int id,int userid);
        string Update(T entity);

        string RejectFile(string FileNo , string Status,string RejectReason);

        string AutoChecker(int userID,string FileList);
        string Automaker(int userID,string FileList);

        IEnumerable<T> GetDetails(int TempID, string FileNo );
  
        IEnumerable<T> GetNextFile(int TempID, string FileNo);

        IEnumerable<T> GetFileNo(string FileNo);


        //IEnumerable<T> GetBranchDetailsUserWise(int id);

        //bool ActiveDeActive(T entity);
    }
}
