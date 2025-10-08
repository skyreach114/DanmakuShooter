using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 6.5f;
    public float lowSpeed = 3.5f;
    public float currentSpeed;
    public float touchSpeedMultiplier = 1.2f;

    private Vector2 moveInput;
    private Vector3 offset;
    private Vector3 targetPosition;

    private bool isDragging = false;
    public bool isLowSpeed = false;

    void Update()
    {
        if (isLowSpeed || Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = lowSpeed;
            Debug.Log("now currentSpeed" + currentSpeed);
        }
        else
        {
            currentSpeed = normalSpeed;
        }
        

        bool isTouchOrClick = (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed) ||
                              (Mouse.current != null && Mouse.current.leftButton.isPressed);
        if (isTouchOrClick)
        {
            Vector2 screenPos = Touchscreen.current != null ?
                                Touchscreen.current.primaryTouch.position.ReadValue() :
                                Mouse.current.position.ReadValue();

            Vector3 currentWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));
            currentWorldPos.z = 0;

            if (!isDragging)
            {
                offset = transform.position - currentWorldPos;
                isDragging = true;
            }

            float finalSpeed = currentSpeed * touchSpeedMultiplier;
            targetPosition = currentWorldPos + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, finalSpeed * Time.deltaTime);
        }
        else
        {
            isDragging = false;

            Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
        }

        // ï«ÇÃì‡ë§Ç≈êßå¿
        float clampedX = Mathf.Clamp(transform.position.x, -3.5f, 3.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -6.7f, 6.7f);
        transform.position = new Vector3(clampedX, clampedY, 0);
    }
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }
}
