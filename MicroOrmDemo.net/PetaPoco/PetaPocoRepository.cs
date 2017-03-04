using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.PetaPocoRepoSample
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
        public int? Quantity { get; set; }

        [Column]
        public DateTime? Date { get; set; }
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

        public Product Product { get; set; }
    }

    //Db entity
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
    }

    public class PetaPocoRepository
    {
        public PetaPocoRepository()
        { }

        public List<Orders> GetOrders()
        {
            using (var db = new PetaPoco.Database("AdventureWorks2014"))
            {
                return db.Query<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                          FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                          INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();

                return GetWorkOrdersWithProduct(db)
                       .Select(x=> new Orders { Id = x.WorkOrderId, Date = x.DueDate, Quantity = x.OrderQty, ProductName = x.Product.Name }).ToList();
            }
        }

        public List<WorkOrder> GetWorkOrdersWithProduct()
        {
            using (var db = new PetaPoco.Database("AdventureWorks2014"))
            {
                return GetWorkOrdersWithProduct(db);
            }
        }

        private List<WorkOrder> GetWorkOrdersWithProduct(Database db)
        {
            return db.Fetch<WorkOrder, Product>(@"SELECT TOP 500 WO.*, P.* 
                                                        FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                        INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
        }
    }
}
