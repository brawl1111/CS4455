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

    bool ifSwapReady;
    bool ifSwapAttack;
    bool isColliding;

    private int skeletonHealth;

    // Start is called before the first frame update
    void Start()
    {
        skeletonNav = GetComponent<NavMeshAgent>();
        skeletonAnim = GetComponent<Animator>();
        //skeletonNav.stoppingDistance = 2.0f;
        aiState = AIState.idle_state;
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
        float distToPlayer = Vector3.Distance(base.transform.position, player.transform.position);
        switch(aiState)
        {
            case AIState.idle_state:
                /*
                if (distToPlayer < 5.0f)
                {
                    Debug.Log("In Melee");
                    skeletonAnim.SetBool("inMeleeDist", true);
                    skeletonAnim.SetBool("inChase", false);
                    aiState = AIState.ready_state;
                    break;
                } else if (distToPlayer <= 10.0f)
                {
                    skeletonAnim.SetBool("inChase", true);
                    aiState = AIState.chase_state;
                    break;
                } */
                if (distToPlayer <= 10.0f)
                {
                    skeletonAnim.SetBool("inChase", true);
                    aiState = AIState.chase_state;
                    break;
                }
                if (distToPlayer > 10.0f && !skeletonNav.pathPending)
                {
                    skeletonAnim.SetBool("inChase", true);
                    Vector3 newPos = RandomNavSphere(base.transform.position, wanderRadius, NavMesh.AllAreas);
                    skeletonNav.SetDestination(newPos);
                    break;
                }
                break;
            case AIState.chase_state:
                if (distToPlayer >= 4.0f && distToPlayer <= 10.0f)
                {
                    Vector3 dirToPlayer = base.transform.position - player.transform.position;
                    Vector3 newPos = base.transform.position - dirToPlayer;
                    skeletonNav.SetDestination(newPos);
                    break;
                } else if (distToPlayer < 3.0f)
                {
                    skeletonAnim.SetBool("inMeleeDist", true);
                    skeletonAnim.SetBool("inChase", false);
                    skeletonNav.stoppingDistance = 3.0f;
                    aiState = AIState.ready_state;
                    break;                   
                } 
                break;
            case AIState.ready_state:
                skeletonAnim.SetBool("inChase", false);
                if (distToPlayer < 3.0f)
                {
                    Quaternion wantedRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, Time.time * (float).0051);
                    skeletonAnim.SetBool("inMeleeDist", true);
                    if (ifSwapAttack)
                    {
                        StartCoroutine(attackDelay());
                        ifSwapReady = true;
                        ifSwapAttack = false;
                        break;
                    }
                } else if (distToPlayer >= 4.0f && distToPlayer <= 10.0f)
                {
                    skeletonAnim.SetBool("inChase", true);
                    skeletonAnim.SetBool("inMeleeDist", false);
                    aiState = AIState.chase_state;
                    break;
                } 
                if (distToPlayer > 10.0f)
                {
                    aiState = AIState.idle_state;
                    skeletonAnim.SetBool("inMeleeDist", false);
                }
                break;
            case AIState.attack_state:
                skeletonAnim.SetBool("inChase", false);
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
        Vector3 heading = player.transform.position - base.transform.position;
        float side = Vector3.Dot(heading, base.transform.forward);

        if (isColliding) return;
        if (side > 0 && collision.gameObject.CompareTag("Hurtbox"))
        {
            isColliding = true;
            if (skeletonAnim.GetBool("attackBuffer") && skeletonAnim.GetBool("inMeleeDist"))
            {
                skeletonAnim.SetBool("Block", true);
                EventManager.TriggerEvent<ShieldClang, Vector3>(this.transform.position);
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
