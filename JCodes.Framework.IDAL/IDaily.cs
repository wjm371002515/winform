using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JCodes.Framework.Data.Model;

namespace JCodes.Framework.Data.IDAL
{
    public interface IDaily
    {
        DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId);

        Daily GetDailyById(int id);

        List<Daily> GetDailyById(string createdate, string shopname, string aliwangwang, string key, string isSolve);

        string AddDaily(Daily daily);

        bool EditDaily(Daily daily);

        bool DelDaily(int id);
    }
}
