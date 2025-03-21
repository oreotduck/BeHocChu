using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource menuAudio;
    public AudioSource pageAudio;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMenuAudio();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            PlayMenuAudio();
        }
        else
        {
            PlayPageAudio();
        }
    }

    public void PlayMenuAudio()
    {
        if (!menuAudio.isPlaying)
        {
            menuAudio.Play();
        }
        pageAudio.Stop();
    }

    public void PlayPageAudio()
    {
        if (!pageAudio.isPlaying)
        {
            pageAudio.Play();
        }
        menuAudio.Stop();
    }
}
