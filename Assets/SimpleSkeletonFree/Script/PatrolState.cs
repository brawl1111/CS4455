using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateBase
{
    public float wanderRadius = 5.0f;
    private WaitForSeconds cooldown = new WaitForSeconds(5f);
    private bool swapToIdle = true;

    public PatrolState(SkeletonFSM character) : base(character)
    {
    }

    public override void Tick(float distToPlayer, Transform transform, GameObject player)
    {
        if (distToPlayer < 10.0f)
        {
            character.skeletonAnim.SetBool("inChase", true);
            character.SetState(new ChaseState(character));
        }

        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
        character.skeletonNav.SetDestination(newPos);

        if (distToPlayer > 10.0f && !character.skeletonNav.pathPending && swapToIdle)
        {
            //Debug.Log("test");
            character.skeletonAnim.SetBool("inPatrol", false);
            character.SetState(new IdleState(character));
        }
    }

    public override void OnStateExit()
    {
        character.skeletonAnim.SetBool("inPatrol", false);
    }

    public override void OnStateEnter()
    {
        
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {

        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}