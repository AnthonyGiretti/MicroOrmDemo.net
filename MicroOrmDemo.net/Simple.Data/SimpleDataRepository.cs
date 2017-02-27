using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.Simple.Data
{
    public class SimpleDataRepository
    {
        private dynamic _dbConnection;
        private dynamic _workerId;
        private dynamic _productName;
        private dynamic _orderQty;
        private dynamic _dueDate;

        public SimpleDataRepository()
        {
            _dbConnection = Database.OpenNamedConnection("AdventureWorks2014");
            _workerId = _dbConnection.WorkOrder.WorkOrderID;
            _productName = _dbConnection.WorkOrder.Product.Name;
            _orderQty = _dbConnection.WorkOrder.OrderQty;
            _dueDate = _dbConnection.WorkOrder.DueDate;
    }

        public List<Orders> GetOrders()
        {
            List<dynamic> data = _dbConnection.WorkOrder.Select(_workerId, _productName, _orderQty, _dueDate).Skip(0).Take(500);

            return data.Select(x => new Orders
            {
                Id = x.WorkOrderID,
                ProductName = x.Name,
                Quantity = x.OrderQty,
                Date = x.DueDate
            }).ToList();
        }

        public Orders GetOrderById(int id)
        {
            dynamic data = _dbConnection.WorkOrder.Select(_workerId, _productName, _orderQty, _dueDate).Find(_workerId == id);

            return new Orders
            {
                Id = data.WorkOrderID,
                ProductName = data.Name,
                Quantity = data.OrderQty,
                Date = data.DueDate
            };
        }

        public void Add(WorkOrder order)
        {
            _dbConnection.WorkOrder.Insert(order);
            //dynamic data = _dbConnection.WorkOrder.Insert(ProductId : 1, OrderQty : 10, StockedQty : 50 .......);
        }

        public void Update(WorkOrder order)
        {
            _dbConnection.WorkOrder.UpdateById(order);
        }

        public void Delete(WorkOrder order)
        {
            _dbConnection.WorkOrder.DeleteById(order);
        }
    }
}
