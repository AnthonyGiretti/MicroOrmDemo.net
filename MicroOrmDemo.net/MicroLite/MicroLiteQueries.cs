using MicroLite;
using MicroLite.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.MicroLite
{
    public class MicroLiteQueries
    {
        public List<Orders> GetOrders()
        {
            var sessionFactory = Configure
                                .Fluently()
                                .ForMsSql2012Connection("AdventureWorks2014")
                                .CreateSessionFactory();


            using (ISession session = sessionFactory.OpenSession())
            {
                return session.Fetch<Orders>(new SqlQuery(@"SELECT TOP 500 [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID")).ToList();

            }
        }

        public List<Orders> GetOrders(int iteration)
        {
            var sessionFactory = Configure
                               .Fluently()
                               .ForMsSql2012Connection("AdventureWorks2014")
                               .CreateSessionFactory();

            var listOrders = new List<Orders>();

            using (ISession session = sessionFactory.OpenSession())
            {

                for (int i = 1; i <= iteration; i++)
                    listOrders.Add(GetOrder(session, i));
            }

            return listOrders;
        }

        private Orders GetOrder(ISession session, int id)
        {
            return session.Single<Orders>(new SqlQuery(@"SELECT [WorkOrderID] AS Id, P.Name AS ProductName, [OrderQty] AS Quantity, [DueDate] AS Date
                                                         FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                                         INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID
                                                         WHERE WorkOrderID = @p0", id));
        }
    }
}
