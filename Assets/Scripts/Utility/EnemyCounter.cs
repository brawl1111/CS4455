using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{

    private GameObject mySpawner;
    private SpawnEnemy spawnEnemyScript;
    //private Spawn spawnBombScriptPool;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyScript = mySpawner.GetComponent<SpawnEnemy>();

        //spawnBombScriptPool = GameObject.Find("BombSpawner2").GetComponent<Spawn>();      // for weird pooling stuff
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        spawnEnemyScript.decrementEnemyCount();

        //spawnBombScriptPool.decrementBombs();     // for weird pooling stuff
        //Debug.Log("bomb disabled");
    }

    public void setSpawner(GameObject spawner)
    {
        mySpawner = spawner;
    }
}