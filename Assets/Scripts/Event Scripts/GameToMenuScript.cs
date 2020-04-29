using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameToMenuScript : MonoBehaviour
{
    public Image black;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = black.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void GoToMenu()
    {
        EventManager.TriggerEvent<ButtonClickEvent, Vector3>(transform.position);
        Time.timeScale = 1f;
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("MainMenu");
    }
}
