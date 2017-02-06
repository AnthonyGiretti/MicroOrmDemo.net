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
        private dynamic _dbConnection = Database.OpenNamedConnection("AdventureWorks2014");

        public List<dynamic> GetOrdersDynamic()
        {
            var workerId = _dbConnection.WorkOrder.WorkOrderID;
            var productName = _dbConnection.WorkOrder.Product.Name;
            var orderQty = _dbConnection.WorkOrder.OrderQty;
            var dueDate = _dbConnection.WorkOrder.DueDate;

            return _dbConnection.WorkOrder.Select(workerId, productName, orderQty, dueDate).Take(500);
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
    }
}
