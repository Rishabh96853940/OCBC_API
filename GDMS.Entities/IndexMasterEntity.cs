using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class IndexMasterEntity:BaseEntity
	{
		public int? id { get; set; }
		public string IndexField { get; set; }
		public string DisplayName { get; set; }
		public int? DepartmentID { get; set; }		 
		public int FieldType { get; set; }
		public string Remark { get; set; }
		public int? MinLenght { get; set; }
        public int? MaxLenght { get; set; }
        public string ListData { get; set; }
        public int IsMandatory { get; set; }
        public string TemplateName { get; set; }
        public int? TemplateID { get; set; }
        public int IsAuto { get; set; }
        public int BranchID { get; set; }
        public int DeptID { get; set; }
        public string FieldTypeText { get; set; }

        public string MasterValues { get; set; }
    }
}
