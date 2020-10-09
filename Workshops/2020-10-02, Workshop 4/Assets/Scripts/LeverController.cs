using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public LeverManager leverData;
    public GameObject model;

    private Material modelMaterial;
    private Transform modelTransform;
    private bool hasBeenTriggered = false;

    void Start()
    {
        modelTransform = model.GetComponent<Transform>();
        modelMaterial = model.GetComponent<Renderer>().material;
    }

    void TriggerLever()
    {
        hasBeenTriggered = true;
        leverData.numberOfLeversOn--;
        modelMaterial.DisableKeyword("_EMISSION");
        modelTransform.Rotate(new Vector3(0f, 0f, 180f));
        Debug.Log("Lever triggered");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!hasBeenTriggered && other.CompareTag("Player"))
        {
            TriggerLever();
        }
    }
}
