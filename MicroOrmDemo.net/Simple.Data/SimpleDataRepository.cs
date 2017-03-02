using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.Simple.Data
{
    public class Orders
    {
        public Orders() { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public DateTime? Date { get; set; }
    }

    //Db entity
    public class WorkOrder
    {
        public WorkOrder() { }

        public int WorkOrderId { get; set; }
        public int ProductID { get; set; }
        public int? OrderQty { get; set; }
        public int? StockedQty { get; set; }
        public int? ScrappedQty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? ScrapReasonID { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

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

        public void Add(WorkOrder workOrder)
        {
            _dbConnection.WorkOrder.Insert(workOrder);
            //dynamic data = _dbConnection.WorkOrder.Insert(ProductId : 1, OrderQty : 10, StockedQty : 50 .......);
        }

        public void Update(WorkOrder workOrder)
        {
            _dbConnection.WorkOrder.UpdateByWorkOrderId(workOrder);
            //_dbConnection.WorkOrder.Update(WorkOrderId: workOrder.WorkOrderId);
        }

        public void Delete(WorkOrder workOrder)
        {
            _dbConnection.WorkOrder.DeleteByWorkOrderId(workOrder.WorkOrderId);
            //_dbConnection.WorkOrder.Delete(WorkOrderId: workOrder.WorkOrderId);
        }
    }
}
