using System;
using System.IO;


namespace fakturomat
{
    public class Faktura
    {
        public int numerFaktury = 0;
        public string nazwaFirmy = "";
        public List<string> podpisyStron = new List<string>();

        public Faktura(int numerFakturyWprowadzony, string nazwaFirmyWprowadzona)
        {
            numerFaktury = numerFakturyWprowadzony;
            nazwaFirmy = nazwaFirmyWprowadzona;
        }
    }

    public class Fakturomat
    {
        static List<Faktura> listaFaktur = new List<Faktura>();
        static Faktura znalezionaFaktura = new Faktura(0, "");
        static bool warunekPetli = true;


        public static void Main()
        {
            int numerFakturyWpisany = 0;
            string nazwaFirmyWpisana = "";
            string wprowadzonyPodpis = "";
            int indexFakturyWBazie = -1;

            while (warunekPetli)
            {
                Console.WriteLine("Faktumator - program do obslugi faktur.");
                Console.WriteLine("Wybierz akcje do wykonania przez program:");
                Console.WriteLine("1 - Dodaj fakture");
                Console.WriteLine("2 - Wyszukaj fakture");
                Console.WriteLine("3 - Usun fakture");
                Console.WriteLine("4 - Podpisz fakture");
                Console.WriteLine("5 - Sprawdz podpisy faktury");
                Console.WriteLine("6 - Zakoncz program");

                int wyborUzytkownika = int.Parse(Console.ReadLine());

                switch(wyborUzytkownika)
                {
                    case 1:
                        Console.WriteLine("Wybrano dodawanie faktury");
                        Console.WriteLine("Wpisz nowy numer faktury:");
                        numerFakturyWpisany = int.Parse(Console.ReadLine());
                        Console.WriteLine("Wpisz nazwe firmy dla ktorej wystawiana jest faktura:");
                        nazwaFirmyWpisana = Console.ReadLine();

                        if ((numerFakturyWpisany != 0) && (nazwaFirmyWpisana != ""))
                        {
                            if (listaFaktur.Find(x => x.numerFaktury == numerFakturyWpisany) == null)
                            {
                                listaFaktur.Add(new Faktura(numerFakturyWpisany, nazwaFirmyWpisana));
                                Console.WriteLine("Faktura zostala dodana");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Ten numer faktury juz istnieje");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wpisany numer faktury jest rowny zero badz nazwa firmy jest pusta");
                            break;
                        }
                    case 2:
                        Console.WriteLine("Wybrano wyszukanie faktury");
                        Console.WriteLine("Wpisz numer wyszukiwanej faktury:");
                        numerFakturyWpisany = int.Parse(Console.ReadLine());

                        znalezionaFaktura = listaFaktur.Find(x => x.numerFaktury == numerFakturyWpisany);
                        if(znalezionaFaktura == null)
                        {
                            Console.WriteLine("Nie znaleziono faktury, wpisz nazwe firmy w celu wyszukania faktury:");
                            nazwaFirmyWpisana = Console.ReadLine();
                            znalezionaFaktura = listaFaktur.Find(x => x.nazwaFirmy == nazwaFirmyWpisana);
                            if(znalezionaFaktura == null)
                            {
                                Console.WriteLine("Nie znaleziono faktury z taka firma");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Dane znalezionej faktury:");
                                Console.WriteLine("Numer faktury: " + znalezionaFaktura.numerFaktury.ToString());
                                Console.WriteLine("Nazwa firmy z faktury: " + znalezionaFaktura.nazwaFirmy);
                                if (znalezionaFaktura.podpisyStron.Count == 0)
                                {
                                    Console.WriteLine("Brak podpisow dla wyszukanej faktury");
                                }
                                else
                                {
                                    Console.Write("Podpisano przez: ");
                                    foreach (string podpis in znalezionaFaktura.podpisyStron)
                                    {
                                        Console.Write(podpis + Environment.NewLine);
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Dane znalezionej faktury:");
                            Console.WriteLine("Numer faktury: " + znalezionaFaktura.numerFaktury.ToString());
                            Console.WriteLine("Nazwa firmy z faktury: " + znalezionaFaktura.nazwaFirmy);
                            if (znalezionaFaktura.podpisyStron.Count == 0)
                            {
                                Console.WriteLine("Brak podpisow dla wyszukanej faktury");
                            }
                            else
                            {
                                Console.Write("Podpisano przez: ");
                                foreach (string podpis in znalezionaFaktura.podpisyStron)
                                {
                                    Console.Write(podpis + Environment.NewLine);
                                }
                            }
                            break;
                        }
                    case 3:
                        Console.WriteLine("Wpisz numer faktury do usuniecia:");
                        numerFakturyWpisany = int.Parse(Console.ReadLine());
                        indexFakturyWBazie = listaFaktur.FindIndex(x => x.numerFaktury == numerFakturyWpisany);

                        if (indexFakturyWBazie == -1)
                        {
                            Console.WriteLine("Nie ma takiego numeru faktury");
                            break;
                        }
                        else
                        {
                            znalezionaFaktura = listaFaktur[indexFakturyWBazie];
                            listaFaktur.RemoveAt(indexFakturyWBazie);
                            znalezionaFaktura = new Faktura(0, "");
                            Console.WriteLine("Faktura zostala usunieta");
                            break;
                        }
                    case 4:
                        Console.WriteLine("Wpisz numer faktury do podpisania:");
                        numerFakturyWpisany = int.Parse(Console.ReadLine());
                        indexFakturyWBazie = listaFaktur.FindIndex(x => x.numerFaktury == numerFakturyWpisany);

                        if (indexFakturyWBazie == -1)
                        {
                            Console.WriteLine("Nie ma takiego numeru faktury");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wprowadz podpisy kolejnych osob oddzielajac przyciskiem enter");
                            Console.WriteLine("Zeby zakonczyc wpisywanie zostaw pusta linie");
                            wprowadzonyPodpis = Console.ReadLine();
                            while (wprowadzonyPodpis != "") 
                            {
                                listaFaktur[indexFakturyWBazie].podpisyStron.Add(wprowadzonyPodpis);
                                wprowadzonyPodpis = Console.ReadLine();
                            }
                            Console.WriteLine("Zakonczono podpisywanie faktury");
                            break;
                        }
                    case 5:
                        Console.WriteLine("Wpisz numer faktury do podpisania:");
                        numerFakturyWpisany = int.Parse(Console.ReadLine());
                        indexFakturyWBazie = listaFaktur.FindIndex(x => x.numerFaktury == numerFakturyWpisany);

                        if (indexFakturyWBazie == -1)
                        {
                            Console.WriteLine("Nie ma takiego numeru faktury");
                            break;
                        }
                        else
                        {
                            
                            if (listaFaktur[indexFakturyWBazie].podpisyStron.Count > 0)
                            {
                                Console.WriteLine("Faktura o numerze " + numerFakturyWpisany.ToString() +" zostala podpisana przez:");
                                foreach (string podpisStrony in listaFaktur[indexFakturyWBazie].podpisyStron)
                                {
                                    Console.WriteLine(podpisStrony);
                                }
                            }
                            break;
                        }
                    case 6:
                        warunekPetli = false;
                        break;
                    default:
                        Console.WriteLine("Podano bledny numer funkcji, sprobuj jeszcze raz");
                        break;
                }
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(Environment.NewLine);
            }
            return;
        }
    }

}
