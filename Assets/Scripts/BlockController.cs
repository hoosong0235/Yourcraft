using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    Color originColor;
    bool isDestroying;
    float timer, time;

    // Start is called before the first frame update
    void Start()
    {
        originColor = GetComponent<Renderer>().material.GetColor("_Color");
        isDestroying = false;
        timer = 0f;
        time = 1f;
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
        Destroy(gameObject);
    }
}
