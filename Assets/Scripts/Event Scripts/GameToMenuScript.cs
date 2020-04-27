using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameToMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToMenu()
    {
        EventManager.TriggerEvent<ButtonClickEvent, Vector3>(transform.position);
        SceneManager.LoadScene("MainMenu");
    }
}
