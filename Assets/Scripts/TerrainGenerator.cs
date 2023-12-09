using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    const int size = 16;
    int x, y, z, xy, zy, i;
    float rand, perc;
    public GameObject ground, grass, wood, leaf, stone, bedrock;
    GameObject groundInstance, grassInstance, woodInstance, leafInstance, stoneInstance, bedrockInstance;
    Dictionary<int, int> xys, zys;

    // Start is called before the first frame update
    void Start()
    {
        xys = new Dictionary<int, int>();
        zys = new Dictionary<int, int>();

        // Initialize xys
        for (x = -size; x < size; x++)
        {
            if (x == -size)
            {
                y = 0;
            }
            else
            {
                rand = UnityEngine.Random.Range(0f, 3f);
                if (rand < 1f) y -= 1;
                else if (rand < 2f) y += 0;
                else y += 1;
            }

            xys.Add(x, y);
        }

        // Initialize zys
        for (z = -size; z < size; z++)
        {
            if (z == -size)
            {
                y = 0;
            }
            else
            {
                rand = UnityEngine.Random.Range(0f, 3f);
                if (rand < 1f) y -= 1;
                else if (rand < 2f) y += 0;
                else y += 1;
            }

            zys.Add(z, y);
        }

        // Initial Terrain Generation
        for (x = -size; x < size; x++)
        {
            for (z = -size; z < size; z++)
            {
                // Grass, Ground, Stone, Bedrock Generation
                xy = xys[x];
                zy = zys[z];
                y = (xy + zy) / 2;

                // rand = UnityEngine.Random.Range(0f, 3f);
                // if (rand < 1f) y -= 1;
                // else if (rand < 2f) y += 0;
                // else y += 1;

                grassInstance = Instantiate(grass);
                grassInstance.transform.position = new UnityEngine.Vector3(x, y, z);

                for (i = 0; i < 4; i++)
                {
                    groundInstance = Instantiate(ground);
                    groundInstance.transform.position = new UnityEngine.Vector3(x, y - i - 1, z);
                }

                for (i = 0; i < 16; i++)
                {
                    stoneInstance = Instantiate(stone);
                    stoneInstance.transform.position = new UnityEngine.Vector3(x, y - i - 5, z);
                }

                bedrockInstance = Instantiate(bedrock);
                bedrockInstance.transform.position = new UnityEngine.Vector3(x, y - 21, z);

                // Tree Generation
                rand = UnityEngine.Random.Range(0f, 1f);
                if (rand < 0.01f)
                {
                    // Wood Generation
                    for (i = 0, perc = 1f; rand < perc; i++, perc = perc * 0.9f, rand = UnityEngine.Random.Range(0f, 1f))
                    {
                        woodInstance = Instantiate(wood);
                        woodInstance.transform.position = new UnityEngine.Vector3(x, y + i + 1, z);
                    }

                    // Leaf Generation
                    // TODO: Probablistic Generation
                    leafInstance = Instantiate(leaf);
                    leafInstance.transform.position = new UnityEngine.Vector3(x, y + i + 1, z);
                    leafInstance = Instantiate(leaf);
                    leafInstance.transform.position = new UnityEngine.Vector3(x - 1, y + i, z);
                    leafInstance = Instantiate(leaf);
                    leafInstance.transform.position = new UnityEngine.Vector3(x + 1, y + i, z);
                    leafInstance = Instantiate(leaf);
                    leafInstance.transform.position = new UnityEngine.Vector3(x, y + i, z - 1);
                    leafInstance = Instantiate(leaf);
                    leafInstance.transform.position = new UnityEngine.Vector3(x, y + i, z + 1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
