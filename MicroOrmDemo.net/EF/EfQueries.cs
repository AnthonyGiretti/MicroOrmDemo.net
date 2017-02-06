using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net.EF
{
    public class EfQueries
    {
        public List<Orders> GetOrders()
        {
            using (var context = new AdventureWorks2014Entities())
            {
                var query = context.WorkOrder.AsNoTracking().Select(
                    x => new Orders
                    {
                        Id = x.WorkOrderID,
                        ProductName = x.Product.Name,
                        Quantity = x.OrderQty,
                        Date = x.DueDate
                    }).Take(500);

                return query.ToList();
            }
        }
    }
}
