using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.Simple.Data
{
    public class SimpleDataQueries
    {
        public List<dynamic> GetOrdersDynamic()
        {
            dynamic dbConnection = Database.OpenNamedConnection("AdventureWorks2014");

            var workerId = dbConnection.WorkOrder.WorkOrderID;
            var productName = dbConnection.WorkOrder.Product.Name;
            var orderQty = dbConnection.WorkOrder.OrderQty;
            var dueDate = dbConnection.WorkOrder.DueDate;

            return dbConnection.WorkOrder.Select(workerId, productName, orderQty, dueDate).Take(500);
        }
        public List<Orders> GetOrders()
        {
            return GetOrdersDynamic().Select(x => new Orders
            {
                Id = x.WorkOrderID,
                ProductName = x.Name,
                Quantity = x.OrderQty,
                Date = x.DueDate
            }).ToList();
        }

        public List<dynamic> GetOrdersDynamic(int iteration)
        {
            dynamic dbConnection = Database.OpenNamedConnection("AdventureWorks2014");

            var listOrders = new List<dynamic>();

            for (int i = 1; i <= iteration; i++)
                listOrders.Add(GetOrderDynamic(dbConnection, i));
            
            return listOrders;
        }

        public List<Orders> GetOrders(int iteration)
        {
            dynamic dbConnection = Database.OpenNamedConnection("AdventureWorks2014");

            var listOrders = new List<Orders>();

            for (int i = 1; i <= iteration; i++)
                listOrders.Add(GetOrder(dbConnection, i));

            return listOrders;
        }

        private dynamic GetOrderDynamic(dynamic dbConnection, int id)
        {
            var workerId = dbConnection.WorkOrder.WorkOrderID;
            var productName = dbConnection.WorkOrder.Product.Name;
            var orderQty = dbConnection.WorkOrder.OrderQty;
            var dueDate = dbConnection.WorkOrder.DueDate;

            return dbConnection.WorkOrder.Select(workerId, productName, orderQty, dueDate).Where(workerId == id).FirstOrDefault();
        }

        private Orders GetOrder(dynamic dbConnection, int id)
        {
            var order = GetOrderDynamic(dbConnection, id);
            return new Orders
            {
                Id = order.WorkOrderID,
                ProductName = order.Name,
                Quantity = order.OrderQty,
                Date = order.DueDate
            };
        }
    }
}
