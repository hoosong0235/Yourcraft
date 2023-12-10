using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform tfPlayer;
    float sen, verRot;

    // Start is called before the first frame update
    void Start()
    {
        tfPlayer = GameObject.Find("Player").transform;
        sen = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        moveCamera();
        rotateCamera();
    }

    void moveCamera()
    {
        transform.position = tfPlayer.position + new Vector3(0f, 0.5f, 0f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, tfPlayer.rotation.eulerAngles.y, 0f);
    }

    void rotateCamera()
    {
        verRot = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.left * verRot * sen * Time.deltaTime);
    }
}
