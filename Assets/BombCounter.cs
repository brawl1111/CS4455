using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCounter : MonoBehaviour
{

    private SpawnBomb spawnBombScript;
    //private Spawn spawnBombScriptPool;

    // Start is called before the first frame update
    void Start()
    {
        spawnBombScript = GameObject.Find("Bomb Spawner").GetComponent<SpawnBomb>();

        //spawnBombScriptPool = GameObject.Find("BombSpawner2").GetComponent<Spawn>();      // for weird pooling stuff
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        spawnBombScript.decrementBombs();

        //spawnBombScriptPool.decrementBombs();     // for weird pooling stuff
        Debug.Log("bomb disabled");
    }
}
