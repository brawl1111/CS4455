using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SpiderChase : MonoBehaviour
{
    private NavMeshAgent navAgent;

    public float aggroRange;
    public GameObject player;
    public int jumpDist;        // whats with assigning stuff where
    public int patrolRadius;

    public AIState aiState;

    public enum AIState
    {
        Idle,
        Patrol,
        Chase
    };

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        aiState = AIState.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        aggroRange = 8.0f;
    }

    // Update is called once per frame
    void Update()
    {
        

        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distToPlayer < aggroRange)
        {
            aiState = AIState.Chase;
        } else
        {
            aiState = AIState.Patrol;
        }


        // switch for state actions
        switch (aiState)
        {

            case AIState.Idle:
                //Debug.Log("spider idle");
                break;

            case AIState.Patrol:
                //Debug.Log("spider patrol");
                if (navAgent.remainingDistance == 0)
                {
                    if (navAgent.pathPending == false)
                    {
                        Vector3 randPos = RandomNavSphere(gameObject.transform.position, patrolRadius, -1);
                        navAgent.SetDestination(randPos);
                    }
                }
                break;

            case AIState.Chase:
                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position - dirToPlayer;
                navAgent.SetDestination(newPos);

                if (isPlayerFacingSpider(45))
                {
                    //Destroy(gameObject);
                    Debug.Log("spider facing player");
                    navAgent.Warp(transform.position + (-1.5f * dirToPlayer));
                    gameObject.transform.LookAt(player.transform);
                }

                break;


        } // switch


    } // update

    private bool isPlayerFacingSpider(float angle)
    {
        return Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;

    }

} // class
