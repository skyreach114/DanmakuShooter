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

    public int enemiesDefeated = 0;
    public int enemiesToClear = 15;

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

    public bool isGameActive = false;
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
            // UI‚Ì‰ŠúÝ’è‚ÍPlayerExperience‚Ås‚¤
        }

        if (speedButton != null && pm != null)
        {
            speedButton.playerMove = pm;
        }

        if (hpIconUI != null && ph != null)
        {
            hpIconUI.InitializeHP(ph);
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

        if (enemiesDefeated >= enemiesToClear)
        {
            GameClear();
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

    private void GameClear()
    {
        isGameActive = false;
        isGameOver = true;

        gameClearText.gameObject.SetActive(true);
        Instantiate(clearEffectPrefab, Vector3.zero, Quaternion.identity);
        retryButton.SetActive(true);
        titleButton.SetActive(true);

        //clearSound.Play();

        if (enemySpawner != null)
        {
            enemySpawner.DestroyAllEnemies();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        isGameOver = true;

        gameOverText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        titleButton.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToTitle()
    {
        BGMManager bgmManager = BGMManager.GetInstance();

        if (bgmManager != null)
        {
            bgmManager.SwitchToTitleBGM();
        }

        SceneManager.LoadScene("TitleScene");
    }
}