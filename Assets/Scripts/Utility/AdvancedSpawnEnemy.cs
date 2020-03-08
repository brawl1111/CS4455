using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Uses same objects in a pool
 * Doesn't need to repeatedly create/destroy new objects
 * Does need to iterate over an array a lot though
 * Completely decoupled from the objects it spawns
 * 
 */
public class AdvancedSpawnEnemy : MonoBehaviour
{

    public GameObject[] pool;
    public GameObject prefab;
    public int maxEnemies;
    public int curEnemies;
    public int respawnTime;

    // Start is called before the first frame update
    void Start()
    {
        pool = new GameObject[maxEnemies];
        for (int i = 0; i < pool.Length; i++)
        {
            GameObject obj = Instantiate(prefab);
            pool[i] = obj;
            //obj.AddComponent<EnemyCounter>();           // so now i can just AddComponent(enemy counter) and setspawn to it's spawner
            //obj.GetComponent<EnemyCounter>().setSpawner(gameObject);
            obj.SetActive(false);
        }

        curEnemies = 0;

        StartCoroutine(EnemySpawner());

    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(respawnTime);

            //spawnEnemy();

            curEnemies = GetNumActive();          // no connection b/w spawned obj and spawner
            // else uses the EnemyCounter script attached to the spawned obj

            if (curEnemies < maxEnemies)
            {
                spawnFromPool();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnFromPool()
    {
        curEnemies++;
        GameObject spawnedObj = pool[FindFirstDeactiveIndex()];
        spawnedObj.SetActive(true);
        spawnedObj.transform.position = transform.position;
        spawnedObj.GetComponent<NavMeshAgent>().Warp(transform.position);
    }

    private int FindFirstDeactiveIndex()
    {
        int index = -1;
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeSelf == false)
            {
                index = i;
                break;
            }
        }
        if (index < 0)
        {
            Debug.Log("no deactive " + prefab + " objs");
        }
        return index;
    }

    private int GetNumActive()
    {
        int count = 0;
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeSelf == true)
            {
                count++;
            }
        }
        return count;
    }

    public void decrementEnemyCount()
    {
        curEnemies--;           // called by EnemyCounter, script is attached to prefabs instantiated
    }

}
