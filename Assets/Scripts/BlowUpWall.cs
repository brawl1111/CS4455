using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpWall : MonoBehaviour
{
	public GameObject newWall;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
        	Instantiate(newWall, transform.position, transform.rotation);
        	Destroy(gameObject);
        }
    }

    public void ExplodeWall()
    {
    	Instantiate(newWall, transform.position, transform.rotation);
		Destroy(gameObject);
    }
}
