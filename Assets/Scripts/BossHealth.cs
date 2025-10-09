using System.Collections;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public GameObject explosionEffectPrefab;
    public AudioSource explosionSound;

    private int explosionCount = 8;
    private float delayBetweenExplosions = 0.2f;

    public override void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(FlashCoroutine());

        if (currentHealth <= 0)
        {
            GameManager.Instance.isGameActive = false;

            BossShooter shooter = GetComponent<BossShooter>();
            if (shooter != null)
            {
                shooter.StopShooting();
            }

            StartCoroutine(ExplosionSequenceRoutine());
        }
    }

    IEnumerator FlashCoroutine()
    {
        AudioSource.PlayClipAtPoint(enemyDamageSound.clip, transform.position, 0.1f);

        spriteRenderer.sprite = damageSprite;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.sprite = defaltSprite;
    }

    IEnumerator ExplosionSequenceRoutine()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }

            if (explosionEffectPrefab != null)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-1.6f, 1.6f),
                    Random.Range(-1.6f, 1.6f),
                    0
                );
                Instantiate(explosionEffectPrefab, transform.position + randomOffset, Quaternion.identity);
            }
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound.clip, transform.position, 0.35f);
            }

            yield return new WaitForSeconds(delayBetweenExplosions);
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        Destroy(gameObject);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameClear();
        }
    }
}