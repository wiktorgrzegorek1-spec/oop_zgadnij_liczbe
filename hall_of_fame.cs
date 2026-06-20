using System;
using System.Collections.Generic;

public class HallOfFame
{
    // HERMETYZACJA: Lista jest prywatna! Z zewnątrz można do niej dodać wynik
    // tylko za pomocą kontrolowanej, publicznej metody AddScore.
    private List<PlayerScore> scores = new List<PlayerScore>();

    public void AddScore(PlayerScore score)
    {
        scores.Add(score);
    }

    public void Clear()
    {
        scores.Clear();
    }

    public bool HasAnyScores()
    {
        if (scores.Count > 0) return true;
        else return false;
    }

    public void ShowTop5(int difficulty)
    {
        List<PlayerScore> filteredScores = new List<PlayerScore>();
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i].Difficulty == difficulty)
            {
                filteredScores.Add(scores[i]);
            }
        }

        // Sortowanie bąbelkowe
        for (int i = 0; i < filteredScores.Count; i++)
        {
            for (int j = 0; j < filteredScores.Count - 1; j++)
            {
                bool swap = false; 
                
                if (filteredScores[j].Attempts > filteredScores[j + 1].Attempts) swap = true;
                else if (filteredScores[j].Attempts == filteredScores[j + 1].Attempts)
                {
                    if (filteredScores[j].TimeInSeconds > filteredScores[j + 1].TimeInSeconds) swap = true;
                }

                if (swap == true)
                {
                    PlayerScore temp = filteredScores[j];
                    filteredScores[j] = filteredScores[j + 1];
                    filteredScores[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("\n--- TOP 5 ---");
        int count = 0; 
        
        for (int i = 0; i < filteredScores.Count; i++)
        {
            if (count >= 5) break; 
            
            PlayerScore s = filteredScores[i];
            string marker = "";
            if (s.IsNewGamePlus == true) marker = "[NG+]";

            Console.WriteLine((count + 1) + ". " + s.Name + " - Proby: " + s.Attempts + ", Czas: " + s.TimeInSeconds + "s " + marker);
            count++; 
        }
    }
}