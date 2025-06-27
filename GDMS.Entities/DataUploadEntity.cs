using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class DataUploadEntity : BaseEntity
	{
        public int? id { get; set; }
        public int? SDID { get; set; }
		public string Ref1 { get; set; }
        public string Ref2 { get; set; }
		public string Ref3 { get; set; }
		public string Ref4 { get; set; }
		public string Ref5 { get; set; }
		public string Ref6 { get; set; }
		public string Ref7 { get; set; }
		public string Ref8 { get; set; }
		public string Ref9 { get; set; }
		public string Ref10 { get; set; }
		public string Ref11 { get; set; }
		public string Ref12 { get; set; }
		public string request_reason { get; set; }
		public string request_no { get; set; }

		public int UserID { get; set; }

		

		public int? BranchName { get; set; }
        public int? DepartmentName { get; set; }
        public int? TemplateID { get; set; }
		public int? UplaodBy { get; set; }		
		public string TemplateName { get; set; }
        public string[] CSVData { get; set; }
        public string DisplayName { get; set; }
        public string IsUnique { get; set; }
        public string FieldType { get; set; }
        public string IsAuto { get; set; }

        public string IsMandatory { get; set; }

		public string PhotoPath { get; set; }

        public string request_number { get; set; }
        public string item_code { get; set; }

        public string retrival_type { get; set; }
        public string item_number { get; set; }

        public string delivery_type { get; set; }

        public string retrieval_reason { get; set; }

        public string remark { get; set; }


        public string file_no { get; set; }
        public string box_barcode { get; set; }
        public string account_code { get; set; }
        public string add_date { get; set; }
        public string description { get; set; }
        public string item_status { get; set; }
        public string lan_no { get; set; }
        public string storage_type { get; set; }
        public string secured_unsecured { get; set; }
        public string zone_name { get; set; }
        public string branch_name { get; set; }
        public string app_branch_code { get; set; }
        public string applicant_name { get; set; }
        public string scheme_name { get; set; }
        public string smemel_product { get; set; }
        public string disb_date { get; set; }
        public string disb_number { get; set; }
        public string total_disb_amt { get; set; }
        public string pos { get; set; }
        public string bal_disb_amt { get; set; }
        public string carton_no { get; set; }
        public string refillingAccess_number { get; set; }
        public string created_by { get; set; }
        public string workorderNO { get; set; }
        public string itemnumber { get; set; }



















        //  public string request_number { get; set; }

    }
}
