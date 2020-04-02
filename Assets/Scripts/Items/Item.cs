using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool oneShot = true;
    protected float duration = 3.0f;
    private float lifeTime = 10f;
    bool destroy = false;
    bool activated = false;
    public GameObject projectilePrefab;

    public void RandomItemSelector(GameObject player, float chanceForBoost = 5)
    {
        float selector = Random.Range(0, 10);
        if(selector < chanceForBoost)
        {
           player.GetComponent<CharacterStats>().currentItem =  player.AddComponent<BoostItem>();
            player.GetComponent<CharacterStats>().boostImage.SetActive(true);
        }
        else
        {
            player.GetComponent<CharacterStats>().currentItem = player.AddComponent<StunItem>();
            player.GetComponent<CharacterStats>().currentItem.projectilePrefab = projectilePrefab;
            player.GetComponent<CharacterStats>().attackImage.SetActive(true);
        }
        Destroy(gameObject);
    }

    public virtual void Activate()
    {
        activated = true;
        duration = 0;
        StartCoroutine(StartLifeCountDown());
    }

    IEnumerator StartLifeCountDown()
    {
        yield return new WaitForSeconds(lifeTime);
        destroy = true;
    }
    void Update()
    {
        if(activated && duration <= 0 || destroy)
        {
            Destroy(GetComponent<Item>());
        }
    }
}
