using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    int x, y, z, xy, zy;
    float rand;
    public GameObject ground, grass, stone, bedrock;
    GameObject groundInstance, grassInstance, stoneInstance, bedrockInstance;
    Dictionary<int, int> xys, zys;

    // Start is called before the first frame update
    void Start()
    {
        xys = new Dictionary<int, int>();
        zys = new Dictionary<int, int>();

        // Initialize xys
        for (x = -64; x < 64; x++)
        {
            if (x == -64)
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
        for (z = -64; z < 64; z++)
        {
            if (z == -64)
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
        for (x = -64; x < 64; x++)
        {
            for (z = -64; z < 64; z++)
            {
                xy = xys[x];
                zy = zys[z];
                y = (xy + zy) / 2;

                // rand = UnityEngine.Random.Range(0f, 3f);
                // if (rand < 1f) y -= 1;
                // else if (rand < 2f) y += 0;
                // else y += 1;

                grassInstance = Instantiate(grass);
                grassInstance.transform.position = new UnityEngine.Vector3(x, y, z);

                for (int i = 0; i < 4; i++)
                {
                    groundInstance = Instantiate(ground);
                    groundInstance.transform.position = new UnityEngine.Vector3(x, y - i - 1, z);
                }

                for (int i = 0; i < 16; i++)
                {
                    stoneInstance = Instantiate(stone);
                    stoneInstance.transform.position = new UnityEngine.Vector3(x, y - i - 5, z);
                }

                bedrockInstance = Instantiate(bedrock);
                bedrockInstance.transform.position = new UnityEngine.Vector3(x, y - 21, z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
