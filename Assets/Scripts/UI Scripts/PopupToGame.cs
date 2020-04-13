using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupToGame : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = gameObject.transform.parent.GetComponent<CanvasGroup>();
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
