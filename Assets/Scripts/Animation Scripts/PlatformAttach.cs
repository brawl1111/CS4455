using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
    	if (other.gameObject.CompareTag("Player")) 
    	{
    		other.gameObject.transform.parent = this.transform;
    		//other.gameObject.transform.localScale = Vector3.one * 20f;
    	}
    }

    void OnTriggerExit(Collider other)
    {
    	if (other.gameObject.CompareTag("Player")) 
    	{
    		other.gameObject.transform.parent = null;
    		other.gameObject.transform.localScale = Vector3.one * 20f;
    	}
    }
}
