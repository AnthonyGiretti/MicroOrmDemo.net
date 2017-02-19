using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.OrmLite
{
    public class OrmLiteQueries
    {
        public List<Orders> GetOrders()
        {
            var ormlite = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString(),SqlServer2014Dialect.Provider);

            using (var dbConnection = ormlite.OpenDbConnection())
            {
                return dbConnection.Select<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
            }
        }
    }
}
