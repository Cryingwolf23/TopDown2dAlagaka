using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn; //this could hold the enemy or player prefab

    public float repeatInterval;  //the interval of the spawning, only the enemy will be changed in the unity editor

    // Start is called before the first frame update
    public void Start()
    {
        /* Condition will only be true if it's the enemySpawnPoint that uses it, because the player does not need to be 
         repeatedly spawned */
        if(repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);  //call the method at 0.0f with interval repeaInterval
        }
    }

    public GameObject SpawnObject()  //method to create spawned objects
    {
        if(prefabToSpawn != null) // Checks if there is a prefab
        {
            //Position is located in the EnemySpawnPosition gameObject in the heirarchy
            //Quarternion.identity means no rotation
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }

        return null; // it will only return null, if there is no prefab set in the unity Editor
    }

}
