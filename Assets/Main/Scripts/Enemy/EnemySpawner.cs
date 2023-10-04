using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;

    private int rand;
    private int randPosition;
    private float timeBtwSpawns = 10f;
    private int spawendEnemies;
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(PeriodicSpawn());
    }

    private IEnumerator PeriodicSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBtwSpawns);
            rand = Random.Range(0, enemies.Length);
            randPosition = Random.Range(0, spawnPoints.Length);
            Instantiate(enemies[rand], spawnPoints[randPosition].position, Quaternion.identity);
        }
    }
}
