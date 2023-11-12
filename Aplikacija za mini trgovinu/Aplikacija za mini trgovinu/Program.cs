
using System.Xml.Linq;

/*
 1 - Artikli
    1. Unos artikla - done
    2. Brisanje artikla
        a. Po imenu artikla
        b. Sve one kojima je istekao rok trajanja
    3. Uredivanje artikla
        a. Zasebno proizvoda (pronade se artikal pa se bira sto se zeli promijeniti)
        b. Popust/poskupljenje na sve proizvode unutar trgovine
    4. Ispis
        a. Svih artikala kako s spremljeni (format: ime (kolicina) - cijena - broj dana od/do isteka)
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

var articleList = new List<(string Name, int Amount, int Price, DateTime DateOfExpiry)> {};
var workers = new List<(string Name, DateTime dateOfBirth)> {};


while (true)
{
    Console.WriteLine("1 - Artikli\n2 - Radnici\n3 - Racuni\n4 - Statistika\n0 - Izlaz iz aplikacije");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("1 - Artikli\n\t1. Unos artikla\n\t2. Brisanje artikla\n\t3. Uredivanje artikla\n\t4. Ispis\n\t0. Povratak na glavni izbornik");
            var choice1 = Console.ReadLine();
            switch(choice1)
            {
                case "1":
                    (string, int, int, DateTime) tempTuple = GetEntry();
                    do
                    {
                        Console.WriteLine("Zelite li spremiti promjene? (y/n)");
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
                        }
                        else
                        {
                            Console.WriteLine("Pogresan odabir.");
                        }
                    } while (true);
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("1 - Artikli\n\t2. Brisanje artikla\n\t\ta. Po imenu\n\t\tb. Svih onih koji imaju vise od 65 godina\n\t\tc. Povratak u glavni izbornik");
                    var choice2 = Console.ReadLine();

                    break;
                case "3":
                    break;
                case "4":
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
    string pattern = "dd/mm/yyyy";
    DateTime dateOfExpiry = DateTime.ParseExact(Console.ReadLine(), pattern, null);

    return (articleName, amount, price, dateOfExpiry);

}

static (string, DateTime) GetWorker()
{
    Console.WriteLine("Unesi ime radnika: ");
    var personName = Console.ReadLine();
    Console.WriteLine("Unesi datum rođenja");
    string pattern = "dd/mm/yyyy";
    DateTime dateOfBirth = DateTime.ParseExact(Console.ReadLine(), pattern, null);

    return (personName, dateOfBirth);

}