using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            path = path + @"\Sap2000\dh1.OUT";

            //var d = File.ReadAllLines(path);
            //var t = d.Where(g => g.Contains("REL DIST1"));
            //string[] splited;
            //foreach (var item in t)
            //{
            //    splited = item.Split(new string[] { "REL DIST1", " " }, StringSplitOptions.RemoveEmptyEntries);
            //    Console.WriteLine(splited[5]);
            //}
            //Console.ReadKey();


            //string data = "THExxQUICKxxBROWNxxFOXabAA";

            //string [] a = data.Split(new string[] { "xx" , "ab" }, StringSplitOptions.RemoveEmptyEntries);

            //foreach (var item in a)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.ReadKey();

            //List<string> numbers = File.ReadLines(path)
            //               .SkipWhile(line => line != "1234567")
            //               .Skip(1)
            //               .SelectMany(line => line.Split())
            //               .ToList();

            List<string> a = File.ReadLines(path)
                           .SkipWhile(line => ! line.Contains("ELEM     103"))
                           .Skip(5)
                           .SelectMany(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                           .ToList();

           



            Console.WriteLine(a[1]);
            Console.ReadKey();

        }

    }

    
}
