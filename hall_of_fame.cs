using System;
using System.Collections.Generic;

public class HallOfFame
{
    public List<PlayerScore> scores = new List<PlayerScore>();

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
        if (scores.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowTop5(int difficulty)
    {
        // 1. Przepisujemy wyniki tylko z wybranego poziomu trudności
        List<PlayerScore> filteredScores = new List<PlayerScore>();
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i].Difficulty == difficulty)
            {
                filteredScores.Add(scores[i]);
            }
        }

        // 2. Sortowanie Bąbelkowe (Bubble Sort) - sortujemy od najlepszego
        for (int i = 0; i < filteredScores.Count; i++)
        {
            for (int j = 0; j < filteredScores.Count - 1; j++)
            {
                bool swap = false;
                
                // Jeśli obecny ma więcej prób niż następny, to zamieniamy miejscami (chcemy najmniej)
                if (filteredScores[j].Attempts > filteredScores[j + 1].Attempts)
                {
                    swap = true;
                }
                // Jeśli mają tyle samo prób, decyduje czas (chcemy najkrótszy)
                else if (filteredScores[j].Attempts == filteredScores[j + 1].Attempts)
                {
                    if (filteredScores[j].TimeInSeconds > filteredScores[j + 1].TimeInSeconds)
                    {
                        swap = true;
                    }
                }

                // Zamiana miejscami za pomocą zmiennej pomocniczej "temp"
                if (swap == true)
                {
                    PlayerScore temp = filteredScores[j];
                    filteredScores[j] = filteredScores[j + 1];
                    filteredScores[j + 1] = temp;
                }
            }
        }

        // 3. Wyświetlamy tylko pierwszych 5
        Console.WriteLine("\n--- TOP 5 ---");
        int count = 0;
        for (int i = 0; i < filteredScores.Count; i++)
        {
            if (count >= 5)
            {
                break; // przerywa pętlę, jeśli pokazaliśmy już 5 wyników
            }

            PlayerScore s = filteredScores[i];
            
            string marker = "";
            if (s.IsNewGamePlus == true)
            {
                marker = "[NG+]";
            }

            Console.WriteLine((count + 1) + ". " + s.Name + " - Proby: " + s.Attempts + ", Czas: " + s.TimeInSeconds + "s " + marker);
            count++;
        }
    }
}