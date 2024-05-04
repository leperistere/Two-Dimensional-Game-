using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    float maxSpawnRateInSeconds = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to spawn enemys
    void EnemySpawn()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));//this is the bottom left point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//this is the top right point of the screen

        //Instantiate an enemy
        GameObject anEnemy = (GameObject)Instantiate (Enemy);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        //Schedule the spawn of each enemy
        ScheduleSpawn();
    }

    void ScheduleSpawn()
    {
        float spawnInSeconds;

        if(maxSpawnRateInSeconds > 1f)
        {
            //Pick a random number
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 1f;

        Invoke("EnemySpawn", spawnInSeconds);
    }

    //Function to increase difficulty
    void IncreaseSpawnRate()
    {
        if(maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        if(maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    //Function to start spawning enemies
    public void ScheduleSpawner()
    {
        //Reset the spawn rate
        maxSpawnRateInSeconds = 5f;

        Invoke("EnemySpawn", maxSpawnRateInSeconds);

        //Increase spawn rate every 30 seconds
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    //Function to stop spawning enemies
    public void UnscheduleSpawner()
    {
        CancelInvoke("EnemySpawn");
        CancelInvoke("IncreaseSpawnRate");
    }
}
