using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 5;
    public int currentHP;

    public float invincibilityDuration = 0.8f;
    private bool isInvincible = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible) return;

        currentHP -= damageAmount;

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        float flashInterval = 0.08f;
        float startTime = Time.time;

        while (Time.time < startTime + invincibilityDuration)
        {
            // �_�ŁiRenderer���I���I�t����j
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
        }

        // �R���[�`���I����A���̏�Ԃɖ߂�
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    void Die()
    {
        GameManager.Instance.GameOver();

        // �j��G�t�F�N�g�̍Đ��A�X�R�A���Z�Ȃǂ̏����������ɓ����

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            TakeDamage(bullet.damage);
        }

        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
