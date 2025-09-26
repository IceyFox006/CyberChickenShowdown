using UnityEngine;

public static class StaticData
{
    private static int winnerID;
    private static FighterSO winningFighter;
    private static FighterSO losingFigher;

    public static int WinnerID { get => winnerID; set => winnerID = value; }
    public static FighterSO WinningFighter { get => winningFighter; set => winningFighter = value; }
    public static FighterSO LosingFigher { get => losingFigher; set => losingFigher = value; }
}
