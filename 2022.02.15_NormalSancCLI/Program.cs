using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2022._02._15_normalsancCLI
{
    class C
    {
        public string nev, orszag;
        public double ugras1, ugras2, pont1, pont2, osszpont;
        public C(string sor)
        {
            var s = sor.Split('\t');
            nev = s[0];
            orszag = s[1];
            ugras1 = double.Parse(s[2]);
            ugras2 = double.Parse(s[3]);
            pont1 = double.Parse(s[4]);
            pont2 = double.Parse(s[5]);
            osszpont = pont1 + pont2;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var lista = new List<C>();
            var sr = new StreamReader("siugras.txt");
            var elso = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                lista.Add(new C(sr.ReadLine()));
            }
            sr.Close();
            //2.
            Console.WriteLine(lista.Count());
            //3.
            var can = (
                from sor in lista
                where sor.orszag == "CAN"
                select sor);
            Console.WriteLine(can.Count());
            //4.
            var dborszag = (
                from sor in lista
                group sor by sor.orszag);
            Console.WriteLine(dborszag.Count());
            //5.
            var simon = (
                 from sor in lista
                 where sor.nev == "AMMANN Simon"
                 select sor.orszag);
            Console.WriteLine(simon.First());
            //6.
            var winnerpont = (from sor in lista select sor.osszpont).Max();
            var winner =     (from sor in lista where sor.osszpont == winnerpont select sor.nev);
            Console.WriteLine(winner.First() + "  " + winnerpont);
            //7.
            bool t = false;
            foreach (var item in lista)
            {
                if (item.orszag == "TUR")
                {
                    t = true;
                    break;
                }
            }
            if (t)
            {
                Console.WriteLine("Volt Török");
            }
            else
            {
                Console.WriteLine("Nem volt");
            }
            //8.
            var elsomax =    (from sor in lista select sor.ugras1).Max();
            var masodikmax = (from sor in lista select sor.ugras2).Max();
            if (elsomax > masodikmax)
            {
                Console.WriteLine(elsomax);
            }
            else
            {
                Console.WriteLine(masodikmax);
            }
            //9.
            var zero = (from sor in lista
                        where sor.pont1 == 0
                       || sor.pont2 == 0
                        select sor);
            Console.WriteLine(zero.Count());
            //10.
            var kinek = (from sor in lista where sor.osszpont == 253.6 select sor);
            foreach (var item in kinek)
            {
                Console.WriteLine(item.nev + "    10.");
            }
            //11.
            var szaz1 = (from sor in lista where sor.ugras1 == 100 select sor).Count();
            var szaz2 = (from sor in lista where sor.ugras2 == 100 select sor).Count();
            Console.WriteLine(szaz1 + szaz2);
            //12
            var maradek = (from sor in lista orderby sor.pont1 descending select sor).Take(30);
            double pont1atl = 0;
            double pont2atl = 0;
            double ugras1atl = 0;
            double ugras2atl = 0;
           /* foreach (var item in lista)
            {
                pont1atl += item.pont1;
                ugras1atl += item.ugras1;
            }*/
            foreach (var item in maradek)
            {
                    pont1atl += item.pont1;
                pont2atl += item.pont2;
                    ugras1atl += item.ugras1;
                ugras2atl += item.ugras2;
            }
            //Console.WriteLine($"1. Sorozat: ugrások átlaga: {ugras1atl/lista.Count()} pontszámok átlaga {pont1atl/lista.Count()}");
            Console.WriteLine($"1. Sorozat: ugrások átlaga: {ugras1atl / 30:0.##} pontszámok átlaga {pont1atl /30:0.##}");
            Console.WriteLine($"2. Sorozat: ugrások átlaga: {ugras2atl / 30} pontszámok átlaga   {pont2atl / 30:0.##}");

            //13
            var sw = new StreamWriter("legjobbak.txt");
            sw.WriteLine("Név;pontszám");
            double teljesatlag = 0;
            foreach (var item in lista)
            {
                teljesatlag += item.pont1 + item.pont2;
            }
            Console.WriteLine(teljesatlag/lista.Count());
            foreach (var item in lista)
            {
                if (item.osszpont>teljesatlag/lista.Count())
                {
                    sw.WriteLine($"{item.nev};{item.osszpont}");
                }
            }
            sw.Close();
            Console.WriteLine("------------------");
            /*  var test = (from sor in lista orderby sor.pont1 descending select sor).Take(30);
               foreach (var item in test)
               {
                   Console.WriteLine(item.pont1);
               }
               Console.WriteLine(test.Count());
               */
            Console.ReadKey();
        }

    }
}
