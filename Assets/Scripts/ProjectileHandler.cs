using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ProjectileHandler : MonoBehaviour
{

    public static ProjectileHandler _pHandler;
    public GameObject[] projPrefabs;

    public GameObject p1projUI;
    public GameObject p2projUI;

    private List<Sprite> Sprites = new List<Sprite>();

    void Awake() 
    {
        if (_pHandler == null)
        {
            _pHandler = this;
        }
        else
        {
            Destroy(gameObject);
        }
        p1projUI = GameObject.Find("Canvas/MatchUI/P1_SelectedProj");
        p2projUI = GameObject.Find("Canvas/MatchUI/P2_SelectedProj");
    }

    // Start is called before the first frame update
    void Start()
    {        
        projPrefabs = Resources.LoadAll<GameObject>("Prefabs/Projectiles");
        foreach(var prefab in projPrefabs)
        {
            Sprites.Add(Resources.Load<Sprite>("Sprites/" + prefab.name));
            //Sprites.Add(Sprite.Create(AssetPreview.GetAssetPreview(prefab), new Rect(0f, 0f, 128f, 128f), new Vector2(0.5f, 0.5f)));
        }
        Sprites.Add(Resources.Load<Sprite>("Sprites/Random"));
    }

    public void ChangeSelectedProjectile(int player, int index)
    {
        if(player == 1)
        {
            p1projUI.GetComponent<Image>().sprite = Sprites[index];

        }
        else if(player == 2)
        {
            p2projUI.GetComponent<Image>().sprite = Sprites[index];
        }
    }

    public int GetProjectileCount()
    {
        return (projPrefabs.Length);
    }

    public GameObject GetSelectedProjectile(int i)
    {
        return projPrefabs[i];
    }

    public void SetInitialSprite()
    {
        p1projUI.GetComponent<Image>().sprite = Sprites[0];
        p2projUI.GetComponent<Image>().sprite = Sprites[0];
    }
}
