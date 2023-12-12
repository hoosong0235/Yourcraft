using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneCameraController : MonoBehaviour
{
    float timer, time;
    bool isClockwise;
    Vector3 initialLocalEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        time = 60f;
        isClockwise = true;
        initialLocalEulerAngles = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClockwise) timer += Time.deltaTime;
        else timer -= Time.deltaTime;

        if (timer > time) isClockwise = false;
        else if (timer < 0) isClockwise = true;

        transform.localEulerAngles = new Vector3(initialLocalEulerAngles.x, initialLocalEulerAngles.y + Mathf.Lerp(-15, 15, timer / time), initialLocalEulerAngles.z);
    }
}
