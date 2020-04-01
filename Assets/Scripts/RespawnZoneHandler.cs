using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZoneHandler : MonoBehaviour
{
    public int respawnZone;
    private GameRespawn gameRespawn;

    // Start is called before the first frame update
    void Start()
    {
        gameRespawn = GameObject.Find("RestartButton").GetComponent<GameRespawn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            gameRespawn.UpdateCurrentRespawn(respawnZone);
        }
    }
}
