using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup)
        {
            Debug.LogError("Canvas Group not found.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp (KeyCode.Escape))
        {
            if (canvasGroup.interactable)
            {
                PauseMenuOff();
            }
            else
            {
                PauseMenuOn();
            }
        }
    }

    public void PauseMenuOn()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }

    public void PauseMenuOff()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
    }
}
