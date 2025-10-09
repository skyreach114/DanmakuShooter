using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // ìGÇ…ÇÊÇ¡ÇƒïœçX
    protected int currentHealth;
    protected float flashDuration = 0.15f;

    public Sprite damageSprite;
    protected Sprite defaltSprite;
    protected SpriteRenderer spriteRenderer;

    public GameObject enemyDieEffectPrefab;

    public AudioSource enemyDamageSound;
    public AudioSource enemyDieSound;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaltSprite = spriteRenderer.sprite;
    }

    public virtual void TakeDamage(int damageAmount)
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
        AudioSource.PlayClipAtPoint(enemyDamageSound.clip, transform.position, 0.1f);

        spriteRenderer.sprite = damageSprite;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.sprite = defaltSprite;
    }

    public virtual void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EnemyDefeated();
        }

        Instantiate(enemyDieEffectPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(enemyDieSound.clip, transform.position, 0.3f);
        ExecuteDestruction();
    }

    void ExecuteDestruction()
    {
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
