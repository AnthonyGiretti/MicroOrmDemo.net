using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.NPoco
{      
    public class Orders
    {
        public Orders() { }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
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

    public class NPocoRepository
    {           
        public NPocoRepository()
        { }

        public async Task<List<Orders>> GetOrders()
        {
            using (var db = new Database("AdventureWorks2014"))
            {
                var data = await db.QueryAsync<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                         FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                         INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID");

                return data.ToList();

                var data2 = await GetWorkOrdersWithProduct(db);
                return data2.Select(x => new Orders { Id = x.WorkOrderId, Date = x.DueDate, Quantity = x.OrderQty, ProductName = x.Product.Name }).ToList();
            }
        }

        public void PopulateExistingOrder(Orders order)
        {
            using (var db = new Database("AdventureWorks2014"))
            {
                db.FirstOrDefaultInto(order, @"SELECT P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                      FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                      INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                                      WHERE WorkOrderID = @0", order.Id);

            }
        }

        public async Task<List<WorkOrder>> GetWorkOrdersWithProduct()
        {
            using (var db = new Database("AdventureWorks2014"))
            {
                return await GetWorkOrdersWithProduct(db);
            }
        }

        private async Task<List<WorkOrder>> GetWorkOrdersWithProduct(Database db)
        {
            var data = await db.FetchAsync<WorkOrder>(@"SELECT TOP 500 WO.*, P.* 
                                                  FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                  INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID");

            return data.ToList();
        }

        public Tuple<List<WorkOrder>,List<Product>> GetWorkOrdersAndProducts()
        {
            using (var db = new Database("AdventureWorks2014"))
            {
                return db.FetchMultiple<WorkOrder, Product>("SELECT TOP 500 * FROM[AdventureWorks2014].[Production].[WorkOrder];SELECT * FROM [Production].[Product];");
            }
        }
    }
}
