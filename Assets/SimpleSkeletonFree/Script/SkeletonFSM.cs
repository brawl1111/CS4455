using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SkeletonFSM : MonoBehaviour
{
    private StateBase currentState;

    internal NavMeshAgent skeletonNav;
    internal Animator skeletonAnim;
    private GameObject player;

    private WaitForSeconds shieldTimer = new WaitForSeconds(1f);
    private WaitForSeconds backHitTimer = new WaitForSeconds(1f);
    private int skeletonHealth = 3;
    private bool isColliding;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        skeletonNav = GetComponent<NavMeshAgent>();
        skeletonAnim = GetComponent<Animator>();
        isColliding = false;
        SetState(new IdleState(this));
    }

    private void Update()
    {
        if (skeletonHealth == 0)
        {
            skeletonAnim.SetBool("isDead", true);
            StartCoroutine(DeathAnimation());
        }
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        currentState.Tick(distToPlayer, transform, player);
    }

    public void SetState(StateBase state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;

        if (currentState != null)
            currentState.OnStateExit();
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
                EventManager.TriggerEvent<ShieldClang, Vector3>(this.transform.position);
            }
            StartCoroutine(SkeletonShieldCD());
        }
        else if (side < 0 && collision.gameObject.CompareTag("Hurtbox"))
        {
            isColliding = true;
            if (skeletonAnim.GetBool("attackBuffer") && skeletonAnim.GetBool("inMeleeDist"))
            {
                skeletonAnim.SetBool("BackHit", true);
                skeletonHealth -= 1;
                EventManager.TriggerEvent<FlinchHit, Vector3>(this.transform.position);
            }
            StartCoroutine(SkeletonHitCD());
        }
    }

    IEnumerator SkeletonShieldCD()
    {
        yield return shieldTimer;
        isColliding = false;
        skeletonAnim.SetBool("Block", false);
    }

    IEnumerator SkeletonHitCD()
    {
        yield return backHitTimer;
        isColliding = false;
        skeletonAnim.SetBool("BackHit", false);
    }

    IEnumerator DeathAnimation()
    {
        yield return backHitTimer;
        Destroy(gameObject);
    }
}
