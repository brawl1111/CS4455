using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SkeletonSM : MonoBehaviour
{
    public enum AIState
    {
        idle_state,
        patrol_state,
        chase_state,
        attack_state,
        ready_state,
    };

    public AIState aiState;
    private NavMeshAgent skeletonNav;
    private Animator skeletonAnim;

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    //private float timer;

    private WaitForSeconds cooldown;
    private WaitForSeconds idleTime;
    private WaitForSeconds readyTime;
    private WaitForSeconds deathTime;

    private GameObject player;

    bool inPatrol;
    bool inRange;
    bool ifSwapIdle;
    bool ifSwapPatrol;
    bool ifSwapReady;
    bool ifSwapAttack;
    bool isColliding;

    private int skeletonHealth;

    // Start is called before the first frame update
    void Start()
    {
        skeletonNav = GetComponent<NavMeshAgent>();
        skeletonAnim = GetComponent<Animator>();
        aiState = AIState.idle_state;
        //timer = 0;
        inRange = false;
        ifSwapIdle = true;
        ifSwapPatrol = true;
        cooldown = new WaitForSeconds(5f);
        idleTime = new WaitForSeconds(1f);
        player = GameObject.FindWithTag("Player");
        readyTime = new WaitForSeconds(2.5f);
        ifSwapReady = true;
        ifSwapAttack = true;
        isColliding = false;
        skeletonHealth = 3;
        deathTime = new WaitForSeconds(2f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(skeletonHealth);
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch(aiState)
        {
            case AIState.idle_state:
                if (distToPlayer < 20.0f)
                {
                    skeletonAnim.SetBool("inPatrol", true);
                    aiState = AIState.chase_state;
                    break;
                }
                if (ifSwapIdle)
                {
                    StartCoroutine(SwapToPatrol());
                    ifSwapIdle = false;
                    ifSwapPatrol = true;
                }
                break;
            case AIState.patrol_state:
                //timer += Time.deltaTime;
                //timer >= wanderTimer &&
                //Debug.Log("patrol");
                if (distToPlayer < 20.0f)
                {
                    skeletonAnim.SetBool("inPatrol", true);
                    aiState = AIState.chase_state;
                    break;
                }

                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
                skeletonNav.SetDestination(newPos);
                //timer = 0;

                if (!inRange && !skeletonNav.pathPending && ifSwapPatrol)
                {
                    StartCoroutine(SwapToIdle());
                    ifSwapIdle = true;
                    ifSwapPatrol = false;
                    //Debug.Log("swap");
                }
                break;
            case AIState.chase_state:
                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 chasePos = transform.position - dirToPlayer;
                skeletonNav.SetDestination(chasePos);
                if (distToPlayer < 3.0f)
                {
                    aiState = AIState.ready_state;
                    skeletonNav.stoppingDistance = 3.0f;
                }
                break;
            case AIState.ready_state:
                skeletonAnim.SetBool("inPatrol", false);
                if (distToPlayer < 3.0f)
                {
                    skeletonAnim.SetBool("inMeleeDist", true);
                    if (ifSwapAttack)
                    {
                        StartCoroutine(attackDelay());
                        ifSwapReady = true;
                        ifSwapAttack = false;
                        break;
                    }
                }
                if (3.0f < distToPlayer && distToPlayer < 20.0f)
                {
                    aiState = AIState.chase_state;
                    skeletonAnim.SetBool("inPatrol", true);
                    skeletonAnim.SetBool("inMeleeDist", false);
                    break;
                }
                if (distToPlayer > 20.0f)
                {
                    inRange = false;
                    aiState = AIState.patrol_state;
                    skeletonAnim.SetBool("inPatrol", true);
                    skeletonAnim.SetBool("inMeleeDist", false);
                    break;
                }
                break;
            case AIState.attack_state:
                if (ifSwapReady)
                {
                    StartCoroutine(SwapToReady());
                    ifSwapAttack = true;
                    ifSwapReady = false;
                }
                break;
        }
        if (skeletonHealth == 0)
        {
            skeletonAnim.SetBool("isDead", true);
            StartCoroutine(DeathAnimation());
        }
    }

    IEnumerator SwapToPatrol()
    {
        yield return idleTime;
        aiState = AIState.patrol_state;
        skeletonAnim.SetBool("inPatrol", true);
    }

    IEnumerator SwapToIdle()
    {
        yield return cooldown;
        aiState = AIState.idle_state;
        skeletonAnim.SetBool("inPatrol", false);
    }

    IEnumerator attackDelay()
    {
        yield return readyTime;
        EventManager.TriggerEvent<SwordSwing, Vector3>(this.transform.position);
        skeletonAnim.SetBool("attackBuffer", true);
        aiState = AIState.attack_state;
    }

    IEnumerator SwapToReady()
    {
        yield return readyTime;
        skeletonAnim.SetBool("attackBuffer", false);
        aiState = AIState.ready_state;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {

        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void OnTriggerEnter(Collider collision)
    {
        Vector3 heading = player.transform.position - transform.position;
        float side = Vector3.Dot(heading, transform.forward);

        if (isColliding) return;
        if (side > 0 && collision.gameObject.CompareTag("Hurtbox"))
        {
            isColliding = true;
            if (skeletonAnim.GetBool("attackBuffer") && skeletonAnim.GetBool("inMeleeDist"))
            {
                skeletonAnim.SetBool("Block", true);
                //Debug.Log("Hit");
            }
            StartCoroutine(SkeletonShieldCD());
        } else if (side < 0 && collision.gameObject.CompareTag("Hurtbox"))
        {
            isColliding = true;
            if (skeletonAnim.GetBool("attackBuffer") && skeletonAnim.GetBool("inMeleeDist"))
            {
                skeletonAnim.SetBool("BackHit", true);
                skeletonHealth -= 1;
                EventManager.TriggerEvent<FlinchHit, Vector3>(this.transform.position);
                //Debug.Log("Hit");
            }
            StartCoroutine(SkeletonHitCD());
        }
    }

    IEnumerator SkeletonShieldCD()
    {
        EventManager.TriggerEvent<ShieldClang, Vector3>(this.transform.position);
        yield return idleTime;
        isColliding = false;
        skeletonAnim.SetBool("Block", false);
    }

    IEnumerator SkeletonHitCD()
    {
        yield return idleTime;
        isColliding = false;
        skeletonAnim.SetBool("BackHit", false);
    }

    IEnumerator DeathAnimation()
    {
        yield return deathTime;
        Destroy(gameObject);
        //skeletonAnim.SetBool("isDead", false);
    }
}
