using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 0.6f;
    [Range(0.5f, 1.5f)] public float pitch = 1f;
    [Range(0f, 0.5f)] public float volumeRandomness = 0.05f;
    [Range(0f, 0.5f)] public float pitchRandomness = 0.05f;
    AudioSource source;
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }
    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-volumeRandomness, volumeRandomness));
        source.pitch = pitch * (1 + Random.Range(-pitchRandomness, pitchRandomness));
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    [SerializeField] Sound[] sounds;
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.parent = transform;
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager: Sound was not found in the list: " + _name);
    }
}
