using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    int index, maxIndex;
    RectTransform rtfSelected;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        maxIndex = 4;
        rtfSelected = GameObject.Find("Canvas").transform.Find("Selected").gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getIndex()
    {
        return index;
    }

    public void addIndex()
    {
        if (index < maxIndex) index++;
        else index = 0;

        updatePos();
    }

    public void subIndex()
    {
        if (index > 0) index--;
        else index = maxIndex;

        updatePos();
    }

    void updatePos()
    {
        rtfSelected.anchoredPosition = new Vector3(56 * (index - 2), 48, 0);
    }
}
