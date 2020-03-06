using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBomb : MonoBehaviour
{

    public GameObject bombPrefab;
    public float respawnTime = 3.0f;
    public int maxBombs = 5;
    public int curBombs = 0;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BombSpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnEnemy()
    {
        GameObject newBomb = Instantiate(bombPrefab) as GameObject;
        newBomb.GetComponent<NavMeshAgent>().Warp(transform.position);
        //newBomb.transform.position = transform.position;      // can't use this with prefabs
        incrementBombs();
    }

    IEnumerator BombSpawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(respawnTime);

            //spawnEnemy();

            if (curBombs < maxBombs)
            {
                spawnEnemy();
            }

        }
    }

    public void incrementBombs()
    {
        curBombs += 1;
    }

    public void decrementBombs()
    {
        // called in BombCounter.cs, which is in the Bomb prefab
        curBombs -= 1;
    }

    public int getNumBombs()
    {
        return curBombs;
    }

}
