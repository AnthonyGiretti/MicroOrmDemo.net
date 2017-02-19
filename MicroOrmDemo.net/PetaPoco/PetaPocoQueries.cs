using System.Collections.Generic;
using System.Linq;

namespace MicroOrmDemo.net.PetaPocoSample
{
    public class PetaPocoQueries
    {
        public List<Orders> GetOrders()
        {
            using (var db = new PetaPoco.Database("AdventureWorks2014"))
            {
                return db.Query<Orders>(@"SELECT TOP 500 [WorkOrderID], P.Name, [OrderQty], [DueDate] 
                                 FROM [AdventureWorks2014].[Production].[WorkOrder] AS WO 
                                 INNER JOIN[Production].[Product] AS P ON P.ProductID = WO.ProductID").ToList();
            }
        }
    }
}
