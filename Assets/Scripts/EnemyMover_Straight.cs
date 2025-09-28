using UnityEngine;

public class EnemyMover_Straight : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 8f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
