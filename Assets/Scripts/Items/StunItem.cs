using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunItem : Item
{
    public override void Activate()
    {
        GameObject temp = Instantiate(projectilePrefab, transform.position + transform.forward * 20, Quaternion.identity);
        temp.transform.forward = transform.forward;
        temp.transform.parent = null;
        base.Activate();
    }
}
