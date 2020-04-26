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

    // Get Components
    public AIState aiState;
    private NavMeshAgent skeletonNav;
    private Animator skeletonAnim;
    private GameObject sword;
    private MeshCollider swordHitbox;

    // Navigation
    public float wanderRadius;
    public float wanderTimer;
    private Transform target;

    // Timers for coroutines
    private WaitForSeconds cooldown;
    private WaitForSeconds idleTime;
    private WaitForSeconds readyTime;
    private WaitForSeconds deathTime;

    // Player Object
    private GameObject player;

    // Flags
    bool ifSwapReady;
    bool ifSwapAttack;
    bool isColliding;

    // Skeleton Stats
    private int skeletonHealth;
    private Renderer skeletonRender;
    private Material skeletonMat;

    // Colors
    Color defaultEmis = new Color(120 / 255f, 120 / 255f, 120 / 255f);

    // Start is called before the first frame update
    void Start()
    {
        //Get Components
        skeletonNav = GetComponent<NavMeshAgent>();
        skeletonAnim = GetComponent<Animator>();
        skeletonRender = GetComponentInChildren<Renderer>();
        skeletonMat = GetComponentInChildren<Renderer>().sharedMaterial;

        //Sword Hitbox
        aiState = AIState.idle_state;
        sword = GameObject.Find("SWORD");
        swordHitbox = sword.GetComponent<MeshCollider>();
        swordHitbox.enabled = false;

        //Find Player
        player = GameObject.FindWithTag("Player");

        //Timers for coroutines
        cooldown = new WaitForSeconds(5f);
        idleTime = new WaitForSeconds(1f);
        readyTime = new WaitForSeconds(2.5f);
        deathTime = new WaitForSeconds(2f);

        //Flags
        ifSwapReady = true;
        ifSwapAttack = true;
        isColliding = false;

        //Skeleton 
        skeletonHealth = 3;

    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector3.Distance(base.transform.position, player.transform.position);
        switch(aiState)
        {
            case AIState.idle_state:
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
                    skeletonAnim.SetBool("inMeleeDist", true);
                    swordHitbox.enabled = true;
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
            // SET THE WIN SCREEN
            GameObject.Find("GameWinCanvas").GetComponent<GameWinMenuToggle>().GameWinMenuOn();
        }
    }

    IEnumerator attackDelay()
    {
        yield return readyTime;
        swordHitbox.enabled = false;
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
        if (isColliding) return;
        if (collision.gameObject.CompareTag("Hurtbox"))
        {
            isColliding = true;
            if (skeletonAnim.GetBool("inMeleeDist"))
            {
                skeletonAnim.SetBool("BackHit", true);
                skeletonHealth -= 1;
                EventManager.TriggerEvent<FlinchHit, Vector3>(this.transform.position);
                skeletonMat.SetColor("_Emission", Color.red);
                skeletonMat.SetColor("_Color", Color.red);
            }       
            StartCoroutine(SkeletonHitCD());
        }
    }

    IEnumerator SkeletonHitCD()
    {
        yield return idleTime;
        isColliding = false;
        skeletonAnim.SetBool("BackHit", false);
        skeletonMat.SetColor("_Emission", defaultEmis);
        skeletonMat.SetColor("_Color", Color.white);
    }

    IEnumerator DeathAnimation()
    {
        yield return deathTime;
        Destroy(gameObject);
    }
}
