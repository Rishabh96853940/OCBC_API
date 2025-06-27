using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Repository.Interfaces
{
    public interface IEmailNotification<T> where T : class
    {
        IEnumerable<T> Get();
        T Get(int id);
        string Add(T entity);

        string InsertUpdateEmailNotification(T entity);
        string UpdatePickupRequest(T entity);
        bool DeleteEmailNotification(string request_id, int userid);


    }
}
