using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 respawnPosition;
    public float minimumYPos = -5f;

    private Rigidbody rb;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();

        if(respawnPosition == null)
        {
            respawnPosition = playerTransform.position;
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (playerTransform.position.y < minimumYPos)
        {
            RespawnPlayer();
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        }
    }

    void RespawnPlayer()
    {
        playerTransform.position = respawnPosition;
        rb.velocity = Vector3.zero;
    }
}