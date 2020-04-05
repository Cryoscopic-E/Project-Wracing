using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    public GameObject currentTarget;
    public float lifetime = 5;
    float distance = 200;
    float speed = 200;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rend = transform.GetChild(0).GetComponent<Renderer>();
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
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        float scaleX = Mathf.Cos(Time.time) * 0.5f + 1;
        float scaleY = Mathf.Sin(Time.time) * 0.5f + 1;
        rend.material.mainTextureScale = new Vector2(scaleX, scaleY);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
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
