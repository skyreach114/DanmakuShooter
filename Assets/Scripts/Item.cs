using UnityEngine;

// アイテムの種類を定義
public enum ItemType { HP_RECOVER, EXP_BOOST }

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public float dropSpeed = 2.5f; // アイテムの降下速度
    public float lifeTime = 10f;

    private PlayerHealth playerHealth;

    void Start()
    {
        // プレイヤーのコンポーネントを初期化時に取得しておく
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // プレイヤーをTagで探す
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