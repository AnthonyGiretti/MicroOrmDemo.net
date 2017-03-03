using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.Massive
{
    public class OrdersDynamic : DynamicModel
    {
        public OrdersDynamic() : base("AdventureWorks2014", "Production.WorkOrder", "WorkOrderID") { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }

    public class WorkOrderDynamic : DynamicModel
    {
        public WorkOrderDynamic() : base("AdventureWorks2014", "Production.WorkOrder", "WorkOrderID") { }

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

    public class Orders
    {
        public Orders() { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }

    public class MassiveRepository
    {
        public List<Orders> GetOrders()
        {
            var table = new OrdersDynamic();

            return table.Query(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").Select(x => new Orders {
                                                                                                                                Id = x.Id,
                                                                                                                                ProductName = x.ProductName,
                                                                                                                                Quantity = x.Quantity,
                                                                                                                                Date = x.Date
                                                                                                                            }).ToList();
        }

        public Orders GetOrderById(int id)
        {
            var table = new OrdersDynamic();

            var data =  table.Query(@"SELECT  [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                     WHERE WorkOrderID = @0", id).FirstOrDefault();

            return new Orders
            {
                Id = data.Id,
                ProductName = data.ProductName,
                Quantity = data.Quantity,
                Date = data.Date
            };
        }

        public void Add(WorkOrder workOrder)
        {
            var table = new WorkOrderDynamic();
            table.Insert(workOrder);
        }

        public void Update(WorkOrder workOrder)
        {
            var table = new WorkOrderDynamic();
            table.Update(workOrder, "WHERE WorkOrderID = @0", workOrder.WorkOrderId.ToString());
        }
        public void Delete(WorkOrder workOrder)
        {
            var table = new WorkOrderDynamic();
            table.Delete("WHERE WorkOrderID = @0",  workOrder.WorkOrderId.ToString());
        }
    }
}
