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

	private void Start ()
	{
		instance = this;
		gameObject.SetActive (false);
	}

	public static void ShowMenu (SkinnedMeshRenderer rend, InfoOnBlock info)
	{
		instance.currRend = rend;
		instance.currInfo = info;

		for (int i = 0; i < instance.transform.childCount; i++) {
			Destroy (instance.transform.GetChild (i).gameObject);
		}
		for (int i = 0; i < rend.sharedMesh.blendShapeCount; i++) {
			var go = Instantiate (instance.sliderPrefab);
			var sl = go.GetComponent<UnityEngine.UI.Slider> ();
			sl.value = rend.GetBlendShapeWeight (i);
			go.GetComponentInChildren<UnityEngine.UI.Text> ().text = rend.sharedMesh.GetBlendShapeName (i);
			var ind = i;
			sl.onValueChanged.AddListener (x => {
				instance.SetBlendshape (ind, x);
			});
			go.transform.SetParent (instance.transform);
		}

		instance.gameObject.SetActive (true);
	}

	void SetBlendshape (int id, float amount)
	{
		try {
			var allInfos = BlockManipulator.GetSelectedInfos ();
			for (int i = 0; i < allInfos.Length; i++) {
				var rends = allInfos [i].GetComponentsInChildren<SkinnedMeshRenderer> ();
				if (rends.Length == 0)
					continue;
				if (allInfos [i].Blendshapes == null)
					allInfos [i].Blendshapes = new float[currRend.sharedMesh.blendShapeCount];
				allInfos [i].Blendshapes [id] = amount;
				for (int j = 0; j < rends.Length; j++) {
					rends [j].SetBlendShapeWeight (id, amount);
				}
			}
		} catch (System.Exception ex) {
			Debug.Log ("Error while setting blendshapes, probably models with different Blendshape counts");
			Debug.Log (ex);
		}
	}

	public static void ApplyAll (SkinnedMeshRenderer rend, InfoOnBlock info)
	{
		if (rend != null && info != null && info.Blendshapes != null) {
			for (int i = 0; i < info.Blendshapes.Length; i++) {
				rend.SetBlendShapeWeight (i, info.Blendshapes [i]);
				if (info.Blendshapes == null)
					info.Blendshapes = new float[rend.sharedMesh.blendShapeCount];
			}
		}


	}

	public static void HideMenu ()
	{
		instance.gameObject.SetActive (false);
	}

}
