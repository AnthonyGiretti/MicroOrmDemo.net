﻿using System;
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
        //1 itération
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

        //multiple itérations
        public List<dynamic> GetOrdersDynamic(int iteration)
        {
            var listOrders = new List<dynamic>();

            var table = new Orders();
            for (int i = 1; i <= iteration; i++)
                    listOrders.Add(GetOrderDynamic(table, i));
            

            return listOrders;
        }
        public List<Orders> GetOrders(int iteration)
        {
            var listOrders = new List<Orders>();

            var table = new Orders();
            for (int i = 1; i <= iteration; i++)
                listOrders.Add(GetOrder(table, i));


            return listOrders;
        }

        private dynamic GetOrderDynamic(Orders table, int id)
        {
            return table.Query(@"SELECT [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                 WHERE WorkOrderID = @0", id).FirstOrDefault();

        }
        
        private Orders GetOrder(Orders table, int id)
        {
            var order = GetOrderDynamic(table, id);
            return new Orders {
                        Id = order.Id,
                        ProductName = order.ProductName,
                        Quantity = order.Quantity,
                        Date = order.Date
                    };
        }
    }
}
