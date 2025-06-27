using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Entities
{
   public class UserLoginEntity:BaseEntity
    {
		public int? id { get; set; }
		public string name { get; set; }
        public string currentpwd { get; set; }
        
        public string userid { get; set; }
		public string email { get; set; }
		public string mobile { get; set; }
		public string pwd { get; set; }
		public string remarks { get; set; }
		public int sysRoleID { get; set; }
        public string username { get; set; }
        public string password { get; set; }		
		public string FileNo { get; set; }
	 	public string AccountType { get; set; }
		public int AccountTypeID { get; set; }
		//public int CreatedBy { get; set; }

		public int Days { get; set; }


	}
}
