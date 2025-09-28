using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static BGMManager bgmManager;
    public EnemySpawner enemySpawner;

    public int enemiesDefeated = 0;
    public int enemiesToLevelUp = 4;
    public int enemiesToClear = 10;

    public CharacterData[] characters;
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    private PlayerShooting playerShooting;

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
        playerShooting = playerObj.GetComponent<PlayerShooting>();

        pc.SetupFromData(data);

        if (speedButton != null && pm != null)
        {
            speedButton.playerMove = pm;
        }

        Invoke("EraseGameStartText", 1f);
    }

    void EraseGameStartText()
    {
        if(enemySpawner != null)
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

        enemiesDefeated++;

        if (enemiesDefeated == enemiesToLevelUp)
        {
            LevelUp();
        }
        else if (enemiesDefeated >= enemiesToClear)
        {
            GameClear();
        }
    }

    void LevelUp()
    {
        Debug.Log("レベルアップ！攻撃力アップ！");

        levelUpText.gameObject.SetActive(true);
        levelUpText.color = new Color32(255, 255, 255, 255);
        StartCoroutine("FadeOut");
        Instantiate(clearEffectPrefab, Vector3.zero, Quaternion.identity);
        playerShooting.EnableSideGuns();
    }

    IEnumerator FadeOut()
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

        //clearSound.Play(); ;

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