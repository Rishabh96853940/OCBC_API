using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Json;
namespace Kotak.Repository
{
	public class BaseRepository : IDisposable
	{
		protected IDbConnection ConnectionString;
		public BaseRepository()
		{
			
			//string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
			//IConfigurationRoot configuration = new ConfigurationBuilder()
			//	.SetBasePath(projectPath)
			//	.AddJsonFile("appsettings.json")
			//	.Build();
			//string connectionString = configuration.GetConnectionString("DBConnection");


			string strCon = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
			ConnectionString = new SqlConnection(strCon);
		}
		public void Dispose()
		{
			//throw new NotImplementedException();
			// Dispose of unmanaged resources.
			Dispose(true);
			// Suppress finalization.
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (ConnectionString == null)
			{
				return;
			}
			ConnectionString?.Dispose();//double check
			ConnectionString = null;
		}
	}
}
