using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLlamaIconController : MonoBehaviour
{
    public GameObject llamaCounter;
    public GameObject llamaCount;
    // public GameObject triggerSphere;
    private int llamasRequired;
    private TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        GateAnimator gateAnimator = gameObject.GetComponent<GateAnimator>();
        llamasRequired = gateAnimator.section * 3;
        textMesh = llamaCount.GetComponent<TextMesh>();
        textMesh.text = llamasRequired.ToString();
        // llamaCounter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCount();
    }

    /*
    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entered");
        if (collider.CompareTag("Player"))
        {
            llamaCounter.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            llamaCounter.SetActive(false);
        }
    }
    */

    private void UpdateCount()
    {
        int currLlamaCount = LlamaCounter.Instance.GetLlamaCount();
        /*
        if (llamaCounter.activeSelf && currLlamaCount != System.Convert.ToInt32(textMesh.text))
        {
            textMesh.text = (llamasRequired - currLlamaCount).ToString();

            if (llamasRequired - currLlamaCount <= 0)
            {
                llamaCounter.SetActive(false);
            }
        }
        */
        if (llamasRequired - currLlamaCount <= 0)
        {
            llamaCounter.SetActive(false);
        }
    }
}
