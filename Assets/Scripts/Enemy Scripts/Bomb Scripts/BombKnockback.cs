using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class BombKnockback : MonoBehaviour
{

    private Rigidbody rb;
    private NavMeshAgent navAgent;
    private GameObject player;

    //public Vector3 velocity;
    public float knockbackDist;
    public float knockbackHeight;
    public float knockbackMult;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.GetComponent<VelocityReporter>().velocity);
        //Debug.Log(rb.velocity.y);
        //Debug.Log(Mathf.Approximately(rb.velocity.y, 0));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hurtbox"))
        {
            Debug.Log("bomb hit by princess");

            rb.isKinematic = false;
            navAgent.enabled = false;

            Vector3 dirToObj = transform.position - other.transform.position;
            Vector3.Normalize(dirToObj);
            Vector3 appliedForce = dirToObj;

            appliedForce = (knockbackMult * player.GetComponent<VelocityReporter>().velocity) + (knockbackDist * dirToObj);
            appliedForce.y = knockbackHeight;

            rb.AddForce(appliedForce, ForceMode.Impulse);

            StartCoroutine(reactivateNav());
            //rb.isKinematic = true;
            //navAgent.enabled = true;
        }
    }

    IEnumerator reactivateNav()
    {
        // no idea how to do this
        //Debug.Log("in reactivate");
        yield return new WaitForSeconds(1);
        //yield return new WaitWhile(() => rb.velocity.y >= 1);
        yield return new WaitUntil(() => Mathf.Approximately(rb.velocity.y, 0) == true);
        //yield return new WaitForSeconds(1);
        //Debug.Log("should be reactive");
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        navAgent.enabled = true;
        navAgent.Warp(transform.position);
    }

}
