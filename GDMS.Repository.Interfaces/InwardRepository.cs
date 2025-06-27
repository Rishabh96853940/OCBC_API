using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface InwardRepository<T> where T : class
	{
        IEnumerable<T> Get();
        T Get(int id);

       // IEnumerable<T> GetPendingData(int UserID);
        IEnumerable<T> GetInwardData(int UserID);  
     //   IEnumerable<T> GetLANDetails(string podnO, string GetLANDetails);
        IEnumerable<T> GetAccountDetails(string LANNo, string BatchNo, string UserID);
        IEnumerable<T> GetAccountdata(string AccountNo,string UserID);
        

        // IEnumerable<T> GetLANDetailsByUser(string LANNo, int UserID);
        IEnumerable<T> GetLANDetailsByUserAndBatchNo(string LANNo,string BatchNo, int UserID);
        IEnumerable<T> GetBatchDetails(string BatchNo, int USERId);
        IEnumerable<T> SearchRecordsByFilter(string FileNo, string SearchBy,string UserID); 
        
        IEnumerable<T> GetBatchClose(string BatchNo);
        IEnumerable<T> GetPODDetailsEntry(string UserID); 
        IEnumerable<T> GetPODDetailsFulletron();
        IEnumerable<T> GetDumpdataSearch(string UserID);
      //  IEnumerable<T> GetPODDetailsFulletronACK();
        IEnumerable<T> GetPODDetailsACK(string UserID);
        
        //     IEnumerable<T> GetPODByLAN(string podnO);
        IEnumerable<T> GetBatchDetails(string BatchNo);
        //string Add(T entity, String[] strReffields);
      //  string PODdetailsEntry(T entity);
        string PODAcknowledge(T entity);

        string SendBackBranch(T entity);

        string updatefilestatus(T entity); 
        string AcknowledgePOD(T entity);
        string UpdateReceivedPOD(T entity);
        string InwardEntry(T entity);
        string PODEntry(T entity);


        string PODdetailsEntry(T entity);        

        // bool Delete(string Territorycode);
        // bool DeleteBarcode(string Territorycode);
        // bool Delete(int id, int userid);
        string Update(T entity);
        IEnumerable<T> GetDetails(int TempID, string FileNo);
        IEnumerable<T> GetStatusReport(T entity);
        IEnumerable<T> GetPendingData(string UserID); 
        
        IEnumerable<T> GetPODReport(T entity);
        IEnumerable<T> GetBatchDetailsReport(T entity);         
        IEnumerable<T> GetInwardREport(T entity);
        IEnumerable<T> GetPendingINVData();
        
        IEnumerable<T> GetFileStatusReport(T entity);         

        //IEnumerable<T> GetFileNo(string FileNo, string CompanyID);
        IEnumerable<T> GetBatchdetailsByBatchNo(string BatchNo);

        
   
    }
}
