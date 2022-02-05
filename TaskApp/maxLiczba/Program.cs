using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace maxLiczba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Wpisz wartości (np 4,5,6,3,2 itd):");
            var liczby = ZamienTekstNaTabliceLiczb(Console.ReadLine());

            if (liczby.Length > 0)
            {
                int[] min_max = ZnadzLiczbeMinMax(liczby);
                Console.WriteLine($"Najmniejsza liczba to: {min_max[0]}");
                Console.WriteLine($"Najwieksza liczba to: {min_max[1]}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Wpisano niepoprawnie wartości!\n\n" +
                    "Kliknij n, aby raz jeszcze wpisać\n" +
                    "Wciśnij dowolony inny przycisk, aby zakończyć");

                if (Console.ReadKey().Key == ConsoleKey.N) Main(null);
            }
        }

        static int[] ZamienTekstNaTabliceLiczb(string tekst, char preparator = ',')
        {
            string linia = null;
            List<int> liczby = new List<int>();
            for(int i = 0; i < tekst.Length; i++)
            {
                if (tekst[i] != preparator) linia += tekst[i];
                else
                {
                    if (int.TryParse(linia, out int liczba))
                    {
                        liczby.Add(liczba);
                        linia = null;
                    }
                    else
                        return Array.Empty<int>();
                }

                #region sprawdź koniec łańcucha czy znajduje się liczba
                if (i == tekst.Length - 1)
                {
                    if (int.TryParse(linia, out int liczba))
                    {
                        liczby.Add(liczba);
                        linia = null;
                    }
                    else
                        return Array.Empty<int>();
                }
                #endregion
            }
            return liczby.ToArray();
        }

        static int[] ZnadzLiczbeMinMax(int[] liczby)
        {
            int max = liczby[0];
            int min = liczby[0];
            for(int x = 1; x < liczby.Length; x++)
            {
                if(liczby[x] > max) max = liczby[x];
                if(liczby[x] < min) min = liczby[x];
            }
            return new int[] {min, max};
        }
    }
}
