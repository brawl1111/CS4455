using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnTime;
    public int maxEnemies;
    public int curEnemies;

    // Start is called before the first frame update
    void Start()
    {
        respawnTime = 3f;
        maxEnemies = 5;
        curEnemies = 0;
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab) as GameObject;
        newEnemy.GetComponent<NavMeshAgent>().Warp(transform.position);
        newEnemy.GetComponent<EnemyCounter>().setSpawner(gameObject);
        incrementEnemyCount();
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(respawnTime);
            if (curEnemies < maxEnemies)
            {
                spawnEnemy();
            }

        }
    }

    public void incrementEnemyCount()
    {
        curEnemies++;
    }

    public void decrementEnemyCount()
    {
        curEnemies--;
    }

    public int getNumEnemies()
    {
        return curEnemies;
    }

}
