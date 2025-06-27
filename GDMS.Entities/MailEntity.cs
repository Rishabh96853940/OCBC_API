using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.Data;

using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class MailEntity : BaseEntity
	{
		public int id { get; set; }
		public string FileNo { get; set; }
		public int UserID { get; set; }
		public int DocID { get; set; }
				
		public string ToEmailID { get; set; }
		public string FromEmailID { get; set; }
		public DateTime ValidDate { get; set; }
		public string IsAttachment { get; set; }
		public string RandomCode { get; set; }
		public string FilePath { get; set; }
		public string ACC { get; set; }
		public string name { get; set; }

		public string Cloudpath { get; set; }

		public string isEncrypted { get; set; }

		public DataTable DataTable { get; set; }

		public string htmlContent { get; set; }
		public string Subject { get; set; }
		public bool predefined { get; set; }
		



	}
}
