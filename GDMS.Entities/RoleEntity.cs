using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class RoleEntity : BaseEntity
    {
		public int? id { get; set; }
		public string roleName { get; set; }
		public string remarks { get; set; }
		public string page_id { get; set; }
		public string page_rights { get; set; }
		public string TaggingRights { get; set; }
        public string page_name { get; set; }
        public int? parent_id { get; set; }
        public string page_right { get; set; }
        public int? Chk { get; set; }
        public string url { get; set; }
        public string visible { get; set; }
        public string caret { get; set; }
        public object Roles { get; set; }
        public object _PageIDAndChk { get; set; }
        public bool isChecked { get; set; }
        public object _PageRight { get; set; }
        public string pageRights { get; set; }

        public int ParentID { get; set; }
        public int ChildID { get; set; }


    }
}
