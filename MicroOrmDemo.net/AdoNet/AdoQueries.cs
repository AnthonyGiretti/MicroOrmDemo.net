using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.AdoNet
{
    public class AdoQueries
    {
        public List<Orders> GetOrders()
        {
            var listOrders = new List<Orders>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {
                connection.Open();

                using (var command = new SqlCommand(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listOrders.Add(
                                new Orders
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                });
                        }
                    }
                }    
            }
            return listOrders;
        }

        public List<Orders> GetOrders(int iteration)
        {
            var listOrders = new List<Orders>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {
                connection.Open();

                for (int i = 1; i <= iteration; i++)
                    listOrders.Add(GetOrder(connection, i));    
            }

            return listOrders;
        }

        private Orders GetOrder(SqlConnection connection, int id)
        {
            Orders order = null;
            using (var command = new SqlCommand(@"SELECT [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                  FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                  INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                                  WHERE WorkOrderID = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order =  new Orders
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ProductName = reader["ProductName"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                        };
                    }
                    
                }
            }
            return order;            
        }
    }
}
