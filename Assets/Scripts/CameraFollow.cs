using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform desiredPosition;
    public Transform lookAtPosition;

    void Awake()
    {
        transform.parent = null;
        if (GameObject.Find("Cameras").transform)
        {
            transform.parent = GameObject.Find("Cameras").transform;
        }
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition.position, Time.deltaTime * 10);
        transform.LookAt(lookAtPosition);
    }
}
