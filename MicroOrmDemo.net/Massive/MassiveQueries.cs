using System;
using System.Collections.Generic;
using System.Linq;
using Massive;

namespace MicroOrmDemo.net.Massive
{
    public class Orders : DynamicModel
    {
        public Orders() : base("AdventureWorks2014", "Production.WorkOrder", "WorkOrderID") { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }

    public class MassiveQueries
    {
        public List<dynamic> GetOrdersDynamic()
        {
            var table = new Orders();

            return table.Query(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
        }

        public List<Orders> GetOrders()
        {
            var orders = GetOrdersDynamic();

            return orders.Select(x=> new Orders
                        {
                            Id = x.Id,
                            ProductName = x.ProductName,
                            Quantity = x.Quantity,
                            Date = x.Date
            }).ToList();            
        }
    }
}
