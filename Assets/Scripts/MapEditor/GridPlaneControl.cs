using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlaneControl : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    Transform aimSphere;

    static GridPlaneControl instance;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        aimSphere.position = LookIntersect();
    }

    public void Move(Vector3 pos)
    {
        var y = pos.y - 0.1f;
        transform.position = new Vector3(transform.position.x, y - y % 3.6f, transform.position.z);
    }


    public static Vector3 LookIntersect()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        var plane = new Plane(Vector3.up, instance.transform.position + Vector3.up / 20f);
        float dist;
        if (plane.Raycast(ray, out dist))
            return ray.origin + ray.direction * dist;
        return Vector3.zero;
    }
}
