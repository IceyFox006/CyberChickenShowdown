using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private AudioData[] _audioLibrary;
    [SerializeField] private GameObject _audioPlayerPrefab;

    public static AudioManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetUp();
    }
    public void SetUp()
    {
        for (int index = 0; index < _audioLibrary.Length; index++)
        {
            AudioData currentData = _audioLibrary[index];
            currentData.Source = gameObject.AddComponent<AudioSource>();
            currentData.Source.clip = currentData.Clip;
            currentData.Source.volume = currentData.Volume;
        }
    }
    public void PlaySound(string name)
    {
        AudioData audioData = FindAudioData(name);
        audioData.Source.Play();
        Debug.Log("Played " + name);
    }
    private AudioData FindAudioData(string name)
    {
        foreach (AudioData audioData in _audioLibrary)
        {
            if (audioData.Name == name)
                return audioData;
        }
        Debug.LogError("Audio clip " + name + " no found in AudioManager audio library.");
        return null;
    }
}

