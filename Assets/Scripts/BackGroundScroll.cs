using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    private float speed = 1.5f;

    void Update()
    {
        transform.position -= new Vector3(0, Time.deltaTime * speed);

        if (transform.position.y <= -14.4)
        {
            transform.position = new Vector3(0, 14.3f);
        }
    }
}
