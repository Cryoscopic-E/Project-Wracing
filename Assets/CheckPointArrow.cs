using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointArrow : MonoBehaviour
{

    public Transform checkPointArrow;
    public CheckPointTrigger checkPointTrigger;
    float speed = 5.0f;
    // Update is called once per frame
    void Update()
    {
        Vector3 checkPointPosition = checkPointTrigger.GetCurrentTargetCheckPoint().transform.position;
    
        Vector3 targetDirection = checkPointPosition - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(checkPointArrow.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        checkPointArrow.rotation = Quaternion.LookRotation(newDirection);

    }
}
