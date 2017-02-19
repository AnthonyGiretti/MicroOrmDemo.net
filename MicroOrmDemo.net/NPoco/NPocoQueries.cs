using NPoco;
using System.Collections.Generic;
using System.Linq;

namespace MicroOrmDemo.net.NPoco
{
    public class NPocoQueries
    {
        public List<Orders> GetOrders()
        {
            using (var db = new Database("AdventureWorks2014"))
            {
                return db.Query<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
            }
        }      
    }
}
