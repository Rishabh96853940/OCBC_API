using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class BranchInwardEntity: BaseEntity
	{
	 	public int id { get; set; }
		public int UserID { get; set; }
		public string batch_no { get; set; }
		public string document_type { get; set; }
		public string apac { get; set; }
		public string appl_apac { get; set; }	 
		public string appl { get; set; }
		public string pod_no { get; set; }
		public string courier_name { get; set; }
		public string pod_dispatch_date { get; set; }
		public string entry_date { get; set; }
		public string entry_by { get; set; }
		public string BatchNo { get; set; }
		public string message { get; set; } 
		public string product { get; set; }
		public string location { get; set; }
		public string sub_lcoation { get; set; }
		public string tenure { get; set; }
		public string maturity_date { get; set; }
		public string maln_party_id { get; set; }
		public string party_name { get; set; }
		public string agr_value { get; set; }
		public string eml_start_date { get; set; }
		public string pdc_type { get; set; }
		public string contract_no { get; set; }
		public string apac_effective_date { get; set; }
		public string region { get; set; }
		public string state { get; set; }
		public string status { get; set; }
		public int CourierID { get; set; }
		public string InwardBy { get; set; }
		public string pod_ack_by { get; set; }
		public string pod_ack_date { get; set; }
		public string new_pod_no { get; set; }

		
	}
}
