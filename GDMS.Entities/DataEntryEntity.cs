using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class DataEntryEntity : BaseEntity
	{
        public int? id { get; set; }
        public int? SDID { get; set; }
        public string FileNo { get; set; }
        public string Ref1 { get; set; }
		public string Ref2 { get; set; }
		public string Ref3 { get; set; }
		public string Ref4 { get; set; }
		public string Ref5 { get; set; }
		public string Ref6 { get; set; }
		public string Ref7 { get; set; }
		public string Ref8 { get; set; }
		public string Ref9 { get; set; }
		public string Ref10 { get; set; }
		public string Ref11 { get; set; }
		public string Ref12 { get; set; }
		public string Ref13 { get; set; }
		public string Ref14 { get; set; }
		public string Ref15 { get; set; }
		public string Ref16 { get; set; }
		public string Ref17 { get; set; }
		public string Ref18 { get; set; }
		public string Ref19 { get; set; }
		public string Ref20 { get; set; }
		public string Ref21 { get; set; }
		public string Ref22 { get; set; }
		public string Ref23 { get; set; }
		public string Ref24 { get; set; }
		public string IsIndexing { get; set; }
		public int DBranchID { get; set; }
		public int DDeptID { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public int? TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string[] CSVData { get; set; }
        public string DisplayName { get; set; }
        public string IsUnique { get; set; }
        public string FieldType { get; set; }
        public string IsAuto { get; set; }
        public string IsMandatory { get; set; }
        public int BranchID { get; set; }
        public int DeptID { get; set; }
        public string FieldTypeText { get; set; }

        public string FilePath { get; set; }
        public string Status { get; set; }        
        public string MasterValues { get; set; }
        public string ColValues { get; set; }

        public int MinLenght { get; set; }
        public int MaxLenght { get; set; }
        public string IndexField { get; set; }
        public string FileList { get; set; }
        public object FVals { get; set; }

        public string RejectReason { get; set; }

        public  IDictionary<int, string> submit_data_ = new Dictionary<int, string>();

        public Dictionary<string, object> di = new Dictionary<string, object>();
        public Dictionary<int, string> submit_data__ = new Dictionary<int,string>();

        public string PhotoPath { get; set; }


        //        public object this[object key];
        //{ get; set; }

        //  public KeyValuePair(TKey key, TValue value);


    }
}
