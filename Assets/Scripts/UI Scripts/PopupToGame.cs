using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopupToGame : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private PopupPanelController panelController;

    void Awake()
    {
        canvasGroup = transform.parent.transform.parent.GetComponent<CanvasGroup>();
        panelController = transform.parent.transform.parent.GetComponent<PopupPanelController>();
        if (!canvasGroup)
        {
            Debug.LogError("Canvas Group not found.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!CursorController.Instance.CursorIsShown())
        {
            CursorController.Instance.ShowCursor();
        }
    }

    public void NextPopupScreen()
    {
        string name = transform.parent.name;
        panelController.TransitionPanels(Int32.Parse(name.Substring(name.Length - 1)));
    }

    public void PopupOff()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
        CursorController.Instance.HideCursor();
        Destroy(gameObject);
    }
}
