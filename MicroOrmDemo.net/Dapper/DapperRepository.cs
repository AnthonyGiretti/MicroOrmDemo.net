using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.Dapper
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

    public class DapperRepository
    {
        public DapperRepository()
        { }

        public async Task<List<Orders>> GetOrders()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {

                var t = await connection.QueryAsync<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID");

                return t.ToList();
            }
        }

        public async Task<List<Orders>> GetOrders2()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {

                var t = await connection.QueryAsync<WorkOrder,Product, Orders>(@"SELECT TOP 500 WO.*, P.* 
                                                                        FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                                        INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID", 
                                                                        (w,p) => {
                                                                            return new Orders
                                                                            {
                                                                                Id = w.WorkOrderId,
                                                                                Date = w.DueDate,
                                                                                Quantity = w.OrderQty,
                                                                                ProductName = w.Product.Name
                                                                            };
                                                                        });

                return t.ToList();
            }
        }

        public async Task<Tuple<List<WorkOrder>, List<Product>>> GetWorkOrdersAndProducts()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {
                var data =  await connection.QueryMultipleAsync("SELECT TOP 500 * FROM[AdventureWorks2014].[Production].[WorkOrder];SELECT * FROM [Production].[Product];");

                return Tuple.Create(data.Read<WorkOrder>().ToList(), data.Read<Product>().ToList());
            }
        }

        public async Task<Orders> GetOrderById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {

                var t=  await connection.QueryAsync<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                 WHERE WorkOrderID = @Id", new { Id = id });

                return t.FirstOrDefault();
            }
        }

        public async Task<WorkOrder> Add(WorkOrder workOrder)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {
                string sql = @"INSERT INTO [AdventureWorks2014].[Production].[WorkOrder] ([OrderQty], [DueDate]) 
                              VALUES (@OrderQty,@DueDate); 
                              SELECT CAST(SCOPE_IDENTITY() as int)";
                var t = await connection.QueryAsync(sql, workOrder);
                workOrder.WorkOrderId = t.First();

                return workOrder;
            }
        }
        public async Task Update(WorkOrder workOrder)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {
                string sql = @"UPDATE [AdventureWorks2014].[Production].[WorkOrder]
                               SET OrderQty = @OrderQty
                               WHERE (@OrderQty,@DueDate)";
                var t = await connection.QueryAsync(sql, workOrder);
            }
        }

        public async Task Delete(WorkOrder workOrder)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString()))
            {
                string sql = @"DELETE 
                               FROM [AdventureWorks2014].[Production].[WorkOrder] 
                               WHERE (@OrderQty,@DueDate)";
                var t = await connection.QueryAsync(sql, workOrder);
            }
        }
    }
}
