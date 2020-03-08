using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSucker : MonoBehaviour
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
        if (c.attachedRigidbody && c.gameObject == GameObject.FindWithTag("Player"))
        {
            HealthManager.Instance.KillPlayer();
        }
    }
}
