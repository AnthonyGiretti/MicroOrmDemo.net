using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroOrmDemo.net.PetaPocoSample
{
    [ExplicitColumns]
    public class Orders
    {
        public Orders() { }

        [Column]
        public int Id { get; set; }

        [Column]
        public string ProductName { get; set; }

        [Column]
        public int Quantity { get; set; }

        [Column]
        public DateTime Date { get; set; }
    }

    public class PetaPocoQueries
    {
        public List<Orders> GetOrders()
        {
            using (var db = new PetaPoco.Database("AdventureWorks2014"))
            {
                return db.Query<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
            }
        }
    }
}
