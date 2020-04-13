using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance
    {
        get {return s_Instance;}
    }
    private static CursorController s_Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else if (s_Instance != this)
        {
            throw new UnityException("There cannot be more than one CursorController script. The instances are " + s_Instance.name + " and " + name + ".");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public bool CursorIsShown()
    {
        return Cursor.visible;
    }
}
