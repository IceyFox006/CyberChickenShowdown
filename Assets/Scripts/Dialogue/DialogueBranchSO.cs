using UnityEngine;

[CreateAssetMenu(fileName = "DialogueBranchSO", menuName = "Scriptable Objects/DialogueBranchSO")]
public class DialogueBranchSO : ScriptableObject
{
    [SerializeField] private DialogueLine[] _lines;

    public DialogueLine[] Lines { get => _lines; set => _lines = value; }
}
