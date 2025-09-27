using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private PlayerShooting shooting;
    private PlayerHealth health;

    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        shooting = GetComponent<PlayerShooting>();
        health = GetComponent<PlayerHealth>();
    }

    public void SetupFromData(CharacterData data)
    {
        // Animator ‚ğƒLƒƒƒ‰‚²‚Æ‚É·‚µ‘Ö‚¦‚é
        if (animator != null && data.animatorController != null)
        {
            animator.runtimeAnimatorController = data.animatorController;
        }

        // ˆÚ“®‘¬“x‚ğ“n‚·
        if (movement != null)
        {
            movement.normalSpeed = data.normalSpeed;
            movement.lowSpeed = data.lowSpeed;
        }

        // ’eƒvƒŒƒnƒu‚ğ“n‚·
        if (shooting != null)
        {
            shooting.fireRate = data.fireRate;
            shooting.bulletPrefab = data.bulletPrefab;
            shooting.bulletSidePrefab = data.bulletSidePrefab;
        }

        // HP ‚ğ“n‚·
        if (health != null)
        {
            health.maxHP = data.maxHP;
            health.currentHP = data.maxHP; // ‰Šú‰»
        }
    }
}
