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
            Destroy(gameObject);
        }
    }
}
