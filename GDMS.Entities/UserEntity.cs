using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class UserEntity
	{
		public int? id { get; set; }
		public string name { get; set; }
		public string userid { get; set; }
		public string email { get; set; }
		public string mobile { get; set; }
		public string pwd { get; set; }
		public string remarks { get; set; }
		public int? sysRoleID { get; set; }	 
		public int? BranchID { get; set; }
	}
}
