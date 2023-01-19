using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    public abstract class ObjectGenerator<T> where T : new() 
    {

        //return T object full data from DataRow
        //Good for select queries
        public static T GeneratFromDataRow(DataRow dr)
        {
            T obj = new T();
            //all T object members repeat
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                //if the DataRow contain the current member
                if (dr.Table.Columns.Contains(property.Name))
                {
                    //specific cel contain the value
                    object cell = dr[property.Name];
                    //if for avoid exceptions and make sure that no defined "NoGetFromSQL" Attribute
                    if (cell != DBNull.Value)
                    {
                        if (dr[property.Name].GetType().Name == "DateTime")
                            property.SetValue(obj, Convert.ChangeType(((DateTime)cell), typeof(DateTime)), null);
                        else
                        //put DataRow specific cell value in the macth T object member, Note! write mach members name
                        {
                            Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            property.SetValue(obj, Convert.ChangeType(cell, t));
                        }
                    }

                }
            }
            return obj;
        }
  
    //get DataSet, call GeneratFromDataRow() in repeat and return list T
    public static List<T> GeneratListFromDataRowCollection(DataRowCollection collection)
    {
        Dictionary<string, Type> data = new Dictionary<string, Type>();
        List<T> objects = new List<T>();
        if (collection.Count == 0) return objects;
        T obj = new T();
        //all T object members repeat
        foreach (PropertyInfo property in obj.GetType().GetProperties())
        {
            if (collection[0].Table.Columns.Contains(property.Name))
            {
                //data.Add(property.Name, property.PropertyType);
                data.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }
        }

        foreach (DataRow dr in collection)
        {
            obj = new T();
            foreach (KeyValuePair<string, Type> item in data)
            {
                object cell = dr[item.Key];
                if (cell != DBNull.Value)
                {
                    //property.SetValue(obj, Convert.ChangeType(((DateTime)cell), typeof(DateTime)), null);
                    //obj.item
                    //         Convert.ChangeType((dr[item.Key]),);
                    if (item.Value == typeof(DateTime))
                        obj.GetType().GetProperty(item.Key).SetValue(obj, Convert.ChangeType(((DateTime)dr[item.Key]), item.Value), null);
                    else
                        obj.GetType().GetProperty(item.Key).SetValue(obj, Convert.ChangeType(dr[item.Key], item.Value), null);
                    // object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                    //                    item.Key
                }
            }
            objects.Add(obj);
        }
        ////optional, use it if your WS and DB support sessions
        //if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 0)
        //    return null;
        //List<T> list = new List<T>();
        //foreach (DataRow dr in ds.Tables[1].Rows)
        //{
        //    list.Add(ObjectGenerator<T>.GeneratFromDataRow(dr));
        //}
        return objects;
    }
    }
}

