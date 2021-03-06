﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle1;
    public float spawnCD = 0.1f;
    public int spawnCount = 1;
    public Tilemap tileMap;

    private List<Vector3> spawnPoints;
    private bool spawning = false;
    private int lastSpawnPointIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new List<Vector3>();

        for(int c = tileMap.cellBounds.xMax/2; c < tileMap.cellBounds.xMax; c++)
        {
            for (int r = tileMap.cellBounds.yMin; r < tileMap.cellBounds.yMax; r++)
            {
                Vector3Int localPoint = new Vector3Int(c, r, (int)tileMap.transform.position.y);
                Vector3 point = tileMap.CellToWorld(localPoint);

                if(tileMap.HasTile(localPoint))
                {
                    spawnPoints.Add(point);
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!spawning)
        {
            StartCoroutine("SpawnObstacle");
        }

    }


    IEnumerator SpawnObstacle()
    {
       spawning = true;
       yield return new WaitForSeconds(spawnCD);

       for(int i = 0; i < spawnCount; i++)
       {
            int listEntry = Random.Range(0, spawnPoints.Count);

            if(listEntry == lastSpawnPointIdx)
            {
                continue;
            }

            lastSpawnPointIdx = listEntry;

            Instantiate(obstacle1, spawnPoints[listEntry]+ new Vector3(0, 0.5f, 0 ), Quaternion.identity);
       }

        spawning = false;
    }

}

    


