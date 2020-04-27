using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        EventManager.TriggerEvent<StartButtonClickEvent, Vector3>(transform.position);
        SceneManager.LoadScene("Section1&2");
        CursorController.Instance.HideCursor();
    }
}