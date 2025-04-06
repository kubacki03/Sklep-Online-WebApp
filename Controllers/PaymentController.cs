using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using WebApplication2.Controllers;
using WebApplication2.Models;

public class PaymentController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    Dictionary<Produkt, int> mapa = new Dictionary<Produkt, int>();
    private JaroWinkler JaroWinkler = new JaroWinkler();


    public PaymentController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;

    }




    [HttpPost]
    public IActionResult CreateCheckoutSession(Klient klient)
    {
        var idKlienta = HttpContext.Session.GetInt32("idKlienta");
        if (!idKlienta.HasValue) {
            HttpContext.Session.SetObjectAsJson("klient", klient);
        }
        

        var koszyk = HttpContext.Session.GetObjectFromJson<Dictionary<int, int>>("Koszyk") ?? new Dictionary<int, int>();
        var produkty = _context.Produkty
            .Where(p => koszyk.Keys.Contains(p.IdProduktu))
            .ToList();
        var client = new Stripe.StripeClient(Environment.GetEnvironmentVariable("STRIPE_KEY"));

      



        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
      
        if (idKlienta.HasValue) {
     
            Console.WriteLine("Pobralem idKlienta z sesji");
            var znizka = _context.DailyZnizki.FirstOrDefault(p => (p.IdKlienta == idKlienta && p.Data == today));
        
            
            if (znizka != null) {
                Console.WriteLine("Klient ma  znizke");
            foreach (var prods in produkty)
            {
                if (prods.IdProduktu == znizka.IdProduktu)
                {
                    prods.Cena = prods.Cena * (1 - znizka.Znizka);
                        Console.WriteLine("Wybrany " + prods.Nazwa + " nowa cena " + prods.Cena);
                }
            }
        } }
        
      
        var lineItems = produkty.Select(item => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(item.Cena * 100)  , 
                Currency = "pln",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.Nazwa
                }
            },
            Quantity = koszyk[item.IdProduktu]
        }).ToList();



        var options = new SessionCreateOptions
        {
            
            PaymentMethodTypes = new List<string> { "card","klarna","blik" },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = Url.Action("Success", "Payment", null, Request.Scheme),
            CancelUrl = Url.Action("Cancel", "Payment", null, Request.Scheme)
        };

        var service = new SessionService(client);
        
        Session session = service.Create(options);

       
        return Redirect(session.Url);
    }

    [HttpGet]
    public IActionResult Success()
    {

        Klient klient = HttpContext.Session.GetObjectFromJson<Klient>("klient");
        var idKlienta = HttpContext.Session.GetInt32("idKlienta");

        if (!idKlienta.HasValue)
        {
            var istniejącyKlient = _context.Klienci.FirstOrDefault(k =>
            k.Imie == klient.Imie &&
            k.Nazwisko == klient.Nazwisko &&
            k.Login == klient.Login &&
            k.Email == klient.Email);

            if (istniejącyKlient == null)
            {

                _context.Klienci.Add(klient);
                _context.SaveChanges();
            }
            else
            {

                klient = istniejącyKlient;
            }

        }
        else
        {
            klient = _context.Klienci.FirstOrDefault(k => (k.IdKlienta == idKlienta));
        }
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        var znizka = _context.DailyZnizki.FirstOrDefault(p => (p.IdKlienta == idKlienta && p.Data == today));


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
                    koszt += ilosc * (produkt.Cena * (1-znizka.Znizka));
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
        var zamowienie = new Zamowienie { IdOdbiorcy = klient.IdKlienta, Data = data, Koszt = koszt, Kontrolna = kontrolna };
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

        

        return View();
    }

    [HttpGet]
    public IActionResult Cancel()
    {
       
        return View();
    }
}
