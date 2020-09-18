using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowScript : MonoBehaviour
{

    private bool growing = false;
    private float maxGrowSize = 25f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (growing && transform.localScale.x < maxGrowSize)
        {
            transform.localScale *= 1.1f;
        }
    }

    public void Grow()
    {
        growing = true;
    }
}
