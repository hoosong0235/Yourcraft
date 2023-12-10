using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float sen, horRot;
    float horMov, verMov, movSpeed;
    float jumpSpeed;
    Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movSpeed = 2f;
        jumpSpeed = 256f;
        sen = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        // Character Rotation
        horRot = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * horRot * sen * Time.deltaTime);

        // Character Movement
        horMov = Input.GetAxis("Horizontal");
        verMov = Input.GetAxis("Vertical");
        movement = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * (Vector3.right * horMov + Vector3.forward * verMov);
        rb.MovePosition(transform.position + movement * movSpeed * Time.deltaTime);

        // Character Jump
        if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector3.up * rb.mass * jumpSpeed);
    }
}
