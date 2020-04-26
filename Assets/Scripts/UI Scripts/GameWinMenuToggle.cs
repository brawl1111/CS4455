using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class GameWinMenuToggle : MonoBehaviour
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
        GameWinMenuOff();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameWinMenuOn()
    {
        StartCoroutine(WasteTimeCoroutine());
        // canvasGroup.interactable = true;
        // canvasGroup.blocksRaycasts = true;
        // canvasGroup.alpha = 1f;
        // Time.timeScale = 0f;
    }

    public void GameWinMenuOff()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
        CursorController.Instance.HideCursor();
    }

    IEnumerator WasteTimeCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
        CursorController.Instance.ShowCursor();
    }
}
