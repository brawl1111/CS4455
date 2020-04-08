using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
I'm trying to place spikes on top of the pushable crates by childing them, but it just doesn't ever register
collisions with the spikes. This is just a stupid workaround that makes the spikes move with the crate.
*/
public class SpikeOnCrate : MonoBehaviour
{
	//The crate this spike is attached to.
	public GameObject crate;
	private Vector3 offset;

	void Start()
	{
		offset = crate.transform.position - transform.position;
	}

    void Update()
    {
    	// Vector3 oldPos = crate.transform.position;
    	// Vector3 newPos = new Vector3(oldPos.x, oldPos.y + 0.9f, oldPos.z); 
     //   	this.transform.position = newPos;
    	transform.position = crate.transform.position + (offset);
    }
}
