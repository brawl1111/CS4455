using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
	public Camera cam;
	public NavMeshAgent agent;
	public Rigidbody rb;
	private bool isJump;
	private bool hold = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        	Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        	RaycastHit hit;

        	if (Physics.Raycast(ray, out hit))
        	{
        		agent.SetDestination(hit.point);
        	}

        }

        if(Input.GetKeyDown(KeyCode.Space)){

			agent.enabled = false;
			rb.isKinematic = false;
			rb.AddForce(-100,400,0);
			if (hold) StartCoroutine(Hold());

 		}
    }

    IEnumerator Hold() {
    	hold = false;
    	yield return new WaitForSeconds(0.1f);
    	isJump = true;
    	hold = true;
    }

    void OnCollisionEnter(Collision col){

	   if(col.gameObject.CompareTag("terrain") && isJump){

	     agent.enabled=true;
	     rb.isKinematic=true;
	     isJump=false;
  		}
	}
}
