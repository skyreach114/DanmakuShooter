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

    // “G‚É“–‚½‚Á‚½‚Ìˆ—‚È‚Ç‚ğ‚±‚±‚É‘‚­‚±‚Æ‚à‚Å‚«‚é‚Ëi—á: OnTriggerEnter2Dj
}
