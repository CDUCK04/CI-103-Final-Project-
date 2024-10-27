using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npc;
    public Transform objectTransform;
    public float maxDistance = 3.0f;
    private int npcCount = 0;
    private int maxNPCs = 2;

    void Start()
    {
        InvokeRepeating("SpawnNPC", 12.0f, 12.0f);
    }

    void SpawnNPC()
    {
        if (npcCount < maxNPCs)
        {
            Vector3 spawnPosition = objectTransform.position + Random.insideUnitSphere * maxDistance;
            Instantiate(npc, spawnPosition, Quaternion.identity);
            npcCount++;
        }
        else
        {
            CancelInvoke("SpawnNPC");
        }
    }
}
