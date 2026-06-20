using System;

public class GameController
{
    public Settings settings = new Settings();
    public HallOfFame hof = new HallOfFame();

    public void Start()
    {
        while (true)
        {
            Console.Clear(); 
            
            if (settings.Language == "PL")
            {
                Console.WriteLine("Witaj w Zgadnij Liczbe 2!");
                Console.WriteLine("1. Nowa Gra (Zgadnij liczbe 1)");
                Console.WriteLine("2. Nowa Gra Plus (NG+)");
                Console.WriteLine("3. Ustawienia");
                if (hof.HasAnyScores()) Console.WriteLine("4. Hall of Fame (TOP 5)");
            }
            else
            {
                Console.WriteLine("Welcome to Guess Number 2!");
                Console.WriteLine("1. New Game (Guess Number 1)");
                Console.WriteLine("2. New Game Plus (NG+)");
                Console.WriteLine("3. Settings");
                if (hof.HasAnyScores()) Console.WriteLine("4. Hall of Fame (TOP 5)");
            }

            string choice = Console.ReadLine(); 

            if (choice == "1") StartGame(false);
            else if (choice == "2") StartGame(true);
            else if (choice == "3") OpenSettings(); 
            else if (choice == "4" && hof.HasAnyScores()) ShowHallOfFame(); 
        }
    }

    public void StartGame(bool isNgPlus)
    {
        if (settings.Language == "PL") Console.WriteLine("Wybierz trudnosc (1-Latwy, 2-Sredni, 3-Trudny):");
        else Console.WriteLine("Choose difficulty (1-Easy, 2-Medium, 3-Hard):");
        
        int diff = 0;
        int.TryParse(Console.ReadLine(), out diff);

        if (diff >= 1 && diff <= 3)
        {
            // ABSTRAKCJA W PRAKTYCE: Zmienna jest typu BaseGame, ale przypisujemy do 
            // niej konkretną instancję w zależności od wybranego trybu
            BaseGame gameEngine;
            
            if (isNgPlus == true) gameEngine = new NgPlusGame();
            else gameEngine = new StandardGame();

            // Odpalamy grę, wynik zapisujemy do bazy
            PlayerScore result = gameEngine.Play(diff, settings);
            if (result != null)
            {
                hof.AddScore(result); 
            }
        }
    }

    public void OpenSettings()
    {
        if (settings.Language == "PL")
        {
            Console.WriteLine("\n--- USTAWIENIA ---");
            Console.WriteLine("A - Jezyk (Obecnie: " + settings.Language + ")");
            Console.WriteLine("B - Pytaj o zaklad (Obecnie: " + settings.AskForBet + ")");
            Console.WriteLine("C - Wyczysc Hall of Fame");
            Console.WriteLine("D - Powrot");
        }
        else
        {
            Console.WriteLine("\n--- SETTINGS ---");
            Console.WriteLine("A - Language (Current: " + settings.Language + ")");
            Console.WriteLine("B - Ask for bet (Current: " + settings.AskForBet + ")");
            Console.WriteLine("C - Clear Hall of Fame");
            Console.WriteLine("D - Return");
        }

        string choice = Console.ReadLine();
        
        if (choice == "A" || choice == "a")
        {
            if (settings.Language == "PL") settings.Language = "EN";
            else settings.Language = "PL";
        }
        else if (choice == "B" || choice == "b")
        {
            if (settings.AskForBet == true) settings.AskForBet = false;
            else settings.AskForBet = true;
        }
        else if (choice == "C" || choice == "c")
        {
            if (settings.Language == "PL") Console.WriteLine("Na pewno? (T/N)");
            else Console.WriteLine("Are you sure? (Y/N)");
            
            string confirm = Console.ReadLine();
            if (confirm == "T" || confirm == "t" || confirm == "Y" || confirm == "y")
            {
                hof.Clear();
            }
        }
    }

    public void ShowHallOfFame()
    {
        if (settings.Language == "PL")
        {
            Console.WriteLine("\n--- HALL OF FAME ---");
            Console.WriteLine("Wybierz poziom: 1. Latwy | 2. Sredni | 3. Trudny");
        }
        else
        {
            Console.WriteLine("\n--- HALL OF FAME ---");
            Console.WriteLine("Choose difficulty: 1. Easy | 2. Medium | 3. Hard");
        }
        
        int diff = 0;
        int.TryParse(Console.ReadLine(), out diff);

        if (diff >= 1 && diff <= 3) hof.ShowTop5(diff);
        
        if (settings.Language == "PL") Console.WriteLine("Wcisnij Enter, aby wrocic...");
        else Console.WriteLine("Press Enter to return...");
        Console.ReadLine();
    }
}