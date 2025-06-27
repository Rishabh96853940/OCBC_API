using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class DashboardEntity : BaseEntity
	{
		public int? id { get; set; }
        public string UserName { get; set; }
        public int Cnt { get; set; }
        public string ActivityName { get; set; }
        public DateTime UplaodDate { get; set; }
        public int FileUplaodCount { get; set; }
        public int DataUplaodCount { get; set; }                      
        public int DataUpload { get; set; }
        public int FileUpload { get; set; }
        public int Tagging { get; set; }
        public int Users { get; set; }
        public int Viewed { get; set; }
        public int Favourite { get; set; }
        public int Download { get; set; }
        public int Searched { get; set; }       

        public string page_name { get; set; }

    }
}
