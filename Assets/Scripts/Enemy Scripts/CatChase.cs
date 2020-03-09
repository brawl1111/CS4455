using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CatChase : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private Animator anim;
    private Rigidbody rb;

    public Vector3 patrolCenter;
    public float aggroRange;
    public float attackRange;
    public GameObject player;
    public int patrolRadius;
    public float baseSpeed;

    public AIState aiState;
    public AIState prevState;

    public enum AIState
    {
        Idle,
        Patrol,
        Chase,
        Eat,
        Attack
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
        rb = GetComponent<Rigidbody>();
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
        else if (distToPlayer < attackRange)
        {
            aiState = AIState.Attack;
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
                Vector3 dirToPlayer = transform.position - player.transform.position;       // this is not direction to player, idk what this is
                Vector3 newPos = transform.position - dirToPlayer;
                navAgent.SetDestination(newPos);


                break;

            case AIState.Attack:
                if (prevState != AIState.Attack)
                {
                    Vector3 jumpDir = getDirToPlayer();      // direction to player
                    rb.isKinematic = false;
                    navAgent.enabled = false;
                    Vector3 appliedForce = 10 * jumpDir;
                    appliedForce.y = 7;
                    rb.AddForce(appliedForce, ForceMode.Impulse);
                    StartCoroutine(reactivateNav());
                }

                break;

            case AIState.Eat:
                if (prevState != AIState.Eat)           // on enter eat
                {
                    navAgent.speed *= 2;
                    //anim.Play("Run");         // figure out how to loop this
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
        navAgent.velocity = Vector3.zero;
        navAgent.ResetPath();
        yield return new WaitForSecondsRealtime(3);
        GetComponent<EnemyDamageController>().curHP++;
        eatenFood.GetComponent<GetEaten>().Eaten();
        navAgent.speed = baseSpeed;
        navAgent.isStopped = false;

    }
    IEnumerator reactivateNav()
    {
        // no idea how to do this
        //Debug.Log("in reactivate");
        yield return new WaitForSeconds(.5f);
        //yield return new WaitWhile(() => rb.velocity.y >= 1);
        yield return new WaitUntil(() => Mathf.Approximately(rb.velocity.y, 0) == true);
        //yield return new WaitForSeconds(1);
        //Debug.Log("should be reactive");
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        navAgent.enabled = true;
        navAgent.Warp(transform.position);
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

    private Vector3 getDirToPlayer()
    {
        return (player.transform.position - transform.position).normalized;
    }
}
