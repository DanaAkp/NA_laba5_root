using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA_laba5
{
    class Program
    {
        static void Main(string[] args)
        {
            //1 осуществить отделение корней:
            //1.1 использовать аналитический метод;
            //1.2 использовать графический метод;
            //1.3 использовать табулирование с шагом h.

            //2 Уточнить изолированные корни:
            //2.1 использовать метод бисекций для всех ε;
            //2.2 использовать метод Ньютона для всех ε;
            //2.3 использовать метод хорд для всех ε;
            //2.4 использовать метод простых итераций для всех ε.

            double a = -3, b = 3;
            int interval = 50;
            Separation(a, b, interval);



            Console.ReadLine();
        }
        public static double Func1(double x)
        {
            return Math.Pow(3, x) - 2 * x - 5;
        }
        public static double Func2(double x)
        {
            return x - Math.Cos(x);
        }
        public static double Func3(double x)
        {
            return 2 * Math.Atan(x) - 3 * x + 2;
        }
        public static void Separation(double a, double b, int interval)
        {
            Console.WriteLine("x\tF(x)\tSign");
            double h = (b - a) / interval;
            double res;
            for (double i = a; i <= b; i+=h)
            {
                res = Func1(i);
                if (res < 0) Console.WriteLine(string.Format("{0:F2}\t{1:F2}\t-", i, res));
                else if(res>0) Console.WriteLine(string.Format("{0:F2}\t{1:F2}\t+", i, res));
                else Console.WriteLine(string.Format("{0:F2}\t{1:F2}\t0", i, res));
            }
            res = Func1(b);
            if (res < 0) Console.WriteLine(string.Format("{0:F2}\t{1:F2}\t-", b, res));
            else if (res > 0) Console.WriteLine(string.Format("{0:F2}\t{1:F2}\t+", b, res));
            else Console.WriteLine(string.Format("{0:F2}\t{1:F2}\t0", b, res));
        }
        public static double Clarification_Bisection(double a, double b, double epsilon)
        {
            while (b - a > epsilon)
            {
                double c = (b - a) / 2;
                double f_c = Func1(c);
                if (f_c == 0) return c;
                else
                {
                    double f_a = Func1(a);
                    double f_b = Func1(b);
                    if (f_c * f_a > 0) a = c;
                    if (f_c * f_b > 0) b = c;
                }
            }
            return (b - a) / 2;
        }
        public static void Clarification_Newton(double a, double b, int interval)
        {

        }
        public static void Clarification_Chorda(double a, double b, int interval)
        {

        }
    }
}
