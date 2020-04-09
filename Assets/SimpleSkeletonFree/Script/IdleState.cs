using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBase
{
    private WaitForSeconds idleTimer = new WaitForSeconds(2f);
    public IdleState(SkeletonFSM character) : base(character)
    {
    }

    public override void Tick(float distToPlayer, Transform transform, GameObject player)
    {
        //Debug.Log(distToPlayer);
        if (distToPlayer > 10.0f)
        {
            character.skeletonAnim.SetBool("inPatrol", true);
            character.SetState(new PatrolState(character));
        }
    }

    public IEnumerator SwapToPatrol()
    {
        yield return idleTimer;
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateEnter()
    {
        
    }
}