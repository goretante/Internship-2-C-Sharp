
using System.Xml.Linq;
using System;
using System.Threading;

/*
 1 - Artikli
    1. Unos artikla - done
    2. Brisanje artikla - done
        a. Po imenu artikla - done
        b. Sve one kojima je istekao rok trajanja - done
    3. Uredivanje artikla
        a. Zasebno proizvoda (pronade se artikal pa se bira sto se zeli promijeniti) - done
        b. Popust/poskupljenje na sve proizvode unutar trgovine - done
    4. Ispis
        a. Svih artikala kako s spremljeni (format: ime (kolicina) - cijena - broj dana od/do isteka) - done
        b. Svih artikala sortirano po imenu - done
        c. Svih artikala sortirano po datumu silazno - done
        d. Svih artikala sortirano do datumu uzlazno - done
        e. Svih artikala sortirano po kolicini - done
        f. Najprodavaniji artikl
        g. Najmanje prodavan artikl
 2 - Radnici
    1. Unos radnika - done
    2. Brisanje radnika
        a. Po imenu - done
        b. Svih onih koji imaju vise od 65 godina - done
    3. Uredivanje radnika - done
    4. Ispis
        a. svih radnika (format: ime - godine) - done
        b. svih radnika kojima je rodendan u tekucem mjesecu - done
 3 - Racuni
    1. Unos novog računa - ispišu se svi proizvodi koji su dostupni u dućanu, zatim se upiše ime proizvoda i količina (isti proizvod moguće samo jednom unijeti). 
       Radnja se ponavlja sve dok se ne unesu svi potrebni proizvodi. Kad je unos završen unosi se ključna rijeć te radnik može vidjeti što je sve na računi i dalje može poništiti račun ili potvrditi njegovo printanje. 
       Ako je radnja potvrdna, ukljanjaju se artikli sa stanja (ukoliko im je stanje 0 brišu se skroz). 
       Id se generira kao prvi sljedeći broj od zadnjeg računa. 
       Isto tako, potrebno je da program odmah izračuna ukupnu cijenu svakog artikla i DateTime izdavanja računa (trenutni DateTime). 
        Radniku se ispiše u konzoli formatirani ispis računa kojeg korisnik vidi (isti kao u sekciji ispis računa).
    2. Ispis
        a. Svih racuna u formatu (id - datum i vrijeme - ukupni iznos (potrebno izracunati iz kolicine artikala i cijene))
            i. Mogucnost odabira racuna po id-u pa se ispisu svi detalji racuna (id - datum i vrijeme - proizvodi) (format: ime - kolicina, svaki u novom redu) te ukupna cijena
 4 - Statistika (potrebna sifra za ulaz)
    1. Ukupan broj artikala u trgovini
    2. Vrijednost artikala koji nisu prodani
    3. Vrijednost svih artikala koji su prodani
    4. Stanje po mjesecima - vlasnik unosi datum i godinu za koji ga zanima, 
                             iznos plaća radnika, 
                             iznos najma i svih ostalih troškova, 
                             a aplikacija izračuna u tom mjesecu koliki je iznos zaradio/izgubio po formuli: 
                             ukupna zarada u tom mjesecu * ⅓ - plaće - ostali troškovi
 0 - Izlaz iz aplikacije - done
 */

// lista artikala
var articleList = new List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> {
    ("Cedevita naranca", 8, 14.2, new DateTime(2023, 2, 1)),
    ("Cedevita limun", 5, 15.0, new DateTime(2024, 2, 1))
};

// lista radnika
var workersList = new List<(string Name, DateTime dateOfBirth)> {
    ("Ivan Ivanović", new DateTime(1988, 11, 5)),
    ("Zoran Matić", new DateTime(1957, 8, 1))
};

var receiptsList = new List<(int ID, DateTime receiptTime)> {
    ( 1,  new DateTime(2023, 11, 15, 14, 33, 15) )

};

/*
var soldItemsList = new List<List<(string Name, int Amount, double Price)>> {

};
soldItemsList.Add(new List<(string Name, int Amount, double Price)>());
soldItemsList[0].Add(("Cedevita limun", 10, 15.0));
*/

// izbornik
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
                    (string, int, double, DateTime) tempTuple = GetEntry();
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
                                    Console.WriteLine("Uspješno ste obrisali artikal.");
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
                                    Thread.Sleep(1000);
                                    break;

                                }
                                else if (option == "n")
                                {
                                    Console.WriteLine("Odustali ste od brisanja artikla.");
                                    Thread.Sleep(1000);
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
                    Console.Clear();
                    Console.WriteLine("1 - Artikli\n\t3. Uređivanje artikala\n\t\ta. Izmjena jednog proizvoda\n\t\tb. Popusti/poskupljenja proizvoda\n\t\tc. Povratak u glavni izbornik");
                    var choice3 = Console.ReadLine();
                    switch (choice3)
                    {
                        case "a":
                            editItem(articleList);
                            break;

                        case "b":
                            editPrices(articleList);
                            break;

                        case "c":
                            Console.WriteLine("Povratak u glavni izbornik!");
                            Thread.Sleep(1000);
                            break;
                    }
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("1 - Artikli\n\t4. Ispis\n\t\ta. Svih artikala kako su spremljeni\n\t\tb. Svih artikala po nazivu\n\t\tc. Svih artikala po datumu (silazno)\n\t\td. Svih artikala po datumu (uzlazno)");
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

                        case "b":
                            Console.WriteLine("Lista svih artikala raspoređena po nazivu:");
                            var listSortedByName = articleList.OrderBy(x => x.Name).ToList();
                            foreach (var item in listSortedByName)
                            {
                                TimeSpan daysUntilExpiry = item.DateOfExpiry.Subtract(DateTime.Now);
                                Console.WriteLine($"{item.Name} ({item.Amount}) - {item.Price} - {daysUntilExpiry.Days}");
                            }
                            Console.WriteLine("Pritisnite tipku ENTER za nastavak...");
                            Console.ReadLine();
                            break;

                        case "c":
                            Console.WriteLine("Lista svih artikala raspoređena po datumu silazno:");
                            List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> listSortedByDescendingDates = articleList;
                            listSortedByDescendingDates.Sort((t1, t2) => t2.DateOfExpiry.CompareTo(t1.DateOfExpiry));
                            foreach (var item in listSortedByDescendingDates)
                            {
                                TimeSpan daysUntilExpiry = item.DateOfExpiry.Subtract(DateTime.Now);
                                Console.WriteLine($"{item.Name} ({item.Amount}) - {item.Price} - {daysUntilExpiry.Days}");
                            }
                            Console.WriteLine("Pritisnite tipku ENTER za nastavak...");
                            Console.ReadLine();
                            break;

                        case "d":
                            Console.WriteLine("Lista svih artikala raspoređena po datumu uzlazno:");
                            List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> listSortedByAscendingDates = articleList;
                            listSortedByAscendingDates.Sort((t1, t2) => t1.DateOfExpiry.CompareTo(t2.DateOfExpiry));
                            foreach (var item in listSortedByAscendingDates)
                            {
                                TimeSpan daysUntilExpiry = item.DateOfExpiry.Subtract(DateTime.Now);
                                Console.WriteLine($"{item.Name} ({item.Amount}) - {item.Price} - {daysUntilExpiry.Days}");
                            }
                            Console.WriteLine("Pritisnite tipku ENTER za nastavak...");
                            Console.ReadLine();
                            break;

                        case "e":
                            Console.WriteLine("Lista svih artikala raspoređena po količini artikala:");
                            List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> listSortedByAmount = articleList;
                            listSortedByAmount.OrderBy(t1 => t1.Amount);
                            foreach (var item in listSortedByAmount)
                            {
                                TimeSpan daysUntilExpiry = item.DateOfExpiry.Subtract(DateTime.Now);
                                Console.WriteLine($"{item.Name} ({item.Amount}) - {item.Price} - {daysUntilExpiry.Days}");
                            }
                            Console.WriteLine("Pritisnite tipku ENTER za nastavak...");
                            Console.ReadLine();
                            break;

                        case "f":
                            Console.WriteLine("Povratak u glavni izbornik!");
                            Thread.Sleep(1000);
                            break;
                    }
                    break;

                case "0":
                    Console.WriteLine("Povratak u glavni izbornik!");
                    Thread.Sleep(1000);
                    break;
            }
            Console.Clear();
            continue;
        case "2":
            Console.Clear();
            Console.WriteLine("2 - Radnici\n\t1. Unos radnika\n\t2. Brisanje radnika\n\t3. Uređivanje radnika\n\t4. Ispis\n\t0. Povratak na glavni izbornik");
            var choice5 = Console.ReadLine();
            switch (choice5)
            {
                case "1":
                    Console.WriteLine("2 - Radnici\n\t1. Unos radnika");
                    (string, DateTime) newWorker = GetWorker();
                    Console.WriteLine("Želite li spremiti promjene? (y/n)");
                    do
                    {
                        var option = Console.ReadLine();
                        if (option == "y")
                        {
                            workersList.Add(newWorker);
                            Console.WriteLine("Uspješno ste dodali radnika.");
                            Thread.Sleep(1000);
                            break;

                        }
                        else if (option == "n")
                        {
                            Console.WriteLine("Odustali ste od dodavanja radnika.");
                            Thread.Sleep(1000);
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
                    Console.WriteLine("2 - Radnici\n\t2. Brisanje radnika\n\t\ta. Po imenu\n\t\tb. Svi stariji od 65 godina");
                    var choice7 = Console.ReadLine();
                    switch (choice7)
                    {
                        case "a":
                            deleteWorker(workersList);
                            break;

                        case "b":
                            DeleteWorkersOverAge(workersList);
                            break;

                        case "c":
                            Console.WriteLine("Povratak u glavni izbornik!");
                            Thread.Sleep(1000);
                            break;
                    }
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("2 - Radnici\n\t3. Uređivanje radnika");
                    EditWorker(workersList);
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("2 - Radnici\n\t4. Ispis\n\t\ta. Ispis svih radnika\n\t\tb. Ispis radnika kojima je rođendan u tekućem mjesecu.");
                    var choice6 = Console.ReadLine();
                    switch (choice6)
                    {
                        case "a":
                            PrintWorkers(workersList);
                            break;

                        case "b":
                            PrintWorkersBornInThisMonth(workersList);
                            break;

                        case "c":
                            Console.WriteLine("Povratak u glavni izbornik!");
                            Thread.Sleep(1000);
                            break;
                    }
                    break;

                case "0":
                    Console.WriteLine("Povratak na glavni izbornik!");
                    Thread.Sleep(1000);
                    break;
                default:
                    Console.WriteLine("Pogresan unos!");
                    Thread.Sleep(1000);
                    break;
            }
            Console.Clear();
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
            Thread.Sleep(1000);
            Console.Clear();
            continue;
    }
    break;
}

// funckije
static (string, int, double, DateTime) GetEntry()
{
    Console.WriteLine("Unesi ime artikla:");
    var articleName = Console.ReadLine();
    Console.WriteLine("Unesi količinu: ");
    int amount;
    int.TryParse(Console.ReadLine(), out amount);
    Console.WriteLine("Unesi cijenu: ");
    double price;
    double.TryParse(Console.ReadLine(), out price);
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

static int articleNameIndexFinder(string articleSearchedName, List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> articleList)
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

static void removeExpiredItem(List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> articleList)
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

static void editItem(List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> articleList)
{
    Console.WriteLine("Unesite ime proizvoda koji želite izmijeniti: ");
    var articleName = Console.ReadLine();
    var foundIndex = articleNameIndexFinder(articleName, articleList);

    if (foundIndex == -1)
    {
        Console.WriteLine("Ne postoji taj proizvod u trgovini.");
        Thread.Sleep(1000);

    }
    else
    {
        Console.WriteLine("Odaberite segment koji želite izmijeniti:\n\t1 - Ime artikla\n\t2 - Količina\n\t3 - Cijena\n\t4 - Rok trajanja");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.WriteLine("Unesite novo ime artikla: ");
                var articleNewName = Console.ReadLine();
                Console.WriteLine("Želite li spremiti promjene? (y/n)");
                do
                {
                    var choice = Console.ReadLine();
                    if (choice == "y")
                    {
                        (string, int, double, DateTime) tempName = (articleNewName, articleList[foundIndex].Amount, articleList[foundIndex].Price, articleList[foundIndex].DateOfExpiry);
                        articleList[foundIndex] = tempName;
                        Console.WriteLine("Uspješno ste izmijenili ime.");
                        Thread.Sleep(1000);
                        break;

                    }
                    else if (choice == "n")
                    {
                        Console.WriteLine("Odustali ste od brisanja artikla.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pogrešan unos slova.");
                    }
                } while (true);
                break;

            case "2":
                Console.WriteLine("Unesite novu kolicinu artikla:");
                int articleNewAmount;
                int.TryParse(Console.ReadLine(), out articleNewAmount);
                Console.WriteLine("Želite li spremiti promjene? (y/n)");
                do
                {
                    var choice = Console.ReadLine();
                    if (choice == "y")
                    {
                        (string, int, double, DateTime) tempAmount = (articleList[foundIndex].Name, articleNewAmount, articleList[foundIndex].Price, articleList[foundIndex].DateOfExpiry);
                        articleList[foundIndex] = tempAmount;
                        Console.WriteLine("Uspješno ste izmijenili količinu.");
                        Thread.Sleep(1000);
                        break;

                    }
                    else if (choice == "n")
                    {
                        Console.WriteLine("Odustali ste od promjene artikla.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pogrešan unos slova.");
                    }
                } while (true);
                break;

            case "3":
                Console.WriteLine("Unesite novu cijenu artikla:");
                double articleNewPrice;
                double.TryParse(Console.ReadLine(), out articleNewPrice);
                Console.WriteLine("Želite li spremiti promjene? (y/n)");
                do
                {
                    var choice = Console.ReadLine();
                    if (choice == "y")
                    {
                        (string, int, double, DateTime) tempPrice = (articleList[foundIndex].Name, articleList[foundIndex].Amount, articleNewPrice, articleList[foundIndex].DateOfExpiry);
                        articleList[foundIndex] = tempPrice;
                        Console.WriteLine("Uspješno ste izmijenili cijenu artikla.");
                        Thread.Sleep(1000);
                        break;

                    }
                    else if (choice == "n")
                    {
                        Console.WriteLine("Odustali ste od promjene artikla.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pogrešan unos slova.");
                    }
                } while (true);
                break;

            case "4":
                Console.WriteLine("Unesite novi datum isteka roka (dd/mm/gggg): ");
                string pattern = "dd/MM/yyyy";
                DateTime newDateOfExpiry = DateTime.ParseExact(Console.ReadLine(), pattern, null);
                Console.WriteLine("Želite li spremiti promjene? (y/n)");
                do
                {
                    var choice = Console.ReadLine();
                    if (choice == "y")
                    {
                        (string, int, double, DateTime) tempDate = (articleList[foundIndex].Name, articleList[foundIndex].Amount, articleList[foundIndex].Price, newDateOfExpiry);
                        articleList[foundIndex] = tempDate;
                        Console.WriteLine("Uspješno ste izmijenili datum roka trajanja.");
                        Thread.Sleep(1000);
                        break;

                    }
                    else if (choice == "n")
                    {
                        Console.WriteLine("Odustali ste od promjene artikla.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pogrešan unos slova.");
                    }
                } while (true);
                break;
        }
    }
}

static void editPrices(List<(string Name, int Amount, double Price, DateTime DateOfExpiry)> articleList)
{
    Console.WriteLine("Unesite postotak poskupljenja/sniženja cijene (pozitivan broj za poskupljenje, negativan za sniženje):");
    double percentage;
    double.TryParse(Console.ReadLine(), out percentage);
    Console.WriteLine();
    Console.WriteLine("Želite li spremiti promjene? (y/n)");
    do
    {
        var choice = Console.ReadLine();
        if (choice == "y")
        {
            if (percentage == 0)
            {
                Console.WriteLine("Nema promjene u cijeni.");
                Thread.Sleep(1000);
            }
            else if (percentage > 0)
            {
                for (int i = 0; i < articleList.Count; i++)
                {
                    double newArticlePrice;
                    newArticlePrice = Math.Round(articleList[i].Price + articleList[i].Price * percentage / 100, 2);
                    (string, int, double, DateTime) temp = (articleList[i].Name, articleList[i].Amount, newArticlePrice, articleList[i].DateOfExpiry);
                    articleList[i] = temp;
                }
                Console.WriteLine($"Cijena je uspješno podignuta za {percentage}%.");
                Thread.Sleep(1000);
            }
            else
            {
                for (int i = 0; i < articleList.Count; i++)
                {
                    double newArticlePrice;
                    newArticlePrice = Math.Round(articleList[i].Price - articleList[i].Price * percentage / 100, 2);
                    (string, int, double, DateTime) temp = (articleList[i].Name, articleList[i].Amount, newArticlePrice, articleList[i].DateOfExpiry);
                    articleList[i] = temp;
                }
                Console.WriteLine($"Cijena je uspješno snižena za {percentage}%.");
                Thread.Sleep(1000);

            }
            break;

        }
        else if (choice == "n")
        {
            Console.WriteLine("Odustali ste od promjene artikla.");
            Thread.Sleep(1000);
            break;
        }
        else
        {
            Console.WriteLine("Pogrešan unos slova.");
        }
    } while (true);


}

static void PrintWorkers(List<(string Name, DateTime DateOfBirth)> workersList)
{
    Console.Clear();
    Console.WriteLine("Lista radnika (Ime i prezime - Godine):");
    foreach (var worker in workersList)
    {
        TimeSpan yearsOld = DateTime.Now.Subtract(worker.DateOfBirth);
        Console.WriteLine($"{worker.Name} - {yearsOld.Days / 365 }");
    }
    Console.WriteLine("Pritisnite tipku ENTER za nastavak...");
    Console.ReadLine();
}

static void PrintWorkersBornInThisMonth(List<(string Name, DateTime DateOfBirth)> workersList)
{
    Console.Clear();
    Console.WriteLine("Lista radnika kojima je rođendan ovaj mjesec:");
    foreach (var worker in workersList)
    {
        if (worker.DateOfBirth.Month == DateTime.Now.Month)
        {
            TimeSpan yearsOld = DateTime.Now.Subtract(worker.DateOfBirth);
            Console.WriteLine($"{worker.Name} - {yearsOld.Days / 365}");
        }
    }
    Console.WriteLine("Pritisnite tipku ENTER za nastavak...");
    Console.ReadLine();
}

static void deleteWorker(List<(string Name, DateTime DateOfBirth)> workersList)
{
    Console.Clear();
    Console.WriteLine("Unesi ime i prezime radnika kojega želite izbrisati: ");
    var status = 0;
    var searchedWorker = Console.ReadLine();
    for (int i = 0; i < workersList.Count; i++)
    {
        if (searchedWorker == workersList[i].Name)
        {
            Console.WriteLine("Želite li spremiti promjene? (y/n)");
            do
            {
                var option = Console.ReadLine();
                if (option == "y")
                {
                    workersList.RemoveAt(i);
                    status = 1;
                    break;
                }
                else if (option == "n")
                {
                    status = 2;
                    break;
                }
                else
                {
                    Console.WriteLine("Pogrešan unos slova");
                }
            } while (true);
        }
        else
        {
            status = 0;
        }
    }

    if (status == 0)
    {
        Console.WriteLine("Navedeno ime nije u listi radnika.");
        Thread.Sleep(1000);
    }
    else if (status == 1)
    {
        Console.WriteLine("Uspješno ste obrisali radnika.");
        Thread.Sleep(1000);
    }
    else if (status == 2)
    {
        Console.WriteLine("Odustali ste od brisanja.");
        Thread.Sleep(1000);
    }
}

static void DeleteWorkersOverAge(List<(string Name, DateTime DateOfBirth)> workersList)
{
    Console.Clear();
    List<(string Name, DateTime DateOfBirth)> tempWorkersList = workersList;
    int overAgeWorkersCount = 0;
    int whileCounter = 0;

    foreach (var worker in tempWorkersList)
    {
        TimeSpan yearsOld = DateTime.Now.Subtract(worker.DateOfBirth);
        var yearsOldv2 = yearsOld.TotalDays / 365;
        if (yearsOldv2 >= 65)
        {
            overAgeWorkersCount++;
        }
    }

    Console.WriteLine("Želite li obrisati sve radnike koji imaju preko 65 godina? (y/n)");
    do
    {
        var option = Console.ReadLine();
        if (option == "y")
        {
            while (whileCounter != overAgeWorkersCount)
            {
                for (int i = 0; i < workersList.Count; i++) {
                    
                    TimeSpan yearsOldv3 = DateTime.Now.Subtract(workersList[i].DateOfBirth);
                    var yearsOldv4 = yearsOldv3.TotalDays / 365;
                    if (yearsOldv4 >= 65)
                    {
                        workersList.RemoveAt(i);
                        whileCounter++;
                    }
                }
            }
            Console.WriteLine("Uspješno ste obrisali radnike starije od 65.");
            break;
        }
        else if (option == "n")
        {
            Console.WriteLine("Odustali ste od brisanja radnika.");
            break;
        }
        else
        {
            Console.WriteLine("Pogrešan unos slova");
        }
    } while (true);

    Thread.Sleep(1000);
}

static int workerNameIndexFinder(string workerSearchedName, List<(string Name, DateTime DateOfBirth)> workersList)
{
    int foundIndex = -1;
    for (int i = 0; i < workersList.Count; i++)
    {
        if (workerSearchedName == workersList[i].Name)
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

static void EditWorker(List<(string Name, DateTime DateOfBirth)> workersList)
{
    Console.WriteLine("Unesite ime i prezime radnika kojeg želite izmijeniti: ");
    var workerToEdit = Console.ReadLine();
    var indexOfWorker = workerNameIndexFinder(workerToEdit, workersList);

    if (indexOfWorker == -1)
    {
        Console.WriteLine("Radnik s tim imenom ne radi u dućanu");
        Thread.Sleep(1000);
    }
    else
    {
        Console.WriteLine("Odaberite segment koji želite izmijeniti:\n\t1 - Ime radnika\n\t2 - Datum rođenja");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.WriteLine("Unesite novo ime radnika: ");
                var workerNewName = Console.ReadLine();
                Console.WriteLine("Želite li spremiti promjene? (y/n)");
                do
                {
                    var choice = Console.ReadLine();
                    if (choice == "y")
                    {
                        (string, DateTime) tempName = (workerNewName, workersList[indexOfWorker].DateOfBirth);
                        workersList[indexOfWorker] = tempName;
                        Console.WriteLine("Uspješno ste izmijenili ime.");
                        Thread.Sleep(1000);
                        break;

                    }
                    else if (choice == "n")
                    {
                        Console.WriteLine("Odustali ste od brisanja artikla.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pogrešan unos slova.");
                    }
                } while (true);
                break;

            case "2":
                Console.WriteLine("Unesite novi datum rođenja (dd/mm/gggg): ");
                string pattern = "dd/MM/yyyy";
                DateTime newDateOfBirth = DateTime.ParseExact(Console.ReadLine(), pattern, null);
                Console.WriteLine("Želite li spremiti promjene? (y/n)");
                do
                {
                    var choice = Console.ReadLine();
                    if (choice == "y")
                    {
                        (string, DateTime) tempDate = (workersList[indexOfWorker].Name, newDateOfBirth);
                        workersList[indexOfWorker] = tempDate;
                        Console.WriteLine("Uspješno ste izmijenili datum roka trajanja.");
                        Thread.Sleep(1000);
                        break;

                    }
                    else if (choice == "n")
                    {
                        Console.WriteLine("Odustali ste od promjene artikla.");
                        Thread.Sleep(1000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pogrešan unos slova.");
                    }
                } while (true);
                break;
        }
    }

}