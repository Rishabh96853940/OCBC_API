using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface ICustomer<T> where T : class
    {

        IEnumerable<T> Get();
        T Get(int id); 
        string Add(T entity);      
        
        IEnumerable<T> GetDetails(int CustID);
        IEnumerable<T> GetCutomerData();
        IEnumerable<T> GetState();
        IEnumerable<T> GetSales();
        IEnumerable<T> GetGDS();
        string CreateSendRequest(T entity);
        


    }
}
