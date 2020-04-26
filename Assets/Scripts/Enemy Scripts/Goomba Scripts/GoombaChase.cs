using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GoombaChase : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator anim;

    public Vector3 patrolCenter;
    public float aggroRange;
    public GameObject player;
    public int patrolRadius;
    public float baseSpeed;

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
        aiState = AIState.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
        aggroRange = 8.0f;
        anim = GetComponent<Animator>();
        //patrolRadius = 3;            // or can set patrolRadius in prefab in Inspector
        patrolCenter = gameObject.transform.position;
        baseSpeed = navAgent.speed;
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
                navAgent.speed = baseSpeed;
                if (navAgent.remainingDistance == 0)
                {
                    if (navAgent.pathPending == false)
                    {
                        Vector3 randPos = RandomNavSphere(patrolCenter, patrolRadius, -1);
                        navAgent.SetDestination(randPos);
                    }
                }
                break;

            case AIState.Chase:
                navAgent.speed = baseSpeed * 2;
                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position - dirToPlayer;
                navAgent.SetDestination(newPos);
                

                break;


        } // switch
        
    } // update

    // origin is navAgent.position, distance is radius of sphere, layermask should = -1
    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;

    }
}
