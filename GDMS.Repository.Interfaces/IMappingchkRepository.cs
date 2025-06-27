using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kotak.Repository.Interfaces
{
	public interface IMappingchkRepository<T> where T : class
	{
		IEnumerable<T> Get();
		T Get(int id);
        string Add(T entity);
        

        string CrownAdd(T entity);

        string Barcoding(T entity);

        string DumpUpload(T entity);

        string UpdateAuditorStatus(T entity);

        string UpdateRackNo(T entity);
        
        string PDCBarcoding(T entity);
        

             bool CrownDelete(int id);
        bool Delete(int id);
        bool Delete(int id,int userid);
        string Update(T entity);

        string regionmappingCreate(T entity);
      //  string regionmappingCreate(T entity);

        IEnumerable<T> GetDetails(int id);

        IEnumerable<T> GetCrownDetails(int id);

        IEnumerable<T> GetDetailsRegion(int id);

        IEnumerable<T> GetCrownDetailsRegion(int id);

        
        IEnumerable<T> GetBranchDetailsRegionWise(int id);

        IEnumerable<T> GetBranchDetailsUserWise(int id);

        IEnumerable<T> GetCrownBranchDetailsUserWise(int id);
        
        IEnumerable<T> GetBranchByDeptUserWise(int id,int deptid);

        string BulkUserUpload(T entity);
        string uploadCSV(T entity);

        //bool ActiveDeActive(T entity);
        string AddEditDump(T entity);
        string AvanseUploaDumpData(T entity);
        string CreateBulkRequest(T entity);
        string Invupload(T entity);
        string ItemStatusupload(T entity);
        string AccessUploadData(T entity);
        Task<string> GetNextRefillingAccessNumber(int userId);

    }
}
