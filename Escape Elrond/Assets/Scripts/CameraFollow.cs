using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 t = transform.position;

        // Case 1
        if(playerTransform.position.x <= 2.6f && playerTransform.position.x >= -2.55f)
        {
            t.x = playerTransform.position.x;
            
        } 

        if(playerTransform.position.y >= -0.75f && playerTransform.position.y <= 25.5f)
        {
            t.y = playerTransform.position.y;
        }

        // Case 2

        if (playerTransform.position.x <= 20.4f && playerTransform.position.x >= -2.55f && playerTransform.position.y >= 21.3)
        {
            t.x = playerTransform.position.x;

        }

        transform.position = t;

    }


}
