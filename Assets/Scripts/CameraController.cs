using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform tfPlayer;
    float sensitivity, yRotation;

    // Start is called before the first frame update
    void Start()
    {
        tfPlayer = GameObject.Find("Player").transform;
        sensitivity = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = tfPlayer.position + new Vector3(0f, 0.5f, 0f);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, tfPlayer.rotation.eulerAngles.y, 0f);

        // Camera Rotation
        yRotation = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.left * yRotation * sensitivity * Time.deltaTime);
    }
}
