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

    // Timers for coroutines
    private WaitForSeconds idleTime;
    private WaitForSeconds readyTime;
    private WaitForSeconds deathTime;
    private WaitForSeconds attackTime;

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

    // Mountain object
    public GameObject magicMtnWall;
    public GameObject music;
    public GameObject particles;

    // Colors
    Color defaultEmis = new Color(120 / 255f, 120 / 255f, 120 / 255f);

    public float turnSpeed = 10f;

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
        idleTime = new WaitForSeconds(1f);
        attackTime = new WaitForSeconds(2f);
        readyTime = new WaitForSeconds(2f);
        deathTime = new WaitForSeconds(1f);

        //Flags
        ifSwapReady = true;
        ifSwapAttack = true;
        isColliding = false;

        //Skeleton
        skeletonHealth = 5;
        skeletonNav.stoppingDistance = 3.0f;

    }

    // Update is called once per frame
    void Update()
    {
        //skeletonNav.transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
        float distToPlayer = Vector3.Distance(base.transform.position, player.transform.position);
        switch(aiState)
        {
            case AIState.idle_state:
                if (distToPlayer <= 15.0f)
                {
                    skeletonAnim.SetBool("inChase", true);
                    aiState = AIState.chase_state;
                    break;
                }
                if (distToPlayer > 15.0f && !skeletonNav.pathPending)
                {
                    skeletonAnim.SetBool("inChase", true);
                    Vector3 newPos = RandomNavSphere(base.transform.position, wanderRadius, NavMesh.AllAreas);
                    skeletonNav.SetDestination(newPos);
                    break;
                }
                break;
            case AIState.chase_state:
                if (distToPlayer >= 5.0f && distToPlayer <= 15.0f)
                {
                    Vector3 dirToPlayer = base.transform.position - player.transform.position;
                    Vector3 newPos = base.transform.position - dirToPlayer;
                    skeletonNav.SetDestination(newPos);
                    break;
                } else if (distToPlayer < 5.0f)
                {
                    skeletonNav.ResetPath();
                    aiState = AIState.ready_state;
                    break;
                } else if (distToPlayer > 15.0f)
                {
                    aiState = AIState.idle_state;
                    break;
                }
                break;
            case AIState.ready_state:
                if (distToPlayer < 5.0f)
                {
                    skeletonNav.ResetPath();
                    if (ifSwapAttack)
                    {
                        swordHitbox.enabled = true;
                        skeletonAnim.SetBool("inMeleeDist", true);
                        skeletonAnim.SetBool("inChase", false);   
                        StartCoroutine(attackDelay());
                        break;
                    }
                } else if (distToPlayer >= 5.0f && distToPlayer <= 15.0f)
                {
                    skeletonAnim.SetBool("inChase", true);
                    skeletonAnim.SetBool("inMeleeDist", false);
                    aiState = AIState.chase_state;
                    break;
                }
                if (distToPlayer > 15.0f)
                {
                    skeletonAnim.SetBool("inMeleeDist", false);
                    aiState = AIState.idle_state;
                }
                break;
            case AIState.attack_state:
                if (ifSwapReady)
                {
                    StartCoroutine(SwapToReady());
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
        //EventManager.TriggerEvent<SwordSwing, Vector3>(this.transform.position);
        yield return attackTime;
        swordHitbox.enabled = false;
        ifSwapReady = true;
        ifSwapAttack = false;
        aiState = AIState.attack_state;
    }

    IEnumerator SwapToReady()
    {
        yield return readyTime;
        ifSwapAttack = true;
        ifSwapReady = false;
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
        skeletonMat.SetColor("_Emission", defaultEmis);
        skeletonMat.SetColor("_Color", Color.white);
    }

    IEnumerator DeathAnimation()
    {
        music.GetComponent<MusicChange>().VolumeDown();
        yield return deathTime;
        magicMtnWall.GetComponent<MountainWallAnimator>().DropWall();
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
