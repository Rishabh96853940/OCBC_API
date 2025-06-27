using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kotak.Entities;
using Kotak.Repository;
using Kotak.Repository.Interfaces;

namespace Kotak.WebAPI.Extensions
{
	public static class ValidateTokenExtension
	{
		//static IAdminRepository<AdminEntity> _adminRepositary;

		public static bool ValidateToken(string token)
		{
			//bool status = false;
			AdminRepository _objRepository = new AdminRepository();
			var admin = _objRepository.CheckToken(token.Trim());
			//if (admin != null)
			//{
			//	var tokenval = admin.Where(x => x.User_Token == token.Trim()).FirstOrDefault();
			//	if (tokenval != null)
			//	{
			//		status = true;
			//	}
			//}			
			return admin;
		}

	}
}
