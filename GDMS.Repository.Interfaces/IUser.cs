using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IUser<T> where T : class
	{
		//IEnumerable<T> GetAll();
		//T Get(int id);
		//int Add(T entity);
		//bool Delete(int id);
	        string UpdateUserToken(T entity);
		//T UserLogin(string userId, string password);

        IEnumerable<T> UserLogin(T entity);

		string  Changepassword(int ID, string password, string currentpwd);
		string Logout(int ID);

		string UpdateMailStatus(string email);		

		IEnumerable<T> Forgotpassword(T entity);
		IEnumerable<T> Sendmailtocustomer(T entity);

		
	}
}
