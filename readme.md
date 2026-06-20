# Zgadnij liczbę 2!

Gra konsolowa będąca sequelem klasycznej gry w zgadywanie liczb, napisana w języku C#.

## Jak uruchomić?
W przypadku korzystania z systemu Linux, najlepiej program jest uruchomić komendą: `dotnet run`
W przypadku korzystania z Systemu Windows normalnie uruchomić w środowisku programistycznym, ale lepiej 

## Zasady gry
Gra polega na odgadnięciu ukrytej przez komputer liczby. Wygrywa ten, kto zrobi to w jak najmniejszej liczbie prób i w jak najkrótszym czasie.

### Tryby gry:
1. **Zgadnij liczbę 1 (Standard)** - Klasyczny tryb, w którym gracz przed rozpoczęciem może założyć się z systemem o maksymalną liczbę prób potrzebnych na odgadnięcie liczby.
2. **Nowa Gra Plus (NG+)** - Utrudniony tryb, w którym ukryta liczba jest losowana od nowa co 7 oddanych strzałów. Tryb zakładu jest tutaj niedostępny.

### Poziomy trudności:
- Łatwy (losowanie z przedziału 1-50)
- Średni (losowanie z przedziału 1-100)
- Trudny (losowanie z przedziału 1-250)

### Ustawienia gry
Z poziomu ekranu powitalnego dostępna jest opcja ustawień, w której gracz może:
- Zmienić język gry (PL / EN).
- Włączyć lub wyłączyć pytanie o tryb zakładu (domyślnie włączone).
- Całkowicie wyczyścić tablicę najlepszych wyników (wymaga potwierdzenia).

### Hall of Fame (TOP 5)
Gra zapisuje najlepsze wyniki z podziałem na poziomy trudności. Opcja rankingu pojawia się w menu głównym dopiero po rozegraniu pierwszej gry. Wyniki są sortowane rosnąco według liczby prób, a w przypadku remisów decyduje krótszy czas rozgrywki (zmierzony w sekundach). Wyniki z trybu Nowa Gra Plus otrzymują specjalne wyróżnienie `[NG+]`.
