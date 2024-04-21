using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager sharedInstance = null;  //will be used to access the singleton object

    public SpawnPoint playerSpawnPoint;

    public RPGCameraManager cameraManager;
    
    void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)  //if there is no other instance then i am not the instance
        {
            Destroy(gameObject);   //destroy me
        }
        else
        {
            sharedInstance = this; //otherwise assign sharedInstance to the current object
        }
    }

    void Start()
    {
        SetUpScene();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void SetUpScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if(playerSpawnPoint != null)
        {
            GameObject player = playerSpawnPoint.SpawnObject(); //call the spawnobject method on SpawnPoint script here

            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
}
