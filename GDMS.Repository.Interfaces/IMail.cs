using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IMail<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);
		T GetMailDetails(string FileNo);
		string Add(T entity);

		string AddEmailDetails(T entity);

		
		bool Delete(int id);
		bool Update(string FileNo);

		bool UpdateErrorLog(string FileNo,string Message);

		 

		string  DownloadFile(string FileNo);
		//IEnumerable<T> GetDetails();
		IEnumerable<T> GetBulkFile(string FileNo, string ToEmailID);

		//GetBulkFile(string FileNo)

		//bool ActiveDeActive(T entity);
	}
}
