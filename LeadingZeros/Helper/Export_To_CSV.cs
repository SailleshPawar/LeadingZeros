using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LeadingZeros.Helper
{
    public static class Export_To_CSV
    {
        private static string GetHeader(Type T)
        {
            var properties = T.GetProperties(BindingFlags.Public | BindingFlags.Instance
                 | BindingFlags.GetProperty | BindingFlags.SetProperty);
            var columns = properties.Select(a => a.Name).ToArray();
            var header = string.Join(",".ToString(), columns);
            return header;
        }

        public static PropertyInfo[] GetOrderedProperties(Type type)
        {
            Dictionary<Type, int> lookup = new Dictionary<Type, int>();

            int count = 0;
            lookup[type] = count++;
            Type parent = type.BaseType;
            while (parent != null)
            {
                lookup[parent] = count;
                count++;
                parent = parent.BaseType;
            }

            return type.GetProperties()
                .OrderByDescending(prop => lookup[prop.DeclaringType]).ToArray();
        }

        public static string ToCsv<T>(string separator, List<T> objectlist)
        {
            StringBuilder csvdata = new StringBuilder("");
            Type t;

            if (objectlist.Count > 0)
            {
                t = objectlist[0].GetType();
            }
            else
            {
                t = objectlist.GetType();
            }

            PropertyInfo[] properties = t.GetProperties();
            StringBuilder strHeader = new StringBuilder("");
            string header = GetAttributeDisplayName(properties, separator);
            csvdata.Append(header);
            foreach (var o in objectlist)
                csvdata.AppendLine(ToCsvFields(separator, properties, o).ToString());
            return csvdata.ToString();
        }

        private static StringBuilder ToCsvFields(string separator, PropertyInfo[] fields, object o)
        {
            StringBuilder linie = new StringBuilder();

            foreach (var f in fields)
            {
                if (linie.Length > 0)
                    linie.Append(separator);

                var x = f.GetValue(o);

                if (x != null)
                    linie.Append(x.ToString());
            }
            return linie;
        }



        public static string GetAttributeDisplayName(PropertyInfo[] properties, string separator)
        {
            StringBuilder sb = new StringBuilder("");
            int counter = 0;
            bool isCustomAttributePresent = true;
            foreach (var item in properties)
            {

                var atts = item.GetCustomAttributes(
                typeof(DisplayAttribute), true);
                if (atts.Length == 0)
                {
                    isCustomAttributePresent = false;
                    break;

                }
                else
                {
                    if (counter.Equals(0))
                    {

                        PropertyInfo property = (atts[0] as DisplayAttribute).ResourceType.GetProperty((atts[0] as DisplayAttribute).Name, BindingFlags.Public | BindingFlags.Static);
                        sb.Append((property.GetValue(null, null)));

                        counter++;
                    }
                    else
                    {

                        PropertyInfo property = (atts[0] as DisplayAttribute).ResourceType.GetProperty((atts[0] as DisplayAttribute).Name, BindingFlags.Public | BindingFlags.Static);
                        sb.Append("," + (property.GetValue(null, null)));
                        counter++;
                    }

                }

            }

            if (!isCustomAttributePresent)
            {
                sb.AppendLine(String.Join(separator, properties.Select(f => f.Name).ToArray()));
                counter++;
            }

            return sb.ToString();
        }
    }
}
