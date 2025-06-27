using Kotak.Entities;
using Kotak.Repository.Interfaces;
using Dapper;
using Kotak.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Kotak.Repository
{
    public class EmailNotificationRepository : BaseRepository, IEmailNotification<EmailNotificationEntity>
    {
        private static readonly Random random = new Random();

        public string Add(EmailNotificationEntity entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<EmailNotificationEntity> Get()
        {
            try
            {
                IList<EmailNotificationEntity> resultList = SqlMapper.Query<EmailNotificationEntity>(ConnectionString, "sp_get_email_notification", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public EmailNotificationEntity Get(int id)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return SqlMapper.Query<EmailNotificationEntity>(ConnectionString, "sp_get_email_notification_by_id", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public string InsertUpdateEmailNotification(EmailNotificationEntity entity)
        {

            try
            {
                //string token = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
             //   int request_id = random.Next(100000, 1000000);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@iID", entity.id);
                parameters.Add("@service_type", entity.service_type);
                parameters.Add("@cc_email_id", entity.cc_email_id);
                parameters.Add("@subject", entity.subject);
                parameters.Add("@Body", entity.body);
                parameters.Add("@branch_id", entity.branch_id);
                parameters.Add("@created_by", entity.userid);
            
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                //IList<EmailNotificationEntity> resultList = SqlMapper.Query<EmailNotificationEntity>(ConnectionString, "SP_InsertUpdatePickupRequest", parameters, commandType: CommandType.StoredProcedure).ToList();
                //return resultList;
                SqlMapper.Execute(ConnectionString, "sp_add_edit_email_notification", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Msg");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public string UpdatePickupRequest(EmailNotificationEntity entity)
        {

            try
            {
                //string token = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
                //   int request_id = random.Next(100000, 1000000);

                DynamicParameters parameters = new DynamicParameters();
                // parameters.Add("@request_id", entity.request_id);
                //parameters.Add("@service_type", entity.service_type);
                //parameters.Add("@document_type", entity.document_type);
                //parameters.Add("@main_file_count", entity.main_file_count);
                //parameters.Add("@collateral_file_count", entity.collateral_file_count);
                //parameters.Add("@satus", entity.collateral_file_count);
                parameters.Add("@updated_by", entity.userid);
            
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                
                SqlMapper.Execute(ConnectionString, "SP_UpdatePickupRequest", param: parameters, commandType: CommandType.StoredProcedure);
                string result = parameters.Get<string>("@Msg");
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool DeleteEmailNotification(string Id, int userid)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@@Id", Id);
               // parameters.Add("@delete_by", userid);
                parameters.Add("@Msg", "", direction: ParameterDirection.Output);
                SqlMapper.Execute(ConnectionString, "sp_delete_email_notification", param: parameters, commandType: CommandType.StoredProcedure);
                string val = parameters.Get<string>("@Msg");
                if (!string.IsNullOrEmpty(val))
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
    }
}
