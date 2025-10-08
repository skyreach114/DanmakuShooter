using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NumberOfPlayerSelectManager : MonoBehaviour
{
    public Button[] playerCountButtons; // 1~4�l�{�^��
    public Button characterSelectButton;

    private Color selectedColor = Color.white;
    private Color normalColor = new(0.7f, 0.7f, 0.7f, 0.6f); // �������O���[

    private int selectedCount = 0;
    private NetworkLauncher launcher;

    void Start()
    {
        launcher = FindFirstObjectByType<NetworkLauncher>();
        characterSelectButton.interactable = false;

        for (int i = 0; i < playerCountButtons.Length; i++)
        {
            int count = i + 1; // �{�^��1��1�l�A�{�^��2��2�l�c
            playerCountButtons[i].onClick.AddListener(() => OnSelectCount(count));
            SetButtonColor(playerCountButtons[i], normalColor);
        }
    }

    private void OnSelectCount(int count)
    {
        selectedCount = count;

        if (launcher != null)
        {
            launcher.SetPlayerCount(count);
        }

        characterSelectButton.interactable = true;
        UpdateButtonColors();

        Debug.Log($"�v���C�l�� {count} �l���I������܂���");
    }

    private void UpdateButtonColors()
    {
        for (int i = 0; i < playerCountButtons.Length; i++)
        {
            var btn = playerCountButtons[i];
            SetButtonColor(btn, (i + 1 == selectedCount) ? selectedColor : normalColor);
        }
    }

    private void SetButtonColor(Button btn, Color color)
    {
        var img = btn.GetComponent<Image>();
        if (img != null)
        {
            img.color = color;
        }
    }

    public void OnPressCharacterSelect()
    {
        launcher.GoToCharacterSelect();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            launcher.GoToCharacterSelect();
        }
    }
}
