using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Entities
{
   public class AdminEntity:BaseEntity
    {
		public int? id { get; set; }
		public string name { get; set; }
		public string userid { get; set; }
		public string email { get; set; }
		public string mobile { get; set; }
		public string pwd { get; set; }
		public string remarks { get; set; }
		public int? sysRoleID { get; set; }
        public string Role { get; set; }

		public string UserType { get; set; }
		public string AccountType { get; set; }
		public string AccountTypeID { get; set; }
		public string roleName { get; set; }

        




    }
}
