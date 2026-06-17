using System;

public class GameController
{
    public Settings settings = new Settings();
    public HallOfFame hof = new HallOfFame();
    public Gameplay gameplay = new Gameplay();

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            if (settings.Language == "PL") Console.WriteLine("Witaj w Zgadnij Liczbe 2!");
            else Console.WriteLine("Welcome to Guess Number 2!");
            
            Console.WriteLine("1. Nowa Gra (Zgadnij liczbe 1)");
            Console.WriteLine("2. Nowa Gra Plus (NG+)");
            Console.WriteLine("3. Ustawienia");
            
            if (hof.HasAnyScores() == true)
            {
                Console.WriteLine("4. Hall of Fame (TOP 5)");
            }

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                StartGame(false); // false oznacza, że to nie jest NG+
            }
            else if (choice == "2")
            {
                StartGame(true); // true oznacza, że to jest NG+
            }
            else if (choice == "3")
            {
                OpenSettings();
            }
            else if (choice == "4" && hof.HasAnyScores() == true)
            {
                ShowHallOfFame();
            }
        }
    }

    public void StartGame(bool isNgPlus)
    {
        if (settings.Language == "PL") Console.WriteLine("Wybierz trudnosc (1-Latwy, 2-Sredni, 3-Trudny):");
        else Console.WriteLine("Choose difficulty (1-Easy, 2-Medium, 3-Hard):");
        
        string input = Console.ReadLine();
        int diff = 0;
        int.TryParse(input, out diff);

        // Uruchamiamy grę tylko jeśli wpisano 1, 2 lub 3
        if (diff >= 1 && diff <= 3)
        {
            PlayerScore result = gameplay.Play(diff, isNgPlus, settings);
            
            // Jeśli wynik nie jest pusty (czyli gracz nie przegrał zakładu), to go dodajemy
            if (result != null)
            {
                hof.AddScore(result);
            }
        }
    }

    public void OpenSettings()
    {
        Console.WriteLine("\n--- USTAWIENIA ---");
        Console.WriteLine("A - Jezyk (Obecnie: " + settings.Language + ")");
        Console.WriteLine("B - Pytaj o zaklad (Obecnie: " + settings.AskForBet + ")");
        Console.WriteLine("C - Wyczysc Hall of Fame");
        Console.WriteLine("D - Powrot");

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
            Console.WriteLine("Na pewno? (T/N)");
            string confirm = Console.ReadLine();
            if (confirm == "T" || confirm == "t")
            {
                hof.Clear();
            }
        }
    }

    public void ShowHallOfFame()
    {
        Console.WriteLine("\n--- HALL OF FAME ---");
        Console.WriteLine("Wybierz poziom: 1. Latwy | 2. Sredni | 3. Trudny");
        
        string input = Console.ReadLine();
        int diff = 0;
        int.TryParse(input, out diff);

        if (diff >= 1 && diff <= 3)
        {
            hof.ShowTop5(diff);
        }
        
        Console.WriteLine("Wcisnij Enter, aby wrocic...");
        Console.ReadLine();
    }
}