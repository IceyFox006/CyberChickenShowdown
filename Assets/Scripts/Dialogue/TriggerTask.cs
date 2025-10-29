using UnityEngine;

public class TriggerTask : MonoBehaviour
{
    [SerializeField] private TaskSO _task;

    private DialogueHandler _dialogueHandler;

    private void Start()
    {
        _dialogueHandler = FindFirstObjectByType<DialogueHandler>();
    }



}
