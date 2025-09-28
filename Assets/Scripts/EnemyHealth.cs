using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // �G�ɂ���ĕύX
    private int currentHealth;
    float flashDuration = 0.15f;

    public Sprite damageSprite;
    private Sprite defaltSprite;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaltSprite = spriteRenderer.sprite;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        StartCoroutine(FlashCoroutine());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashCoroutine()
    {
        spriteRenderer.sprite = damageSprite;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.sprite = defaltSprite;
    }

    void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }

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
    }
}
