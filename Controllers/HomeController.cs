using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication2.Controllers;
using WebApplication2.Models;

using static System.Runtime.InteropServices.JavaScript.JSType;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    Dictionary<Produkt, int> mapa = new Dictionary<Produkt, int>();
    private JaroWinkler JaroWinkler = new JaroWinkler();

 
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
      
    }

    public IActionResult AddProdukt()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Zamow()
    {
        var idKlienta = HttpContext.Session.GetInt32("idKlienta");

        var klient = _context.Klienci.FirstOrDefault(k => (k.IdKlienta == idKlienta));
        
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        var znizka = _context.DailyZnizki.FirstOrDefault(p => (p.IdKlienta == idKlienta && p.Data==today ));

       
        var koszyk = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Koszyk") ?? new Dictionary<int, int>();
        Double koszt = 0;
        DateTime data = DateTime.Now;
       
        long kontrolna = new Random().NextInt64();


        foreach (var para in koszyk)
        {
            int idProduktu = para.Key;
            int ilosc = para.Value;
            var produkt = _context.Produkty.FirstOrDefault(p => p.IdProduktu == idProduktu);

       
                if (znizka != null)
                {
                    if (znizka.IdProduktu == idProduktu)
                    {
                        koszt += ilosc * (produkt.Cena * znizka.Znizka);
                }
                else
                {
                    koszt += ilosc * produkt.Cena;
                }

            }
            else
            {
                koszt += ilosc * produkt.Cena;
            }



           

        };
        var zamowienie = new Zamowienie { IdOdbiorcy = klient.IdKlienta, Data = data,Koszt=koszt,Kontrolna=kontrolna };
        _context.Zamowienia.Add(zamowienie);
        _context.SaveChanges();

        var zamowienie2 = _context.Zamowienia
        .FirstOrDefault(z => z.IdOdbiorcy == klient.IdKlienta && z.Kontrolna == kontrolna);
        

       
        foreach (var para in koszyk)
        {
            int idProduktu = para.Key;  
            int ilosc = para.Value;     

         
            var produkt = _context.Produkty.FirstOrDefault(p => p.IdProduktu == idProduktu);

            if (produkt != null)
            {
                produkt.Ilosc -= ilosc;

               
                if (produkt.Ilosc < 0)
                {
                    produkt.Ilosc = 0;
                }


                var produktZamowienie = new ProduktZamowienie
                {
                    IdZamowienia = zamowienie2.IdZamowienia,
                    IdProduktu = produkt.IdProduktu,
                    Ilosc = para.Value
                };

                _context.ProduktyZamowienia.Add(produktZamowienie);
              
                _context.SaveChanges();
            }
        }

  
        HttpContext.Session.Remove("Koszyk");

   
        return RedirectToAction("Index");
    }
    public IActionResult LosujZnizke()
    {
        int idKlienta = (int)HttpContext.Session.GetInt32("idKlienta");
        Random rand = new Random();
        double znizka = 0.1 + rand.NextDouble() * (0.4 - 0.1);

        var produkty = _context.Produkty.ToList();

        int indeks = (int)rand.NextInt64(0, produkty.Count);
        var idProduktu = produkty[indeks].IdProduktu;
        var data = DateOnly.FromDateTime(DateTime.Now);

        var dailyZnizka = new DailyZnizka
        {
            IdKlienta = idKlienta,
            IdProduktu = idProduktu,
            Data = data,
            Znizka = znizka
        };
        _context.Add(dailyZnizka);
        _context.SaveChanges();

       
        var produkt = produkty[indeks];
        var nowaCena = produkt.Cena * (1 - znizka);

       
        ViewData["Znizka"] = znizka;
        ViewData["Produkt"] = produkt.Nazwa;
        ViewData["NowaCena"] = nowaCena;

        return RedirectToAction("pokazHistorie", new { znizka = Math.Round(znizka * 100, 2), nazwaProduktu = produkt.Nazwa, nowaCena = Math.Round(nowaCena, 2) });
    }
    public IActionResult pokazHistorie(double? znizka = null, string nazwaProduktu = null, double? nowaCena = null)
    {
        var idKlienta = HttpContext.Session.GetInt32("idKlienta");
        var zamowienia = _context.Zamowienia
                                 .Where(z => z.IdOdbiorcy == idKlienta)
                                 .Include(z => z.ProduktZamowienia)
                                 .ThenInclude(pz => pz.Produkt)
                                 .ToList();

        var today = DateOnly.FromDateTime(DateTime.Now);
        var dailyZnizka = _context.DailyZnizki
                                  .FirstOrDefault(p => p.IdKlienta == idKlienta && p.Data== today);

        if (dailyZnizka != null)
        {
            var produkt = _context.Produkty.FirstOrDefault(p => p.IdProduktu == dailyZnizka.IdProduktu);
            ViewData["Znizka"] = Math.Round(dailyZnizka.Znizka * 100, 2);
            ViewData["Produkt"] = produkt?.Nazwa;
            ViewData["NowaCena"] = Math.Round(produkt?.Cena * (1 - dailyZnizka.Znizka) ?? 0, 2);
        }

        return View("HistoriaZakupow", zamowienia);
    }


    [HttpPost]
    public IActionResult UsunKoszyk()
    {
      
        HttpContext.Session.Remove("Koszyk");

      
        return RedirectToAction("Index");
    }



    [HttpPost]
    public IActionResult ZwiekszIlosc(int idProduktu, int ilosc)
    {
        var koszyk = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Koszyk") ?? new Dictionary<int, int>();

        if (koszyk.ContainsKey(idProduktu))
        {
            var produkt = _context.Produkty.FirstOrDefault(p => p.IdProduktu == idProduktu);
            if (produkt != null && koszyk[idProduktu] + ilosc <= produkt.Ilosc) // Sprawdzenie maksymalnej iloœci
            {
                koszyk[idProduktu] += ilosc;
            }
        }

        HttpContext.Session.SetObjectAsJson("Koszyk", koszyk);
        return RedirectToAction("Koszyk");
    }

    [HttpPost]
    public IActionResult ZmniejszIlosc(int idProduktu, int ilosc)
    {
        var koszyk = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Koszyk") ?? new Dictionary<int, int>();

        if (koszyk.ContainsKey(idProduktu))
        {
            koszyk[idProduktu] -= ilosc;
            if (koszyk[idProduktu] <= 0) 
            {
                koszyk.Remove(idProduktu);
            }
        }

        HttpContext.Session.SetObjectAsJson("Koszyk", koszyk);
        return RedirectToAction("Koszyk");
    }


    [HttpPost]
    public IActionResult Zaloguj(string login, string password)
    {
        string hashedHaslo = HashingExample.HashString(password);
       
        Klient klient = _context.Klienci
            .FirstOrDefault(p => (p.Login == login && p.Haslo == hashedHaslo) || (p.Email == login && p.Haslo == hashedHaslo));

        if (klient != null)
        {
            HttpContext.Session.SetInt32("idKlienta", klient.IdKlienta);
           
            return RedirectToAction("Index"); 
            
        }
        else
        {
           
            ModelState.AddModelError("", "Nie znaleziono konta.");
            return View("Logowanie"); 
        }
    }


    public IActionResult PokazLogowanie()
    {
        return View("Logowanie");

    }

    public IActionResult PokazRejestrowanie()
    {
        return View("Rejestrowanie");

    }
    public IActionResult Mail(Klient klient)
    {
        Klient klient2 = HttpContext.Session.GetObjectFromJson<Klient>("klient");
        bool klientIstnieje = _context.Klienci.Any(k =>
            k.Imie == klient.Imie &&
            k.Nazwisko == klient.Nazwisko &&
            k.Email == klient.Email
        );

        if (klientIstnieje)
        {
            ModelState.AddModelError("", "Klient o takich danych ju¿ istnieje.");
            return View("Rejestrowanie", klient); 
        }

        HttpContext.Session.SetObjectAsJson("klient", klient);
        Random random = new Random();
        int kod = random.Next(1000, 10000); 
        EmailService emailService = new EmailService();
        emailService.SendEmail(klient.Email, kod); 

        
        HttpContext.Session.SetInt32("kod", kod);

        
        return View("Weryfikacja");
    }
    [HttpPost]
    public IActionResult WeryfikujKod(int kod)
    {
        
        var kodWeryfikacyjny = HttpContext.Session.GetInt32("kod");

        if (kodWeryfikacyjny.HasValue && kodWeryfikacyjny.Value == kod)
        {

           
            return RedirectToAction("Rejestruj");
        }
        else
        {
            
            ModelState.AddModelError("", "Niepoprawny kod weryfikacyjny.");
          
            return View("Weryfikacja");
        }
    }

   
  


    public IActionResult Rejestruj()
    {
        Klient klient = HttpContext.Session.GetObjectFromJson<Klient>("klient");
        bool klientIstnieje = _context.Klienci.Any(k =>
            k.Imie == klient.Imie &&
            k.Nazwisko == klient.Nazwisko &&
            k.Email == klient.Email
        );
       
        if (!klientIstnieje)
        {
           
            klient.Haslo = HashingExample.HashString(klient.Haslo);
            _context.Klienci.Add(klient); 
            _context.SaveChanges(); 

            HttpContext.Session.SetInt32("idKlienta", klient.IdKlienta);
            return RedirectToAction("Index"); 
        }
        else
        {
            ModelState.AddModelError("", "Klient o takich danych ju¿ istnieje.");
            return View("Rejestrowanie", klient); 
        }
    }


    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddProdukt(Produkt produkt)
    {
        
      

            _context.Produkty.Add(produkt);
            _context.SaveChanges();
            return RedirectToAction("Index"); 
      
        
       
    }

    public IActionResult AddToCart(int idProduktu, int ilosc)
{
    var koszyk = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Koszyk") ?? new Dictionary<int, int>();

    if (koszyk.ContainsKey(idProduktu))
    {
        koszyk[idProduktu] += ilosc;
    }
    else
    {
        koszyk[idProduktu] = ilosc;
    }

    HttpContext.Session.SetObjectAsJson("Koszyk", koszyk);

    return RedirectToAction("Index");
}


    public IActionResult Wyloguj()
    {
        if (HttpContext.Session.GetInt32("idKlienta").HasValue) { 
        HttpContext.Session.Remove("idKlienta"); }
        return RedirectToAction("Index");
    }

    public IActionResult Koszyk()
    {  
        var idKlienta = HttpContext.Session.GetInt32("idKlienta");

        Klient klient = null;
        DailyZnizka znizka = null;
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
       
        if (idKlienta.HasValue)
        {
            klient = _context.Klienci.FirstOrDefault(k => k.IdKlienta == idKlienta.Value);
            znizka = _context.DailyZnizki.FirstOrDefault(p => (p.IdKlienta == idKlienta && p.Data == today));
        }

       
        ViewBag.Klient = klient;
        var koszyk = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Koszyk") ?? new Dictionary<int, int>();

    var produkty = _context.Produkty
        .Where(p => koszyk.Keys.Contains(p.IdProduktu))
        .ToList();


       
        if (znizka != null)
        {
            Console.WriteLine("Znizka nie jest nullem");
            foreach(var p in produkty)
            {
                if (p.IdProduktu == znizka.IdProduktu)
                {
                    p.Cena = p.Cena * (1-znizka.Znizka);
                    Console.WriteLine("produkt " + p.IdProduktu + "znizkowy" + znizka.IdProduktu);                }
            }
        }
      


    var widokModel = produkty.ToDictionary(p => p, p => koszyk[p.IdProduktu]);

    return View(widokModel);
}






    
    public IActionResult Index()
    {
        
        var idKlienta = HttpContext.Session.GetInt32("idKlienta");

        Klient klient = null;
        DailyZnizka znizka = null;
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        
        if (idKlienta.HasValue)
        {
            klient = _context.Klienci.FirstOrDefault(k => k.IdKlienta == idKlienta.Value);
            znizka = _context.DailyZnizki.FirstOrDefault(z => z.IdKlienta == idKlienta && z.Data==today);
        }

        
        ViewBag.Klient = klient;

        if (znizka != null)
        {
            ViewBag.IdProduktuZnizka = znizka.IdProduktu;
            ViewBag.Znizka = znizka.Znizka;
        }

       
        var produkty = _context.Produkty.ToList();

        return View(produkty);
    }


    public IActionResult poKategori(string kategoria)
{
   
    var produkty = string.IsNullOrEmpty(kategoria)
        ? _context.Produkty.ToList() 
        : _context.Produkty.Where(p => p.Kategoria == kategoria).ToList();

    ViewData["Category"] = string.IsNullOrEmpty(kategoria) ? "Wszystkie produkty" : kategoria;
    return View("Index", produkty);
}

    public IActionResult poNazwie(string nazwa)
    {

        Console.WriteLine("Nazwa " + nazwa);

        var produkty = _context.Produkty.ToList();
        List<Produkt> prods = new List<Produkt>();
        foreach (var p in produkty)
        {
            Console.WriteLine(p.Nazwa+" " + JaroWinkler.GetSimilarity(p.Nazwa, nazwa));
            if (JaroWinkler.GetSimilarity(p.Nazwa, nazwa) > 0.60)
            {
                Console.WriteLine("Pasuje " + p.Nazwa+ " " + nazwa);
                prods.Add(p);
            }
        }

        return View("Index", prods);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}


