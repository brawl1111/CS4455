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
        defense_state,
        flinch_state,
        death_state
    };

    public AIState aiState;
    private NavMeshAgent skeletonNav;
    private Animator skeletonAnim;

    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private float timer;

    bool inPatrol;
    bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        skeletonNav = GetComponent<NavMeshAgent>();
        skeletonAnim = GetComponent<Animator>();
        aiState = AIState.idle_state;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch(aiState)
        {
            case AIState.idle_state:
                StartCoroutine(SwapToIdle());
                break;
            case AIState.patrol_state:
                //timer += Time.deltaTime;
                //timer >= wanderTimer &&

                if (!skeletonNav.pathPending)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
                    skeletonNav.SetDestination(newPos);
                    //timer = 0;
                }

                if (!inRange && skeletonNav.remainingDistance == 0)
                {
                    aiState = AIState.idle_state;
                    skeletonAnim.SetBool("inPatrol", false);
                }
                break;
        }


    }

    public IEnumerator SwapToIdle()
    {
        yield return new WaitForSeconds(.5f);
        aiState = AIState.patrol_state;
        skeletonAnim.SetBool("inPatrol", true);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {

        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
