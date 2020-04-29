using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneController : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = player.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            // SET THE WIN SCREEN
            Debug.Log("starting sitting");
            StartCoroutine(SittingSequence());
            // GameObject.Find("GameWinCanvas").GetComponent<GameWinMenuToggle>().GameWinMenuOn();
        }
    }

    //Sequence where Princess sits on the throne
    IEnumerator SittingSequence()
    {
        player.GetComponent<CharacterMovement>().enabled = false;
        player.GetComponent<CharacterMovement>().SetAnimMovementToZero();
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<CapsuleCollider>().enabled = false;
        anim.SetBool("isFalling", false);
        anim.SetTrigger("gameDone");
        player.transform.forward = -1f * transform.forward;
        player.transform.position = new Vector3(-19.7f, 231f, 41f);
        // anim.MatchTarget(new Vector3(-19.7f, 232f, 41f), Quaternion.Euler(0f, -179f, 0f), AvatarTarget.Root,
        //     new MatchTargetWeightMask(new Vector3(1f, 0f, 1f), 1f),
        //     anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 0.75f);

        yield return new WaitForSeconds(5f);
        GameObject.Find("GameWinCanvas").GetComponent<GameWinMenuToggle>().GameWinMenuOn();
    }
}
