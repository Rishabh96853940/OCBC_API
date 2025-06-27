
using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
    public interface IDocument<T> where T : class
    {
        IEnumerable<T> GetDetails(int userid);
        IEnumerable<T> GetDocumentsNamesList(int userid);

        IEnumerable<T> GetDocumentsNamesListForEdit(int userid);
        IEnumerable<T> GetDeptCodeList(int userid);

        string InsertDocument(T entity);
        string InsertDocumentDetails(T entity);

        bool DeleteDocumentDetailMaster(int id, int documentID, int userId);
        T GetDepartmentByDocumentDetails(T entity);



    }
}
