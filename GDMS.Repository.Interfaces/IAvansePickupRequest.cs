using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Repository.Interfaces
{
    public interface IAvansePickupRequest<T> where T : class
    {
        IEnumerable<T> Get(int userid, int userid2);
        
        T Get(int id);
        string Add(T entity);

        string InsertUpdatePickupRequest(T entity);
        string UpdatePickupRequest(T entity);
        bool DeletePickupRequest(string request_id, int userid);
        IEnumerable<T> GetAvansePickupRequestByACK(int UserID);
        string UpdatePickupRequestACK(T entity);

        string AddEditPickupRequestSchedule(T entity);
        IEnumerable<T> GetPickupRequestSchedule(int userid);

        IEnumerable<T> GetFileInventory(int userid);
        IEnumerable<T> GetFileInventoryQC(int userid);

        IEnumerable<T> GetFileInwardByRequestId(string request_id, int userid);
        IEnumerable<T> GetFileInwardByLaNo(string LanNo, string request_no, string document_tyype, string service_type, int UserID);
        IEnumerable<T> GetFileInventoryByCartonNo(string request_id);

        string AddEditFileInventory(T entity);

        string AddEditFileInventoryQC(T entity);
        string UpdateClosedRequest(T entity);
        IEnumerable<T> GetFileInventoryByLanNo(string LanNo);

        string UpdatePackCarton(T entity);


        string CloseRequest(T entity);

        IEnumerable<T> GetFileInventoryByRequestNo(string request_id);
        string GetAvancePickUpRequestNo(int userid,string request_no);

        string GetAvanceRefillingRequestNumber(int userid, string request_no);
        string GetCartonNoByRequestId(int userid, string request_no);
        IEnumerable<T> GetPickupRequestReport(T entity);
        IEnumerable<T> GetFileInventoryReport(T entity);

        IEnumerable<T> GetPickupRequestHistory(string RequestNo, int USERId);

        IEnumerable<T> GetPickupRequestDashboardData(int USERId, string status, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null, string monthyear = null);
        IEnumerable<T> GetPickupRequestDashboardDataCount(int USERId, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null, string monthyear = null);
        string AddPickedUpDate(T entity);




        ///  string GetFileInventoryByLanNo(https://us05web.zoom.us/j/85320629674?pwd=sMNcS6EJqJKVMH5kPSK1DMLN13FtRh.1);


    }
}
