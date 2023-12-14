using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ground, grass, wood, leaf, stone, bedrock;
    List<GameObject> blocks;
    Rigidbody rb;
    Transform tfCamera;
    float sen, horRot;
    float horMov, verMov, movSpeed;
    Vector3 movement;
    bool isJumping;
    float jumpSpeed;
    float maxDistanceInteract;
    RaycastHit hitInfoDestroy, hitInfoPlace;
    GameObject goPoint, goDestroy, goPlace;
    Vector3 hitOrigin, hitPoint;
    CanvasController canvasController;

    // Start is called before the first frame update
    void Start()
    {
        isJumping = true;
        rb = GetComponent<Rigidbody>();
        tfCamera = GameObject.Find("Main Camera").transform;
        movSpeed = 2f;
        jumpSpeed = 256f;
        sen = 100f;
        maxDistanceInteract = 5f;
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();
        blocks = new List<GameObject> { ground, grass, wood, leaf, stone };
    }

    // Update is called once per frame
    void Update()
    {
        moveCharacter();
        rotateCharacter();
        jumpCharacter();

        selectBlock();
        pointBlock();
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
            canvasController.subIndex();
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            canvasController.addIndex();
        }
    }

    void pointBlock()
    {
        if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
        {
            if (goPoint != hitInfoDestroy.collider.gameObject)
            {
                try
                {
                    if (goPoint != null) goPoint.GetComponent<BlockController>().endPoint();
                }
                catch
                {
                    print("[ERROR 5-1] endPoint with null goPoint");
                }

                try
                {
                    goPoint = hitInfoDestroy.collider.gameObject;
                    goPoint.GetComponent<BlockController>().enabled = true;
                    goPoint.GetComponent<BlockController>().startPoint();
                }
                catch
                {
                    print("[ERROR 4-1] startPoint with null goPoint");
                }
            }
            else
            {
                try
                {
                    goPoint = hitInfoDestroy.collider.gameObject;
                    goPoint.GetComponent<BlockController>().startPoint();
                }
                catch
                {
                    print("[ERROR 4-1] startPoint with null goPoint");
                }
            }
        }
        else
        {
            try
            {
                if (goPoint != null) goPoint.GetComponent<BlockController>().endPoint();
            }
            catch
            {
                print("[ERROR 5-2] endPoint with null goPoint");
            }

            goPoint = null;
        }
    }

    void destroyBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
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
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
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
                else
                {
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

                goDestroy = null;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
            {
                try
                {
                    goDestroy.GetComponent<BlockController>().endDestroy();
                }
                catch
                {
                    print("[ERROR 2-1] endDestroy wiht null goDestroy");
                }

                goDestroy = null;
            }
        }
    }

    void placeBlock()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoPlace, maxDistanceInteract))
            {
                hitOrigin = hitInfoPlace.collider.gameObject.transform.position;
                hitPoint = hitInfoPlace.point;

                if (canvasController.getBlockNums() > 0)
                {
                    goPlace = Instantiate(blocks[canvasController.getIndex()]);
                    goPlace.transform.position = new Vector3(
                        hitOrigin.x - 0.5f == hitPoint.x ? hitOrigin.x - 1f : (hitOrigin.x + 0.5f == hitPoint.x ? hitOrigin.x + 1f : hitOrigin.x),
                        hitOrigin.y - 0.5f == hitPoint.y ? hitOrigin.y - 1f : (hitOrigin.y + 0.5f == hitPoint.y ? hitOrigin.y + 1f : hitOrigin.y),
                        hitOrigin.z - 0.5f == hitPoint.z ? hitOrigin.z - 1f : (hitOrigin.z + 0.5f == hitPoint.z ? hitOrigin.z + 1f : hitOrigin.z)
                    );

                    canvasController.subBlockNums();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isJumping = false;
    }
}
