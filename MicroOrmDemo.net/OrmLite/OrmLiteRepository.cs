using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.OrmLite
{
    public class OrmLiteRepository
    {
        private OrmLiteConnectionFactory _connectionFactory;
        public OrmLiteRepository()
        {
            _connectionFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["AdventureWorks2014"].ToString(), SqlServer2014Dialect.Provider);
        }

        public List<Orders> GetOrders()
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                var sql = dbConnection
                          .From<WorkOrder>()
                          .Join<Product>((w, p) => w.ProductID == p.ProductID)
                          .Select<WorkOrder,Product>((w,p) => new { Id = w.WorkOrderId, ProductName =  p.Name, Quantity = w.OrderQty, Date = w.DueDate })
                          .Limit(0,500);                   
                
                return dbConnection.Select<Orders>(sql).ToList();

                //SQL Syntax
                return dbConnection.Select<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
            }
        }

        public Orders GetOrderById(int id)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                var sql = dbConnection
                          .From<WorkOrder>()
                          .Join<Product>((w, p) => w.ProductID == p.ProductID)
                          .Select<WorkOrder, Product>((w, p) => new { Id = w.WorkOrderId, ProductName = p.Name, Quantity = w.OrderQty, Date = w.DueDate })
                          .Where<WorkOrder>(w=> w.WorkOrderId == id);

                return dbConnection.Select<Orders>(sql).FirstOrDefault();

                //SQL Syntax
                return dbConnection.Select<Orders>(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                     FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                     INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                                     WHERE WorkOrderID = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Add(WorkOrder workOrder)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                dbConnection.Insert(workOrder);
            }
        }
        public void Update(WorkOrder workOrder)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                dbConnection.Update(workOrder);
            }
        }

        public void Delete(WorkOrder workOrder)
        {
            using (var dbConnection = _connectionFactory.OpenDbConnection())
            {
                dbConnection.Delete<WorkOrder>(p=> p.WorkOrderId == workOrder.WorkOrderId);
                //dbConnection.DeleteById<WorkOrder>(workOrder.WorkOrderId);
            }
        }
    }
}
