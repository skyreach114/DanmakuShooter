using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // �G�ɓ����������̏����Ȃǂ������ɏ������Ƃ��ł���ˁi��: OnTriggerEnter2D�j
}
