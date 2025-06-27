using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class PagesEntity
	{
		public int? id { get; set; }
		public string page_name { get; set; }
		public int? parent_id { get; set; }
		public string url { get; set; }
		public string visible { get; set; }
		public string caret { get; set; }
	}
}
