using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCloning : MonoBehaviour
{
    public GameObject aiPrefab;
    private int numberOfAIs = 1;
    private float spawnInterval = 0.1f;
    public int max = 0;
    public float RespawnTime;
    [HideInInspector] public int ile = 0;
    public Transform maincamera;

    private void Start()
    {
        // Rozpocznij tworzenie klonów po starcie
        InvokeRepeating("SpawnAi", 0f, spawnInterval);
    }
    public void SpawnAi()
    {

            for (int i = 0; i < numberOfAIs; i++)
            {
            if (ile < max)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject aiInstance = Instantiate(aiPrefab, spawnPosition, Quaternion.identity);

                // Pobierz komponent Healthbar z klonowanego obiektu Ai
                Healthbar healthbar = aiInstance.GetComponentInChildren<Healthbar>();

                if (healthbar != null)
                {
                    // Ustaw obiekt gracza jako cel dla paska zdrowia
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
        float spawnRadius = 10f;
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, 10f, NavMesh.AllAreas))
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