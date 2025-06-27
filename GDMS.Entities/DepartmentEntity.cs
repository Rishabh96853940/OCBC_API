using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class DepartmentEntity : BaseEntity
	{
		public int? DID { get; set; }
		public string DepartmentName { get; set; }
		public int? ProjectID { get; set; }
		public int? id { get; set; }

		public int? UserID { get; set; }
		public int DepartmentID { get; set; }

		public string UserName { get; set; }
		public int? ischecked { get; set; }
		public string checkedList { get; set; }

		public string SFCode { get; set; }
		public string SFName { get; set; }
		public string Zone { get; set; }
		public string Region { get; set; }
		public string HubName { get; set; }
        


    }
}
