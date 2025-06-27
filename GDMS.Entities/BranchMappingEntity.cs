using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class BranchMappingEntity:BaseEntity
	{
		public int? id { get; set; }
		public int? BranchID { get; set; }
		public int UserID	{ get; set; }
        public string BranchName { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
                
        public int? ischecked { get; set; }
        public string checkedList { get; set; }
        public int DeptID { get; set; }
        public string SFCode { get; set; }
        public string SFName { get; set; }
      //  public string BranchName { get; set; }
        public string branch_code { get; set; }
        public string branch_name { get; set; }

        
    }
}
