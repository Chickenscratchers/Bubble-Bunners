using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float minXPosition;
    public float maxXPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        float xPosition = player.transform.position.x;
        if (xPosition < minXPosition)
        {
            xPosition = minXPosition;
        }
        else if (xPosition > maxXPosition)
        {
            xPosition = maxXPosition;
        }

        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
}
