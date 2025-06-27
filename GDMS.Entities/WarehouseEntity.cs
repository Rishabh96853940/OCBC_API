using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Entities
{
	public class WarehouseEntity : BaseEntity
	{
		public int? DID { get; set; }
        public int? Id { get; set; }
        public int? UserID { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseDescription { get; set; }
		public string IsActive { get; set; }
        public int? userid { get; set; }


    }
}
