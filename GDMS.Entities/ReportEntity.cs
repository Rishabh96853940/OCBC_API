using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
    public class ReportEntity : BaseEntity
    {

        public int? id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string request_id { get; set; }
        public string disb_date { get; set; }
        public string lan_no { get; set; }
        public string carton_no { get; set; }
        public string file_no { get; set; }

        public string document_type { get; set; }
        public string bal_disb_amt { get; set; }
        public string scheme_name { get; set; }
        public string smemel_product { get; set; }
        public string total_disb_amt { get; set; }
        public string branch_name { get; set; }
        public string zone_name { get; set; }
        public string disb_number { get; set; }
        public string applicant_name { get; set; }
        public string pos { get; set; }
        public string file_status { get; set; }

        public string schedule_date { get; set; }

        public string created_by { get; set; }
        public string created_date { get; set; }
        public string request_status { get; set; }
        public string status { get; set; }
        public string location { get; set; } 

        public string box_barcode { get; set; }
        public string account_code { get; set; }
        public string add_date { get; set; }
        public string description { get; set; }
        public string item_status { get; set; }
        public string storage_type { get; set; }
        public string retrival_request_date { get; set; }
        public string item_staus { get; set; } 
        public string count { get; set; }
        public string page_count { get; set; }
        public string crown_branch { get; set; }
        public string app_branch_code { get; set; }
        public string InwardBy { get; set; }
        public string InwardAt { get; set; }
        public string crown_branch_name { get; set; }
        public string request_number { get; set; }
        public string item_number { get; set; }
        public string retrival_remark { get; set; }
        public string item_code { get; set; }
        public string retrival_type { get; set; }
        public string request_close_by { get; set; }
        public string request_close_date { get; set; }
        public string dispatch_address { get; set; }
        public string pod_number { get; set; }
        public string delivery_type { get; set; }
        public string courier_name { get; set; }
        public string remark { get; set; }
        public string workorder_number { get; set; }
        public string approval_by { get; set; }
        public string approval_date { get; set; }
        public string pod_entry_by { get; set; }
        public string pod_entry_date { get; set; }
        public string EmailID { get; set; }
        public string Disbursement_Date { get; set; }
        public string Upload_By { get; set; }
        public string UploadAt { get; set; }


        
    }
}
