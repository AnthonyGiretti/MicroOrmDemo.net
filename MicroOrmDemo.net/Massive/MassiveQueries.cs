using System;
using System.Collections.Generic;
using System.Linq;
using Massive;

namespace MicroOrmDemo.net.Massive
{
    public class MassiveQueries
    {
        public List<dynamic> GetOrdersDynamic()
        {
            var table = new OrdersDynamicModel();

            return table.Query(@"SELECT TOP 500 [WorkOrderID], P.Name, [OrderQty], [DueDate] 
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
        }

        public List<Orders> GetOrders()
        {
            var orders = GetOrdersDynamic();

            return orders.Select(x=> new Orders
                        {
                            Id = x.WorkOrderID,
                            ProductName = x.Name,
                            Quantity = x.OrderQty,
                            Date = x.DueDate
                        }).ToList();            
        }
    }
}
