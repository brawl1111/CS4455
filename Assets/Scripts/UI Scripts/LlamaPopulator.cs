using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LlamaPopulator : MonoBehaviour
{
    public Sprite llamaIcon;
    private Stack<GameObject> llamas;

    // Start is called before the first frame update
    void Start()
    {
        llamas = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddLlamaIcon()
    {
        GameObject newLlamaIcon = new GameObject();
        Image llama = newLlamaIcon.AddComponent<Image>();
        llama.sprite = llamaIcon;
        newLlamaIcon.GetComponent<RectTransform>().SetParent(gameObject.transform);
        newLlamaIcon.SetActive(true);
        newLlamaIcon.transform.position = gameObject.transform.position + new Vector3(84 * (LlamaCounter.Instance.GetLlamaCount() - 1), 0, 0);
        newLlamaIcon.transform.localScale = new Vector3(1f, 1f, 1f);
        llamas.Push(newLlamaIcon);
    }

    public void RemoveLlamaIcons()
    {
        while (llamas.Count > 0)
        {
            Destroy(llamas.Pop());
        }
    }
}
