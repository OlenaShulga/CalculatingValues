using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt2_Shulga58686P
{
    class Program

    {

        static void Main(string[] args)
        {

            //wypisanie metryki programu
            Console.WriteLine("\n\t\tProgram 'Obliczenia Iteracyjne' umożliwia wielokrotne obliczanie wybranych wartości z analizy szeregów potęgowych");
            //deklaracja zmiennej dla przechowania wybranej funkcjonalności
            ConsoleKeyInfo osWybranaFunkcjonalność, osTrace;
            bool osRealizacjaZeSledzeniem;

            do
            {
                Console.WriteLine("\n\n\tMENU funkcjonalne programu :");
                Console.WriteLine("\n\tA. Obliczenie wartości (sumy) zadanego szeregu potęgowego");
                Console.WriteLine("\n\tB. Tablicowanie wartości szeregu potęgowego");
                Console.WriteLine("\n\tC. Tablicowanie wartości pierwiastka kwadratowego, obliczanego metodą Herona, z wartości zadanego szeregu potęgowego");
                Console.WriteLine("\n\tD. Tablicowanie wartości n-tego pierwiastka, obliczanego metodą Newtona, z wartości zadanego szeregu potęgowego");
                Console.WriteLine("\n\tE. Suma podzielników");
                Console.WriteLine("\n\tF. Sprawdzanie, czy wczytane słowo jest palindromem");
                Console.WriteLine("\n\tX. Zakończenie (wyjście z) programu");
                Console.Write("\n\tNaciśnięciem odpowiednego klawisza wybierz jedną z oferowanych funkcjonalności : ");

                osWybranaFunkcjonalność = Console.ReadKey();

                //wyznaczenie trybu: ze śledzeniem lub bez
                osRealizacjaZeSledzeniem = false;
                if ((osWybranaFunkcjonalność.Key == ConsoleKey.A) || (osWybranaFunkcjonalność.Key == ConsoleKey.B) || (osWybranaFunkcjonalność.Key == ConsoleKey.C) || (osWybranaFunkcjonalność.Key == ConsoleKey.D))
                {
                    Console.Write("\n\n\tJeśli chcesz wykonywać program ze śledzeniem, naciśnij 't' lub 'T', jeśli bez śledzenia, to inny dowolny klawisz : ");
                    osTrace = Console.ReadKey();
                    if (osTrace.Key == ConsoleKey.T)
                        osRealizacjaZeSledzeniem = true;
                }

                switch (osWybranaFunkcjonalność.Key)
                {
                    case ConsoleKey.A:
                        Console.WriteLine("\n\n\tWYBRANO : 'A. Obliczenie wartości (sumy) zadanego szeregu potęgowego'");
                        float osX, osEps, osS;
                        int osn;
                        osWczytanieLiczbyTypuFLOAT("wartość niezależnej zmiennej X", osRealizacjaZeSledzeniem, out osX);

                        do
                        {
                            osWczytanieLiczbyTypuFLOAT("dokładność obliczeń Eps", osRealizacjaZeSledzeniem, out osEps);

                            //sprawdzanie warunku 0<Eps<1
                            if ((osEps >= 1.0f) || (osEps <= 0.0f))
                                Console.WriteLine("\n\tERROR : Podana wartość dokładności obliczeń Eps wykracza poza przedział dozwolonych wartości : 0 < Eps < 1");
                        } while ((osEps >= 1.0f) || (osEps <= 0.0f));
                        osObliczanieSumySzereguPotęgowego(osX, osEps, osRealizacjaZeSledzeniem, out osS, out osn);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\tWyniki : obliczona suma szeregu potęgowego jest równa : Suma szeregu = {0, 4:F3}, zsumowano n = {1} wyrazów ciągu liczbowego", osS, osn);
                        Console.ResetColor();
                        //chwilowe zatrzymanie programu
                        Console.Write("\n\tDla kontynuowania działania programu naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.B:
                        Console.WriteLine("\n\n\tWYBRANO : 'B. Tablicowanie wartości szeregu potęgowego'");
                        float osXd, osXg, osh, osEps1, osX1;
                        int osN;


                        //wczytanie granic tablicowania
                        do
                        {
                            //wczytanie Xd
                            osWczytanieLiczbyTypuFLOAT("wartość dolnej granicy Xd przedziału tablicowania wartości szeregu", osRealizacjaZeSledzeniem, out osXd);

                            //wczytanie Xg
                            osWczytanieLiczbyTypuFLOAT("wartość górnej granicy Xg przedziału tablicowania wartości szeregu", osRealizacjaZeSledzeniem, out osXg);

                            //sprawdzanie warunku wejściowego Xd<Xg
                            if (osXd > osXg)
                                Console.WriteLine("\n\tERROR : Dolna granica przedziału tablicowania musi być mniejsza od górnej granicy przedziału tablicowania!!!");
                        } while (osXd > osXg);

                        //wczytanie przyrostu h
                        do
                        {
                            osWczytanieLiczbyTypuFLOAT("wartość przyrostu h zmian wartości niezależnej zmiennej X", osRealizacjaZeSledzeniem, out osh);

                            //sprawdzanie warunku wejściowego Xd<Xg
                            if ((osh <= 0.0f) || (osh > ((osXg - osXd) / 2.0f)))
                                Console.WriteLine("\n\tERROR : Przyrost h zmian wartości niezależnej zmiennej X wykracza poza dozwolony zakres wartości : 0 < h < (Xg-Xd)/2 !!!");

                        } while ((osh <= 0.0f) || (osh > ((osXg - osXd) / 2.0f)));

                        //wczytanie wartości Eps
                        do
                        {
                            osWczytanieLiczbyTypuFLOAT("wartość dokładności obliczeń Eps", osRealizacjaZeSledzeniem, out osEps1);

                            //sprawdzanie warunku 0<Eps<1
                            if ((osEps1 >= 1.0f) || (osEps1 <= 0.0f))
                                Console.WriteLine("\n\tERROR : Podana wartość dokładności obliczeń Eps wykracza poza przedział dozwolonych wartości : 0 < Eps < 1");
                        } while ((osEps1 >= 1.0f) || (osEps1 <= 0.0f));

                        //wyznaczanie liczniści wierszy w tabeli
                        int osn1 = (int)(Math.Abs(osXg - osXd) / osh) + 1;
                        float[,] osTWS;
                        osTWS = new float[osn1, 3];
                        osX1 = osXd;
                        for (int osi = 0; osX1 <= osXg; osi++, osX1 = osXd + osh * osi)
                        {
                            osTWS[osi, 0] = osX1;
                            osObliczanieSumySzereguPotęgowego(osX1, osEps1, osRealizacjaZeSledzeniem, out osTWS[osi, 1], out osN);
                            osTWS[osi, 2] = (float)osN;
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\t\t\t\t\t\t\tTablicowe zestawienie wartości szeregu w przedziale : [{0}, {1}]", osXd, osXg);
                        Console.ResetColor();
                        Console.WriteLine("\n\t\t  X\t\t  S(X)  \t\t  S(X)  \t\t  S(X)  \t\t  S(X)  \t\tLicznik zsumowanych wyrazów");
                        Console.WriteLine("\n\t\t    \t\tdomyślny\t\twykładniczy\t\tstałopozycyjny\t\tzwięzły");
                        Console.WriteLine("\n\t\t----\t\t---------\t\t---------\t\t---------\t\t---------\t\t----------------------------");

                        for (int osi = 0; osi <= osn1 - 1; osi++)
                            Console.WriteLine("\n\t\t{0,4:F2}\t\t{1}\t\t{2,8:E2}\t\t{3,8:F2}\t\t{4,8:G2}\t\t\t{5,6:F0}", osTWS[osi, 0], osTWS[osi, 1], osTWS[osi, 1], osTWS[osi, 1],
                                osTWS[osi, 1], osTWS[osi, 2]);

                        //chwilowe zatrzymanie programu
                        Console.Write("\n\tDla kontynuowania działania programu naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.C:
                        Console.WriteLine("\n\n\tWYBRANO : 'C. Tablicowanie wartości pierwiastka kwadratowego, obliczanego metodą Herona, z wartości zadanego szeregu potęgowego'");
                        short osk;

                        //wczytanie granic tablicowania
                        do
                        {
                            //wczytanie Xd
                            osWczytanieLiczbyTypuFLOAT("wartość dolnej granicy Xd przedziału tablicowania wartości szeregu", osRealizacjaZeSledzeniem, out osXd);

                            //wczytanie Xg
                            osWczytanieLiczbyTypuFLOAT("wartość górnej granicy Xg przedziału tablicowania wartości szeregu", osRealizacjaZeSledzeniem, out osXg);

                            //sprawdzanie warunku wejściowego Xd<Xg
                            if (osXd > osXg)
                                Console.WriteLine("\n\tERROR : Dolna granica przedziłu tablicowania musi być mniejsza od górnej granicy przedziału tablicowania!!!");
                        } while (osXd > osXg);

                        //wczytanie przyrostu h
                        do
                        {
                            osWczytanieLiczbyTypuFLOAT("wartość przyrostu h zmian wartości niezależnej zmiennej X", osRealizacjaZeSledzeniem, out osh);

                            //sprawdzanie warunku wejściowego Xd<Xg
                            if ((osh <= 0.0f) || (osh > ((osXg - osXd) / 2.0f)))
                                Console.WriteLine("\n\tERROR : Przyrost h zmian wartości niezależnej zmiennej X wykracza poza dozwolony zakres wartości : 0 < h < (Xg-Xd)/2 !!!");

                        } while ((osh <= 0.0f) || (osh > ((osXg - osXd) / 2.0f)));

                        //wczytanie wartości Eps
                        do
                        {
                            osWczytanieLiczbyTypuFLOAT("wartość dokładności obliczeń Eps", osRealizacjaZeSledzeniem, out osEps1);

                            //sprawdzanie warunku 0<Eps<1
                            if ((osEps1 >= 1.0f) || (osEps1 <= 0.0f))
                                Console.WriteLine("\n\tERROR : Podana wartość dokładności obliczeń Eps wykracza poza przedział dozwolonych wartości : 0 < Eps < 1");
                        } while ((osEps1 >= 1.0f) || (osEps1 <= 0.0f));

                        //wyznaczanie liczności wierszy w tabeli
                        int osn2 = (int)(Math.Abs(osXg - osXd) / osh) + 1;
                        float[,] osTWS_1;
                        osTWS_1 = new float[osn2, 5];
                        osX1 = osXd;
                        for (int osi = 0; osX1 <= osXg; osi++, osX1 = osXd + osh * osi)
                        {
                            osTWS_1[osi, 0] = osX1;
                            osObliczanieSumySzereguPotęgowego(osX1, osEps1, osRealizacjaZeSledzeniem, out osTWS_1[osi, 1], out osN);
                            osTWS_1[osi, 2] = (float)osN;
                            osTWS_1[osi, 3] = osSqrtHerona(osTWS_1[osi, 1], osEps1, out osk);
                            osTWS_1[osi, 4] = (float)Math.Sqrt(osTWS_1[osi, 1]);

                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\t\t\t\t\t\t\tTablicowe zestawienie wartości szeregu w przedziale : [{0}, {1}]", osXd, osXg);
                        Console.ResetColor();
                        Console.WriteLine("\n\t\t  X\t\t  S(X)  \t\tLicznik zsumowanych wyrazów\t\tPierwiastek kwadratowy metodą Herona\t\tPierwiastek kwadratowy SQRT");
                        Console.WriteLine("\n\t\t----\t\t---------\t\t----------------------------\t\t-----------------------------------\t\t-------------------------");
                        for (int osi = 0; osi <= osn2 - 1; osi++)
                            Console.WriteLine("\n\t\t{0,4:F2}\t\t{1,8:F3}\t\t\t{2,6:F0}\t\t\t\t\t{3,8:F3}\t\t\t\t\t{4,8:F3}", osTWS_1[osi, 0], osTWS_1[osi, 1], osTWS_1[osi, 2],
                                osTWS_1[osi, 3], osTWS_1[osi, 4]);

                        //chwilowe zatrzymanie programu
                        Console.Write("\n\tDla kontynuowania działania programu naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D:
                        Console.WriteLine("\n\n\tWYBRANO : 'D. Tablicowanie wartości n-tego pierwiastka, obliczanego metodą Newtona, z wartości zadanego szeregu potęgowego'");
                        ushort osN1;
                        //wczytanie granic tablicowania
                        do
                        {
                            //wczytanie Xd
                            osWczytanieLiczbyTypuFLOAT("wartość dolnej granicy Xd przedziału tablicowania wartości szeregu", osRealizacjaZeSledzeniem, out osXd);

                            //wczytanie Xg
                            osWczytanieLiczbyTypuFLOAT("wartość górnej granicy Xg przedziału tablicowania wartości szeregu", osRealizacjaZeSledzeniem, out osXg);

                            //sprawdzanie warunku wejściowego Xd<Xg
                            if (osXd > osXg)
                                Console.WriteLine("\n\tERROR : Dolna granica przedziłu tablicowania musi być mniejsza od górnej granicy przedziału tablicowania!!!");
                        } while (osXd > osXg);

                        //wczytanie przyrostu h
                        do
                        {
                            osWczytanieLiczbyTypuFLOAT("wartość przyrostu h zmian wartości niezależnej zmiennej X", osRealizacjaZeSledzeniem, out osh);

                            //sprawdzanie warunku wejściowego Xd<Xg
                            if ((osh <= 0.0f) || (osh > ((osXg - osXd) / 2.0f)))
                                Console.WriteLine("\n\tERROR : Przyrost h zmian wartości niezależnej zmiennej X wykracza poza dozwolony zakres wartości : 0 < h < (Xg-Xd)/2 !!!");

                        } while ((osh <= 0.0f) || (osh > ((osXg - osXd) / 2.0f)));

                        //wczytanie wartości Eps
                        do
                        {
                            osWczytanieLiczbyTypuFLOAT("wartość dokładności obliczeń Eps", osRealizacjaZeSledzeniem, out osEps1);

                            //sprawdzanie warunku 0<Eps<1
                            if ((osEps1 >= 1.0f) || (osEps1 <= 0.0f))
                                Console.WriteLine("\n\tERROR : Podana wartość dokładności obliczeń Eps wykracza poza przedział dozwolonych wartości : 0 < Eps < 1");
                        } while ((osEps1 >= 1.0f) || (osEps1 <= 0.0f));

                        //wczytanie wartości n
                        osWczytanieLiczbyTypuUSHORT("wartość stopnia pierwiastka", osRealizacjaZeSledzeniem, out osN1);

                        //wyznaczanie liczności wierszy w tabeli
                        osn2 = (int)(Math.Abs(osXg - osXd) / osh) + 1;
                        osTWS_1 = new float[osn2, 5];
                        osX1 = osXd;
                        for (int osi = 0; osX1 <= osXg; osi++, osX1 = osXd + osh * osi)
                        {
                            osTWS_1[osi, 0] = osX1;
                            osObliczanieSumySzereguPotęgowego(osX1, osEps1, osRealizacjaZeSledzeniem, out osTWS_1[osi, 1], out osN);
                            osTWS_1[osi, 2] = osN_tyPierwiastekNewtona(osTWS_1[osi, 1], osN1, osEps1, out osk);
                            osTWS_1[osi, 3] = (float)osk;
                            osTWS_1[osi, 4] = (float)Math.Pow(osTWS_1[osi, 1], 1.0f / osN1);

                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\t\t\t\t\t\tTablicowe zestawienie wartości szeregu w przedziale : [{0}, {1}]", osXd, osXg);
                        Console.ResetColor();
                        Console.WriteLine("\n\t\t  X\t\t  S(X)  \t\t{0}-ty pierwiastek Newtona\t\tLicznik iteracji\t\tMath.Pow(...)", osN1);
                        Console.WriteLine("\n\t\t----\t\t---------\t\t-------------------------\t\t----------------\t\t---------------");
                        for (int osi = 0; osi <= osn2 - 1; osi++)
                            Console.WriteLine("\n\t\t{0,4:F2}\t\t{1,8:F3}\t\t\t{2,6:F3}\t\t\t\t{3,8:F0}\t\t\t{4,8:F3}", osTWS_1[osi, 0], osTWS_1[osi, 1], osTWS_1[osi, 2],
                                osTWS_1[osi, 3], osTWS_1[osi, 4]);

                        //chwilowe zatrzymanie programu
                        Console.Write("\n\tDla kontynuowania działania programu naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;


                    case ConsoleKey.E:
                        int osP, osj, osSum, osC;
                        string[] osLiczby;
                        int[] dziel;
                        ushort osL;
                        //wczytanie n
                        Console.WriteLine("\n");
                        osWczytanieLiczbyTypuUSHORT("liczność liczb wejściowych", osRealizacjaZeSledzeniem, out osL);


                        //Console.Write("\n\n\tPodaj liczbę wejściową : ");
                        for (int osi = 0; osi < osL; osi++)
                        {


                            do
                            {
                                //wczytanie liczb
                                Console.Write("\n\n\tPodaj liczby wejściowe przez spacje : ");
                                osLiczby = Console.ReadLine().Split(' ');
                                Console.WriteLine("\n\tERROR : w zapisie podanej liczby wystąpił niedozwolony znak!!!");
                                Console.Write("\n\tPodaj ponownie liczbę wejściową : ");
                            } while (!Int32.TryParse(osLiczby[osi], out osC));
                            osP = osC / 2;
                            dziel = new int[osP];
                            osj = 0;
                            for (int i = 1; i < osC; i++)
                                if (osC % i == 0)
                                {
                                    dziel[osj] = i;
                                    osj++;
                                }
                            osSum = 0;
                            for (int i = 0; i < osj; i++)
                                osSum += dziel[i];

                            Console.Write("\n\n\tDla podanej liczby L = {0} suma dzielników = {1}, dzielniki : ", osC, osSum);
                            for (int i = 0; i < osj; i++)
                                Console.Write("\t{0} ", dziel[i]);
                        }

                        //chwilowe zatrzymanie programu
                        Console.Write("\n\n\tDla kontynuowania działania programu naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.F:

                        string osslowo, osslowo_2;
                        int osK;
                        Console.Write("\n\n\tPodaj slowo do sprawdzania : ");
                        osslowo = Console.ReadLine();
                        osK = osslowo.Length;
                        osslowo_2 = "";
                        osslowo = osslowo.ToLower();

                        for (int osi = osK - 1; osi >= 0; osi--)
                        {
                            osslowo_2 += osslowo[osi];
                        }
                        if (osslowo_2 == osslowo)
                            Console.WriteLine("\n\tPodane slowo jest palindromem: {0} - {1}", osslowo, osslowo_2);
                        else
                            Console.WriteLine("\n\tPodane slowo nie jest palindromem: {0} - {1}", osslowo, osslowo_2);

                        //chwilowe zatrzymanie programu
                        Console.Write("\n\tDla kontynuowania działania programu naciśnij dowolny klawisz...");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.X:
                        break;

                    default:
                        Console.WriteLine("\n\n\tERROR : Wybrano nie aktywny klawisz!!!");
                        Console.WriteLine("\n\tTeraz należy wybrać klawisz aktywny (funkcjonalny)!");
                        break;
                }
            } while (osWybranaFunkcjonalność.Key != ConsoleKey.X);
            //wypisanie komunikatów końcowych
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\n\n\tAutor programu : Olena Shulga, Nr indeksu : 58686");
            Console.WriteLine("\n\tDzisiejsza data : {0}", DateTime.Now);
            Console.WriteLine("\n\tDla zakończenia działania programu naciśnij dowolny klawisz...");
            Console.ReadKey();

        }

        static void osWczytanieLiczbyTypuFLOAT(string osZaproszenie, bool osRealizacjaZeSledzeniem, out float osWczytanaLiczba)
        {
            Console.Write("\n\tPodaj " + osZaproszenie + " : ");
            while (!float.TryParse(Console.ReadLine(), out osWczytanaLiczba))
            {
                Console.WriteLine("\n\tERROR: w zapisie podanej liczby wystąpił niedozwolony znak!");
                Console.Write("\n\tPodaj ponownie " + osZaproszenie + " : ");
            }
            if (osRealizacjaZeSledzeniem)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n\tTRACE : Wczytana " + osZaproszenie + " = {0}", osWczytanaLiczba);
                Console.ResetColor();
            }
        }

        static void osWczytanieLiczbyTypuUSHORT(string osZaproszenie, bool osRealizacjaZeSledzeniem, out ushort osWczytanaLiczba)
        {
            Console.Write("\n\tPodaj " + osZaproszenie + " : ");
            while (!ushort.TryParse(Console.ReadLine(), out osWczytanaLiczba))
            {
                Console.WriteLine("\n\tERROR: w zapisie podanej liczby naturalnej wystąpił niedozwolony znak!");
                Console.Write("\n\tPodaj ponownie " + osZaproszenie + " : ");
            }
            if (osRealizacjaZeSledzeniem)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n\tTRACE : Wczytana " + osZaproszenie + " = {0}", osWczytanaLiczba);
                Console.ResetColor();
            }
        }

        static void osObliczanieSumySzereguPotęgowego(float osX, float oseps, bool osRealizacjaZeSledzeniem, out float osSuma, out int osn)
        {
            float osw, osr, osp;
            osn = 0;
            osp = 1.0f;
            osr = 1.0f;
            osSuma = 0.0f;
            do
            {
                osp *= 2.0f;
                osn++;
                osr *= (osX + 1.0f) / osn;
                osw = (1.0f + osp) * osr;
                osSuma += osw;
                if (osRealizacjaZeSledzeniem)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n\tTRACE : W {0}-ej iteracji : SumaSzeregu = {1,6:f3} a wartość wyrazu : W = {2,6:F3}", osn, osSuma, osw);
                    Console.ResetColor();
                }

            } while (Math.Abs(osw) > oseps);
        }

        static float osSqrtHerona(float osa, float osEps, out short osi)
        {
            if (osa <= 0.0f)
            {
                osi = -1;
                return 0.0f;
            }
            else
            {
                float osXi, osXi_1;
                osi = 0;
                osXi = osa / 2.0f;
                do
                {
                    osXi_1 = osXi;
                    osi++;
                    osXi = (osXi_1 + osa / osXi_1) / 2.0f;
                } while (Math.Abs(osXi - osXi_1) > osEps);
                return osXi;
            }
        }

        static float osN_tyPierwiastekNewtona(float osa, ushort osn, float osEps, out short osi)
        {
            if (osa <= 0.0f)
            {
                osi = -1;
                return 0.0f;
            }
            else
            {
                float osXi, osXi_1;
                osi = 0;
                osXi = osa / osn;
                do
                {
                    osXi_1 = osXi;
                    osi++;
                    osXi = (1.0f / osn) * ((osn - 1.0f) * osXi_1 + osa / (float)Math.Pow(osXi_1, (osn - 1.0f)));
                } while (Math.Abs(osXi - osXi_1) > osEps);
                return osXi;
            }

        }
    }
}

