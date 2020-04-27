using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResumer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeGame()
    {
        EventManager.TriggerEvent<ButtonClickEvent, Vector3>(transform.position);
        this.transform.parent.transform.parent.GetComponent<PauseMenuToggle>().PauseMenuOff();
    }
}
