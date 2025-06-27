using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IFileDetaisRepository<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);
		string Add(T entity);
		bool Delete(int id);
		string Update(T entity);
        //IEnumerable<T> GetDetails();
        IEnumerable<T> GetFileDetails(string FileNo);

        IEnumerable<T> GetNextFile(string FileNo);

		IEnumerable<T> GetPendingData(int UserID);



		//bool ActiveDeActive(T entity);
	}
}
