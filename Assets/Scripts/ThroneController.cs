using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            // SET THE WIN SCREEN
            GameObject.Find("GameWinCanvas").GetComponent<GameWinMenuToggle>().GameWinMenuOn();
        }
    }
}
