using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CatChase : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator anim;

    public Vector3 patrolCenter;
    public float aggroRange;
    public GameObject player;
    public int patrolRadius;

    public AIState aiState;
    public AIState prevState;

    public enum AIState
    {
        Idle,
        Patrol,
        Chase,
        Eat
    };

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        aiState = AIState.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
        aggroRange = 8.0f;
        //anim = GetComponent<Animator>();
        //patrolRadius = 3;            // or can set patrolRadius in prefab in Inspector
        patrolCenter = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        prevState = aiState;

        if (GetComponent<EnemyDamageController>().curHP == 1)
        {
            aiState = AIState.Eat;
        }
        else if (distToPlayer < aggroRange)
        {
            aiState = AIState.Chase;
        }
        else
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

            case AIState.Eat:
                if (prevState != AIState.Eat)
                {
                    GameObject food = GameObject.FindGameObjectWithTag("food");
                    navAgent.SetDestination(food.transform.position);
                    StartCoroutine(Eating(food));
                }
                break;

            default:

                break;


        } // switch

    } // update

    IEnumerator Eating(GameObject eatenFood)
    {
        yield return new WaitUntil(() => Vector3.Distance(eatenFood.transform.position, transform.position) < 3);
        navAgent.ResetPath();
        yield return new WaitForSecondsRealtime(3);
        GetComponent<EnemyDamageController>().curHP++;
        eatenFood.GetComponent<GetEaten>().Eaten();

    }

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
