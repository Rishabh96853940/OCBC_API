using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Kotak.Entities;
using Kotak.Repository.Interfaces;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace Kotak.Repository
{
    public class InventoryRepository : BaseRepository, Iinventory<InventoryEntity>
    {

        public IEnumerable<InventoryEntity> Get()
        {
            try
            {
                IList<InventoryEntity> resultList = SqlMapper.Query<InventoryEntity>(ConnectionString, "sp_GetBatchInventoryDetails", commandType: CommandType.StoredProcedure).ToList();
                return resultList;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}