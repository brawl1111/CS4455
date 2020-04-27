using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Panel 1").gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransitionPanels(int currPanel)
    {
        EventManager.TriggerEvent<ButtonClickEvent, Vector3>(transform.position);
        transform.Find("Panel " + currPanel.ToString()).gameObject.SetActive(false);
        transform.Find("Panel " + (currPanel + 1).ToString()).gameObject.SetActive(true);
    }
}