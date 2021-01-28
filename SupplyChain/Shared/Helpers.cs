using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SupplyChain.Shared
{
    public static class Helpers
    {
        public static async Task<List<T>> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = await GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static async Task<T> GetItem<T>(DataRow dr)
        {
            var t = await Task.Run(() =>
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in dr.Table.Columns)
                {
                    PropertyInfo[] properties = obj.GetType().GetProperties();
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        else
                            continue;
                    }
                }
                return obj;
            });

            return t;

        }
    }
}
