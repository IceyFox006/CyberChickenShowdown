using UnityEngine;

public static class StaticData
{
    private static PlayerSO player1;
    private static PlayerSO player2;

    private static int initialMatchCount;
    private static int currentMatchCount;
    public static PlayerSO Player1 { get => player1; set => player1 = value; }
    public static PlayerSO Player2 { get => player2; set => player2 = value; }
    public static int InitialMatchCount { get => initialMatchCount; set => initialMatchCount = value; }
    public static int CurrentMatchCount { get => currentMatchCount; set => currentMatchCount = value; }

    public static PlayerSO GetWinner()
    {
        if (player1.Wins > player2.Wins)
            return player1;
        else
            return player2;
        //if (player1.IsWinner)
        //    return player1;
        //else
        //    return player2;
    }
    public static void ResetGame()
    {
        ResetPlayers();
        initialMatchCount = 0;
        currentMatchCount = 0;
    }
    public static void ResetPlayers()
    {
        player1.Reset();
        player2.Reset();
    }
    //public static void ResetWinner()
    //{
    //    player1.IsWinner = false;
    //    player2.IsWinner = false;
    //}
}
