using Fusion;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EnemySpawner enemySpawner;
    public BossSpawner bossSpawner;

    private int enemiesDefeated = 0;
    private int bossSpawnCount = 10;

    public CharacterData[] characters;
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    public PlayerExperience playerExperience;
    public HPIcon hpIconUI;
    public PlayerSpeedButton speedButton;
    public TextMeshProUGUI levelUpText;
    public TextMeshProUGUI gameStartText;
    public TextMeshProUGUI gameClearText;
    public TextMeshProUGUI gameOverText;
    public GameObject retryButton;
    public GameObject titleButton;
    public GameObject clearEffectPrefab;

    public AudioSource bossSpawnSound;
    public AudioSource gameOverSound;
    public AudioSource clearSound;

    public bool isGameActive = false;
    public bool isBossSpawn = false;
    public bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        levelUpText.gameObject.SetActive(false);
        gameClearText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.SetActive(false);
        titleButton.SetActive(false);

        int idx = PlayerPrefs.GetInt("SelectedCharacter", 0);
        idx = Mathf.Clamp(idx, 0, characters.Length - 1);

        var data = characters[idx];

        var playerObj = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);

        var pc = playerObj.GetComponent<PlayerController>();
        var pm = playerObj.GetComponent<PlayerMovement>();
        var ph = playerObj.GetComponent<PlayerHealth>();
        var ps = playerObj.GetComponent<PlayerShooting>();

        pc.SetupFromData(data);

        if (playerExperience != null)
        {
            playerExperience.Setup(ps);
            // UIÇÃèâä˙ê›íËÇÕPlayerExperienceÇ≈çsÇ§
        }

        if (speedButton != null && pm != null)
        {
            speedButton.playerMove = pm;
        }

        if (hpIconUI != null && ph != null)
        {
            hpIconUI.InitializeHP(ph);
        }

        if (BGMManager.instance != null)
        {
            BGMManager.instance.SwitchToGameSceneBGM();
        }

        Invoke("EraseGameStartText", 1f);
    }

    void EraseGameStartText()
    {
        if (enemySpawner != null)
        {
            enemySpawner.StartSpawning();
        }

        isGameActive = true;
        gameStartText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Retry();
        }
    }

    public void EnemyDefeated()
    {
        if (!isGameActive) return;

        if (playerExperience != null)
        {
            playerExperience.AddExp(1);
            enemiesDefeated++;
        }

        if (enemiesDefeated == bossSpawnCount)
        {
            StartCoroutine(StartBossEncounter());
        }
    }

    IEnumerator StartBossEncounter()
    {
        isBossSpawn = true;

        if (BGMManager.instance != null)
        {
            BGMManager.instance.StopBGM();
            bossSpawnSound.Play();

            if (enemySpawner != null && bossSpawner != null)
            {
                enemySpawner.DestroyAllEnemies();
                bossSpawner.StartSpawning();
            }

            if (bossSpawnSound.clip != null)
            {
                yield return new WaitForSeconds(bossSpawnSound.clip.length - 0.2f);
            }

            BGMManager.instance.StopAndSwitchBGM(BGMManager.instance.bossBGM);
        }
    }

    public IEnumerator FadeOut()
    {
        while (true)
        {
            for (int i = 0; i < 255; i++)
            {
                levelUpText.color = levelUpText.color - new Color32(0, 0, 0, 1);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }

    public void GameClear()
    {
        isGameActive = false;
        isGameOver = true;

        gameClearText.gameObject.SetActive(true);
        Instantiate(clearEffectPrefab, Vector3.zero, Quaternion.identity);
        retryButton.SetActive(true);
        titleButton.SetActive(true);

        clearSound.Play();

        BGMManager.instance.StopAndSwitchBGM(BGMManager.instance.clearBGM);
    }

    public void GameOver()
    {
        isGameActive = false;
        isGameOver = true;

        gameOverText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        titleButton.SetActive(true);

        gameOverSound.Play();
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToTitle()
    {
        if (BGMManager.instance != null)
        {
            BGMManager.instance.SwitchToTitleBGM();
        }

        SceneManager.LoadScene("TitleScene");
    }
}