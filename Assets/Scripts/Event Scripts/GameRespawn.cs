using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRespawn : MonoBehaviour
{
    public GameObject[] respawns;
    private GameObject player;
    private int currentRespawn;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentRespawn = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespawnGame()
    {
        player.transform.position = respawns[currentRespawn].transform.position;

        HealthManager.Instance.AddHealth(3);

        HealthManager.Instance.SetIsAlive(true);
    }

    public void UpdateCurrentRespawn(int respawnZone)
    {
        currentRespawn = respawnZone;
    }
}
