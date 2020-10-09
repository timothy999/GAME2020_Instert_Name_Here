using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingScript : MonoBehaviour
{

    public GameObject BulletPrefab; //Template for "ammo"

    public int maxAmmo = 3;

    public float fireForce = 10;

    public float bulletLife = 1.0f;

    int ammo; //Max 3, refill when not in the level anymore

    int ammoCycle; // to remove oldest bullets

    GameObject[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        bullets = new GameObject[maxAmmo];
        ammo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Destroy(bullets[ammo]);
            Vector3 firingPosition = transform.position;
            firingPosition.y += 1.9f;
            Vector3 firingAngle = transform.forward;
            firingAngle.y = map(GetComponent<PlayerMovementScript>().verticalLookRotation, -180f, 180f, -4f, 4f);

            bullets[ammo] = Instantiate(BulletPrefab, firingPosition, transform.rotation) as GameObject;
            bullets[ammo].GetComponent<Rigidbody>().AddForce(firingAngle* fireForce);
            Destroy(bullets[ammo], bulletLife);
            ammo++;
            if (ammo == maxAmmo) {
                ammo = 0;
            }
        }
    }

    float map(float value, float low1, float high1, float low2, float high2) {
        return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
    }
}

