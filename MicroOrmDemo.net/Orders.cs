using Massive;
using MicroOrmDemo.net.Massive;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net
{
    //Business object
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

        public Product Product { get; set; }
    }

    //Db entity
    [Schema("Production")] //OrmLite set schema
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }

        public Product()
        {

        }
    }
}
