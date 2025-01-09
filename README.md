Dokumentacja projektu "Projekt_ASPNET_Magazyn"

1. Opis projektu

Projekt "Projekt_ASPNET_Magazyn" to aplikacja webowa zarządzająca asortymentem i zamówieniami w magazynie.
Przeznaczona jest dla dwóch typów użytkowników:
- Administratora: zarządzanie produktami, zamówieniami i użytkownikami.
- Zwykłego użytkownika: przeglądanie asortymentu oraz składanie zamówień.

Aplikacja została zbudowana z użyciem ASP.NET Core, z integracją bazy danych SQL Server dla przechowywania danych o użytkownikach, produktach oraz zamówieniach.

2. Struktura projektu

Główne elementy projektu:
- Test.sln: Plik rozwiązania Visual Studio.
- /Controllers: Zawiera kontrolery aplikacji:
  - AsortymentController: Zarządza operacjami na produktach.
  - ZamowieniaController: Obsługuje zamówienia.
  - HomeController: Odpowiada za stronę główną.
- /Models: Zawiera modele danych:
  - Asortyment: Reprezentuje produkty magazynowe.
  - Zamowienia: Reprezentuje złożone zamówienia.
  - ApplicationUser: Rozszerza domyślną klasę użytkownika ASP.NET Identity.
- /Views: Widoki aplikacji podzielone na foldery dla każdego kontrolera.
- /wwwroot: Zasoby statyczne (CSS, JavaScript, obrazy).
- appsettings.json: Konfiguracja aplikacji, w tym połączenie z bazą danych.

3. Kluczowe funkcjonalności

Dla administratora:
- Zarządzanie asortymentem (dodawanie, edytowanie, usuwanie produktów).
- Przeglądanie i zarządzanie zamówieniami.
- Zarządzanie użytkownikami (dodawanie ról, przeglądanie).

Dla zwykłego użytkownika:
- Przeglądanie dostępnych produktów.
- Składanie zamówień.
- Przeglądanie historii swoich zamówień.
