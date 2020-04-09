using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : StateBase
{
    private WaitForSeconds attackTimer = new WaitForSeconds(3f);
    
    public ReadyState(SkeletonFSM character) : base(character)
    {
    }

    public override void Tick(float distToPlayer, Transform transform, GameObject player)
    {
        if (distToPlayer <= 3.0f)
        {
            character.StartCoroutine(attackDelay());
        }
        if (3.0f < distToPlayer && distToPlayer < 10.0f)
        {
            character.skeletonAnim.SetBool("inChase", true);
            character.skeletonAnim.SetBool("inMeleeDist", false);
            character.SetState(new ChaseState(character));
        }
        if (distToPlayer > 10.0f)
        {
            character.skeletonAnim.SetBool("inPatrol", true);
            character.skeletonAnim.SetBool("inMeleeDist", false);
            character.SetState(new PatrolState(character));
        }

    }

    IEnumerator attackDelay()
    {
        yield return attackTimer;
        EventManager.TriggerEvent<SwordSwing, Vector3>(character.transform.position);
        character.skeletonAnim.SetBool("attackBuffer", true);
        //character.SetState(new AttackState(character));
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateEnter()
    {
    }
}
