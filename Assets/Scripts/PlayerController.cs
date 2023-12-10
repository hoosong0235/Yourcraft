using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Transform tfCamera;
    float sen, horRot;
    float horMov, verMov, movSpeed;
    Vector3 movement;
    float jumpSpeed;
    float maxDistance;
    RaycastHit hitInfo;
    GameObject goDestroy;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tfCamera = GameObject.Find("Main Camera").transform;
        movSpeed = 2f;
        jumpSpeed = 256f;
        sen = 100f;
        maxDistance = 5f;
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

        // Destroy Block
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfo, maxDistance))
            {
                goDestroy = hitInfo.collider.gameObject;
                goDestroy.GetComponent<BlockController>().startDestroy();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfo, maxDistance))
            {
                if (goDestroy != hitInfo.collider.gameObject)
                {
                    if (goDestroy != null) goDestroy.GetComponent<BlockController>().endDestroy();

                    goDestroy = hitInfo.collider.gameObject;
                    goDestroy.GetComponent<BlockController>().startDestroy();
                }
            }
            else
            {
                if (goDestroy != null) goDestroy.GetComponent<BlockController>().endDestroy();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfo, maxDistance))
            {
                if (goDestroy != null) goDestroy.GetComponent<BlockController>().endDestroy();
            }
        }

        // Place Block
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
}
