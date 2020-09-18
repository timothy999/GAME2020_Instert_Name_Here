using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{

    bool hasSeed = false;

    Vector3 spawnPosition;

    private GameObject seed;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (hasSeed)
        {
            Vector3 seedPos = transform.position;
            seedPos.y += 2;
            seed.transform.position = seedPos;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        if (other.gameObject.CompareTag("Bullet"))
        {
            hasSeed = false;
            transform.position = spawnPosition;
        }
        else if (other.gameObject.CompareTag("Seed"))
        {
            seed = other.gameObject;
            hasSeed = true;
            Debug.Log("PickUp");

        }
        else if (other.gameObject.CompareTag("Planter") && hasSeed)
        {
            hasSeed = false;
            other.gameObject.GetComponent<GrowScript>().Grow();
            Destroy(seed);
        }
    }
}
