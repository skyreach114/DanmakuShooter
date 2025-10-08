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
        Debug.Log($"�L���� {characters[index].displayName} ��I�����܂���");
    }

    public void OnPressStartGame()
    {
        if (selectedIndex < 0) return;

        BGMManager bgmManager = BGMManager.GetInstance();

        if (bgmManager != null)
        {
            // 2. BGM���~���A�Q�[��BGM�ɐ؂�ւ��鏈�����Ăяo��
            bgmManager.StopAndSwitchBGM(gameBGMClip);
        }

        SceneManager.LoadScene("GameScene");
    }
}