
using System.Xml.Linq;
using System;
using System.Threading;

/*
 1 - Artikli
    1. Unos artikla - done
    2. Brisanje artikla
        a. Po imenu artikla - done
        b. Sve one kojima je istekao rok trajanja - done
    3. Uredivanje artikla
        a. Zasebno proizvoda (pronade se artikal pa se bira sto se zeli promijeniti)
        b. Popust/poskupljenje na sve proizvode unutar trgovine
    4. Ispis
        a. Svih artikala kako s spremljeni (format: ime (kolicina) - cijena - broj dana od/do isteka) - done
        b. Svih artikala sortirano po imenu
        c. Svih artikala sortirano po datumu silazno
        d. Svih artikala sortirano do datumu uzlazno
        e. Svih artikala sortirano po olicini
        f. Najprodavaniji artikl
        g. Najmanje prodavan artikl
 2 - Radnici
    1. Unos radnika
    2. Brisanje radnika
        a. Po imenu
        b. Svih onih koji imaju vise od 65 godina
    3. Uredivanje radnika
    4. Ispis
        a. svih radnika (format: ime - godine)
        b. svih radnika kojima je rodendan u tekucem mjesecu
 3 - Racuni
    1. Unos novog računa - ispišu se svi proizvodi koji su dostupni u dućanu, zatim se upiše ime proizvoda i količina (isti proizvod moguće samo jednom unijeti). Radnja se ponavlja sve dok se ne unesu svi potrebni proizvodi. Kad je unos završen unosi se ključna rijeć te radnik može vidjeti što je sve na računi i dalje može poništiti račun ili potvrditi njegovo printanje. Ako je radnja potvrdna, ukljanjaju se artikli sa stanja (ukoliko im je stanje 0 brišu se skroz). Id se generira kao prvi sljedeći broj od zadnjeg računa. Isto tako, potrebno je da program odmah izračuna ukupnu cijenu svakog artikla i DateTime izdavanja računa (trenutni DateTime). Radniku se ispiše u konzoli formatirani ispis računa kojeg korisnik vidi (isti kao u sekciji ispis računa).
    2. Ispis
        a. Svih racuna u formatu (id - datum i vrijeme - ukupni iznos (potrebno izracunati iz kolicine artikala i cijene))
            i. Mogucnost odabira racuna po id-u pa se ispisu svi detalji racuna (id - datum i vrijeme - proizvodi) (format: ime - kolicina, svaki u novom redu) te ukupna cijena
 4 - Statistika (potrebna sifra za ulaz)
    1. Ukupan broj artikala u trgovini
    2. Vrijednost artikala koji nisu prodani
    3. Vrijednost svih artikala koji su prodani
    4. Stanje po mjesecima - vlasnik unosi datum i godinu za koji ga zanima, iznos plaća radnika, iznos najma i svih ostalih troškova, a aplikacija izračuna u tom mjesecu koliki je iznos zaradio/izgubio po formuli: ukupna zarada u tom mjesecu * ⅓ - plaće - ostali troškovi
 0 - Izlaz iz aplikacije - done
 */

var articleList = new List<(string Name, int Amount, int Price, DateTime DateOfExpiry)> {
    ("Cedevita naranca", 5, 15, new DateTime(2023, 2, 1)),
    ("Cedevita limun", 5, 15, new DateTime(2024, 2, 1))
};
var workers = new List<(string Name, DateTime dateOfBirth)> {};


while (true)
{
    Console.WriteLine("1 - Artikli\n2 - Radnici\n3 - Računi\n4 - Statistika\n0 - Izlaz iz aplikacije");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("1 - Artikli\n\t1. Unos artikla\n\t2. Brisanje artikla\n\t3. Uređivanje artikla\n\t4. Ispis\n\t0. Povratak na glavni izbornik");
            var choice1 = Console.ReadLine();
            switch(choice1)
            {

                case "1":
                    (string, int, int, DateTime) tempTuple = GetEntry();
                    Console.WriteLine("Želite li spremiti promjene? (y/n)");
                    do
                    {
                        var option = Console.ReadLine();
                        if (option == "y")
                        {
                            articleList.Add(tempTuple);
                            Console.WriteLine("Promjene pohranjene.");
                            break;
                        }
                        else if (option == "n")
                        {
                            Console.WriteLine("Odustali ste od promjene.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Pogrešan unos slova.");
                        }
                    } while (true);
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("1 - Artikli\n\t2. Brisanje artikla\n\t\ta. Po imenu\n\t\tb. Brisanje artikala kojima je prošao rok\n\t\tc. Povratak u glavni izbornik");
                    var choice2 = Console.ReadLine();
                    switch (choice2)
                    {
                        case "a":
                            Console.WriteLine("Unesite ime proizvoda kojeg želite obrisati: ");
                            var nameOfWantedArticle = Console.ReadLine();
                            int foundIndex = articleNameIndexFinder(nameOfWantedArticle, articleList);
                            if (foundIndex == -1)
                            {
                                Console.WriteLine("Taj artikal nije pronaden!");
                                break;
                            }

                            Console.WriteLine("Želite li spremiti promjene? (y/n)");
                            do
                            {
                                var option = Console.ReadLine();
                                if (option == "y")
                                {
                                    articleList.RemoveAt(foundIndex);
                                    Console.WriteLine("UspjeŠno ste obrisali artikal.");
                                    Thread.Sleep(1000);
                                    break;

                                } 
                                else if (option == "n")
                                {
                                    Console.WriteLine("Odustali ste od brisanja artikala.");
                                    Thread.Sleep(1000);
                                    break;
                                } 
                                else
                                {
                                    Console.WriteLine("Pogrešan unos slova.");
                                }
                            } while (true);
                            break;
                        case "b":
                            Console.WriteLine("Jeste li sigurni da zelite obrisati sve artikle kojima je prosao rok? (y/n)");
                            do
                            {
                                var option = Console.ReadLine();
                                if (option == "y")
                                {
                                    removeExpiredItem(articleList);
                                    Console.WriteLine("Uspješno ste obrisali artikle.");
                                    break;

                                }
                                else if (option == "n")
                                {
                                    Console.WriteLine("Odustali ste od brisanja artikla.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Pogrešan unos slova.");
                                }
                            } while (true);
                            break;
                        case "c":
                            Console.WriteLine("Povratak u glavni izbornik!");
                            Thread.Sleep(1000);
                            break;

                    }
                    break;

                case "3":
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("1 - Artikli\n\t4. Ispis\n\t\ta. Svih artikala kako su spremljeni");
                    var choice4 = Console.ReadLine();
                    switch (choice4)
                    {
                        case "a":
                            Console.WriteLine("Lista svih artikala po redoslijedu spremanja: ");
                            foreach (var item in articleList)
                            {
                                TimeSpan daysUntilExpiry = item.DateOfExpiry.Subtract(DateTime.Now);
                                Console.WriteLine($"{item.Name} ({item.Amount}) - {item.Price} - {daysUntilExpiry.Days}");
                            }
                            Console.WriteLine("Pritisnite tipku ENTER za nastavak...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                case "0":
                    Console.WriteLine("Povratak na glavni meni!");
                    break;
            }
            Console.Clear();
            continue;
        case "2":
            Console.WriteLine("odabir 2");
            continue;
        case "3":
            Console.WriteLine("odabir 3");
            continue;
        case "4":
            Console.WriteLine("odabir 4");
            continue;
        case "0":
            Console.WriteLine("Izlaz!");
            break;
        default:
            Console.WriteLine("Pogresan unos.");
            continue;
    }
    break;
}

static (string, int, int, DateTime) GetEntry()
{
    Console.WriteLine("Unesi ime artikla:");
    var articleName = Console.ReadLine();
    Console.WriteLine("Unesi količinu: ");
    int amount;
    int.TryParse(Console.ReadLine(), out amount);
    Console.WriteLine("Unesi cijenu: ");
    int price;
    int.TryParse(Console.ReadLine(), out price);
    Console.WriteLine("Unesi datum isteka roka (dd/mm/gggg): ");
    string pattern = "dd/MM/yyyy";
    DateTime dateOfExpiry = DateTime.ParseExact(Console.ReadLine(), pattern, null);

    return (articleName, amount, price, dateOfExpiry);

}

static (string, DateTime) GetWorker()
{
    Console.WriteLine("Unesi ime radnika: ");
    var personName = Console.ReadLine();
    Console.WriteLine("Unesi datum rođenja");
    string pattern = "dd/MM/yyyy";
    DateTime dateOfBirth = DateTime.ParseExact(Console.ReadLine(), pattern, null);

    return (personName, dateOfBirth);

}

static int articleNameIndexFinder(string articleSearchedName, List<(string Name, int Amount, int Price, DateTime DateOfExpiry)> articleList)
{
    int foundIndex = -1;
    for (int i = 0; i < articleList.Count; i++)
    {
        if (articleSearchedName == articleList[i].Name)
        {
            foundIndex = i;
            break;
        }
        else
        {
            foundIndex = -1;
        }
    }

    return foundIndex;
}

static void removeExpiredItem(List<(string Name, int Amount, int Price, DateTime DateOfExpiry)> articleList)
{
    for (int i = 0; i < articleList.Count; i++)
    {
        TimeSpan daysUntilExpiry = articleList[i].DateOfExpiry.Subtract(DateTime.Now);
        if (daysUntilExpiry.Days < 0)
        {
            articleList.RemoveAt(i);
        }
    }

}