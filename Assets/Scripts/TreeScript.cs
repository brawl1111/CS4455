using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public AudioClip treeCrack;
    public AudioClip treeHitGround;
    public AudioSource audioSource;

    private bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // Gets the player object and checks if they are currently spinning
        // If so, we're gonna start the falling animation
        GameObject collider = collision.gameObject;
        if (collider.gameObject == GameObject.Find("Princess") &&
            collider.gameObject.GetComponent<CharacterMovement>().GetIsSpinning() &&
            !isFalling)
        {
            // GameObject player = collider.gameObject.transform.GetChild(1).gameObject;
            // if (player == GameObject.Find("Player") && player.GetComponent<SpinningAttackPrincess>().getIsSpinning())
            // {
                gameObject.GetComponent<Animation>().Play("Tree Fall");
                EventManager.TriggerEvent<TreeCrackEvent, Vector3>(this.gameObject.transform.position);
                isFalling = true;
            // }
        }
        // But wait, what about when we hit the ground? We'll want to play
        // A sound to suggest that the tree has finished moving
        // In this case, Ground is eqivalent to layer 8
        else if (collider.gameObject.layer == 8)
        {
            EventManager.TriggerEvent<TreeHitGroundEvent, Vector3>(this.gameObject.transform.position);
        }
    }
}
