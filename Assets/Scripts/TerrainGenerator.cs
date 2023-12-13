using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    const int size = 32;
    int x, y, z, xy, zy, i, j, k, minX, maxX, minZ, maxZ;
    int groundDepth, grassDepth, stoneDepth, bedrockDepth;
    float rand, perc;
    public GameObject ground, grass, wood, leaf, stone, bedrock;
    GameObject groundInstance, grassInstance, woodInstance, leafInstance, stoneInstance, bedrockInstance;
    Dictionary<int, int> xys, zys;
    Transform tfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        groundDepth = 2;
        grassDepth = 1;
        stoneDepth = 4;
        bedrockDepth = 1;

        minX = -size;
        maxX = size - 1;
        minZ = -size;
        maxZ = size - 1;
        xys = new Dictionary<int, int>();
        zys = new Dictionary<int, int>();
        tfPlayer = GameObject.Find("Player").transform;

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
                xy = xys[x];
                zy = zys[z];
                y = (xy + zy) / 2;

                // rand = UnityEngine.Random.Range(0f, 3f);
                // if (rand < 1f) y -= 1;
                // else if (rand < 2f) y += 0;
                // else y += 1;

                generateTerrain(x, y, z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateGenerateTerrain();
    }

    void generateTerrain(float x, float y, float z)
    {
        // Grass, Ground, Stone, Bedrock Generation
        grassInstance = Instantiate(grass);
        grassInstance.transform.position = new UnityEngine.Vector3(x, y, z);

        for (i = 0; i < groundDepth; i++)
        {
            groundInstance = Instantiate(ground);
            groundInstance.transform.position = new UnityEngine.Vector3(x, y - i - grassDepth, z);
        }

        for (i = 0; i < stoneDepth; i++)
        {
            stoneInstance = Instantiate(stone);
            stoneInstance.transform.position = new UnityEngine.Vector3(x, y - i - grassDepth - groundDepth, z);
        }

        for (i = 0; i < bedrockDepth; i++)
        {
            bedrockInstance = Instantiate(bedrock);
            bedrockInstance.transform.position = new UnityEngine.Vector3(x, y - grassDepth - groundDepth - stoneDepth, z);
        }

        // Tree Generation
        rand = UnityEngine.Random.Range(0f, 1f);
        if (rand < 0.01f)
        {
            // Wood Generation
            for (i = 0; i < 3; i++)
            {
                woodInstance = Instantiate(wood);
                woodInstance.transform.position = new UnityEngine.Vector3(x, y + i + 1, z);
            }
            for (i = 3, perc = 1f; rand < perc; i++, perc = perc * 0.5f, rand = UnityEngine.Random.Range(0f, 1f))
            {
                woodInstance = Instantiate(wood);
                woodInstance.transform.position = new UnityEngine.Vector3(x, y + i + 1, z);
            }

            // Leaf Generation
            // TODO: Probablistic Generation
            for (j = i; j < i + 3; j++)
            {
                List<List<int>> targets;

                if (j == i)
                {
                    targets = new List<List<int>> { new List<int> { -1, -1 }, new List<int> { -1, 0 }, new List<int> { -1, 1 }, new List<int> { 0, -1 }, new List<int> { 0, 1 }, new List<int> { 1, -1 }, new List<int> { 1, 0 }, new List<int> { 1, 1 } };
                }
                else if (j == i + 1)
                {
                    targets = new List<List<int>> { new List<int> { -1, -1 }, new List<int> { -1, 0 }, new List<int> { -1, 1 }, new List<int> { 0, -1 }, new List<int> { 0, 0 }, new List<int> { 0, 1 }, new List<int> { 1, -1 }, new List<int> { 1, 0 }, new List<int> { 1, 1 } };
                }
                else
                {
                    targets = new List<List<int>> { new List<int> { -1, 0 }, new List<int> { 1, 0 }, new List<int> { 0, 0 }, new List<int> { 0, -1 }, new List<int> { 0, 1 } };
                }

                foreach (List<int> target in targets)
                {
                    leafInstance = Instantiate(leaf);
                    leafInstance.transform.position = new UnityEngine.Vector3(x + target[0], y + j, z + target[1]);
                }
            }
        }
    }

    void updateGenerateTerrain()
    {
        if (tfPlayer.position.x - size < minX)
        {
            xy = xys[minX--];
            rand = UnityEngine.Random.Range(0f, 3f);
            if (rand < 1f) xy -= 1;
            else if (rand < 2f) xy += 0;
            else xy += 1;
            xys.Add(minX, xy);

            for (z = minZ; z < maxZ + 1; z++)
            {
                zy = zys[z];
                y = (xy + zy) / 2;

                generateTerrain(minX, y, z);
            }
        }
        else if (tfPlayer.position.x + size - 1 > maxX)
        {
            xy = xys[maxX++];
            rand = UnityEngine.Random.Range(0f, 3f);
            if (rand < 1f) xy -= 1;
            else if (rand < 2f) xy += 0;
            else xy += 1;
            xys.Add(maxX, xy);

            for (z = minZ; z < maxZ + 1; z++)
            {
                zy = zys[z];
                y = (xy + zy) / 2;

                generateTerrain(maxX, y, z);
            }
        }

        if (tfPlayer.position.z - size < minZ)
        {
            zy = zys[minZ--];
            rand = UnityEngine.Random.Range(0f, 3f);
            if (rand < 1f) zy -= 1;
            else if (rand < 2f) zy += 0;
            else zy += 1;
            zys.Add(minZ, zy);

            for (x = minX; x < maxX + 1; x++)
            {
                xy = xys[x];
                y = (xy + zy) / 2;

                generateTerrain(x, y, minZ);
            }
        }
        else if (tfPlayer.position.z + size > maxZ)
        {
            zy = zys[maxZ++];
            rand = UnityEngine.Random.Range(0f, 3f);
            if (rand < 1f) zy -= 1;
            else if (rand < 2f) zy += 0;
            else zy += 1;
            zys.Add(maxZ, zy);

            for (x = minX; x < maxX + 1; x++)
            {
                xy = xys[x];
                y = (xy + zy) / 2;

                generateTerrain(x, y, maxZ);
            }
        }
    }
}
