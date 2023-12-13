using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    Color deactivatedColor, activatedColor, currentColor;
    bool isDestroying, isPointed;
    float timer, time;
    List<float> times;
    int code;
    public GameObject ground, grass, wood, leaf, stone, bedrock;
    GameObject item;
    List<GameObject> items;

    // Start is called before the first frame update
    void Start()
    {
        deactivatedColor = GetComponent<Renderer>().material.GetColor("_Color");
        activatedColor = Color.Lerp(deactivatedColor, Color.black, 0.25f);
        currentColor = deactivatedColor;
        isDestroying = false;
        isPointed = false;
        timer = 0f;
        times = new List<float> { 1f, 1f, 2f, 0.5f, 4f, 60f };
        items = new List<GameObject> { ground, grass, wood, leaf, stone, bedrock };

        // Initialize Destroy Time
        code = int.Parse(gameObject.name.Substring(0, 3));
        time = times[code];
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

        GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(isPointed ? activatedColor : deactivatedColor, Color.black, timer / time));
    }

    public void startPoint()
    {
        isPointed = true;
    }

    public void endPoint()
    {
        isPointed = false;
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
        item = Instantiate(items[code]);
        item.transform.position = transform.position;

        Destroy(gameObject);
    }
}
