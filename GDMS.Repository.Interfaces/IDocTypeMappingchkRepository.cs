using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IDocTypeMappingchkRepository<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);
		string Add(T entity);
		bool Delete(int id);
        bool Delete(int id,int userid);
        string Update(T entity);

        //IEnumerable<T> GetDetails(int id);

        IEnumerable<T> GetDetailsByID(int id,int TemplateID);
         
              IEnumerable<T> getDoctypeListByTempID(int id, int TemplateID);
        IEnumerable<T> GetBranchDetailsUserWise(int id);

        //bool ActiveDeActive(T entity);
    }
}
