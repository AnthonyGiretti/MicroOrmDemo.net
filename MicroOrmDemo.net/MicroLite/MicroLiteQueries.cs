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
    }
}
