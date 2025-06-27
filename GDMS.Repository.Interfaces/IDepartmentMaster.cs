

using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
    public interface IDepartmentMaster<T> where T : class
    {
        IEnumerable<T> GetDetails(int userid);

        IEnumerable<T> GetDepartmentMapDetailsByID(int userid);

        string AddDepartment(T entity);

        IEnumerable<T> GetDepartmentUserMappings();

        IEnumerable<T> GetSysUsers();

        IEnumerable<T> GetDepartmentsLists();
        string InsertDepartmentMapping(T entity);
        string BulkInsertDepartmentMapping(T entity);
        string UpdateDepartment(T entity);
        bool Delete(int id);
        bool DeleteDepartment(int id);

    }
}
