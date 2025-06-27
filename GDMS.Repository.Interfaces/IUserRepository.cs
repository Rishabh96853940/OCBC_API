using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IUserRepository<T> where T : class
	{
		//IEnumerable<T> GetAll();
		//T Get(int id);
		//int Add(T entity);
		//bool Delete(int id);
	        string UpdateUserToken(T entity);
		 T UserLogin(string userId, string password);

       // IEnumerable<T> UserLogin(string userId, string password);
    }
}
