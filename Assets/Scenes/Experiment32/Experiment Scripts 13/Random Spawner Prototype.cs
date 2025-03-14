using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RandomSpawnerPrototype : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public float respawnTime = 200f;
    public int maxEntities = 20;
    public GameObject[] spawnablesEntities;
    public List<GameObject> spawnedEntities;
    [Space]
    public float width = 10f;
    public float length = 10f;
    public float height = 10f;
    public Color color = Color.green;




    private float currentTimer;


    private void OnDrawGizmos() 
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, new Vector3(width, height, length));
    }


    private void Start()
    {
        currentTimer = respawnTime;
    }

    private void Update()
    {
        if (currentTimer < respawnTime)
        {
            currentTimer += Time.deltaTime;
        }
        else
        {
            if (spawnablesEntities.Length <= 0)
            {   
                Debug.LogError("Spawnable Entities are not set");
                return;
            }
            int amountToRespawn = 0;
            amountToRespawn = GetAvailableEntities();

            for (int i = 0; i < amountToRespawn; i++)
            {
                int chosenEntity = 0;
                chosenEntity = Random.Range(0, spawnablesEntities.Length);




                StartCoroutine(SpawnObject(spawnablesEntities[chosenEntity]));
            }

            currentTimer = 0;
        }
    }

    public int GetAvailableEntities()
    {
        if (spawnedEntities.Count > 0)
        {
            for (int i = 0; i < spawnedEntities.Count; i++)
            {
                if (spawnedEntities[i] == null)
                {
                    spawnedEntities.RemoveAt(i);
                    i--;
                }
            }
        }

        return maxEntities - spawnedEntities.Count;
    }

    public IEnumerator SpawnObject(GameObject obj)
    {
        bool foundSpot = false;

        while (!foundSpot)
        {
            Vector3 spawnPos = transform.position;

            spawnPos.x += Random.Range(-width, width);
            spawnPos.y += height;
            spawnPos.z += Random.Range(-length, length);

            if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.GetComponent<Terrain>() != null)
                {
                    GameObject spawnObj = Instantiate(obj, hit.point, Quaternion.identity);

                    spawnedEntities.Add(spawnObj);

                        if (spawnObj.CompareTag("Building"))
                        {
                            if (navMeshSurface != null)
                            {
                                navMeshSurface.BuildNavMesh();
                            }
                        }
                    foundSpot = true;
                }

            }
        }

        yield return null;
    }


}