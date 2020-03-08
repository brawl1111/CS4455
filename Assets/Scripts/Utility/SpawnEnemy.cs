using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float respawnTime = 3.0f;
    public int maxEnemies = 5;
    public int curEnemies = 0;


    // Start is called before the first frame update
    void Start()
    {
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
        //throw new Exception("spawned enemy must have EnemyCounter script");
        // enemies must have an EnemyCounter script, lets spawner keep track of enemy population
        //newBomb.transform.position = transform.position;      // can't use this with prefabs
        incrementEnemyCount();
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(respawnTime);

            //spawnEnemy();

            if (curEnemies < maxEnemies)
            {
                spawnEnemy();
            }

        }
    }

    public void incrementEnemyCount()
    {
        curEnemies += 1;
    }

    public void decrementEnemyCount()
    {
        // called in BombCounter.cs, which is in the Bomb prefab
        curEnemies -= 1;
    }

    public int getNumBombs()
    {
        return curEnemies;
    }

}