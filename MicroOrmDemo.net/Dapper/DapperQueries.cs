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

                return connection.Query<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
            }
        }

        public List<Orders> GetOrder(int iteration)
        {
            var listOrders = new List<Orders>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {
                
                for (int i = 1; i <= iteration; i++)
                    listOrders.Add(GetOrder(connection, i));
            }

            return listOrders;
        }

        private Orders GetOrder(SqlConnection connection, int id)
        {
            return connection.Query<Orders>(@"SELECT [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                              FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                              INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                              WHERE WorkOrderID = @Id",new { Id = id }).FirstOrDefault();
        }
    }
}
