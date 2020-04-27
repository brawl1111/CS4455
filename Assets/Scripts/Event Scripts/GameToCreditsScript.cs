using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameToCreditsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToCredits()
    {
        EventManager.TriggerEvent<ButtonClickEvent, Vector3>(transform.position);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }
}
