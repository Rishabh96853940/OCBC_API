using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Kotak.WebAPI.Extensions
{
    public static class SecurityExtensions
    {
		public static string EncryptPassword(string password)
		{
			MD5 md5 = new MD5CryptoServiceProvider();

			//compute hash from the bytes of text  
			md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

			//get hash result after compute it  
			byte[] result = md5.Hash;

			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				//change it into 2 hexadecimal digits  
				//for each byte  
				strBuilder.Append(result[i].ToString("x2"));
			}

			return strBuilder.ToString();
		}

		//public static string DecryptPassword(string password)
		//{
		//	MD5 md5 = new MD5CryptoServiceProvider();
		//	MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
		//	byte[] hashedBytes = null;
		//	UTF8Encoding encoder = new UTF8Encoding();
		//	hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password));
		//	string strpa = hashedBytes.ToString();
		//	return strpa;
		//}
	}
}
