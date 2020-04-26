using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableCrateScript : MonoBehaviour
{
    private Rigidbody rigidbody;
    private CharacterMovement playerMvt;
    private AudioSource audio;
    private int massPushable = 175;
    private int massHeavy = 10000;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerMvt = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMvt.isGroundedCheck)
        {
            rigidbody.mass = massPushable;
        }
        else
        {
            rigidbody.mass = massHeavy;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("We be entered");
        if (!audio.isPlaying && rigidbody.mass == massPushable && c.CompareTag("Player"))
        {
            Debug.Log("We be playin");
            audio.Play();
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player") && audio.isPlaying)
        {
            audio.Pause();
        }
    }
}