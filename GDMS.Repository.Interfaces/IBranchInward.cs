using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IBranchInward<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);
		string Add(T entity);
		bool Delete(int id);
		string Update(T entity);
		string UpdateBatchNo(T entity); 
		IEnumerable<T> GetBatchDetails(string BatchID, string UserId );
		IEnumerable<T> getPODdetails(string UserId); 
		IEnumerable<T> GetAppacDetails(T entity);
		//string AddEditAppacdetails(T entity);
		string PODdetailsEntry(T entity);
		IEnumerable<T> GetBatchdetailsByBatchNo(string BatchNo);
		IEnumerable<T> getPODAckPending(string UserId);
		//getPODAckPending

		string PODAckdetailsEntry(T entity);

		IEnumerable<T> getDocumentdetails(string appl, string apac);
		bool DeleteApack(string batch_no, string appl, string apac);

	}
}
