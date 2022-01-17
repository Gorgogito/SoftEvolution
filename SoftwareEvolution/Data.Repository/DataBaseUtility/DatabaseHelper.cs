using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Infraestructura.Repository
{
    public class DatabaseHelper
    {
        public static string Instance;

        public static string DbProvider()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build().GetConnectionString("ProviderName");
        }
        public static string DbConnectionString()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build().GetConnectionString(Instance);
        }

        public static IDatabase GetDatabase(string _Instance = "CnFacte")
        {
            Instance = _Instance;
            IDatabase db = new DatabaseSql();
            return db;
        }

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build().GetConnectionString(Instance));
        }

        //public static List<T> ReadData<T>(string storeProcedure,List<RequestParameter> parameters)
        //{
        //    using (IDatabase db = GetDatabase())
        //    {
        //        db.ProcedureName = storeProcedure;
        //        foreach (var item in parameters)
        //        {
        //            db.AddParameter(item.parameterName, item.parameterType, item.parameterDirection, item.parameterValue);
        //        }
        //        using (IDataReader drlector = db.GetDataReader())
        //        {

        //        }
            


        //        }

        //    //using (var connection = new SqlConnection(constr))
        //    //using (var command = new SqlCommand(queryString, connection))
        //    //{
        //    //    connection.Open();
        //    //    using (var reader = command.ExecuteReader())
        //    //        if (reader.HasRows)
        //    //            return Mapper.DynamicMap<IDataReader, List<T>>(reader);
        //    //}

        //    //return null;
        //}

    }

    public class RequestParameter
    {
        public string parameterName { get; set; }
        public object parameterType { get; set; }
        public ParameterDirection parameterDirection { get; set; }
        public object parameterValue { get; set; }
     }
}
