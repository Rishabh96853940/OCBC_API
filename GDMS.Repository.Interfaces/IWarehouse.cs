
using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
    public interface IWarehouse<T> where T : class
    {
        IEnumerable<T> GetWarehouseLists();
        string AddWarehouse(T entity);
        string UpdateWarehouse(T entity);
        bool DeleteWarehouse(int id);

    }
}
