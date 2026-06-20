public class PlayerScore
{
    // HERMETYZACJA: Używamy właściwości (get; set;) zamiast gołych zmiennych publicznych.
    public string Name { get; set; }
    public int Attempts { get; set; }
    public int TimeInSeconds { get; set; }
    public int Difficulty { get; set; }
    public bool IsNewGamePlus { get; set; }
}