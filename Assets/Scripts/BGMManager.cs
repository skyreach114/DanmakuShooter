using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance = null;

    private AudioSource audioSource;
    public AudioClip titleAndSelectBGM;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = titleAndSelectBGM;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopAndSwitchBGM(AudioClip gameBGM)
    {
        audioSource.Stop();

        audioSource.clip = gameBGM;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SwitchToTitleBGM()
    {
        if (audioSource.clip != titleAndSelectBGM)
        {
            audioSource.Stop();
            audioSource.clip = titleAndSelectBGM;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public static BGMManager GetInstance()
    {
        return instance;
    }
}