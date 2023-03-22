using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject playerRef;
    Rigidbody2D rb;
    Vector3 refVelocity = Vector3.zero;
    float smoothTime = 0.2f;
    int offsetB = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = playerRef.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

/*        if (rb.velocity.x > 3f)
        {
            offsetB = 4;
        }
        else
        {
            offsetB = 0;
        }

        if (rb.velocity.x < -3f)
        {
            offsetB = -4;
        }
        if (rb.velocity.x > 15f)
        {
            offsetB = 7;
        }
        if (rb.velocity.x < -15f)
        {
            offsetB = -7;
        }

        Debug.Log(rb.velocity.x);*/



        Vector3 targetPosition = new Vector3(playerRef.transform.position.x + offsetB, playerRef.transform.position.y, -2);
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition, ref refVelocity, 0.3f);
    }
}
