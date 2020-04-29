using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanGentlySpin : MonoBehaviour
{
	private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent(typeof(Animator)) as Animator;
    }

    void OnTriggerEnter(Collider c)
    {
        anim.SetBool("playerNear", true);
    }

    void OnTriggerExit(Collider c)
    {
        anim.SetBool("playerNear", false);
    }
}
