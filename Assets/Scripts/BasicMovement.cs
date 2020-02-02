using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

	public float speed = 5f;
	public float jumpHeight = 2f;
	public float distanceToGround;
	public LayerMask ground;

	private Rigidbody rb;
	private Vector3 inputs = Vector3.zero;
	private bool isGrounded = true;
	private Transform groundCheck;

    public int maxExtraJumps;
    private int extraJumps;

    void Start()
    {
        extraJumps = maxExtraJumps;
        rb = GetComponent<Rigidbody>();
        groundCheck = transform.GetChild(0);
        /*
		GroundCheck gets the first child of the capsule. In this case, it's an empty child
		that's located at the bottom of the capsule. This is for checking if the capsule is "grounded." We use
		a function called Physics.CheckSphere which draws an imaginary sphere centered around
		groundCheck and determines if anything is intersecting that sphere.
        */
    }
 
    // Update is called once per frame
    void Update()
    {
        if (isGrounded) extraJumps = maxExtraJumps; //if player is on ground, reset double jump count
        
        if (Input.GetKeyDown("space") && isGrounded)
        {
            //Debug.Log("normal jump");
        	//rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpHeight;
        }
        else if (Input.GetKeyDown("space") && extraJumps > 0)
        {
            //Debug.Log("double jump");
            //rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpHeight;
            extraJumps--;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, ground, QueryTriggerInteraction.Ignore);
        //Debug.Log(isGrounded);
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        if (inputs != Vector3.zero)
        {
            transform.forward = inputs;
        }
    	rb.MovePosition(rb.position + inputs * speed * Time.fixedDeltaTime);
    }
}


// public class BasicMovement : MonoBehaviour
// {

//     public float Speed = 5f;
//     public float JumpHeight = 2f;
//     public float GroundDistance = 0.2f;
//     public float DashDistance = 5f;
//     public LayerMask Ground;

//     private Rigidbody _body;
//     private Vector3 _inputs = Vector3.zero;
//     private bool _isGrounded = true;
//     private Transform _groundChecker;

//     void Start()
//     {
//         _body = GetComponent<Rigidbody>();
//         _groundChecker = transform.GetChild(0);
//     }

//     void Update()
//     {
//         _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);


//         _inputs = Vector3.zero;
//         _inputs.x = Input.GetAxis("Horizontal");
//         _inputs.z = Input.GetAxis("Vertical");
//         Debug.Log(_inputs);
//         if (_inputs != Vector3.zero) 
//         {

//             transform.forward = _inputs;
//         }

//         if (Input.GetKeyDown("space") && _isGrounded)
//         {
//             _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
//         }
//     }


//     void FixedUpdate()
//     {
//         _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
//     }
// }