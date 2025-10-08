using UnityEngine;

// �A�C�e���̎�ނ��`
public enum ItemType { HP_RECOVER, EXP_BOOST }

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public float dropSpeed = 2.5f; // �A�C�e���̍~�����x
    public float lifeTime = 10f;

    private PlayerHealth playerHealth;

    void Start()
    {
        // �v���C���[�̃R���|�[�l���g�����������Ɏ擾���Ă���
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // �v���C���[��Tag�ŒT��
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect();
            Destroy(gameObject);
        }

        void ApplyEffect()
        {
            if (itemType == ItemType.HP_RECOVER)
            {
                if (playerHealth != null && playerHealth.currentHP < playerHealth.maxHP)
                {
                    playerHealth.currentHP += 1;
                    GameManager.Instance.hpIconUI.ForceUpdateIcon();
                }
            }
            else if (itemType == ItemType.EXP_BOOST)
            {
                if (PlayerExperience.Instance != null)
                {
                    PlayerExperience.Instance.AddExp(2);
                }
            }
        }
    }
}