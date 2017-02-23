﻿using PetaPoco;
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

        public List<Orders> GetOrders(int iteration)
        {
            var listOrders = new List<Orders>();
            using (var db = new PetaPoco.Database("AdventureWorks2014"))
            {
                for (int i = 1; i <= iteration; i++)
                    listOrders.Add(GetOrder(db, i));
            }

            return listOrders;
        }

        private Orders GetOrder(Database db, int id)
        {
            return db.Query<Orders>(@"SELECT [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                              FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                              INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                              WHERE WorkOrderID = @0", id).FirstOrDefault();
        }
    }
}
