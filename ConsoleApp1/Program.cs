using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cat cat1 = new Cat();
            // cat1.color = "Red";
            // cat1.leg = 4;
            // Cat.Manu cat2 = new Cat.Manu();
            // cat2.A1 = "ABC";
            // cat2.A2 = 4;

            //Action InitForm = new Action(() => cat1.GetType().GetMethod("eye")?.Invoke(cat1, null));

            // //double a;

            // var a = cat1.GetType().GetMethod("eye")?.Invoke(cat1, null);

            // Console.WriteLine("cat eye: " + cat1.eye());
            // Console.WriteLine(a) ;

            // Console.ReadKey();


            A a = new A();
            a.A1 = 5;
            Console.WriteLine(a.A2());
            Console.ReadKey();

            Cons con = new Cons();
            Cons.s = 5;
            
        }
        
       
        
    }

    public class Cons
    {
        public static double s { get; set; }
    }


    public class A
    {
        public double A1 { get; set; }
        public double A2()
        {
            return A1 * Cons.s; 
        }
    }
}
