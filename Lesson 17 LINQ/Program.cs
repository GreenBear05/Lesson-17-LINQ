using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Lesson_17_LINQ {
    internal class Program {
        static void Main(string[] args) {
            string Path = @"D:\path.txt";
            //var listcollection = new MyClasslist();
            //listcollection.workListCollection();

            var rand = new Random();
            
            var productsList = new List<Product>();
            var workJson = new WorkJson<List<Product>>(Path);
            var dictonory = new Dictionary<int,Product>();

            for (var i = 1;  i <= 10;  i++) {
                
                var product = new Product() { 
                    Name =  "Продукт"+ i,
                    Energy = rand.Next(10,50),
                };
                dictonory[i] = product;
                productsList.Add(product);
            }
            object a = productsList;
            workJson.JsonWrite(productsList);
            var products = productsList.Where(item => item.Energy > 20);

            var productWrite = workJson.JsonRead();

            foreach (var item in productWrite) {
                Console.WriteLine(item.ToString());

            }
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
        public override string ToString() {
            return $"[{Name} ({Energy})]";
        }
    }
    public class WorkJson<T> {
        string Path = @"D:\path.txt";
        public WorkJson(string path) {
            Path = path;
        }
        
        public void JsonWrite(T list) {
            
            var Options = new JsonSerializerOptions() {
               
                WriteIndented = true,
                
            };

            var ser = JsonSerializer.Serialize(list,Options);
            File.WriteAllText(Path,ser);
                
        }
        //public void JsonWrite(object list) {

        //    var Options = new JsonSerializerOptions() {

        //        WriteIndented = true,

        //    };

        //    var ser = JsonSerializer.Serialize(list, Options);
        //    File.WriteAllText(Path, ser);

        //}
        public T JsonRead() {
            
           
            var file = File.ReadAllText(Path);
            var ser = JsonSerializer.Deserialize<T>(file);

            return ser;

        }


    }
}
