using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance = null;

    private AudioSource audioSource;

    public BGMSetting titleAndSelectBGM; // Inspector��Clip��Volume��ݒ�
    public BGMSetting gameSceneBGM;
    public BGMSetting bossBGM; // GameManager����ړ������邩�AGameManager���Œ��ڊǗ�
    public BGMSetting clearBGM;

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

        audioSource.clip = titleAndSelectBGM.clip;
        audioSource.volume = titleAndSelectBGM.volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    [System.Serializable] // Unity��Inspector�ɕ\�����邽�߂ɕK�v
    public class BGMSetting
    {
        public AudioClip clip;
        [Range(0.0f, 1.0f)] // Inspector�ŃX���C�_�[�\��
        public float volume = 1.0f;
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void StopAndSwitchBGM(BGMSetting newBGM)
    {
        audioSource.Stop();

        audioSource.clip = newBGM.clip;
        audioSource.volume = newBGM.volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SwitchToTitleBGM()
    {
        if (audioSource.clip != titleAndSelectBGM.clip)
        {
            StopAndSwitchBGM(titleAndSelectBGM);
        }
    }

    public void SwitchToGameSceneBGM()
    {
        if (audioSource.clip != gameSceneBGM.clip)
        {
            StopAndSwitchBGM(gameSceneBGM);
        }
    }

    public static BGMManager GetInstance()
    {
        return instance;
    }
}