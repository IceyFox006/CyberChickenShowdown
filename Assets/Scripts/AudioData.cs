using UnityEngine;

[System.Serializable]
public class AudioData
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField][Range(0, 1)] float _volume;

    private AudioSource source;

    public AudioClip Clip { get => _clip; set => _clip = value; }
    public float Volume { get => _volume; set => _volume = value; }
    public AudioSource Source { get => source; set => source = value; }
    public string Name { get => _name; set => _name = value; }
}
