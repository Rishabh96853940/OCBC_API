using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IRetrival<T> where T : class
	{
        IEnumerable<T> Get();
        T Get(int id);

        // IEnumerable<T> GetPendingData(int UserID);
        //IEnumerable<T> GetInwardData(int UserID);  
        //   IEnumerable<T> GetLANDetails(string podnO, string GetLANDetails);
        //  IEnumerable<T> GetAccountDetails(string LANNo, string BatchNo);
        //  IEnumerable<T> GetAccountdata(string AccountNo);
        IEnumerable<T> GetBatchDetails(string BatchNo, int USERId);
        IEnumerable<T> GetRetrivaldetails(int USERId); 
        IEnumerable<T> RetrievalDashboard(int USERId, string status);

        IEnumerable<T> getpoddetails(int USERId);
        IEnumerable<T> getRetrivalDispatch(int USERId);
        IEnumerable<T> getRetrivalForUdpatestatus(int USERId);        
        IEnumerable<T> getRetrivalDispatchByRequestno(int USERId, string request_no);
         

        //getRetrivalDispatchByRequestno

        IEnumerable<T> GetAck(int USERId);
        IEnumerable<T> getAckByRequestno(int USERId);
        IEnumerable<T> getAckdetailsbyRequestno(int USERId, string request_no);
        IEnumerable<T> GetloanclosebyrequestNo(int USERId, string request_no);        
        IEnumerable<T> Getloanclose(int USERId);         
        IEnumerable<T> GetFileDetails(string LanNo, int USERId);
        IEnumerable<T> GetBatchClose(string BatchNo);
        IEnumerable<T> GetDumpdataSearch(int USERId);

        string podentrydump(T entity);

        IEnumerable<T> Getbasicsearch(int USERId, string File_No);
        IEnumerable<T> PickupHistory(int USERId, string File_No);
        IEnumerable<T> SearchRecordsByFilter(int USERId, string FileNo, int SearchBy);
        IEnumerable<T> BasicSearchRecordsByFilter(int USERId, string FileNo, int SearchBy);
        IEnumerable<T> GetRefilingdata(int USERId);         
        string Add(T entity);        
        string UpdaterequestNo(T entity);
        string updateScanCopy(T entity);
        string updateLoanCopy(T entity);        

        //UpdaterequestNo
        string podentry(T entity);
        string ACkentry(T entity);
        string LoancloseCasesentry(T entity);
    
        IEnumerable<T> GetRefillingAck(int USERId);
        string RefillingAckentry(T entity);        
        bool Delete(int id);
        IEnumerable<T> GetReffilingRequestNo( string request_no ,int USERId);

        IEnumerable<T> AckAndGetReffilingRequestBYRequestNo(string request_no, int USERId);


        string RefillingAckItemNumber(string request_no, int USERId, string item_number, string workorder_number);
        IEnumerable<T> GetBarcode(string request_no);

        //GetReffilingRequestNo

        //RefRequestUdpate
        string RefRequestUdpate(T entity);
        string RefRequestUdpatePODNumber(T entity);
        string UpdatePOD(T entity); 
        string UpdateStatus(T entity);
        IEnumerable<T> getRetrivalByRequestno(int USERId, string request_no);




        IEnumerable<T> GetRetrievalReport(T entity);
        IEnumerable<T> GetRefillingReport(T entity);
        IEnumerable<T> Getoutreport(T entity);
        IEnumerable<T> getdumpsearch(T entity);
        string AddRetrivalRequest(T entity);
        string AddPickupRequest(T entity);
        
        string ClosedRetrivalRequest(string request_number,int USERId, string dispatch_address);
        
        IEnumerable<T> GetRequestNumber(int USERId,string request_number); 
            IEnumerable<T> GetApprovalByRequestNo(int USERId, string request_number);
        IEnumerable<T> GetDataByRequestNumber(int USERId, string request_number);
        string GetAvanceRetrivalRequestNo(int userid);

        IEnumerable<T> ApprovalRequestPending(int USERId);
        IEnumerable<T> GetRefilingRequest(int USERId);

        IEnumerable<T> GetRetrivalRequest(int USERId);
        IEnumerable<T> GetRefilingRequestDataByRequestNo( int USERId, string request_no);
        string AddRefilingRequest(T entity);
        IEnumerable<T> CheckRefilingRequestByItemNo(int USERId, string item_no);
        string CloseRefilingRequest(string request_number, int userid);
        IEnumerable<T> GetRefilingRequestACK(int USERId);
        IEnumerable<T> GetRefilingRequestACKByRequestNo(int USERId, string requestNo);
        IEnumerable<T> GetRetrivalPprooval(int USERId);
        string AddEditApprovalDetails(T entity);
        bool DeleteApproval(int userid);
        IEnumerable<T> GetApprovalUser(int USERId);
        IEnumerable<T> RetrievalDashboard(int USERId, string status, int timeperiod, DateTime? fromDate = null, DateTime? toDate = null);

        string refilingdumpupload(T entity);
        IEnumerable<T> GetLanDetailsQuickSearch(int USERId, string lan_no);

        //  IEnumerable<T> GetRetrivalHistory(string RequestNo, int USERId);
        //  IEnumerable<T> GetRefilingHistory(string RequestNo, int USERId);
    }
}
