using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class BranchEntity:BaseEntity
	{
		public int? id { get; set; }
		public string BranchName { get; set; }

        public string crown_branch_name { get; set; }
        public string address { get; set; }
	//	//public int DepartmentID { get; set; }
	//	public string DepartmentName { get; set; }
	public string BranchCode { get; set; }

		//       { field: 'BranchCode', header: 'BRANCH CODE', index: 2 },
		//vendor
		public string branch_name { get; set; }
		//public string Zone { get; set; }
		//public string Region { get; set; }
		public string branch_code { get; set; }
		public int userid { get;set; }
		public bool status { get;set; }


	}
}
