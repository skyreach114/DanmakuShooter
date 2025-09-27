using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 5;
    public int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
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

        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
