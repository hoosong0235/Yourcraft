using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    int index, maxIndex;
    RectTransform rtfSelected;
    List<int> blockNums;
    Text groundNum, grassNum, woodNum, leafNum, stoneNum;
    List<Text> blockNumTexts;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        maxIndex = 4;
        rtfSelected = GameObject.Find("Canvas").transform.Find("Selected").gameObject.GetComponent<RectTransform>();
        blockNums = new List<int> { 0, 0, 0, 0, 0 };
        groundNum = GameObject.Find("Canvas").transform.Find("GroundNum").gameObject.GetComponent<Text>();
        grassNum = GameObject.Find("Canvas").transform.Find("GrassNum").gameObject.GetComponent<Text>();
        woodNum = GameObject.Find("Canvas").transform.Find("WoodNum").gameObject.GetComponent<Text>();
        leafNum = GameObject.Find("Canvas").transform.Find("LeafNum").gameObject.GetComponent<Text>();
        stoneNum = GameObject.Find("Canvas").transform.Find("StoneNum").gameObject.GetComponent<Text>();
        blockNumTexts = new List<Text> { groundNum, grassNum, woodNum, leafNum, stoneNum };
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

    public int getBlockNums()
    {
        return blockNums[index];
    }

    public void addBlockNums(int i)
    {
        // Not Working for Bedrock
        if (i < maxIndex + 1) blockNumTexts[i].text = (++blockNums[i]).ToString();
    }

    public void subBlockNums()
    {
        blockNumTexts[index].text = (--blockNums[index]).ToString();
    }
}
