using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPopulator : MonoBehaviour
{
    public Sprite heartIcon;
    public Text heartCountText;
    private Stack<GameObject> hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = new Stack<GameObject>();
        // InitHeartIcons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddHeartCount(int heartCount)
    {
        heartCountText.GetComponent<Text>().text = heartCount.ToString();
    }

    public void RemoveHeartCount(int heartCount)
    {
        heartCountText.GetComponent<Text>().text = heartCount.ToString();
    }

    public void AddHeartIcon()
    {
        GameObject newHeartIcon = new GameObject();
        Image heart = newHeartIcon.AddComponent<Image>();
        heart.sprite = heartIcon;
        newHeartIcon.GetComponent<RectTransform>().SetParent(gameObject.transform);
        newHeartIcon.SetActive(true);
        newHeartIcon.transform.position = gameObject.transform.position + new Vector3(84 * (HealthManager.Instance.GetHealth() - 1), 0, 0);
        newHeartIcon.transform.localScale = new Vector3(1f, 1f, 1f);
        hearts.Push(newHeartIcon);
    }

    public void AddHeartIconAtCertainPoint(int point)
    {
        GameObject newHeartIcon = new GameObject();
        Image heart = newHeartIcon.AddComponent<Image>();
        heart.sprite = heartIcon;
        newHeartIcon.GetComponent<RectTransform>().SetParent(gameObject.transform);
        newHeartIcon.SetActive(true);
        newHeartIcon.transform.position = gameObject.transform.position + new Vector3(84 * point, 0, 0);
        newHeartIcon.transform.localScale = new Vector3(1f, 1f, 1f);
        hearts.Push(newHeartIcon);
    }

    public void InitHeartIcons()
    {
        AddHeartIconAtCertainPoint(0);
        AddHeartIconAtCertainPoint(1);
        AddHeartIconAtCertainPoint(2);
    }

    public void RemoveHeartIcon()
    {
        Destroy(hearts.Pop());
    }

    public void RemoveHeartIcons()
    {
        while (hearts.Count > 0)
        {
            Destroy(hearts.Pop());
        }
    }
}
