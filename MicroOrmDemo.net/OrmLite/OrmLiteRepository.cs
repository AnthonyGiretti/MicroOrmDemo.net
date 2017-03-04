using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.OrmLite
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
    [Schema("Production")] //OrmLite set schema
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

    //Db entity
    [Schema("Production")] //OrmLite set schema
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
    }

    public class OrmLiteRepository
    {
        private OrmLiteConnectionFactory _connectionFactory;
        public OrmLiteRepository()
        {
            _connectionFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString(), SqlServer2014Dialect.Provider);
        }

        public async Task<List<Orders>> GetOrders()
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                var sql = dbConnection
                          .From<WorkOrder>()
                          .Join<Product>((w, p) => w.ProductID == p.ProductID)
                          .Select<WorkOrder,Product>((w,p) => new { Id = w.WorkOrderId, ProductName =  p.Name, Quantity = w.OrderQty, Date = w.DueDate })
                          .Limit(0,500);

                var data = await dbConnection.SelectAsync<Orders>(sql);
                return data.ToList();
                //SQL Syntax
                return await dbConnection.SelectAsync<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID");
            }
        }

        public async Task<Orders> GetOrderById(int id)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                var sql = dbConnection
                          .From<WorkOrder>()
                          .Join<Product>((w, p) => w.ProductID == p.ProductID)
                          .Select<WorkOrder, Product>((w, p) => new { Id = w.WorkOrderId, ProductName = p.Name, Quantity = w.OrderQty, Date = w.DueDate })
                          .Where<WorkOrder>(w => w.WorkOrderId == id);

                var data =  await dbConnection.SelectAsync<Orders>(sql);
                return data.FirstOrDefault();

                //SQL Syntax
                data = await dbConnection.SelectAsync<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                                     WHERE WorkOrderID = @Id", new { Id = id });
                return data.FirstOrDefault();
            }
        }

        public async Task Add(WorkOrder workOrder)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                await dbConnection.InsertAsync(workOrder);
            }
        }
        public async Task Update(WorkOrder workOrder)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                await dbConnection.UpdateAsync(workOrder);
            }
        }

        public async Task Delete(WorkOrder workOrder)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                await dbConnection.DeleteAsync<WorkOrder>(p=> p.WorkOrderId == workOrder.WorkOrderId);
                //dbConnection.DeleteById<WorkOrder>(workOrder.WorkOrderId);
            }
        }
    }
}
