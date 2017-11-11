using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManipulator : MonoBehaviour
{
    [SerializeField]
    LayerMask selectMask;
    [SerializeField]
    GameObject highlightParent;
    [SerializeField]
    GameObject highlightPrefab;
    [SerializeField]
    GameObject arrowHolder;
    [SerializeField]
    GameObject selected;


    public static bool moveInGrid { get; set; }
    public void SetMoveInGrid(bool s) { moveInGrid = s; }
    public static bool IsManipulating { get; private set; }
    public void SetManipulating(bool isM) { IsManipulating = isM; }

    static BlockManipulator instance;

    private void Start() //TODO: Input na jedno místo, první pohyb a pak až selectování ty fagu
    {
        instance = this;
        moveInGrid = true;
        ShowHandles();
    }

    public static void AddSelectItem(GameObject item)
    {
        if (instance.selected.transform.childCount == 0)
            instance.selected.transform.position = item.transform.position;

        item.transform.SetParent(instance.selected.transform, true);

        instance.ShowHandles();
    }

    public static void SelectItem(GameObject item)
    {
        if (item == instance.selected)
        {
            DeselectAll();
            return;
        }
        instance.selected.transform.DetachChildren();
        instance.selected.transform.position = item.transform.position;

        item.transform.SetParent(instance.selected.transform, true);

        instance.ShowHandles();
    }

    public static void DeselectAll()
    {
        instance.selected.transform.DetachChildren();
        instance.ShowHandles();
    }

    public void NextVariation()
    {
        ChangeVariation(1);
    }
    public void PrevVariation()
    {
        ChangeVariation(-1);
    }

    public void ChangeVariation(int shift)
    {
        if (selected.transform.childCount == 0)
            return;
        var infoBlocks = selected.GetComponentsInChildren<InfoOnBlock>();
        DestroySelected();
        for (int i = 0; i < infoBlocks.Length; i++)
        {
            var pos = infoBlocks[i].transform.position;
            var info = infoBlocks[i].Info;
            info.Variation += shift;
            TileSpawner.Spawn(info, pos);
        }
    }

    public void DestroySelected()
    {
        if (selected != null)
        {
            while (selected.transform.childCount != 0)
            {
                var tr = selected.transform.GetChild(0);
                tr.SetParent(null);
                Destroy(tr.gameObject);
            }
            ShowHandles();
        }
    }

    void ShowHandles()
    {
        if (selected.transform.childCount == 0)
        {
            highlightParent.SetActive(false);
            arrowHolder.SetActive(false);
            return;
        }
        else
        {
            highlightParent.SetActive(true);
            arrowHolder.SetActive(true);
        }

        for (int i = 0; i < highlightParent.transform.childCount; i++)
        {
            Destroy(highlightParent.transform.GetChild(i).gameObject);
        }

        for (int j = 0; j < selected.transform.childCount; j++)
        {
            var child = selected.transform.GetChild(j);

            Vector3 min = Vector3.one * 99999;
            Vector3 max = Vector3.one * -999999;

            var cols = child.GetComponentsInChildren<Collider>();
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
            var go = Instantiate(highlightPrefab);

            go.transform.position = (min + max) / 2f;
            var size = max - min;
            go.transform.localScale = size;
            go.transform.SetParent(highlightParent.transform);

            if (j == 0)
            {
                arrowHolder.transform.position = go.transform.position + size/2f;
            }
        }
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
                shift = new Vector3(Round.ToFour(shift.x), Round.ToThreePointSix(shift.y), Round.ToFour(shift.z));
            }
            selected.transform.position += shift;
            ShowHandles();
        }

    }

    public void MoveSelectedTo(Vector3 pos)
    {
        if (selected != null)
        {
            IsManipulating = true;
            if (moveInGrid)
            {
                //selected.transform.position = new Vector3(pos.x - pos.x % 4f, pos.y - pos.y % 3.6f, pos.z - pos.z % 4f);
                selected.transform.position = new Vector3(Round.ToFour(pos.x), Round.ToThreePointSix(pos.y), Round.ToFour(pos.z));

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


    public InfoOnBlock[] GetSelectedInfos()
    {
        return selected.GetComponentsInChildren<InfoOnBlock>();
    }
}
