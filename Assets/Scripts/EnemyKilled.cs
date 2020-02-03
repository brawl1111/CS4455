using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilled : MonoBehaviour
{

	void Start() {
		Debug.Log("start Hurtbox");
	}
    void OnTriggerEnter(Collider c)
    {
    	Debug.Log(c.gameObject);
    	if (c.gameObject.CompareTag("Enemy"))
    	{
    		Debug.Log("detect enemy");
    		c.gameObject.SetActive(false);
    	}
    }
}
