using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _speakerNameText;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private GameObject _continueButton;

    private Queue<DialogueLine> lines;
    private DialogueLine currentLine = new DialogueLine();
    private bool isTyping;

    public GameObject ContinueButton { get => _continueButton; set => _continueButton = value; }


    private void Start()
    {
        lines = new Queue<DialogueLine>();
    }

    //Starts a dialogue.
    public void StartDialogue(DialogueBranchSO dialogue)
    {
        GameManager.Instance.DisableAllInput();
        _animator.SetBool("IsOpen", true);
        GameManager.Instance.UniversalEventSystem.SetSelectedGameObject(_continueButton);
        lines.Clear();
        foreach (DialogueLine line in dialogue.Lines)
            lines.Enqueue(line);

        DisplayNextLine();
    }

    //Displays the next line of dialogue and updates the UI.
    public void DisplayNextLine()
    {
        if (isTyping)
        {
            StopCoroutine(TypeText());
            _dialogueText.text = currentLine.Text;
            return;
        }
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();
        //Change speaker sprite
        _speakerNameText.text = currentLine.Speaker.Name;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        _dialogueText.text = "";
        isTyping = true;
        foreach (char character in currentLine.Text.ToCharArray())
        {
            _dialogueText.text += character;
            yield return null;
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        GameManager.Instance.EnableAllInput();
        _animator.SetBool("IsOpen", false);
    }
}
