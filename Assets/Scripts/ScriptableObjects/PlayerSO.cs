using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _ID;
    [SerializeField] private FighterSO _fighter;
    private bool isWinner;
    private int wins;

    public string Name { get => _name; set => _name = value; }
    public FighterSO Fighter { get => _fighter; set => _fighter = value; }
    public int ID { get => _ID; set => _ID = value; }
    public bool IsWinner { get => isWinner; set => isWinner = value; }
    public int Wins { get => wins; set => wins = value; }

    public void Reset()
    {
        _name = "Player " + _ID;
        _fighter = null;
        isWinner = false;
        wins = 0;
    }
}
