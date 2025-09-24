using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedButton : MonoBehaviour
{
    public Player playerScript;

    public Sprite lowSpeedSprite;
    public Sprite normalSpeedSprite;

    public Image buttonImage;

    void Start()
    {
        buttonImage.sprite = lowSpeedSprite;
    }

    public void ToggleSpeed()
    {
        playerScript.isLowSpeed = !playerScript.isLowSpeed;

        if (playerScript.isLowSpeed)
        {
            buttonImage.sprite = normalSpeedSprite;
            playerScript.SetSpeed(playerScript.lowSpeed);
        }
        else
        {
            buttonImage.sprite = lowSpeedSprite;
            playerScript.SetSpeed(playerScript.normalSpeed);
        }
    }
}
