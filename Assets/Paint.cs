using UnityEngine;
using UnityEngine.EventSystems;

public class Paint : MonoBehaviour
{
    public static Tool tool = Tool.pincel;
    public Transform dot;
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                switch (tool)
                {
                    case Tool.pincel:
                            Vector3 pos = new Vector3(hit.point.x, hit.point.y, 0);
                            Transform dotTransform = Instantiate(dot, pos, dot.rotation);
                            dotTransform.parent = transform;
                        break;
                    case Tool.eraser:
                        Debug.Log(objectHit.name);
                        if (objectHit.name.Contains("dot"))
                            Destroy(objectHit.gameObject);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void SetTool(int tool)
    {
        Paint.tool = (Tool) tool;
    }
    public void ClearBoard()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
public enum Tool
{
    pincel = 1,
    eraser = 0
}
