using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Enums
{
    public enum CustomError
    {
        [Description("Invalid Request")]
        InvalidRequest = 10,

        [Description("Entity Not Found")]
        NotFound = 20
    }
}
