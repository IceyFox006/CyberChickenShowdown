using UnityEngine;

[CreateAssetMenu(fileName = "TaskSO", menuName = "Scriptable Objects/TaskSO")]
public class TaskSO : ScriptableObject
{
    [SerializeField] private DialogueBranchSO _dialogue;
    private bool isComplete;
    [SerializeField] private TaskSO _next;

    public DialogueBranchSO Dialogue { get => _dialogue; set => _dialogue = value; }
    public bool IsComplete { get => isComplete; set => isComplete = value; }
    public TaskSO Next { get => _next; set => _next = value; }
}
