using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace Paralelismo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> lista = PopulateTheList();

            Console.WriteLine("1 - LINQ without paralelism " + LinqWithoutParalelism(lista));
            Console.WriteLine("2 - LINQ with paralelism " + LinqWithParalelism(lista));
            Console.WriteLine("3 - Lambda without paralelism: " + LambdaWithoutParalelism(lista));
            Console.WriteLine("4 - Lambda with paralelism: " + LambdaWithParalelism(lista));

            /* Comment the above line and run it again with this order
            Console.WriteLine("3 - Lambda without paralelism: " + LambdaWithoutParalelism(lista));
            Console.WriteLine("4 - Lambda with paralelism: " + LambdaWithParalelism(lista));
            Console.WriteLine("1 - LINQ without paralelism " + LinqWithoutParalelism(lista));
            Console.WriteLine("2 - LINQ with paralelism " + LinqWithParalelism(lista));

             * */

            Console.ReadKey();
        }

        private static List<Person> PopulateTheList()
        {
            List<Person> lista = new List<Person>();
            Random rnd = new Random();

            for (int i = 0; i < 3000000; i++)
            {
                lista.Add(new Person("Person " + i, rnd.Next(0, 100),
                rnd.Next(999999999).ToString()));
            }
            return lista;
        }

        private static TimeSpan LambdaWithParalelism(List<Person> lista)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var listaComParalelismoLambda = lista.Where(n => n.Age > 18 && n.Age < 60 && n.Phone.StartsWith("11")).AsParallel();
            var qtd = listaComParalelismoLambda.Count();
            sw.Stop();

            return sw.Elapsed;

        }

        private static TimeSpan LambdaWithoutParalelism(List<Person> lista)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var listaComParalelismoLambda = lista.Where(n => n.Age > 18 && n.Age < 60 && n.Phone.StartsWith("11"));
            var qtd = listaComParalelismoLambda.Count();
            sw.Stop();
            return sw.Elapsed;

        }

        private static TimeSpan LinqWithParalelism(List<Person> lista)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var listaComParalelismoLinq = (from p in lista
                                           where p.Age > 18 && p.Age < 60 && p.Phone.StartsWith("11")
                                           select p).AsParallel();
            var qtd = listaComParalelismoLinq.Count();
            sw.Stop();
            return sw.Elapsed;
        }

        private static TimeSpan LinqWithoutParalelism(List<Person> lista)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var listaSemParalelismoLinq = from p in lista
                                          where p.Age > 18 && p.Age < 60 && p.Phone.StartsWith("11")
                                          select p;
            var qtd = listaSemParalelismoLinq.Count();
            sw.Stop();
            return sw.Elapsed;
        }

    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public Person(string name, int age, string phone)
        {
            this.Name = name;
            this.Age = age;
            this.Phone = phone;
        }
    }
}
