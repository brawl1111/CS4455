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
            StartCoroutine(SittingSequence());
            // GameObject.Find("GameWinCanvas").GetComponent<GameWinMenuToggle>().GameWinMenuOn();
        }
    }

    //Sequence where Princess sits on the throne
    IEnumerator SittingSequence()
    {
        player.GetComponent<CharacterMovement>().enabled = false;
        player.GetComponent<CharacterMovement>().SetAnimMovementToZero();
        player.transform.forward = -1f * transform.forward;
        anim.SetTrigger("gameDone");

        yield return new WaitForSeconds(3f);
        GameObject.Find("GameWinCanvas").GetComponent<GameWinMenuToggle>().GameWinMenuOn();
    }
}
