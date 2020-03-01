using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Tweaks to jump to make it feel less floaty.
*/

public class RefinedJump : MonoBehaviour
{
 
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
    public float gravityScale = 2f;

	private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
 		if (rb.velocity.y < 0)
 		{
 			//When player is falling, apply more gravity to fall faster.
 			rb.velocity += Vector3.up * Physics.gravity.y * gravityScale * (fallMultiplier - 1) * Time.deltaTime;
            //Debug.Log(rb.velocity);
 		}
 		else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
 		{
 			//Basically if the player holds jump for longer, the player will jump higher.
 			rb.velocity += Vector3.up * Physics.gravity.y * gravityScale * (lowJumpMultiplier - 1) * Time.deltaTime;
 		}
    }
}
