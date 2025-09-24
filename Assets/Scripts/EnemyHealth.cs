using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // 敵によって変更
    private int currentHealth;

    public Sprite damageSprite;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
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
