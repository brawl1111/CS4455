using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinedJump : MonoBehaviour
{
 
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
 		if (rb.velocity.y < 0)
 		{
 			rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1);
 		}
 		else if (rb.velocity.y > 0 && !Input.GetKey("space"))
 		{
 			rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1);
 		}
    }
}
