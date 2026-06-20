using System;

public class Gameplay
{
    public PlayerScore Play(int difficulty, bool isNgPlus, Settings settings)
    {
        Random rnd = new Random();
        int maxNumber = 50; // domyślnie łatwy

        if (difficulty == 2) maxNumber = 100;
        if (difficulty == 3) maxNumber = 250;

        int hiddenNumber = rnd.Next(1, maxNumber + 1);
        int attempts = 0;
        int betLimit = 0;

        // Pytanie o zakład
        if (isNgPlus == false && settings.AskForBet == true)
        {
            if (settings.Language == "PL") Console.WriteLine("Ile maksymalnie prob potrzebujesz?");
            else Console.WriteLine("Max attempts?");
            
            string betInput = Console.ReadLine();
            int.TryParse(betInput, out betLimit);
        }

        // Mierzenie czasu za pomocą wbudowanego zegara
        DateTime startTime = DateTime.Now;

        string[] lowPL = { "Za malo!", "Wiecej!", "Nisko!", "Podnies!", "W gore!" };
        string[] highPL = { "Za duzo!", "Mniej!", "Wysoko!", "Obniz!", "W dol!" };
        string[] lowEN = { "Too low!", "More!", "Low!", "Raise!", "Up!" };
        string[] highEN = { "Too high!", "Less!", "High!", "Lower!", "Down!" };

        while (true)
        {
            attempts++;
            
            if (settings.Language == "PL") Console.WriteLine("\nProba numer: " + attempts);
            else Console.WriteLine("\nAttempt number: " + attempts);

            // NG+ przelosowanie
            if (isNgPlus == true && attempts % 7 == 0)
            {
                hiddenNumber = rnd.Next(1, maxNumber + 1);
                if (settings.Language == "PL") Console.WriteLine("Liczba zostala przelosowana!");
                else Console.WriteLine("Number randomized!");
            }

            if (settings.Language == "PL") Console.Write("Podaj liczbe: ");
            else Console.Write("Enter number: ");

            string input = Console.ReadLine();
            int guess = 0;
            if (int.TryParse(input, out guess) == false)
            {
                if (settings.Language == "PL") 
                {
                    Console.WriteLine("Blad! Musisz wpisac liczbe. Sprobuj ponownie.");
                }
                else 
                {
                    Console.WriteLine("Error! You must enter a number. Try again.");
                }
                continue; // wraca na początek pętli, jeśli ktoś wpisał np. litery
            }

            // WARUNEK WYGRANEJ
            if (guess == hiddenNumber)
            {
                DateTime endTime = DateTime.Now;
                TimeSpan diff = endTime - startTime; // odejmujemy czasy
                int seconds = (int)diff.TotalSeconds;

                if (settings.Language == "PL") Console.WriteLine("Zgadles! Podaj swoje imie:");
                else Console.WriteLine("Correct! Enter name:");
                
                string name = Console.ReadLine();

                // Tworzymy nowy obiekt z wynikiem i wypełniamy go danymi
                PlayerScore score = new PlayerScore();
                score.Name = name;
                score.Attempts = attempts;
                score.TimeInSeconds = seconds;
                score.Difficulty = difficulty;
                score.IsNewGamePlus = isNgPlus;

                return score;
            }

            // Przegrana przez zakład
            if (betLimit > 0 && attempts >= betLimit)
            {
                if (settings.Language == "PL") Console.WriteLine("Przegrales zaklad!");
                else Console.WriteLine("Bet lost!");
                return null;
            }

            // Błędny strzał - losujemy odpowiedź
            int msgIndex = rnd.Next(0, 5);
            if (guess < hiddenNumber)
            {
                if (settings.Language == "PL") Console.WriteLine(lowPL[msgIndex]);
                else Console.WriteLine(lowEN[msgIndex]);
            }
            else
            {
                if (settings.Language == "PL") Console.WriteLine(highPL[msgIndex]);
                else Console.WriteLine(highEN[msgIndex]);
            }
        }
    }
}
