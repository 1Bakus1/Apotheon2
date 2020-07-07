using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public GameObject isEnemySpawned;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    // Start is called before the first frame update
    void Update()
    {
        isEnemySpawned = GameObject.FindGameObjectWithTag("Enemy");
        if (isEnemySpawned == null)
        {
            Spawn();
        }
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (playerHealth.currentHeath <= 0f)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
