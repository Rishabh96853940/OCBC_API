using Kotak.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
    public class AvansePickupRequestEntity : BaseEntity
    {
        public string MSG { get; set; }
        public int id { get; set; }
        public int Id { get; set; }
        public string page_type { get; set; }
        public string service_type { get; set; }
        public string lan_no { get; set; }
        public string document_type { get; set; }
        public int main_file_count { get; set; }
        public int collateral_file_count { get; set; }
        public DateTime entry_date { get; set; }
        public bool status { get; set; }
        public string request_date { get; set; }
        public string request_by { get; set; }
        public string request_id { get; set; }
        public int userid { get;set; }
        public int branch_id { get;set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public string ack_by { get; set; }
        public string ack_date { get; set; }

        public string created_by { get; set; }
        public string created_date { get; set; }
        public string schedule_date { get; set; }
        public string vehicle_number { get; set; }
        public string escort_name { get; set; }
        public string pickup_address { get; set; }

        public string address { get; set; }

        public int pickup_request_id { get; set; }
        public string remark { get; set; }

        public string pra_remark { get; set; }

        public string carton_no { get; set; }
        public string file_no { get; set; }

        public string entry_by { get; set; }

        public long Counts { get; set; }

        public string activity { get; set; }

        public string secured_unsecured { get; set; }

        public string zone_name { get; set; }
        public string disb_number { get; set; }
        public string app_branch_code { get; set; }
        public string applicant_name { get; set; }
        public string total_disb_amt { get; set; }
        public string pos { get; set; }
        public string bal_disb_amt { get; set; }
        public string scheme_name { get; set; }
        public string smemel_product { get; set; }
        public string disb_date { get; set; }

        public string request_status { get; set; }
        public string carton_status { get; set; }
        public string escort_number { get; set; }
        public string reschedule_by { get; set; }
        public string reschedule_reason { get; set; }
        public string reschedule_done_date{ get;set; }
        public string reschedule_date{ get;set; }
        public string FromDate{ get;set; }
        public string ToDate{ get;set; }
        public string crown_branch_name { get;set; }
        public string request_close_by { get; set; }
        public string inward_collateral_file_count { get; set; }
        public string inward_main_file_count { get; set; }
        public string pickedup_by { get; set; }
        public string pickedup_date { get; set; }
        public string pickedup_remark { get; set; }
        public string CreatedBy { get; set; }
        public string InwardBy { get; set; }




    }
}
