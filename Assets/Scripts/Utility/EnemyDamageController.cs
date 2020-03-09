using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDamageController : MonoBehaviour
{
    public int maxHP;
    public int curHP;
    
    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hurtbox"))
        {
            curHP--;
            if (curHP <= 0)
            {
                //Destroy(gameObject);          // this is for regular spawner
                gameObject.SetActive(false);    // this is for advanced spawner?
            }
        }
    }

    private void OnDisable()
    {
        curHP = maxHP;
    }

}
