using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IRoleRepository<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);

        //T GetPageList(int id);
        //T GetRightList(int id);
        string Add(T entity);
		bool Delete(int id);
		string Update(T entity);

        IEnumerable<T> GetPageList(int id);
        IEnumerable<T> GetRightList(int id);
        IEnumerable<T> GetPageListByYserwise(int id);

        

        //bool ActiveDeActive(T entity);
    }
}
