using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRespawn : MonoBehaviour
{
    public GameObject[] respawns;
    private GameObject player;
    private int currentRespawn;
    private float yAxis;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentRespawn = 0;
        yAxis = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespawnGame()
    {
        EventManager.TriggerEvent<ButtonClickEvent, Vector3>(transform.position);
        player.transform.position = respawns[currentRespawn].transform.position;
        player.transform.rotation = Quaternion.Euler(0f, yAxis, 0f);

        HealthManager.Instance.AddHealth(3);
        HealthManager.Instance.SetIsAlive(true);
    }

    public void UpdateCurrentRespawn(int respawnZone, float newYAxis)
    {
        currentRespawn = respawnZone;
        yAxis = newYAxis;
    }
}
