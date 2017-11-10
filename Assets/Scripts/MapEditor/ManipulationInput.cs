using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationInput : MonoBehaviour
{
    [SerializeField]
    LayerMask objectMask;
    [SerializeField]
    LayerMask controlsMask;
    [SerializeField]
    GridPlaneControl grid;
    [SerializeField]
    BlockManipulator manip;

    MoveSelected moving;

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Delete))
            manip.DestroySelected();
        if (BlockManipulator.moveInGrid)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                manip.RotateSelectedBy(new Vector3(0, -45, 0));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                manip.RotateSelectedBy(new Vector3(0, 45, 0));
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Q))
            {
                manip.RotateSelectedBy(new Vector3(0, 30 * Time.deltaTime, 0));
            }
            else if (Input.GetKey(KeyCode.E))
            {
                manip.RotateSelectedBy(new Vector3(0, -30 * Time.deltaTime, 0));
            }
        }
        if (Physics.Raycast(ray, out hit, 100f, controlsMask))
        {
            if (Input.GetMouseButton(0))
                manip.SetManipulating(true);
            if (Input.GetMouseButtonDown(0))
            {
                var m = hit.collider.GetComponent<MoveSelected>();
                if (m != null)
                    m.StartMove();
                moving = m;
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (moving != null)
                moving.Move();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (moving != null)
                moving.EndMove();
            moving = null;
        }
        if (!BlockManipulator.IsManipulating && Physics.Raycast(ray, out hit, 100f, objectMask))
        {
            if (grid != null)
                grid.Move(hit.point);


        }
        if (!BlockManipulator.IsManipulating && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100f, objectMask))
            {
                BlockManipulator.SelectItem(hit.collider.gameObject.transform.root.gameObject);


            }
        }
    }
}
