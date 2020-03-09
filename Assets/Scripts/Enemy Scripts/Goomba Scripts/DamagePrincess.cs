using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePrincess : MonoBehaviour
{
	private SphereCollider sphereCol; 
    // Start is called before the first frame update
    void Start()
    {
        sphereCol = GetComponent<SphereCollider>();
    }

    void OnCollisionEnter(Collision c)
    {
    	//Debug.Log("collision detected with " + c.gameObject.CompareTag("Player"));
    	if (c.gameObject.CompareTag("Player") && !c.gameObject.GetComponent<CharacterMovement>().GetIsSpinning())
    	{
    		HealthManager.Instance.SubtractHealth(1);
    	}
    }
}
