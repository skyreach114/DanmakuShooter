using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void OnPressCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("CharacterSelectScene");
        }
    }
}