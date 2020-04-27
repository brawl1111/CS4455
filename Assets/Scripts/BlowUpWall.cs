using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpWall : MonoBehaviour
{
	public GameObject newWall;
	bool hasAlreadyExploded = false; //fixes glitch where two bombs exploding can instantiate the prefab twice
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
        	Instantiate(newWall, transform.position, transform.rotation);
        	Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
    	Debug.Log(col);
    	if (col.gameObject.CompareTag("Explosion"))
    	{
    		ExplodeWall();
    	}
    }
    public void ExplodeWall()
    {
    	if (!hasAlreadyExploded)
    	{
    		hasAlreadyExploded = true;
	    	Instantiate(newWall, transform.position, transform.rotation);
			Destroy(gameObject);
    	}
    }
}
