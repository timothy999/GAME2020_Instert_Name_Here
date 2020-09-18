using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float Speed = 5.0f;
    public float LifeTime = 3.0f;
    public int damage = 25;

    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        transform.position +=
            transform.forward * Speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // TODO: Let player die and respawn

        Destroy(gameObject);
    }
}