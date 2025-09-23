using UnityEngine;

[CreateAssetMenu(fileName = "BoardSO", menuName = "Scriptable Objects/BoardSO")]
public class BoardSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Inspector2DArrayLayout _gameBoardLayout;

    public string Name { get => _name; set => _name = value; }
    public Inspector2DArrayLayout GameBoardLayout { get => _gameBoardLayout; set => _gameBoardLayout = value; }
}
