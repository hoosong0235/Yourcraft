using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    Color originColor;
    bool isDestroying;
    float timer, time;
    String code;
    CanvasController canvasController;

    // Start is called before the first frame update
    void Start()
    {
        originColor = GetComponent<Renderer>().material.GetColor("_Color");
        isDestroying = false;
        timer = 0f;
        canvasController = GameObject.Find("CanvasController").GetComponent<CanvasController>();

        // Initialize Destroy Time
        code = gameObject.name.Substring(0, 3);
        switch (code)
        {
            case "000": // Ground
                time = 1f;
                break;
            case "001": // Grass
                time = 1f;
                break;
            case "002": // Wood
                time = 2f;
                break;
            case "003": // Leaf
                time = 0.5f;
                break;
            case "004": // Stone
                time = 4f;
                break;
            case "005": // Bedrock
                time = 60f;
                break;
            default: // Unknown
                time = 60f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroying)
        {
            timer += Time.deltaTime;
            if (timer > time) destroy();
        }
        else
        {
            timer = 0f;
        }

        GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(originColor, Color.black, timer / time));
    }

    public void startDestroy()
    {
        isDestroying = true;
    }

    public void endDestroy()
    {
        isDestroying = false;
    }

    void destroy()
    {
        canvasController.addBlockNums(int.Parse(code));
        Destroy(gameObject);
    }
}
