using MonoFrame.BaseObject;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MonoFrame.BaseLogics
{
  public static  class Extension
    {
        public static string GetTableName(this DoBase aDoBase)
        {
            return aDoBase.GetType().Name;
        }

        public static string GetPrimaryKey(this DoBase aDoBase)
        {
            PropertyInfo[] propertyInfos = aDoBase.GetType().GetProperties();

            foreach(PropertyInfo property in propertyInfos)
            {
               if( property.GetCustomAttributesData().Where(x => x.AttributeType.Name == "KeyAttribute").Any())
                {
                    return property.Name;
                }
            }

            return null;
        }

        internal static void SetPrimaryValue(this DoBase aDoBase, object Id)
        {
            PropertyInfo[] propertyInfos = aDoBase.GetType().GetProperties();

            foreach (PropertyInfo property in propertyInfos)
            {
                if (property.GetCustomAttributesData().Where(x => x.AttributeType.Name == "KeyAttribute").Any())
                {
                    property.SetValue(aDoBase,Id);
                }
            }

            
        }
        public static long GetPrimaryKeyValue(this DoBase aDoBase)
        {
            PropertyInfo[] propertyInfos = aDoBase.GetType().GetProperties();

            foreach (PropertyInfo property in propertyInfos)
            {
                if (property.GetCustomAttributesData().Where(x => x.AttributeType.Name == "KeyAttribute").Any())
                {
                    return Convert.ToInt64(property.GetValue(aDoBase));
                }
            }

            return 0;
        }

        public static void AddProperty(this ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }




    }
}
