using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Scriptable Objects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _ID;
    [SerializeField] private FighterSO _fighter;
    private float savedSuper;
    private int wins;

    [SerializeField] private GameObject _uiIndicatorPrefab;
    public string Name { get => _name; set => _name = value; }
    public FighterSO Fighter { get => _fighter; set => _fighter = value; }
    public int ID { get => _ID; set => _ID = value; }
    public int Wins { get => wins; set => wins = value; }
    public float SavedSuper { get => savedSuper; set => savedSuper = value; }
    public GameObject UiIndicatorPrefab { get => _uiIndicatorPrefab; set => _uiIndicatorPrefab = value; }

    public void Reset()
    {
        _name = "Player " + _ID;
        savedSuper = 0;
        wins = 0;
    }
}
