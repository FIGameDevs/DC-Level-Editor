using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManipulator : MonoBehaviour
{
    [SerializeField]
    LayerMask selectMask;
    [SerializeField]
    GameObject highlight;
    [SerializeField]
    GameObject arrowHolder;


    public static bool moveInGrid { get; set; }
    public void SetMoveInGrid(bool s) { moveInGrid = s; }
    public static bool IsManipulating { get; private set; }
    public void SetManipulating(bool isM) { IsManipulating = isM; }
    GameObject selected;

    static BlockManipulator instance;

    private void Start() //TODO: Input na jedno místo, první pohyb a pak až selectování ty fagu
    {
        instance = this;
        moveInGrid = true;
        if (selected == null)
        {
            highlight.SetActive(false);
            arrowHolder.SetActive(false);
        }
        else
        {
            highlight.SetActive(true);
            arrowHolder.SetActive(true);
        }
    }

    public static void SelectItem(GameObject item)
    {
        instance.selected = item;
        instance.ShowHandles();

        if (instance.selected == null)
        {
            instance.highlight.SetActive(false);
            instance.arrowHolder.SetActive(false);
        }
        else
        {
            instance.highlight.SetActive(true);
            instance.arrowHolder.SetActive(true);
        }

    }

    public void DestroySelected()
    {
        if (selected != null)
        {
            Destroy(selected);
            highlight.SetActive(false);
            arrowHolder.SetActive(false);
        }
    }

    void ShowHandles()
    {
        Vector3 min = Vector3.one * 99999;
        Vector3 max = Vector3.one * -999999;

        var cols = selected.transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].bounds.min.x < min.x)
                min.x = cols[i].bounds.min.x;
            if (cols[i].bounds.min.y < min.y)
                min.y = cols[i].bounds.min.y;
            if (cols[i].bounds.min.z < min.z)
                min.z = cols[i].bounds.min.z;
            if (cols[i].bounds.max.x > max.x)
                max.x = cols[i].bounds.max.x;
            if (cols[i].bounds.max.y > max.y)
                max.y = cols[i].bounds.max.y;
            if (cols[i].bounds.max.z > max.z)
                max.z = cols[i].bounds.max.z;
        }
        highlight.transform.position = (min + max) / 2f;
        var size = max - min;
        highlight.transform.localScale = size;


        arrowHolder.transform.position = highlight.transform.position + new Vector3(highlight.transform.localScale.y / 2f + 0.15f, highlight.transform.localScale.y / 2f + 0.15f, highlight.transform.localScale.y / 2f + 0.15f);
    }

    private void LateUpdate()
    {
        IsManipulating = false;
    }

    public void MoveSelectedBy(Vector3 shift)
    {
        if (selected != null)
        {
            IsManipulating = true;
            if (moveInGrid)
            {
                shift = new Vector3(shift.x - shift.x % 4, shift.y - shift.y % 4, shift.z - shift.z % 4);
            }
            selected.transform.position += shift;
            highlight.transform.position += shift;
            arrowHolder.transform.position += shift;
        }

    }

    public void MoveSelectedTo(Vector3 pos)
    {
        if (selected != null)
        {
            IsManipulating = true;
            if (moveInGrid)
            {
                selected.transform.position = new Vector3(pos.x - pos.x % 4f, pos.y - pos.y % 3.6f, pos.z - pos.z % 4f);
            }
            else
            {
                selected.transform.position = pos;
            }
            ShowHandles();
        }

    }

    public Vector3 SelectedPos()
    {
        return selected == null ? Vector3.zero : selected.transform.position;
    }


    public void RotateSelectedBy(Vector3 angle)
    {
        if (selected != null)
        {
            if (moveInGrid)
            {
                angle.x -= angle.x % 45;
                angle.y -= angle.y % 45;
                angle.z -= angle.z % 45;
            }
            selected.transform.rotation = Quaternion.Euler(selected.transform.rotation.eulerAngles + angle);
            ShowHandles();
        }
    }
}
