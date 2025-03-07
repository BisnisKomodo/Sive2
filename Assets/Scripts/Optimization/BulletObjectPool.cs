using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;
    private List<GameObject> instantiatedParticles = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particle.SetActive(false);
            instantiatedParticles.Add(particle);
        }
    }

    public void SpawnBulletHole(Vector3 position, Quaternion rotation)
    {
        GameObject particle = GetAvailableParticle();
        particle.SetActive(true);
        particle.transform.position = position;
        particle.transform.rotation = rotation;
        StartCoroutine(DisableAfterSeconds(particle, 4f));
    }

    private GameObject GetAvailableParticle()
    {
        foreach (var particle in instantiatedParticles)
        {
            if (!particle.activeSelf)
            {
                return particle;
            }
        }

        var newParticle = Instantiate(particlePrefab);
        instantiatedParticles.Add(newParticle);
        return newParticle;
    }

    private IEnumerator DisableAfterSeconds(GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);
        particle.SetActive(false);
    }
    
}
