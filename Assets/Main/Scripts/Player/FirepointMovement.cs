using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepointMovement : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float radius = 2f, angularspeed = 2f;

    private float positionX, positionY, angle = 0f;

    private void FixedUpdate()
    {
        positionX = (float)(center.position.x + Math.Cos(angle) * radius);
        positionY = (float)(center.position.y + Math.Sin(angle) * radius);
        transform.position = new Vector2(positionX, positionY);

        angle += angularspeed * Time.fixedDeltaTime;

        if (angle >= 360)
        {
            angle = 0;
        }
    }
}
