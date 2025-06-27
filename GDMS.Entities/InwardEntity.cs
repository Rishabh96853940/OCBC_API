using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class InwardEntity : BaseEntity
	{
        public int? id { get; set; }      
        public string FileNo { get; set; }
     //   public int CompanyID { get; set; }
		//public int Division { get; set; }
		public int BranchID { get; set; } 
		public int UserID { get; set; }
		public int CourierID { get; set; }
		public string DispatchStatus { get; set; }
		public string PODDispatchDate { get; set; }
		public string Ekycstatus { get; set; }
		public string AccountStatus { get; set; }
		public string SenderName { get; set; }
		public string AcknowledgeBy { get; set; }
		public string PODAckBy { get; set; }
		public string PODAckDate { get; set; }
		public string Batchstatus { get; set; }
		public string SFType { get; set; }	 
		public string Courier { get; set; }
		public string UserName { get; set; }
		public string BRInwardDate { get; set; }
		public string BRInwardBy { get; set; }
		public string PODEntrydate { get; set; }
		
		//	public int PageCount { get; set; }
		public string BatchCloseDate { get; set; }
	//	public string FileUploadDate { get; set; }
		public string FileStatus { get; set; }
		public string CRInwardDate { get; set; }
		public string CRInwardBy { get; set; }	 
		public string Status { get; set; }
		public string NewPODNo { get; set; }		
		public DateTime DATETO { get; set; }
		public DateTime DATEFROM { get; set; }
		public string Acceptdate { get; set; }

		//public string Verifydate { get; set; }

		public string DispatchDate { get; set; }
		public string InwardDate { get; set; }
	 	

		//public string Acceptdate { get; set; }
		//	public string Holddate { get; set; }
		//	public string ReHolddate { get; set; }

		//	public string FileUploadStatusremark { get; set; }

		public string AcknowledgeDate { get; set; }
		public string Janmabandhi { get; set; }
		public string BranchName { get; set; }
		public string SearchBy { get; set; } 
		public string InwardBy { get; set; }
 
		public string status { get; set; }
 
		public string LAN { get; set; }
 
		public int UserIDS { get; set; }	
		 
		public string IsMailSent { get; set; } 	   
	 
		public string BatchStatus { get; set; }
		public string Totalrecords { get; set; }
		public string BatchCreatedBy { get; set; }
		public string BatchCreatedDate { get; set; }
		public string AWBEntryBy { get; set; }
		public string PODNo { get; set; }
		public string DespatchedDate { get; set; } 
		public string CourierName { get; set; } 	 
		public string BatchNo  { get; set; } 
		public string LANNo { get; set; }	  
		public string CartonNo { get; set; }

		public string AccountNo { get; set; }
		public string AppNo { get; set; }
		public string CRN { get; set; }
		public string URN { get; set; }
		public string DBDate { get; set; }
		public string DBMonth { get; set; }
		public string DBYear { get; set; }
		public string ProductCode { get; set; }
		public string ProductType { get; set; }
		public string ProductName { get; set; }

		public string COD_OFFICR_ID { get; set; }
		public string CustomerName { get; set; }
		public string BranchCode { get; set; }
		public string ToAddress { get; set; }		
		public string Zone { get; set; }
		public string ClosedDate { get; set; }
		public int Vouchers { get; set; }
		public int Registers { get; set; }
		public int ManualReceipts { get; set; }
		public int SRforms { get; set; }
		public string TwlLoction { get; set; }
		public string BranchType { get; set; }
	}
}
