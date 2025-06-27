using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class FileUploadEntity : BaseEntity
    {
		public int ID { get; set; }
        public int id { get; set; }
        public string OFileName { get; set; }
		public string nFileName { get; set; }
		public string FilePath { get; set; }
		public string IsLocked { get; set; }
		public int? LockedBy { get; set; }
		public string IsIndexing { get; set; }
		public string EntryDate { get; set; }
		public string IsDigi { get; set; }
		public int PageCount { get; set; }
		public int EntryBy { get; set; }

		public int FileCount { get; set; }				
		public int DocType { get; set; }
		public int UplaodBy { get; set; }		
		public string EMPNo { get; set; }
		public int BranchID { get; set; }
		public int DeptID { get; set; }
		public int TemplateID { get; set; }
		public string AccountNo { get; set; }
		public string IsTagging { get; set; }

		public string TaggedFilePath { get; set; }
		
		public int? TaggingBy { get; set; }
        public int len { get; set; }

		public int SubfolderID { get; set; }

		public int DSConfigName { get; set; }

		public string full_content { get; set; }


		
		public string TempPath { get; set; }
		public string Temp { get; set; }
		public DateTime? TaggingDate { get; set; }
		//add by nk 
		public string cloudpath { get; set; }
		public string DirectoryName { get; set; }

		public string DepartmentName { get; set; }
		public string BranchName { get; set; }

		public string TemplateName { get; set; }
		public string SubfolderName { get; set; }


		


	}

	#region Corporate Signer--Api

	public class CS_FileUpload
	{
		public string filename { get; set; }
		public string pfxid { get; set; }
		public string pfxpwd { get; set; }
		public string signloc { get; set; }
		public string timestamp { get; set; }
		public string checksum { get; set; }
		public string accessid { get; set; }
		public string accesspwd { get; set; }
		public string uploadfile { get; set; }
		public string UploadFile_DG { get; set; }
	}

	public class Response
	{
		public int StatusID { get; set; }
		public string Message { get; set; }
	}
    #endregion
}
