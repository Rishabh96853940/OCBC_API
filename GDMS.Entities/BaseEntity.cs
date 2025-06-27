using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Entities
{
    public class BaseEntity
    {
		public DateTime? DateCreated { get; set; }
		public int CreatedBy { get; set; }
		public string User_Token { get; set; }
	}
}
