Sklep online - sklep online z rozszerzonymi funkcjonalnościami

## DEMO
https://youtu.be/gEpwFOSvnXw

## Funkcje
-dodawanie/usuwanie produtków do koszyka i edycja koszyka
-filtrowanie produktów po kategorii i nazwie
-logowanie/rejestracja
-płatności poprzez bramkę płatniczą Stripe
-weryfikacja logowania poprzez potwierdzenie kodu wysłanego na email
-losowanie własnych dziennych promocji
-historia zakupów użytkownika


## Technologie
- ASP.NET CORE
- STRIPE
- HTML
- CSS
-ENTITY FRAMEWORK
-SQL SERVER

## Instalacja
1) Pobierz projekt
   git clone https://github.com/kubacki03/Sklep-Online-WebApp.git
2)Przejdz do katalogu z projektem
  cd WebApplication2
3)Dodaj zmienne środowiskowe (klucz stripe, email oraz haslo do gmail)
4)Zauktalizuj bazę danych
  w konsoli menadzera pakietów wpisz update-database
5)Skompiluj
  dotnet build
6)Uruchom projekt
  dotnet run
