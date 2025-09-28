using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // 敵によって変更
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

        // 破壊エフェクトの再生、スコア加算などの処理をここに入れる

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
