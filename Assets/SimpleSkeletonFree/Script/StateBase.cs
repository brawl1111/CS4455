using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    protected SkeletonFSM character;

    public abstract void Tick(float distToPlayer, Transform position, GameObject player);

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public StateBase(SkeletonFSM character)
    {
        this.character = character;
    }
}