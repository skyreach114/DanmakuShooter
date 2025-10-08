using Fusion;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public CharacterData[] characters;
    public Image[] images;
    public Button startGameButton;
    private int selectedIndex = -1;

    public AudioClip gameBGMClip;

    void Start()
    {
        startGameButton.interactable = false;
    }

    public void SelectCharacter(int index)
    {
        selectedIndex = index;
        PlayerPrefs.SetInt("SelectedCharacter", selectedIndex);
        PlayerPrefs.Save();
        startGameButton.interactable = true;

        foreach (Image image in images)
        {
            image.color = new Color(1, 1, 1, 0.02f);
        }

        images[index].color = new Color(1, 1, 1, 0.2f);
        Debug.Log($"キャラ {characters[index].displayName} を選択しました");
    }

    public void OnPressStartGame()
    {
        if (selectedIndex < 0) return;

        BGMManager bgmManager = BGMManager.GetInstance();

        if (bgmManager != null)
        {
            // 2. BGMを停止し、ゲームBGMに切り替える処理を呼び出す
            bgmManager.StopAndSwitchBGM(gameBGMClip);
        }

        SceneManager.LoadScene("GameScene");
    }
}