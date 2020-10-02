using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHydrationScript : MonoBehaviour
{

    public float maxLiquid = 100;

    public float liquid = 0;

    public float dryOutTime = 10;

    public GameObject deathScreen;

    float timeCount;

    int grabbedKey = 0;

    public GameObject pillar1;
    public GameObject pillar2;
    public GameObject pillar3;

    GameObject carry;

    bool K1 = false;
    bool K2 = false;
    bool K3 = false;

    PlayerMovementScript script;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        liquid = maxLiquid;
        script = gameObject.GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carry != null && grabbedKey != 0) {

            carry.transform.parent = transform; 
        }
        if (K1 && K2 && K3) {
            script.setWin();
        }
        timeCount += Time.deltaTime;
        if (timeCount >= dryOutTime) {
            liquid -= 10;
            Debug.Log("Drier");
            timeCount = 0;
        }

        if (liquid <= 0) {
            Debug.Log("Dead");
            deathScreen.SetActive(true);
            Invoke("respawn", 3f);    
            liquid = maxLiquid;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            liquid = maxLiquid;
            timeCount = 0;
            //Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Key1"))
        {
            if (grabbedKey == 0)
            {
                grabbedKey = 1;
                carry = other.gameObject;
            }
        }
        if (other.gameObject.CompareTag("Key2"))
        {
            if (grabbedKey == 0)
            {
                grabbedKey = 2;
                carry = other.gameObject;
            }
        }
        if (other.gameObject.CompareTag("Key3"))
        {
            if (grabbedKey == 0)
            {
                grabbedKey = 3;
                carry = other.gameObject;
            }
        }
        if (other.gameObject.CompareTag("Pillar1") && grabbedKey == 1)
        {
            grabbedKey = 0;
            InvokeRepeating("remove1", 0f, 0.1f);
            K1 = true;
        }
        if (other.gameObject.CompareTag("Pillar2") && grabbedKey == 2)
        {
            grabbedKey = 0;
            InvokeRepeating("remove2", 0f, 0.1f);
            K2 = true;
            
        }
        if (other.gameObject.CompareTag("Pillar3") && grabbedKey == 3)
        {
            grabbedKey = 0;
            InvokeRepeating("remove3", 0f, 0.1f);
            K3 = true;
            
        }
    }

    void respawn() {
        script.respawn();
        deathScreen.SetActive(false);
    }

    void remove1() {
        carry.transform.parent = pillar1.transform;
        Vector3 temp = pillar1.transform.position;
        temp.y -= 0.03f;
        pillar1.transform.position = temp;
    }

    void remove2()
    {
        carry.transform.parent = pillar2.transform;
        Vector3 temp = pillar2.transform.position;
        temp.y -= 0.03f;
        pillar2.transform.position = temp;
    }

    void remove3()
    {
        carry.transform.parent = pillar3.transform;
        Vector3 temp = pillar3.transform.position;
        temp.y -= 0.03f;
        pillar3.transform.position = temp;
    }

    public void enemyHit(float damage)
    {
        liquid -= damage;
    }
}
