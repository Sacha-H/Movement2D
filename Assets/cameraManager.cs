using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject playerRef;
    Vector3 ref_Velocity = Vector3.zero;
    float smoothTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       Vector3 targetPosition = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y, transform.position.z);
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition, ref ref_Velocity, smoothTime);
    }
}
