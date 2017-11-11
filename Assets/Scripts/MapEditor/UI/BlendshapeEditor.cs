using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendshapeEditor : MonoBehaviour
{

    [SerializeField]
    GameObject sliderPrefab;

    static BlendshapeEditor instance;

    SkinnedMeshRenderer currRend;
    InfoOnBlock currInfo;

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public static void ShowMenu(SkinnedMeshRenderer rend, InfoOnBlock info)
    {
        instance.currRend = rend;
        instance.currInfo = info;

        for (int i = 0; i < instance.transform.childCount; i++)
        {
            Destroy(instance.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < rend.sharedMesh.blendShapeCount; i++)
        {
            var go = Instantiate(instance.sliderPrefab);
            var sl = go.GetComponent<UnityEngine.UI.Slider>();
            sl.value = rend.GetBlendShapeWeight(i);
            go.GetComponentInChildren<UnityEngine.UI.Text>().text = rend.sharedMesh.GetBlendShapeName(i);
            var ind = i;
            sl.onValueChanged.AddListener(x => { instance.SetBlendshape(ind, x); });
            go.transform.SetParent(instance.transform);
        }

        instance.gameObject.SetActive(true);
    }

    void SetBlendshape(int id, float amount)
    {
        if (currRend != null && currInfo != null)
        {
            currRend.SetBlendShapeWeight(id, amount);
            if (currInfo.Blendshapes == null)
                currInfo.Blendshapes = new float[currRend.sharedMesh.blendShapeCount];

            currInfo.Blendshapes[id] = amount;
        }
    }

    public static void HideMenu()
    {
        instance.gameObject.SetActive(false);
    }

}
