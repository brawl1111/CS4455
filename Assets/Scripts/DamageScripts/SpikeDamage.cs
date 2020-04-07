using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
    	Debug.Log(collision);
    	if (collision.gameObject.CompareTag("Player"))
    	{
    		HealthManager.Instance.SubtractHealth(1);
    	}
    }
}
