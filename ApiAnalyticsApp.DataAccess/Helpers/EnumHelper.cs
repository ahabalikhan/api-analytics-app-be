using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Text;

namespace ApiAnalyticsApp.DataAccess.Helpers
{
    public static class EnumHelper
    {
        public static void ThrowCustomErrorException<T>(this T value, HttpStatusCode code, string description = null, object data = null)
            where T : Enum
        {
            var errorDescription = string.IsNullOrEmpty(description) ? value.GetDescription() : description;
            throw new CustomErrorException((int)Convert.ChangeType(value, typeof(int)), code, errorDescription, data);
        }

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();

            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            var field = type.GetField(name);
            if (field == null)
            {
                return null;
            }

            var attr = field.GetCustomAttribute<DescriptionAttribute>();

            return attr?.Description;
        }
    }
}
