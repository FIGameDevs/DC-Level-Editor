using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelected : MonoBehaviour
{

    [SerializeField]
    BlockManipulator manipulator;
    [SerializeField]
    Vector3 axis;

    Vector3 startPos = Vector3.zero;
    Vector3 offset;

    public void StartMove()
    {
        var selPos = manipulator.SelectedPos();
        var plane = new Plane(Vector3.Cross(axis, new Vector3(axis.y, -axis.z, axis.x)), selPos);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (plane.Raycast(ray, out enter))
        {

            var fromPos = ray.origin + ray.direction * enter;
            startPos = manipulator.SelectedPos();
            offset = fromPos - startPos;
        }

    }

    public void Move()
    {

        var selPos = manipulator.SelectedPos();
        var plane = new Plane(Vector3.Cross(axis, new Vector3(axis.y, -axis.z, axis.x)), selPos);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (plane.Raycast(ray, out enter))
        {

            var rayPos = ray.origin + ray.direction * enter;
            var posDelta = rayPos - startPos;
            Vector3 newPos;
            newPos = selPos + new Vector3((posDelta.x - offset.x) * axis.x, (posDelta.y - offset.y) * axis.y, (posDelta.z - offset.z) * axis.z);
            manipulator.MoveSelectedTo(newPos);
            startPos = newPos;
        }
    }
    public void EndMove()
    {
        startPos = Vector3.zero;
    }

}
