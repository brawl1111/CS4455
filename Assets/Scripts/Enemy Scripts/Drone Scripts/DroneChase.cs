using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DroneChase : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator anim;
    public Vector3[] circlePoints;
    private int circleIndex = 0;
    public AIState prevState;

    public Vector3 patrolCenter;
    public float aggroRange;
    public GameObject player;
    public int patrolRadius;

    public AIState aiState;

    public enum AIState
    {
        Idle,
        Patrol,
        Chase,
        Circle
    };

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        aiState = AIState.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
        aggroRange = 16f;
        //anim = GetComponent<Animator>();
        //patrolRadius = 3;            // or can set patrolRadius in prefab in Inspector
        patrolCenter = gameObject.transform.position;
        circlePoints = new Vector3[4];
    }

    // Update is called once per frame
    void Update()
    {

        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        prevState = aiState;

        if (distToPlayer < aggroRange)
        {
            aiState = AIState.Chase;
        } else if (navAgent.isStopped)
        {
            aiState = AIState.Circle;
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

            case AIState.Circle:

                if (prevState != AIState.Circle)
                {
                    circleIndex = 0;
                }

                for (int i = 0; i < circlePoints.Length; i++)
                {
                    Vector3 newPoint = new Vector3(Mathf.Cos((90 * i) * Mathf.Deg2Rad), player.transform.position.y, Mathf.Sin(90 * i) * Mathf.Deg2Rad) * 5;
                    circlePoints[i] = player.transform.position + newPoint;
                }

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
