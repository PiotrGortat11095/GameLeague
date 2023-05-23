using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCloning : MonoBehaviour
{
    public AiMobs aiPrefab;
    public int maxClones = 5;
    [HideInInspector]public int cloneCount = 0;
    public float spawnRadius = 5f;
    private Vector3 randomPosition;

    public void CloneAI()
    {
        if (cloneCount >= maxClones)
        {
            return;
        }
        Vector3 spawnPosition = GetRandomSpawnPosition();
        if (spawnPosition != Vector3.zero) // zero vector indicates failed navmesh sample
        {
            AiMobs clonedAI = Instantiate(aiPrefab, spawnPosition, Quaternion.identity);
            cloneCount++;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        NavMeshHit hit;
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            Vector3 spawnPosition = transform.position + randomOffset;

            if (NavMesh.SamplePosition(spawnPosition, out hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero; // Return zero vector if no valid navmesh location found
    }
}
