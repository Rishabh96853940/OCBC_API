using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class EmailNotificationEntity : BaseEntity
	{
		public int? id { get; set; }
		public string service_type { get; set; } 
	public string to_email_id { get; set; } 
		public string cc_email_id { get; set; }
		//public string Zone { get; set; }
		//public string Region { get; set; }
		public string subject { get; set; }
        public string created_by { get; set; }
        public string body { get; set; }
        public string created_date { get; set; }
        public int userid { get;set; }
        public int branch_id { get; set; }
        public string branch_code { get; set; }
        public bool status { get;set; }


	}
}
