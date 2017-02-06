using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace MicroOrmDemo.net.Dapper
{
    public class DapperQueries
    {
        public List<Orders> GetOrders()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {

                return connection.Query<Orders>(@"SELECT TOP 500 [WorkOrderID], P.Name, [OrderQty], [DueDate] 
                                                  FROM[AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                  INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
            }
        }
    }
}
