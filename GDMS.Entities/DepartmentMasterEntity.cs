

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
    public class DepartmentMasterEntity : BaseEntity
    {
        public int? Id { get; set; }
        public string DepartmentName { get; set; }

        public string DepartmentCode { get; set; }

        public int? IsActive { get; set; }

        public int? userid { get; set; }
        public int? isApproved { get; set; }

        public string UserName { get; set; }

        public int sysUserID { get; set; }
        public int departmentID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        public List<int> departmentIDs { get; set; }


        public List<int> deleteDepartmentIDs { get; set; }


        //public int id { get; set; }
    }
}
