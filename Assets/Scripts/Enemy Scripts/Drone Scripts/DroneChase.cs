using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DroneChase : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator anim;
    private int circleIndex = 0;
    public AIState prevState;       // public for testing purposes
    private float baseSpeed;
    private float baseStopDist;

    public Vector3 patrolCenter;
    public float aggroRange;
    public GameObject player;
    public int patrolRadius;
    public float stopDist;

    public AIState aiState;

    public enum AIState
    {
        Idle,
        Patrol,
        Chase,
        Circle,
        Kite
    }; // Idle, Circle currently unused

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        aiState = AIState.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
        aggroRange = 30f;
        //anim = GetComponent<Animator>();
        patrolRadius = 3;            // or can set patrolRadius in prefab in Inspector
        patrolCenter = gameObject.transform.position;
        baseSpeed = navAgent.speed;
        baseStopDist = navAgent.stoppingDistance;
        stopDist = navAgent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {

        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        prevState = aiState;

        if (Mathf.Abs(distToPlayer - stopDist) < 1)
        {
            aiState = AIState.Idle;
        }
        else if (distToPlayer < stopDist && isPlayerFacingThis(60))
        {
            aiState = AIState.Kite;
        }
        else if (distToPlayer < aggroRange)
        {
            aiState = AIState.Chase;
        }
        else
        {
            aiState = AIState.Patrol;
        }

        onStateChange(prevState, aiState);

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
                        Vector3 randPos = RandomNavSphere(patrolCenter, patrolRadius, -1);
                        navAgent.SetDestination(randPos);
                    }
                }
                break;

            case AIState.Chase:
                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position - dirToPlayer;
                navAgent.SetDestination(newPos);
                break;

            case AIState.Kite:

                if (isPlayerFacingThis(60))
                {
                    //Debug.Log("kiting begun");
                    navAgent.isStopped = false;
                    //navAgent.stoppingDistance = 0;
                    navAgent.SetDestination(transform.position + dirToTarget(gameObject, player));
                    Debug.Log(navAgent.destination);
                }

                break;


        } // switch

    } // update

    private void LateUpdate()
    {
        /*      // scrapping this
        switch (aiState)
        {
            case AIState.Circle:
                Orbit();
                break;
        }
        */

    } // lateUpdate

    // origin is navAgent.position, distance is radius of sphere, layermask should = -1
    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;

    }

    /*      // scrapped for now
    private void Orbit()
    {
        Transform targetTransform = player.transform;

        if (targetTransform != null)
        {
            // Keep us at orbitDistance from target
            transform.position = targetTransform.position + (transform.position - targetTransform.position).normalized * orbitDistance;
            transform.RotateAround(targetTransform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        }
    }
    */

    private bool isPlayerFacingThis(float angle)
    {
        return Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < angle;
    }

    private void onStateChange(AIState prev, AIState cur)
    {
        onStateExit(prev, cur);
        onStateEnter(prev, cur);
    }

    private void onStateEnter(AIState prev, AIState cur)
    {
        if (cur != prev)
        {
            switch(cur)
            {
                case AIState.Patrol:
                    navAgent.ResetPath();
                    break;

                case AIState.Kite:
                    navAgent.stoppingDistance = 0;
                    navAgent.speed = baseSpeed / 2;
                    break;
            }
        }
    }

    private void onStateExit(AIState prev, AIState cur)
    {
        if (prev != cur)
        {
            switch(prev)
            {
                case AIState.Kite:
                    navAgent.stoppingDistance = baseStopDist;
                    navAgent.speed = baseSpeed;
                    break;
            }
        }
    }

    private Vector3 dirToTarget(GameObject target, GameObject origin)
    {
        return (target.transform.position - origin.transform.position).normalized;
    }

}
