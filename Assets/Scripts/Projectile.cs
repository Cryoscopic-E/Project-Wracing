using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    public GameObject currentTarget; 
    float distance = 200;
    float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach(GameObject player in gameManager.players)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = player.transform.position - transform.position;

            if (Vector3.Dot(forward, toOther) > 0 && Vector3.Distance(transform.position, player.transform.position) < distance)
            {
                currentTarget = player;
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterStats>().Stun(3);
            Destroy(gameObject);
        }
    }
}
