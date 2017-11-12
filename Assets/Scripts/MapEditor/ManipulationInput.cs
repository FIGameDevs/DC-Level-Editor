using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField]
    UnityEngine.UI.Toggle snapToggle;
    MoveSelected moving;
    void Update()
    {
        if (SaveAndLoad.GetInputField().isFocused)
        {
            return;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Shortcuts
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))
        {
            manip.NextVariation();
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
        {
            manip.PrevVariation();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            var infos = manip.GetSelectedInfos();
            BlockManipulator.DeselectAll();

            for (int i = 0; i < infos.Length; i++)
            {
                TileSpawner.Spawn(infos[i].Info, infos[i].transform.position + Vector3.back * 4f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            var infos = manip.GetSelectedInfos();
            BlockManipulator.DeselectAll();

            for (int i = 0; i < infos.Length; i++)
            {
                TileSpawner.Spawn(infos[i].Info, infos[i].transform.position + Vector3.forward * 4f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            var infos = manip.GetSelectedInfos();
            BlockManipulator.DeselectAll();

            for (int i = 0; i < infos.Length; i++)
            {
                TileSpawner.Spawn(infos[i].Info, infos[i].transform.position + Vector3.left * 4f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            var infos = manip.GetSelectedInfos();
            BlockManipulator.DeselectAll();

            for (int i = 0; i < infos.Length; i++)
            {
                TileSpawner.Spawn(infos[i].Info, infos[i].transform.position + Vector3.right * 4f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            var infos = manip.GetSelectedInfos();
            BlockManipulator.DeselectAll();

            for (int i = 0; i < infos.Length; i++)
            {
                TileSpawner.Spawn(infos[i].Info, infos[i].transform.position + Vector3.down * 4f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            var infos = manip.GetSelectedInfos();
            BlockManipulator.DeselectAll();

            for (int i = 0; i < infos.Length; i++)
            {
                TileSpawner.Spawn(infos[i].Info, infos[i].transform.position + Vector3.up * 4f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Delete))
            manip.DestroySelected();
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                manip.MoveSelectedBy(Vector3.left * 4f);
            else
                manip.MoveSelectedBy(Vector3.right * 4f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                manip.MoveSelectedBy(Vector3.down * 4f);
            else
                manip.MoveSelectedBy(Vector3.up * 4f);
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Y))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                manip.MoveSelectedBy(Vector3.back * 4f);
            else
                manip.MoveSelectedBy(Vector3.forward * 4f);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            manip.SetMoveInGrid(!BlockManipulator.moveInGrid);
            snapToggle.isOn = !snapToggle.isOn;
        }
        if (BlockManipulator.moveInGrid)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                manip.RotateSelectedBy(new Vector3(0, -90, 0));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                manip.RotateSelectedBy(new Vector3(0, 90, 0));
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


        if (EventSystem.current.IsPointerOverGameObject())//is mouse over UI
            return;

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
        
        if (!BlockManipulator.IsManipulating && Physics.Raycast(ray, out hit, 100f, objectMask))
        {
            if (grid != null)
                grid.Move(hit.point);


        }
        if (!BlockManipulator.IsManipulating && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100f, objectMask))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    BlockManipulator.AddSelectItem(hit.collider.gameObject.transform.root.gameObject);
                else
                    BlockManipulator.SelectItem(hit.collider.gameObject.transform.root.gameObject);


            }
        }
    }
}
