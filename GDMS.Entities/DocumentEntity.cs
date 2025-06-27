

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
    public class DocumentEntity : BaseEntity
    {
        public int? Id { get; set; }

        public string DocumentType { get; set; }

        public string DetailDocumentType { get; set; }

        public int? RetentionPeriod { get; set; }


        public int? userid { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentCode { get; set; }
        public int departmentID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public int documentID { get; set; }



    }
}
