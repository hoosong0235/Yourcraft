using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed, jumpSpeed;
    float sensitivity, xRotation, xMovement, zMovement;
    Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 2f;
        jumpSpeed = 256f;
        sensitivity = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        // Character Rotation
        xRotation = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * xRotation * sensitivity * Time.deltaTime);

        // Character Movement
        xMovement = Input.GetAxis("Horizontal");
        zMovement = Input.GetAxis("Vertical");
        movement = Vector3.right * xMovement + Vector3.forward * zMovement;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Character Jump
        if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector3.up * rb.mass * jumpSpeed);
    }
}
