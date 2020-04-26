using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityFrames : MonoBehaviour
{
	private Transform mesh;
	private WaitForSeconds blinkWait;
	private float blinkWaitSeconds = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        mesh = transform.GetChild(2);
        blinkWait = new WaitForSeconds(blinkWaitSeconds);
    }


    public void startIFrame() {
    	StartCoroutine(IFrame());
    }

    IEnumerator IFrame()
    {
    	mesh.gameObject.SetActive(false);
        yield return blinkWait;
        mesh.gameObject.SetActive(true);
        yield return blinkWait;
        mesh.gameObject.SetActive(false);
        yield return blinkWait;
        mesh.gameObject.SetActive(true);
        yield return blinkWait;
        mesh.gameObject.SetActive(false);
        yield return blinkWait;
        mesh.gameObject.SetActive(true);
        yield return blinkWait;
        mesh.gameObject.SetActive(false);
        yield return blinkWait;
        mesh.gameObject.SetActive(true);
        yield return blinkWait;
        mesh.gameObject.SetActive(false);
        yield return blinkWait;
        mesh.gameObject.SetActive(true);
        yield return blinkWait;
        mesh.gameObject.SetActive(false);
        yield return blinkWait;
        mesh.gameObject.SetActive(true);
        yield return blinkWait;
    
  

    }
}
