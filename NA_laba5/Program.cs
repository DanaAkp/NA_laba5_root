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
            int interval = 10;
            double epsilon = Math.Pow(10, -4);
            Separation(a, b, interval);


            double a1 = -2.40;
            double b1 = -1.80;
            Console.WriteLine("Метод бисекций");
            Console.WriteLine(Clarification_Bisection(a1, b1, epsilon));
            //Console.WriteLine(Clarification_Bisection(1.92, 2.04, epsilon));

            Console.WriteLine("Метод ньютона");
            Console.WriteLine(Clarification_Newton(a1, b1, epsilon));

            Console.WriteLine("Метод хорд ");
            Console.WriteLine(Clarification_Chorda(a1, b1, epsilon));

            Console.WriteLine("Метод простых итераций");
            Console.WriteLine(Clarification_Iter(a1, b1, epsilon));
            Console.ReadLine();
        }
        public static double Func1(double x)
        {
            //return Math.Pow(3, x) - 2 * x - 5;
            return Math.Pow(x, 3) - 4 * x + 2;
        }
        public static double Func2(double x)
        {
            // return x - Math.Cos(x);
            return (4 * x / 3) - (Math.Pow(x, 3) / 12) - (1 / 6);
        }

        public static double func1_Proizz(double x)
        {
            return 3 * x * x - 4;
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
        public static string Clarification_Bisection(double a, double b, double epsilon)
        {
            string s = "n\ta\tc\tb\tf(a)\tf(c)\tf(b)\t|b-a|\n";
            int count = 0;
            while (b - a > epsilon)
            {
                double c = (a+b) / 2;
                s +=count+"\t"+ Math.Round( a,4) + "\t" + Math.Round(c, 4) + "\t" + Math.Round(b, 4) + "\t";
                double f_c = Func1(c);
                if (f_c == 0) break; 
                else
                {
                    double f_a = Func1(a);
                    double f_b = Func1(b);
                    s += count + Math.Round(a, 4) + "\t" + Math.Round(c, 4) + "\t" + Math.Round(b,4) + "\t";
                    if (f_c * f_a > 0) a = c;
                    if (f_c * f_b > 0) b = c;
                }

                s += (b - a).ToString() + "\n";count++;
            }
            s += "Корень равен " + (a + b) / 2;
            return s;
        }
        public static string Clarification_Newton(double a, double b, double epsilon)
        {
            string s = "n\tx\tf(x)\t|Xn+1 - Xn|\n";
            double f_x = Func1(a); 
            double f_x_1= func1_Proizz(a);
            double x_n = a - f_x / f_x_1;
            s += "0\t"+a + "\t" + f_x + "\t" + (x_n - a).ToString() + "\n";
            int c = 1;
            while (x_n - a > epsilon)
            {
                a = x_n;
                f_x = Func1(a);
                f_x_1 = func1_Proizz(a);
                x_n = a - f_x / f_x_1;
                s += c + "\t" + a + "\t" + f_x + "\t" + (x_n - a).ToString() + "\n";
                c++;
            }
            s += "Корень равен " + x_n;
            return s;
        }
        public static string Clarification_Chorda(double a, double b,double epsilon)
        {
            string s = "n\tx0\tx2\tx1\t|Xn - Xn-1|\n";
            double x0 = a;
            double x1 = b;
            double x2 = new_Xn(x1, x0);
            double f_x = Func1(a);
            s += "0\t" + x0 + "\t" + x2 + "\t" + x1 + "\t" + (x1-x2).ToString() + "\n";
            int c = 1;
            while (x1 - x2 > epsilon)
            {
                x0 = x1;
                x1 = x2;
                x2 = new_Xn(x1, x0);
                s += c + "\t" + x0 + "\t" + x2 + "\t" + x1 + "\t" + (x1 - x2).ToString() + "\n";
                c++;
            }
            s += "Корень равен " + x2;
            return s;
        }
        public static double new_Xn(double Xn, double Xn_1)
        {
            return Xn - Func1(Xn) * (Xn_1 - Xn) / (Func1(Xn_1) - Func1(Xn));
        }

        public static string Clarification_Iter(double a, double b, double epsilon)
        {
            if (Math.Abs( Func2(a)) < 1 && Math.Abs( Func2(b)) < 1) return "Не выполняется основное условие сходимости";

            string s = "n\tx0\tx1\t|Xn - Xn-1|\n";
            double x0 = a;
            double x1 = Func2(x0);
            s += "0\t" + x0 + "\t" + x1 + "\t" + (x1 - x0).ToString() + "\n";
            int c = 1;
            while (x1-x0 > epsilon)
            {
                x0 = x1;
                x1 = Func2(x0);
                s += c + "\t" + x0 + "\t"  + x1 + "\t" + (x1 - x0).ToString() + "\n";
                c++;
            }
            s += "Корень равен " + x1;
            return s;
        }
    }
}
