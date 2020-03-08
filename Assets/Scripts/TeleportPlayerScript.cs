using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPlayerScript : MonoBehaviour
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
        Debug.Log("I got a something inside my bones");
        if (c.gameObject == GameObject.FindWithTag("Player"))
        {
            Debug.Log("I got a player inside my bones");
            SceneManager.LoadScene("Section3&4");
        }
    }
}
