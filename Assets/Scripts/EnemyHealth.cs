using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // �G�ɂ���ĕύX
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
