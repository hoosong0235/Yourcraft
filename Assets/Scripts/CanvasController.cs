using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    int index, maxIndex;
    RectTransform rtfSelected;
    List<int> blockNums;
    public GameObject groundNum, grassNum, woodNum, leafNum, stoneNum, bedrockNum, selected;
    Text groundNumText, grassNumText, woodNumText, leafNumText, stoneNumText, bedrockNumText;
    List<Text> blockNumTexts;
    bool isDebug;
    public GameObject d1txt, d2txt, d3txt, dbg;
    List<GameObject> debugs;
    Transform tfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        maxIndex = 5;
        rtfSelected = selected.GetComponent<RectTransform>();
        blockNums = new List<int> { 0, 0, 0, 0, 0, 0 };
        groundNumText = groundNum.GetComponent<Text>();
        grassNumText = grassNum.GetComponent<Text>();
        woodNumText = woodNum.GetComponent<Text>();
        leafNumText = leafNum.GetComponent<Text>();
        stoneNumText = stoneNum.GetComponent<Text>();
        bedrockNumText = bedrockNum.GetComponent<Text>();
        blockNumTexts = new List<Text> { groundNumText, grassNumText, woodNumText, leafNumText, stoneNumText, bedrockNumText };
        isDebug = false;
        debugs = new List<GameObject> { d1txt, d2txt, d3txt, dbg };
        tfPlayer = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            isDebug = !isDebug;
            foreach (GameObject debug in debugs) debug.SetActive(isDebug);
        }

        if (isDebug)
        {
            float dt = Time.deltaTime;
            float fps = 1.0f / dt;

            d2txt.GetComponent<Text>().text = String.Format("fps: {0} ({1}ms per frame)", (int)fps, (int)(dt * 1000));
            d3txt.GetComponent<Text>().text = String.Format("XYZ: {0} / {1} / {2}", tfPlayer.position.x, tfPlayer.position.y, tfPlayer.position.z);
        }
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
        rtfSelected.anchoredPosition = new UnityEngine.Vector3(32 * index - 80, 16, 0);
    }

    public int getBlockNums()
    {
        return blockNums[index];
    }

    public void addBlockNums(int i)
    {
        blockNumTexts[i].text = (++blockNums[i]).ToString();
    }

    public void subBlockNums()
    {
        blockNumTexts[index].text = (--blockNums[index]).ToString();
    }
}
