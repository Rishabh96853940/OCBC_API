using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class StatusEntity : BaseEntity
	{
		public int? id { get; set; }
		public string BranchName { get; set; }
        public string Department { get; set; }
        public string FileNo { get; set; }
        public string PageCount { get; set; }
        public string IsIndexing { get; set; }
        public string IsTagged { get; set; }
        public DateTime DATETO { get; set; }
        public DateTime DATEFROM { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Activity { get; set; }
        public string LogDate { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
        public string Ref6 { get; set; }
        public string Ref7 { get; set; }
        public string Ref8 { get; set; }
        public string Ref9 { get; set; }
        public string Remark { get; set; }
        public string SendOn { get; set; }
        public string Size { get; set; }
        public string TemplateName { get; set; }
        //public string Ref13 { get; set; }
        //public string Ref14 { get; set; }
        //public string Ref15 { get; set; }
        //public string Ref16 { get; set; }
        //public string Ref17 { get; set; }
        //public string Ref18 { get; set; }
        //public string Ref19 { get; set; }
        public string Customer { get; set; }

        public string IsSend { get; set; }

        //  public string Folder { get; set; }

        public string htmlContent { get; set; }

        public string ToEmailID { get; set; }         
        public string Folder { get; set; }
        public string SubFolder { get; set; }

        public string SubfolderName { get; set; }
        public Int32 TemplateID { get; set; }
        
        public int Cnt { get; set; }
        public int DDeptID { get; set; }
        public string ActivityName { get; set; }
        
        public DateTime UplaodDate { get; set; }
        public int FileUplaodCount { get; set; }
        public int DataUplaodCount { get; set; }               
        public int FileSize { get; set; }
        //public string Version { get; set; }
        public string ACC { get; set; }

      //  public int PageCount { get; set; }
        public string USERID { get; set; }
        public string EntryDate { get; set; }

        public int DataUpload { get; set; }
        public int FileUpload { get; set; }
        public int Tagging { get; set; }
        public int Users { get; set; }
        public int Viewed { get; set; }
        public int Favourite { get; set; }
        public int Download { get; set; }
        public int Searched { get; set; }
        public string Archive { get; set; }
        public string page_name { get; set; }
        public string FilePath { get; set; }

        //public string RelPath { get; set; }
        //public string PhotoPath { get; set; }
        public string TagName { get; set; }
        public string _Flag { get; set; }
        public string Status { get; set; }
        //public int TagID { get; set; }
        public int ActiivtyID { get; set; }
        public string Template { get; set; }
        
        public string DepartmentName { get; set; }

        public int TAT { get; set; }
        public int BranchID { get; set; }
     
        public int userID { get; set; }

        public string SendBy { get; set; }
        public string SendDate { get; set; }
        public string LoginDate { get; set; }

      //  public string FileNo { get; set; }

        public string DownloadDate { get; set; }

        public string SearchedDate { get; set; }

        public string EmailDate { get; set; }

        public string LastLoginDatetime { get; set; }


        public string PasswodExpiryDate { get; set; }

        public string Cabinet { get; set; }
       // public string Folder { get; set; }
        public int AttemptCount { get; set; }

        //--------------------------------------------------Newly added below variables

        public string CartonNo { get; set; }

        public string WarehouseName { get; set; }

      //  public string WareHouseID { get; set; }

        public string ItemLcoation { get; set; }
        public string ItemStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
