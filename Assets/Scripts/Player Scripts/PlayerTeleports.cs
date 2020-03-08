using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Part of this script is meant for debugging purposes only
 * PlayerTeleports takes a keyboard input and moves
 * the princess to the beginning of each section based
 * on input.
 *
 * In addition, this script controls the movement of the player
 * from section 2 to section 3
 * We may also use it to reset the Player's spawn when they die
 **/
public class PlayerTeleports : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Teleport to section 1");
            SceneManager.LoadScene("Section1&2");
            gameObject.transform.position = new Vector3(-13.67f, 5.06f, 6f);
            // set llama counter as if we had gone through the previous section
            LlamaCounter.Instance.ResetLlamaCount();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Teleport to section 2");
            gameObject.transform.position = new Vector3(-102.1f, 27.2f, 36.37f);
            // set llama counter as if we had gone through the previous section
            LlamaCounter.Instance.ResetLlamaCount();
            LlamaCounter.Instance.ThreexLlamaCount();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Teleport to section 3");
            SceneManager.LoadScene("Section3&4");
            // set llama counter as if we had gone through the previous section
            LlamaCounter.Instance.ResetLlamaCount();
            LlamaCounter.Instance.ThreexLlamaCount();
            LlamaCounter.Instance.ThreexLlamaCount();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Teleport to section 4");
            SceneManager.LoadScene("Section3&4");
            // set llama counter as if we had gone through the previous section
            LlamaCounter.Instance.ResetLlamaCount();
            LlamaCounter.Instance.ThreexLlamaCount();
            LlamaCounter.Instance.ThreexLlamaCount();
            LlamaCounter.Instance.ThreexLlamaCount();
        }
    }
}
