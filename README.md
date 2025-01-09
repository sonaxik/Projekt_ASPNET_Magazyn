# Dokumentacja projektu "Projekt_ASPNET_Magazyn"

## 1. Opis projektu

Projekt "Projekt_ASPNET_Magazyn" to aplikacja webowa zarządzająca asortymentem i zamówieniami w magazynie.
Przeznaczona jest dla dwóch typów użytkowników:

- **Administratora**: zarządzanie produktami, zamówieniami i użytkownikami.
- **Zwykłego użytkownika**: przeglądanie asortymentu oraz składanie zamówień.

Aplikacja została zbudowana z użyciem ASP.NET Core, z integracją bazy danych SQL Server dla przechowywania danych o użytkownikach, produktach oraz zamówieniach.

---

## 2. Struktura projektu

### Główne elementy projektu:

- **Test.sln**: Plik rozwiązania Visual Studio.
- **/Controllers**: Zawiera kontrolery aplikacji:
  - `AsortymentController`: Zarządza operacjami na produktach.
  - `ZamowieniaController`: Obsługuje zamówienia.
  - `HomeController`: Odpowiada za stronę główną.
- **/Models**: Zawiera modele danych:
  - `Asortyment`: Reprezentuje produkty magazynowe.
  - `Zamowienia`: Reprezentuje złożone zamówienia.
  - `ApplicationUser`: Rozszerza domyślną klasę użytkownika ASP.NET Identity.
- **/Views**: Widoki aplikacji podzielone na foldery dla każdego kontrolera.
- **/wwwroot**: Zasoby statyczne (CSS, JavaScript, obrazy).
- **appsettings.json**: Konfiguracja aplikacji, w tym połączenie z bazą danych.

---

## 3. Kluczowe funkcjonalności

### Dla administratora:

- Zarządzanie asortymentem (dodawanie, edytowanie, usuwanie produktów).
- Przeglądanie i zarządzanie zamówieniami.
- Zarządzanie użytkownikami (dodawanie ról, przeglądanie).

### Dla zwykłego użytkownika:

- Przeglądanie dostępnych produktów.
- Składanie zamówień.
- Przeglądanie historii swoich zamówień.

---

## 4. Instrukcja uruchomienia

1. **Wymagania wstępne:**

   - Zainstalowany .NET SDK 6.0 lub nowszy.
   - SQL Server do obsługi bazy danych.

2. **Konfiguracja:**

   - W pliku `appsettings.json` ustaw poprawne dane połączenia z bazą danych:
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=Test;Integrated Security=True;TrustServerCertificate=True"
     }
     ```

3. **Migracje bazy danych:**

   - W konsoli NuGet wykonaj komendy:
     ```
     Update-Database
     ```

4. **Uruchomienie aplikacji:**

   - W Visual Studio wybierz `Test.sln`.
   - Ustaw `HTTPS` jako profil uruchomienia.
   - Kliknij "Start" (F5).

---

## 5. Informacje o bazie danych

### Tabele:

- **AspNetUsers**: Przechowuje dane użytkowników.
- **AspNetRoles**: Przechowuje role użytkowników (Admin, User).
- **Asortyment**: Przechowuje dane o produktach (nazwa, cena, ilość).
- **Zamowienia**: Przechowuje dane o zamówieniach (produkt, użytkownik, ilość, data).

### Inicjalizacja danych:

W pliku `Program.cs` aplikacja:

- Tworzy domyślne role `Admin` i `User`.
- Dodaje użytkownika admina (`admin@admin.com`) z hasłem (`Admin1234!`) i użytkownika (`user@user.com`) z hasłem (`User1234!`).
- Inicjalizuje tabelę `Asortyment` przykładowymi produktami.

---

## 6. Uprawnienia użytkowników

### Administrator (Admin):

- Dostęp do wszystkich funkcji aplikacji.
- Możliwość przeglądania, edytowania i usuwania produktów oraz zamówień.
- Podgląd na wszystkich użytkowników i sumaryczną liczbę ich zamówień.

### Zwykły użytkownik (User):

- Dostęp do przeglądania asortymentu.
- Możliwość składania zamówień.
- Możliwość przeglądania swoich zamówień.

---

## 7. Widoki aplikacji

### Dla administratora:

- **Asortyment/Index**: Lista produktów z opcjami zarządzania.
- **Zamowienia/Index**: Lista zamówień z możliwością edycji i usunięcia.
- **Uzytkownicy/Index**: Lista użytkowników z sumaryczną liczbą ich zamówień.

### Dla użytkownika:

- **Asortyment/UserIndex**: Lista dostępnych produktów.
- **Zamowienia/UserIndex**: Historia zamówień użytkownika.

