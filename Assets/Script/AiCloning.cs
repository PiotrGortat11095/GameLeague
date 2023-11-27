using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCloning : MonoBehaviour
{
    public GameObject aiPrefab;
    public GameObject npc;
    public float spawnRadius = 10f;
    private int numberOfAIs = 1;
    private float spawnInterval = 1f;
    public int max = 0;
    public float RespawnTime;
    [HideInInspector] public int ile = 0;
    public bool activequest = false;
    public Transform maincamera;


    public void Update()
    {
        if (npc != null)
        {
            NPCInteractable npcc = npc.GetComponent<NPCInteractable>();
            if (npcc.Activequest && npcc.ItemName == aiPrefab.GetComponent<ThisItem>().przedmiotDoDodania.Name)
            {
                activequest = false;
            }

            if (npcc.questended && npcc.ItemName == aiPrefab.GetComponent<ThisItem>().przedmiotDoDodania.Name)
            {
                activequest = true;
            }
        }

        if (!activequest)
            InvokeRepeating("SpawnAi", 0f, spawnInterval);
    }
    public void SpawnAi()
    {

            for (int i = 0; i < numberOfAIs; i++)
            {
            if (ile < max)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject aiInstance = Instantiate(aiPrefab, spawnPosition, transform.rotation);


                Healthbar healthbar = aiInstance.GetComponentInChildren<Healthbar>();

                if (healthbar != null)
                {

                    healthbar.SetTarget(maincamera);
                }
                ile++;
                aiInstance.AddComponent<AiDestroyHandler>().aiCloning = this;
            }
            }

    }
    public void DecreaseIleCD()
    {
        Invoke(nameof(DecreaseIle), RespawnTime);
    }
    public void DecreaseIle()
    {
        ile--;
    }


    private Vector3 GetRandomSpawnPosition()
    {
        float spawnRadius = 30f;
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, spawnRadius, NavMesh.AllAreas))
        {
            spawnPosition = hit.position;
        }

        return spawnPosition;
    }

}
public class AiDestroyHandler : MonoBehaviour
{
    public AiCloning aiCloning;

    private void OnDestroy()
    {
        if (aiCloning != null)
        {
            aiCloning.DecreaseIleCD();
        }
    }
}