using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AG_AAC103.Models
{
    public static class DapperORM
    {
        private static readonly string connectionString = @"data source=localhost\sqlexpress;initial catalog=CommodityInfo;integrated security=True;MultipleActiveResultSets=True";

        //Execute the procedures which doesn't return anthing. 
        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                con.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
        //To return integer
        //DapperORM.ExecuteReturnScalar<T>(procedurename, Param Value)
        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                return (T) Convert.ChangeType(con.ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure),typeof(T));
            }
        }
        //To return List
        //Dapper.ORM.ReturnList<EmpClass> = IEnumerable<EmpClas>

        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                return con.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

    


    }
}