using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class RetrivalEntity : BaseEntity
	{
		public int? id { get; set; }
		public string CartonNo { get; set; }
		public string request_no { get; set; }
        public string request_number { get; set; }

        public string file_no { get; set; }
        public string carton_no { get; set; }
        public string lan_no { get; set; }
        public string document_type { get; set; }
        //pod_entry_date
        public string courier_name { get; set; }
		public string customer_name { get; set; }
		public int customer_id { get; set; }
		public string pod_number { get; set; }
		public string barcode { get; set; }
		public string request_type { get; set; }
		public string request_reason { get; set; }
		public string dispatch_address { get; set; }
		public string file_status { get; set; }
		public string request_date { get; set; }
		public string request_by { get; set; }
		public string approval_date { get; set; }
		public string approval_by { get; set; }
		public string status { get; set; }
		public string DATETO { get; set; }
		public string DATEFROM { get; set; }
		public string property_barcode { get; set; }
		public string file_path { get; set; }		
		public string file_barcode { get; set; }
		public string property_barcode_status { get; set; }
		public string file_barcode_status { get; set; }
		public string ack_by { get; set; }
		public string ack_date { get; set; }
		public string ack_path { get; set; }
		public string BatchNo { get; set; }
		public string pod_entry_date { get; set; }		
		public string Courier_id { get; set; }
		public string loan_close_file_path { get; set; }
		public string loan_close_date { get; set; }
		public string ref_pod_number { get; set; }
		public string ref_courier_name { get; set; }
		public string ref_entry_date { get; set; }
		public string ref_entry_by { get; set; }
		public string ref_ack_date { get; set; }
		public string ref_ack_by { get; set; }
		public string Attachment { get; set; }
		public string ref_request_no { get; set; }
		public string FileBarcode { get; set; }
		public string AccountType { get; set; }
		public string remark { get; set; }
		public int total_files { get; set; }
		public string branch_code { get; set; }
		public string branch_name { get; set; }
		public string product_type { get; set; }
		public string zone { get; set; }
		public string FromDate { get; set; }
		public string ToDate { get; set; }
		public string lc_filepath { get; set; }
		public string request_email { get; set; }
		public string Days { get; set; }
		public string item_number { get; set; }
		public string item_code { get; set; }
		public string retrival_type { get; set; }
		public string created_date { get; set; }

        public string request_close_date { get; set; }

        
        public string updated_by { get; set; }
        public string created_by { get; set; }
        public string updated_date { get; set; }

        public string pickup_date { get; set; }
        public string item_status { get; set; }

        public string request_status { get; set; }
        public int userid { get; set; }

        public string refilling_by { get; set; }

        public string refilling_date { get; set; }

    //    public string pod_entry_date { get; set; }

        public string pod_entry_by { get; set; } 
        public string delivery_type { get; set; }
        public string retrieval_reason { get; set; }
        public int branch_id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; } 
        public string retrival_remark { get; set; } 
        public string workorder_number { get; set; } 
        public string disb_date { get; set; } 
        public string[] upload_data { get; set; } 
        public string retrival_request_date { get; set; }

        public string page_count { get; set; } 

        public string reject_by { get; set; }
        public string reject_date { get; set; }
        public long Counts { get; set; }
        public string activity { get; set; }
        public string crown_branch { get; set; } 
        public string applicant_name { get; set; }  
		public string crown_branch_name { get; set; }
		public string FileCount { get; set; }
		public string request_close_by { get; set; }
		public string inward_collateral_file_count { get; set; }
		public string inward_main_file_count { get; set; }
		public string[] CSVData { get; set; }
		public string partically_dispatch_count { get; set; }
        public string pickup_request_no { get; set; }
        public int isInsertionAvailable { get; set; }
        public string inventory_insertion_date { get; set; }
        public string name { get; set; }
        public string service_type { get; set; }
        public string request_id { get; set; }
        public string bal_disb_amt { get; set; }
        public string scheme_name { get; set; }
        public string smemel_product { get; set; }
        public string total_disb_amt { get; set; }
        public string zone_name { get; set; }
        public string disb_number { get; set; }
        public string pos { get; set; }
        public string schedule_date { get; set; }
        public string location { get; set; }
        public string box_barcode { get; set; }
        public string account_code { get; set; }
        public string add_date { get; set; }
        public string description { get; set; }
        public string storage_type { get; set; }
        public string item_staus { get; set; }
        public string count { get; set; }
        public string app_branch_code { get; set; }
        public string InwardBy { get; set; }
        public string InwardAt { get; set; }
        public string EmailID { get; set; }
        public string Disbursement_Date { get; set; }
        public string Upload_By { get; set; }
        public string UploadAt { get; set; }
        public string main_file_count { get; set; }
        public string collateral_file_count { get; set; }
        public string MSG { get; set; }
        public int Id { get; set; }
        public string page_type { get; set; }
        public DateTime entry_date { get; set; }
        public string vehicle_number { get; set; }
        public string escort_name { get; set; }
        public string pickup_address { get; set; }
        public string address { get; set; }
        public int pickup_request_id { get; set; }
        public string pra_remark { get; set; }
        public string entry_by { get; set; }
        public string secured_unsecured { get; set; }
        public string carton_status { get; set; }
        public string escort_number { get; set; }
        public string reschedule_by { get; set; }
        public string reschedule_reason { get; set; }
        public string reschedule_done_date { get; set; }
        public string reschedule_date { get; set; }
        public string pickedup_by { get; set; }
        public string pickedup_date { get; set; }
        public string pickedup_remark { get; set; }

    }
}
