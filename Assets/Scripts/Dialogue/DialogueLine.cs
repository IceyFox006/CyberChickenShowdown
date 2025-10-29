using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [SerializeField] private CharacterSO _speaker;
    [TextArea(3,5)][SerializeField] private string _text;

    public CharacterSO Speaker { get => _speaker; set => _speaker = value; }
    public string Text { get => _text; set => _text = value; }
}
