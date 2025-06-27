using System;
using System.Collections.Generic;
using System.Text;

namespace Kotak.Repository.Interfaces
{
	public interface IDashboard<T> where T : class
    {      

        IEnumerable<T> GetStatusCount(string FileNo);

        IEnumerable<T> GetStatusActivityCountPieCHart(string FileNo);

        //IEnumerable<T> GetDashboardData(int userID);
        //IEnumerable<T> GetDashboardFileData(int userID);

        IEnumerable<T> GetActivityCount(int Userid);      

        IEnumerable<T> GetActivityUserLog();

        IEnumerable<T> CheckAccessRight(int RoleID, string PageName);

    }
}
