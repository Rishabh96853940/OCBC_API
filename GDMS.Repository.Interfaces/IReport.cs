using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Repository.Interfaces
{
    public interface IReport<T> where T : class
    {

        IEnumerable<T>  GetFileInventoryReport(T entity);
        IEnumerable<T> GetItemStatusReport(T entity);
        IEnumerable<T> GetDumpReport(T entity);
        IEnumerable<T> GetFileInventoryReportByDisDate(T entity);

        //Task<IEnumerable<T>> GetFileInventoryReportAsync(T entity);

        IEnumerable<T> GetFileInventoryUploadReport(T entity);
    }
}
