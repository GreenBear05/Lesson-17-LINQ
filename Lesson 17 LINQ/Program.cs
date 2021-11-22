using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson_17_LINQ {
    internal class Program {
        static void Main(string[] args) {

            //var listcollection = new MyClasslist();
            //listcollection.workListCollection();

            var rand = new Random();
            
            var productsList = new List<Product>();
            var workJson = new WorkJson<Product>();


            for (var i = 1;  i <= 10;  i++) {
                var product = new Product() { 
                    Name =  "Продукт"+ i,
                    Energy = rand.Next(10,50),
                };
                productsList.Add(product);
            }

            var products = productsList.Where(item => item.Energy > 20);


            

            workJson.JsonWrite(products);

            var productWrite = workJson.JsonRead();
            foreach (var item in productWrite) {
                Console.WriteLine(item.Name + " "+item.Energy + "\n");

            }

            //foreach (var item in ) {
            //    Console.WriteLine(item);
            //}
            // Console.WriteLine(productWrite[0].Name);
            Console.ReadKey();
        }
    }
    public class MyClasslist {
        /// <summary>
        /// работа linq с колекцией типа list
        /// </summary>
        public void workListCollection() {
            var collection = new List<int>();

            for (var i = 0; i < 10; i++) {
                collection.Add(i);
            }
            //есть 2 вида оброщения к колекциям первый вид
            #region from


            var res = from item in collection
                      where item < 5
                      select item;
            #endregion
            //второй вид записи чаще всего пользуются 2 тк проше и удобней
            #region collection.Where
            var res2 = collection.Where(item => item > 2)
                .Where(item => item < 5);
            #endregion



            foreach (var item in res) {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n");
            foreach (var item in res2) {
                Console.WriteLine(item);
            }

            
        }
       
    }
    public class Product {
        public string Name { get; set; }
        public int Energy { get; set; }

        //переопределение ToString()
        //public override string ToString() {
        //    return $"{Name} ({Energy})" ;
        //}
    }
    public class WorkJson<T> {
        string path = @"D:\path.txt";
        public void JsonWrite(IEnumerable<T> list) {
            using (var file = new StreamWriter(path, false)) {
                var serializer = new JsonSerializer();
                foreach (var item in list) {
                    serializer.Serialize(file, item);
                    file.WriteLine();
                }
                file.Close();
            }
        }
        public IEnumerable<T> JsonRead() {

            List<T> mas = new List<T>();


            for (int i = 0; i < 2; i++) {


                using (var file =  new FileStream(path,FileMode.Open) ) {

                    //var b = new (file);

                    //Console.WriteLine();

                    var reader = new JsonTextReader(file);
                    var ser = new JsonSerializer();
                    var a = ser.Deserialize<T>(reader);



                    mas.Add(a);





                }
            }

            return mas;

        }
       

    }
}
