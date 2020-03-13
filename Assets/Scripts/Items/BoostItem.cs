using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : Item
{
    public override void Activate()
    {
        GetComponent<MovementController>().GetBoost(duration);
        base.Activate();
    }
}
