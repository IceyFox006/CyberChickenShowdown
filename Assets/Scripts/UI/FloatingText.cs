using UnityEngine;

[System.Serializable]
public class FloatingText
{
    [SerializeField] private bool _isInternal;
    [SerializeField] private string _text;
    [SerializeField] private Color _color;
    [SerializeField] private int _size = 24;
    [SerializeField] private bool _isBold;
    [SerializeField] private bool _isItalic;

    public string Text { get => _text; set => _text = value; }
    public Color Color { get => _color; set => _color = value; }
    public bool IsInternal { get => _isInternal; set => _isInternal = value; }
    public bool IsBold { get => _isBold; set => _isBold = value; }
    public bool IsItalic { get => _isItalic; set => _isItalic = value; }
    public int Size { get => _size; set => _size = value; }
}
