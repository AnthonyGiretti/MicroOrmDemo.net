using MicroOrmDemo.net.AdoNet;
using MicroOrmDemo.net.Dapper;
using MicroOrmDemo.net.EF;
using MicroOrmDemo.net.Massive;
using MicroOrmDemo.net.MicroLite;
using MicroOrmDemo.net.NPoco;
using MicroOrmDemo.net.OrmLite;
using MicroOrmDemo.net.PetaPocoSample;
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
            /*
            Init();

            Console.WriteLine();
            Console.WriteLine("10 itérations");
            //Appels multiple, 100 itérations , 1 enregistrement
            MultipleIteration(10);

            Console.WriteLine();
            Console.WriteLine("100 itérations");
            //Appels multiple, 100 itérations , 1 enregistrement
            MultipleIteration(100);

            Console.WriteLine();
            Console.WriteLine("1000 itérations");
            //Appels multiple, 100 itérations , 1 enregistrement
            MultipleIteration(1000);
       
            Console.WriteLine();
            Console.WriteLine("1 itération ramenant 500 enregistrements");
            //Appel unique, 500 enregistrements
            SingleCall();

            Console.ReadLine();
            */

            var repo = new OrmLiteRepository();
            var data = repo.GetOrders().Result;
        }

        private static void Init()
        { }


        private static void SingleCall()
        {
            //Ado.net
            var adoquery = new AdoQueries();
            var watch = new Stopwatch();
            watch.Start();
            var dataAdo = adoquery.GetOrders();
            watch.Stop();
            Console.WriteLine("ADO.NET: " + watch.ElapsedMilliseconds);

            //Entity Framework
            var efquery = new EfQueries();
            watch = new Stopwatch();
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
            massivequery = new MassiveQueries();
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
            var simpleDataQuery = new SimpleDataQueries();
            watch = new Stopwatch();
            watch.Start();
            var datasimpleDataDynamic = simpleDataQuery.GetOrdersDynamic();
            watch.Stop();
            Console.WriteLine("Simple Data dynamic: " + watch.ElapsedMilliseconds);

            //Simple data dynamic to strongly typed
            simpleDataQuery = new SimpleDataQueries();
            watch = new Stopwatch();
            watch.Start();
            var datasimpleData = simpleDataQuery.GetOrders();
            watch.Stop();
            Console.WriteLine("Simple Data strongly typed : " + watch.ElapsedMilliseconds);

            var petaPocoQuery = new PetaPocoQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataPetaPoco = petaPocoQuery.GetOrders();
            watch.Stop();
            Console.WriteLine("PetaPoco : " + watch.ElapsedMilliseconds);

            var microLiteQuery = new MicroLiteQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataMicroLite = microLiteQuery.GetOrders();
            watch.Stop();
            Console.WriteLine("MicroLite : " + watch.ElapsedMilliseconds);

            var nPocoQuery = new NPocoQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataNPoco = nPocoQuery.GetOrders();
            watch.Stop();
            Console.WriteLine("NPoco : " + watch.ElapsedMilliseconds);

           
        }

        private static void MultipleIteration(int calls)
        {
            //Ado.net
            var adoquery = new AdoQueries();
            var watch = new Stopwatch();
            watch.Start();
            var data = adoquery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("ADO.NET: " + watch.ElapsedMilliseconds);

            //Dapper
            var dapperquery = new DapperQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataDapper = dapperquery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("Dapper : " + watch.ElapsedMilliseconds);

            //EF
            var efquery = new EfQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataEf = efquery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("EF : " + watch.ElapsedMilliseconds);

            //massive dynamic
            var massivequery = new MassiveQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataMassiveDynamic = massivequery.GetOrdersDynamic(calls);
            watch.Stop();
            Console.WriteLine("Massive dynamic: " + watch.ElapsedMilliseconds);

            //massive dynamic to Strongly typed
            massivequery = new MassiveQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataMassiveTyped = massivequery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("Massive strongly typed: " + watch.ElapsedMilliseconds);

            //microlite
            var microLiteQuery = new MicroLiteQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataMicroLite = microLiteQuery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("MicroLite : " + watch.ElapsedMilliseconds);

            //Simple data dynamic 
            var simpleDataQuery = new SimpleDataQueries();
            watch = new Stopwatch();
            watch.Start();
            var datasimpleDataDynamic = simpleDataQuery.GetOrdersDynamic(calls);
            watch.Stop();
            Console.WriteLine("Simple Data dynamic: " + watch.ElapsedMilliseconds);

            //Simple data strongly typed
            simpleDataQuery = new SimpleDataQueries();
            watch = new Stopwatch();
            watch.Start();
            var datasimpleDataStronglyTyped = simpleDataQuery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("Simple Data strongly typed: " + watch.ElapsedMilliseconds);

            //Orm lite
            var ormLiteQuery = new OrmLiteQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataormLite = ormLiteQuery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("Orm Lite: " + watch.ElapsedMilliseconds);

            var petaPocoQuery = new PetaPocoQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataPetaPoco = petaPocoQuery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("PetaPoco : " + watch.ElapsedMilliseconds);

            var nPocoQuery = new NPocoQueries();
            watch = new Stopwatch();
            watch.Start();
            var dataNPoco = nPocoQuery.GetOrders(calls);
            watch.Stop();
            Console.WriteLine("NPoco : " + watch.ElapsedMilliseconds);
        }
    }
}
