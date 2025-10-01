using UnityEngine;

public static class StaticData
{
    private static PlayerSO player1;
    private static PlayerSO player2;

    //private static int winnerID;
    //private static FighterSO winningFighter;
    //private static FighterSO losingFigher;

    //public static int WinnerID { get => winnerID; set => winnerID = value; }
    //public static FighterSO WinningFighter { get => winningFighter; set => winningFighter = value; }
    //public static FighterSO LosingFigher { get => losingFigher; set => losingFigher = value; }
    public static PlayerSO Player1 { get => player1; set => player1 = value; }
    public static PlayerSO Player2 { get => player2; set => player2 = value; }

    public static PlayerSO GetWinner()
    {
        if (player1.IsWinner)
            return player1;
        else
            return player2;
    }
    public static void ResetPlayers()
    {
        player1.Reset();
        player2.Reset();
    }
    public static void ResetWinner()
    {
        player1.IsWinner = false;
        player2.IsWinner = false;
    }
}
