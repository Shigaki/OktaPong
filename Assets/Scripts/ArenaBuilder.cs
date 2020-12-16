using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBuilder : MonoBehaviour
{

    public int maxLayout;

    private GameObject arenaLayout;

    // Start is called before the first frame update
    void Start()
    {
        maxLayout = 5;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            BuildLayout(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            BuildLayout(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildLayout(2);
        }*/
    }

    private void DeleteLayout()
    {
        if (arenaLayout != null)
        {
            Destroy(arenaLayout);
            arenaLayout = null;
        }
    }

    private void CreateLayout()
    {
        if (arenaLayout == null)
        {
            arenaLayout = new GameObject();
            arenaLayout.transform.parent = transform;
            arenaLayout.transform.localPosition = new Vector3(0f, 0f, 0f);
            arenaLayout.name = "Layout";
        }
    }
    public void BuildRandomLayout()
    {
        BuildLayout(Random.Range(0, maxLayout));
    }

    public void BuildLayout(int layout)
    {
        DeleteLayout();
        CreateLayout();

        switch (layout)
        {  
            case 0:
                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
                go.transform.localScale = new Vector3(10f, 10f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";
                break;
            case 1:
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(0f, 5f, 0f);
                go.transform.localScale = new Vector3(12f, 2f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(0f, -5f, 0f);
                go.transform.localScale = new Vector3(12f, 2f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";
                break;
            case 2:
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(-7.5f, 5f, 0f);
                go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                go.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(7.5f, 5f, 0f);
                go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                go.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(-7.5f, -5f, 0f);
                go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                go.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(7.5f, -5f, 0f);
                go.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 45f));
                go.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
                go.transform.localScale = new Vector3(3f, 3f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";
                break;
            case 3:
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(-14f, 0f, 0f);
                go.transform.localScale = new Vector3(1f, 4f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(14f, 0f, 0f);
                go.transform.localScale = new Vector3(1f, 4f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(0f, 8f, 0f);
                go.transform.localScale = new Vector3(6f, 1f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(0f, -8f, 0f);
                go.transform.localScale = new Vector3(6f, 1f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";

                go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.parent = arenaLayout.transform;
                go.transform.localPosition = new Vector3(0f, 0f, 0f);
                go.transform.localScale = new Vector3(3f, 3f, 1f);
                go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Mat_Wall");
                go.tag = "Wall";
                break;
            case 4:
                break;
            default:
                break;

        }
    }
}
