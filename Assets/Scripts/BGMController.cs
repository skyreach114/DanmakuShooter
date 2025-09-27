using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    private static BGMController instance = null;

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

    public static BGMController GetInstance()
    {
        return instance;
    }
}