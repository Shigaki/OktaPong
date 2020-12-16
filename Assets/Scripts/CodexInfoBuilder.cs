using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodexInfoBuilder : MonoBehaviour
{

    public GameObject[] projPrefabs;

    public GameObject itemPrefab;
    private int itemCount = 0;

    private List<Sprite> Sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();

        {
            projPrefabs = Resources.LoadAll<GameObject>("Prefabs/Projectiles");
            //containerRectTransform.sizeDelta = new Vector2(containerRectTransform.rect.width, projPrefabs.Length * scrollHeight);

            foreach (var prefab in projPrefabs)
            {
                GameObject go;
                if (itemCount == projPrefabs.Length - 1)
                {
                    go = AddPanel(containerRectTransform, itemCount, true);
                }
                else
                {
                    go = AddPanel(containerRectTransform, itemCount);
                }
                var p = prefab.GetComponent<Projectile>();

                go.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + prefab.name);
                go.transform.Find("Name").GetComponent<Text>().text = p.pName;
                go.transform.Find("Info").GetComponent<Text>().text = "Base Damage: " + p.baseDamage + "\n" +
                                                                      "Size: "        + p.size + "\n" +
                                                                      "Speed: "       + p.speed + "\n" +
                                                                                        p.description;
                itemCount++;

            }
            containerRectTransform.anchoredPosition = Vector3.zero;

        }
    }


    public GameObject AddPanel(RectTransform containerRT, int index, bool lastIndex = false)
    {
        RectTransform itemRectTransform = itemPrefab.GetComponent<RectTransform>();

        float width = containerRT.rect.width;
        float ratio = width / itemRectTransform.rect.width;
        float height = itemRectTransform.rect.height * ratio;

        //Debug.Log("IT " + rowCount + " HEIGHT: " + containerRectTransform.rect.height);

        float scrollHeight = (height / 2) * index;
        containerRT.offsetMin = new Vector2(containerRT.offsetMin.x, -scrollHeight / 2);
        containerRT.offsetMax = new Vector2(containerRT.offsetMax.x, scrollHeight / 2);

        GameObject newPanel = Instantiate(itemPrefab);
        newPanel.name = gameObject.name + index;
        newPanel.transform.SetParent(gameObject.transform);

        RectTransform rectTransform = newPanel.GetComponent<RectTransform>();
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rectTransform.rect.height);

        float x = -containerRT.rect.width / 2;
        float y = -containerRT.rect.height - height;
        rectTransform.offsetMin = new Vector2(x, y);

        x = rectTransform.offsetMin.x + width;
        y = rectTransform.offsetMin.y + height;
        rectTransform.offsetMax = new Vector2(x, y);

        if(lastIndex)
        {
            containerRT.offsetMin = new Vector2(containerRT.offsetMin.x, -(scrollHeight + height) / 2);
            containerRT.offsetMax = new Vector2(containerRT.offsetMax.x, (scrollHeight + height) / 2);
        }

        return newPanel;
    }

}
