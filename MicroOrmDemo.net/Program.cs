using MicroOrmDemo.net.Dapper;
using MicroOrmDemo.net.EF;
using MicroOrmDemo.net.Massive;
using MicroOrmDemo.net.OrmLite;
using MicroOrmDemo.net.Simple.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOrmDemo.net
{
    class Program
    {
        static void Main(string[] args)
        {

            Fire1();
            //Fire2();
        }

        private static void Fire1()
        {
            //Entity Framework
            var efquery = new EfQueries();
            var watch = new Stopwatch();
            watch.Start();
            var dataEF = efquery.GetOrders();
            watch.Stop();
            Console.WriteLine("EF: " + watch.ElapsedMilliseconds);

            //Dapper
            var dapperquery = new DapperQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataDapper = dapperquery.GetOrders();
            watch.Stop();
            Console.WriteLine("Dapper: " + watch.ElapsedMilliseconds);

            //massive dynamic
            var massivequery = new MassiveQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataMassiveDynamic = massivequery.GetOrdersDynamic();
            watch.Stop();
            Console.WriteLine("Massive dynamic: " + watch.ElapsedMilliseconds);

            //massive dynamic to Strongly typed
            //var massivequery = new MassiveQueries();
            //watch = new Stopwatch();
            //watch.Start();
            //var dataMassiveTyped = massivequery.GetOrders();
            //watch.Stop();
            //Console.WriteLine("Massive strongly typed: " + watch.ElapsedMilliseconds);

            //Orm lite
            var ormLiteQuery = new OrmLiteQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataormLite = ormLiteQuery.GetOrders();
            watch.Stop();
            Console.WriteLine("Orm Lite: " + watch.ElapsedMilliseconds);

            //Simple data dynamic 
            var simpleDataQuery = new SimpleDataQueries();
            watch = new Stopwatch();
            watch.Start();
            var datasimpleDataDynamic = simpleDataQuery.GetOrdersDynamic();
            watch.Stop();
            Console.WriteLine("Simple Data dynamic: " + watch.ElapsedMilliseconds);

            //Simple data dynamic to strongly typed
            //var simpleDataQuery = new SimpleDataQueries();
            //watch = new Stopwatch();
            //watch.Start();
            //var datasimpleData = simpleDataQuery.GetOrders();
            //watch.Stop();
            //Console.WriteLine("Simple Data strongly typed : " + watch.ElapsedMilliseconds);

            Console.ReadLine();
        }

        private static void Fire2()
        {
            //Entity Framework
            var efquery = new EfQueries();
            var watch = new Stopwatch();
            watch.Start();
            var dataEF = efquery.GetOrders();
            watch.Stop();
            Console.WriteLine("EF: " + watch.ElapsedMilliseconds);

            //Dapper
            var dapperquery = new DapperQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataDapper = dapperquery.GetOrders();
            watch.Stop();
            Console.WriteLine("Dapper: " + watch.ElapsedMilliseconds);

            //massive dynamic
            //var massivequery = new MassiveQueries();
            //watch = new Stopwatch();
            //watch.Start();
            //var dataMassiveDynamic = massivequery.GetOrdersDynamic();
            //watch.Stop();
            //Console.WriteLine("Massive dynamic: " + watch.ElapsedMilliseconds);

            //massive dynamic to Strongly typed
            var massivequery = new MassiveQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataMassiveTyped = massivequery.GetOrders();
            watch.Stop();
            Console.WriteLine("Massive strongly typed: " + watch.ElapsedMilliseconds);

            //Orm lite
            var ormLiteQuery = new OrmLiteQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataormLite = ormLiteQuery.GetOrders();
            watch.Stop();
            Console.WriteLine("Orm Lite: " + watch.ElapsedMilliseconds);

            //Simple data dynamic 
            //var simpleDataQuery = new SimpleDataQueries();
            //watch = new Stopwatch();
            //watch.Start();
            //var datasimpleDataDynamic = simpleDataQuery.GetOrdersDynamic();
            //watch.Stop();
            //Console.WriteLine("Simple Data dynamic: " + watch.ElapsedMilliseconds);

            //Simple data dynamic to strongly typed
            var simpleDataQuery = new SimpleDataQueries();
            watch = new Stopwatch();
            watch.Start();
            var datasimpleData = simpleDataQuery.GetOrders();
            watch.Stop();
            Console.WriteLine("Simple Data strongly typed : " + watch.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
