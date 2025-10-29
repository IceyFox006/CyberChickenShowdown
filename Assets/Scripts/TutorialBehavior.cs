using System;
using System.Collections;
using UnityEngine;

public class TutorialBehavior : MonoBehaviour
{
    [SerializeField] private DialogueHandler _dialogueHandler;
    [SerializeField] private TaskSO _startTask;
    private TaskSO _currentTask;

    //[SerializeField] private List<TaskSO> _tasks = new List<TaskSO>();
    private void Start()
    {
        StartCoroutine(LateStart());
        PlayerMatch3.MatchTaskComplete += TaskCompleted;
    }
    private void OnDestroy()
    {
        PlayerMatch3.MatchTaskComplete -= TaskCompleted;
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        BeginTask(_startTask);
    }

    public void BeginTask(TaskSO task)
    {
        _currentTask = task;
        _dialogueHandler.StartDialogue(task.Dialogue);
    }

    //Make a match
    public void TaskCompleted()
    {
        Debug.Log("HERE");
        BeginTask(_currentTask.Next);
    }
    //Charge super bar
    //Use super

}
