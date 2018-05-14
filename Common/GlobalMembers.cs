using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace HRMS.Common
{
    public class GlobalMembers
    {
        public static string conString = ConfigurationManager.ConnectionStrings["myConnectionString"].ToString();
    }
}