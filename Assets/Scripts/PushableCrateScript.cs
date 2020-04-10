using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableCrateScript : MonoBehaviour
{
    private Rigidbody rigidbody;
    private CharacterMovement playerMvt;
    private int massPushable = 175;
    private int massHeavy = 10000;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerMvt = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
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
}
