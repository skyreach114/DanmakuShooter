using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedButton : MonoBehaviour
{
    public PlayerMovement playerMove;

    public Sprite lowSpeedSprite;
    public Sprite normalSpeedSprite;

    public Image buttonImage;

    void Start()
    {
        buttonImage.sprite = lowSpeedSprite;
    }

    public void ToggleSpeed()
    {
        playerMove.isLowSpeed = !playerMove.isLowSpeed;

        if (playerMove.isLowSpeed)
        {
            buttonImage.sprite = normalSpeedSprite;
            playerMove.SetSpeed(playerMove.lowSpeed);
            Debug.Log(playerMove.lowSpeed);
        }
        else
        {
            buttonImage.sprite = lowSpeedSprite;
            playerMove.SetSpeed(playerMove.normalSpeed);
        }
    }
}
