using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRestoreHealth : MonoBehaviour
{
	void Start()
	{
		gameObject.SetActive(true);
	}
    void OnTriggerEnter(Collider col)
    {
    	if (col.gameObject.CompareTag("Player"))
    	{
    		HealthManager.Instance.AddHealth(1);
    		gameObject.SetActive(false);
    	}
    }
}
