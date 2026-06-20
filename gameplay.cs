using System;

// 1. ABSTRAKCJA: Tworzymy główną klasę bazową (szablon). Jest oznaczona jako 'abstract', 
// co oznacza, że nie można stworzyć "po prostu gry", trzeba wybrać jej konkretny rodzaj.
public abstract class BaseGame
{
    // Zmienna chroniona: dostępna dla tej klasy i tych, które po niej dziedziczą.
    protected Random rnd = new Random();

    // Główny silnik gry - dziedziczony przez wszystkie tryby gry
    public PlayerScore Play(int difficulty, Settings settings)
    {
        int maxNumber = 50; 
        if (difficulty == 2) maxNumber = 100;
        if (difficulty == 3) maxNumber = 250;

        int hiddenNumber = rnd.Next(1, maxNumber + 1);
        int attempts = 0;
        
        // POLIMORFIZM: Ta metoda odpali się inaczej dla gry standardowej, a inaczej dla NG+
        int betLimit = SetupBet(settings); 

        DateTime startTime = DateTime.Now;
        string[] lowPL = { "Za malo!", "Wiecej!", "Nisko!", "Podnies!", "W gore!" };
        string[] highPL = { "Za duzo!", "Mniej!", "Wysoko!", "Obniz!", "W dol!" };

        while (true)
        {
            attempts++; 
            if (settings.Language == "PL") Console.WriteLine("\nProba numer: " + attempts);
            else Console.WriteLine("\nAttempt number: " + attempts);

            // POLIMORFIZM: Przestrzeganie specjalnych zasad (np. tasowanie liczb)
            ApplyRules(ref hiddenNumber, attempts, maxNumber, settings);

            if (settings.Language == "PL") Console.Write("Podaj liczbe: ");
            else Console.Write("Enter number: ");

            string input = Console.ReadLine();
            int guess = 0;
            
            if (int.TryParse(input, out guess) == false)
            {
                if (settings.Language == "PL") Console.WriteLine("Blad! Wpisz liczbe.");
                else Console.WriteLine("Error! Enter a number.");
                continue; 
            }

            if (guess == hiddenNumber)
            {
                DateTime endTime = DateTime.Now;
                int seconds = (int)(endTime - startTime).TotalSeconds; 

                if (settings.Language == "PL") Console.WriteLine("Zgadles! Podaj imie:");
                else Console.WriteLine("Correct! Enter name:");
                
                PlayerScore score = new PlayerScore();
                score.Name = Console.ReadLine();
                score.Attempts = attempts;
                score.TimeInSeconds = seconds;
                score.Difficulty = difficulty;
                score.IsNewGamePlus = GetIsNgPlus(); // Abstrakcyjne oznaczanie

                return score;
            }

            if (betLimit > 0 && attempts >= betLimit)
            {
                if (settings.Language == "PL") Console.WriteLine("Przegrales zaklad!");
                else Console.WriteLine("Bet lost!");
                return null; 
            }

            int msgIndex = rnd.Next(0, 5);
            if (guess < hiddenNumber) Console.WriteLine(lowPL[msgIndex]);
            else Console.WriteLine(highPL[msgIndex]);
        }
    }

    // --- ABSTRAKCJA I POLIMORFIZM ---
    // Metody 'virtual' mogą być nadpisane przez dzieci, ale mają zachowanie domyślne.
    // Metody 'abstract' MUSZĄ być nadpisane przez dzieci.
    
    protected virtual int SetupBet(Settings settings) { return 0; }
    
    protected virtual void ApplyRules(ref int hiddenNumber, int attempts, int maxNumber, Settings settings) { }
    
    protected abstract bool GetIsNgPlus();
}


// 2. DZIEDZICZENIE: Klasa StandardGame rozszerza BaseGame
public class StandardGame : BaseGame
{
    // 3. POLIMORFIZM: Nadpisujemy metodę o zakład - pytamy o niego tylko tutaj
    protected override int SetupBet(Settings settings)
    {
        int betLimit = 0;
        if (settings.AskForBet == true)
        {
            // Nieskończona pętla, która nie wypuści gracza, dopóki nie poda cyfr
            while (true) 
            {
                if (settings.Language == "PL") Console.WriteLine("Ile maksymalnie prob potrzebujesz? (Wpisz 0 aby pominac zaklad):");
                else Console.WriteLine("Max attempts? (Enter 0 to skip bet):");
                
                string input = Console.ReadLine();

                // Jeśli udało się zamienić tekst na liczbę, przerywamy pętlę (break) i gramy dalej
                if (int.TryParse(input, out betLimit) == true)
                {
                    break; 
                }
                // Jeśli wpisano litery, wyświetlamy błąd i pętla leci od nowa
                else
                {
                    if (settings.Language == "PL") Console.WriteLine("Blad! Musisz wpisac liczbe.");
                    else Console.WriteLine("Error! You must enter a number.");
                }
            }
        }
        return betLimit;
    }

    protected override bool GetIsNgPlus() { return false; }
}


// 2. DZIEDZICZENIE: Klasa NgPlusGame rozszerza BaseGame
public class NgPlusGame : BaseGame
{
    // 3. POLIMORFIZM: Zmieniamy zasady - tasujemy liczbę co 7 prób!
    protected override void ApplyRules(ref int hiddenNumber, int attempts, int maxNumber, Settings settings)
    {
        if (attempts % 7 == 0)
        {
            hiddenNumber = rnd.Next(1, maxNumber + 1);
            if (settings.Language == "PL") Console.WriteLine("Liczba zostala przelosowana! (NG+)");
            else Console.WriteLine("Number randomized! (NG+)");
        }
    }

    protected override bool GetIsNgPlus() { return true; }
}