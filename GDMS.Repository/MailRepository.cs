using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;

namespace Kotak.Repository
{
public	class MailRepository : BaseRepository, IMail<MailEntity>
	{
		public IEnumerable<MailEntity> Get()
		{
			try
			{
				IList<MailEntity> resultList = SqlMapper.Query<MailEntity>(ConnectionString, "GetMailListByFileNo", commandType: CommandType.StoredProcedure).ToList();
				return resultList;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public MailEntity Get(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@USERID", id);
				return SqlMapper.Query<MailEntity>(ConnectionString, "GetBranchList", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public MailEntity GetMailDetails(string  FileNo)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@FileNo", FileNo);
				return SqlMapper.Query<MailEntity>(ConnectionString, "GetMailListByFileNo", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public IEnumerable<MailEntity> GetBulkFile(string FileNo,string ToEmailID)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@FileNo", FileNo);
				parameters.Add("@ToEmailID", ToEmailID);
				IList<MailEntity> resultList = SqlMapper.Query<MailEntity>(ConnectionString, "GetMailListByFileNo", parameters, commandType: CommandType.StoredProcedure).ToList();
				MailEntity dtd = new MailEntity();
				//dtd.DataTable = ToDataTables(resultList);
				return resultList;

			}
			catch (Exception ex)
			{
				LogError(ex, "MSG5");
				throw;
			}
		}

		private void LogError(Exception ex, string msg)
		{
			var TempPath = (ConfigurationManager.AppSettings["temp"].ToString());
			string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			message += string.Format("Message: {0}", ex.Message);
			message += Environment.NewLine;
			message += string.Format("StackTrac: {0}", msg);
			message += Environment.NewLine;
			message += string.Format("Source: {0}", ex.Source);
			message += Environment.NewLine;
			message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
			message += Environment.NewLine;
			message += "-----------------------------------------------------------";
			message += Environment.NewLine;
			string path = Path.Combine(TempPath, "ErrorLog.txt");
			using (StreamWriter writer = new StreamWriter(path, true))
			{
				writer.WriteLine(message);
				writer.Close();
			}
		}


		public DataTable ToDataTables<T>(IList<T> data)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
			DataTable table = new DataTable();
			for (int i = 0; i < props.Count; i++)
			{
				PropertyDescriptor prp = props[i];
				table.Columns.Add(prp.Name, prp.PropertyType);
			}
			object[] values = new object[props.Count];
			foreach (T item in data)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = props[i].GetValue(item);
				}
				table.Rows.Add(values);
			}
			return table;
		}

		public string Add(MailEntity entity)
		{
			try
			{
				return AddUpdate(entity);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public bool Update(string FileNo)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@FileNo", FileNo);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "SP_SendMailUpdateStatus", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");

				if (val.Trim() == "File Sent successfully")
				{
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public bool UpdateErrorLog(string FileNo,string Error)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@FileNo", FileNo);
				parameters.Add("@Error", Error);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "sp_UpdateErrorLog", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");

				if (val.Trim() == "File Sent successfully")
				{
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public bool Delete(int id)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Id", id);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "usp_DeleteBranch", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");

                if (val.Trim()== "Deleted successfully")
				{
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private string AddUpdate(MailEntity entity)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@FileNo", entity.FileNo);
			parameters.Add("@UserID", entity.UserID);
			parameters.Add("@ToEmailID", entity.ToEmailID);
			parameters.Add("@Validity", entity.ValidDate);
			parameters.Add("@IsAttachment", entity.IsAttachment);
			parameters.Add("@RandomCode", entity.RandomCode);
			//parameters.Add("@DocID", entity.DocID);
			parameters.Add("@MSG","", direction: ParameterDirection.Output);		
			SqlMapper.Execute(ConnectionString, "SP_AddEditSendEmails", param: parameters, commandType: CommandType.StoredProcedure);

			return parameters.Get<string>("@MSG");
		}

		public string AddEmailDetails(MailEntity entity)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@FileNo", entity.FileNo);
			parameters.Add("@UserID", entity.UserID);
			parameters.Add("@ToEmailID", entity.ToEmailID);
			parameters.Add("@Validity", DateTime.Now);
			parameters.Add("@IsAttachment", entity.IsAttachment);
			parameters.Add("@RandomCode", entity.RandomCode);
			parameters.Add("@MSG", "", direction: ParameterDirection.Output);
			SqlMapper.Execute(ConnectionString, "SP_AddEditSendEmails", param: parameters, commandType: CommandType.StoredProcedure);

			return parameters.Get<string>("@MSG");
		}

		public string DownloadFile(string FileNo)
		{
			try
			{
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@FileNo", FileNo);
				parameters.Add("@Msg", "", direction: ParameterDirection.Output);
				SqlMapper.Execute(ConnectionString, "SP_DownlaodFileFromMailS", param: parameters, commandType: CommandType.StoredProcedure);
				string val = parameters.Get<string>("@Msg");
				return val.Trim();
			}
			catch (Exception ex)
			{
				LogError(ex, "MSG3");
				throw;
			}
		}
	}
}
