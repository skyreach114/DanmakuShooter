using UnityEngine;

public class EnemyMover_Static : MonoBehaviour
{
    public float speed = 2f;
    public float stopYPosition = 4f;
    public float maxAngleDeviation = 15f;

    private Vector3 moveDirection;

    void Start()
    {
        float randomAngle = Random.Range(-maxAngleDeviation, maxAngleDeviation);
        float angleInDegrees = 270f + randomAngle;

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        moveDirection = new Vector3(
            Mathf.Cos(angleInRadians), // X¬•ª
            Mathf.Sin(angleInRadians), // Y¬•ª
            0
        ).normalized; // ”O‚Ì‚½‚ß³‹K‰»
    }

    void Update()
    {
        if (transform.position.y > stopYPosition)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        }
        else
        {
            enabled = false;
        }
    }
}