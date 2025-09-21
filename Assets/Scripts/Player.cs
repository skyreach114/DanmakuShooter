using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float normalSpeed = 6f;
    public float lowSpeed = 3f;
    public float currentSpeed;

    private Vector2 moveInput;
    private Vector3 offset;

    private bool isDragging = false;

    void Update()
    {

        bool isTouchOrClick = (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed) ||
                              (Mouse.current != null && Mouse.current.leftButton.isPressed);
        if (isTouchOrClick)
        {
            Vector2 screenPos = Touchscreen.current != null ?
                                Touchscreen.current.primaryTouch.position.ReadValue() :
                                Mouse.current.position.ReadValue();

            Vector3 currentWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane));
            currentWorldPos.z = 0; // 2D�Ȃ̂�Z��0�ɌŒ�

            if (!isDragging)
            {
                // **�y�d�v�z�I�t�Z�b�g���v�Z���ċL��**
                // �v���C���[�̌��݈ʒu����^�b�`�ʒu�����������̂��I�t�Z�b�g�ɂȂ�
                offset = transform.position - currentWorldPos;
                isDragging = true;
            }

            // 3. **�h���b�O���̏���**
            // ���݂̃^�b�`�ʒu�ɁA�L�������I�t�Z�b�g�𑫂����ʒu�փv���C���[���ړ�������
            Vector3 targetPosition = currentWorldPos + offset;

            // �ړ������炩�ɂ���Ȃ�Lerp�Ȃǂ��g�����ǁA����̓V���v���ɒ��ڑ��
            transform.position = targetPosition;
        }
        else
        {
            isDragging = false;

            currentSpeed = Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed
            ? lowSpeed : normalSpeed;
            Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
        }

        // �ǂ̓����Ő���
        float clampedX = Mathf.Clamp(transform.position.x, -3.5f, 3.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -6.7f, 6.7f);
        transform.position = new Vector3(clampedX, clampedY, 0);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
