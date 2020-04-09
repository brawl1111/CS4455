using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : StateBase
{
    public ChaseState(SkeletonFSM character) : base(character)
    {
    }

    public override void Tick(float distToPlayer, Transform transform, GameObject player)
    {
        if (10.0f > distToPlayer && distToPlayer > 3.0f)
        {
            character.transform.LookAt(player.transform);
            character.skeletonNav.SetDestination(player.transform.position);
            
            if (distToPlayer <= 3.0f)
            {
                character.skeletonAnim.SetBool("inMeleeDist", true);
                character.SetState(new ReadyState(character));
            }
        } else if (distToPlayer > 10.0f)
        {
            character.skeletonAnim.SetBool("inPatrol", true);
            character.SetState(new PatrolState(character));
        }
    }

    public override void OnStateExit()
    {
        character.skeletonAnim.SetBool("inChase", false);
    }
}
