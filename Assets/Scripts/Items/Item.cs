using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool oneShot = true;
    protected float duration = 3.0f;
    bool activated = false;

    public void RandomItemSelector(GameObject player, float chanceForBoost = 5)
    {
        float selector = Random.Range(0, 10);
        if(selector < chanceForBoost)
        {
           player.GetComponent<CharacterStats>().currentItem =  player.AddComponent<BoostItem>();
        }
        else
        {
            player.GetComponent<CharacterStats>().currentItem = player.AddComponent<StunItem>();
        }
        Destroy(gameObject);
    }

    public virtual void Activate()
    {
        activated = true;
        duration = 0;
    }

    void Update()
    {
        if(activated && duration <= 0)
        {
            Destroy(GetComponent<Item>());
        }
    }
}
