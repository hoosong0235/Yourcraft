using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ground, grass, wood, leaf, stone, bedrock;
    Rigidbody rb;
    Transform tfCamera;
    float sen, horRot;
    float horMov, verMov, movSpeed;
    Vector3 movement;
    bool isJumping;
    float jumpSpeed;
    float maxDistanceDestroy, maxDistancePlace;
    RaycastHit hitInfoDestroy, hitInfoPlace;
    GameObject goDestroy, goPlace;
    Vector3 hitOrigin, hitPoint;
    CanvasController canvasController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        isJumping = true;
        rb = GetComponent<Rigidbody>();
        tfCamera = GameObject.Find("Main Camera").transform;
        movSpeed = 2f;
        jumpSpeed = 256f;
        sen = 100f;
        maxDistanceDestroy = 5f;
        maxDistancePlace = 5f;
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCharacter();
        rotateCharacter();
        jumpCharacter();

        selectBlock();
        destroyBlock();
        placeBlock();
    }

    void moveCharacter()
    {
        horMov = Input.GetAxis("Horizontal");
        verMov = Input.GetAxis("Vertical");
        movement = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * (Vector3.right * horMov + Vector3.forward * verMov);
        rb.MovePosition(transform.position + movement * movSpeed * Time.deltaTime);
    }

    void rotateCharacter()
    {
        horRot = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * horRot * sen * Time.deltaTime);
    }

    void jumpCharacter()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rb.AddForce(Vector3.up * rb.mass * jumpSpeed);
                isJumping = true;
            }
        }
    }

    void selectBlock()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            canvasController.addIndex();
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            canvasController.subIndex();
        }
    }

    void destroyBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceDestroy))
            {
                try
                {
                    goDestroy = hitInfoDestroy.collider.gameObject;
                    goDestroy.GetComponent<BlockController>().startDestroy();
                }
                catch
                {
                    print("[ERROR 1-1] startDestroy with null goDestroy");
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceDestroy))
            {
                if (goDestroy != hitInfoDestroy.collider.gameObject)
                {
                    try
                    {
                        if (goDestroy != null) goDestroy.GetComponent<BlockController>().endDestroy();
                    }
                    catch
                    {
                        print("[ERROR 3-1] endDestroy with null nonnull goDestroy");
                    }

                    try
                    {
                        goDestroy = hitInfoDestroy.collider.gameObject;
                        goDestroy.GetComponent<BlockController>().startDestroy();
                    }
                    catch
                    {
                        print("[ERROR 1-2] startDestroy with null goDestroy");
                    }
                }
            }
            else
            {
                try
                {
                    if (goDestroy != null) goDestroy.GetComponent<BlockController>().endDestroy();
                }
                catch
                {
                    print("[ERROR 3-2] endDestroy with null nonnull goDestroy");
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceDestroy))
            {
                try
                {
                    goDestroy.GetComponent<BlockController>().endDestroy();
                }
                catch
                {
                    print("[ERROR 2-1] endDestroy wiht null goDestroy");
                }
            }
        }
    }

    void placeBlock()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoPlace, maxDistancePlace))
            {
                hitOrigin = hitInfoPlace.collider.gameObject.transform.position;
                hitPoint = hitInfoPlace.point;

                goPlace = Instantiate(grass);
                goPlace.transform.position = new Vector3(
                    hitOrigin.x - 0.5f == hitPoint.x ? hitOrigin.x - 1f : (hitOrigin.x + 0.5f == hitPoint.x ? hitOrigin.x + 1f : hitOrigin.x),
                    hitOrigin.y - 0.5f == hitPoint.y ? hitOrigin.y - 1f : (hitOrigin.y + 0.5f == hitPoint.y ? hitOrigin.y + 1f : hitOrigin.y),
                    hitOrigin.z - 0.5f == hitPoint.z ? hitOrigin.z - 1f : (hitOrigin.z + 0.5f == hitPoint.z ? hitOrigin.z + 1f : hitOrigin.z)
                );
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isJumping = false;
    }
}
