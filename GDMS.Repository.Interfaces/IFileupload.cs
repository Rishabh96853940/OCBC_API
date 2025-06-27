using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IFileupload<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);
		string Add(T entity);
		bool Delete(int id);
		string Update(T entity);
		string SFTpFileUpdate(T entity);

		bool UpdateFileCount(int FileCount, string CustomerName, string Templatename,string strDepartmentName,string strEntityName);

	//	T GetFileStatus(int RegionID,int CustomerID);
		IEnumerable<T> GetFileStatus(int RegionID, int CustomerID);

		IEnumerable<T> GetsyncFiles(int BranchID, int DepartmentID);

		IEnumerable<T> GetTempalteList();
		IEnumerable<T> GetEntityList();

		
		//IEnumerable<T> GetDetails();

		//bool ActiveDeActive(T entity);
	}
}
