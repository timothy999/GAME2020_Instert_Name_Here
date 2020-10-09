using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    public float speed = 12;

    public float jumpSpeed = 10f;
    public float gravity = 9.81f;

    Transform cameraT;
    public float verticalLookRotation;

    Vector3 spawn;

    bool cursorVisible;

    public CharacterController controller;

    float vSpeed = 0;

    bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //MouseRotation
        transform.Rotate(Vector3.up * Input.GetAxis("RotationLeftRight") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("RotationUpDown") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        //Movement with gravity
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        
        if (controller.isGrounded)
        {
            vSpeed = 0; // grounded character has vSpeed = 0...
            if (Input.GetKeyDown("space"))
            { // unless it jumps:
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        if (won)
        {
            vSpeed = 3;
        }
        move.y = vSpeed;

        
        controller.Move(move * speed * Time.deltaTime);

        //lock mouse
        if (Input.GetMouseButtonUp(0))
        {
            if (!cursorVisible)
            {
                UnlockMouse();
            }
            else
            {
                LockMouse();
            }
        }
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorVisible = true;
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorVisible = false;
    }

    public void respawn() {
        controller.enabled = false;
        transform.position = spawn;
        controller.enabled = true;
        Debug.Log("respawned");
    }

    public void setWin() {
        won = true;
    }
}
