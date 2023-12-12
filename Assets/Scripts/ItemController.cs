using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    CanvasController canvasController;
    Transform tfPlayer;
    int code;
    bool isFalling;
    float maxDistanceMagnet;
    float distance;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();
        tfPlayer = GameObject.Find("Player").transform;
        code = int.Parse(gameObject.name.Substring(0, 3));
        isFalling = true;
        maxDistanceMagnet = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 10f, 0f));

        if (!isFalling)
        {
            distance = Vector3.Distance(tfPlayer.position, transform.position);
            if (distance < maxDistanceMagnet)
            {
                movement = (tfPlayer.position - transform.position) / (5 * distance);
                transform.position += movement;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvasController.addBlockNums(code);
            Destroy(gameObject);
        }
        else
        {
            isFalling = false;
        }
    }
}
