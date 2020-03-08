using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{

    private GameObject mySpawner;
    //private AdvancedSpawnEnemy spawnScript;
    private SpawnEnemy spawnEnemyScript;
    //private Spawn spawnBombScriptPool;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        //spawnEnemyScript = mySpawner.GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        spawnEnemyScript.decrementEnemyCount();
    }

    public void setSpawner(GameObject spawner)      // this is called by the spawner
    {
        mySpawner = spawner;
        spawnEnemyScript = mySpawner.GetComponent<SpawnEnemy>();
    }
}