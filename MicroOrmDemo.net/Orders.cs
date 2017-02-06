using Massive;
using MicroOrmDemo.net.Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net
{
    public class OrdersDynamicModel : DynamicModel
    {
        public OrdersDynamicModel() : base("AdventureWorks2014", "Production.WorkOrder", "WorkOrderID") { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }

    public class Orders
    {
        public Orders() { }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}
