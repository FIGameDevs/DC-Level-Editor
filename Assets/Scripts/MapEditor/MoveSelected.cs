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

    public void StartMove()
    {
        var selPos = manipulator.SelectedPos();
        var plane = new Plane(Vector3.Cross(axis, new Vector3(axis.y, -axis.z, axis.x)), selPos);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (plane.Raycast(ray, out enter))
        {

            startPos = ray.origin + ray.direction * enter - selPos;
        }
    }

    public void Move()
    {

        if (startPos != Vector3.zero)
        {
            var selPos = manipulator.SelectedPos();
            var plane = new Plane(Vector3.Cross(axis, new Vector3(axis.y, -axis.z, axis.x)), selPos);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter;
            if (plane.Raycast(ray, out enter))
            {

                var rayPos = ray.origin + ray.direction * enter;
                var posDelta = rayPos - selPos;
                var newPos = selPos + new Vector3(posDelta.x * axis.x + (-startPos.x) * axis.x, posDelta.y * axis.y + (-startPos.y) * axis.y, posDelta.z * axis.z + (-startPos.z) * axis.z);
                manipulator.MoveSelectedTo(newPos);
            }

        }

    }
    public void EndMove()
    {
        startPos = Vector3.zero;
    }

}
