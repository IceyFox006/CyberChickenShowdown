using UnityEngine;

[System.Serializable]
public class FloatingText
{
    [SerializeField] private bool _isInternal;
    [SerializeField] private string _text;
    [SerializeField] private Color _color;

    public string Text { get => _text; set => _text = value; }
    public Color Color { get => _color; set => _color = value; }
    public bool IsInternal { get => _isInternal; set => _isInternal = value; }
}
