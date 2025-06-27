using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetCrownBranchList();
        IEnumerable<T> Get();
		T Get(int id);
		string Add(T entity);
		bool Delete(int id);
		string Update(T entity);
		string SFTpFileUpdate(T entity);
		string InsertBranchDeatils(T entity);

		string InsertCrownBranchDeatils(T entity);

        
        string UpdateBranchDeatils(T entity);
        bool DeleteBranchDeatils(int id, int userid);
        T GetbranchDeatilsById(int id);

        T GetCrownbranchDeatilsById(int id);
        IEnumerable<T> GetbranchDeatilsByUserId(int id);        

        bool UpdateFileCount(int FileCount, string CustomerName);

	//	T GetFileStatus(int RegionID,int CustomerID);
		IEnumerable<T> GetFileStatus(int RegionID, int CustomerID);

	//	IEnumerable<T> GetsyncFiles(int BranchID, int DepartmentID);

		//IEnumerable<T> GetDetails();

		//bool ActiveDeActive(T entity);
	}
}
