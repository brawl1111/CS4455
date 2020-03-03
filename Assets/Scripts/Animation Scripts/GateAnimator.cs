﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnimator : MonoBehaviour
{

    public AudioClip gateOpen;
    public AudioSource audioSource;
    public GameObject princess;
    public int section;

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
        // We're currently swinging
        if (anim.isPlaying)
        {
            return;
        }
        // We should be swinging out
        if (!anim.isPlaying && swinging)
        {
            anim.Play("Gate Swing");
            EventManager.TriggerEvent<GateSwingEvent, Vector3>(gameObject.transform.position);
            inSwing = true;
        }
        // We should be swinging in
        if (!anim.isPlaying && unswinging)
        {
            anim.Play("Gate Unswing");
            EventManager.TriggerEvent<GateSwingEvent, Vector3>(gameObject.transform.position);
            inSwing = false;
        }
        // We're done swinging and we're out
        if (inSwing)
        {
            swinging = false;
        }
        // We're done swinging and we're in
        else
        {
            unswinging = false;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody && !inSwing && princess.GetComponent<CharacterMovement>().GetLlamaCount(section) == 3)
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
