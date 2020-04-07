using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRestoreHealth : MonoBehaviour
{
	public int healthRestored = 1;
	void Start()
	{
		gameObject.SetActive(true);
	}
    void OnTriggerEnter(Collider col)
    {
    	if (col.gameObject.CompareTag("Player"))
    	{
    		HealthManager.Instance.AddHealth(healthRestored);
    		gameObject.SetActive(false);
    	}
    }
}
