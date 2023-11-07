using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pickups;

public class Spawn : MonoBehaviour
{
    public GameObject[] Prefabs;
    public Transform spawnPoint;
    // Start is called before the first frame update
    private void Start()
    {
        SpawnRandomPickup();
    }

    private void SpawnRandomPickup()
    {
        PickupType randomType = (PickupType)Random.Range(0, System.Enum.GetValues(typeof(PickupType)).Length);

        GameObject prefabToSpawn = Prefabs[(int)randomType];

        if (prefabToSpawn != null && spawnPoint != null)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab or spawn point not assigned");
        }
    }

}
