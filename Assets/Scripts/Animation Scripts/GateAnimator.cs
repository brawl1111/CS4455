using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnimator : MonoBehaviour
{

    private Animation anim;
    private bool swinging = false;
    private bool unswinging = false;
    private bool inSwing = false;

    void Start()
    {
        GameObject gate = gameObject.transform.Find("Gate").gameObject;

        if (gate)
        {
            anim = gate.GetComponent<Animation>();
        }
    }

    void Update()
    {
        if (anim.isPlaying)
        {
            return;
        }
        if (!anim.isPlaying && swinging)
        {
            anim.Play("Gate Swing");
            inSwing = true;
        }
        if (!anim.isPlaying && unswinging)
        {
            anim.Play("Gate Unswing");
            inSwing = false;
        }
        if (inSwing)
        {
            swinging = false;
        }
        else
        {
            unswinging = false;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody && !inSwing)
        {
            swinging = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody && inSwing)
        {
            unswinging = true;
        }
    }
}
