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

            #region Реализация класса WorkJson<T>
            //string Path = @"D:\path.txt";

            ////var listcollection = new MyClasslist();
            ////listcollection.workListCollection();

            //var rand = new Random();
            
            //var productsList = new List<Product>();
            //var productsDictonary = new Dictionary<int, Product>();

            //var workJson = new WorkJson<List<Product>>(Path);
            //var workJson2 = new WorkJson<Dictionary<int, Product>>(Path);

            //for (var i = 1;  i <= 10;  i++) {
                
            //    var product = new Product() { 
            //        Name =  "Продукт"+ i,
            //        Energy = rand.Next(10,50),
            //    };
            //    productsDictonary[i] = product;
            //    productsList.Add(product);
            //}

            //workJson.JsonWrite(productsList);
            ////workJson2.JsonWrite(productsDictonary);
            //var products = productsList.Where(item => item.Energy > 20);

            //var productWrite = workJson.JsonRead();
            
            //foreach (var item in productWrite) {
            //    Console.WriteLine(item.ToString());

            //}
            #endregion

            var randomfile = new GeneratingRandomFiles();
           // randomfile.Files();
            randomfile.WorkFail();
            #region приведение к типу даных as
            object oj = randomfile;
            var v = oj as GeneratingRandomFiles;
            #endregion
            #region is сравнение типа данных
            var grf = randomfile is GeneratingRandomFiles;
            #endregion
            
            
           
            Console.ReadKey();
        }
    }
    #region MyClasslist работа linq с колекцией типа list



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
    #endregion
    public class Product {
        public string Name { get; set; }
        public int Energy { get; set; }
        //переопределение ToString()
        public override string ToString() {
            return $"[{Name} ({Energy})]";
        }
    }

    #region WorkJson<T> супер реализация сереализации
    public class WorkJson<T> {
        /// <summary>
        /// Серелизуюет любой обект или массив обектов в типе JSON
        /// </summary>
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
    #endregion

    public class GeneratingRandomFiles {
        Random randomEF = new Random();
        Random randomNF = new Random();

        static string[] NameFile = {
            "Work",
            "Documents",
            "Game",
            "File",
            "Program"
        };
        string[] ExtensionFile = {
            "doc",
            "docx",
            "xls",
            "xlsx",
            "jpg",
            "mp3",
            "mp4"
        };

        public void Files() {

            for (int i = 0; i < 10000; i++) {
                var nf = NameFile[randomEF.Next(0, NameFile.Length)];
                var ef = ExtensionFile[randomNF.Next(0, ExtensionFile.Length)];
                if (!File.Exists($@"D:\work\{nf}{i}.{ef}")) {
                    File.Create($@"D:\work\{nf}{i}.{ef}");
                }
            }
        }
        
        string path = @"D:\work\";
        public void WorkFail() {
            var derct = Directory.EnumerateFiles(path)
                .Select(item => item.Remove(0, path.Length))
                .Where(item => item.EndsWith("doc"))
                .Where(item => item.StartsWith("W")) ;
           
            foreach (var item in derct) {
                Console.WriteLine(item);
            }
        }
    }
}
